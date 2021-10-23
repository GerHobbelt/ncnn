
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace trnzdsk
{

	
	
	static class utily
	{
	
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
		public static extern int system(string command);
		
		const string rensrc = @"Q:\z\bookpdf\0bak\tu\xx\rp\";
		
		public static char[] sep0x9 = { '\t' };
		public static char[] sepQuo_s = { '\'' };
		public static char[] sepDuo = { ',' };
		public static char[] sep0x20 = { '@' };
		
		enum colsig
		{
RecordOffset = 0,
Signature,
IntegrityCheck,
Style,
HEADER_MFTREcordNumber,
HEADER_SequenceNo,
Header_HardLinkCount,
FN_ParentReferenceNo,
FN_ParentSequenceNo,
FN_FileName,
FilePath,
HEADER_Flags,
RecordActive,
FileSizeBytes,
SI_FilePermission,
FN_Flags,
FN_NameType,
ADS,
SI_CTime,
SI_ATime,
SI_MTime,
SI_RTime,
MSecTest,
FN_CTime,
FN_ATime,
FN_MTime,
FN_RTime,
CTimeTest,
FN_AllocSize,
FN_RealSize,
FN_EaSize,
SI_USN,
DATA_Name,
DATA_Flags,
DATA_LengthOfAttribute,
DATA_IndexedFlag,
DATA_VCNs,
DATA_NonResidentFlag,
DATA_CompressionUnitSize,
HEADER_LSN,
HEADER_RecordRealSize,
HEADER_RecordAllocSize,
HEADER_BaseRecord,
HEADER_BaseRecSeqNo,
HEADER_NextAttribID,
DATA_AllocatedSize,
DATA_RealSize,
DATA_InitializedStreamSize,
SI_HEADER_Flags,
SI_MaxVersions,
SI_VersionNumber,
SI_ClassID,
SI_OwnerID,
SI_SecurityID,
SI_Quota,
FN_CTime_2,
FN_ATime_2,
FN_MTime_2,
FN_RTime_2,
FN_AllocSize_2,
FN_RealSize_2,
FN_EaSize_2,
FN_Flags_2,
FN_NameLength_2,
FN_NameType_2,
FN_FileName_2,
GUID_ObjectID,
GUID_BirthVolumeID,
GUID_BirthObjectID,
GUID_DomainID,
VOLUME_NAME_NAME,
VOL_INFO_NTFS_VERSION,
VOL_INFO_FLAGS,
FN_CTime_3,
FN_ATime_3,
FN_MTime_3,
FN_RTime_3,
FN_AllocSize_3,
FN_RealSize_3,
FN_EaSize_3,
FN_Flags_3,
FN_NameLength_3,
FN_NameType_3,
FN_FileName_3,
DATA_Name_2,
DATA_NonResidentFlag_2,
DATA_Flags_2,
DATA_LengthOfAttribute_2,
DATA_IndexedFlag_2,
DATA_StartVCN_2,
DATA_LastVCN_2,
DATA_VCNs_2,
DATA_CompressionUnitSize_2,
DATA_AllocatedSize_2,
DATA_RealSize_2,
DATA_InitializedStreamSize_2,
DATA_Name_3,
DATA_NonResidentFlag_3,
DATA_Flags_3,
DATA_LengthOfAttribute_3,
DATA_IndexedFlag_3,
DATA_StartVCN_3,
DATA_LastVCN_3,
DATA_VCNs_3,
DATA_CompressionUnitSize_3,
DATA_AllocatedSize_3,
DATA_RealSize_3,
DATA_InitializedStreamSize_3,
STANDARD_INFORMATION_ON,
ATTRIBUTE_LIST_ON,
FILE_NAME_ON,
OBJECT_ID_ON,
SECURITY_DESCRIPTOR_ON,
VOLUME_NAME_ON,
VOLUME_INFORMATION_ON,
DATA_ON,
INDEX_ROOT_ON,
INDEX_ALLOCATION_ON,
BITMAP_ON,
REPARSE_POINT_ON,
EA_INFORMATION_ON,
EA_ON,
PROPERTY_SET_ON,
LOGGED_UTILITY_STREAM_ON,
DT_DataRun
		}
		
		
		
		
		static string clcmd5(string filePath)
		{
			// Not sure if BufferedStream should be wrapped in using block
			using(var stream = new BufferedStream(File.OpenRead(filePath), 0x120000))
			{
			    // The rest remains the same
			}
			
			return "123";
		}
		
		static void drdup(string fiho)
		{
			string[] kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\ar\2\"+fiho+".txt");
			int konl=kon.Length;
			HashSet<string> deuuk = new HashSet<string>();
			
			for(int i=0;i<konl;i++)
			{
					if(kon[i].Length<5)
					{
						continue;
					}
					
					
					string[] ozb=kon[i].Split(sep0x9);
					if(deuuk.Contains(ozb[0]))
					{
						kon[i]=string.Empty;
					}
					else
					{
						deuuk.Add(ozb[0]);
					}
			}
			
			File.WriteAllLines(fiho+".txt",kon);
		}
		
		static void klln()
		{
			HashSet<string> deuuk = new HashSet<string>();
			string[] kon=File.ReadAllLines(@"klln.txt");
			int konl=kon.Length;
			for(int i=0;i<konl;i++)
			{
				deuuk.Add(kon[i]);
			}
			
			kon=File.ReadAllLines(@"twtknu.csv");
			konl=kon.Length;
			
			for(int i=0;i<konl;i+=2)
			{
				if(deuuk.Contains(kon[i]))
				{
					kon[i]=string.Empty;
					kon[i+1]=string.Empty;
				}
			}
			
			File.WriteAllLines("twtknu.csv+",kon);
			
		}
		static void myjpg()
		{
			string[] jlist=Directory.GetFiles(@"Q:\z\bookpdf\0bak\tu\xj\jr\t5akx\o","*.png",SearchOption.TopDirectoryOnly);
			
			foreach(var jj in jlist)
			{
				byte[] bf=File.ReadAllBytes(jj);
				if((bf[0xd]==1)&&(bf[0xf]==0x48)&&(bf[0x11]==0x48))
				{
					bf[0xd]=2;
					bf[0xf]=0x26;
					bf[0x11]=0x26;
					File.WriteAllBytes(jj,bf);
				}
				else
				{
					Console.WriteLine(jj);
				}
			}
		}
		
		static void mkdbsjs()
		{
			HashSet<string> leold = new HashSet<string>();
			string[] kon=File.ReadAllLines(@"twtkold.csv");
			int konl=kon.Length;
			for(int i=0;i<konl;i+=2)
			{
				leold.Add(kon[i]);
			}
			List<string> istu=new List<string>();
			if(File.Exists("istatus.txt"))
			{
				kon=File.ReadAllLines(@"istatus.txt");
				konl=kon.Length;
				//HashSet<string> leistu = new HashSet<string>();
				
				for(int i=0;i<konl;i++)
				{
					var sty=kon[i];
					ulong xx;
					if(ulong.TryParse(sty, out xx))
					{
						if(!leold.Contains(sty))
						{
							leold.Add(sty);
							//leistu.Add(sty);
							istu.Add(sty);
						}
					}
					
				}
				istu.Add(string.Empty);
				File.WriteAllText("istatus.txt",string.Join("\n-\n",istu));
			}
			kon=File.ReadAllLines(@"twtknu.csv");
			konl=kon.Length;
			istu=new List<string>();
			istu.Add("window.twilist=[\n");
			for(int i=0;i<konl;i+=2)
			{
				var sty=kon[i];
				if(leold.Contains(sty))
				{
					kon[i]=string.Empty;
					kon[i+1]=string.Empty;
					
				}
				else
				{
					istu.Add("'"+kon[i]+"','"+kon[i+1]+"',\n");
				}
			}
			istu.Add("];");
			File.WriteAllLines("twtknu.csv+",kon);
			File.WriteAllText("dbs.js",string.Join(string.Empty,istu));
			
		}
		static void cleantwtknu2()
		{
			HashSet<string> bythumb = new HashSet<string>();
			string[] kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\twtkold_v.csv");
			int konl=kon.Length;
			for(int i=0;i<konl;i+=2)
			{
				bythumb.Add(kon[i]);
			}
			kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\twtknu_v.csv");
			konl=kon.Length;
			
			for(int i=0;i<konl;i+=2)
			{
				
				if(bythumb.Contains(kon[i]))
				{
					kon[i]=string.Empty;
					kon[i+1]=string.Empty;
					
				}
				
			}
			
			File.WriteAllLines(@"Q:\z\bookpdf\0bak\tu\twtknu_v.csv+",kon);
			
			kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\twtkold.csv");
			konl=kon.Length;
			
			for(int i=0;i<konl;i+=2)
			{
				
				if(bythumb.Contains(kon[i]))
				{
					kon[i]=string.Empty;
					kon[i+1]=string.Empty;
					
				}
				
			}
			
			File.WriteAllLines(@"Q:\z\bookpdf\0bak\tu\twtkold.csv+",kon);
		}
		
		static void cleantwtknu()
		{
			HashSet<string> bythumb = new HashSet<string>();
			string[] kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\twtknu.csv");
			int konl=kon.Length;
			for(int i=1;i<konl;i+=2)
			{
				if(bythumb.Contains(kon[i]))
				{
					kon[i-1]=string.Empty;
					kon[i]=string.Empty;
				} else {bythumb.Add(kon[i]);}
			}
			
			File.WriteAllLines(@"Q:\z\bookpdf\0bak\tu\twtknu.csv+",kon);
		}
		
		static void cleanrepl()
		{
			HashSet<string> bythumb = new HashSet<string>();
			string[] kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\repl.txt");
			int konl=kon.Length;
			for(int i=0;i<konl;i++)
			{
				var vz=kon[i];
				if(vz.Length>10&&vz[0]!=';')
				{
					string[] zp=vz.Split(sep0x9);
					bythumb.Add(zp[0]);
				}
				
			}
			
			kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\replfx.txt");
			konl=kon.Length;
			for(int i=0;i<konl;i++)
			{
				var vz=kon[i];
				if(vz.Length>10)
				{
					string[] zp=vz.Split(sep0x9);
					bythumb.Add(zp[0]);
				}
				
			}
			
			kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\greasemonkey\requires\dbs.js");
			konl=kon.Length;
			for(int i=0;i<konl;i++)
			{
				var vz=kon[i];
				if(vz.Length>20)
				{
					string[] zp=vz.Split(sepQuo_s);
					if(bythumb.Contains(zp[3]))
					{
						kon[i]=string.Empty;
					}
				}
				
			}
			
			File.WriteAllLines(@"Q:\z\bookpdf\0bak\tu\greasemonkey\requires\dbs.js+",kon);
		}
		
		static void prtnup()
		{
			string[] jsk={
			"2gcg",
"2img",
"2ma",
"2ws",
"3kimoi",
"3msc",
"3trash",
"1cg",
"1djs",
"1gcg",
"1img",
"1ma",
"1ws",
"2cg",
"2djs"};
			for(int fv=0;fv<15;fv++)
			{
				drdup(jsk[fv]);
			}
			
			/*
			bool[] mmata=new bool[730000];
			
			Dictionary<string,int> kole =new Dictionary<string, int>();
			
			for(int fv=0;fv<15;fv++)
			{
				string[] kon=File.ReadAllLines(@"Q:\z\bookpdf\0bak\tu\ar\2\"+jsk[fv]+".txt");
				int konl=kon.Length;
				for(int i=0;i<konl;i++)
				{
					if(kon[i].Length<5)
					{
						continue;
					}
					
					
					string[] ozb=kon[i].Split(sep0x9);
					string oz=ozb[0].Split(sep0x20)[2];
					int zna=int.Parse(ozb[1].Split(sepDuo)[1]);
					mmata[zna]=true;
					int dvf=-1;
					if(kole.TryGetValue(oz,out dvf))
					{
						if(dvf!=zna)
						{
							mmata[dvf]=false;
							mmata[zna]=false;
							
						}
					}
					else
					{
						kole[oz]=zna;
					}
				}
				
			}
			for(int i=0;i<730000;i++)
			{
				if(!mmata[i])
				{
					Console.WriteLine(i);
				}
			}
			*/
		}
		
		static void cleanrepv()
		{
			HashSet<string> bythumb = new HashSet<string>();
			string[] kon=File.ReadAllLines("../../../aadata.js");
			int konl=10+(kon.Length/3);
			for(int i=0;i<konl;i++)
			{
				string[] vz=kon[i].Split(sepQuo_s);
				if(vz.Length==3)
				{
					
					bythumb.Add(vz[1]);
				}
				
			}
			
			kon=File.ReadAllLines("../repl_v.txt");
			konl=kon.Length;
			
			for(int i=0;i<konl;i++)
			{
				var vz=kon[i];
				if(vz.Length>20)
				{
					string[] zp=vz.Split(sep0x9);
					if(bythumb.Contains(zp[0]))
					{
						kon[i]=string.Empty;
					}
					else {bythumb.Add(zp[0]);}
				}
			}
			File.WriteAllLines(@"../repl_v.txt+",kon);
		}
		
		static void Main(string[] args)
		{
			
			cleanrepv();
			
			
		}
		
	}
}