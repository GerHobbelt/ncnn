
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
			system("rd /s /q zb");
			system("md zb");
			
			string[][] data=ReadAllLines("rlines.csv");
			int cota=data.Length-1;
			string[] izkor=new string[4+cota*3];
			string[] mizr=data[0];
			izkor[0]="var curEro=0;\t\t//\t"+(cota/10)+"\n\nthumb=['";
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
		
		static void cleandup()
		{
			 HashSet<string> deuuk = new HashSet<string>();
			 string[] data=File.ReadAllLines("rlines.csv");
			 int cota=data.Length;
			 List<string> kle=new List<string>();
			for(int i=0;i<cota;i++)
			{
				var ivo=data[i].Split('\t');
				if(!deuuk.Contains(ivo[0]))
				{
					kle.Add(data[i]);
					deuuk.Add(ivo[0]);
				}

			}
			
			File.WriteAllLines("rlines_.csv",kle.ToArray());
		}
		
		static void Main(string[] args)
		{
			//cleandup();
			mkjs();
		}
		
	}
}
