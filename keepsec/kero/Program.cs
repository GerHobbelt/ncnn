
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
		
		public static char[] sep0xA = { '\n' };
		public static char[] sepQuo = { '\"' };
		public static char[] sep0x9 = { '\t' };
		
		public static string[] vtwimg={
			"https://pbs.twimg.com/ext_tw_video_thumb/",	"https://pbs.twimg.com/amplify_video_thumb/",	"https://pbs.twimg.com/tweet_video_thumb/","https://pbs.twimg.com/media/"};
		public static string[] vtwvid={
			"https://video.twimg.com/ext_tw_video/",		"https://video.twimg.com/amplify_video/",		"https://video.twimg.com/tweet_video/"};
		public static string[] vtwsig=	{
			string.Empty,									"@",											"!",										"+"};
		
		
		
		static void klening()
		{
			string[] data = File.ReadAllLines("rlines.csv");
			File.Move("rlines.csv", "zb/rlines." + DateTimeOffset.Now.ToUnixTimeSeconds().ToString("X") + ".csv");
			
			int cota = data.Length - deuu.Count;
			
			List<string> kle = new List<string>();
			
			for (int i = 0; i < cota; i++) {
				var ivo = data[i].Split(sep0x9);
				if (!deuu.Contains(ivo[0])) {
					kle.Add(data[i]);
				}

			}
			
			int cota2 = data.Length;
			for (int i = cota; i < cota2; i++) {
				kle.Add(data[i]);
			}
			
			File.WriteAllLines("rlines.csv", kle.ToArray());
		}
		
		static string trimthumb(string thupic)
		{
			for(int i=0;i<4;i++)
			{
				thupic = thupic.Replace(vtwimg[i], vtwsig[i]);
				if (thupic[0] != 'h') {return thupic;}
			}
			return thupic;
		}
		
		static string trimvid(string vid)
		{
			for(int i=0;i<3;i++)
			{
				vid = vid.Replace(vtwvid[i], vtwsig[i]);
				if (vid[0] != 'h') {return vid;}
			}
			return vid;
		}
		
		static string[] dlpage(string url,char[] sep)
		{
			return dndr.DownloadString(url).Split(sep);
				
		}
		
		static void Main(string[] args)
		{
			ServicePointManager.Expect100Continue = true;                
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
			StreamWriter sw = File.AppendText("rlines.csv");
			while (true) {
			
				int getsome = 90000;
	
				
				
				try {
					string[] html = dndr.DownloadString("https://www.twidouga.net/realtime_t.php").Split(sep0xA);
															//https://www.twidouga.net/realtime_t.php
															//https://www.nurumayu.net/ko/twidouga/realtime_tzucks.php
					
					string[] html2 = dndr.DownloadString("https://www.twidouga.net/ko/realtime_t.php").Split(sep0xA);
															//https://www.twidouga.net/ko/realtime_t.php
															//https://www.nurumayu.net/ko/twidouga/realtime_t.php
				
					int htmllen = html.Length;
				
					for (int i = 0; i < htmllen; i++) {
						if (html[i].Length > 200 && html[i].Contains("video.twimg.com")) {
							string[] siu = html[i].Split(sepQuo);
						
							string thupic = trimthumb(siu[7]);
						
							
						
							if (!deuu.Contains(thupic)) {
								deuu.Add(thupic);
								string vid = trimvid(siu[3]);

								string msg = html[i + 2].Split(sepQuo)[1].Replace("https://twitter.com/", string.Empty).Replace("https://mobile.twitter.com/", string.Empty);
								sw.WriteLine(thupic + "\t" + vid + "\t" + msg);
							
								getsome -= 2500;
								
							}
						}
					}
				
					html=html2;
					htmllen = html.Length;
				
					for (int i = 0; i < htmllen; i++) {
						if (html[i].Length > 200 && html[i].Contains("video.twimg.com")) {
							string[] siu = html[i].Split(sepQuo);
						
							string thupic = trimthumb(siu[7]);
						
						
							if (!deuu.Contains(thupic)) {
								deuu.Add(thupic);
								string vid = trimvid(siu[3]);

								string msg = html[i + 2].Split(sepQuo)[1].Replace("https://twitter.com/", string.Empty).Replace("https://mobile.twitter.com/", string.Empty);
								sw.WriteLine(thupic + "\t" + vid + "\t" + msg);
							
								getsome -= 2500;
								
							}
						}
					}
					
				} catch {}
				
				
				try{
					string[] html = dlpage("https://twivideo.net/?realtime",sep0xA);
					int htmllen = html.Length;
				
					for (int i = 0; i < htmllen; i++) {
						if (html[i].Length > 150 && html[i].Contains("video.twimg.com")) {
							
							string thupic=trimthumb(html[i+1].Split(sepQuo)[1]);
							if (!deuu.Contains(thupic)) {
								deuu.Add(thupic);
								getsome -= 35;
								
								string vid=trimvid(html[i].Split(sepQuo)[1]);
								string msg = html[i + 5].Split(sepQuo)[3].Replace("https://twitter.com/", string.Empty).Replace("https://mobile.twitter.com/", string.Empty);
								sw.WriteLine(thupic + "\t" + vid + "\t" + msg);
								
								
							}
						}
						
					}
				
				}
				catch{}
				
				/*
				try{
					string[] html = dlpage("https://tk2dl.com/t/recent.html",sep0xA);
					int htmllen = html.Length;
				
					for (int i = 0; i < htmllen; i++) {
						
					}
					
				}
				catch{}
				*/
				
				if (getsome != 90000) {
						sw.Flush();
				
						
						if (deuu.Count > 1000) {
							break;
						}
						
						if(getsome<30000)
							getsome=30000;
					}
				
				Thread.Sleep(getsome);
			}
			
			sw.Close();			
			klening();			
			system("start eroneto.exe");
		}
		
	}
}
