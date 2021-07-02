
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace eroneto
{
	
	static class utily
	{
	
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		
		public static char[] sepTab = { '\t' };
		
		public static HashSet<string> deuu = new HashSet<string>();
		
		const int sepjslen=50000;
		const string fapai=@"D:\TDDOWNLOAD\1223\apdsa\gfsoz\rlines.";
		
		
		static void cleandup(string[] data)
		{
			 
			int cota=data.Length;
			List<string> kle=new List<string>();
			for(int i=cota-1;i>=0;i--)
			{
				var ivo=data[i].Split(sepTab);
				if(!deuu.Contains(ivo[0]))
				{
					kle.Add(data[i]);
					deuu.Add(ivo[0]);
				}

			}
			
			cota=kle.Count;
			data=new string[cota];
			int k=cota-1;
			foreach(string ka in kle)
			{
				data[k]=ka;
				k--;
			}
			
			
			
			File.WriteAllLines("rlines++.csv",data);
		}
		
		static string[][] js50src;
		static int js50srccur;
		
		static void fixlazt(string[] arr)
		{
			int laz=arr.Length-1;
			arr[laz]=arr[laz].Replace(",\n'","];");
		}
		
		static bool mkjs50pp(int ord)
		{
			bool islast=false;
			if(js50srccur<sepjslen){islast=true;}
			
			int sta=0;
			
			if(islast)
			{
				sta=js50srccur+1;
			}
			else
			{
				sta=sepjslen;
			}
			
			string[] thumbz=new string[sta];
			string[] vidz=new string[sta];
			string[] msgz=new string[sta];
			
			for(int i=0;i<sta;i++)
			{
				var ivo=js50src[js50srccur-i];
				thumbz[i]=ivo[0]+"',\n'";;
				vidz[i]=ivo[1]+"',\n'";;
				msgz[i]=ivo[2]+"',\n'";;
				
			}
			fixlazt(thumbz);
			fixlazt(vidz);
			fixlazt(msgz);
			
			string stfazt="thumb=['";
			if(islast){stfazt="\t\t//\t"+(sta/10)+"\n\nthumb=['";}
			
			StreamWriter sw = File.AppendText("aadata."+ord+".js");
			
			sw.Write(stfazt);
			foreach(var st in thumbz)
			{
				sw.Write(st);
			}
			sw.Write("\n\nvids=['");
			foreach(var st in vidz)
			{
				sw.Write(st);
			}
			sw.Write("\n\nmsgs=['");
			foreach(var st in msgz)
			{
				sw.Write(st);
			}
			sw.Close();
			return !islast;
		}
		
		static void prjs()
		{
			
			
			string[] tmpstarr=File.ReadAllLines("rlines.csv");
			int lyncot=tmpstarr.Length;
			
			string tmpfana=fapai+"0";
			if(File.Exists(tmpfana))
			{
				js50src=new string[12][];
				js50src[0]=tmpstarr;
				int fiidx=1;
				
				
				
				tmpstarr=File.ReadAllLines(tmpfana);
				lyncot+=tmpstarr.Length;
				js50src[fiidx]=tmpstarr;
				fiidx++;
				
				for(int i=1;i<10;i++)
				{
					tmpfana=fapai+i;
					if(File.Exists(tmpfana))
					{
						tmpstarr=File.ReadAllLines(tmpfana);
						lyncot+=tmpstarr.Length;
						js50src[fiidx]=tmpstarr;
						fiidx++;
					} else {break;}
				
				}
				
				
				tmpfana=fapai+"+";
				if(File.Exists(tmpfana))
				{
					
					tmpstarr=File.ReadAllLines(tmpfana);
					lyncot+=tmpstarr.Length;
					js50src[fiidx]=tmpstarr;
					fiidx++;
				}
				
				List<string> gaba=new List<string>();	//!!!!!!
				
				string[][] tpr2=new string[lyncot][];
				
				tmpstarr=js50src[0];
				int ct2=0;
				
				/*
				HashSet<string> deuuk = new HashSet<string>();
				bool do1000=true;
				*/
				
				
				
				
				
				for(int i=tmpstarr.Length-1;i>=0;i--)
				{
					var ivo=tmpstarr[i].Split(sepTab);
					if(!deuu.Contains(ivo[0]))
					{
						
						
							deuu.Add(ivo[0]);
							lyncot--;
							tpr2[lyncot]=ivo;
							
							/*
							if(do1000)
							{
								deuuk.Add(ivo[0]);
								ct2++;
								if(ct2>999){do1000=false;}
							}
							*/
						
					}
					else {gaba.Add(tmpstarr[i]+"\tBS");}	//!!!!!!
					

				}
				/*
				deuuk.Clear();
				deuuk=null;
				*/
				
				for(int vfp=1;vfp<fiidx;vfp++)
				{
					tmpstarr=js50src[vfp];
					ct2=tmpstarr.Length;
					for(int i=0;i<ct2;i++)
					{
						var ivo=tmpstarr[i].Split(sepTab);
						if(!deuu.Contains(ivo[0]))
						{
							lyncot--;
							tpr2[lyncot]=ivo;
						}
						else {gaba.Add(tmpstarr[i]+"\tv"+vfp);}	//!!!!!!
						
					}
					js50src[vfp]=null;
				
				}
				
				
				
				ct2=((tpr2.Length-lyncot)/10000)-8;
				
				if(ct2%5==0){ct2--;}
				
				ct2*=10000;
				js50src=new string[ct2][];
				Array.Copy(tpr2,lyncot,js50src,0,ct2);
				ct2=lyncot+ct2;
				int forcsvlen=tpr2.Length-ct2;
				tmpstarr=new string[forcsvlen];
				
				for(int i=0;i<forcsvlen;i++)
				{
					var ivo=tpr2[ct2];
					tmpstarr[i]=ivo[0]+"\t"+ivo[1]+"\t"+ivo[2];
					ct2++;
				}
				File.WriteAllLines("rlines++.csv",tmpstarr);
				
				
				js50srccur=js50src.Length-1;
				ct2=0;
				while(mkjs50pp(ct2))
				{
					js50srccur-=sepjslen;
					ct2++;
				}
				
				File.WriteAllLines("++gaba++.csv",gaba.ToArray());	//!!!!!!
				
				
			}
			else
			{
				cleandup(tmpstarr);
			}
			
			
		
		}
		
		
		static void Main(string[] args)
		{
			prjs();
		}
		
	}
}
