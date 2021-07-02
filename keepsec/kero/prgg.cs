
using System;
using System.IO;
using System.Text;
using System.Linq;
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
		
		[DllImport("kernel32.dll")]
		public static extern uint GetTickCount();
		
		public static char[] sep0x5d = { ']' };
		public static char[] sepDuo = { ',' };
		public static char[] sepdot = { '.' };
		public static char[] sepMao = { ':' };
		public static char[] sepXie = { '/' };
		public static char[] sepTab = { '\t' };
		public static char[] sepQuo = { '\"' };
		public static char[] sepArwQuo = { '<','>','\''};
		const int sepjslen=50000;
		
		static string[] js50src;
		static int js50srccur=0;
		
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
				var ivo=js50src[js50srccur-i].Split(sepTab);
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
		const string bkero="bkero.html";
		static void mkjs50()
		{
			js50src=File.ReadAllLines("rlines.csv");
			js50srccur=js50src.Length-1;
			int ord=0;
			while(mkjs50pp(ord))
			{
				js50srccur-=sepjslen;
				ord++;
			}
			
			//js50src=File.ReadAllLines(bkero);
			//js50src[1]="var scril = "+(ord+1)+";";
			//File.WriteAllLines(bkero,js50src);
			                   
			
			
		}
		
		static void gzcomp(string fna)
		{
			byte[] data=File.ReadAllBytes(fna);
			MemoryStream output = new MemoryStream();
		    using (GZipStream dstream = new GZipStream(output,CompressionMode.Compress))
		    {
		        dstream.Write(data, 0, data.Length);
		    }
		    File.WriteAllBytes(fna+".gz",output.ToArray());
		    
		}
		
		
		static void findmymiss()
		{
			js50src=File.ReadAllLines("aadata.3.js");
			int fy=0;
			for(int i=0;i<50010;i++)
			{
				if(js50src[i].StartsWith("'!"))
				{
					fy++;
					
					if(fy>5)
					{
						Console.WriteLine(js50src[i]);
					}
				}
				else
				{
					fy=0;
				}
			}
			
			Console.ReadKey();
		}
		
		
		static string[][] ReadAllLines(string path)
		{
			var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			int fsl=(int)fs.Length;
			byte[] fb=new byte[fsl];
			fs.Read(fb,0,fsl);
			fs.Close();
			var data= Encoding.ASCII.GetString(fb).Replace("\r\n","\n").Split('\n');

			int cota=data.Length-1;
			string[][] reta=new string[cota][];
			
			for(int i=0;i<cota;i++)
			{
				 var ivo=data[i].Split('\t');
				
				 	
				 reta[i]=ivo;
				 
			}
			
			
			
			return reta;

		}
		
		static void mkjs()
		{
			//system("rd /s /q zb");
			//system("md zb");
			
			string[][] data=ReadAllLines("rlines.csv");
			int cota=data.Length-1;
			string[] izkor=new string[4+cota*3];
			string[] mizr=data[0];
			izkor[0]="\t\t//\t"+(cota/10)+"\n\nthumb=['";
			izkor[1+cota]=mizr[0]+"'];\n\nvids=['";
			izkor[2+cota*2]=mizr[1]+"'];\n\nmsgs=['";
			izkor[3+cota*3]=mizr[2]+"'];";
			
			for(int i=1;i<cota;i++)
			{
				mizr=data[cota-i];
				izkor[i]=mizr[0]+"',\n'";
				izkor[1+cota+i]=mizr[1]+"',\n'";
				izkor[2+i+cota*2]=mizr[2]+"',\n'";
			}
			
			File.WriteAllText("aadata.js",string.Concat(izkor));
		}
		
		static void aco1(WebClient dndr)
		{
			string[] acotz=File.ReadAllLines("aco.txt");
			
			int acolen=acotz.Length;
			
			for(int i=0;i<acolen;i++)
			{
				string html = dndr.DownloadString("https://lab.syncer.jp/api/13/"+acotz[i]);
				if(html.StartsWith("[200"))
				{
					string[] xap=html.Replace(",[[\"",",").Replace(",[",string.Empty).Replace("\"",string.Empty).Replace(@"https:\/\/pbs.twimg.com\/ext_tw_video_thumb\/",string.Empty).Replace(@"\/","/").Split(sep0x5d,StringSplitOptions.RemoveEmptyEntries);
					string[] xap2=xap[0].Split(sepDuo);
					xap[0]=xap2[1]+","+xap2[2];
					File.WriteAllLines(acotz[i]+".log",xap);
				}
				else
				{
					Console.WriteLine(acotz[i]+"====="+html);
				}
				
				Thread.Sleep(90000);
			}
			
			
		}
		
		
		static void aco2(WebClient dndr)
		{
			string[] acotz=File.ReadAllLines("ikon.csv");
			StreamWriter sw = File.AppendText("rlineskota.csv");
			
			StreamWriter origjson = File.AppendText("xxj.csv");
			
			int acolen=acotz.Length;
			
			for(int i=0;i<acolen;i++)
			{
				try{
					//
				string html = dndr.DownloadString("https://lab.syncer.jp/api/100/"+acotz[i]);
				if(html.StartsWith("[200"))
				{
					html=html.Replace(@"https:\/\/video.twimg.com\/ext_tw_video\/",string.Empty).Replace(@"https:\/\/pbs.twimg.com\/ext_tw_video_thumb\/",string.Empty).Replace(@"\/","/").Replace("https:",string.Empty).Replace("http:",string.Empty);
					origjson.WriteLine(html);
					string[] xap=html.Split(sepDuo);
					
					string tumb=xap[2].Replace("\"",string.Empty);
					string vid=xap[4].Split(sepMao)[1].Replace("\"",string.Empty);
					sw.WriteLine(tumb+"\t"+vid+"\t-"+acotz[i]);
					sw.Flush();
					
					
				}
				else
				{
					Console.WriteLine(acotz[i]+"====="+html);
				}
				
				
				}
				catch
					{
						Console.WriteLine("error==="+acotz[i]);
					}
					
					Thread.Sleep(90000);
			}
				
			sw.Close();
			origjson.Close();
			
			
		}
		
		static void chkmiss()
		{
			string[] acotz=File.ReadAllLines("ikon.csv");
			string bizfind=File.ReadAllText("rlines - 複製.csv");
			
			foreach(string st in acotz)
			{
				if(bizfind.IndexOf("/"+st)<0)
				{
					Console.WriteLine(st);
				}
			}
			system("pause");
		}
		static HashSet<string> deuu = new HashSet<string>();
		static StreamWriter swtwtk;
		
		static bool chknwrt(string src)
		{
			if (!deuu.Contains(src)) {
					deuu.Add(src);
					swtwtk.WriteLine(src);
					return true;}
			return false;
		}
		
		static void twtk(WebClient dndr)
		{
			
			string[] html=File.ReadAllLines("twtkold.csv");
			int nl=html.Length;
			
			for(int i=0;i<nl;i++)
			{
				deuu.Add(html[i]);
			}
			
			html=File.ReadAllLines("twtknu.csv");
			nl=html.Length;
			
			for(int i=0;i<nl;i++)
			{
				deuu.Add(html[i]);
			}
			
			
			swtwtk = File.AppendText("twtknu.csv");
			for(int pg=0;pg<315;pg++)
			{
				int gsome=0;
				
					
					try{
						html = dndr.DownloadString("https://tk2dl.com/t/recent.html?start="+((pg%12)*40)).Split(sepQuo);
					}catch{}
					
					nl=html.Length;
					
					for(int i=0;i<nl;i++)
					{
						if(html[i]==" value="&&html[i-1]=="x")
						{
							if(chknwrt(html[i+1]))
							{gsome++;}
							
							
							
						}
					}
					if(gsome!=0)
					{Console.WriteLine("tk2d"+pg+", "+gsome);}
					
				
				
				gsome=0;
				try{
						html = dndr.DownloadString("https://tw-dl.net/hozon.php?p="+pg).Split(sepQuo);
					}catch{}
				nl=html.Length;
				for(int i=0;i<nl;i++)
				{
					int hlll=html[i].Length;
					if(hlll>0x10&&hlll<0x24)
					{
						if(html[i].StartsWith("./v.php?video="))
						{
							
							if(chknwrt(html[i].Split('=')[1]))
							{gsome++;}
						}
					
					}
				}
				if(gsome!=0)
				{Console.WriteLine("hozon"+pg+", "+gsome);}
				else {Console.WriteLine("---nohozon"+pg);}
				swtwtk.Flush();
				Thread.Sleep(30000);
			}
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
		
		static void rrsrt(string fna,string ky="ky.txt")
		{
			fna+=".txt";
			Dictionary<ulong,List<string>> oq = new Dictionary<ulong,List<string>>();
			string[] kyy=File.ReadAllLines(ky);
			int kyl=kyy.Length;
			
			
			
			
			string[] stt=File.ReadAllLines("../"+fna);
			int sttl=stt.Length;
			bool endpause=false;
			
			
			string lztsig=string.Empty;
			for(int i=0;i<sttl;i++)
			{
				var oz=stt[i].Replace(" ",string.Empty).Split(sepDuo);
				var ozl=oz.Length;
				
				if(ozl[0]==lztsig)
				{
					Console.WriteLine(oz[2]);
					lztsig=oz[0];
					endpause=true;
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
						skor+=(1<<v);
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
			File.WriteAllText(fna,string.Join("\n",rzt));
		
		}
		
		static string[] imgextt={".jpg",".png",".gif"};
		
		static void imhan_all(WebClient dndr)
		{
			
			string[] html=Directory.GetFiles(@"Q:\z\bookpdf\0bak\tu\ar\2","*.stop.txt",SearchOption.TopDirectoryOnly);
			if(html.Length==0){
				fkcimh();
				return;}
				
			dndr.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			
			int sta=int.Parse(html[0].Split(sepdot)[1]);
			
			system("klen.bat "+(sta-1));
			
			string cfn="0dta."+sta+".run.txt";
			File.Move(html[0],cfn);
			swtwtk = File.AppendText(cfn);
			
			
			
			
			
			
			for(int i=sta;i<688500;i+=512)
			{
				uint timesta=GetTickCount();
				try{
				html = dndr.DownloadString("https://imhentai.xxx/gallery/"+i+"/").Split(sepArwQuo);
				}catch{}
				int nl=html.Length;
				int pgnum=0;
				List<string> prd=new List<string>();
				string typi=string.Empty;
				string sig=string.Empty;
				for(int vv=0;vv<nl;vv++)
				{
					string nas=html[vv];
					if(nas=="li class=\"pages\"")
					{
						pgnum=int.Parse(html[vv+1].Substring(7));
					}
					else if(nas.StartsWith("/parody/")||nas.StartsWith("/tag/")||nas.StartsWith("/group/"))
					{
						prd.Add(nas.Split(sepXie)[2]);
					}
					else if(nas.StartsWith("/category/"))
					{
						typi = nas.Split(sepXie)[2];
					}
					else if(nas.EndsWith("/1t.jpg\" /"))
					{
						string[] hoo=nas.Split(sepQuo);
						sig=hoo[hoo.Length-2].Substring(9).Replace("/1t.jpg",string.Empty);
						break;
					}
				
				}
				
				
				int tstdown=pgnum/2;
				int next=0;
			zapi:
					try{
						dndr.DownloadFile("https://m"+sig+"/"+tstdown+imgextt[next],"g\\"+sta+"\\"+sig.Substring(sig.Length-11)+imgextt[next]);
				
					}
					catch{
					if(next<2)
					{
						next++;
						goto zapi;
					}
					}
				
				pgnum=1+(pgnum/5);
				switch(next)
				{
					case 1:
						sig=sig.Replace(".imhentai.xxx/","@p"+pgnum+"@");
						break;
					case 2:
						sig=sig.Replace(".imhentai.xxx/","@g"+pgnum+"@");
						break;
					default:
						sig=sig.Replace(".imhentai.xxx/","@j"+pgnum+"@");
						break;
				}
				
				swtwtk.WriteLine("'"+sig+"',\t//,"+i+", "+typi+", "+string.Join(", ",prd));
				swtwtk.Flush();
				
				
				timesta=GetTickCount()-timesta;
				int slp=5000-(int)timesta;
				if(slp>0)
				{
					Thread.Sleep(slp);
				}
				
				
				
			}
			
			sta+=1;
			swtwtk.Close();
			File.Move(cfn,"0dta."+sta+".stop.txt");
				
		}
		
		const string tubsig="/thumb.jpg";
		static void imhan_anima(WebClient dndr)
		{
			swtwtk = File.AppendText("imhenta_anim.txt");
			string[] html=null;
			for(int pg=1;pg<420;pg++)
			{
				try{
						html = dndr.DownloadString("https://imhentai.xxx/tag/animated/?page="+pg).Split(sepQuo);
				}catch{}
				int nl=html.Length;
					
					for(int i=0;i<nl;i++)
					{
						string gei=html[i];
						if(gei.Length>40&&gei.EndsWith(tubsig))
						{
							
							swtwtk.WriteLine(gei.Substring(8,gei.Length-18));
							
							
						}
					}
				
					swtwtk.Flush();
				Thread.Sleep(10000);
			}
				
				//420
		
		}
		
		static void dlbati()
		{
			
			ServicePointManager.Expect100Continue = true;                
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
			

			//client.Encoding = Encoding.UTF8; 
			imhan_all(new WebClient());
			//system("pause");
			
		}
		const string schpa=@"Q:\z\bookpdf\0bak\tu\ar\";
		const string jsh123="',\n100,50,350,350,0.5,'";
		const string jsh0="',\n\n100,50,350,350,0.5,'";
		
		const string tu1=@"Q:\z\bookpdf\0bak\tu\";
		
		static void prtmv()
		{
			string[] html=File.ReadAllLines("oz.txt");
			int nl=html.Length;
			
			for(int i=0;i<nl;i++)
			{
				deuu.Add(html[i]);
			}
			html=Directory.GetFiles(tu1,"*.gif",SearchOption.TopDirectoryOnly);
			nl=html.Length;
			
			for(int i=0;i<nl;i++)
			{
				string fsig=html[i].Replace(tu1,string.Empty).Replace(".gif",string.Empty);
				if(!deuu.Contains(fsig))
				{
					Console.WriteLine("move "+fsig+".gif ar\\");
				}
			}
			
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
		
		}
		
		static void mkplaceho()
		{
			string[] giflist=Directory.GetFiles(schpa,"*.gif",SearchOption.TopDirectoryOnly);
			int giflistl=giflist.Length;
			
			int nonXX=0;
			
			for(int i=0;i<giflistl;i++)
			{
				
				string gsho=giflist[i].Replace(schpa,string.Empty).Replace(".gif",string.Empty);
				
				if(gsho.StartsWith("xx_"))
				{
					giflist[i]=string.Empty;
					
					if(!File.Exists(gsho+".gif")){continue;}
					
					string dstgsho=gsho.Replace("xx_",string.Empty);
					
				lpsa:
					
					if(File.Exists("../xx/_"+dstgsho+".gif"))
					{
						
						system("mvx.bat "+dstgsho+".gif "+gsho);
						/*
move ../xx/_%1 ../xx/byby/%1
move %2.gif sele/_%1
						 */
					}
					else if(File.Exists(dstgsho+".gif"))
					{
						system("mvx2.bat "+dstgsho+".gif "+gsho);
												/*
move sele/_%1 sele/bb/%1
move %2.gif sele/_%1
						 */
					}
					else
					{
						Console.WriteLine("Xno dst: "+dstgsho);
						dstgsho=Console.ReadLine();
						goto lpsa;
					}
				
				}
				else
				{
					string jsh=jsh123;
					if((nonXX&3)==0){jsh=jsh0;}
					
					giflist[i]=jsh+gsho;
					
					string replsbo="xx_"+gsho+".gif";
					if(File.Exists(replsbo))
					{
						File.Move(replsbo,"sele/_"+gsho+".gif");
					}
					else{
						system(@"mklink /H sele\_"+gsho+".gif "+gsho+".gif");
					}
					
					
					nonXX++;
				
				}
				
				
				
				
				
				
				
				
				
				
			}
			Console.Write("\n\n\n\n\n");
			for(int i=0;i<giflistl;i++)
			{
				Console.Write(giflist[i]);
			}
			Console.Write("',");
			
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
		
		}
		
		
		
		/*
		static WebClient glbdl;
		const string jprog="../bk40792.html";
		static void buukdl()
		{
			ServicePointManager.Expect100Continue = true;                
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
			
			glbdl=new WebClient();
			glbdl.Encoding = Encoding.UTF8;
			
			while(true)
			{
				Console.WriteLine("Enter Url:");
				string ul=Console.ReadLine();
				string[] ty=ul.Split(sepXie);
				int pglen=int.Parse(ty[ty.Length-1].Replace(".html",string.Empty));
				ty[ty.Length-1]=string.Empty;
				ul=string.Join("/",ty);
				
				system("del *.html");
				
				for(int i=0;i<pglen;i++)
				{
					string fna=(i+1).ToString()+".html";
					try{
					string konten=glbdl.DownloadString(ul+fna).Replace("https://c",string.Empty);
					File.WriteAllText(fna,konten);
					Thread.Sleep(500);
					}
					catch{}
				}
				ty=File.ReadAllLines(jprog);
				ty[2]="var pgcot= "+pglen+";";
				File.WriteAllLines(jprog,ty);
				system("gofalkon.bat");
			}
			
			
		}
		
		static void cleandup()
		{
			 HashSet<string> deuuk = new HashSet<string>();
			 string[] data=File.ReadAllLines("rlines.csv");
			 int cota=data.Length;
			 List<string> kle=new List<string>();
			for(int i=cota-1;i>=0;i--)
			{
				var ivo=data[i].Split('\t');
				if(!deuuk.Contains(ivo[0]))
				{
					kle.Add(data[i]);
					deuuk.Add(ivo[0]);
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
		*/
		
		const string rensrc=@"Q:\z\bookpdf\0bak\tu\xx\rp\";
		
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
		
		
		static void Main(string[] args)
		{
			//mkplaceho();
			dlbati();
		}
		
	}
}
