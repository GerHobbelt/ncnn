
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

namespace eroneto
{

	class pbm
	{
		public byte[] data;
		public byte[] bslut;
		public int cz;
		public int cy;
		public int cx1;
		public int cx2=0;
		public unsafe ushort* lut;
		public ulong[] tmp8=new ulong[8];
		
		public unsafe pbm(int zstart,string lutna="30030")
		{
			cz=zstart;
			data=File.ReadAllBytes(@"Q:\z\bookpdf\0bak\tu\gg\prm\"+zstart.ToString("D3")+".pbm");
			bslut=File.ReadAllBytes(@"Q:\z\bookpdf\0bak\tu\gg\ord2n."+lutna);
			fixed(byte* izz=&bslut[0])
			{
				lut=(ushort*)izz;
			}
		}
		
		public void seekto(ulong ptt)
		{
			ulong rez=ptt%0xD4D8070U;
			cy=(int)(rez/30030);
			
		}
		
		
		
		public unsafe ulong val
		{
			get{
				
				for(;cy<7432;cy++)
				{
					ulong bscy=30030*(ulong)cy;
					for(;cx1<720;cx1++)
					{
						byte vv=data[cy*720+cx1+13];
						
						
						int bscx1=cx1<<3;
						for(;cx2<8;)
						{
							if(cx2==0)
							{
								Array.Clear(tmp8, 0, 8);
								
								if((vv&0x80)!=0)
									tmp8[0]=bscy+lut[bscx1];
								
								if((vv&0x40)!=0)
									tmp8[1]=bscy+lut[bscx1+1];
								
								if((vv&0x20)!=0)
									tmp8[2]=bscy+lut[bscx1+2];
								
								if((vv&0x10)!=0)
									tmp8[3]=bscy+lut[bscx1+3];
								
								if((vv&8)!=0)
									tmp8[4]=bscy+lut[bscx1+4];
								
								if((vv&4)!=0)
									tmp8[5]=bscy+lut[bscx1+5];
								
								if((vv&2)!=0)
									tmp8[6]=bscy+lut[bscx1+6];
								
								if((vv&1)!=0)
									tmp8[7]=bscy+lut[bscx1+7];
								
								
							}
							
							var rett=tmp8[cx2];
							if(rett!=0){cx2++;return rett;}
							cx2++;
							
							
						}
						cx2=0;
						
					}
					cx1=0;
					
					
					
					
				}
				
				return 0;
				
				
			}
		}
	}
	
	class prmWRK
	{
		public bool[] flgz;
		public ushort[] prm16;
		public ulong blsiz=0x80000000-0x80;
		
		[DllImport("kernel32.dll")]
		public static extern uint GetTickCount();
		
		ulong calcdyvi(ulong vsta,ulong skstart)
		{
			
			
			ulong dyvi=1+(skstart/vsta);
			
			if(dyvi==1)
			{
				dyvi=3;
			}
			else if((dyvi&1)==0)
			{
					dyvi+=1;
			}
			return vsta*dyvi;
		}
		
		void dloop_old(int biw,List<ushort> pool)
		{
			int skstart=	1<<(biw-1);
			int skendo=1<<((biw<<1)-1);
			
			
			
			
			//Parallel.For(skstart, skendo,i =>
			for(int i=skstart;i<skendo;i++)
			{
			             	if(flgz[i])
			             	{
			             		
			             	}
			             	else
			             	{
			             		
			             		
			             		
			             		ulong vsta=(1U+((ulong)i<<1));
			             		pool.Add((ushort)vsta);
								ulong v_add=vsta<<1;

								vsta*=3;
								
								
								for(;vsta<blsiz;vsta+=v_add)
								{
									flgz[(vsta>>1)]=true;
								}
			             		
			             	}
			 }	//);
			
			
		}
		
		void dloop(int biw,List<ushort> pool)
		{
			ulong skstart=	1U<<(biw-1);
			ulong skendo=1U<<((biw<<1)-1);
			
			
			
			
			//Parallel.For(skstart, skendo,i =>
			for(ulong i=skstart;i<skendo;i++)
			{
			             	if(flgz[i])
			             	{
			             		
			             	}
			             	else
			             	{
			             		
			             		ulong vsta=(1U+(i<<1));
			             		pool.Add((ushort)vsta);
			             		
			             		
			             		
			             		
			             		
			             		ulong j=((blsiz-1U-i)/vsta);
			             		for(;j>=i;j--)
								{
			             			unchecked
									{
									if(flgz[j])
									{
										
									}
									else
									{
										flgz[(i*j<<1)+i+j]=true;
									}
			             			}
								}
								
			             		
			             		
			             		
			             	}
			 }	//);
			
			
		}
		
		public prmWRK()
		{
			List<ushort> list_prm16=new List<ushort>();
			flgz=new bool[(int)blsiz];
			flgz[0]=true;
			
			
			int biw=1;
			
			/*
			blsiz<<=1;
			for(int i=0;i<4;i++)
			{
				uint sta=GetTickCount();
				dloop_old(biw,list_prm16);
				Console.WriteLine("time: "+(GetTickCount()-sta));
				biw<<=1;
			}
			blsiz>>=1;
			*/
			
			//==========
			for(int i=0;i<4;i++)
			{
				uint sta=GetTickCount();
				dloop(biw,list_prm16);
				Console.WriteLine("time: "+(GetTickCount()-sta));
				biw<<=1;
			}
			
			//==========
			
			
			prm16=list_prm16.ToArray();
			ulong pcot=(ulong)prm16.Length;
			Console.WriteLine(pcot);
			
			
			
			for(ulong i=32768;i<blsiz;i++)
			{
				if(flgz[i])
           		{
             		
             	}
             	else
             	{
             		pcot++;
             		//Console.WriteLine((i<<1)+1);
             		//if(pcot%10==0){Console.ReadKey();}
             		
             	}
			}
			
			Console.WriteLine(pcot);
			
			
			
		}
	}
	
	
	class calcsta
	{
		public virtual ulong calc(ulong vsta,ulong skstart)
		{
			return vsta*3;
		}
	}
	
	class calcstaNon0 : calcsta
	{
		public override ulong calc(ulong vsta,ulong skstart)
		{
			ulong dyvi=(skstart/vsta);
			if(skstart-dyvi*vsta!=0)
			{
				dyvi++;
			}
			
			if((dyvi&1)==0)
			{
					dyvi+=1;
			}
			return vsta*dyvi;
		}
	}
	
	static class utily
	{
	
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		const string rensrc=@"Q:\z\bookpdf\0bak\tu\xx\rp\";
		
		public static char[] sepDuo = { ',' };
		public static char[] sep0x20 = { '@' };
		
		static void konren()
		{
			string[] dysr=Directory.GetFiles(rensrc+"a","*.gif",SearchOption.TopDirectoryOnly);
			string[] resr=Directory.GetFiles(rensrc+"b","*.gif",SearchOption.TopDirectoryOnly);
			int stl=dysr.Length;
			for(int i=0;i<stl;i++)
			{
				File.Move(resr[i],"_"+dysr[i].Substring(29));
				
			}
		
		}

		static bool[] prm16flg;
		static ushort[] prm16;
		
		static void fillprm16()
		{
			var it = new pbm(0);
			int valput=5;
			for(int i=0;i<0x10000;i++)
			{
				var thava=it.val;
				if(thava>0x10000)
				{
					break;
				}
				prm16[valput]=(ushort)thava;
				valput++;
				
			}
			
		}
		
		static void prm16to32()
		{
			prm16=new ushort[6541];
			ulong skstart=	0;//0xF0000000;
			ulong howmany=	0x100000000-0x80;
			
			ulong skendo=skstart+howmany;
			
			howmany>>=1;
			
			calcsta stacal;
			
			if(skstart==0)
			{
				stacal=new calcsta();
			}
			else
			{
				stacal=new calcstaNon0();
			}
			
			
			ulong skstarthalf=skstart>>1;
			prm16[0]=3;
			prm16[1]=5;
			prm16[2]=7;
			prm16[3]=11;
			prm16[4]=13;
			fillprm16();
			prm16flg=new bool[howmany];
			
			for(int i=0;i<6541;i++)
			{
				ulong vsta=(ulong)prm16[i];
				ulong v_add=vsta<<1;
				
				
				vsta=stacal.calc(vsta,skstart);
				for(;vsta<skendo;vsta+=v_add)
				{
					unchecked{
						var fg=(vsta>>1)-skstarthalf;
						
					prm16flg[fg]=true;
					}
				}
			}
			Console.WriteLine("flagfin");
			ulong pcot=0;
			ulong laztval=0;
			for(ulong i=0;i<howmany;i++)
			{
				if(prm16flg[i])
				{
					
				}
				else
				{
					laztval=skstart+1+(i<<1);
					
					pcot++;
					
					Console.WriteLine(laztval);
					if(pcot%10==0)
						Console.ReadKey();
					
				}
			}
			Console.WriteLine(pcot);
			Console.WriteLine(laztval.ToString("X8"));
			
		}

		static ulong[] maxvalz={3,3,3,3,3,
			3,3,3,3,3};
		
		static void prmprrt(int digi)
		{
			
			var dick=prmp_dict(digi);
			foreach(var kp in dick)
			{
				string tali=kp.Key;
				foreach(var sh in kp.Value)
				{
					Console.WriteLine(sh.ToString("D2")+tali);
				}
			}
			
		}
		
		static Dictionary<string,List<int>> prmp_dict(int digi)
		{
			maxvalz=new ulong[]{3,3,3,3,3,
			3,3,3,3,3};
			ushort[] prmbs={3,5,7,11,13,
							17,19,23,29,31};
			uint maxsv=2;
			
			
			
			for(int i=1;i<digi;i++)
			{
				var thenum=prmbs[i];
				maxsv*=(uint)(thenum-1);
				for(int v=0;v<(digi-i);v++)
				{
					maxvalz[v]*=thenum;
				}
				
				
			}
			
			uint maxval=(uint)maxvalz[0];
			ulong maxvalx2=maxval<<1;
			bool[] flg=new bool[maxval];
			
			for(int i=0;i<digi;i++)
			{
				ulong v_add=(ulong)prmbs[i]<<1;
				for(ulong v=prmbs[i];v<maxvalx2;v+=v_add)
				{
					flg[(v-1)>>1]=true;
				}
			}
			
			Dictionary<string,List<int>> reita=new Dictionary<string,List<int>>();
			
			//for(int i=0;i<10;i++){maxvalz[i]<<=1;}
			
			for(ulong i=0;i<maxval;i++)
			{
				if(flg[i]){}
				else{
					
					ulong val=i;//(i<<1)+1;
					string kstr=string.Empty;
					int[] dvs = new int[digi];
					for(int v=1;v<digi;v++)
					{
						ulong dvss=val/maxvalz[v];
						val-=dvss*maxvalz[v];
						
						dvs[v]=(int)dvss;
					}
					
					string sig=string.Empty;
					for(int v=2;v<digi;v++)
					{
						sig+=","+dvs[v].ToString("D2");
						
					}
					sig+=","+val.ToString("D2");
					
					List<int> rrr;
					if(reita.TryGetValue(sig,out rrr))
					{
						rrr.Add(dvs[1]);
					}
					else
					{
						rrr=new List<int>();
						rrr.Add(dvs[1]);
						reita[sig]=rrr;
					}
					
					//Console.WriteLine();
				
				}
			}
			
			return reita;
		}
		
		static string fix0x20(string inin)
		{
			string[] mmg=inin.Split(sep0x20);
			var mmgll=mmg[1].Length-1;
			var st1=mmg[1].Substring(0,mmgll);
			var st2=mmg[1].Substring(mmgll);
			return mmg[0]+"@"+st2+st1+"@"+mmg[2];
		}
		static Dictionary<string,StreamWriter> dzt=new Dictionary<string,StreamWriter>();
		static void todzt(string inin)
		{
			//inin=fix0x20(inin);
			inin=inin.Replace("female","girl");
			if(inin.Contains("guro")||inin.Contains("scat")||inin.Contains("furry")||inin.Contains("yaoi")||inin.Contains("males-only"))
			{
				dzt["trash"].WriteLine(inin);
				return;
			}
			
			string tali=string.Empty;
			
			if(inin.Contains("lolicon")||inin.Contains("bondage")||inin.Contains("rape"))
			{
				tali="-lo";
			}
			
			
			string[] smg=inin.Replace(" ",string.Empty).Split(sepDuo);
			
			//Console.WriteLine(smg[3]+tali);
			dzt[smg[3]+tali].WriteLine(inin);
			
				
		}

		static void rrsrt(string fna,string ky)
		{
			fna+=".txt";
			Dictionary<ulong,List<string>> oq = new Dictionary<ulong,List<string>>();
			string[] kyy=File.ReadAllLines(ky);
			int kyl=kyy.Length;
			
			
			
			
			string[] stt=File.ReadAllLines("../"+fna);
			int sttl=stt.Length;
			int endpause=0;
			
			//int wholeloop=0; //!!!!!!!!!
			
			string lztsig=string.Empty;
			for(int i=0;i<sttl;i++)
			{
				var oz=stt[i].Replace(" ",string.Empty).Split(sepDuo);
				var ozl=oz.Length;
				if(ozl<4){continue;}
				if(oz[0]==lztsig)
				{
					Console.WriteLine("imhentai.xxx/gallery/"+oz[2]+"/");
					endpause++;
					continue;
				}
				lztsig=oz[0];
				
				ulong skor=0;
				
				/*
				//184329,583902
				for(int v=0;v<kyl;v++)
				{
					int izp=Array.IndexOf(oz,kyy[v]);
					if(izp<0){wholeloop+=ozl;}
					else{skor+=((ulong)1<<v);wholeloop+=izp;}
				}
				*/
				
					
				//138241,423662
				for(int v=3;v<ozl;v++)
				{
					int izp=Array.IndexOf(kyy,oz[v]);
					if(izp>0){skor+=((ulong)1<<izp);
						//wholeloop+=izp;
					}
					else if(izp==0){skor++;}
					//else{wholeloop+=kyl;}
				}
				
				
				
				/*
				for(int v=0;v<3;v++){oz[v]=string.Empty;}
				string tztstr=string.Join(",",oz);
				for(int v=0;v<kyl;v++)
				{
					if(tztstr.Contains(kyy[v]))
					{
						skor+=((ulong)1<<v);
					}
					
				}
				*/
				
				List<string> kle;
				if(oq.TryGetValue(skor,out kle))
				{
					kle.Add(stt[i]);
				}
				else
				{
					kle=new List<string>();
					kle.Add(stt[i]);
					oq[skor]=kle;
				}
				
			}
			
			var sortedDict = from entry in oq orderby entry.Key descending select entry.Value;
			List<string> rzt=new List<string>();
			foreach(var ss in sortedDict)
			{
				if(ss.Count==1){
				rzt.Add(ss[0]+", ^");}
				else{
					rzt.Add(string.Join("\n",ss));
					rzt.Add("//==");
				}
				
			}
			rzt.Add(string.Empty);
			File.WriteAllText(fna,string.Join("\n",rzt));

			//Console.WriteLine(wholeloop);
			if(endpause>0){
				Console.WriteLine("dup: "+endpause);
				Console.ReadKey();
				
				Console.ReadKey();}
		
		}

		static void rrslp()
		{
			lpha:
			Console.WriteLine("to Clean: ");
			string fna=Console.ReadLine();
			string ky="ky.txt";
			if(fna=="3trash"){ky="kygb.txt";}
			rrsrt(fna,ky);
			goto lpha;
		}
		static Dictionary<string,int> oqcot;

		static void gpsrt(string[] stt)
		{
			
			int sttl=stt.Length;
			
			
			
			for(int i=0;i<sttl;i++)
			{
				var oz=stt[i].Replace(" ",string.Empty).Split(sepDuo);
				var ozl=oz.Length;
				if(ozl<4){continue;}
				for(int v=3;v<ozl;v++)
				{
					int rfout=0;
					if(oqcot.TryGetValue(oz[v],out rfout))
					{
						oqcot[oz[v]]++;
					}
					else
					{
						oqcot[oz[v]]=1;
					}
					
				}
			}

			
		}

		static void oqALL()
		{
			oqcot=new Dictionary<string,int>();
			string[] fnaz=Directory.GetFiles(@"Q:\z\bookpdf\0bak\tu\ar\2\g","*.txt",SearchOption.TopDirectoryOnly);

			foreach(var fna in fnaz)
			{
				gpsrt(File.ReadAllLines(fna));
			}

			var sortedDict = from entry in oqcot orderby entry.Value ascending select entry;
			foreach(var ss in sortedDict)
			{
				if(ss.Value>5)
					Console.WriteLine(ss.Key+"\t"+ss.Value);
			}

		}
		
		static void klen()
		{
			var mzc=File.AppendText("3msc.txt");
			dzt["misc"]=mzc;
			dzt["misc-lo"]=mzc;
			dzt["non-h"]=mzc;
			dzt["non-h-lo"]=mzc;
			dzt["trash"]=File.AppendText("3trash.txt");
			dzt["manga"]=File.AppendText("2ma.txt");
			dzt["artist-cg"]=File.AppendText("2cg.txt");
			dzt["game-cg"]=File.AppendText("2gcg.txt");
			dzt["doujinshi"]=File.AppendText("2djs.txt");
			dzt["western"]=File.AppendText("2ws.txt");
			dzt["image-set"]=File.AppendText("2img.txt");
			
			dzt["manga-lo"]=File.AppendText("1ma.txt");
			dzt["artist-cg-lo"]=File.AppendText("1cg.txt");
			dzt["doujinshi-lo"]=File.AppendText("1djs.txt");
			dzt["game-cg-lo"]=File.AppendText("1gcg.txt");
			dzt["western-lo"]=File.AppendText("1ws.txt");
			dzt["image-set-lo"]=File.AppendText("1img.txt");
			
			
			
			
			
			string[] html=Directory.GetFiles(@"Q:\z\bookpdf\0bak\tu\ar\2","*.stop.txt",SearchOption.TopDirectoryOnly);
			if(html.Length==0){return;}
			
			string fna=html[0];
			html=File.ReadAllLines(fna);
			int hl=html.Length;
			for(int i=0;i<hl;i++)
			{
				todzt(html[i]);
				
			}

			foreach(var kp in dzt)
			{
				dzt[kp.Key].Close();
			}

			File.Move(fna,fna.Replace(".stop.",".byebye."));

			var zz =File.AppendText(fna);
			zz.Close();
		
		}

		const string gpir=@"Q:\z\bookpdf\0bak\tu\ar\2\g\";
		static void fkcimh()
		{
			string[] html=Directory.GetFiles(gpir,"*.*",SearchOption.AllDirectories);
			int hl=html.Length;
			for(int i=0;i<hl;i++)
			{
				html[i]=html[i].Replace(gpir,"'").Replace("\\","/");
			}
			html[hl-1]+="',\n";
			string  yz=string.Join("',\n",html);
			int huzm=1+(2048/hl);
			var fo=new StreamWriter("locimh.js");
			fo.WriteLine("var cimh=[");

			for(int i=0;i<huzm;i++)
			{
				fo.Write(yz);
			}
			fo.Write("];");
			fo.Close();

			

		}
		
		
		static void Main(string[] args)
		{
			var opk=new prmWRK();
		}
		
	}
}