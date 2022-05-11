
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Win32.SafeHandles;
using System.Security.Cryptography;


namespace pSDtif
{
	public struct bbox
	{
		public int x1;
		public int y1;
		public int x2;
		public int y2;
		public int h
		{
			get{
				return y2-y1;
			}
		}
		public int w
		{
			get{
				return x2-x1;
			}
		}
		public int p
		{
			get{
				if(x1==x2 || y1==y2){return 0xFFFF;}
				
				int rx2=(int)psdsplit.gInfo[1];
				if(x2<rx2){rx2=x2;}
				int r1=x1;
				if(r1<0){r1=0;}
				rx2=((rx2-r1)<<8)/w;
				int ry2=(int)psdsplit.gInfo[0];
				if(y2<ry2){ry2=y2;}
				r1=y1;
				if(r1<0){r1=0;}
				ry2=((ry2-r1)<<8)/h;
				
				return rx2*ry2;
			}
		}
		public override String ToString()
        {
              return y1+"."+x1+"\n"+y2+"."+x2;
        }
		public void fill(string[] yfo)
		{
			yfo[0]=y1+"."+x1;
			string pv="V";
			if(p<0xD000){pv="X";}
			yfo[1]=h+"."+w+"."+pv+".";
		}
	}
	public struct LayerBlendingRanges
	{
		public ushort black;
		public ushort white;
	}
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	public struct ChnnInfo
	{
		public short id;
		public int DataLen;
		public override String ToString()
        {
			return id+" = "+DataLen.ToString("X");
        }
	}
	
	public struct MIB8s
	{
		public uint sig0;
		public uint sig;
		public int len;
	}
	
	public struct LayerInfo
	{
		public uint sig0;
		public uint Blnd;
		public byte opacity;
		public byte clipping;
		public byte flags;
		public byte filler;
		public int extraL;
		public int MskL;
		public override String ToString()
        {
			return "opa= "+opacity.ToString("X");
        }
		public unsafe void fill(string[] yfo)
		{
			string iz=null;
			if(opacity!=0xff)
			{
			iz="."+opacity.ToString("X")+".";
			}
			else {iz="._.";}
			
			int flg=0;
			
			
			switch(Blnd)
			{
					case 0x6E6F726D:      //      mron
						flg=0;
						break;
					case 0x6D756C20:      //       lum
						flg=1;
						break;
					case 0x7363726E:      //      nrcs
						flg=2;
						break;
					case 0x6F766572:      //      revo
						flg=3;
						break;
					case 0x734C6974:      //      tiLs
						flg=9;
						break;
					case 0x6C646467:	//lddg
						flg=21;
						break;

				default:
						Console.WriteLine("!case 0x"+Blnd.ToString("X")+":");
					break;
			}
			if((flags&2) !=0)
			{
				flg+=32;
			}
			yfo[0]+=iz+flg.ToString("X");
			
		}
		
	}
	
	
	static class psdsplit
	{
	
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		
		
		public static char[] sep0x9 = { '\t' };
		public static char[] sepWpa = { '\\' };
		public static char[] sepQuo_s = { '\'' };
		public static char[] sepDuo = { ',' };
		public static char[] sepDot = { '.' };
		public static char[] sep0x20 = { '@' };
		public static char[] sepAstro={'*'};
		public static char[] sepgun={'|'};
		
		
		

	[DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr securityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern int GetFinalPathNameByHandle([In] SafeFileHandle hFile, [Out] StringBuilder lpszFilePath, [In] int cchFilePath, [In] int dwFlags);

		
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern int GetCompressedFileSizeW(string inname, int opnul);
    
	[DllImport("DLLWIN.dll", CharSet = CharSet.Unicode)]
    static extern void UnsafeWrite_Open(string lpFileName);
    
    [DllImport("DLLWIN.dll")]
    static unsafe extern void UnsafeWrite_Write(byte* buff,int len);
    
    [DllImport("DLLWIN.dll")]
    static unsafe extern void UnsafeWrite_Write(byte[] buff,int len);
    
    [DllImport("DLLWIN.dll")]
    static extern void UnsafeWrite_Close();

    public static byte[] jtifhd=  {
	0x49, 0x49, 0x2A, 0, 8, 0, 0, 0, 0x10, 0, 0xFE, 0, 4, 0, 1, 0, 
	0, 0, 0, 0, 0, 0, 0, 1, 3, 0, 1, 0, 0, 0, 0xE8, 0x12, 
	0, 0, 1, 1, 3, 0, 1, 0, 0, 0, 0x89, 0x0A, 0, 0, 2, 1, 
	3, 0, 3, 0, 0, 0, 0xCA, 0, 0, 0, 3, 1, 3, 0, 1, 0, 
	0, 0, 7, 0, 0, 0, 6, 1, 3, 0, 1, 0, 0, 0, 6, 0, 
	0, 0, 0x11, 1, 4, 0, 1, 0, 0, 0, 0xD8, 0, 0, 0, 0x12, 1, 
	3, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0x15, 1, 3, 0, 1, 0, 
	0, 0, 3, 0, 0, 0, 0x16, 1, 3, 0, 1, 0, 0, 0, 0x89, 0x0A, 
	0, 0, 0x17, 1, 4, 0, 1, 0, 0, 0, 0x42, 0, 0, 0, 0x1A, 1, 
	5, 0, 1, 0, 0, 0, 0xD0, 0, 0, 0, 0x1B, 1, 5, 0, 1, 0, 
	0, 0, 0xD0, 0, 0, 0, 0x1C, 1, 3, 0, 1, 0, 0, 0, 1, 0, 
	0, 0, 0x28, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0, 0x5C, 0x93, 
	7, 0, 0xA8, 0x65, 0x6F, 9, 0x20, 1, 0, 0, 8, 0, 8, 0, 8, 0, 
	0x80, 0xFC, 0x0A, 0, 0x10, 0x27, 0, 0, 0xFF, 0xD8, 0xFF, 0xEE, 0, 0x0E, 0x41, 0x64, 
	0x6F, 0x62, 0x65, 0, 0x64, 0x80, 0, 0, 0, 1, 0xFF, 0xC0, 0, 0x11, 8, 0, 
	1, 0, 1, 3, 1, 0x22, 0, 2, 0x11, 1, 3, 0x11, 1, 0xFF, 0xDD, 0, 
	4, 0, 1, 0xFF, 0xDA, 0, 0x0C, 3, 1, 0, 2, 0x11, 3, 0x11, 0, 0x3F, 
	0, 0xDD, 0x5F, 0x92, 0x57, 0xE4, 0x1F, 4, 0xFF, 0xD9, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 
	0x41, 0x64, 0x6F, 0x62, 0x65, 0x20, 0x50, 0x68, 0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x20, 
	0x44, 0x6F, 0x63, 0x75, 0x6D, 0x65, 0x6E, 0x74, 0x20, 0x44, 0x61, 0x74, 0x61, 0x20, 0x42, 0x6C, 
	0x6F, 0x63, 0x6B, 0, 0x4D, 0x49, 0x42, 0x38, 0x72, 0x79, 0x61, 0x4C,				0,0,0,0};
    
    static void fdump(string fname,byte[] buf,int off,int len)
    {
    	using(FileStream fs = File.OpenWrite(fname))
    	{
    		fs.Write(buf,off,len);
    	}
    }
    
    static unsafe void minify(string fna)
    {
    	
    	
    	
    	File.Move(fna,fna+".bye");
    	uint gW=0;
    	uint gH=0;
    	int lyrOff=0;
    	int lyrLen=0;
    	fixed(byte* src=&psd[0])
    	{
    		byte* cur=src+8;
    		int ydx=3*(*(ushort*)cur);
    		cur+=2;
    		uint* toc=(uint*)cur;
    		
    		for(int i=0;i<ydx;i+=3)
    		{
    			uint sig=toc[i]&0xffff;
    			switch(sig)
    			{
    				case 0x100:
    					gW=toc[i+2];
    					break;
    				case 0x101:
    					gH=toc[i+2];
    					break;
    				case 0x935c:
    					
    					lyrOff=(int)(toc[i+2]);
    					lyrLen=psd.Length-lyrOff;
    					lyrOff-=0x120;
    					break;
    				default:
    					break;
    			}
    		}
    		
    	}
    	
    	Array.Copy(jtifhd,0,psd,lyrOff,0x120);
    	fixed(byte* src=&psd[lyrOff])
    	{
    		uint* yfo=(uint*)(src+0xa);
    		yfo[5]=gW;
    		yfo[8]=gH;
    		yfo[29]=gH;
    		yfo[46]=(uint)lyrLen;
    		
    	}
    	fdump(fna,psd,lyrOff,lyrLen+0x120);
    	
    	
    }
    
    static unsafe int MIB8dump(byte* lcur,string[] yfodump)
    {
    	byte* cur = lcur;
    	MIB8s info=*(MIB8s*)cur;
    	
    	cur+=0xC;
    	switch(info.sig)
    	{
    		case 0x6C756E69:       //      inul
    			yfodump[10]=Encoding.Unicode.GetString(cur+4,(*(int*)cur)*2);
				break;
			case 0x6C736374:       //      tcsl
				uint typ=*(uint*)cur;

				if(typ==1 || typ==2){yfodump[11]="F";yfodump[12]=typ.ToString();}
				else if(typ==3){yfodump[11]="P";}
				else{yfodump[11]="X"+typ;};
				break;
			case 0x6C6E7372:       //      rsnl
				typ=*(uint*)cur;
				if(typ==0x6c617972){yfodump[12]+="L";}
				else if(typ==0x6c736574){yfodump[12]+="F";}
				else if(typ==0x636f6e74){yfodump[12]+="C";}
				else if(typ==0x72656E64){yfodump[12]+="R";}
				else {Console.WriteLine("^^case 0x"+typ.ToString("X")+":\t//\t"+Encoding.ASCII.GetString(cur,4));}
				break;
			case 0x6C796964:       //      diyl
				yfodump[13]="#"+(*(uint*)cur);
				break;
    		case 0x43674564:       //      dEgC
				break;
			case 0x54795368:       //      hSyT	text
				break;
				
			
			case 0x6C73646B:        //      kdsl
				break;
			case 0x6C6D676D:        //      mgml
				break;
				
			case 0x626C6E63:        //      cnlb
				break;
			case 0x63757276:        //      vruc
				break;
			case 0x68756532:        //      2euh
				break;
			case 0x7068666C:        //      lfhp	photofilter
				break;
			case 0x76696241:        //      Abiv	Vibrance
				break;
			case 0x506C4C64:        //      dLlP
				break;
			case 0x536F4C64:        //      dLoS
				break;
			case 0x74736C79:        //      ylst
				break;

			
			case 0x62727374:       //      tsrb	Channel blending restrictions
				break;
			case 0x6C724658:       //      XFrl	lyrEffect
				break;

			case 0x6C667832:       //      2xfl	lyrEffect2
				break;
			
			case 0x6C797672:       //      rvyl	PSver=130
				break;
				
			
			case 0x73686D64:       //      dmhs
				break;
			case 0x66787270:       //      prxf
				double* po=(double*)cur;
				yfodump[23]=po[0]+","+po[1];
				break;
			case 0x636C626C:       //      lblc
				break;
			case 0x696E6678:       //      xfni
				break;
			case 0x6B6E6B6F:       //      oknk
				break;
			case 0x6C737066:       //      fpsl
				break;
			case 0x62726974:       //      tirb
				break;
			case 0x6C636C72:       //      rlcl
				break;
    		default:
				Console.WriteLine("case 0x"+info.sig.ToString("X")+":\t//\t"+Encoding.ASCII.GetString(cur-8,4));
    			break;
    	}
    	
    	return 0xC+info.len;
    }
    
    static unsafe void XtraInfo(byte* lcur,string[] yfodump,int endlen)
    {
    	byte* cur = lcur;
    	int mskL=*(int*)cur;
    	cur+=4;
    	if(mskL!=0)
    	{
    		//Console.WriteLine(((ulong)lcur).ToString("X"));
    		//Console.ReadKey();
    		bbox bo=*(bbox*)cur;
    		
    		
    		yfodump[2]=bo.y1+"."+bo.x1+"."+bo.h+"."+bo.w+"."+(*(uint*)(cur+0x10)).ToString("X");
    		
    		
    		cur+=mskL;
    	}
    	mskL=*(int*)cur;
    	cur+=4;
    	int nBlending_ranges=mskL>>2;
    	LayerBlendingRanges* Blending_ranges=(LayerBlendingRanges*)cur;
    	cur+=mskL;
    	mskL=(int)cur[0]+1;
    	int zz=mskL%4;
    	if(zz!=0){mskL+=(4-zz);}
    	cur+=mskL;
    	endlen-=(int)(cur-lcur);
    	while(endlen>0)
    	{
    		int rdl=MIB8dump(cur,yfodump);
    		endlen-=rdl;
    		cur+=rdl;
    	}
    	
    }

    static int ReadTo(string fna,int icur)
    {
    	int ady=0;
    	using(FileStream fs = File.OpenRead(fna))
    	{
    		ady=(int)fs.Length;
    		fs.Read(psd,icur,ady);
    	}
    	return ady;
    }

    static unsafe void recon(string fpai)
    {
    	string fpa=fpai+"/";
    	dpaz=File.ReadAllLines(fpa+"9paz.txt");
    	lyrCot=dpaz.Length-1;
    	string[] zet=dpaz[lyrCot].Split(sepDot);
    	gInfo[0]=Convert.ToUInt32(zet[0] , 16);
    	gInfo[1]=Convert.ToUInt32(zet[1] , 16);
    	int icur=Convert.ToInt32(zet[2] , 16)<<12;
    	psd=new byte[icur];
    	icur=0;
    	Array.Copy(jtifhd,psd,0x150);
    	icur+=0x150;
    	icur+=ReadTo(fpa+"0.blo",icur);
    	fixed(byte* src=&psd[0x150])
    	{
    		
    		byte* cur=src+2;
    		for(int vv=0;vv<lyrCot;vv++)
    		{
    			string bspa=fpa+dpaz[vv];
    			cur+=0x10;
    			short chn=*(short*)cur;
    			
    			cur+=2;
    			ChnnInfo* cyfo=(ChnnInfo*)cur;
    			for(int i=0;i<chn;i++)
    			{
    				if(cyfo[i].DataLen!=2)
    				{
	    				int rsz=ReadTo(bspa+cyfo[i].id+".bin",icur);
	    				cyfo[i].DataLen=rsz;
	    				icur+=rsz;
    				} else {icur+=2;}
    			}
    			
    			cur+=6*chn;
    			LayerInfo lyfo=*(LayerInfo*)cur;
    			cur+=0x10;
    			
    			cur+=lyfo.extraL;
    			
    			
    		}
    	}
    	int rszv=icur-0x150;
    	int zz=(rszv+8)&0xf;
    	if(zz!=0){icur+=(0x10-zz);}
    	
    	
    	icur+=ReadTo(fpa+"1.blo",icur);
    	zz=(icur-0x120);
    	
    	fixed(byte* src=&psd[0])
    	{
    		uint* yfo=(uint*)(src+0xa);
    		
    		
    		yfo[8]=gInfo[0];
    		yfo[29]=gInfo[0];
    		yfo[5]=gInfo[1];
    		yfo[46]=(uint)zz;
    		int* rsz=(int*)(src+0x14C);
    		rsz[0]=rszv;
    		
    	}
    	using(FileStream fs = File.OpenWrite(fpai+".tif"))
    	{
    		fs.Write(psd,0,icur);
    	}
    	
    }
    static string[][] yfoAll;
    static string[] dpaz;
    static int lyrCot;
    public static byte[] psd;
    public static uint[] gInfo={0,0,0};
    
    static void makeHie()
    {
    	List<string> paPath=new List<string>();
		for(int vv=lyrCot-1;vv>=0;vv--)
		{
			string odpaz=dpaz[vv];
			if(paPath.Count!=0)
			{
				dpaz[vv]=string.Join(string.Empty,paPath)+odpaz;
			}
			
			switch(yfoAll[vv][11])
			{
				case "F":
					paPath.Add(odpaz);
					break;
				case "P":
					dpaz[vv]=string.Empty;
					paPath.RemoveAt(paPath.Count - 1);
					break;
				
			}
			//Console.WriteLine(dpaz[vv]+" = "+yfoAll[vv][10]);
			
		}
    }
    
    static unsafe void parse(string fna)
    {
    	
    	if(!File.Exists(fna+".tif"))
    	{
    		recon(fna);
    		return;
    	}
    	psd=File.ReadAllBytes(fna+".tif");
    	
    	
    	
    	fixed(byte* src=&psd[0])
    	{
    		uint* yfo=(uint*)(src+0xa);
    		
    		if(yfo[47]!=0x120)
    		{
    			Console.WriteLine("CleanItFirst");
    			minify(fna+".tif");
    			return;
    		}
    		gInfo[0]=yfo[8];
    		gInfo[1]=yfo[5];
    		gInfo[2]=yfo[46];
    		
    	}
    	Directory.CreateDirectory(fna);
    	Directory.SetCurrentDirectory(fna);
    	
    	ChnnInfo[][] dta=null;
    	
    	
    	fixed(byte* src=&psd[0x14C])
    	{
    		int len=*(int*)src;
    		int xtraOff=0x150+len;
    		int lenAg=len%4;
    		if(lenAg!=0){xtraOff+=(4-lenAg);}
    		fdump("1.blo",psd,xtraOff,psd.Length-xtraOff);
    		byte* cur=src+4;
    		
    		lyrCot=*(ushort*)cur;
    		cur+=2;
    		dta=new ChnnInfo[lyrCot][];
    		yfoAll=new string[lyrCot][];
    		dpaz=new string[lyrCot+1];
    		dpaz[lyrCot]=gInfo[0].ToString("X")+"."+gInfo[1].ToString("X")+"."+((3*gInfo[2])>>13).ToString("X");
    		for(int vv=0;vv<lyrCot;vv++)
    		{
    			
    			dpaz[vv]=(lyrCot-vv)+"\\";
    			
    			
    			string[] yfodump=new string[25];
    			
    			bbox bb=*(bbox*)cur;
    			bb.fill(yfodump);
    			//Console.WriteLine("area= "+bb.p.ToString("X"));
    			cur+=0x10;
    			short chn=*(short*)cur;
    			ChnnInfo[] sto=new ChnnInfo[chn];
    			cur+=2;
    			ChnnInfo* cyfo=(ChnnInfo*)cur;
    			for(int i=0;i<chn;i++)
    			{
    				sto[i]=cyfo[i];
    				//Console.WriteLine(cyfo[i]);
    			}
    			dta[vv]=sto;
    			cur+=6*chn;
    			LayerInfo lyfo=*(LayerInfo*)cur;
    			lyfo.fill(yfodump);
    			
    			
    			//Console.WriteLine(lyfo);
    			//Console.WriteLine("!case 0x"+lyfo.Blnd.ToString("X")+":\t//\t"+Encoding.ASCII.GetString(cur+4,4));
    			
    			
    			cur+=0x10;
    			XtraInfo(cur,yfodump,lyfo.extraL);
    			cur+=lyfo.extraL;
    			yfoAll[vv]=yfodump;
    			//File.WriteAllLines(dpa+"/n.txt",yfodump);
    			
    		}
    		int dtaHier=(int)(cur-src);
    		fdump("0.blo",psd,0x150,dtaHier-4);
    		
    		makeHie();
    		File.WriteAllText("9paz.txt", string.Join("\n",dpaz).Replace("\\","/"));
    		
    		Console.WriteLine("dtaHier: "+(dtaHier+0x14C).ToString("X"));
    		Console.ReadKey();
    		for(int vv=0;vv<lyrCot;vv++)
    		{
    			string dpa=dpaz[vv];
    			
    			var cdta=dta[vv];
    			int chn = cdta.Length;
    			string[] kompinfo=new string[chn+1];
    			bool dump=false;
    			if(dpa!=string.Empty)
    			{
    				dump=true;
    				if(!Directory.Exists(dpa)){system("md "+dpa);}
    				
    			}
    			
    			int ttal=0;
    			for(int i=0;i<chn;i++)
    			{
    				int syl=cdta[i].DataLen;
    				string zuci="0";
    				if(syl>2)
    				{
    					zuci=(*(ushort*)cur).ToString();
    					string ofpa=dpa+cdta[i].id+".bin";
    					if(!File.Exists(ofpa))
    					{
    					UnsafeWrite_Open(ofpa);
    					UnsafeWrite_Write(cur,syl);
    					UnsafeWrite_Close();
    					}
    				}
    				kompinfo[i+1]=zuci;
    				cur+=syl;
    				ttal+=syl;
    			}
    			if(dump)
    			{
	    			var yfovv=yfoAll[vv];
	    			kompinfo[0]=yfovv[1];
	    			if(yfovv[1].EndsWith(".X."))
	    			{
	    				string yd="badSma:\t\\";
	    				if(ttal>0xD0000){yd="badBig:\t\\";}
	    				Console.WriteLine(yd+dpa+"\t"+yfovv[10]);
	    			}
	    			
	    			
	    			yfovv[1]=string.Join(string.Empty,kompinfo);
	    			File.WriteAllLines(dpa+"n.txt",yfovv);
    			}
    		}
    		
    		
    	}
    }
    
		
		static void Main(string[] args)
		{
			
			if(args.Length > 1)
			{
				parse(args[0]);
				return;
			}
			string fna=args[0]+".tif";
			psd=File.ReadAllBytes(fna);
			minify(fna);
			
		}
		
	}
}