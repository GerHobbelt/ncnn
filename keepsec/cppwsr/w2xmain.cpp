// waifu2x implemented with ncnn library

//#include "simptoolextern.h"

typedef struct scalpp
{
	float DstSize;
	char model;
	char mdl;
	char skl;
	char noiz;
} ScaleParam;



enum
{
	_first8to32_ = 1,
	_mid32to32_ = 0,
	_end32to8_ = 2,
	_simp8to8_ = 3

};

extern int stepinit;
extern ScaleParam ScaleSteps[8];
extern char FillScaleParam(ScaleParam* dst, float skale);


#include <stdio.h>
#include <algorithm>
#include <queue>
#include <vector>
#include <clocale>
#include <iostream>


// image decoder and encoder with wic
#include "wic_image.h"

#include "webp_image.h"

#if _WIN32
#define Sleep_1024_ Sleep(1024)
#else
#define Sleep_1024_ sleep(1)
#endif


#include <wchar.h>
static wchar_t* optarg = NULL;
static int optind = 1;
static wchar_t getopt(int argc, wchar_t* const argv[], const wchar_t* optstring)
{
    if (optind >= argc || argv[optind][0] != L'-')
        return -1;

    wchar_t opt = argv[optind][1];
    const wchar_t* p = wcschr(optstring, opt);
    if (p == NULL)
        return L'?';

    optarg = NULL;

    if (p[1] == L':')
    {
        optind++;
        if (optind >= argc)
            return L'?';

        optarg = argv[optind];
    }

    optind++;

    return opt;
}

static std::vector<int> parse_optarg_int_array(const wchar_t* optarg)
{
    std::vector<int> array;
    array.push_back(_wtoi(optarg));

    const wchar_t* p = wcschr(optarg, L',');
    while (p)
    {
        p++;
        array.push_back(_wtoi(p));
        p = wcschr(p, L',');
    }

    return array;
}


// ncnn
#include <cpu.h>
#include <gpu.h>
#include <platform.h>

#include "waifu2x.h"

#include "filesystem_utils.h"


bool i16out = false;

static void print_usage()
{
    fprintf(stdout, "Usage: waifu2x-ncnn-vulkan -i infile -o outfile [options]...\n\n");
    fprintf(stdout, "  -h                   show this help\n");
    fprintf(stdout, "  -v                   verbose output\n");
    fprintf(stdout, "  -i input-path        input image path (jpg/png/webp) or directory\n");
    fprintf(stdout, "  -o output-path       output image path (jpg/png/webp) or directory\n");
    fprintf(stdout, "  -n noise-level       denoise level (-1/0/1/2/3, default=0)\n");
    fprintf(stdout, "  -s scale             upscale ratio (1/2, default=2)\n");
    fprintf(stdout, "  -t tile-size         tile size (>=32/0=auto, default=0) can be 0,0,0 for multi-gpu\n");
    fprintf(stdout, "  -m model-path        waifu2x model path (default=models-cunet)\n");
    fprintf(stdout, "  -g gpu-id            gpu device to use (default=auto) can be 0,1,2 for multi-gpu\n");
    fprintf(stdout, "  -j load:proc:save    thread count for load/proc/save (default=1:2:2) can be 1:2,2,2:2 for multi-gpu\n");
    fprintf(stdout, "  -x                   enable tta mode\n");
    fprintf(stdout, "  -f format            output image format (jpg/png/webp, default=ext/png)\n");
}



class Task
{
public:
    int id;
    int webp;

    path_t inpath;
    path_t outpath;

	int StepRem;
	ScaleParam* param;


    ncnn::Mat inimage;
    ncnn::Mat outimage;
};

class TaskQueue
{
public:
    TaskQueue()
    {
    }

    void put(const Task& v)
    {
        lock.lock();

        while (tasks.size() >= 8) // FIXME hardcode queue length
        {
            condition.wait(lock);
        }

        tasks.push(v);

        lock.unlock();

        condition.signal();
    }

    void get(Task& v)
    {
        lock.lock();

        while (tasks.size() == 0)
        {
            condition.wait(lock);
        }

        v = tasks.front();
        tasks.pop();

		if (v.id == -233&& tasks.size()>0)
		{
			tasks.push(v);
			v = tasks.front();
			tasks.pop();
		}

        lock.unlock();

        condition.signal();
    }

	int taskcount()
	{
	

		return tasks.size();
	}

	bool isEmpty()
	{
		return tasks.size()==0;
	}

private:
    ncnn::Mutex lock;
    ncnn::ConditionVariable condition;
    std::queue<Task> tasks;
};

TaskQueue toproc;
TaskQueue tosave;

class LoadThreadParams
{
public:
    int scale;
    int jobs_load;

    // session data
    std::vector<path_t> input_files;
    std::vector<path_t> output_files;
};


int tskcot = 0;

void* load(void* args)
{
    const LoadThreadParams* ltp = (const LoadThreadParams*)args;
    const int count = ltp->input_files.size();
    const int scale = ltp->scale;
	

    #pragma omp parallel for schedule(static,1) num_threads(ltp->jobs_load)
    for (int i=0; i<count; i++)
    {
        const path_t& imagepath = ltp->input_files[i];

        int webp = 0;

        unsigned char* pixeldata = 0;
        int w;
        int h;
        int c;


        FILE* fp = _wfopen(imagepath.c_str(), L"rb");

        if (fp)
        {
            // read whole file
            unsigned char* filedata = 0;
            int length = 0;
            {
                fseek(fp, 0, SEEK_END);
                length = ftell(fp);
                rewind(fp);
                filedata = (unsigned char*)malloc(length);
                if (filedata)
                {
                    fread(filedata, 1, length, fp);
                }
                fclose(fp);
            }

            if (filedata)
            {
                pixeldata = webp_load(filedata, length, &w, &h, &c);
                if (pixeldata)
                {
                    webp = 1;
                }
                else
                {
                    // not webp, try jpg png etc.

                    pixeldata = wic_decode_image(imagepath.c_str(), &w, &h, &c);

                }

                free(filedata);
            }
        }

		//printf("\n%ls_onread?=%d\n", imagepath.c_str(), c);

        if (pixeldata)
        {
            Task v;
            v.id = tskcot+i*4;
            v.webp = webp;
            v.inpath = imagepath;
            v.outpath = ltp->output_files[i];


			
            v.inimage = ncnn::Mat(w, h, (void*)pixeldata, (size_t)c,c);



           

			//printf("onload, elepack=%d", v.outimage.elempack);

			v.StepRem = stepinit;
			v.param = ScaleSteps;

			//========
			
			if (stepinit == 1)
			{
				if (i16out)
				{
					v.outimage = ncnn::Mat(w * scale, h * scale, (int)c, (size_t)2);
				}
				else
				{
					v.outimage = ncnn::Mat(w * scale, h * scale, (size_t)c, c);
				}
				
			}
			else
			{
				float dstsk = ScaleSteps[0].DstSize;
				float wji = w * scale;
				float hji = h * scale;
				wji = wji * dstsk+0.5f;
				hji = hji * dstsk + 0.5f;
				v.outimage = ncnn::Mat((int)wji, (int)hji, (int)c, (size_t)2);
			}
			

			// in.create(in_tile_w, in_tile_h, RGB_CHANNELS, sizeof(float));
			//========


			

			//v.outimage.c = 3;
			//v.inimage.c = 3;
			//printf("\nC_putV=%d\n", v.outimage.c);

            path_t ext = get_file_extension(v.outpath);
            if (c == 4 && (ext == PATHSTR("jpg") || ext == PATHSTR("JPG") || ext == PATHSTR("jpeg") || ext == PATHSTR("JPEG")))
            {
                path_t output_filename2 = ltp->output_files[i] + PATHSTR(".png");
                v.outpath = output_filename2;

                fwprintf(stderr, L"image %ls has alpha channel ! %ls will output %ls\n", imagepath.c_str(), imagepath.c_str(), output_filename2.c_str());

            }

            toproc.put(v);

			
        }
        else
        {

            fwprintf(stderr, L"decode image %ls failed\n", imagepath.c_str());

        }
    }

	tskcot += count * 4;
    return 0;
}

class ProcThreadParams
{
public:
    Waifu2x* waifu2x;
};


void putback(Task v_orig, ncnnNetPack* modelscale)
{
	Task v;
	v.id = v_orig.id + 1;
	v.webp = v_orig.webp;
	v.inpath = v_orig.inpath;
	v.outpath = v_orig.outpath;
	v.StepRem = v_orig.StepRem - 1;
	v.param = v_orig.param + 1;

	int ow = v_orig.outimage.w;
	int oh = v_orig.outimage.h;
	int oc = v_orig.outimage.dims;
	size_t oelep = v_orig.outimage.elempack;
	

	



	v.inimage = v_orig.outimage;		//ncnn::Mat(ow, oh, odata, oelep, oc);

	int mdskal = modelscale[v.param->model].scale;


	
	

	if (v.StepRem < 2)
	{
		//printf("\nstep%d, out=int8\n", v.StepRem);

		if (i16out)
		{
			v.outimage = ncnn::Mat(ow*mdskal, oh*mdskal, (int)oc, (size_t)2);
		}
		else
		{
			v.outimage = ncnn::Mat(ow*mdskal, oh*mdskal, (size_t)oc, (int)oc);
		}
			
		
	}
	else
	{
		float dstsk = v.param->DstSize;
		float wji = ow * mdskal;
		float hji = oh * mdskal;
		wji = wji * dstsk + 0.5f;
		hji = hji * dstsk + 0.5f;
		//printf("\nstep%d, out=fp16\n", v.StepRem);
		v.outimage = ncnn::Mat((int)(wji), (int)(hji), v_orig.outimage.c, (size_t)2);
	}


	//=======


	v_orig.inimage.release();
	//free(v_orig.inimage.data);

	toproc.put(v);
}


void* proc(void* args)
{
	//puts("manyproc?");
    const ProcThreadParams* ptp = (const ProcThreadParams*)args;
    Waifu2x* waifu2x = ptp->waifu2x;



    for (;;)
    {
        Task v;

        toproc.get(v);

		if (v.id == -233)
		{
			
				break;
			
		}
		
			


		auto tup = _mid32to32_;
		if (stepinit == 1)
		{
			tup = _simp8to8_;
		}
		else if (v.StepRem == stepinit)
		{
			tup = _first8to32_;
		}
		else if (v.StepRem == 1)
		{
			tup = _end32to8_;
		}

		
		

	
        waifu2x->process(v.inimage, v.outimage,v.param[0], tup);


		

		if (v.StepRem > 1)
		{
			putback(v, waifu2x->nets);
		}
		else
		{
			//printf("\nsvvinroc=%x\n",v.outimage.data);
			tosave.put(v);
		}

        
    }

    return 0;
}

class SaveThreadParams
{
public:
    int verbose;
};

void* save(void* args)
{
    const SaveThreadParams* stp = (const SaveThreadParams*)args;
    const int verbose = stp->verbose;
	

	for (;;)
	{
		Task v;

		tosave.get(v);

		if (v.id == -233)
		{


			break;

		}




		// free input pixel data
		{
			unsigned char* pixeldata = (unsigned char*)v.inimage.data;
			if (v.webp == 1)
			{
				v.inimage.release();
				//free(pixeldata);
			}
			else
			{
				v.inimage.release();
				//free(pixeldata);

			}
		}


		if (i16out)
		{
			
			PGM16save((v.outpath+ PATHSTR(".ppm")).c_str(), v.outimage.w, v.outimage.h, v.outimage.c, (const unsigned char*)v.outimage.data);
		}
		else
		{
		int success = 0;

		path_t ext = get_file_extension(v.outpath);

		if (ext == PATHSTR("webp") || ext == PATHSTR("WEBP"))
		{
			success = webp_save(v.outpath.c_str(), v.outimage.w, v.outimage.h, v.outimage.elempack, (const unsigned char*)v.outimage.data);
		}
		else if (ext == PATHSTR("png") || ext == PATHSTR("PNG"))
		{

			success = wic_encode_image(v.outpath.c_str(), v.outimage.w, v.outimage.h, v.outimage.elempack, v.outimage.data);

		}
		else if (ext == PATHSTR("jpg") || ext == PATHSTR("JPG") || ext == PATHSTR("jpeg") || ext == PATHSTR("JPEG"))
		{

			success = wic_encode_jpeg_image(v.outpath.c_str(), v.outimage.w, v.outimage.h, v.outimage.elempack, v.outimage.data);

		}
		if (success)
		{
			if (verbose)
			{

				fwprintf(stdout, L"%ls -> %ls done\n", v.inpath.c_str(), v.outpath.c_str());

			}
		}
		else
		{

			fwprintf(stderr, L"encode image %ls failed\n", v.outpath.c_str());

		}
	}
    }

    return 0;
}

std::vector<path_t> inout_files[2];

int wmain(int argc, wchar_t** argv)
{
    path_t inputpath=PATHSTR(".\\tibr_o.jpg");;
    path_t outputpath=PATHSTR(".\\output.png");;
    int noise = 1;
   
    std::vector<int> tilesize;
	int model = 1;//PATHSTR("models-cunet");
    std::vector<int> gpuid;
    int jobs_load = 1;
    std::vector<int> jobs_proc;
    int jobs_save = 2;
    int verbose = 0;
    int tta_mode = 0;
    path_t format = PATHSTR("png");
	float kscale = 2.0f;

    setlocale(LC_ALL, "");
    wchar_t opt;
    while ((opt = getopt(argc, argv, L"i:o:n:s:t:m:g:j:f:vxh")) != (wchar_t)-1)
    {
        switch (opt)
        {
        case L'i':
            inputpath = optarg;
            break;
        case L'o':
            outputpath = optarg;
            break;
		case L'k':
			kscale = (float)_wtoi(optarg);
        case L'n':
            noise = _wtof(optarg);
            break;
        case L't':
            tilesize = parse_optarg_int_array(optarg);
            break;
        case L'm':
            model = (int)(optarg[0])-0x60;
            break;
        case L'g':
            gpuid = parse_optarg_int_array(optarg);
            break;
        case L'j':
            swscanf(optarg, L"%d:%*[^:]:%d", &jobs_load, &jobs_save);
            jobs_proc = parse_optarg_int_array(wcschr(optarg, L':') + 1);
            break;
        case L'f':
            format = optarg;
            break;
        case L'v':
            verbose = 1;
            break;
        case L'x':
            tta_mode = 1;
            break;
        case L'h':
        default:
            print_usage();
            return -1;
        }
    }

	const bool maxuse=(FillScaleParam(ScaleSteps, kscale)>(char)0);

	int scale = ScaleSteps[ScaleSteps[0].model].skl;

    if (inputpath.empty() || outputpath.empty())
    {
        print_usage();
        return -1;
    }

    if (noise < -1 || noise > 3)
    {
        fprintf(stderr, "invalid noise argument\n");
        return -1;
    }

    if (scale < 1 || scale > 2)
    {
        fprintf(stderr, "invalid scale argument\n");
        return -1;
    }

    if (tilesize.size() != (gpuid.empty() ? 1 : gpuid.size()) && !tilesize.empty())
    {
        fprintf(stderr, "invalid tilesize argument\n");
        return -1;
    }

    for (int i=0; i<(int)tilesize.size(); i++)
    {
        if (tilesize[i] != 0 && tilesize[i] < 32)
        {
            fprintf(stderr, "invalid tilesize argument\n");
            return -1;
        }
    }

    if (jobs_load < 1 || jobs_save < 1)
    {
        fprintf(stderr, "invalid thread count argument\n");
        return -1;
    }

    if (jobs_proc.size() != (gpuid.empty() ? 1 : gpuid.size()) && !jobs_proc.empty())
    {
        fprintf(stderr, "invalid jobs_proc thread count argument\n");
        return -1;
    }

    for (int i=0; i<(int)jobs_proc.size(); i++)
    {
        if (jobs_proc[i] < 1)
        {
            fprintf(stderr, "invalid jobs_proc thread count argument\n");
            return -1;
        }
    }

    if (!path_is_directory(outputpath))
    {
        // guess format from outputpath no matter what format argument specified
        path_t ext = get_file_extension(outputpath);

        if (ext == PATHSTR("png") || ext == PATHSTR("PNG"))
        {
            format = PATHSTR("png");
        }
        else if (ext == PATHSTR("webp") || ext == PATHSTR("WEBP"))
        {
            format = PATHSTR("webp");
        }
        else if (ext == PATHSTR("jpg") || ext == PATHSTR("JPG") || ext == PATHSTR("jpeg") || ext == PATHSTR("JPEG"))
        {
            format = PATHSTR("jpg");
        }
        else
        {
            fprintf(stderr, "invalid outputpath extension type\n");
            return -1;
        }
    }

    if (format != PATHSTR("png") && format != PATHSTR("webp") && format != PATHSTR("jpg"))
    {
        fprintf(stderr, "invalid format argument\n");
        return -1;
    }

    // collect input and output filepath
    
    {
	

		if (FillPathList(inputpath, outputpath,format, inout_files) < 0)
			return -1;
    }

    


    

    //path_t paramfullpath = sanitize_filepath(parampath);
    //path_t modelfullpath = sanitize_filepath(modelpath);


    CoInitializeEx(NULL, COINIT_MULTITHREADED);


    ncnn::create_gpu_instance();

    if (gpuid.empty())
    {
        gpuid.push_back(ncnn::get_default_gpu_index());
    }

	const int use_gpu_count = (int)gpuid.size();

    if (jobs_proc.empty())
    {
        jobs_proc.resize(use_gpu_count, 2);
    }

    if (tilesize.empty())
    {
        tilesize.resize(use_gpu_count, 0);
    }

    int cpu_count = std::max(1, ncnn::get_cpu_count());
    jobs_load = std::min(jobs_load, cpu_count);
    jobs_save = std::min(jobs_save, cpu_count);

    int gpu_count = ncnn::get_gpu_count();
    for (int i=0; i<use_gpu_count; i++)
    {
        if (gpuid[i] < 0 || gpuid[i] >= gpu_count)
        {
            fprintf(stderr, "invalid gpu device\n");

            ncnn::destroy_gpu_instance();
            return -1;
        }
    }

    int total_jobs_proc = 0;
    for (int i=0; i<use_gpu_count; i++)
    {
        int gpu_queue_count = ncnn::get_gpu_info(gpuid[i]).compute_queue_count;
        jobs_proc[i] = std::min(jobs_proc[i], gpu_queue_count);
        total_jobs_proc += jobs_proc[i];
    }

    for (int i=0; i<use_gpu_count; i++)
    {
        if (tilesize[i] != 0)
            continue;

        uint32_t heap_budget = ncnn::get_gpu_device(gpuid[i])->get_heap_budget();

        // more fine-grained tilesize policy here
        if (model==1)
        {
            if (heap_budget > 2600)
                tilesize[i] = 400;
            else if (heap_budget > 740)
                tilesize[i] = 200;
            else if (heap_budget > 250)
                tilesize[i] = 100;
            else
                tilesize[i] = 32;
        }
        else if (model==2
            || model==3)
        {
            if (heap_budget > 1900)
                tilesize[i] = 400;
            else if (heap_budget > 550)
                tilesize[i] = 200;
            else if (heap_budget > 190)
                tilesize[i] = 100;
            else
                tilesize[i] = 32;
        }
    }

    {
        std::vector<Waifu2x*> waifu2x(use_gpu_count);

        for (int i=0; i<use_gpu_count; i++)
        {
            waifu2x[i] = new Waifu2x(gpuid[i], tta_mode);

			waifu2x[i]->load(ScaleSteps[0].mdl-0x60, ScaleSteps[0].skl, ScaleSteps[0].noiz);

			if (maxuse)
			{
				puts("use more than 1");
				for (int jjp = 1; jjp < 3; jjp++)
				{
					if (ScaleSteps[jjp].mdl != (char)0xFF)
						waifu2x[i]->load(ScaleSteps[jjp].mdl - 0x60, ScaleSteps[jjp].skl, ScaleSteps[jjp].noiz, jjp);
				}
			}
            

		

       
            waifu2x[i]->tilesize = tilesize[i];
            
        }

        // main routine
		maruta:
        {
            // load image
            LoadThreadParams ltp;
            ltp.scale = scale;
            ltp.jobs_load = jobs_load;
            ltp.input_files = inout_files[0];
            ltp.output_files = inout_files[1];
		

            ncnn::Thread load_thread(load, (void*)&ltp);

            // waifu2x proc
            std::vector<ProcThreadParams> ptp(use_gpu_count);
            for (int i=0; i<use_gpu_count; i++)
            {
                ptp[i].waifu2x = waifu2x[i];
            }

            std::vector<ncnn::Thread*> proc_threads(total_jobs_proc);
            {
                int total_jobs_proc_id = 0;
                for (int i=0; i<use_gpu_count; i++)
                {
                    for (int j=0; j<jobs_proc[i]; j++)
                    {
                        proc_threads[total_jobs_proc_id++] = new ncnn::Thread(proc, (void*)&ptp[i]);
                    }
                }
            }

            // save image
            SaveThreadParams stp;
            stp.verbose = verbose;

            std::vector<ncnn::Thread*> save_threads(jobs_save);
            for (int i=0; i<jobs_save; i++)
            {
                save_threads[i] = new ncnn::Thread(save, (void*)&stp);
            }

            // end
            load_thread.join();
			

			


			/*
			while (true)
			{
				slleep:
				Sleep_1024_;
				
				
				if ((toproc.isEmpty() && tosave.isEmpty()))
				{
					Sleep_1024_;

					
					if (!(toproc.isEmpty() && tosave.isEmpty()))
					{
						goto slleep;
					}
					

					break;
				}
			}
			*/
			
			Task end;
            end.id = -233;

            for (int i=0; i<total_jobs_proc; i++)
            {
                toproc.put(end);
            }

            for (int i=0; i<total_jobs_proc; i++)
            {
                proc_threads[i]->join();
                delete proc_threads[i];
            }

            for (int i=0; i<jobs_save; i++)
            {
                tosave.put(end);
            }

            for (int i=0; i<jobs_save; i++)
            {
                save_threads[i]->join();
                delete save_threads[i];
            }
        }

		
		if (isDIR)
		{
			
			loopwait:
			int gff = FillPathList(inputpath, outputpath, format, inout_files);
			//printf("\ngetgff=%d\n",gff);
			if (gff > 0)
			{
				//printf("\ndolppodir\n");
				goto maruta;
			}
			else if (gff < 0)
				goto byebye;
			else
			{
				Sleep_1024_;
				goto loopwait;
			}
				

			
		
		
		}
		byebye:

			

		

		

        for (int i=0; i<use_gpu_count; i++)
        {
            delete waifu2x[i];
        }
        waifu2x.clear();
    }

    ncnn::destroy_gpu_instance();

	
    return 0;
}
