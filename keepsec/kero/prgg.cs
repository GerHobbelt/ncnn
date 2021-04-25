
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace eroneto
{
	
	static class utily
	{
	
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		public static char[] sep0x5d = { ']' };
		public static char[] sepDuo = { ',' };
		public static char[] sepMao = { ':' };
		
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
			izkor[0]="curEro= 000;\t\t//\t"+(cota/10)+"\n\nthumb=['";
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
			
			system("pause");
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
			
			system("pause");
		}
		
		static void dlbati()
		{
			
			ServicePointManager.Expect100Continue = true;                
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
			

			
			aco2(new WebClient());
			
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
		
		static void Main(string[] args)
		{
			dlbati();
			//mkjs();
		}
		
	}
}
