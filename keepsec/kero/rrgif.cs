
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
			konren();
		}
		
	}
}