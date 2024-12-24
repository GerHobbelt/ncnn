using System;
using System.Windows.Forms;

namespace derpl
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			mf.mf_inst=new mf();
			Application.Run(mf.mf_inst);
		}
		
	}
}
