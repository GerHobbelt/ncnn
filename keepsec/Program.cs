
using System;
using System.IO;
using KT;

namespace G1conso
{
	class Program
	{
		public static G1M gg;
		
		public static void Main(string[] args)
		{
			G1pkg.guessing("tt.bin");
			gg = G1pkg.m_list[0];
			
			var lkk = gg.iG1MG.objB;
			foreach(var bb in lkk)
			{
				File.WriteAllText("toto"+bb.ord+".js",bb.ToCSV(true));
				File.WriteAllText("tobw"+bb.ord+".js",bb.RealBlendMappingCSV(true));
			}
			
		}
	}
}