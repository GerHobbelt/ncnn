
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace eroneto
{
	
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
			if(inin.Contains("guro")||inin.Contains("furry")||inin.Contains("yaoi")||inin.Contains("males-only"))
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

		static void rrsrt(string fna,string ky="ky.txt")
		{
			fna+=".txt";
			Dictionary<ulong,List<string>> oq = new Dictionary<ulong,List<string>>();
			string[] kyy=File.ReadAllLines(ky);
			int kyl=kyy.Length;
			
			
			
			
			string[] stt=File.ReadAllLines("../"+fna);
			int sttl=stt.Length;
			int endpause=0;
			
			
			string lztsig=string.Empty;
			for(int i=0;i<sttl;i++)
			{
				var oz=stt[i].Replace(" ",string.Empty).Split(sepDuo);
				var ozl=oz.Length;
				
				if(oz[0]==lztsig)
				{
					Console.WriteLine("imhentai.xxx/gallery/"+oz[2]+"/");
					
					lztsig=oz[0];
					endpause++;
					continue;
				}
				lztsig=oz[0];
				
				
				
				for(int v=0;v<3;v++)
				{
					oz[v]=string.Empty;
					
				}
				string tztstr=string.Join(",",oz);
				
				ulong skor=0;
				
				for(int v=0;v<kyl;v++)
				{
					if(tztstr.Contains(kyy[v]))
					{
						skor+=((ulong)1<<v);
					}
					
				}
				
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
				rzt.Add(string.Join("\n",ss));
			}
			rzt.Add(string.Empty);
			File.WriteAllText(fna,string.Join("\n",rzt));

			if(endpause>0){
				Console.WriteLine("dup: "+endpause);
				Console.ReadKey();
				
				Console.ReadKey();}
		
		}

		static void gpsrt(string fna)
		{
			string[] stt=File.ReadAllLines(fna);
			int sttl=stt.Length;
			
			Dictionary<string,int> oqcot=new Dictionary<string,int>();
			
			for(int i=0;i<sttl;i++)
			{
				var oz=stt[i].Replace(" ",string.Empty).Split(sepDuo);
				var ozl=oz.Length;
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

			var sortedDict = from entry in oqcot orderby entry.Value ascending select entry;
			foreach(var ss in sortedDict)
			{
				if(ss.Value>5)
					Console.WriteLine(ss.Key+"\t"+ss.Value);
			}
		}
		
		static void klen()
		{
			dzt["misc"]=File.AppendText("3msc.txt");
			dzt["misc-lo"]=dzt["misc"];
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
			if(args.Length>1)
			{
				rrsrt(args[0],args[1]);
			}
			else{rrsrt(args[0]);}
		}
		
	}
}