

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Lz4;
using U3D.Class;


namespace U3D
{
	public class uObject
	{
		public uMeta meta;
		
		
		
		public virtual void Load(byte[] buf)
		{
			UnityEngine.Debug.Log(this.GetType().Name+" not load");
		}
	}
	
	
	
	public class PackedFiles
	{
		public ulong Offset;
		public ulong Size;
		public uint flags;
		public string Name;
	}
	
	public class ABheader
	{
		public string signature;
		public uint version;
		public string VerLow;
		public string VerUp;
		public long size;
		public int compressedBlocksInfoSize;
		//public int uncompressedBlocksInfoSize;
		//public uint flags;
		public int ABHsize;
		public bool Compressed;
		public ulong resSraw;
		
		
		public CompressedBlocks[] compressedblocks;
		public PackedFiles[] Files;
		public Stream CachedReader;
		
		public ABheader(Stream reader)
		{
			CachedReader = reader;
			reader.Position = 0;
			byte[] buf = reader.ReadBytes(0x40);
			
			ReaderBE rbr = new ReaderBE(buf);
			
			signature = rbr.rStr();
			version = rbr.ru4c();
			VerLow = rbr.rStr();
			VerUp = rbr.rStr();
			size = rbr.ri8c();
			compressedBlocksInfoSize = rbr.ri4c();
			int uncompressedBlocksInfoSize = rbr.ri4c();
			uint flags = rbr.ru4c();
			
			int toBlo = rbr.seek;
			ABHsize = toBlo + compressedBlocksInfoSize;
			rbr.Clear();
			
			
			reader.Position = 0;
			buf = reader.ReadBytes(ABHsize);
			rbr.Reload(buf);
			rbr.seek = toBlo;
			
			ReFillrbr(rbr,uncompressedBlocksInfoSize,flags);
			
			if(compressedblocks!=null)
			{
				ABHsize = 0;
				Compressed = true;
				CachedReader.Close();
				//CachedReader = new memreader~
				
			}
			
			
			if (Files.Length==2) {
				
				resSraw = Files[1].Offset;
				
			}
			
			//reader.Position = ABHsize;
			
			//if ram, ABHsize=0;
		}
		
		unsafe void ReFillrbr(ReaderBE rbr, int uncompsize, uint flags)
		{
			byte[] buf;
			switch (flags & 0x3F) {
				default: //None
						
					break;
				case 1:
					buf = new byte[uncompsize];
					break;
				case 2: //LZ4
				case 3: //LZ4HC
					buf = new byte[uncompsize];
					using (var decoder = new Lz4DecoderStream(new MemoryStream(rbr.Bas, rbr.seek, rbr.Bas.Length - rbr.seek)))
						decoder.Read(buf, 0, uncompsize);
                        
					rbr.Reload(buf);
                       	
					break;
                    
			}
			
			rbr.seek += 0x10; //uncompressedDataHash
			int blocksInfoCount = rbr.ri4c();
			CompressedBlocks cb0 = *(CompressedBlocks*)rbr.ReadTc(0);
			if (blocksInfoCount == 1 && cb0.compressedSize == cb0.uncompressedSize) {
				
			} else {
				compressedblocks = new CompressedBlocks[blocksInfoCount];
				fixed(byte* srcbb = &rbr.Bas[rbr.seek]) {
					for (int i = 0; i < blocksInfoCount; i++) {
					
						compressedblocks[i]=*(CompressedBlocks*)(srcbb+0xA*i);
						
					
					}
				}
			}
			
			rbr.seek+=blocksInfoCount*0xA;
			
			blocksInfoCount = rbr.ri4c();
			
			Files = new PackedFiles[blocksInfoCount];
			for (int i = 0; i < blocksInfoCount; i++) {
				Files[i] = new PackedFiles
				{
					Offset = rbr.ru8c(),
					Size = rbr.ru8c(),
					flags = rbr.ru4c(),
					Name = rbr.rStr()
					
				};
			}
			
			rbr.Clear();
				
		}
		
		
		
		public Stream reNewReader()
		{
			var v = CachedReader;
			CachedReader = null;
			return v;
		}
	}
	
	public class SerializedFileHeader
	{
		public uint m_MetadataSize;
		public ulong m_FileSize;
		public uint m_Version;
		public long m_DataOffset;
		public bswap m_Endianess;
		
		public bswap8 v16h_0;
		public bswap v16h_C;
		public bswap v16h_28;
		public bswap v16h_2C;
	}
	
	public class SerializedType
	{
		public ClassIDType classID;
		public bool m_IsStrippedType;
		public short m_ScriptTypeIndex = -1;
		public List<TypeTreeNode> m_Nodes;
		public yGUID m_ScriptID;
		//Hash128
		public yGUID m_OldTypeHash;
		//Hash128
		public int[] m_TypeDependencies;
		
		public override string ToString()
		{
			return classID.ToString();
		}
        
        
	}
	
	public class TypeTreeNode
	{
		//public string m_Type;
		//public string m_Name;
		public TypeInfo info;
		

		public TypeTreeNode()
		{
		}

		public TypeTreeNode(string type, string name, int level, bool align)
		{
			
			info.m_Level = (byte)level;
			info.m_MetaFlag = align ? (uint)0x4000 : 0;
		}
		
		const string ind = "\t";
		const string spe = " ";
		
		public override string ToString()
		{
			int lel = info.m_Level;
			string[] kalo = new string[lel+4];
			for(int i=0;i<lel;i++)
				kalo[i]=ind;
			
			kalo[lel]=Dics.GetString(info.m_TypeStr);
			kalo[lel+1] = spe;
			kalo[lel+2] = Dics.GetString(info.m_NameStr);
			kalo[lel+3] = info.m_IsArray ? "[]":string.Empty;
			
			
			return string.Concat(kalo);
		}

	}
	
	
	
	public class SerializedFile
	{
		public SerializedFileHeader Header;
		public bswap Version;
		public BuildTarget m_TargetPlatform;
		public bool m_EnableTypeTree;
		
		
		public ABheader isAB;
		
		public int[] rStart = new int[2];
		public int Linked_Type;
		public yGUID Linked_GUID;
		public string Linked_str;
		public LocalSerializedObjectIdentifierStruct[] m_ScriptTypes;
		public SerializedType[] m_Types;
		public SerializedFile[] Dependency;
		public List<uMeta> Content = new List<uMeta>();
		public Dictionary<long,int> UID2ORD = new Dictionary<long, int>();
		
		
		
		
		public SerializedFile()
		{
			
		}
		
		public SerializedFile(Stream reader, SerializedFileHeader Head, ABheader ab)
		{
			Load(reader, Head, ab);
			
		}
		
		public unsafe void Load(Stream reader, SerializedFileHeader Head, ABheader ab)
		{
			isAB = ab;
			Header = Head;
			
			long cur = rStart[0] = (int)reader.Position;
			
			byte[] buf = reader.ReadBytes(0x10);
			
			string VersionString;
			cur += buf.strlen(out VersionString) + 1;
			VerParse(VersionString);
			
			reader.Position = cur;
			
			MetaParse(reader, Header.m_Endianess.a != 0);
			
			foreach(var oo in Content)
			{
				oo.Load(reader);
			}
			
			reader.Close();
		}
		
		void VerParse(string ver)
		{
			string[] spe = ver.Split(versplit, StringSplitOptions.RemoveEmptyEntries);
			var vmain = int.Parse(spe[0]);
			if (vmain > 2010)
				vmain -= 2010;
			Version.a = (byte)vmain;
			Version.b = byte.Parse(spe[1]);
			Version.c = byte.Parse(spe[2]);
			Version.d = byte.Parse(spe[3]);
			
		}
		
		static char[] versplit = { '.', 'p', 'f', 'b' };
		//static bswap bswapy;
		unsafe void MetaParse(Stream reader, bool isBE)
		{
			
			int cur = 5;
			byte[] buf = reader.ReadBytes(cur);
			
			ReaderLE drd;
			if (isBE)
				drd = new ReaderBE(buf);
			else
				drd = new ReaderLE(buf);
			
			if (Header.m_Version >= 8) {
				cur -= 4;
				m_TargetPlatform = (BuildTarget)drd.ri4c();
                
			}
			if (Header.m_Version >= 13) {
				cur -= 1;
				m_EnableTypeTree = (buf[4] > 0);
			}
            
			if (cur != 0)
				reader.Position -= cur;
            
			cur = 0;
			buf = reader.ReadBytes((int)(Header.m_MetadataSize + rStart[0] - reader.Position));
            
			fixed(byte* srcbb = &buf[0]) {
				drd.Reload(srcbb);
            	
				int typCount = drd.ri4cp();
				m_Types = new SerializedType[typCount];
				for (int i = 0; i < typCount; i++) {
					m_Types[i] = TypeParse(drd);
				}
				
				
				int bigIDEnabled = 0;
				if (Header.m_Version >= 7 && Header.m_Version < 14) {
					bigIDEnabled = drd.ri4cp();
				}
				
				int objectCount = drd.ri4cp();
				
				rStart[1] = (int)(reader.Position - buf.Length - rStart[0] + drd.seek);  
				cur = Extensionnyaa.mod4less(rStart[1]);
				drd.seek += cur;
				rStart[1] += cur; // rstart[0]+rstart[1] = abs offset to metas
				
				long vprs = Header.m_DataOffset;
				
				if (isAB != null) {
					vprs += isAB.ABHsize;
				}
				
				int syz = 0x14;
				cur = 0;
				if (Header.m_Version >= 22) {
					
					syz = 0x18;
					cur = 2; //u20
				} else if (Header.m_Version < 16) {
					cur = 1; //u4
				}
				
				byte* du = drd.ReadTcp(0);
				if (cur == 0) {
					for (int i = 0; i < objectCount; i++) {
						
						uMetaStruct ynfo = *(uMetaStruct*)du;
						
						var meta = GetNewuMeta(ynfo.PathID);
						
						meta.Offset = ynfo.Offset + vprs;
						meta.Size = ynfo.Size;
						meta.MB_type = m_Types[ynfo.TypeID];
						meta.ClassID = meta.MB_type.classID;
						
						du += syz;
						
					}
				} else {
					for (int i = 0; i < objectCount; i++) {
						
						uMetaStruct ynfo = *(uMetaStruct*)du;
						
						
						if (cur == 1) {
							var meta = GetNewuMeta((long)ynfo.PathID_u4);
							
							
							meta.Offset = ynfo.Offset_u4 + vprs;
							meta.Size = ynfo.Size_u4;
							//meta.MB_type = m_Types[ynfo.TypeID];
							meta.ClassID = (ClassIDType)ynfo.ClassID_u4;
						} else if (cur == 2) {
							var meta = GetNewuMeta(ynfo.PathID);
							meta.Offset = ynfo.Offset_u20 + vprs;
							meta.Size = ynfo.Size_u20;
							meta.MB_type = m_Types[ynfo.TypeID_u20];
							meta.ClassID = meta.MB_type.classID;
						}
						
						
						du += syz;
						
					}
				}
				
				drd.seek += syz * objectCount;
				
				if (Header.m_Version >= 11) {
					syz = 0xc;
					objectCount = drd.ri4cp();
					m_ScriptTypes = new LocalSerializedObjectIdentifierStruct[objectCount];
					for (int i = 0; i < objectCount; i++) {
						m_ScriptTypes[i] = *(LocalSerializedObjectIdentifierStruct*)drd.ReadTcp(syz);
					}
				 	
					drd.seek += syz * objectCount;
				 	
				}
				
				objectCount = drd.ri4cp();
				
				Dependency = new SerializedFile[objectCount];
				
				for (int i = 0; i < objectCount; i++) {
					string nzstr = drd.rStrp();
					yGUID yg = *(yGUID*)drd.ReadTcp(16);
					int tpp = drd.ri4cp();
					SerializedFile dp = MakeDependency(drd.rStrp());
					
					dp.setLinked(nzstr, tpp, yg);
					
					Dependency[i] = dp;
				}
				
			}
            
			
            

		}
		
		public void setLinked(string str, int Type, yGUID guid)
		{
			if (str != string.Empty || Type != 0 || guid.g0.blong != 0) {
				UnityEngine.Debug.Log("non empty dep");
			}
			Linked_str = str;
			Linked_Type = Type;
			Linked_GUID = guid;
			
		}
		
		SerializedFile MakeDependency(string key)
		{
			key = key.Replace("library/", "resources/").ToLower();
			SerializedFile sf;
			if (UnityAsset.Assets.TryGetValue(key, out sf)) {
				return sf;
			} else {
				sf = new SerializedFile();
				UnityAsset.Assets[key] = sf;
				return sf;
			}
		}
		
		uMeta GetNewuMeta(long pid)
		{
			int rett;
			if (UID2ORD.TryGetValue(pid, out rett)) {
				return Content[rett];
			} else {
				var nyme = new uMeta(this);
				UID2ORD[pid] = Content.Count;
				Content.Add(nyme);
				return nyme;
			}
		}
		
		unsafe SerializedType TypeParse(ReaderLE drd)
		{
			var type = new SerializedType();
			
			type.classID = (ClassIDType)drd.ri4cp();
			
			if (Header.m_Version >= 16) {
				type.m_IsStrippedType = (drd.r1p() == 1);
			}

			if (Header.m_Version >= 17) {
				type.m_ScriptTypeIndex = drd.ri2cp();
			}

			if (Header.m_Version >= 13) {
				if ((Header.m_Version < 16 && type.classID < 0) || (Header.m_Version >= 16 && type.classID == ClassIDType.MonoBehaviour)) {
					type.m_ScriptID = *(yGUID*)drd.ReadTcp(16); //Hash128
				}
				type.m_OldTypeHash = *(yGUID*)drd.ReadTcp(16); //Hash128
			}
			
			if (m_EnableTypeTree) {
				var typeTree = new List<TypeTreeNode>();
				if (Header.m_Version >= 12 || Header.m_Version == 10) {
					TypeTreeBlobRead(typeTree, drd);
				} else {
					//ReadTypeTree(typeTree);
				}

				if (Header.m_Version >= 21) {
					type.m_TypeDependencies = drd.ReadNArraycp<int>(4);
				}

				type.m_Nodes = typeTree;
			}
			
			return type;

		}
		
		unsafe void TypeTreeBlobRead(List<TypeTreeNode> typeTree, ReaderLE drd)
		{
			uint numberOfNodes = drd.ru4cp();
			int stringBufferSize = drd.ri4cp();
            
            
			byte* pTy = drd.CurPtr();
            
			if (drd.isBE) {
				
			}
			
			uint siz = 0x18;
			if (Header.m_Version >= 19) {
				siz = 0x20;
			}
			
			drd.seek += (int)(siz * numberOfNodes);
			
			for (uint i = 0; i < numberOfNodes; i++) {
				
				
				
				var typeTreeNode = new TypeTreeNode();
				typeTree.Add(typeTreeNode);
				typeTreeNode.info = *(TypeInfo*)(pTy + siz * i);
                
				typeTreeNode.info.m_TypeStr = TypeInfoStr(typeTreeNode.info.m_TypeStr, drd);
				typeTreeNode.info.m_NameStr = TypeInfoStr(typeTreeNode.info.m_NameStr, drd);
				
			}
			
			drd.seek += stringBufferSize;
			

            
		}
		
		int TypeInfoStr(int offset, ReaderLE drd)
		{
			if (offset < 0)
				return offset & 0x7fffffff;
			else
				return Dics.PutUserStr(drd.rStrp(offset));
			
				
		}
		
		
		
		public static unsafe SerializedFile Create(string GameBase, string asset)
		{
			string path = asset;
			if (GameBase != null)
				path = GameBase + '\\' + asset;
			else if (UnityYAMLAsset.GameBase != null)
				path = UnityYAMLAsset.GameBase + '\\' + asset;
			
			SerializedFile sf = null;
			return Create(path, sf);
		}
		
		public static unsafe SerializedFile Create(string path, SerializedFile src)
		{
			
			
			
			Stream reader = File.OpenRead(path);
			
			if (reader.Length < 0x14) {
				reader.Close();
				return null;
				
			}
			
			byte[] buf = reader.ReadBytes(0x30, 0, 0x14);
			
			ABheader abhh = null;
			
			if (buf.ru4(0) == 0x74696e55) {
				abhh = new ABheader(reader);
				reader = abhh.reNewReader();
				
				buf = reader.ReadBytes(0x30, 0, 0x14);
			}
			
			var tmpHeader = new SerializedFileHeader();
			
			fixed(byte* srcbb = &buf[0]) {
				bswap* bs = (bswap*)srcbb;
				uint ver = bs[2]._1u();
				tmpHeader.m_Version = ver;
				tmpHeader.m_Endianess = bs[4];
				if (ver >= 0x16) {
					reader.Read(buf, 0x14, 0x1C);
					bswap8* bs8 = (bswap8*)srcbb;
					
					tmpHeader.v16h_0 = bs8[0];
					tmpHeader.v16h_C = bs[3];
					
					tmpHeader.m_MetadataSize = bs[5]._1u();
					tmpHeader.m_FileSize = bs8[3]._1u();
					tmpHeader.m_DataOffset = bs8[4]._1i();
					
					tmpHeader.v16h_28 = bs[10];
					tmpHeader.v16h_2C = bs[11];
					
				} else {
					tmpHeader.m_MetadataSize = bs[0]._1u();
					tmpHeader.m_FileSize = bs[1]._1u();
					
					tmpHeader.m_DataOffset = bs[3]._1u();
					if (ver < 9) {
						
					}
					
				}

			}
			long cur = reader.Length;
			
			if (tmpHeader.m_MetadataSize >= cur || tmpHeader.m_DataOffset >= cur)
				return null;
			
			
			//if(abhh.resSraw<0)
			//	abhh.resSloc(tmpHeader.m_FileSize);
			
			
			if (src == null)
				return new SerializedFile(reader, tmpHeader, abhh);
			else {
				src.Load(reader, tmpHeader, abhh);
				return src;
			}
			
			
			
		}
		
	}
	
	



	public static class UnityYAMLAsset
	{
		public static Dictionary<string,YAMLfile> Guid2File = new Dictionary<string,YAMLfile>();
		public static string GameBase;
		
		public static yBlock LookUpBlock(string fidstr, Dictionary<ulong,yBlock> LookUp, yObject sender)
		{
			yBlock yy;
			ulong fid = ulong.Parse(fidstr.Trim());
			
			if (fid == 0)
				return yBlock.fZero;
			
			if (!LookUp.TryGetValue(fid, out yy)) {
				yy = new yBlock(string.Empty);
				LookUp[fid] = yy;
				
			}
			
			yy.refs.Add(sender);
			return yy;
				
		}
		
		public static YAMLfile LookUpFile(string[] info, yObject sender)
		{
			info[1] = info[1].Trim();
			string ky = info[3] = info[3].Trim();
			info[5] = info[5].Trim();
			
			
			
			YAMLfile yy;
			if (Guid2File.TryGetValue(ky, out yy)) {
				
				int cy = int.Parse(info[1]);
				
				if (yy.catalog < 0)
					yy.catalog = cy;
				else if (yy.catalog != cy)
					UnityEngine.Debug.Log($"CurCatalog={cy}, OrigCatalog={yy.catalog}, dummy={yy.isDummy}");
				
				cy = int.Parse(info[5]);
				
				if (yy.usage < 0)
					yy.usage = cy;
				else if (yy.usage != cy)
					UnityEngine.Debug.Log($"CurUse={cy}, OrigUse={yy.usage}, dummy={yy.isDummy}");
					
			} else {
				yy = new YAMLfile(info);
				
			}
			
			yy.refs.Add(sender);
			return yy;
				
		}
		
		
	}
	
	public static class UnityAsset
	{
		public static Dictionary<string,SerializedFile> Assets = new Dictionary<string,SerializedFile>();
		public static string GameBase;
		
		public static string[] StaticRes = {
			"/resources/unity default resources",
			"/resources/unity_builtin_extra"
		};
		
		public static void Load(string root)
		{
			GameBase = root;
			string[] assfiz = Directory.GetFiles(root, "*", SearchOption.TopDirectoryOnly);
			
			int lyro = root.Length + 1;
			
			
			
			
			foreach (var path in assfiz) {
				chkr(path, path.Substring(lyro).ToLower());
				
			}
			
			foreach (var dpath in StaticRes) {
				chkr(root + dpath, dpath.Substring(1));
				
			}
			
			
		}
		
		static void chkr(string path, string pathkey)
		{
			
			SerializedFile sery = null;
			if (Assets.TryGetValue(pathkey, out sery)) {
				if(sery.Header == null)
				{
					SerializedFile.Create(path, sery);
				}
				
			} else {
				sery = SerializedFile.Create(path, sery);
				if (sery != null)
					Assets[pathkey] = sery;
			}
		}
	}
	
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TypeInfo
	{
		public ushort m_Version;
		public byte m_Level;
		
		[MarshalAs(UnmanagedType.I1)] 
		public bool m_IsArray;
                
		public int m_TypeStr;
		public int m_NameStr;
		public int m_ByteSize;
		public int m_Index;
		public uint m_MetaFlag;
		
		public ulong m_RefTypeHash;
	}
	
	[StructLayout(LayoutKind.Explicit, Size = 0xA), Serializable]
	public struct CompressedBlocks
	{
		[FieldOffset(0)] public uint uncompressedSize;
		[FieldOffset(4)] public uint compressedSize;
		[FieldOffset(8)] public ushort flags;
	}
	
	[StructLayout(LayoutKind.Explicit, Size = 24), Serializable]
	public struct uMetaStruct
	{
		[FieldOffset(0)] public long PathID;
		[FieldOffset(0)] public int PathID_u4;
		[FieldOffset(4)] public int Offset_u4;
		[FieldOffset(8)] public int Offset;
		[FieldOffset(8)] public int Size_u4;
		[FieldOffset(8)] public long Offset_u20;
		[FieldOffset(12)] public int TypeID_u4;
		[FieldOffset(12)] public int Size;
		[FieldOffset(16)] public int TypeID;
		[FieldOffset(16)] public ushort ClassID_u4;
		[FieldOffset(16)] public int Size_u20;
		[FieldOffset(18)] public ushort Destroyed_u4;
		[FieldOffset(20)] public int TypeID_u20;
		
	}
	
	
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct LocalSerializedObjectIdentifierStruct
	{
		public int localSerializedFileIndex;
		public long localIdentifierInFile;
	}
	
	//=====================

	
	public class yObject
	{
		
		public yObjType yType;
		public yObject Parent;
		public int qnum;
	}
	
	public enum yObjType
	{
		File,
		Block,
		Element,
		Array,
		Multivale,
		Value,
	}
	
	public class yKlass :  yObject
	{
		public List<yObject> refs = new List<yObject>();
		
		public bool isDummy = false;
	}
	
	public class YAMLfile : yKlass
	{
		public static YAMLfile fZero = new YAMLfile();
		
		public yElement meta;
		public string Path;
		public Dictionary<ulong,yBlock> Blocks = new Dictionary<ulong, yBlock>();
		public int catalog = -1;
		public int usage = -1;
		public string TmpGuid;
		
		public static char[] kongge = { ' ', '&' };
		
		
		
		public YAMLfile()
		{
			yType = yObjType.File;
			isDummy = true;
		}
		
		public YAMLfile(string[] info)
		{
			yType = yObjType.File;
			isDummy = true;
			
			catalog = int.Parse(info[1]);
			TmpGuid = info[3];
			usage = int.Parse(info[5]);
		}
		
		public YAMLfile(string metapath, yElement metain, string Dummy)
		{
			
			LoadnonYAML(metapath, metain, Dummy);
		}
		
		public void LoadnonYAML(string metapath, yElement metain, string Dummy)
		{
			yType = yObjType.File;
			int len = UnityYAMLAsset.GameBase.Length + 1;
			Path = metapath.Substring(len, metapath.Length - len - 5);
			meta = metain;
			Blocks[0xFFFFFFFFFFFFFFFF] = new yBlock(Dummy);
		}
		
		public YAMLfile(string metapath, yElement metain, string[] txt)
		{
			yType = yObjType.File;
			Load(metapath, metain, txt);
			
		}
		
		public void Load(string metapath, yElement metain, string[] txt)
		{
			int len = UnityYAMLAsset.GameBase.Length + 1;
			
			Path = metapath.Substring(len, metapath.Length - len - 5);
			meta = metain;
			
			
			
			
			List<int> addrlist = new List<int>();
			List<ulong> idlist = new List<ulong>();
			len = txt.Length;
			for (int i = 2; i < len; i++) {
				if (txt[i].StartsWith("--- !u!")) {
					addrlist.Add(i);
					string[] paar = txt[i].Substring(7).Split(kongge, StringSplitOptions.RemoveEmptyEntries);
					ulong idd = ulong.Parse(paar[1]);
					idlist.Add(idd);
					Blocks[idd] = new yBlock(this, int.Parse(paar[0]), idd);
				}
			}
			
			addrlist.Add(len);
			len = addrlist.Count - 1;
			
			
			
			for (int i = 0; i < len; i++) {
				int start = addrlist[i];
				int end = addrlist[i + 1];
				
				
				
				Blocks[idlist[i]].Load(StringGroup.Parse(txt, start + 1, end), Blocks);
			}
			isDummy = false;
		}
		
		
		public static string[] nonYAML = {
			".png",
			".jpg",
			".tga",
			".shader",
			".cginc",
			".cg",
			".fbx",
			".cs"
		};
		
		public static YAMLfile Create(string path)
		{
			if (!File.Exists(path))
				path = UnityYAMLAsset.GameBase + '\\' + path;
			
			string azet = path.Substring(0, path.Length - 5).ToLower();
			
			if (!File.Exists(azet))
				return null;
			
			
			var tmpmeta = new yElement(StringGroup.Parse(File.ReadAllLines(path), 0, -1).substr, null, null, 0);
			bool isDm = false;
			string rtig = null;
			
			string[] txt = null;
			
			foreach (var tig in nonYAML) {
				if (azet.EndsWith(tig)) {
					isDm = true;
					rtig = tig;
					goto aftalo;
				}
			}
			
			
			
			
			
			
			txt = File.ReadAllLines(azet);
			
			if (!txt[0].StartsWith("%YAML"))
				return null;
			
			
			
			aftalo:
			
			azet = ((yValue)tmpmeta.Fields["guid"]).ToString();
			
			YAMLfile yaoi;
			if (!UnityYAMLAsset.Guid2File.TryGetValue(azet, out yaoi)) {
				if (isDm) {
					yaoi = new YAMLfile(path, tmpmeta, rtig);
				} else {
					yaoi = new YAMLfile(path, tmpmeta, txt);
				}
				
				UnityYAMLAsset.Guid2File[azet] = yaoi;
			}
			
			if (yaoi.isDummy) {
				if (isDm) {
					yaoi.LoadnonYAML(path, tmpmeta, rtig);
				} else {
					yaoi.Load(path, tmpmeta, txt);
				}
			}
				
			
			return yaoi;
			
			
			
		}
	}
	
	
	
	public class yBlock : yKlass
	{
		public YAMLfile BaseFile;
		public static yBlock fZero = new yBlock(string.Empty);
		
		public string ClassStr;
		public ClassIDType ClassID;
		public yElement content;
		public ulong LocalID;
		
		public yBlock(YAMLfile yfile, int klasid, ulong Lid)
		{
			yType = yObjType.Block;
			ClassID = (ClassIDType)klasid;
			BaseFile = yfile;
			LocalID = Lid;
		}
		
		public yBlock(string dummyStr)
		{
			ClassStr = dummyStr;
			yType = yObjType.Block;
			LocalID = 0xFFFFFFFFFFFFFFFF;
			isDummy = true;
		}
		
		public void Load(StringGroup sgp, Dictionary<ulong,yBlock> LookUp)
		{

			qnum = sgp.topstr_n;
			ClassStr = sgp.topstr;
			content = new yElement(sgp.substr, LookUp, this, sgp.topstr_n + 1);
		}
		
		public override string ToString()
		{
			return ClassStr;
		}
	}
	
	
	
	public class yElement : yObject
	{
		public const string indentx2 = "  ";
		public static char[] Maohao = { ':' };
		
		public static string mkindhead(int n)
		{
			string[] comb = new string[n];
			for (int i = 0; i < n; i++)
				comb[i] = indentx2;
			
			return String.Concat(comb);
		}
		
		public Dictionary<string,yObject> Fields = new Dictionary<string, yObject>();
		
		public static yObject mkLinks(string g, Dictionary<ulong,yBlock> LookUp, yObject sender)
		{
			string[] fyd = g.Split(yMultiValue.kuohao, StringSplitOptions.RemoveEmptyEntries);
			if (fyd.Length == 2) {
				return UnityYAMLAsset.LookUpBlock(fyd[1], LookUp, sender);
					
			} else if (fyd.Length == 6) {
				return UnityYAMLAsset.LookUpFile(fyd, sender);
			}
				
			return null;
		}
		
		public static yObject mkpurevalue(string g, Dictionary<ulong,yBlock> LookUp, yObject sender)
		{
			g = g.Trim();
			if (g == "{}") {
				return new yMultiValue();
			} else if (g == "[]") {
				return new yArray();
			} else if (g.StartsWith("{fileID:")) {
				return mkLinks(g, LookUp, sender);
						
			} else if (g.StartsWith("{")) {
				return new yMultiValue(g);
			} else {
				return new yValue(g);
			}
			
			
		}
		
		public string[] isArrayStr(StringGroup[] Zulist)
		{
			List<string> purevalues = new List<string>();
			foreach (var zuv in Zulist) {
				if (zuv.topstr[0] != '>')
					return null;
				
				if (zuv.substr.Length != 1)
					return null;
				
				
				purevalues.Add(zuv.substr[0].topstr.Trim());
			}
			
			return purevalues.ToArray();
		}
		
		public yElement(StringGroup[] sgplist, Dictionary<ulong,yBlock> LookUp, yObject sender, int qpos)
		{
			qnum = qpos;
			Parent = sender;
			yType = yObjType.Element;
			
			foreach (var sg in sgplist) {
				if (sg.substr == null) {
					var keypair = sg.topstr.Split(Maohao, 2, StringSplitOptions.RemoveEmptyEntries);
					
					if (keypair.Length == 1)
						Fields[keypair[0].Trim()] = null;
					else {
						var jj = mkpurevalue(keypair[1], LookUp, this);
						jj.qnum = sg.topstr_n;
						Fields[keypair[0].Trim()] = jj;
					}
						
				} else if (sg.substr[0].topstr[0] == '>') {
					string[] strarr = isArrayStr(sg.substr);
					if (strarr == null) {
						Fields[sg.topstr.Trim()] = new yArray(sg.substr, LookUp, this, sg.topstr_n);
					} else {
						Fields[sg.topstr.Trim()] = new yArray(strarr, LookUp, this);
					}
				} else {
					Fields[sg.topstr.Trim()] = new yElement(sg.substr, LookUp, this, sg.topstr_n);
				}
			}
		}
		
		
		
	}
	
	
	
	public class yArray: yObject
	{
		public string[] PureValueHead;
		public yObject[] array;
		
		public yArray()
		{
			yType = yObjType.Array;
			array = new yObject[0];
		}
		
		public yArray(StringGroup[] mixed, Dictionary<ulong,yBlock> LookUp, yObject sender, int qpos)
		{
			qnum = qpos;
			Parent = sender;
			yType = yObjType.Array;
			int yd = mixed.Length;
			array = new yObject[yd];
			
			for (int i = 0; i < yd; i++) {
				array[i] = new yElement(mixed[i].substr, LookUp, this, mixed[i].substr[0].topstr_n);
			}
		}
		

		
		public yArray(string[] pure, Dictionary<ulong,yBlock> LookUp, yObject sender)
		{
			Parent = sender;
			yType = yObjType.Array;
			string oldkey = string.Empty;
			int yd = pure.Length;
			bool nohead = true;
			PureValueHead = new string[yd];
			array = new yObject[yd];
			
			for (int i = 0; i < yd; i++) {
				if (pure[i].StartsWith("{fileID:")) {
					array[i] = yElement.mkLinks(pure[i], LookUp, this);
				} else if (pure[i].StartsWith("{")) {
					array[i] = new yMultiValue(pure[i]);
				} else if (pure[i].StartsWith("'")) {
					array[i] = new yValue(pure[i]);
				} else {
					var keypair = pure[i].Split(yElement.Maohao, 2, StringSplitOptions.RemoveEmptyEntries);
					
					if (keypair.Length == 2) {
						nohead = false;
						var ky = keypair[0].Trim();
						if (!String.Equals(oldkey, ky)) {
							oldkey = ky;
						}
						
						PureValueHead[i] = oldkey;
						
						
						array[i] = yElement.mkpurevalue(keypair[1], LookUp, this);
					} else {
						array[i] = new yValue(pure[i]);
					}
					
					
				}
			}
			
			if (nohead)
				PureValueHead = null;
		}
	}
	
	public class yValue: yObject
	{
		public string value;
		
		public yValue(string g)
		{
			yType = yObjType.Value;
			value = g;
		}
		
		public override string ToString()
		{
			return value;
		}

	}
	
	public class yMultiValue: yObject
	{
		public Dictionary<string,string> values;
		public static char[] kuohao = { '{', ':', ',', '}' };
		public yMultiValue()
		{
			yType = yObjType.Multivale;
		}
		
		public yMultiValue(string g)
		{
			yType = yObjType.Multivale;
			
			values = new Dictionary<string, string>();
			
			string[] paar = g.Split(kuohao, StringSplitOptions.RemoveEmptyEntries);
			if (paar.Length % 2 != 0)
				UnityEngine.Debug.Log("MultiValueBug");
			
			int len = paar.Length / 2;
			for (int i = 0; i < len; i++) {
				values[paar[i * 2].Trim()] = paar[i * 2 + 1].Trim();
			}
		}
	}
	
	
	
	
	

	public class qStringGroup
	{
		public int topstr;
		public List<qStringGroup> substr = new List<qStringGroup>();

		
		public bool isArrayHead;
		
		public qStringGroup parent;
		
		public qStringGroup(int init)
		{
			topstr = init;
		}
		
		
		
		
		public static bswap CountInd(string g)
		{
			bswap bs = new bswap();
			
			for (int i = 0; i < 256; i++) {
				if (g[i * 2] != ' ' || g[i * 2 + 1] != ' ') {
					
					
					if (g[i * 2] == '-') {
						bs.a = (byte)(i + 1);
						bs.b = 1;
						return bs;
					} else {
						bs.a = (byte)(i);
						return bs;
					}
				}
			}
			
			return bs;
		}
		public static qStringGroup GetNParent(qStringGroup sg, int n)
		{
			qStringGroup rt = sg;
			
			if (sg.isArrayHead)
				rt = sg.parent;

				
			
			for (int i = 0; i < n; i++) {
				rt = rt.parent;
				
			}
			return rt;
		}
		
		
		
		public static void setmoth(qStringGroup moth, qStringGroup nxsg)
		{
			if (moth.isArrayHead)
				nxsg.parent = moth.parent;
			else
				nxsg.parent = moth;
		}
		
		
		
		public static qStringGroup MakeZu(qStringGroup moth, qStringGroup nxsg)
		{
			var grphd = new qStringGroup(-1);
			grphd.isArrayHead = true;
			setmoth(moth, grphd);
						
			grphd.substr.Add(nxsg);
			setmoth(moth, nxsg);
						
			moth.substr.Add(grphd);
			return grphd;
		}
		
		
	}
	
	public class StringGroup
	{
		public string topstr;
		public int topstr_n;
		public StringGroup[] substr;
		
		public StringGroup(string[] g, int y)
		{
			topstr_n = y;
			if (y == -1) {
				topstr = ">";
				return;
			}
			if (y == -2) {
				topstr = "<";
				return;
			}
				
			
			
			topstr = g[y];
		}
		
		
		public StringGroup(string y)
		{
			topstr_n = -1;
			topstr = y;
		}
		
		public static int findprev(bool[] pos, int kk)
		{
			for (int i = kk; i >= 0; i--) {
				if (pos[i])
					return i;
			}
			return 0;
		}
		
		public static void fixqs(qStringGroup qs)
		{
			int y = qs.substr.Count;
			bool[] pos = new bool[y];
			List<qStringGroup> nyusub = new List<qStringGroup>();
			for (int i = 0; i < y; i++) {
				if (qs.substr[i].topstr == -1) {
					pos[i] = true;
					nyusub.Add(qs.substr[i]);
				}
			}
			
			for (int i = 0; i < y; i++) {
				var valu = qs.substr[i];
				if (valu.topstr != -1) {
					qs.substr[findprev(pos, i)].substr.Add(valu);
				}
			}
			
			qs.substr = nyusub;
			
		}
		
		public static void q2str(string[] g, qStringGroup qs, StringGroup sg)
		{
			
			int y = qs.substr.Count;
			if (y == 0)
				return;
			
			if (y > 1) {
				if (qs.substr[0].topstr == -1 && qs.substr[1].topstr != -1) {
					fixqs(qs);
					y = qs.substr.Count;
				}
			}
			
			
			sg.substr = new StringGroup[y];
			for (int i = 0; i < y; i++) {
				var nsg = new StringGroup(g, qs.substr[i].topstr);
				sg.substr[i] = nsg;
				q2str(g, qs.substr[i], nsg);
			}
		}
		
		
		public static StringGroup Parse(string[] g, int start, int endpos)
		{
			if (endpos < 0)
				endpos = g.Length;
			
			qStringGroup root = new qStringGroup(-2);
			qStringGroup moth = root;
			qStringGroup lastt = root;
			
			int curind = qStringGroup.CountInd(g[start]).a;
			for (int i = start; i < endpos; i++) {
				bswap nxindbs = qStringGroup.CountInd(g[i]);
				int nxind = nxindbs.a;
				
				g[i] = g[i].Substring(nxind * 2);
				qStringGroup nxsg = new qStringGroup(i);

				if (nxind > curind)
					moth = lastt;
				else if (nxind < curind)
					moth = qStringGroup.GetNParent(moth, curind - nxind);
				
				
				if (nxindbs.b == 1) {
					if (moth.isArrayHead)
						moth = moth.parent;
					
					moth = qStringGroup.MakeZu(moth, nxsg);
				} else {
					moth.substr.Add(nxsg);
					qStringGroup.setmoth(moth, nxsg);
				}
				
				
				
				
				lastt = nxsg;
				curind = nxind;
				
			}
			
			if (root.substr.Count == 1) {
				var vroot = root.substr[0];
				root.substr = null;
				root = vroot;
				root.parent = null;
			}
			
			StringGroup Realroot = new StringGroup(g, root.topstr);
			
			q2str(g, root, Realroot);
			
			return Realroot;
		}
		
		public override string ToString()
		{
			
			if (substr != null) {
				return topstr + "\t//" + substr.Length;
			} else {
				return topstr;
			}
			
			
		}

	}
	
	//propertyGrid1.SelectedObject = new JTypeDescriptor(JObject.Parse(File.ReadAllText("test.json")));
  
	/*	
public class yTypeDescriptor : ICustomTypeDescriptor
  {
      public yTypeDescriptor(yObject yobject)
      {
          if (yobject == null)
              throw new ArgumentNullException("jobject");

          yObject = yobject;
      }

      // NOTE: the property grid needs at least one r/w property otherwise it will not show properly in collection editors...
      public yObject yObject { get; set; }

      public override string ToString()
      {
          // we display this object's serialized json as the display name, for example
          return yObject.ToString();
      }

      PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
      {
          // browse the JObject and build a list of pseudo-properties
          List<PropertyDescriptor> props = new List<PropertyDescriptor>();
          foreach (var kv in JObject)
          {
              props.Add(new Prop(kv.Key, kv.Value, null));
          }
          return new PropertyDescriptorCollection(props.ToArray());
      }

      AttributeCollection ICustomTypeDescriptor.GetAttributes()
      {
          return null;
      }

      string ICustomTypeDescriptor.GetClassName()
      {
          return null;
      }

      string ICustomTypeDescriptor.GetComponentName()
      {
          return null;
      }

      TypeConverter ICustomTypeDescriptor.GetConverter()
      {
          return null;
      }

      EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
      {
          throw new NotImplementedException();
      }

      PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
      {
          return null;
      }

      object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
      {
          return null;
      }

      EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
      {
          throw new NotImplementedException();
      }

      EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
      {
          throw new NotImplementedException();
      }

      PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
      {
          return ((ICustomTypeDescriptor)this).GetProperties(null);
      }

      object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
      {
          return this;
      }

      // represents one dynamic pseudo-property
      private class Prop : PropertyDescriptor
      {
          private Type _type;
          private object _value;

          public Prop(string name, object value, Attribute[] attrs)
              : base(name, attrs)
          {
              _value = ComputeValue(value);
              _type = _value == null ? typeof(object) : _value.GetType();
          }

          private static object ComputeValue(object value)
          {
              if (value == null)
                  return null;

              JArray array = value as JArray;
              if (array != null)
              {
                  // we use the arraylist because that's all the property grid needs...
                  ArrayList list = new ArrayList();
                  for (int i = 0; i < array.Count; i++)
                  {
                      JObject subo = array[i] as JObject;
                      if (subo != null)
                      {
                          yTypeDescriptor td = new yTypeDescriptor(subo);
                          list.Add(td);
                      }
                      else
                      {
                          JValue jv = array[i] as JValue;
                          if (jv != null)
                          {
                              list.Add(jv.Value);
                          }
                          else
                          {
                              // etc.
                          }
                      }
                  }
                  // we don't support adding things
                  return ArrayList.ReadOnly(list);
              }
              else
              {
                  // etc.
              }
              return value;
          }

          public override bool CanResetValue(object component)
          {
              return false;
          }

          public override Type ComponentType
          {
              get { return typeof(object); }
          }

          public override object GetValue(object component)
          {
              return _value;
          }

          public override bool IsReadOnly
          {
              get { return false; }
          }

          public override Type PropertyType
          {
              get { return _type; }
          }

          public override void ResetValue(object component)
          {
          }

          public override void SetValue(object component, object value)
          {
              _value = value;
          }

          public override bool ShouldSerializeValue(object component)
          {
              return false;
          }
      }
  }
  */
}