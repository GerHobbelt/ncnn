
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using worker;

namespace derpl
{

	public class mf : Form
	{
		public static RichTextBox richTextBox1= new RichTextBox();
		public static Label  button1 = new Label();
		public static Label  button2 = new Label();
		public static Label  button3 = new Label();
		public static mf mf_inst;
		public static EventHandler evBtnX=new EventHandler(Work.BtnX);
		public static EventHandler evBtnSV=new EventHandler(Work.BtnSV);
		public static EventHandler evBtnVVVV=new EventHandler(Work.BtnVVVV);
		

		
		public void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// richTextBox1
			//
			//this.richTextBox1.Multiline =true;
			richTextBox1.Location = new Point(0, 19);
			richTextBox1.Size = new Size(300, 976);
			richTextBox1.Font = new Font("MingLiU Regular", 12);  

			richTextBox1.TabIndex = 0;
			Work.txtboxload();
			// 
			// button1
			// 

button1.BorderStyle = BorderStyle.FixedSingle;
button2.BorderStyle = BorderStyle.FixedSingle;
button3.BorderStyle = BorderStyle.FixedSingle;
button2.Cursor = Cursors.Hand;	//new Cursor("Caplat/wa.cur"); //
button3.Cursor = Cursors.NoMoveVert;
button1.Cursor = Cursors.No;
			button1.Location = new Point(274, 0);
			button1.Size = new Size(16, 16);
			button1.TabIndex = 1;
			button1.Text = "X";
			button1.Click += evBtnX;
			// 
			// button2
			// 
			button2.Location = new Point(0, 0);
			button2.Size = new Size(48, 16);
			button2.TabIndex = 2;
			button2.Text = "SAVE";
			button2.Click += evBtnSV;
			// 
			// button3
			// 
			button3.Location = new Point(116, 0);
			button3.Size = new Size(48, 16);
			button3.TabIndex = 3;
			button3.Text = "VVVV";
			button3.Click += evBtnVVVV;
			// 
			// MainForm
			// 
			this.ClientSize = new Size(300, 1000);
			this.Controls.Add(button3);
			this.Controls.Add(button2);
			this.Controls.Add(button1);
			this.Controls.Add(richTextBox1);
			this.FormBorderStyle = FormBorderStyle.None;
			this.Location = new Point(1300, 40);
			
			this.StartPosition = FormStartPosition.Manual;

			this.TopMost = true;
			this.ResumeLayout(false);

		}
		
		public mf(){InitializeComponent();Work.EnableContextMenu();}
		

		
	}
}
