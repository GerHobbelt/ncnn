
using System;
using System.Runtime;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using KT;
using U3D;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
//using MessagePack;

namespace G1conso
{
	class Program
	{
		public static G1M gg;
		
		public static void Main(string[] args)
		{
			//testsklpa();
			tztw2x();
			return;
			//@"D:\TDDOWNLOAD\Aunit\mikns2\LoveR_Maki_Data"
			//UnityAsset.LoadAB(@"D:\TDDOWNLOAD\cm3d2\ph\abdata\map");
			//UnityAsset.ToYAML(@"Q:\ha\zempti\zvzko");
			
			
			
			
			/*
			testyamlbase();
			//testsefoo();
			return;
			*/
			
			/*
			bswap byby = new bswap();
			byby.thewhole = 0x80000000;
			byby._ROL(0,1);
			Console.WriteLine(byby.thewhole.ToString("X8"));
			
			//bswap.xadd1(5,7);
			return;
			*/
			
			var kali = G1pkg.Create("tt.bin_BE");	//_BE
			gg = kali.m_list[0];
			
			
			/*
			int ll=gg.iG1MG.s6.map.Length;
			
			for(int i=0;i<ll;i++)
			{
				Console.WriteLine("s6Block"+i);
				Console.WriteLine(gg.iG1MG.s6.map[i].print());
			}
			*/
			
			printvtx();
			//tztkla();
			
			//chkset6();
			
			
		}
		
		public static uint FloorLog2(uint x)
		{
			x |= (x >> 1);
			x |= (x >> 2);
			x |= (x >> 4);
			x |= (x >> 8);
			x |= (x >> 16);

			return (uint)(NumBitsSet(x) - 1);
		}

		public static uint CeilingLog2(uint x)
		{
			int y = (int)(x & (x - 1));

			y |= -y;
			y >>= (WORDBITS - 1);
			x |= (x >> 1);
			x |= (x >> 2);
			x |= (x >> 4);
			x |= (x >> 8);
			x |= (x >> 16);

			return (uint)(NumBitsSet(x) - 1 - y);
		}

		public static int NumBitsSet(uint x)
		{
			x -= ((x >> 1) & 0x55555555);
			x = (((x >> 2) & 0x33333333) + (x & 0x33333333));
			x = (((x >> 4) + x) & 0x0f0f0f0f);
			x += (x >> 8);
			x += (x >> 16);

			return (int)(x & 0x0000003f);
		}

		private const int WORDBITS = 32;

		
		[DllImport("W2XLIB.dll", CharSet = CharSet.Unicode)]
		public static extern void InOutList(int count, string[] in_paths, string[] out_paths);
		[DllImport("W2XLIB.dll", CharSet = CharSet.Unicode)]
		public static extern int runW2X(int argc, string[] argv);
		[DllImport("W2XLIB.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern byte* wic_decode_image(string File, ref int w, ref int h, ref int c);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int FindFileFunc(IntPtr inpath, IntPtr outpath, IntPtr fmt, IntPtr vlist);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public unsafe delegate void EncodeFunc(int IOid, int width, int height, int channel, int is16bit, byte* data);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public unsafe delegate byte* DecodeFunc(int IOid, ref int width, ref int height, ref int channel, ref int is16bit, ref int ScaleParamLen, ref ScaleParam[] newparam);
		
		
		
		[DllImport("W2XLIB.dll")]
		public static extern void SetFindFileFunc(FindFileFunc func, bool setisDir);
		
		[DllImport("W2XLIB.dll")]
		public static extern void SetEncodeFunc(EncodeFunc func);
		
		[DllImport("W2XLIB.dll")]
		public static extern int SetDecodeFunc(DecodeFunc func);
		
		
		
	 	
		[Serializable]
		[StructLayout(LayoutKind.Explicit, Size = 8)]
		public struct ScaleParam
		{
			[FieldOffset(0)] public float DstScale;
			[FieldOffset(4)] public byte model;
			[FieldOffset(5)] public byte x1;
			[FieldOffset(6)] public byte x2;
			[FieldOffset(7)] public byte x3;
			
		}
		
		static string w2xintztdir = @"E:\r\cud\ncnn\src\bin\Release\tiztk";
		static string w2xouttztdir = @"E:\r\cud\ncnn\src\bin\Release\tztouty";
		
		static string[] InNames;
		static string[] OutNames;
		static ScaleParam[][] sparams;
		static double basicUpSample = 2;
		
		static unsafe void tztw2x()
		{
			SetFindFileFunc(findfunc_example, true);
			basicUpSample = (double)SetDecodeFunc(decfunc_example);
			//SetEncodeFunc(encfunc_example);
			string[] cmdlines = { string.Empty, "-i", w2xintztdir, "-o", w2xouttztdir };
			runW2X(cmdlines.Length, cmdlines);
		}
		
		static void testsklpa()
		{
			kjj:
			string ff = Console.ReadLine();
			
			if (ff[0] == 'x')
				return;
			
			string[] ffp = ff.Split(',');
			
			ScaleParam[] syk;
			if (ffp.Length == 2) {
				syk = makesparam(166, 250, dstw: int.Parse(ffp[0]), dsth: int.Parse(ffp[1]));
			} else {
				syk = makesparam(0, 0, scale: double.Parse(ffp[0]));
			}
			
			int steps = syk.Length;
			
			Console.WriteLine(steps + " steps");
			for (int i = 0; i < steps; i++) {
				Console.WriteLine(syk[i].DstScale + "\t//" + (2.0f * syk[i].DstScale));
			}
			goto kjj;
		}
		
		static ScaleParam[] makesparam(int origw, int origh, double scale = 0, int dstw = 0, int dsth = 0)
		{
			ScaleParam[] k;
			double dstscale;
			
			int srcskl = 0;
			int dstskl = 0;
			
			if (scale == 0) {
				double dstsklw = 144514;
				double dstsklh = 144514;
				
			
				if (dstw != 0) {
					srcskl = origw;
					dstskl = dstw;
					dstsklw = ((double)dstw) / ((double)origw);
				}
				if (dsth != 0) {
					srcskl = origh;
					dstskl = dsth;
					dstsklh = ((double)dsth) / ((double)origh);
				}
				
				dstscale = Math.Min(dstsklw, dstsklh);
				
				
				
				
			} else {
				srcskl=114514;
				dstskl = (int)(scale*(double)srcskl+0.5);
				dstscale = scale;
			}
			
			if (dstscale <= basicUpSample)
				goto retdummy;
			
			
			int steps = (int)Math.Ceiling(Math.Log(dstscale, basicUpSample));
			k = new ScaleParam[steps];
			
			if (steps == 2) {
				k[0].DstScale = (float)(dstscale / (basicUpSample * basicUpSample));
				k[0].model = 0;
				
				k[1].DstScale = 1.0f;
				k[1].model = 1;
				
				return k;
			}
			
			
			int skll = ((int)basicUpSample) / 2;
			double upl = (double)((int)1 << (steps * skll));
			
			
			
			if (dstscale == upl) {
				
				k[0].DstScale = 1.0f;
				k[0].model = 0;
				for (int i = 1; i < steps; i++) {
					k[i].DstScale = 1.0f;
					k[i].model = 1;
				}
				
				
				return k;
			}
			
			
			
			
			double downer = Math.Pow(dstscale / upl, 1.0 / (double)((steps - 1) * (3 * steps - 2) / 2)); //(steps+2)/2)); //(steps)/2));
			double maxdowner = Math.Pow(downer, 2 * (steps - 1));//,steps); //,steps-1);
			
			k[0].DstScale = (float)maxdowner;
			k[0].model = 0;
			
			k[steps-1].DstScale = 1.0f;
			k[steps-1].model = 1;
			
			
			
			if (dstskl != 0) {
				//srcskl=(int)((double)srcskl*basicUpSample*maxdowner+0.5);
				for (int i = 1; i < steps - 1; i++) {
					maxdowner /= downer;
					//float dyv = (float)maxdowner;
					
					//srcskl = (int)((float)srcskl*basicUpSample*dyv+0.5f);
					
					k[i].DstScale = (float)maxdowner;//dyv;
					k[i].model = 1;
				}
				
				//if (scale == 0)
				//	k[steps-2].DstScale=(float)(int)((double)origw*dstscale/basicUpSample+0.5);
				
				//srcskl*=(int)basicUpSample;
				
				
				//k[0].DstScale*=(float)(((double)(dstskl-1))/((double)srcskl));
				/*
				if(srcskl>dstskl)
					k[0].DstScale*=(float)(((double)(dstskl-1))/((double)srcskl));
				else if(srcskl<dstskl)
					k[0].DstScale*=(float)(((double)(dstskl+1))/((double)srcskl));
				*/
				
				
				
				//Console.WriteLine("multifll="+srcskl);
				
			} else {
				for (int i = 1; i < steps - 1; i++) {
					maxdowner /= downer;
					k[i].DstScale = (float)maxdowner;
					k[i].model = 1;
				}
			}
			
			k[steps - 1].DstScale = 1.0f;
			k[steps - 1].model = 1;
			return k;
			
			
			retdummy:
			{
				k = new ScaleParam[1];
				k[0].DstScale = 1.0f;
				k[0].model = 0;
				return k;
			}
			
			
			
		}
		
		static unsafe byte* decfunc_example(int IOid, ref int width, ref int height, ref int channel, ref int is16bit, ref int ScaleParamLen, ref ScaleParam[] newparam)
		{
			is16bit = 1;
			byte* rett = wic_decode_image(InNames[IOid], ref width, ref height, ref channel);
			
			Console.WriteLine("InC#: width=" + width + ", height=" + height);
			Console.ReadKey();
			
			var newparam0 = makesparam(width, height, dstw: 1280, dsth: 1920);
			sparams[IOid] = newparam0;
			newparam = newparam0;
			ScaleParamLen = newparam0.Length;
			return rett;
		}
		
		static unsafe void encfunc_example(int IOid, int width, int height, int channel, int is16bit, byte* data)
		{
			
			byte[] output = new byte[width * height * channel * is16bit];
			Marshal.Copy((IntPtr)data, output, 0, output.Length);
			File.WriteAllBytes(OutNames[IOid], output);
		}
		
		static int findfunc_example(IntPtr inpath, IntPtr outpath, IntPtr fmt, IntPtr vlist)
		{
			string[] drr = Directory.GetFiles(w2xintztdir, "*", SearchOption.TopDirectoryOnly);
			int simplen = drr.Length;
			string[] innams = new string[simplen];
			string[] outnams = new string[simplen];
			
			int realget = 0;
			
			for (int i = 0; i < simplen; i++) {
				string oona = drr[i].Replace(w2xintztdir, w2xouttztdir) + ".png";
				if (!File.Exists(oona)) {
					innams[realget] = drr[i];
					outnams[realget] = oona;
					realget++;
				}
			}
			
			if (realget != 0) {
				InNames = innams;
				OutNames = outnams;
				sparams = new ScaleParam[realget][];
				
				InOutList(realget, InNames, OutNames);
			}
			
			return realget;
			
			
		}
		
		unsafe static void fp16conv()
		{
			const int hdsz = 0x10;
			byte[] ff16 = File.ReadAllBytes(@"E:\r\cud\ncnn\src\bin\Release\400x532x3xfp16.ppp");
			int lly = ff16.Length / 2;
			byte[] otbuf = new byte[hdsz + lly];
			
			fixed(byte* srcbb = &ff16[0]) {
				ushort* f = (ushort*)srcbb;
				
				for (int i = 0; i < lly; i++) {
					
					uint zita = f[i];
					zita = ((zita & 0x8000) << 16) | (((zita & 0x7c00) + 0x1C000) << 13) | ((zita & 0x03FF) << 13);
					
					bswap bs = new bswap();
					bs.buint = zita;
					float bsf = bs.bfloat * 255.0f;
					
					if (bsf < 0) {
						Console.WriteLine(bsf);
						Console.ReadKey();
					}
					
					otbuf[hdsz + i] = (byte)((uint)bsf);
					
					
				}
			}
			
			File.WriteAllBytes("dxp3.pgm", otbuf);
		}
		
		static void cplshader()
		{
			string[] shalis = File.ReadAllLines(@"E:\r\cud\ncnn\src\bin\0shader\00lyst.txt");
			
			foreach (var li in shalis) {
				string[] siga = li.Split('_');
				string dstna = "";
				
				if (siga[0] == "cain") {
					dstna += "CIN";
				} else if (siga[0] == "dain") {
					dstna += "DIN";
				} else if (siga[0] == "realsr") {
					dstna += "RSR";
				} else if (siga[0] == "srmd") {
					dstna += "SMD";
				} else if (siga[0] == "waifu2x") {
					dstna += "W2X";
				}
				
				if (siga[1] == "preproc") {
					dstna += "A";
				} else if (siga[1] == "postproc") {
					dstna += "Z";
				}
				
				if (siga.Length > 2 && siga[2] == "tta") {
					dstna += "1";
				} else {
					dstna += "0";
				}
				
				Console.WriteLine("gs/glslangValidator.exe -V -g0 -Os -o spv/" + dstna + "b " + li + ".comp");
				Console.WriteLine("gs/glslangValidator.exe -DNCNN_fp16_storage=1 -V -g0 -Os -o spv/" + dstna + "m " + li + ".comp");
				Console.WriteLine("gs/glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -g0 -Os -o spv/" + dstna + "s " + li + ".comp");
			}
		}
		
		static void printvtx()
		{
			var jo = gg.iG1MG.render;
			
			int nn = jo.Length;
			
			for (int i = 0; i < nn; i++) {
				File.WriteAllText(i + ".csv", jo[i].ToCSV());
				File.WriteAllText(i + "_realblend.csv", jo[i].RealBlendMappingCSV());
			}
		}
		
		
		
		static MD5 md5 = MD5.Create();
		static yGUID ComputeHash(string filePath)
		{
    
			return new yGUID(md5.ComputeHash(File.ReadAllBytes(filePath)));
    
		}
		
		static void tzt()
		{
			string[] fis = Directory.GetFiles(@"Q:\z\vxko", "*", SearchOption.AllDirectories);
			
			int ll = fis.Length;
			
			zoyo:
			for (int i = 0; i < ll; i++) {
				if (fis[i] == string.Empty)
					continue;
				
				string dy = fis[i].Replace(@"Q:\z\vxko", @"Q:\z\vzko");
				if (File.Exists(dy)) {
					try {
						var gvxko = ComputeHash(fis[i]);
						var gvzko = ComputeHash(dy);
						if (gvxko.rEquals(gvzko)) {
						
							File.Delete(dy);
							File.Move(fis[i], dy);
							fis[i] = string.Empty;
						} else
							Console.WriteLine("ne==" + fis[i]);
					} catch {
					}
				}
				
			}
			system("timeout /t 10");
			goto zoyo;
			
			/*
			var gu1 = ComputeHash(@"Q:\a\uu\Editor20181\Data\Tools\nodejs\node_perfctr_provider.man");
			var gu2 = ComputeHash(@"Q:\z\vxko\node_perfctr_provider.man");
			Console.WriteLine(gu1.rEquals(gu2));
			var gu3 = ComputeHash(@"Q:\z\vxko\scripts\generate-definitelytyped.sh");
			Console.WriteLine(gu1.rEquals(gu3));
			*/
		}
		
		static string[] fmtout = {"\\u000A",
			"cir",	//1
			" ",
			"cir",	//3
			"\\u000A",
			"tri",	//5
			" ",
			"tri",	//7
			"\\u000A",
			"sqr",
			" ",
			"sqr",
			"\\u000A",
			"cir",
			" ",
			"cir",
			"\\u000A",
			"tri",
			" ",
			"tri",
			"\\u000A",
			"sqr",
			" ",
			"sqr",
			"\""
		};
		
		
		static List<string> seconsig = new List<string>() {
			"初九",
			"九二",
			"九三",
			"九四",
			"九五",
			"上九",
			"初六",
			"六二",
			"六三",
			"六四",
			"六五",
			"上六"
		};
		/*
		static string[,] yaoci = {
{"坤初六：履霜，堅冰至。	象曰「履霜堅冰」，陰始凝也，馴致其道，至堅冰也。","坤六二，直、方、大，不習，無不利。	象曰六二之動，直以方也。「不習無不利」，地道光也。","坤六三，含章，可貞，或從王事，無成有終。	《象》曰「含章可貞」，以時發也。「或從王事」，知光大也。","坤六四，括囊，無咎無譽。	象曰「括囊無咎」，慎不害也。","坤六五，黃裳，元吉。	象曰「黃裳元吉」，文在中也。","坤上六，龍戰於野，其血玄黃。	象曰「龍戰於野」，其道窮也。"},
{"復初九，不遠復，無祗悔，元吉。	象曰「不遠之復」，以修身也。","復六二，休復，吉。	象曰「休復之吉」，以下仁也。","復六三，頻復，厲，無咎。	象曰「頻復之厲」，義無咎也。","復六四，中行獨復。	象曰「中行獨復」，以從道也。","復六五，敦復，無悔。	象曰「敦復無悔」，中以自考也。","復上六，迷復，凶，有災眚。用行師，終有大敗，以其國君凶，至於十年不克徵。	象曰「迷復之凶」，反君道也。"},
{"師初六，師出以律，否臧凶。	象曰「師出以律，」失律凶也。","師九二，在師中吉，無咎，王三錫命。	象曰「在師中吉」，承天寵也。「王三錫命」，懷萬邦也。","師六三，師或輿屍，凶。	象曰「師或輿屍」，大無功也。","師六四，師左次，無咎。	象曰「左次無咎」，未失常也。","師六五，田有禽。利執言，無咎。長子帥師，弟子輿屍，貞凶。	象曰「長子帥師」，以中行也。「弟子輿屍」，使不當也。","師上六，大君有命，開國承家，小人勿用。	象曰「大君有命」，以正功也。「小人勿用」，必亂邦也。"},
{"臨初九，咸臨，貞吉。	象曰「咸臨貞吉」，志行正也。","臨九二，咸臨，吉，無不利。	象曰「咸臨吉無不利」，未順命也。","臨六三，甘臨，無攸利；既憂之，無咎。	象曰「甘臨」，位不當也。「既憂之」。咎不長也。","臨六四，至臨，無咎。	象曰「至臨無咎」，位當也。","臨六五，知臨，大君之宜，吉。	象曰「大君之宜」，行中之謂也。","臨上六，敦臨，吉，無咎。	象曰「敦臨之吉」，志在內也。"},
{"謙初六，謙謙君子，用涉大川，吉。	象曰「謙謙君子」，卑以自牧也。","謙六二，鳴謙，貞吉。	象曰「鳴謙貞吉」，中心得也。","謙九三，勞謙君子，有終，吉。	象曰「勞謙君子」，萬民服也。","謙六四，無不利，摠謙。	象曰「無不利，摠謙」，不違則也。","謙六五，不富以其鄰，利用侵伐，無不利。	象曰「利用侵伐」，徵不服也。","謙上六，鳴謙，利用行師徵邑國。	象曰「鳴謙」，志未得也。「可用行師」，徵邑國也。"},
{"夷初九，明夷，於飛垂其翼。君子於行，三日不食。有攸往，主人有言。	象曰「君子於行」，義不食也。","夷六二，明夷夷於左股，用拯馬壯，吉。	象曰六二之吉，順以則也。","夷九三，明夷於南狩，得其大首，不可疾貞。	象曰「南狩」之志，乃得大也。","夷六四，入於左腹，獲明夷之心，於出門庭。	象曰「入於左腹」，獲心意也。","夷六五，箕子之明夷，利貞。	象曰箕子之貞，明不可息也。","夷上六，不明，晦，初登於天，後入於地。	象曰「初登於天」，照四國也。「後入天地」，失則也。"},
{"升初六，允升，大吉。	象曰「允升大吉」，上合志也。","升九二，孚乃利用禴，無咎。	象曰九二之孚，有喜也。","升九三，升虛邑。	象曰「升虛邑」，無所疑也。","升六四，王用亨於岐山，吉，無咎。	象曰「王用亨於岐山」，順事也。","升六五，貞吉，升階。	象曰「貞吉升階」，大得志也。","升上六，冥升，利於不息之貞。	象曰冥升在上，消不富也。"},
{"泰初九，拔茅茹，以其匯。徵吉。	象曰「拔茅徵吉」，志在外也。","泰九二，包荒，用馮河，不遐遺。朋亡，得尚於中行。	象曰「包荒，得尚於中行」，以光大也。","泰九三，無平不陂，無往不復。艱貞無咎。勿恤其孚，於食有福。	象曰「無往不復」，天地際也。","泰六四，翩翩，不富以其鄰，不戒以孚。	象曰「翩翩，不富」，皆失實也。「不戒以孚」，中心願也。","泰六五，帝乙歸妹，以祉元吉。	象曰「以祉元吉」，中以行願也。","泰上六，城復於隍，勿用師，自邑告命。貞吝。	象曰「城復於隍」，其命亂也。"},
{"豫初六，鳴豫，凶。	象曰「初六鳴豫」，志窮凶也。","豫六二，介於石，不終日，貞吉。	象曰「不終日貞吉」，以中正也。","豫六三，盱豫，悔，遲有悔。	象曰「盱豫不悔」，位不當也。","豫九四，由豫，大有得，勿疑。朋盍簪。	象曰「由豫大有得」，志大行也。","豫六五，貞疾，恆不死。象曰「六五貞疾」，乘剛也。「恆不死」，中未亡也。","豫上六，冥豫，成有渝。無咎。	象曰「冥豫」在上，何可長也？"},
{"震初九，震來虩虩，後笑言啞啞，吉。	象曰「震來虩虩」，恐致福也。「笑言啞啞」，後有則也。","震六二，震來厲，億喪貝，躋於九陵，勿逐，七日得。	象曰「震來厲」，乘剛也。","震六三，震蘇蘇，震行無眚。	象曰「震蘇蘇」，位不當也。","震九四，震遂泥。	象曰「震遂泥」，未光也。","震六五，震往來，厲，意無喪，有事。	象曰「震往來厲」，危行也。其事在中，大無喪也。","震上六，震索索，視矍矍，徵凶。震不於其躬，於其鄰，無咎。婚媾有言。	象曰「震索索」，中未得也。雖凶無咎，畏鄰戒也。"},
{"解初六，無咎。	象曰剛柔之際，義無咎也。","解九二，田獲三狐，得黃矢，貞吉。	象曰九二貞吉，得中道也。","解六三，負且乘，致寇至，貞吝。	象曰「負且乘」，亦可醜也。自我致戎，又誰咎也？","解九四，解而拇，朋至斯孚。	象曰「解而拇」，未當位也。","解六五，君子維有解，吉，有孚於小人。	象曰君子有解，小人退也。","解上六，公用射隼於高墉之上，獲之，無不利。	象曰「公用射隼」，以解悖也。"},
{"妹初九，歸妹以娣。跛能履，徵吉。	象曰「歸妹以娣」，以恆也。「跛能履吉」，相承也。","妹九二，眇能視，利幽人之貞。	象曰「利幽人之貞」，未變常也。","妹六三，歸妹以須，反歸以娣。	象曰「歸妹以須」，未當也。","妹九四，歸妹愆期，遲歸有時。	象曰：「愆期」之志，有待而行也。","妹六五，帝乙歸妹，其君之袂不如其娣之袂良。月幾望，吉。	象曰「帝乙歸妹，不如其娣之袂良」也。其位在中，以貴行也。","妹上六，女承筐無實，士刲羊無血，無攸利。	象曰上六無實，承虛筐也。"},
{"ｇ初六，飛鳥以凶。	象曰「飛鳥以凶」，不可如何也。","ｇ六二，過其祖，遇其妣。不及其君，遇其臣。無咎。	象曰「不及其君」，臣不可過也。","ｇ九三，弗過防之，從或戕之，凶。	象曰「從或戕之」，凶如何也？","ｇ九四，無咎。弗過遇之，往厲必戒，勿用永貞。	象曰「弗過遇之」，位不當也。「往厲必戒」，終不可長也。","ｇ六五，密雲不雨，自我西郊。公弋取彼在穴。	象曰「密雲不雨」，已上也。","ｇ上六，弗遇過之，飛鳥離之，凶，是謂災眚。	象曰「弗遇過之」，已亢也。"},
{"豐初九，遇其配主，雖旬無咎，往有尚。	象曰「雖旬無咎」，過旬災也。","豐六二，豐其蔀，日中見鬥。往得疑疾，有孚發若，吉。	象曰「有孚發若」，信以發志也。","豐九三，豐其沛，日中見沫，折其右肱，無咎。	象曰「豐其沛」，不可大事也。「折其右肱」，終不可用也。","豐九四，豐其蔀，日中見鬥，遇其夷主，吉。	象曰「豐其蔀」，位不當也。「日中見鬥」，幽不明也。「遇其夷主」，吉行也。","豐六五，來章有慶譽，吉。	象曰六五之吉，有慶也。","豐上六，豐其屋，蔀其家，窺其戶，闃其無人，三歲不覿，凶。	象曰「豐其屋」，天際翔也。「窺其戶，闃其無人」，自藏也。"},
{"恆初六，浚恆，貞凶，無攸利。	《象曰「浚恆」之「凶」，始求深也。","恆九二，悔亡。	象曰九二「悔亡」，能久中也。","恆九三，不恆其德，或承之羞，貞吝。	象曰「不恆其德」，無所容也。","恆九四，田無禽。	象曰久非其位，安得禽也。","恆六五，恆其德，貞，婦人吉，夫子凶。	象曰婦人貞吉，從一而終也。夫子制義，從婦凶也。","恆上六，振恆，凶。	象曰振恆在上，大無功也。"},
{"壯初九，壯於趾，徵凶，有孚。	象曰「壯於趾」，其孚窮也。","壯九二，貞吉。	象曰九二「貞吉」，以中也。","壯九三，小人用壯，君子用罔，貞厲。羝羊觸藩，羸其角。	象曰「小人用壯」，君子以罔也。","壯九四，貞吉，悔亡。藩決不羸，壯於大輿之輹。	象曰「藩決不羸」，尚往也。","壯六五，喪羊於易，無悔。	象曰「喪羊於易」，位不當也。","壯上六，羝羊觸藩，不能退，不能遂，無攸利，艱則吉。	象曰「不能退，不能遂」，不詳也。「艱則吉」，咎不長也。"},
{"比初六，有孚比之，無咎。有孚盈缶，終來有它，吉。	象曰比之初六，有它吉也。","比六二，比之自內，貞吉。	象曰「比之自內」，不自失也。","比六三，比之匪人。	象曰比之匪人」，不亦傷乎？","比六四，外比之，貞吉。	象曰外比於賢，以從上也。","比九五，顯比，王用三驅，失前禽，邑人不誡，吉。	象曰「顯比」之吉，位正中也。捨逆取順，失前禽也。邑人不誡，上使中也。","比上六，比之無首，凶。	象曰「比之無首」，無所終也。"},
{"屯初九，磐桓，利居貞。利建侯。	象曰雖磐桓，志行正也。以貴下賤，大得民也。","屯六二，屯如邅如，乘馬班如。匪寇，婚媾。女子貞不字，十年乃字。	象曰六二之難，乘剛也。十年乃字，反常也。","屯六三，即鹿無虞，惟入於林中，君子幾不如捨，往吝。	象曰「即鹿無虞」，以從禽也。君子捨之，往吝窮也。","屯六四，乘馬班如，求婚媾。往吉，無不利。	象曰求而往，明也。","屯九五，屯其膏，小，貞吉；大，貞凶。	象曰「屯其膏」，施未光也。","屯上六，乘馬班如，泣血漣如。	象曰「泣血漣如」，何可長也。"},
{"坎初六，習坎，入於坎，窞，凶。	象曰「習坎入坎」，失道，凶也。","坎九二，坎有險，求小得。	象曰「求小得」，未出中也。","坎六三，來之坎，坎險且枕，入於坎，窞，勿用。	象曰「來之坎坎」，終無功也。","坎六四，樽酒簋貳用缶，納約自牖，終無咎。	象曰「樽酒簋貳」，剛柔際也。","坎九五，坎不盈，祗既平，無咎。	象曰「坎不盈」，中未大也。","坎上六，系用徽纆，窴於叢棘，三歲不得，凶。	象曰上六失道，凶三歲也。"},
{"節初九，不出戶庭，無咎。	象曰「不出戶庭」，知通塞也。","節九二，不出門庭，凶。	象曰「不出門庭凶」，失時極也。","節六三，不節若，則嗟若，無咎。	象曰「不節之嗟」，又誰咎也。","節六四，安節。亨。	象曰「安節之亨」，承上道也。","節九五，甘節，吉，往有尚。	象曰「甘節之吉」，居位中也。","節上六，苦節，貞凶，悔亡。	象曰「苦節貞凶」，其道窮也。"},
{"蹇初六，往蹇來譽。	象曰「往蹇來譽」，宜待也。","蹇六二，王臣蹇蹇，匪躬之故。	象曰「王臣蹇蹇」，終無尤也。","蹇九三，往蹇來反。	象曰「往蹇來反」，內喜之也。","蹇六四，往蹇來連。	象曰「往蹇來連」，當位實也。","蹇九五，大蹇朋來。	象曰「大蹇朋來」，以中節也。","蹇上六，往蹇來碩，吉，利見大人。	象曰「往蹇來碩」，志在內也。「利見大人」，以從貴也。"},
{"既初九，曳其輪，濡其尾，無咎。	象曰「曳其輪」，義無咎也。","既六二，「婦喪其茀，勿逐，七日得。	象曰「七日得」，以中道也。","既九三，高宗伐鬼方，三年克之，小人勿用。	象曰「三年克之」，憊也。","既六四，繻有衣袽，終日戒。	象曰「終日戒」，有所疑也。","既九五，東鄰殺牛，不如西鄰之禴祭，實受其福。	象曰「東鄰殺牛」，不如西鄰之時也。「實受其福」，吉大來也。","既上六，濡其首，厲。	象曰「濡其首厲」，何可久也？"},
{"井初六，井泥不食。舊井無禽。	象曰「井泥不食」，下也。「舊井無禽」，時捨也。","井九二，井谷射鮒，甕敝漏。	象曰「井谷射鮒」，無與也。","井九三，井渫不食，為我心惻。可用汲，王明並受其福。	象曰「井渫不食」，行惻也。求「王明」，受福也。","井六四，井甃，無咎。	象曰「井甃無咎」，修井也。","井九五，井洌，寒泉食。	象曰「寒泉之食」，中正也。","井上六，井收勿幕，有孚元吉。	象曰「元吉」在「上」，大成也。"},
{"需初九，需於郊，利用恆，無咎。	象曰「需於郊」，不犯難行也。「利用恆無咎」，未失常也。","需九二，需於沙，小有言，終吉。	象曰「需於沙」，衍在中也。雖小有言，以終吉也。","需九三，需於泥，致寇至。	象曰「需於泥」，災在外也。自我致寇，敬慎不敗也。","需六四，需於血，出自穴。	象曰「需於血，」順以聽也。","需九五，需於酒食，貞吉。	象曰「酒食貞吉」，以中正也。","需上六，入於穴，有不速之客三人來，敬之終吉。	象曰「不速之客來，敬之終吉」，雖不當位，未大失也。"},
{"萃初六，有孚不終，乃亂乃萃，若號，一握為笑，勿恤，往無咎。	象曰「乃亂乃萃」，其志亂也。","萃六二，引吉，無咎，孚乃利用禴。	象曰「引吉無咎」，中未變也。","萃六三，萃如嗟如，無攸利，往無咎，小吝。	象曰「往無咎」，上巽也。","萃九四，大吉無咎。	象曰「大吉無咎」，位不當也。","萃九五，萃有位，無咎。匪孚，元永貞，悔亡。	象曰「萃有位」，志未光也。","萃上六，繼咨涕洟，無咎。	象曰「繼咨涕洟」，未安上也。"},
{"隨初九，官有渝，貞吉，出門交有功。	象曰「官有渝」，從正吉也。「出門交有功」，不失也。","隨六二，系小子，失丈夫。	象曰「系小子」，弗兼與也。","隨六三，系丈夫，失小子，隨有求，得。利居貞。	象曰「系丈夫」，志舍下也。","隨九四，隨有獲，貞凶。有孚在道，以明，何咎？	象曰「隨有獲」，其義凶也。「有孚在道」，明功也。","隨九五，孚於嘉，吉。	象曰「孚於嘉吉」，位正中也。","隨上六，拘繫之，乃從維之，王用亨於西山。	象曰「拘繫之」，上窮也。"},
{"困初六，臀困於株木，入於幽谷，三歲不覿。	象曰「入於幽谷」，幽不明也。","困九二，困於酒食，朱紱方來。利用享祀。徵凶，無咎。	象曰「困於酒食」，中有慶也。","困六三，困於石，據於蒺藜，入於其宮，不見其妻，凶。	象曰「據於蒺藜」，乘剛也。「入於其宮，不見其妻」，不祥也。","困九四，來徐徐，困於金車，吝，有終。	象曰「來徐徐」，志在下也。雖不當位，有與也。","困九五，劓刖，困於赤紱乃徐有說，利用祭祀。	象曰「劓刖」，志未得也。「乃徐有說」，以中直也。「利用祭祀」，受福也。","困上六，困於葛藟，於臲,曰動悔有悔，徵吉。	象曰「困於葛藟」，未當也。「動悔有悔」，吉行也。"},
{"兌初九，和兌，吉。	象曰「和兌之吉」，行未疑也。","兌九二，孚兌，吉，悔亡。	象曰「孚兌之吉」，信志也。","兌六三，來兌，凶。	象曰「來兌之凶」，位不當也。","兌九四，商兌未寧，介疾有喜。	象曰「九四之喜」，有慶也。","兌九五，孚於剝，有厲。	象曰「孚於剝」，位正當也。","兌上六，引兌。	象曰上六「引兌」，未光也。"},
{"咸初六，咸其拇。	《象》曰「咸其拇」，志在外也。","咸六二，咸其腓，凶。居吉。	《象》曰雖「凶居吉」，順不害也。","咸九三，咸其股，執其隨，往吝。	象曰「咸其股」，亦不處也。志在隨人，所執下也。","咸九四，貞吉。悔亡。憧憧往來，朋從爾思。	象曰「貞吉悔亡」，未感害也。「憧憧往來」，未光大也。","咸九五，咸其脢，無悔。	象曰「咸其脢」，志末也。","咸上六，咸其輔頰舌。	象曰「咸其輔頰舌」，滕口說也。"},
{"革初九，鞏用黃牛之革。	象曰「鞏用黃牛」，不可以有為也。","革六二，巳日乃革之，徵吉，無咎。	象曰「巳日革之」，行有嘉也。","革九三，徵凶。貞厲。革言三就，有孚。	象曰「革言三就」，又何之矣。","革九四，悔亡。有孚改命，吉。	象曰「改命之吉」，信志也。","革九五，大人虎變，未佔有孚。	象曰「大人虎變」，其文炳也。","革上六，君子豹變，小人革面，徵凶，居貞吉。	象曰「君子豹變」，其文蔚也。「小人革面」，順以從君也。"},
{"Ｇ初六，藉用白茅，無咎。	象曰「藉用白茅」，柔在下也。","Ｇ九二，枯楊生稊，老夫得其女妻，無不利。	象曰「老夫女妻，」，過以相與也。","Ｇ九三，棟橈，凶。	象曰「棟橈」之「凶」，不可以有輔也。","Ｇ九四，棟隆，吉。有它，吝。	象曰「棟隆之吉」，不橈乎下也。","Ｇ九五，枯楊生華，老婦得其士夫，無咎無譽。	象曰「枯楊生華」，何可久也。「老婦士夫」，亦可醜也。","Ｇ上六，過涉滅頂，凶。無咎。	象曰「過涉之凶」，不可咎也。"},
{"夬初九，壯於前趾，往不勝，為咎。	象曰不勝而往，咎也。","夬九二，惕號，莫夜有戎，勿恤。	象曰「有戎勿恤」，得中道也。","夬九三，壯於頄，有凶。君子夬夬獨行，遇雨若濡，有慍無咎。	象曰「君子夬夬」，終無咎也。","夬九四，臀無膚，其行次且。牽羊悔亡，聞言不信。	象曰「其行次且」，位不當也。「聞言不信」，聰不明也。","夬九五，莧陸夬夬中行，無咎。	象曰「中行無咎」，中未光也。","夬上六，無號，終有凶。	象曰「無號之凶」，終不可長也。"},
{"剝初六：剝床以足，蔑貞凶。	象曰「剝床以足」，以滅下也。","剝六二：剝床以辨，蔑貞凶。	象曰「剝床以辨」，未有與也。","剝六三：剝之，無咎。	象曰「剝之無咎」，失上下也。","剝六四：剝床以膚，凶。	象曰「剝床以膚」，切近災也。","剝六五：貫魚以宮人寵，無不利。	象曰「以宮人寵」，終無尤也。","剝上九：碩果不食，君子得輿，小人剝廬。	象曰「君子得輿」，民所載也。「小人剝廬」，終不可用也。"},
{"頤初九，捨爾靈龜，觀我朵頤，凶。	象曰「觀我朵頤」，亦不足貴也。","頤六二，顛頤拂經於丘頤，徵凶。	象曰「六二徵凶」，行失類也。","頤六三，拂頤，貞凶，十年勿用，無攸利。	象曰「十年勿用」，道大悖也。","頤六四，顛頤，吉。虎視眈眈，其欲逐逐，無咎。	象曰「顛頤之吉」，上施光也。","頤六五，拂經，居貞吉，不可涉大川。	象曰「居貞之吉」，順以從上也。","頤上九，由頤，厲，吉。利涉大川。	象曰「由頤厲吉」，大有慶也。"},
{"蒙初六，發蒙，利用刑人，用說桎梏，以往吝。	象曰「利用刑人」，以正法也。","蒙九二，包蒙，吉。納婦，吉。子克家。	象曰「子克家」，剛柔節也。","蒙六三，勿用取女，見金夫，不有躬。無攸利。	象曰「勿用取女」，行不順也。","蒙六四，困蒙，吝。	象曰「困蒙之吝」，獨遠實也。","蒙六五，童蒙，吉。	象曰「童蒙」之「吉」，順以巽也。","蒙上九，擊蒙，不利為寇，利禦寇。	象曰「利」用「禦寇」，上下順也。"},
{"損初九，已事遄往，無咎。酌損之。	象曰「已事遄往」，尚合志也。","損九二，利貞。徵凶，弗損，益之。	象曰「九二利貞」，中以為志也。","損六三，三人行則損一人，一人行則得其友。	象曰「一人行」，「三」則疑也。","損六四，損其疾，使遄有喜，無咎。	象曰「損其疾」，亦可喜也。","損六五，或益之十朋之龜，弗克違，元吉。	象曰六五元吉，自上祐也。","損上九，弗損，益之，無咎，貞吉，利有攸往，得臣無家。	象曰「弗損，益之」，大得志也。"},
{"艮初六，艮其趾，無咎。利永貞。	象曰「艮其趾」，未失正也。","艮六二，艮其腓，不拯其隨，其心不快。	象曰「不拯其隨」，未退聽也。","艮九三，艮其限，列其夤，厲，熏心。	象曰「艮其限」，危熏心也。","艮六四，艮其身，無咎。	象曰「艮其身」，止諸躬也。","艮六五，艮其輔，言有序，悔亡。	象曰「艮其輔」，以中正也。","艮上九，敦艮，吉。	象曰「敦艮之吉」，以厚終也。"},
{"賁初九，賁其趾，捨車而徒。	象曰「捨車而徒」，義弗乘也。","賁六二，賁其須。	象曰「賁其須」，與上興也。","賁九三，賁如，濡如，永貞吉。	象曰「永貞之吉」，終莫之陵也。","賁六四，賁如皤如，白馬翰如。匪寇，婚媾。	象曰六四，當位疑也。「匪寇婚媾」，終無尤也。","賁六五，賁於丘園，束帛戔戔，吝，終吉。	象曰「六五之吉」，有喜也。","賁上九，白賁，無咎。	象曰「白賁無咎」，上得志也。"},
{"蠱初六，乾父之蠱，有子，考無咎。厲，終吉。	象曰「乾父之蠱」，意承考也。","蠱九二，乾母之蠱，不可貞。	象曰「乾母之蠱」，得中道也。","蠱九三，乾父之蠱，小有悔，無大咎。	象曰「乾父之蠱」，終無咎也。","蠱六四，裕父之蠱，往見吝。	象曰「裕父之蠱」，往未得也。","蠱六五，乾父之蠱，用譽。	象曰「乾父用譽」，承以德也。","蠱上九，不事王侯，高尚其事。	象曰「不事王侯」，志可則也。"},
{"Ｘ初九，有厲，利已。	象曰「有厲利已」，不犯災也。","Ｘ九二，輿說輹。	象曰「輿說輹」，中無尤也。","Ｘ九三，良馬逐，利艱貞，曰閒輿衛，利有攸往。	象曰「利有攸往」，上合志也。","Ｘ六四，童牛之牿，元吉。	象曰「六四元吉」，有喜也。","Ｘ六五，豶豕之牙，吉。	象曰「六五之吉」，有慶也。","Ｘ上九，何天之衢，亨。	象曰「何天之衢」，道大行也。"},
{"晉初六，晉如摧如，貞吉。罔孚，裕無咎。	象曰「晉如摧如」，獨行正也。「裕無咎」。未受命也。","晉六二，晉如，愁如，貞吉。受茲介福於，其王母。	象曰「受茲介福」，以中正也。","晉六三，眾允，悔亡。	象曰「眾允」之志，上行也。","晉九四，晉如鼫鼠，貞厲。	象曰「鼫鼠貞厲」，位不當也。","晉六五，悔亡，失得，勿恤。往吉，無不利。	象曰「失得勿恤」，往有慶也。","晉上九，晉其角，維用伐邑，厲吉，無咎，貞吝。	象曰「維用伐邑」，道未光也。"},
{"噬初九，屨校滅趾，無咎。	象曰「屨校滅趾」，不行也。","噬六二，噬膚滅鼻，無咎。	象曰「噬膚滅鼻」，乘剛也。","噬六三，噬臘肉遇毒，小吝，無咎。	象曰「遇毒」，位不當也。","噬九四，「噬乾胏，得金矢。利艱貞，吉。	象曰「利艱貞吉」，未光也。","噬六五，噬乾肉得黃金。貞厲，無咎。	象曰「貞厲無咎」，得當也。","噬上九，何校滅耳，凶。	象曰「何校滅耳」，聰不明也。"},
{"未初六，濡其尾，吝。	象曰「濡其尾」，亦不知極也。","未九二，曳其輪，貞吉。	象曰九二貞吉，中以行正也。","未六三，未濟，徵凶。利涉大川。	象曰「未濟徵凶」，位不當也。","未九四，貞吉，悔亡，震用伐鬼方，三年，有賞於大國。	象曰「貞吉悔亡」，志行也。","未六五，貞吉，無悔。君子之光，有孚吉。	象曰「君子之光」，其輝吉也。","未上九，有孚於飲酒，無咎。濡其首，有孚失是。	象曰「飲酒濡首」，亦不知節也。"},
{"睽初九，悔亡。喪馬勿逐自復。見惡人無咎。	象曰「見惡人」，以辟咎也。","睽九二，遇主於巷，無咎。	象曰「遇主於巷」，未失道也。","睽六三，見輿曳，其牛掣，其人天且劓，無初有終。	象曰「見輿曳」，位不當也。「無初有終」，遇剛也。","睽九四，睽孤遇元夫，交孚，厲，無咎。	象曰「交孚無咎」，志行也。","睽六五，悔亡。厥宗噬膚，往何咎？	象曰「厥宗噬膚」，往有慶也。","睽上九，睽孤見豕負途，載鬼一車，先張之弧，後說之弧，匪寇，婚媾。往遇雨則吉。	象曰「遇雨之吉」，群疑亡也。"},
{"旅初六，旅瑣瑣，斯其所取災。	象曰「旅瑣瑣」，志窮災也。","旅六二，旅即次，懷其資，得童僕，貞。	象曰「得童僕貞」，終無尤也。","旅九三，旅焚其次，喪其童僕，貞厲。	象曰「旅焚其次」，亦以傷矣。以旅與下，其義喪也。","旅九四，旅於處，得其資斧，我心不快。	象曰「旅於處」，未得位也。「得其資斧」，心未快也。","旅六五，射雉，一矢亡，終以譽命。	象曰「終以譽命」，上逮也。","旅上九，鳥焚其巢，旅人先笑後號咷。喪牛於易，凶。	象曰以旅在上，其義焚也。「喪牛於易」，終莫之聞也。"},
{"離初九，履錯然，敬之無咎。	象曰「履錯之敬」，以辟咎也。","離六二，黃離，元吉。	象曰「黃離元吉」，得中道也。","離九三，日昃之離，不鼓缶而歌，則大耋之嗟，凶。	象曰「日昃之離」，何可久也？","離九四，突如，其來如，焚如，死如，棄如。	象曰「突如其來如」，無所容也。","離六五，出涕沱若，慼嗟若，吉。	象曰六五之吉，離王公也。","離上九，王用出征，有嘉折首，獲匪其醜，無咎。	象曰「王用出征」，以正邦也。"},
{"鼎初六，鼎顛趾，利出否。得妾以其子，無咎。	象曰「鼎顛趾」，未悖也。「利出否」，以從貴也。","鼎九二，鼎有實，我仇有疾，不我能即，吉。	象曰「鼎有實」，慎所之也。「我仇有疾」，終無尤也。","鼎九三，鼎耳革，其行塞，雉膏不食，方雨，虧悔，終吉。	象曰「鼎耳革」，失其義也。","鼎九四，鼎折足，覆公餗，其形渥，凶。	象曰「覆公餗」，信如何也。","鼎六五，鼎黃耳金鉉，利貞。	象曰「鼎黃耳」，中以為實也。","鼎上九，鼎玉鉉，大吉，無不利。	象曰玉鉉在上，剛柔節也。"},
{"有初九，無交害匪咎。艱則無咎。	象曰大有初九，無交害也。","有九二，大車以載，有攸往，無咎。	象曰「大車以載」，積中不敗也。","有九三，公用亨於天子，小人弗克。	象曰公用亨於天子，小人害也。","有九四，匪其彭，無咎。	象曰「匪其彭，無咎。」明辨晢也。","有六五，厥孚交如威如，吉。	象曰「厥孚交如」，信以發志也。「威如之吉」，易而無備也。","有上九，自天祐之，吉，無不利。	象曰大有上吉，自天祐也。"},
{"觀初六，童觀，小人無咎，君子吝。	象曰「初六童觀」，「小人」道也。","觀六二，窺觀，利女貞。	象曰「窺觀女貞」，亦可醜也。","觀六三，觀我生，進退。	象曰「觀我生進退」，未失道也。","觀六四，觀國之光，利用賓於王。	象曰「觀國之光」，尚賓也。","觀九五，觀我生，君子無咎。	象曰「觀我生」，觀民也。","觀上九，觀其生，君子無咎。	象曰「觀其生」，志未平也。"},
{"益初九，利用為大作，元吉，無咎。	象曰「元吉無咎」，下不厚事也。","益六二，或益之十朋之龜，弗克違。永貞吉。王用享於帝，吉。	象曰「或益之」，自外來也。","益六三，益之用凶事，無咎。有孚。中行告公用圭。	象曰「益用凶事」，固有之也。","益六四，中行告公，從，利用為依遷國。	象曰「告公從」，以益志也。","益九五，有孚惠心，勿問，元吉。有孚，惠我德。	象曰「有孚惠心」，勿問之矣。「惠我德」，大得志也。","益上九，莫益之，或擊之，立心勿恆，凶。	象曰「莫益之」，偏辭也。「或擊之」，自外來也。"},
{"渙初六，用拯馬壯，吉。	象曰初六之吉順也。","渙九二，渙奔其機，悔亡。	象曰「渙奔其機」，得願也。","渙六三，渙其躬，無悔。	象曰「渙其躬」，志在外也。","渙六四，渙其群，元吉。渙有丘，匪夷所思。	象曰「渙其群元吉」，光大也。","渙九五，渙汗其大號，渙王居，無咎。	象曰「王居無咎」，正位也。","渙上九，渙其血，去逖出，無咎。	象曰「渙其血」，遠害也。"},
{"中初九，虞吉，有它不燕。	象曰初九「虞吉」，志未變也。","中九二，鳴鶴在陰，其子和之。我有好爵，吾與爾靡之。	象曰「其子和之」，中心願也。","中六三，得敵，或鼓或罷，或泣或歌。	象曰「或鼓或罷」，位不當也。","中六四，月幾望，馬匹亡，無咎。	象曰「馬匹亡」，絕類上也。","中九五，有孚攣如，無咎。	象曰「有孚攣如」，位正當也。","中上九，翰音登於天，貞凶。	象曰「翰音登於天」，何可長也？"},
{"漸初六，鴻漸於乾。小子厲，有言，無咎。	象曰「小子之厲」，義無咎也。","漸六二，鴻漸於磐，飲食衎衎，吉。	象曰「飲食衎衎」，不素飽也。","漸九三，鴻漸於陸。夫徵不復，婦孕不育，凶。利禦寇。	象曰「夫徵不復」，離群醜也。「婦孕不育」，失其道也。「利用禦寇」，順相保也。","漸六四，鴻漸於木，或得其桷，無咎。	象曰「或得其桷」，順以巽也。","漸九五，鴻漸於陵，婦三歲不孕，終莫之勝，吉。	象曰「終莫之勝吉」，得所願也。","漸上九，鴻漸於陸，其羽可用為儀，吉。	象曰「其羽可用為儀，吉」，不可亂也。"},
{"家初九，閒有家，悔亡。	象曰「閒有家」，志未變也。","家六二，無攸遂，在中饋，貞吉。	象曰六二之吉，順以巽也。","家九三，家人嗃，悔厲吉；婦子嘻嘻，終吝。	象曰「家人嗃」，未失也。「婦子嘻嘻」，失家節也。","家九四，富家，大吉。	象曰「富家大吉」，順在位也。","家九五，王假有家，勿恤，吉。	象曰「王假有家」，交相愛也。","家上九，有孚威如，終吉。	象曰威如之吉，反身之謂也。"},
{"巽初六，進退，利武人之貞。	象曰「進退」，志疑也。「利武人之貞」，志治也。","巽九二，巽在床下，用史巫紛若，吉，無咎。	象曰「紛若之吉」，得中也。","巽九三，頻巽，吝。	象曰「頻巽之吝」，志窮也。","巽六四，悔亡，田獲三品。	象曰「田獲三品」，有功也。","巽九五，貞吉，悔亡，無不利，無初有終。先庚三日，後庚三日，吉。	象曰九五之吉，位正中也。","巽上九，巽在床下，喪其資斧，貞凶。	象曰「巽在床下」，上窮也。「喪其資斧」，正乎凶也。"},
{"ｘ初九，「復自道，何其咎？吉。	象曰「復自道」，其義「吉」也。","ｘ九二，牽復，吉。	象曰牽復在中，亦不自失也。","ｘ九三，輿說輻。夫妻反目。	象曰「夫妻反目」，不能正室也。","ｘ六四，有孚，血去，惕出無咎。	象曰「有孚惕出」，上合志也。","ｘ九五，有孚攣如，富以其鄰。	象曰「有孚攣如」，不獨富也。","ｘ上九，既雨既處，尚德載。婦貞厲。月幾望，君子徵凶。	象曰「既雨既處」，德積載也。「君子徵凶」，有所疑也。"},
{"否初六，拔茅茹以其匯。貞吉，亨。	象曰「拔茅貞吉」，志在君也。","否六二，包承，小人吉，大人否。亨。	象曰「大人否亨」，不亂群也。","否六三，包羞。	象曰「包羞」，位不當也。","否九四，有命，無咎，疇離祉。	象曰「有命無咎」，志行也。","否九五，休否，大人吉。其亡其亡，繫於苞桑。	象曰大人之吉，位正當也。","否上九，傾否，先否後喜。	象曰否終則傾，何可長也。"},
{"妄初九，無妄往，吉。	象曰「無妄之往」，得志也。","妄六二，不耕獲，不菑畬，則利用攸往。	象曰「不耕獲」，未富也。","妄六三，無妄之災，或繫之牛，行人之得，邑人之災。	象曰行人得牛，邑人災也。","妄九四，可貞。無咎。	象曰「可貞無咎」，固有之也。","妄九五，無妄之疾，勿藥有喜。	象曰「無妄之藥」，不可試也。","妄上九，無妄行，有眚，無攸利。	象曰「無妄之行」，窮之災也。"},
{"訟初六，不永所事，小有言，終吉。	象曰「不永所事」，訟不可長也。雖「小有言」，其辯明也。","訟九二，不克訟，歸而逋。其邑人三百戶，無眚。	象曰「不克訟」，歸逋竄也。自下訟上，患至掇也。","訟六三，食舊德，貞厲，終吉。或從王事，無成。	象曰食舊德，從上吉也。","訟九四，不克訟，復既命渝。安貞吉。	象曰復即命渝，安貞不失也。","訟九五：訟，元吉。	象曰「訟，元吉」以中正也。","訟上九：或錫之鞶帶，終朝三褫之。	象曰以訟受服，亦不足敬也。"},
{"履初九，素履往，無咎。	象曰「素履之往」，獨行願也。","履九二，履道坦坦，幽人貞吉。	象曰「幽人貞吉」，中不自亂也。","履六三，眇能視，跛能履，履虎尾，咥人，凶。武人為於大君。	象曰「眇能視」，不足以有明也。「跛能履」，不足以與行也。「咥人之凶」，位不當也。「武人為於大君」，志剛也。","履九四，履虎尾，愬愬，終吉。	象曰「愬愬終吉」。志行也。","履九五，夬履，貞厲。	象曰「夬履貞厲」，位正當也。","履上九，視履考祥，其旋元吉。	象曰元吉在上，大有慶也。"},
{"遯初六，遯尾，厲，勿用有攸往。	象曰「遯尾」之「厲」，不往何災也？","遯六二，執之用黃牛之革，莫之勝說。	象曰「執用黃牛」，固志也。","遯九三，系遯，有疾厲，畜臣妾吉。	象曰「系遯」之「厲」，有疾憊也。「畜臣妾吉」，不可大事也。","遯九四，好遯，君子吉，小人否。	象曰「君子好遯，小人否」也。","遯九五，嘉遯，貞吉。	象曰「嘉遯貞吉」，以正志也。","遯上九，肥遯，無不利。	象曰「肥遯無不利」，無所疑也。"},
{"同初九，同人於門，無咎。	象曰「出門同人」，又誰咎也。","同六二，同人於宗，吝。	象曰「同人於宗」，吝道也。","同九三，伏戎於莽，升其高陵，三歲不興。	象曰「伏戎於莽」，敵剛也。「三歲不興」，安行也。","同九四，乘其墉，弗克攻，吉	象曰「乘其墉」，義弗克也。其「吉」，則困而反則也。","同九五，同人先號咷而後笑，大師克，相遇。	象曰同人之先，以中直也。大師相遇，言相剋也。","同上九，同人於郊，無悔。	象曰「同人於郊」，志未得也。"},
{"姤初六，繫於金柅，貞吉。有攸往，見凶，羸豕孚蹢躅。	象曰「繫於金柅」，柔道牽也。","姤九二，包有魚，無咎，不利賓。	象曰「包有魚」，義不及賓也。","姤九三，臀無膚，其行次且，厲，無大咎。	象曰「其行次且」，行未牽也。","姤九四，包無魚，起凶。	象曰「無魚之凶」，遠民也。","姤九五，以杞包瓜，含章，有隕自天。	象曰九五含章，中正也。有隕自天，志不捨命也。","姤上九，姤其角，吝，無咎。	象曰「姤其角」，上窮吝也。"},
{"乾初九：潛龍，勿用。","乾九二：見龍在田，利見大人。","乾九三：君子終日乾乾，夕惕若厲，無咎。","乾九四：或躍在淵，無咎。","乾九五：飛龍在天，利見大人。","乾上九：亢龍，有悔。"}};
		*/
		static void procyao()
		{
			List<string>[] kole = new List<string>[64];
			
			for (int i = 0; i < 64; i++) {
				kole[i] = new List<string>();
			}
			
			string[] astr = File.ReadAllLines("htm/gg.txt");
			foreach (var st in astr) {
				if (st.Length < 4)
					continue;
				
				var sy = st.Substring(1, 2);
				if (seconsig.Contains(sy)) {
					char fasu = st[0];
					
					int k = -1;
					for (int i = 0; i < 64; i++) {
						if (gBlob.gua[i] == fasu) {
							k = i;
							break;
						}
					}
					
					if (k != -1) {
						kole[k].Add(st);
					}
				}
			}
			
			for (int i = 0; i < 64; i++) {
				var lu = kole[i];
				Console.Write("{");
				
				foreach (var st in lu) {
					Console.Write("\"" + st + "\",");
				}
				
				Console.WriteLine("},");
			}
		}
		
		static int[][] guarec;
		
		static void gpairr(int blenn, int rulen, bool tsuo = false)
		{
			guarec = bitarr.setblen(blenn, true);
			var kole = new List<bitarrPair>();
			byte[] rule = bitarr.GenRule()[rulen];
			var zeroone = new bitarrPair(bitarr.One, bitarr.Zero);
			
			kole.Add(zeroone);
			
			if (tsuo) {
				ProcTsuo(zeroone, rule, kole);
			} else {
				ProcPair(zeroone, rule, kole);
			}
			
			
			
			int lux = guarec.Length;
			/*
			for (int i = 0; i < lux; i++) {
				Console.WriteLine(gBlob.gua[i] + "::");
				for(int y=0;y<bitarr.blen;y++)
					Console.WriteLine("\t" + gBlob.Pgua(guarec[i][y],rule[y],tsuo));
				
				Console.ReadKey();
			}
			*/
			
			for (int i = 0; i < lux; i++) {
				Console.Write("[");
				
				var gg = guarec[i];
				for (int y = 0; y < bitarr.blen; y++)
					Console.Write(gg[y] + ",");
				
				Console.WriteLine("],\t\t//" + i);
			}
		}
		
		static bool notcontn(List<bitarrPair> kole, bitarrPair tzt)
		{
			foreach (var pp in kole) {
				if (pp.a[0].toInt() == tzt.a[0].toInt() && pp.a[1].toInt() == tzt.a[1].toInt())
					return false;
				
				if (pp.a[1].toInt() == tzt.a[0].toInt() && pp.a[0].toInt() == tzt.a[1].toInt())
					return false;
				
			}
			
			return true;
		}
		
		static void ProcTsuo(bitarrPair pair, byte[] rule, List<bitarrPair> kole)
		{
			Console.WriteLine(pair);
			
			var p0int = pair.a[0].toInt();
			var p0int_neg = pair.a[0].toInt(true);
			
			var p1int = pair.a[1].toInt();
			var p1int_neg = pair.a[1].toInt(true);
			
			
			for (int i = 0; i < bitarr.blen; i++) {
				var k = pair.a[0].Tsuo(i, rule[i]);
				
				var k0int = k.a[0].toInt();
				var k1int = k.a[1].toInt();
				
				guarec[p0int][i] = (k1int + 1);
				guarec[p0int_neg][rule[i]] = (k0int + 1);
					
				guarec[k1int][rule[i]] = -(p0int + 1);
				guarec[k0int][i] = -(p0int_neg + 1);
				
				if (notcontn(kole, k)) {
					kole.Add(k);
					ProcTsuo(k, rule, kole);
				}
				
				k = pair.a[1].Tsuo(i, rule[i]);
				
				k0int = k.a[0].toInt();
				k1int = k.a[1].toInt();
				
				guarec[p1int][i] = (k1int + 1);
				guarec[p1int_neg][rule[i]] = (k0int + 1);
					
				guarec[k1int][rule[i]] = -(p1int + 1);
				guarec[k0int][i] = -(p1int_neg + 1);
				
				if (notcontn(kole, k)) {
					kole.Add(k);
					ProcTsuo(k, rule, kole);
				}
				
			}
		}
		
		static void ProcPair(bitarrPair pair, byte[] rule, List<bitarrPair> kole)
		{
			Console.WriteLine(pair);
			
			var p0int = pair.a[0].toInt();
			
			var p1int = pair.a[1].toInt();
			
			
			for (int i = 0; i < bitarr.blen; i++) {
				var k = pair.xchng(i, rule[i]);
				
				var k0int = k.a[0].toInt();
				var k1int = k.a[1].toInt();
					

					
				guarec[p0int][i] = k1int;
				guarec[p1int][rule[i]] = k0int;
					
				guarec[k1int][rule[i]] = p0int;
				guarec[k0int][i] = p1int;
				
				if (notcontn(kole, k)) {
					
					
					
					kole.Add(k);
					ProcPair(k, rule, kole);
				}
			}
		}
		
		
		static void clearobjj()
		{
			kaza:
			string[] stt = File.ReadAllLines("jj.txt");
			foreach (var st in stt) {
				//if(st.Contains(@"\Release\obj\")||(st.Contains(@"\Release\")&&st.Contains(@".pdb")))
				if (st.Contains(@"\Release\")) {
					try {
						File.Delete(st);
					} catch {
					}
				} else {
					Console.WriteLine(st);
				}
			}
			Console.WriteLine("fin");
			Console.ReadKey();
			goto kaza;
		}
		
		static void testyamlbase()
		{
			UnityYAMLAsset.Load(@"Q:\ha\zempti\G1conso\bin\samp\ybase");
			UnityYAMLAsset.Output(@"Q:\ha\zempti\G1conso\bin\samp\ybaseout");
		}
		
		
		static void intai()
		{
			bool[] gua = new bool[6];
			while (true) {
				string ipu = Console.ReadLine();
				if (ipu.Length > 5) {
				
					for (int i = 0; i < 6; i++) {
						if (ipu[i] == '1')
							gua[5 - i] = true;
						else
							gua[5 - i] = false;
					}
				
					fmtout[1] = gua[0] ? "\\u25CF" : "\\u25CB";
					fmtout[3] = gua[0] ? "\\u25CB" : "\\u25CF";
				
					fmtout[5] = gua[1] ? "\\u25B2" : "\\u25B3";
					fmtout[7] = gua[1] ? "\\u25B3" : "\\u25B2";
				
					fmtout[9] = gua[2] ? "\\u25A0" : "\\u25A1";
					fmtout[11] = gua[2] ? "\\u25A1" : "\\u25A0";
				
					fmtout[13] = gua[3] ? "\\u25CF" : "\\u25CB";
					fmtout[15] = gua[3] ? "\\u25CB" : "\\u25CF";
				
					fmtout[17] = gua[4] ? "\\u25B2" : "\\u25B3";
					fmtout[19] = gua[4] ? "\\u25B3" : "\\u25B2";
				
					fmtout[21] = gua[5] ? "\\u25A0" : "\\u25A1";
					fmtout[23] = gua[5] ? "\\u25A1" : "\\u25A0";
				
					Console.WriteLine(string.Concat(fmtout));
				}
			}
		}
		
		static SerializedFile seefood;
		static YAMLfile sm;
		
		static void testsefoo()
		{
			
			
			SerializedFile sf = null;
			seefood = SerializedFile.Create("samp/u2018.bin", sf);
		}
		
		static void testyamlparse()
		{
			UnityYAMLAsset.GameBase = @"G:\s\Aisyo\Assets";
			sm = YAMLfile.Create(@"G:\s\Aisyo\Assets\AnimationClip\menu title.anim.meta");
		}
		
		static void testtg()
		{
			string[] txt = File.ReadAllLines("tzt.txt");
			StringGroup.Parse(txt, 0, txt.Length);
		}
		
		/*
		static void tztkla()
		{
			teztklaz tyk = new teztklaz();
			tyk.b1 = gg.iG1MG.objB[0].RealBlendMapping;
			int g1cot = gg.iG1MG.objB.Length;
			tyk.b2 = new Varray[g1cot][];
			
			int rr = 0;
			foreach (var gobjj in gg.iG1MG.objB) {
				tyk.b2[rr] = gobjj.byOrd;
				
				
				rr++;
			}
			
			BinaryFormatter bs2 = new BinaryFormatter();
			bs2.Serialize(File.OpenWrite("Q:\\tkla.bin"), tyk);
			
			//var selia = MessagePackSerializer.Serialize(tyk);
			//File.WriteAllBytes("Q:\\tkla.bin",selia);
		}
		
		
		static void chkset6()
		{
			var kdic = new Dictionary<int,List<int>>();
			var bonski = gg.iG1MG.map.s6;
			
			foreach (var bn in bonski) {
				foreach (var ma in bn.map) {
					kdic.AddToList(ma.MSid, (int)ma.MMid);
				}
			}
			
			foreach (var kp in kdic) {
				Console.Write(kp.Key);
				
				var ly = kp.Value.Distinct();
				
				foreach (var mm in ly)
					Console.WriteLine("=" + mm);
			}
			
			Console.ReadKey();
		}
		
		static void printvtx()
		{
			var lkk = gg.iG1MG.objB;
			foreach (var bb in lkk) {
				File.WriteAllText("t." + bb.ord + ".js", bb.ToCSV(true));
				File.WriteAllText("w." + bb.ord + ".js", bb.RealBlendMappingCSV(true));
			}
		}
		
		*/
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		static char[] zep = { '\\', '.' };
		static char[] zep2 = { '(', ')' };
		const string symb = " : error LNK2001: unresolved external symbol ";
		const string subase = @"E:\r\cud\alog";
		
		
		static void clen2()
		{
			Directory.CreateDirectory(subase + "/IN");
			Directory.CreateDirectory(subase + "/OUT");
			
			string[] loug = Directory.GetFiles(@"E:\r\cud\alog");
			foreach (string f in loug) {
				string[] li = File.ReadAllLines(f);
				int syl = li.Length;
				
				List<string> dupcolle = new List<string>();
				
				string srclib = Path.GetFileName(f).Replace(".log", string.Empty) + "\t";
				
				foreach (string line in li) {
					if (line == string.Empty)
						continue;
					
					string[] gli = line.Split('\t');
					if (!dupcolle.Contains(gli[1])) {
						dupcolle.Add(gli[1]);
					}
					
					string[] fasu = gli[0].ToLower().Split(zep2);
					
					
					
					string baselib = "/";
					int dbba = 0;
					if (fasu.Length > 1) {
						baselib = fasu[0].Replace(".lib", string.Empty) + "/";
						dbba = 1;
					}
					string inrec = subase + "/IN/" + baselib + fasu[dbba];
					if (File.Exists(inrec)) {
						File.AppendAllText(inrec, srclib + gli[1] + Environment.NewLine);
					} else {
						Directory.CreateDirectory(Path.GetDirectoryName(inrec));
						File.WriteAllText(inrec, srclib + gli[1] + Environment.NewLine);
					}
					
					
				}
				
				File.WriteAllLines(subase + "/OUT/" + Path.GetFileName(f), dupcolle.ToArray());
				
			}
		}
		
		
		static void clen1()
		{
			string[] loug = Directory.GetFiles(subase);
			foreach (string f in loug) {
				string[] li = File.ReadAllLines(f);
				int syl = li.Length;
				
				for (int i = 0; i < syl; i++) {
					if (li[i].Contains(symb)) {
						li[i] = li[i].Replace(symb, "\t");
					} else {
						li[i] = null;
					}
				}
				
				File.WriteAllLines(f, li);
			}
		}
		
		static void anadepe()
		{
			string[] depbase = File.ReadAllLines("11liblistp.txt");
			int ll = depbase.Length;
			
			
			for (int i = 0; i < ll; i++) {
				string depba = depbase[i];
				depbase[i] = string.Empty;
				File.WriteAllLines("11liblist.txt", depbase);
				system("zkl.bat " + depba.Split(zep)[1]);
				
				depbase[i] = depba;
				
			}
		}
	}
}