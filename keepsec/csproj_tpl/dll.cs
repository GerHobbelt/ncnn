using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using derpl;

namespace worker
{
	public class Work
	{
		
		public static Size fullexp = new Size(300, 1000);
		public static bool isdrp=true;
		public static Size srked = new Size(300, 16);

		public static ContextMenuStrip cms = new ContextMenuStrip();
		
		public static ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Copy");
		
		public static EventHandler evtsmif_copy=new EventHandler(tsmif_copy);
		public static CancelEventHandler evtsmif_opening =  new CancelEventHandler(tsmif_opening);
		
		public static void tsmif_copy(object sender, EventArgs e)
		{
			mf.richTextBox1.Copy();
			BtnVVVV(sender, e);
		}
		
		public static void tsmif_opening(object sender, CancelEventArgs e)
		{
			tsmiCopy.Enabled = (mf.richTextBox1.SelectionLength > 0);
		}
		
		public static void EnableContextMenu()
		{
			
			RichTextBox rtb=mf.richTextBox1;
			if (rtb.ContextMenuStrip == null)
			{
	           
	            cms.ShowImageMargin = false;
	
	           
	            
	            tsmiCopy.Click += evtsmif_copy;
	            cms.Items.Add(tsmiCopy);
	
	           
	
	            cms.Opening += evtsmif_opening;
	
	            rtb.ContextMenuStrip = cms;
	        }
			
		}

		
		
		public static string txtboxload(){
			mf.richTextBox1.LoadFile("xcancel.twcursor.txt", RichTextBoxStreamType.PlainText);
			return null;//File.ReadAllText("xcancel.twcursor.txt"); 
		}
		
		public static void BtnX(object sender, EventArgs e){Application.Exit();}

		
		public static void BtnVVVV(object sender, EventArgs e)
		{
			if(isdrp){
				mf.mf_inst.ClientSize=srked;
				isdrp=false;
			} else {
				mf.mf_inst.ClientSize=fullexp;
				isdrp=true;
			}
			
		}
	
		
		public static void BtnSV(object sender, EventArgs e){File.WriteAllText("xcancel.twcursor2.txt",mf.richTextBox1.Text);}
	}
}