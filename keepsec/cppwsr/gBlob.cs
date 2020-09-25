
using System;

namespace G1conso
{
	
	public class bitarr: IEquatable<bitarr>
	{
		public static int blen = 6;
		public static int toGua = 0;
		public static string fmt = "D2";
		public bool[] arr = new bool[blen];
		const char str0 = '0';
		const char str1 = '1';
		
		public static int[][] setblen(int bb, bool cabi)
		{
			blen = bb * 2;
			
			uint arrsz = 0xFFFFFFFF;
			arrsz >>= blen;
			arrsz <<= blen;
			arrsz = ~arrsz;
			
			arrsz++;
			//Console.WriteLine("=>"+arrsz);
			fmt = "D" + ((int)(Math.Ceiling(Math.Log10(arrsz))));
			
			
			
			if (cabi) {
				int[][] rett = new int[arrsz][];
				
				for (int i = 0; i < arrsz; i++) {
					rett[i] = new int[blen];
				}
				
				return rett;
			}
			return null;
		}
		
		public bitarr(bool fillt)
		{
			arr = new bool[32];
			
			if (fillt) {
				for (int i = 0; i < 32; i++)
					arr[i] = true;
			}
		}
		
		public bitarr(int num)
		{
			for (int i = 0; i < blen; i++) {
				arr[i] = ((num & 1) == 1);
				num >>= 1;
			}
		}
		
		public bitarr(bool[] inarr)
		{
			for (int i = 0; i < blen; i++) {
				arr[i] = inarr[i];
			}
		}
		
		public bitarr(bool[] inarr, bool neg)
		{
			for (int i = 0; i < blen; i++) {
				arr[i] = !inarr[i];
			}
		}
		
		public int toInt(bool usetsuo = false)
		{
			int num = 0;
			
			if (usetsuo) {
				for (int i=blen-1; i >= 0; i--) {
					if (!arr[i])
						num |= 1;
					num <<= 1;
				}
			} else {
				for (int i = blen-1; i >=0; i--) {
					if (arr[i])
						num |= 1;
					num <<= 1;
				}
			}
			
			
				
				
			
			
			return num >> 1;
		}
		
		public static bitarr Zero = new bitarr(false);
		public static bitarr One = new bitarr(true);
		
		public bool Equals(bitarr other)
		{
			for (int i = 0; i < blen; i++) {
				if (arr[i] != other.arr[i])
					return false;
			}
			
			return true;
		}
		
		public bitarrPair Tsuo(int src, byte dst)
		{
			bitarrPair k = new bitarrPair(new bitarr(this.arr), new bitarr(this.arr, true));
			
			var srcboo = k.a[0].arr[src];
			var dstboo = k.a[1].arr[dst];
			
			k.a[0].arr[src] = dstboo;
			k.a[1].arr[dst] = srcboo;
			
			return k;
		}
		
		/*
		public override bool Equals(object obj)
		{
			return Equals(obj as bitarr);
		}
		
		public override int GetHashCode()
    	{
			return toInt();
		}
		
		*/
		
		public override string ToString()
		{
			if (toGua == 1) {
				char[] otpu = new char[blen];
			
				for (int i = 0; i < blen; i++) {
					otpu[i] = arr[i] ? str1 : str0;
				}
			
				return new string(otpu);
			} else
				return toInt().ToString(fmt);
		}
		
		/*
		public static ushort mksrcdd(int src,int dst)
		{
			
			return (ushort)(dst|(src<<8));
		}
		*/
		
		public static byte[][] GenRule()
		{
			/*
			ushort[][] ret = new ushort[3][];
			
			for(int i=0;i<3;i++)
			{
				ret[i]=new ushort[blen];
			}
			
			int hafblen=blen>>1;
			for(int i=0;i<hafblen;i++)
			{
				ret[0][i]=mksrcdd(i,i+hafblen);
				ret[0][i+hafblen]=mksrcdd(i+hafblen,i);
				
				ret[1][i*2]=mksrcdd(i*2,i*2+1);
				ret[1][i*2+1]=mksrcdd(i*2+1,i*2);
				
				
				ret[2][i]=mksrcdd(i,blen-i-1);
				ret[2][blen-i-1]=mksrcdd(blen-i-1,i);
			}
			*/
			
			byte[][] ret = new byte[3][];
			
			for (int i = 0; i < 3; i++) {
				ret[i] = new byte[blen];
			}
			
			int hafblen = blen >> 1;
			for (int i = 0; i < hafblen; i++) {
				ret[0][i] = (byte)(i + hafblen);
				ret[0][i + hafblen] = (byte)(i);
				
				ret[1][i * 2] = (byte)(i * 2 + 1);
				ret[1][i * 2 + 1] = (byte)(i * 2);
				
				
				ret[2][i] = (byte)(blen - i - 1);
				ret[2][blen - i - 1] = (byte)(i);
			}
			
			return ret;
			
		}

	}
	
	public class bitarrPair : IEquatable<bitarrPair>
	{
		public bitarr[] a = new bitarr[2];
		
		public bool Equals(bitarrPair other)
		{
			if (a[0] == other.a[0] && a[1] == other.a[1])
				return true;
			
			if (a[1] == other.a[0] && a[0] == other.a[1])
				return true;
			
			return false;
		}
		
		/*
		public override bool Equals(object obj)
		{
			return Equals(obj as bitarrPair);
		}
		
		public override int GetHashCode()
    	{
			return (int)((a[0].toInt()<<16)|a[1].toInt());
    	}
		*/
		
		public bitarrPair(bitarr  a1, bitarr a2)
		{
			a[0] = a1;
			a[1] = a2;
		}
		
		public bitarrPair(bitarrPair  inin)
		{
			for (int vv = 0; vv < 2; vv++) {
				a[vv] = new bitarr(inin.a[vv].arr);
			}
		}
		
		
		
		public bitarrPair xchng(int src, byte dst)
		{
			
			
			
			bitarrPair k = new bitarrPair(this);
			
			var srcboo = k.a[0].arr[src];
			var dstboo = k.a[1].arr[dst];
			
			k.a[0].arr[src] = dstboo;
			k.a[1].arr[dst] = srcboo;
			
			return k;
		}
		
		public override string ToString()
		{
			//if(a[0].toInt()==a[1].toInt())
			//	return "["+a[0]+"]";
			
			
			return "[" + a[0] + ", " + a[1] + "]";
			
		}
	}
	
	
	public class gBlob
	{
		public static string[] kordfmt = {"{0}初{1}",
			"{0}{1}二",
			"{0}{1}三",
			"{0}{1}四",
			"{0}{1}五",
			"{0}上{1}"
		};
		
		public static char[] gua = {
			'坤', '復', '師', '臨', '謙', '夷', '升', '泰',
			//8
			'豫', '震', '解', '妹', 'ｇ', '豐', '恆', '壯',
			//16
			'比', '屯', '坎', '節', '蹇', '既', '井', '需',
			//24
			'萃', '隨', '困', '兌', '咸', '革', 'Ｇ', '夬',
			//32
			'剝', '頤', '蒙', '損', '艮', '賁', '蠱', 'Ｘ',
			//40
			'晉', '噬', '未', '睽', '旅', '離', '鼎', '有',
			//48
			'觀', '益', '渙', '中', '漸', '家', '巽', 'ｘ',
			//56
			'否', '妄', '訟', '履', '遯', '同', '姤', '乾'
		};
		
		/*
		 * 

坤復師臨 謙夷升泰


坤復師臨 謙夷升泰
豫震解妹 ｇ豐恆壯
比屯坎節 蹇既井需
萃隨困兌 咸革Ｇ夬
剝頤蒙損 艮賁蠱Ｘ
晉噬未睽 旅離鼎有
觀益渙中 漸家巽ｘ
否妄訟履 遯同姤乾

		*/
		
		public static int gsizex2;
		public static int gmask;
		public static int gmaskhalf;
		public int gnum;
		public int[] links;
		public bool[] BeLinked;
		public static string pfm = "D2";
		
		public gBlob(int na)
		{
			gnum = na;
			links = new int[gsizex2];
			BeLinked = new bool[gsizex2];
		}
		
		public static string prtsgn(gBlob inblob, int lnkidx)
		{
			if (inblob.BeLinked[lnkidx])
				return "\t-" + inblob.links[lnkidx].ToString(pfm);
			else
				return "\t" + inblob.links[lnkidx].ToString(pfm);
		}
		
		
		public static string liujiu(int namu, int ord)
		{
			if (((namu >> ord) & 1) == 0)
				return "六";
			else
				return "九";
		}
		
		static int[] rmap = { 3, 4, 5, 0, 1, 2 };
		
		public static string Pgua(int ig,int ord,bool tsuo)
		{
			if(tsuo)
			{
				if(ig<0)
					ig=-ig;
				
				ig--;
			}
			
			return string.Format(kordfmt[ord], gua[ig], liujiu(ig, ord));
		}
		
		public static string prtsgn_gua(gBlob inblob, int lnkidx)
		{
			string jiantou = "\n=>";
			if (inblob.BeLinked[lnkidx])
				jiantou = "\n<=";
			
			int basgua = inblob.gnum;
			
			string spa1 = string.Format(kordfmt[lnkidx], gua[basgua], liujiu(basgua, lnkidx));
			
			
			basgua = inblob.links[lnkidx];
			int dyst = rmap[lnkidx];
			string spa2 = string.Format(kordfmt[dyst], gua[basgua], liujiu(basgua, dyst));
			
			return spa1 + jiantou + spa2 + "\n";
		}
		
		public static string spgua(int nn)
		{
			int gg = nn / 64;
			int gg2 = nn % 64;
			return gua[gg] + "_" + gua[gg2];
		}
		
		public static string prtsgn_gua2(gBlob inblob, int lnkidx)
		{
			if (inblob.BeLinked[lnkidx])
				return "\t-" + spgua(inblob.links[lnkidx]);
			else
				return "\t" + spgua(inblob.links[lnkidx]);
		}
		
		public static gBlob[] make(int sz)
		{
			gsizex2 = sz * 2;
			
			gmask = ~((-1 >> gsizex2) << gsizex2);
			
			
			gmaskhalf = ~((-1 >> sz) << sz);
			
			
			var arrsz = 1 << gsizex2;
			
			pfm = "X2";//"D"+((int)(Math.Ceiling(Math.Log10(arrsz)))-1).ToString();
			
			
			var rett = new gBlob[arrsz];
			
			for (int i = 0; i < arrsz; i++) {
				rett[i] = new gBlob(i);
				
				
			}
			
			for (int vv = 0; vv < arrsz; vv++) {
				for (int k = 0; k < sz; k++) {
					int lower = vv & gmaskhalf;
					int upper = (vv >> sz) & gmaskhalf;
					
					if (((lower >> k) & 1) == ((upper >> k) & 1)) {
						int negvv = ((~vv) & gmask);
						int dyst = negvv ^ (1 << k);
						rett[dyst].links[k] = vv;
						rett[dyst].BeLinked[k] = true;
						
						rett[vv].links[k + sz] = dyst;
						
						dyst = negvv ^ (1 << (k + sz));
						
						rett[dyst].links[sz + k] = vv;
						rett[dyst].BeLinked[sz + k] = true;
						
						rett[vv].links[k] = dyst;
						
					}
				}
			}
			
			for (int i = 0; i < arrsz; i++) {
				var tmpr = rett[i];
				Console.WriteLine("\n\n(" + spgua(tmpr.gnum) + "):"); 	//tmpr.gnum.ToString(pfm)
				//for(int k=gsizex2-1;k>=0;k--)
				for (int k = 0; k < gsizex2; k++) {
					Console.WriteLine(prtsgn_gua2(tmpr, k));
				}
			}
			
			return rett;
		}
	}
}
