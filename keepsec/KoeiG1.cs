
using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;



namespace KT
{
	/*
	public class Block
	{
		public int BaseOffset;
		public Block(int offset)
		{
			BaseOffset = offset;
		}
	}
	 */
	
	public class TMC
	{
		
	}
	
	
	public abstract class G1Mtagged
	{
		public abstract byte[] dump();
		public abstract unsafe void Load(byte[] buff);
		public G1Mtagged next;
	}
	
	public class MG : G1Mtagged
	{
		public int BaseOffset;
		public G1MGH Header;
		public MGMAP map = new MGMAP();
		public MG_vtx[] objB;
		
		
		public MG(int offset)
		{
			BaseOffset = offset;
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
		
		/*
		public unsafe void Load(byte[] buff,int offset)
		{
			
		}
		 */
		public override unsafe void Load(byte[] buff)
		{
			if (G1pkg.isBE)
				buff.fIX4charN(BaseOffset + 0xC, 2);
			
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MGH* f = (G1MGH*)srcbb;
				Header = f[0];
			}
			
			int nSub = Header.nSection;
			
			int cur = BaseOffset + 0x30;
			for (int vvv = 0; vvv < nSub; vvv++) {
				G1MGH_C tmpHD;
				fixed(byte* srcbb = &buff[cur]) {
					G1MGH_C* f = (G1MGH_C*)srcbb;
					tmpHD = f[0];
				}
				uint sig = tmpHD.sig & 0xFF;
				int tmpcur1 = cur + 0xC;
				
				switch (sig) {
					case 1:		//color
						s1(buff, tmpHD.num, tmpcur1);
						break;
					case 2:		//tex
						s2(buff, tmpHD.num, tmpcur1);
						break;
					case 3:		//shaderparam
						s3(buff, tmpHD.num, tmpcur1);
						break;
					case 4:		//vtx
						s4(buff, tmpHD.num, tmpcur1);
						break;
					case 5:		//vtxformat
						s5(buff, tmpHD.num, tmpcur1);
						break;
					case 6:		//SkinningMap
						s6(buff, tmpHD.num, tmpcur1);
						break;
					case 7:		//trilist
						s7(buff, tmpHD.num, tmpcur1);
						break;
					case 8:		//obj
						s8(buff, tmpHD.num, tmpcur1);
						break;
					case 9:		//shader
						s9(buff, tmpHD.num, tmpcur1);
						break;
					default:
						break;
				}
				
				cur += tmpHD.size;
			}
			
			map.s4 = objB;
			
			int n = objB.Length;
			for (int i = 0; i < n; i++) {
				var curB = objB[i];
				if (curB.Trash)
					continue;
				
				curB.fmt.main = i;
				FVF[] vtxfmt = curB.fmt.fmt;
				int nDecl = vtxfmt.Length;
				
				
				var tbyOrd = new Varray[nDecl];
				
				
				
				
				bool multiSrc = false;
				FVF[][] sepSrc = null;
				if (curB.fmt.Srcs.Length > 1) {
					multiSrc = true;
					int kk = curB.fmt.Srcs.Length;
					sepSrc = new FVF[kk][];
					int[] nsepSrc = new int[kk + 1];
					for (int i0 = 0; i0 < nDecl; i0++) {
						nsepSrc[vtxfmt[i0].src + 1]++;
					}
					
					for (int i0 = 0; i0 < kk; i0++) {
						int kk2 = nsepSrc[i0 + 1];
						var nyFVF = new FVF[kk2];
						var pad = nsepSrc[i0];
						for (int i1 = 0; i1 < kk2; i1++) {
							nyFVF[i1] = vtxfmt[pad + i1];
						}
						
						sepSrc[i0] = nyFVF;
					}
					
					vtxfmt = sepSrc[0];
					nDecl = vtxfmt.Length;
				}
				
				FillVarray(buff, tbyOrd, 0, curB, vtxfmt);
				
				if (multiSrc) {
					int kk = sepSrc.Length;
					for (int i0 = 1; i0 < kk; i0++) {
						FillVarray(buff, tbyOrd, sepSrc[i0 - 1].Length, curB.SubSrcs[i0 - 1], sepSrc[i0]);
					}
				}
				
				
				curB.byOrd = tbyOrd;
			}
			
			
			Dictionary<int,List<int>> vtx2grp = new Dictionary<int,List<int>>();
			n = map.s8.Length;
			for (int i = 0; i < n; i++) {
				var tuse = map.s8[i];
				vtx2grp.AddToList(tuse.Info.vtx_fmt_id, i);
				
				tuse.Skinning = map.s6[tuse.Info.BoneSkin_id];
				tuse.Idx = map.s7[tuse.Info.idx_id];
				
				//...
			}
			
			foreach (var kp in vtx2grp) {
				int sz = kp.Value.Count;
				int[] pp = kp.Value.ToArray();
				MG_Grp[] gp = new MG_Grp[sz];
				for (int i = 0; i < sz; i++) {
					gp[i] = map.s8[pp[i]];
				}
				
				objB[map.s5[kp.Key].main].Renders = gp;
			}
		}
		
		void s1(byte[] buff, int HDnum, int tmpcur1)
		{
			for (int i = 0; i < HDnum; i++) {
				
			}
		}
		
		void s2(byte[] buff, int HDnum, int tmpcur1)
		{
			for (int i = 0; i < HDnum; i++) {
				
			}
		}
		
		unsafe void s3(byte[] buff, int HDnum, int tmpcur1)
		{
			for (int i = 0; i < HDnum; i++) {
				
			}
		}
		
		unsafe void s4(byte[] buff, int HDnum, int tmpcur1)
		{
			objB = new MG_vtx[HDnum];
			
			for (int i = 0; i < HDnum; i++) {
				
				var tobjB = new MG_vtx(i);
				
				tobjB.C0 = buff.ri4(tmpcur1);
				tmpcur1 += 4;
				int EleSz = buff.ri4(tmpcur1);
				tmpcur1 += 4;
				int EleCot = buff.ri4(tmpcur1);
				tmpcur1 += 4;
				tobjB.C3 = buff.ri4(tmpcur1);
				tmpcur1 += 4;
				
				
				tobjB.BaseOffset = tmpcur1;
				tobjB.fast_count = EleCot;
				tobjB.fast_size = EleSz;
				
				objB[i] = tobjB;
				
				tmpcur1 += EleSz * EleCot;
				
			}
		}
		
		unsafe void s5(byte[] buff, int HDnum, int tmpcur1)
		{
			map.s5 = new MG_VertexFmt[HDnum];
			for (int i = 0; i < HDnum; i++) {
				
				
				int nUseBlock = buff.ri4(tmpcur1); // How Many vtx Blocks use this Desc?
				
				
				tmpcur1 += 4;
				int[] Share = buff.ri4N(tmpcur1, nUseBlock);
				tmpcur1 += 4 * nUseBlock;
				
				MG_VertexFmt tmpfmt = new MG_VertexFmt(i);
				tmpfmt.Srcs = Share;
				
				objB[Share[0]].fmt = tmpfmt;
				
				if (nUseBlock > 1) {
					var srcs = new MG_vtx[nUseBlock - 1];
					for (int i0 = 1; i0 < nUseBlock; i0++) {
						var ansrc = objB[Share[i0]];
						ansrc.Trash = true;
						ansrc.fmt = tmpfmt;
						srcs[i0 - 1] = ansrc;
					}
					objB[Share[0]].SubSrcs = srcs;
				}
				
				int nDecl = buff.ri4(tmpcur1);		//sizeof(FVF)*nDecl
				tmpcur1 += 4;
				if (G1pkg.isBE) {

					fixed(byte* srcbb = &buff[tmpcur1]) {
						bswap* f = (bswap*)srcbb;
						for (int i1 = 0; i1 < nDecl; i1++) {
							f[i1 * 2]._2fix();
							f[i1 * 2 + 1]._1();
							
							
						}
						
						
					}
					
				}
				
				tmpfmt.fmt = new FVF[nDecl];
				
				fixed(byte* srcbb = &buff[tmpcur1]) {
					FVF* f = (FVF*)srcbb;
					for (int i1 = 0; i1 < nDecl; i1++) {
						tmpfmt.fmt[i1] = f[i1];
					}
				}
				
				
				
				
				tmpfmt.BaseOffset = tmpcur1;
				map.s5[i] = tmpfmt;
				tmpcur1 += 8 * nDecl;
				
				
			}
		}
		
		unsafe void s6(byte[] buff, int HDnum, int tmpcur1)
		{
			var ause = new MG_BoneSkin[HDnum];
			for (int i = 0; i < HDnum; i++) {
				int cot = buff.ri4(tmpcur1);
				tmpcur1 += 0x4;
				var tuse = new MG_BoneSkin(i);
				var mapp = new MGs6[cot];
				
				fixed(byte* srcbb = &buff[tmpcur1]) {
					MGs6* f = (MGs6*)srcbb;
					for (int i1 = 0; i1 < cot; i1++) {
						mapp[i1] = f[i1];
							
							
					}
				}
				tuse.BaseOffset = tmpcur1;
				tuse.map = mapp;
				ause[i] = tuse;
				tmpcur1 += 0xC * cot;
				
			}
			map.s6 = ause;
		}
		
		unsafe void s7(byte[] buff, int HDnum, int tmpcur1)
		{
			var ause = new MG_idx[HDnum];
			for (int i = 0; i < HDnum; i++) {
				int[] info = buff.ri4N(tmpcur1, 3);
				tmpcur1 += 0xC;
				MG_idx tuse = new MG_idx(i);
				tuse.BaseOffset = tmpcur1;
				tuse.C1 = info[1];
				tuse.C2 = info[2];
				int sz = info[0];
				var tuseidx = new ushort[sz];
				int cur1padding = 0;
				if (sz % 2 != 0)
					cur1padding = 2;
				if (G1pkg.isBE) {
					int sz4 = sz / 2;
					if (cur1padding != 0)
						sz4++;
					
					fixed(byte* srcbb = &buff[tmpcur1]) {
						bswap* f = (bswap*)srcbb;
						for (int i1 = 0; i1 < sz4; i1++) {
							f[i1]._2fix();
							
							
						}
					}
				} 
				
				fixed(byte* srcbb = &buff[tmpcur1]) {
					ushort* f = (ushort*)srcbb;
					for (int i1 = 0; i1 < sz; i1++) {
						tuseidx[i1] = f[i1];
							
							
					}
				}
				
				tuse.idx = tuseidx;
				ause[i] = tuse;
				tmpcur1 += sz * 2 + cur1padding;
			}
			map.s7 = ause;
		}
		
		unsafe void s8(byte[] buff, int HDnum, int tmpcur1)
		{
			var ause = new MG_Grp[HDnum];
			
			
			
			fixed(byte* srcbb = &buff[tmpcur1]) {
				MGs8* f = (MGs8*)srcbb;
				for (int i = 0; i < HDnum; i++) {
					MG_Grp tuse = new MG_Grp(i);
					if (G1pkg.isBE) {
						var ut = f[i].flag >> 2;
						f[i].flag = ut | (ut >> 29);
					}
					
					tuse.Info = f[i];
				
				
					ause[i] = tuse;
				}
			}
			
			map.s8 = ause;
		}
		
		void s9(byte[] buff, int HDnum, int tmpcur1)
		{
			for (int i = 0; i < HDnum; i++) {
				
			}
		}
		
		unsafe void FillVarray(byte[] buff, Varray[] dst, int dst_start, MG_vtx vtx, FVF[] vtxfmt)
		{
			int cot = vtx.fast_count;
			int bsz = vtx.fast_size / 4;
			int tmpcur1 = vtx.BaseOffset;
			int nDecl = vtxfmt.Length;
			
			Dictionary<int,List<int>> dicbyType = new Dictionary<int,List<int>>();
			Dictionary<int,List<int>> dicbyUsage = new Dictionary<int,List<int>>();
			Dictionary<int,List<int>> dicbyIdx = new Dictionary<int,List<int>>();
			
			for (int i = 0; i < nDecl; i++) {
				FVF tfvf = vtxfmt[i];
				dicbyType.AddToList((int)tfvf.valuetype, dst_start + i);
				dicbyUsage.AddToList((int)tfvf.usage, dst_start + i);
				dicbyIdx.AddToList((int)tfvf.idx, dst_start + i);
				
				switch (tfvf.valuetype) {
					case D3DDECLTYPE.FLOAT2:
						dst[dst_start + i] = new Varray2F(cot, tfvf);
						break;
					case D3DDECLTYPE.FLOAT3:
						dst[dst_start + i] = new Varray3F(cot, tfvf);
						
						break;
					case D3DDECLTYPE.FLOAT4:
						dst[dst_start + i] = new Varray4F(cot, tfvf);
						
						break;
					case D3DDECLTYPE.UBYTE4:
					case D3DDECLTYPE.UDEC3:
						
						if (G1pkg.isBE) {
							
							fixed(byte* srcbb = &buff[tmpcur1+tfvf.offset]) {
								bswap* xx = (bswap*)srcbb;
								for (int i1 = 0; i1 < cot; i1++) {
									xx[i1 * bsz]._1();
								}
							}
						}
						
						dst[dst_start + i] = new Varray4U(cot, tfvf);
						
						break;
					default:
						UnityEngine.Debug.Log("Varray: " + tfvf.valuetype);
						Console.ReadKey();
						break;
				}
				
			}
			
			foreach (var kp in dicbyType) {
				vtx.byDataType[kp.Key] = kp.Value.ToArray();
			}
			
			foreach (var kp in dicbyUsage) {
				vtx.byUsage[kp.Key] = kp.Value.ToArray();
			}
			
			foreach (var kp in dicbyIdx) {
				vtx.byIdx[kp.Key] = kp.Value.ToArray();
			}
			
			fixed(byte* srcbb = &buff[tmpcur1]) {
				uint* f = (uint*)srcbb;
				
				for (int i0 = 0; i0 < nDecl; i0++) {
					var dd = dst[dst_start + i0];
					uint* pArrDst = dd.pArray;
					int subloop = dd.dsize;
					int bufOffset = dd.fvf.offset / 4;
					
					Parallel.For(0, cot, i => {
						//for (int i = 0; i < cot; i++) {
						for (int i1 = 0; i1 < subloop; i1++) {
							pArrDst[subloop * i + i1] = f[i * bsz + bufOffset + i1];
						}
					}
					);
					
				}
				
			}
			
			
			
		}
		
		
	}
	public class MGMAP
	{
		public int BaseOffset_s1;
		public MG_vtx[] s4;
		public MG_VertexFmt[] s5;
		public MG_BoneSkin[] s6;
		
		public MG_idx[] s7;
		public int BaseOffset_s8;
		public MG_Grp[] s8;
		
	}
	
	
	public class MG_Color		//s1
	{
		public MGs1 info;
		public int ord;
		public MG_Color(int o)
		{
			ord = o;
		}
	}
	
	public class MG_BoneSkin		//s6
	{
		public MGs6[] map;
		public int ord;
		public int BaseOffset;
		public MG_BoneSkin(int o)
		{
			ord = o;
		}
	}
	
	public class MG_tex		//s2
	{
		public int ord;
		public MG_tex(int o)
		{
			ord = o;
		}
	}
	
	public class MG_VertexFmt		//s5
	{
		public MG_VertexFmt(int o)
		{
			ord = o;
		}
		
		public int main;
		public int ord;
		public int BaseOffset;
		public int[] Srcs;
		public FVF[] fmt;
	}
	
	public class MG_idx
	{
		public ushort[] idx;
		public int C1;
		public int C2;
		
		public MG_idx(int o)
		{
			ord = o;
		}
		

		public int BaseOffset;
		public int ord;
	}
	
	public class MG_Grp		//s8
	{
		public MGs8 Info;
		public MG_Color Color;
		public MG_tex Tex;
		
		
		public MG_vtx Vtx;
		public MG_BoneSkin Skinning;
		public MG_idx Idx;
		
		public MG_shader_sub Shader;
		
		
		public MG_Grp(int o)
		{
			ord = o;
		}
		
		
		
		public int ord;
	
		
	}
	
	public class MG_shader_sub
	{
		public MGs9_Element info;
		public MG_shader_sub(int o)
		{
			ord = o;
		}
		public int BaseOffset;
		
		
		public int ord;
	}
	
	public class MG_shader
	{
		public MGs9_Header Header;
		public MG_shader_sub[] subShader;
		public MG_shader(int o)
		{
			ord = o;
		}
		
		
		public int BaseOffset;
		public int ord;
	}
	
	public class MG_vtx		//s4
	{
		public MG_vtx(int o)
		{
			ord = o;
		}
		

		public int BaseOffset;
		public int ord;
		
		public int C0;
		public int C3;
		public int fast_count;
		public int fast_size;
		public MG_VertexFmt fmt;
		public MG_Grp[] Renders;
		public MG_vtx[] SubSrcs;
		public bool Trash = false;
		
		public Varray Pos;
		public Varray Nrm;
		public Varray Tgt;
		public Varray2F UV;
		public BoneWeight[] RealBlendMapping;
		
		public Dictionary<uint,Varray> sig2type = new Dictionary<uint,Varray>();
		public Varray[] byOrd;
		public int[][] byDataType = new int[18][];
		public int[][] byUsage = new int[14][];
		public int[][] byIdx = new int[6][];
		
		public void BuildBoneWeight()
		{
			if (byUsage[2] == null)
				return;
			
			Varray4U Varr_idx = (Varray4U)byOrd[byUsage[2][0]];
			Varray Varr_wgt_r = byOrd[byUsage[1][0]];
			
			Varray4F Varr_wgt4F = null;
			Varray3F Varr_wgt3F = null;
			
			
			int dyk = Varr_wgt_r.dsize;
			
			
			
			switch (Varr_wgt_r.dsize) {
				case 1:
					Varray4U src = (Varray4U)Varr_wgt_r;
					Varr_wgt4F = new Varray4F(fast_count, Varr_wgt_r.fvf);
					Varr_wgt4F.fvf.valuetype = D3DDECLTYPE.FLOAT4;
					for (int i = 0; i < fast_count; i++) {
						Varr_wgt4F.arr[i].v4.x = (float)(((double)src.arr[i].a) / 255.0);
						Varr_wgt4F.arr[i].v4.y = (float)(((double)src.arr[i].b) / 255.0);
						Varr_wgt4F.arr[i].v4.z = (float)(((double)src.arr[i].c) / 255.0);
						Varr_wgt4F.arr[i].v4.w = (float)(((double)src.arr[i].d) / 255.0);
					}
					dyk = 4;
					break;
				case 3:
					Varr_wgt3F = (Varray3F)Varr_wgt_r;
					break;
				case 4:
					Varr_wgt4F = (Varray4F)Varr_wgt_r;
					break;
				default:
					break;
			}
			
			RealBlendMapping = new BoneWeight[fast_count];
			
			foreach (var rran in Renders) {
				int sta = rran.Info.Vert_Start;
				int cot = rran.Info.Vert_Count;
				var skm = rran.Skinning;
				if (dyk == 3) {
					for (int i = 0; i < cot; i++) {
						var nyubw = new BoneWeight();
						int sv= sta+i;
						bswap vulu = Varr_idx.arr[sv];
						nyubw.boneIndex0 = skm.map[vulu.a / 3].MSid;
						nyubw.boneIndex1 = skm.map[vulu.b / 3].MSid;
						nyubw.boneIndex2 = skm.map[vulu.c / 3].MSid;
						
					
						Vector3 v4 = Varr_wgt3F.arr[sv];
						nyubw.weight0 = v4.x;
						nyubw.weight1 = v4.y;
						nyubw.weight2 = v4.z;
						
					
						RealBlendMapping[sv] = nyubw;
					}
				} else {
					for (int i = 0; i < cot; i++) {
						var nyubw = new BoneWeight();
						int sv= sta+i;
						bswap vulu = Varr_idx.arr[sv];
						nyubw.boneIndex0 = skm.map[vulu.a / 3].MSid;
						nyubw.boneIndex1 = skm.map[vulu.b / 3].MSid;
						nyubw.boneIndex2 = skm.map[vulu.c / 3].MSid;
						nyubw.boneIndex3 = skm.map[vulu.d / 3].MSid;
					
						Vector4 v4 = Varr_wgt4F.arr[sv].v4;
						nyubw.weight0 = v4.x;
						nyubw.weight1 = v4.y;
						nyubw.weight2 = v4.z;
						nyubw.weight3 = v4.w;
						
					
						RealBlendMapping[sv] = nyubw;
					}
				}
			}
		}
		
		public string ToJSON()
		{
			if(Trash)
				return string.Empty;
			
			StringBuilder sb = new StringBuilder(fast_size * fast_count * 4);
			sb.Append('[');
			int vlen = byOrd.Length;
			for (int vv = 0; vv < fast_count; vv++) {
				sb.Append('{');
				for (int i = 0; i < vlen; i++) {
					sb.Append('\'');
					sb.Append(G1pkg.D3DusageStr[(int)byOrd[i].fvf.usage]);
					sb.Append("':'");
					sb.Append(byOrd[i].GetStr(vv));
					sb.Append("',");
				}
				sb.Length--;
				sb.Append("}\n,");
			}
			sb.Length--;
			sb.Append(']');
			return sb.ToString();
		}
		
		public string ToCSV(bool insideJS)
		{
			if(Trash)
				return string.Empty;
			
			StringBuilder sb = new StringBuilder(fast_size * fast_count * 4);
			
			string begg = "Offset\t";
			string rn = "\n{0:X}\t";
			if (insideJS) {
				begg = "var csv=\"Offset\t";
				rn = "@{0:X}\t";
			}
			
			sb.Append(begg);
			
			int vlen = byOrd.Length;
			for (int i = 0; i < vlen; i++) {
				sb.Append(G1pkg.D3DusageStr[(int)byOrd[i].fvf.usage]);
				sb.Append('\t');
			}
			sb.Length--;
			
			int bsy = BaseOffset;
			
			for (int vv = 0; vv < fast_count; vv++) {
				sb.AppendFormat(rn, bsy);
				for (int i = 0; i < vlen; i++) {
					sb.Append(byOrd[i].GetStr(vv));
					sb.Append('\t');
				}
				sb.Length--;
				bsy += fast_size;
				
			}
			
			if (insideJS) {
				sb.Append("\";");
			}
			
			return sb.ToString();
		}
		
		
		public string RealBlendMappingCSV(bool insideJS)
		{
			if(Trash)
				return string.Empty;
			
			if(RealBlendMapping==null)
				BuildBoneWeight();
			
			StringBuilder sb = new StringBuilder(fast_size * fast_count);
			
			string begg = "Offset\tWeight\tIndex";
			string rn = "\n{0:X}\t";
			if (insideJS) {
				begg = "var csv=\"Offset\tWeight\tIndex";
				rn = "@{0:X}\t";
			}
			
			sb.Append(begg);
			int bsy = BaseOffset;
			
			for (int vv = 0; vv < fast_count; vv++) {
				sb.AppendFormat(rn, bsy);
				sb.Append(RealBlendMapping[vv].GetStr());
				bsy += fast_size;
			}
			
			if (insideJS) {
				sb.Append("\";");
			}
			
			return sb.ToString();
		}
	}
	

	
	
	public class MF: G1Mtagged
	{
		public int BaseOffset;
		public G1MFH Header;
		
		public MF(int offset)
		{
			BaseOffset = offset;
		}
		
		public override unsafe void Load(byte[] buff)
		{
			
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MFH* f = (G1MFH*)srcbb;
				Header = f[0];
			}
			
			
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class MS: G1Mtagged
	{
		
		public G1MSH Header;
		
		public int BaseOffset;
		public MS(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MSH* f = (G1MSH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class MM: G1Mtagged
	{
		
		public G1MMH Header;
		
		public int BaseOffset;
		public MM(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MMH* f = (G1MMH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class COLL: G1Mtagged
	{
		
		public G1MMH Header;
		
		public int BaseOffset;
		public COLL(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MMH* f = (G1MMH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class NUNO: G1Mtagged
	{
		
		public G1MMH Header;
		
		public int BaseOffset;
		public NUNO(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MMH* f = (G1MMH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class NUNV: G1Mtagged
	{
		
		public G1MMH Header;
		
		public int BaseOffset;
		public NUNV(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MMH* f = (G1MMH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class EXTR: G1Mtagged
	{
		
		public G1MMH Header;
		
		public int BaseOffset;
		public EXTR(int offset)
		{
			BaseOffset = offset;
		}
		
		
		public override unsafe void Load(byte[] buff)
		{
			if (BaseOffset < 0)
				return;
			
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MMH* f = (G1MMH*)srcbb;
				Header = f[0];
			}
		}
		
		public override byte[] dump()
		{
			return new byte[0];
		}
	}
	
	public class G1M
	{
		public string id;
		public int BaseOffset;
		public G1T[] texture;
		
		public G1MH Header;
		public MF iG1MF;
		public MS iG1MS;
		public MM iG1MM;
		public MG iG1MG;
		public COLL iCOLL;
		public NUNO iNUNO;
		public NUNV iNUNV;
		public EXTR iEXTR;
		
		public G1Mtagged chain;
		
		
		public G1M(int offset)
		{
			BaseOffset = offset;
		}
		
		public unsafe void Load(byte[] buff, G1T[] tex)
		{
			if (chain != null)
				return;
			
			chain = new EXTR(-1);
			if (G1pkg.isBE) {
				G1pkg.bswapALL(buff, BaseOffset, 8);
			}
			
			
			fixed(byte* srcbb = &buff[BaseOffset]) {
				G1MH* f = (G1MH*)srcbb;
				Header = f[0];
			}
			
			int cur = BaseOffset + Header.HeaderLen;
			int end = BaseOffset + Header.hd.size;
			int nCompo_parsed = 0;
			int nCompo = Header.nComponent;
			
			G1Mtagged priv = chain;
			
			while (cur < end && nCompo_parsed < nCompo) {
				G1H_C tmpHD;
				fixed(byte* srcbb = &buff[cur]) {
					G1H_C* f = (G1H_C*)srcbb;
					tmpHD = f[0];
				}
				
				bool doLoad = true;
				switch (tmpHD.sig) {
					case G1Tag.MF:
						priv = priv.next = iG1MF = new MF(cur);
						
						break;
					case G1Tag.MS:
						priv = priv.next = iG1MS = new MS(cur);
						
						break;
					case G1Tag.MM:
						priv = priv.next = iG1MM = new MM(cur);
						
						break;
					case G1Tag.MG:
						priv = priv.next = iG1MG = new MG(cur);
						
						break;
					case G1Tag.COLL:
						priv = priv.next = iCOLL = new COLL(cur);
						
						break;
					case G1Tag.NUNO:
						priv = priv.next = iNUNO = new NUNO(cur);
						
						break;
					case G1Tag.NUNV:
						priv = priv.next = iNUNV = new NUNV(cur);
						
						break;
					case G1Tag.EXTR:
						priv = priv.next = iEXTR = new EXTR(cur);
						
						break;
					default:
						doLoad = false;
						UnityEngine.Debug.Log("UnkSub: " + G1pkg.bswapy._4char((uint)(tmpHD.sig)));
						break;
				}
				
				if (doLoad)
					priv.Load(buff);
				
				nCompo_parsed++;
				cur += tmpHD.size;
			}
			
			
			chain = chain.next;
			
			//=============
			if (tex != null) {
				texture = tex;
				
				foreach (G1T g1t in texture) {
					g1t.Load(buff);
				}
			}
		}
	}
	
	public class G1T
	{
		public string id;
		public int BaseOffset;
		public G1T(int offset)
		{
			BaseOffset = offset;
		}
		
		public void Load(byte[] buff)
		{
			
		}
		
	}
	
	public static class G1pkg
	{
		public static List<G1M> m_listl = new List<G1M>();
		public static List<G1T> t_listl = new List<G1T>();
		public static G1M[] m_list;
		public static G1T[] t_list;
		public static bool isBE = false;
		public static bswap bswapy;
		
		public static byte[] guessing(string file)
		{
			return guessing(File.ReadAllBytes(file));
		}
		
		
		public static unsafe void bswapALL(byte[] buf, int offset, int lenidx, int uselen = -1)
		{
			int rlen = 0;
			if (uselen >= 0) {
				rlen = uselen;
			} else if (lenidx >= 0) {
				rlen = bswapy._1(buf.ri4(offset + lenidx));
			} else
				return;
			
			rlen /= 4;
			fixed(byte* srcbb = &buf[offset]) {
				bswap* f = (bswap*)srcbb;
				for (int i = 0; i < rlen; i++)
					f[i]._1();
			}
		}
		
		public static byte[] guessing(byte[] buf)
		{
			
			
			if (buf.Length < 4)
				return buf;
			
			
			uint firstsig2 = buf.ru4(0);
			isBE = false;
			
			uint gay = CreateBySig(0, firstsig2, false);
			
			if (gay != 0) {
				goto ret;
			}
			
			isBE = true;
			gay = CreateBySig(0, bswapy._1(firstsig2), false);
			if (gay != 0) {
				goto ret;
			}
			
			
			isBE = false;
			
			if (firstsig2 > (buf.Length / 8)) {
				isBE = true;
				firstsig2 = bswapy._1(firstsig2);
			}
			
			if (!chkpkg(buf, firstsig2)) {
				isBE = false;
				return buf;
			}
			
			ret:
			
			bool hastex = false;
			
			if (t_listl.Count != 0) {
				hastex = true;
				t_list = t_listl.ToArray();
				t_listl = null;
				
			}
			
			
			if (m_listl.Count != 0) {
				m_list = m_listl.ToArray();
				m_listl = null;
				foreach (var mm in m_list) {
					mm.Load(buf, hastex ? t_list : null);
				}
			}
			
			t_listl = null;
			m_listl = null;
			
			
			isBE=false;
			
			return buf;
		}
		
		static bool chkpkg(byte[] buf, uint count)
		{
			if (count == 0)
				return false;
			
			uint hdsize = buf.ru4(4);
			if (isBE)
				hdsize = bswapy._1(hdsize);
			
			if (count * 8 + 4 != hdsize)
				return false;
			
			uint gay = 0;
			
			if (isBE) {
				for (int i = 0; i < count; i++) {
					int addr = bswapy._1(buf.ri4(4 + i * 8));
					if (buf.ri4(8 + i * 8) != 0)
						gay += CreateBySig(addr, bswapy._1(buf.ru4(addr)));
				}
			} else {
				for (int i = 0; i < count; i++) {
					int addr = buf.ri4(4 + i * 8);
					if (buf.ri4(8 + i * 8) != 0)
						gay += CreateBySig(addr, buf.ru4(addr));
				}
			}
			
			
			
			
			if (gay == 0)
				return false;
			
			return true;
			
		}
		
		static uint CreateBySig(int offset, uint sig, bool dolog = true)
		{
			switch ((G1Tag)sig) {
				case G1Tag.G1M:	//G1M
					m_listl.Add(new G1M(offset));
					return 1;
				case G1Tag.G1T:	//G1T
					t_listl.Add(new G1T(offset));
					return 0x10000;
				default:
					if (dolog)
						UnityEngine.Debug.Log("Unktag: " + bswapy._4char(sig));
					return 0;
			}
		}
		
		public static unsafe void fIX2wordN(this byte[] src, int offset, int n)
		{
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				for (int i = 0; i < n; i++) {
					f[i] = bswapy._2fix(f[i]);
				}
			}
		}
		
	
		
		public static unsafe void fIX2wordNP(this byte[] src, int offset, int n, int mul)
		{
			mul /= 4;
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				for (int i = 0; i < n; i++) {
					f[mul * i] = bswapy._2fix(f[mul * i]);
				}
			}
		}
		
		public static unsafe void fIX2word(this byte[] src, int offset)
		{
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				
				f[0] = bswapy._2fix(f[0]);
				
			}
		}
		
		public static unsafe void fIX4charN(this byte[] src, int offset, int n)
		{
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				for (int i = 0; i < n; i++) {
					f[i] = bswapy._1(f[i]);
				}
			}
		}
		
		public static unsafe void fIX4charNP(this byte[] src, int offset, int n, int mul)
		{
			mul /= 4;
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				for (int i = 0; i < n; i++) {
					f[mul * i] = bswapy._1(f[mul * i]);
				}
			}
		}
		
		public static unsafe void fIX4char(this byte[] src, int offset)
		{
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				
				f[0] = bswapy._1(f[0]);
				
			}
		}
		
		
		public static unsafe uint ru4(this byte[] src, int offset)
		{
			fixed(byte* srcbb = &src[offset]) {
				//uint* f = (uint*)srcbb;
				//return f[0];
				return *(uint*)srcbb;
			}
		}
		
		public static unsafe int ri4(this byte[] src, int offset)
		{
			fixed(byte* srcbb = &src[offset]) {
				//int* f = (int*)srcbb;
				//return f[0];
				return *(int*)srcbb;
			}
		}
		
		public static unsafe uint[] ru4N(this byte[] src, int offset, int n)
		{
			uint[] ret = new uint[n];
			fixed(byte* srcbb = &src[offset]) {
				uint* f = (uint*)srcbb;
				for (int i = 0; i < n; i++) {
					ret[i] = f[i];
				}
			}
			
			return ret;
		}
		
		public static unsafe int[] ri4N(this byte[] src, int offset, int n)
		{
			int[] ret = new int[n];
			fixed(byte* srcbb = &src[offset]) {
				int* f = (int*)srcbb;
				for (int i = 0; i < n; i++) {
					ret[i] = f[i];
				}
			}
			
			return ret;
		}
		
		public static void AddToList(this Dictionary<int, List<int>> dictionary, int key, int value)
		{
			List<int> tlist;
			if (dictionary.TryGetValue(key, out tlist)) {
				tlist.Add(value);
			} else {
				tlist = new List<int>();
				tlist.Add(value);
				dictionary[key] = tlist;
			}
			
			
		}
		
		public static string GetStr(this BoneWeight bw)
		{
			
			return $"{bw.weight0}, {bw.weight1}, {bw.weight2}, {bw.weight3}\t{bw.boneIndex0:X2}, {bw.boneIndex1:X2}, {bw.boneIndex2:X2}, {bw.boneIndex3:X2}";
		}
		
		#region dics
		public static string[] D3DusageStr = {
			"Pos",
			"BWgt",
			"BIdx",
			"Norm",
			"PSIZE",
			"UV",
			"Tngt",
			"BiNorm",
			"TESSFACTOR",
			"POSITIONT",
			"Colr",
			"FOG",
			"DEPTH",
			"SAMPLE"
		};
		public static Dictionary<string, string> shLUT = new Dictionary<string, string>() {
			{ "@0000CD94","S4" },
			{ "@001F0240","PBB" },
			{ "@001F74FF","CBC" },
			{ "@00200625","WID" },
			{ "@00202EF0","STD" },
			{ "@002440C6","ESM" },
			{ "@00266DFA","RLR" },
			{ "@018A1DA6","PBAS-ACsHhaNRpScm" },
			{ "@0318C5BD","PBB-CIra3N" },
			{ "@040B6A10","R4KG" },
			{ "@046D56A6","RAIN" },
			{ "@0477CDB0","HBAO" },
			{ "@04A25925","STAR" },
			{ "@04A750F8","MRLR" },
			{ "@04C06AA8","EYET" },
			{ "@0656C1A7","PBAB-AHhaNScm" },
			{ "@075E56CF","PBBTRCP-GbN" },
			{ "@07E303FE","PBB-AIriN" },
			{ "@08FB162D","PBBGRASS2-B1GbN" },
			{ "@0A50ADCE","PBB-Lm" },
			{ "@0AEE836B","KTGLTerrainShaderRSM" },
			{ "@0C15CEC1","PBB-ADsmGbGfPm" },
			{ "@0CEE747F","PBB-AlrsmIra3sSn" },
			{ "@0D0D124A","CLOUDPLANE" },
			{ "@0D323E09","DS-Cll" },
			{ "@0E51B1A3","PBB-Ira3sN" },
			{ "@0E6B92FF","PBB-AGbN" },
			{ "@11D5443D","PBB-CGbN" },
			{ "@127245DB","PBB-AlrsmGbIra3Sn" },
			{ "@15522542","PBAB-AGfIriNRp" },
			{ "@159B52B2","PBBGRASS2-B1N" },
			{ "@168FADB2","EYET-Zc" },
			{ "@1871C03F","PBAB-AGbHhaN" },
			{ "@18CEBEB2","PBB-AlrdpeCGbIlaIra3h" },
			{ "@1B4C0A12","PBB-AEmN" },
			{ "@1C81A0F0","PBBTRCP-LmGbN" },
			{ "@1C8FB3EA","PBB-CEmLrN" },
			{ "@1CB7DCD3","PBAS-ACN" },
			{ "@1DB2FF5E","PBBTRFR-Gb" },
			{ "@1E1B73BD","VFOG_SCATTER" },
			{ "@1EB5BB50","PBB-CEmN" },
			{ "@1FA3C744","PBAB-AHhaN" },
			{ "@21858E0D","PBB-AIriNbSss" },
			{ "@2240F3FB","PBB-GbLmN" },
			{ "@231705D7","PBDECAL-Cdl" },
			{ "@24945D4A","PBAS-ACCsHhaNRp" },
			{ "@2691B283","PBB-ACEmGbGfN" },
			{ "@2BA9FF66","LPVPROPAGATION" },
			{ "@2BF86DEF","PBAS-ACCsGbHhaNRp" },
			{ "@2F628388","CLOUDPARTICLE" },
			{ "@2FA78241","PBB-GbLmM3N" },
			{ "@30568CA7","PBB-AlrdpeCIlaIra3N" },
			{ "@3173DFA9","PBB-AlrdpeIlaIra3hsN" },
			{ "@32A13AB5","PBBTRCP-LmN" },
			{ "@33AE1016","PBB-GbSpo" },
			{ "@35446E6D","PBB-AGbGfNWtp" },
			{ "@3A52C98F","PBB-AlrdpeGbIlaIra3N" },
			{ "@3B77700E","KTGLTerrainShaderFW" },
			{ "@3BD606AE","PBAB-ACGbHhaNRp" },
			{ "@3C4896A9","SDCTH-Sb" },
			{ "@3CA1121F","PBBGRASS2-LmGbN" },
			{ "@410DC186","PBB-LmM3N" },
			{ "@4293C5B1","PBB-EmNT0Tn" },
			{ "@430C3252","PBB-CIra3hsN" },
			{ "@4465D763","KTGLTerrainShaderPl" },
			{ "@46E0DAFF","KTGLTerrainShader" },
			{ "@470991FB","PBB-ACGbGfN" },
			{ "@48F5E344","PBBTRFR-Lm" },
			{ "@49414EA4","PBBGRASS2-LmN" },
			{ "@49CC0DFC","PBB-ACGbN" },
			{ "@4A11F98D","PBB-EmLrN" },
			{ "@4A7C62F1","LPVINJECTION" },
			{ "@4D0B0C35","PBB-NV" },
			{ "@4DC0AEB6","PBBTRFR" },
			{ "@4E575176","EYET-GbZd" },
			{ "@4F9BA1A0","PBAS-ACsGbHhaN" },
			{ "@51953C63","PBAS-AGb" },
			{ "@5267FB65","PBAS-ACsHhaN" },
			{ "@52BE1DB6","HEIGHT2NORMAL" },
			{ "@52C15846","PBB-ADsmGfPm" },
			{ "@55BC640D","PBAS-ACCsGbHhaN" },
			{ "@59CC0BE9","DS-CllRr" },
			{ "@5A0DE057","EYET-GbZc" },
			{ "@5C1ADA25","PBB-EmNTV" },
			{ "@5C3B77B2","PBB-AlrdpeCIlaIra3hsN" },
			{ "@5D0304BE","PBB-EmNT0TgTn" },
			{ "@5D782C33","STD-NT0" },
			{ "@5DB39DD4","PBBTRCP-N" },
			{ "@5DCC0D97","SHADOW-Sb" },
			{ "@6205E15A","PBB-Ira3N" },
			{ "@627F92DA","PBB-GbN" },
			{ "@6297F507","PBB-EmNT0TnV" },
			{ "@63B7FC41","PBB-AlrdpeGbIlaIra3" },
			{ "@64BBCE88","PBB-EmLmN" },
			{ "@66121A36","PBB_GBNT0Tn" },
			{ "@668AF81A","PBAS-AN" },
			{ "@6709B5BB","SMTRBR" },
			{ "@670DC409","PBB-GbIra3hN" },
			{ "@69D7D814","PBB-AlrdpeIlaIra3N" },
			{ "@6C3BF117","SWATER" },
			{ "@6C4913E4","PBAB-AGfIriN" },
			{ "@6C580E25","PBB-Ira3hsN" },
			{ "@6C5BB04C","SRIVER" },
			{ "@6DDD1837","SMTRFR" },
			{ "@6E2F1646","THAIR-Gb" },
			{ "@6F39CA8C","PBB-AlrdpeCGbIlaIra3N" },
			{ "@72580B10","PBB-AGbNbSss" },
			{ "@727AEE1A","PBB-AlrdpeCGbIlaIra3" },
			{ "@7440A2C5","PBSOCEAN-Gb" },
			{ "@7472E50A","R4KCB" },
			{ "@74B02236","PBB-AlrsmGbIra3" },
			{ "@74EDD58A","PBB-C" },
			{ "@75D36250","PBBTRFR-GbN" },
			{ "@76585601","PBB-EmGbTe" },
			{ "@76D7A576","PBB-NT0TgTn" },
			{ "@77370030","PBB-ANScmSss" },
			{ "@79E7FB4E","PBB-AlrsGbIra3sVd" },
			{ "@7A726314","NOISE" },
			{ "@7A735049","PBB-CIra3hN" },
			{ "@7B326311","PBB-EmGbLm" },
			{ "@7E70DA91","EYET-Zd" },
			{ "@7E9A951B","PBB-AlrdpeGbIlaIra3hN" },
			{ "@7F9C218D","SDCTH" },
			{ "@80815943","PBB_GBNT0TgTn" },
			{ "@815555B0","PBB-AlrdpeCIlaIra3sN" },
			{ "@81DBB8A9","PBB-AlrdpeGbIlaIra3h" },
			{ "@84E9DF33","SHADOW" },
			{ "@87B3245F","PBB-N" },
			{ "@88E09B82","PBAB-RSM" },
			{ "@893B69CD","PBB-EmNT0TeTgTn" },
			{ "@893C967D","STD-EmNT0TeTgTn" },
			{ "@89D02B13","PBAS-RSM" },
			{ "@8AF6AC71","PBBTRFR-LmGbN" },
			{ "@8CB8B694","PBB-EmNT0TgTnV" },
			{ "@8D611F9B","PBB-AlrdpeCIlaIra3hN" },
			{ "@8F29D54E","PBB-Ira3hN" },
			{ "@8F7CD3EC","PBB-Gf" },
			{ "@8FAA321A","PBB-AN" },
			{ "@8FABD786","PBAB-ACHhaNRpScm" },
			{ "@9004B1B0","PBB-EmGb" },
			{ "@9010BB9E","THAIR" },
			{ "@91C3E32E","SKYPLANE" },
			{ "@92F621AA","PBAS-ACGb" },
			{ "@9313E358","PBB-CN" },
			{ "@93414606","PBB-EmTe" },
			{ "@937BDBA4","PBB-AGbSss" },
			{ "@942B2162","PBB-CGbIra3N" },
			{ "@95676CC7","PBB-ANSss" },
			{ "@95A21CEC","PBB-LmMm0N" },
			{ "@9767D5FD","PBB-ANScm" },
			{ "@99ABC198","PBB-AEmGbGfN" },
			{ "@99C4D66C","PBB-AGbNSss" },
			{ "@9A2804C8","PBAB-ACGbGfN" },
			{ "@9B0996EC","PBB-EmGbNT0Tn" },
			{ "@9E9B9327","PBB-GbGf" },
			{ "@9EBF5DAD","OUTLINE" },
			{ "@9F0D38EE","PBB-CGbIra3hN" },
			{ "@A03390D4","PBB-AEmGfIriNScm" },
			{ "@A1164636","PBBTRFR-LmN" },
			{ "@A23A754C","PBBTRBR-GbN" },
			{ "@A24BFE23","PBB-EmNT0TeTgTnV" },
			{ "@A2CBC5E7","PBAB-ACHhaN" },
			{ "@A43E81FE","CLOUDCIRCLE" },
			{ "@A4FA6D67","PBB-EmN" },
			{ "@A4FB9A17","STD-EmN" },
			{ "@AC7B42E8","PBAS-ACCsHhaN" },
			{ "@AD43BFF6","PBB-CRSM" },
			{ "@ADF0467B","PBB-AGfN" },
			{ "@B0929C27","PBB-GbLmMm0N" },
			{ "@B0EC59C0","PBB-LmN" },
			{ "@B119F589","WID_NS" },
			{ "@B2413DB2","PBB-AGfIriNWtp" },
			{ "@B37E1320","PBB-AGbGfN" },
			{ "@B3E07C89","PBB-GbLm" },
			{ "@B3EB7080","PBB-AlrdpeCGbIlaIra3hN" },
			{ "@B6C749EB","PBB-AlrdpeIlaIra3sN" },
			{ "@B75DBF6D","PBBTRBR-LmGbN" },
			{ "@B9633DFF","PBBTRFR-LmGb" },
			{ "@BB56ADAE","PBGE_DECAL" },
			{ "@BBCA43C8","PBB-EmGbLrN" },
			{ "@BBE3E6E8","PBB-Gb" },
			{ "@BC43A843","PBAB-AGbGfN" },
			{ "@BF97C09B","PBB-Spo" },
			{ "@BFF5B3D5","PBAS-AGbN" },
			{ "@C17AD19D","PBAB-AGbHhaNRp" },
			{ "@C1981B00","EYET-Gb" },
			{ "@C1B17866","PBB-AGbNSmSss" },
			{ "@C38203D1","HEIGHTFOG" },
			{ "@C3BCEC4C","PBAB-ACGbHhaN" },
			{ "@C7CCB9C7","CLOUDHEIGHT" },
			{ "@C82DBC53","PBB-AlrsIra3sVd" },
			{ "@C84B1A02","RIPPLE" },
			{ "@CC28A955","PBBTRFR-N" },
			{ "@CD7D5932","PBBTRBR-LmN" },
			{ "@D1CD5A16","EFFECT" },
			{ "@D352C603","PBB-EmGbLmN" },
			{ "@D658A408","PBB-EmGbNT0TeTgTn" },
			{ "@D6A546B9","PBB-EmGbNT0TgTn" },
			{ "@D96FD076","PBB-EmGbNT" },
			{ "@D9CC10C9","PBAB-ACHhaNRp" },
			{ "@DAA5D37E","PBAS-ACsGbHhaNRp" },
			{ "@DBABE271","SHADOW_SCM" },
			{ "@E046FE94","PBB-CIra3sN" },
			{ "@E07516A0","PBB-AlrdpeIlaIra3hN" },
			{ "@E191C0B1","PBB-AGb" },
			{ "@E19D68C7","PBAS-ACCsHhaNRpScm" },
			{ "@E1E0B10F","PBB-CEmGbLrN" },
			{ "@E327CC3E","PBBGRASS2-GbN" },
			{ "@E4FB71EF","PBB-CGb" },
			{ "@E5ED4A1F","SNOWCOVER" },
			{ "@E7B22E31","PBB-AIriNSss" },
			{ "@EA284541","PBB-ANSmSss" },
			{ "@EA53CA17","PBB-AEmGfIriN" },
			{ "@EAD00D47","PBAB-ACGfIriN" },
			{ "@EAEAB9E4","PBB-ACEmGfIriN" },
			{ "@EF38C5B7","PBB-AEmGbN" },
			{ "@EFDD9822","PBAB-AHhaNRp" },
			{ "@F0615241","PBB-ACN" },
			{ "@F0A49878","PBAS-ACGbN" },
			{ "@F2885BD3","PBSRIVER-Gb" },
			{ "@F2A276F5","PBB-CEmGbN" },
			{ "@F2E62E76","PBB-AlrsmIra3Sn" },
			{ "@F316FD15","PBB-GbIra3N" },
			{ "@F343F325","PBAB-AGbGfNRp" },
			{ "@F3E63C01","PBB-RSM" },
			{ "@F5BB85C5","PBAB-AHhaNRpScm" },
			{ "@F65B5716","PBB-EmLm" },
			{ "@F70EFD9C","PBB-ACGfIriN" },
			{ "@F88FBC51","PBBTRBR-N" },
			{ "@F9476603","PBBGRASS2-N" },
			{ "@FB33962D","PBAB-CRSM" },
			{ "@FB3C2F5F","PBB-AGfIriN" },
			{ "@FB710CC3","PBAS-ACsHhaNRp" },
			{ "@FB7A0729","PBB-NT0Tn" },
			{ "@FC2325BE","PBAS-CRSM" },
			{ "@FD1607E4","KTGLTerrainShaderGb" },
			{ "@FE5EC175","PBB-Em" },
			{ "@FE5FEE25","STD-Em" },
			{ "@FE652922","PBB-EmGbN" },
			{ "@FEF217D8","SMTRCP" }
		};
		#endregion
	}
}

namespace U3D
{
	
}

namespace DAZ
{
	
}

namespace UE
{
	
}

#region G1Headers

public struct MGs1
{
	public Color Cdiffuse;
	public float f4;
	public float f5;
	public float f6;
	
	public int i7;
	
	public float f8;
	public float f9;
	public float f10;
	public float f11;
	public float f12;
	public float f13;
	public float f14;
	public short s15_0;
	public short s15_1;
	
}

public struct MGs8
{
	public uint flag;	// if 3D, not /3, but-0xE
	public int vtx_fmt_id;	//s5
	public int BoneSkin_id;
	//s6
	public int Color_id;
	//s1
	public int unk3;
	public int ShaderParam_id;
	//s3
	public int tex_id;
	//s2
	
	public int idx_id;
	//s7
	public int isEnabled;
	public int DrawMode;
	
	public int Vert_Start;
	public int Vert_Count;
	public int Idx_Start;
	public int Idx_Count;
}

public struct G1H_C
{
	public G1Tag sig;
	public uint ver;
	public int size;
}


public struct G1MGH_C
{
	public uint sig;
	public int size;
	public int num;
}

public struct G1MMH
{
	public G1H_C hd;
	public int nMtx;
}

public struct G1MH
{
	public G1H_C hd;
	
	public int HeaderLen;
	public int unk;
	public int nComponent;
}

public struct MGs6
{
	public uint MMid;
	public uint flag;
	public int MSid;
}

public struct MGs9_Header
{
	public int i0;
	public int id;
	public int i2;
	public int subz1;
	public int subz2;
	public int i5;
	public int i6y;
	public int i7y;
	public int i8;
}

public struct MGs9_Element
{
	public unsafe fixed byte bName[0x10];
	public short s40;
	public short s41;
	public uint i5;
	public uint subz;
	public unsafe string Name(uint i)
	{
		
		fixed(byte* hyy = bName) {
			return Encoding.ASCII.GetString(hyy, 10).Replace("\0", string.Empty);
		}
	}
}

public struct G1MSH
{
	public G1H_C hd;

	public int HeaderLen;
	public uint unkflag;
	public ushort nUseBone;
	public ushort nAllBone;
	public ushort nXtraBone;
	public ushort unk;
}

public struct G1MGH
{
	public G1H_C hd;
	public uint graphsig;
	public uint graphsig2;
	public Vector3 innBound;
	public Vector3 outBound;
	public int nSection;
	
}

public struct G1MFH
{
	public G1H_C hd;
	
	public int platform;
	
	public int nUseBone;
	
	public int i5;
	
	public int nMat;
	
	public int nS1;
	public int nS2;
	public int nS3;
	
	public int i10;
	//yellow
	public int i11;
	public int i12;
	//yellow


	public int nS4;
	public int nS5_real;
	public int nS5;

	public int nS6;

	public int nS6_mtx;
	
	public int nS7;
	public int nS8;
	public int nS8_2;

	public int nS9;
	public int nS9_allshader;
	//pink
	public int nS9_allrender;

	public int nColl_alls2;
	public int nColl_alls3;

	public int nNUNO_2_3;

	public int nNUNO_m;
	public int nNUNO_n;
	public int nNUNO_o;

	public int nNUNO_p;
	public int nNUNO_q;
	public int nNUNO_r;
	public int nNUNO_s;

	public int nNUNO_sum_above;

	public int nNUNV_m;

	public int nNUNV_1_5;

	public int nNUNV_n;
	public int nNUNV_o;
	public int nNUNV_p;
}

public struct D3Dprop
{
	public D3DDECLTYPE valuetype;
	public D3DDECLUSAGE usage;
	public byte idx;
}

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FVF
{
	[FieldOffset(0)] public ushort src;
	[FieldOffset(2)] public ushort offset;
	[FieldOffset(4)] public uint D3Dsig1;
	[FieldOffset(4)] public D3Dprop D3Dsig2;
	[FieldOffset(4)] public D3DDECLTYPE valuetype;
	[FieldOffset(6)] public D3DDECLUSAGE usage;
	[FieldOffset(7)] public byte idx;
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct Vector4to3
{
	[FieldOffset(0)] public Vector3 v3;
	[FieldOffset(0)] public Vector4 v4;
	[FieldOffset(0xC)] public float w;
	
	public override string ToString()
	{
		return v4.ToString();
	}
}

public enum G1Tag : uint
{
	MF = 0x47314d46,
	MS = 0x47314d53,
	MM = 0x47314d4d,
	MG = 0x47314d47,
	COLL = 0x434f4c4c,
	NUNO = 0x4e554e4f,
	NUNV = 0x4e554e56,
	EXTR = 0x45585452,
	G1M = 0x47314d5f,
	G1T = 0x47315447,
	
	
}

public enum D3DDECLTYPE : ushort
{
	FLOAT1 = 0,
	FLOAT2 = 1,
	FLOAT3 = 2,
	FLOAT4 = 3,
	D3DCOLOR = 4,
	UBYTE4 = 5,
	SHORT2 = 6,
	SHORT4 = 7,
	UBYTE4N = 8,
	SHORT2N = 9,
	SHORT4N = 10,
	USHORT2N = 11,
	USHORT4N = 12,
	UDEC3 = 13,
	DEC3N = 14,
	FLOAT16_2 = 15,
	FLOAT16_4 = 16,
	UNUSED = 17
}

public enum D3DDECLUSAGE : byte
{
	POSITION = 0,
	BLENDWEIGHT = 1,
	BLENDINDICES = 2,
	NORMAL = 3,
	PSIZE = 4,
	TEXCOORD = 5,
	//idx5
	TANGENT = 6,
	BINORMAL = 7,
	TESSFACTOR = 8,
	POSITIONT = 9,
	COLOR = 10,
	//idx1
	FOG = 11,
	DEPTH = 12,
	SAMPLE = 13
}



#endregion

public class Varray
{
	public FVF fvf;
	public unsafe uint* pArray;
	public int dsize;
		
	public virtual string GetStr(int idx)
	{
		return string.Empty;
	}
		
}
	
public class Varray4U : Varray
{
	public bswap[] arr;
	public unsafe Varray4U(int size, FVF infvf)
	{
		fvf = infvf;
		if (infvf.usage == D3DDECLUSAGE.COLOR)
			isHex = true;
			
		dsize = 1;
		arr = new bswap[size];
		fixed(bswap* srcbb = arr) {
			pArray = (uint*)srcbb;
				
		}
	}
		
	bool isHex = false;
		
	public override string GetStr(int idx)
	{
		if (isHex)
			return $"{arr[idx].a:X2}{arr[idx].b:X2}{arr[idx].c:X2}{arr[idx].d:X2}";
		else
			return arr[idx].ToString();
	}
}
	
public class Varray2F : Varray
{
	public Vector2[] arr;
	public unsafe Varray2F(int size, FVF infvf)
	{
		fvf = infvf;
		dsize = 2;
		arr = new Vector2[size];
		fixed(Vector2* srcbb = arr) {
			pArray = (uint*)srcbb;
				
		}
	}
		
	public override string GetStr(int idx)
	{
		return arr[idx].ToString();
	}
}
	
public class Varray3F : Varray
{
	public Vector3[] arr;
	public unsafe Varray3F(int size, FVF infvf)
	{
		fvf = infvf;
		dsize = 3;
		arr = new Vector3[size];
		fixed(Vector3* srcbb = arr) {
			pArray = (uint*)srcbb;
				
		}
	}
		
	public override string GetStr(int idx)
	{
		return arr[idx].ToString();
	}
}
	
public class Varray4F : Varray
{
	public Vector4to3[] arr;
	public unsafe Varray4F(int size, FVF infvf)
	{
		fvf = infvf;
		dsize = 4;
		arr = new Vector4to3[size];
		fixed(Vector4to3* srcbb = arr) {
			pArray = (uint*)srcbb;
				
		}
	}
		
	public override string GetStr(int idx)
	{
		return arr[idx].ToString();
	}
}

[StructLayout(LayoutKind.Explicit, Size = 4)]
public unsafe struct bswap
{
	[FieldOffset(0)] public fixed byte Text[4];
	[FieldOffset(0)] public int thewholeS;
	[FieldOffset(0)] public uint thewhole;
	[FieldOffset(0)] public float thewholef;
	[FieldOffset(0)] public short s0S;
	[FieldOffset(0)] public ushort s0;
	[FieldOffset(0)] public byte a;
	[FieldOffset(1)] public byte b;

	[FieldOffset(2)] public ushort s1S;
	[FieldOffset(2)] public ushort s1;
	[FieldOffset(2)] public byte c;
	[FieldOffset(3)] public byte d;
	

	public override string ToString()
	{
		return String.Format("{0}, {1}, {2}, {3}", new object[] {
			a,
			b,
			c,
			d,
		});
	}
	
	
	public int _1(int i)
	{
		thewholeS = i;
		/*
		unchecked {
			return
    	(int)(((thewholeS & 0xff000000) >> 24) |
			((thewholeS & 0x00ff0000) >> 8) |
			((thewholeS & 0x0000ff00) << 8) |
			((thewholeS & 0x000000ff) << 24));
		}
		 */
		
		byte ty = a;
		a = d;
		d = ty;
		ty = b;
		b = c;
		c = ty;

		return thewholeS;
		

	}
	
	public uint _1(uint i)
	{
		thewhole = i;
		
		
		byte ty = a;
		a = d;
		d = ty;
		ty = b;
		b = c;
		c = ty;

		return thewhole;
		

	}
	
	public int _2(int i)
	{
		thewholeS = i;
		
		
		byte ty = a;
		a = b;
		b = ty;
		ty = c;
		c = d;
		d = ty;

		return thewholeS;
		

	}
	
	public uint _2(uint i)
	{
		thewhole = i;
		
		
		byte ty = a;
		a = b;
		b = ty;
		ty = c;
		c = d;
		d = ty;


		return thewhole;
		

	}
	
	public int _2fix(int i)
	{
		thewholeS = i;
		
		
		ushort ty = s0;
		s0 = s1;
		s1 = ty;

		return thewholeS;
		

	}
	
	public uint _2fix(uint i)
	{
		thewhole = i;
		
		
		ushort ty = s0;
		s0 = s1;
		s1 = ty;


		return thewhole;
		

	}
	
	public void _2fix()
	{
		ushort ty = s0;
		s0 = s1;
		s1 = ty;
	}
	
	public float _1(float f)
	{
		thewholef = f;
		byte ty = a;
		a = d;
		d = ty;
		ty = b;
		b = c;
		c = ty;
		return thewholef;
	}

	public void _1()
	{
		
		byte ty = a;
		a = d;
		d = ty;
		ty = b;
		b = c;
		c = ty;
		
	}
	
	public void _s1()
	{
		
		byte ty = c;
		c=d;
		d=ty;
		
	}
	
	public void _s0()
	{
		
		byte ty = a;
		a=b;
		b=ty;
		
	}

	public ushort _1(ushort us)
	{
		s0 = us;
		byte ty = a;
		a = b;
		b = ty;
		return s0;
	}
	
	public short _1(short s)
	{
		s0S = s;
		byte ty = a;
		a = b;
		b = ty;
		return s0S;
	}
	
	public unsafe string _4char(uint i)
	{
		thewhole = i;
		fixed(byte* hyy = Text) {
			return Encoding.ASCII.GetString(hyy, 4);
		}
	}

}

