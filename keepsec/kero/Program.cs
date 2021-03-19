
using System;
using System.IO;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace eroneto
{
	
	static class Program
	{
		static HashSet<string> deuu = new HashSet<string>();
		static WebClient dndr = new WebClient();
		
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		public static char[] sep0xA={'\n'};
		public static char[] sepQuo={'\"'};
		public static char[] sep0x9={'\t'};
		
		static void klening()
		{
			string[] data=File.ReadAllLines("rlines.csv");
			File.Move("rlines.csv","zb/rlines."+DateTimeOffset.Now.ToUnixTimeSeconds().ToString("X")+".csv");
			
			int cota=data.Length-deuu.Count;
			
			List<string> kle=new List<string>();
			
			for(int i=0;i<cota;i++)
			{
				var ivo=data[i].Split(sep0x9);
				if(!deuu.Contains(ivo[0]))
				{
					kle.Add(data[i]);
				}

			}
			
			int cota2=data.Length;
			for(int i=cota;i<cota2;i++)
			{
					kle.Add(data[i]);
			}
			
			File.WriteAllLines("rlines.csv",kle.ToArray());
		}
		
		static void Main(string[] args)
		{
			ServicePointManager.Expect100Continue = true;                
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			StreamWriter sw = File.AppendText("rlines.csv");
			while(true)
			{
			
				string[] html=dndr.DownloadString("https://www.nurumayu.net/ko/twidouga/realtime_t.php").Split(sep0xA);
				
				int htmllen=html.Length;
				bool getsome=false;
				for(int i=0;i<htmllen;i++)
				{
					if(html[i].Length>200&&html[i].Contains("video.twimg.com"))
					{
						string[] siu=html[i].Split(sepQuo);
						
						string thupic=siu[7].Replace("http://pbs.twimg.com/ext_tw_video_thumb/",string.Empty);
						
						if(thupic[0]=='h')
						{
							thupic=thupic.Replace("http://pbs.twimg.com/amplify_video_thumb/","@");
						}
						
						if(thupic[0]=='h')
						{
							thupic=thupic.Replace("http://pbs.twimg.com/tweet_video_thumb/","!");
						}
						
						if(!deuu.Contains(thupic))
						{
							deuu.Add(thupic);
							string vid=siu[3].Replace("https://video.twimg.com/ext_tw_video/",string.Empty);
							
							if(vid[0]=='h')
							{
								vid=vid.Replace("https://video.twimg.com/amplify_video/","@");
							}
							
							if(vid[0]=='h')
							{
								vid=vid.Replace("https://video.twimg.com/tweet_video/","!");
							}
							
							
							string msg=html[i+2].Split(sepQuo)[1].Replace("https://twitter.com/",string.Empty).Replace("https://mobile.twitter.com/",string.Empty);
							sw.WriteLine(thupic+"\t"+vid+"\t"+msg);
							
							getsome=true;
						}
					}
				}
				
				if(getsome)
				{
					sw.Flush();
				
					if(deuu.Count>1000)
					{
						
						sw.Close();
						
						klening();
						
						system("start eroneto.exe");
						break;
						
					}
				}
				
				Thread.Sleep(90000);
			}
		}
		
	}
}
