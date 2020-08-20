

using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;



namespace U3D
{
	public class uObject
	{
		public uint Offset;
	}
	
	public class ABheader
	{
		
	}
	
	public class SerializedFileHeader
	{
		public uint m_MetadataSize;
		public long m_FileSize;
		public uint m_Version;
		public long m_DataOffset;
		public bswap m_Endianess;
		
		public bswap8 v16h_0;
		public bswap v16h_C;
		public bswap v16h_28;
		public bswap v16h_2C;
	}
	
	public class SerializedFile
	{
		public SerializedFileHeader Header = new SerializedFileHeader();
		public string VerionString;
		public Dictionary<ulong,int> UID2ORD = new Dictionary<ulong, int>();
		public uObject[] Content;
		public ABheader isAB;
		
		public unsafe SFtype ChkType(Stream reader)
		{
			
			
			bswap8 hyd = new bswap8(reader,true);
			
			//if(hyd._8char() == "UnityFS")
			if(hyd.buint0 == 0x74696e55)
				return SFtype.AB;
			else
				return SFtype.Asset;
			
			
			
		}
		
		
		
		public unsafe void ParseAsset(Stream reader)
		{
			byte[] buf = reader.ReadBytes(0x30,0,0x14);
			
			
			
			
			fixed(byte* srcbb = &buf[0])
			{
				bswap* bs = (bswap*)srcbb;
				uint ver = bs[2]._1u();
				Header.m_Version = ver;
				Header.m_Endianess = bs[4];
				if(ver >= 0x16)
				{
					reader.Read(buf,0x14,0x1C);
					bswap8* bs8 = (bswap8*)srcbb;
					
					Header.v16h_0 = bs8[0];
					Header.v16h_C = bs[3];
					
					Header.m_MetadataSize = bs[5]._1u();
					Header.m_FileSize = bs8[3]._1i();
					Header.m_DataOffset = bs8[4]._1i();
					
					Header.v16h_28 = bs[10];
					Header.v16h_2C = bs[11];
					
				}
				else 
				{
					Header.m_MetadataSize = bs[0]._1u();
					Header.m_FileSize = bs[1]._1u();
					
					Header.m_DataOffset = bs[3]._1u();
					if(ver < 9)
					{
						
					}
					
				}

			}
			long cur = reader.Length;
			
			if(Header.m_MetadataSize >= cur || Header.m_DataOffset >= cur)
				return;
			
			cur = reader.Position;
			
			buf = reader.ReadBytes(0x10);
			
			cur+=buf.strlen(out VerionString);
			
			reader.Position=cur;
		}
		
		public SerializedFile(string GameBase, string asset)
		{
			string path = asset;
			if (GameBase != null)
				path = GameBase + '\\' + asset;
			
			
			var reader = File.OpenRead(path);
			switch (ChkType(reader)) {
				case SFtype.Asset:
					ParseAsset(reader);
					break;
				default:
					break;
			}
			
			
			
			
		}
		
	}
	
	public static class UnityYAMLAsset
	{
		public static Dictionary<string,YAMLfile> Guid2File= new Dictionary<string,YAMLfile>();
		public static string GameBase;
		
		public static yBlock LookUpBlock(string fidstr,Dictionary<ulong,yBlock> LookUp,yObject sender)
		{
			yBlock yy;
			ulong fid = ulong.Parse(fidstr.Trim());
			
			if(fid == 0)
				return yBlock.fZero;
			
			if(!LookUp.TryGetValue(fid,out yy))
			{
				yy= new yBlock();
				LookUp[fid] = yy;
				
			}
			
			yy.refs.Add(sender);
			return yy;
				
		}
		
		public static YAMLfile LookUpFile(string[] info,yObject sender)
		{
			info[1]=info[1].Trim();
			string ky = info[3]=info[3].Trim();
			info[5]=info[5].Trim();
			
			
			
			YAMLfile yy;
			if(Guid2File.TryGetValue(ky,out yy))
			{
				
				int cy = int.Parse(info[1]);
				
				if(yy.catalog<0)
					yy.catalog = cy;
				else if(yy.catalog!=cy)
					UnityEngine.Debug.Log($"CurCatalog={cy}, OrigCatalog={yy.catalog}, dummy={yy.isDummy}");
				
				cy = int.Parse(info[5]);
				
				if(yy.usage<0)
					yy.usage = cy;
				else if(yy.usage!=cy)
					UnityEngine.Debug.Log($"CurUse={cy}, OrigUse={yy.usage}, dummy={yy.isDummy}");
					
			}
			else
			{
				yy = new YAMLfile(info);
				
			}
			
			yy.refs.Add(sender);
			return yy;
				
		}
		
		
	}
	
	public static class UnityAsset
	{
		public static string GameBase;
	}
	
	#region enumz
	public enum SFtype
	{
		Asset = 0,
		AB = 1,
	}
	
	public enum ClassIDType
	{
		Object							= 0,
		
		// =====================================
		// Runetime section
		// =====================================

		GameObject						= 1,
		Component						= 2,
		LevelGameManager				= 3,
		Transform						= 4,
		TimeManager						= 5,
		GlobalGameManager				= 6,
		BehaviourManager				= 7,
		Behaviour						= 8,
		GameManager						= 9,
		AudioManager					= 11,
		ParticleAnimator				= 12,
		InputManager					= 13,
		EllipsoidParticleEmitter		= 15,
		Filter							= 16,
		Pipeline						= 17,
		EditorExtension					= 18,
		Physics2DSettings				= 19,
		Camera							= 20,
		Material						= 21,
		MeshRenderer					= 23,
		Renderer						= 25,
		ParticleRenderer				= 26,
		Texture							= 27,
		Texture2D						= 28,
		Scene							= -29,
		SceneSettings					= -29,
		OcclusionCullingSettings		= 29,
		RenderManager					= -30,
		GraphicsSettings				= 30,
		PipelineManager					= 31,
		MeshFilter						= 33,
		BaseBehaviourManager			= 34,
		LateBehaviourManager			= 35,
		OcclusionPortal					= 41,
		Mesh							= 43,
		Skybox							= 45,
		FixedBehaviourManager			= 46,
		QualitySettings					= 47,
		Shader							= 48,
		Script							= -49,
		TextAsset						= 49,
		Rigidbody2D						= 50,
		Physics2DManager				= 51,
		NotificationManager				= 52,
		Collider2D						= 53,
		Rigidbody						= 54,
		PhysicsManager					= 55,
		Collider						= 56,
		Joint							= 57,
		CircleCollider2D				= 58,
		HingeJoint						= 59,
		PolygonCollider2D				= 60,
		BoxCollider2D					= 61,
		PhysicsMaterial2D				= 62,
		UpdateManager					= 63,
		MeshCollider					= 64,
		BoxCollider						= 65,
		SpriteCollider2D				= -66,
		CompositeCollider2D				= 66,
		RenderLayer						= 67,
		EdgeCollider2D					= 68,
		CapsuleCollider2D				= 70,
		AnimationManager				= 71,
		ComputeShader					= 72,
		AnimationClip					= 74,
		ConstantForce					= 75,
		WorldParticleCollider			= 76,
		TagManager						= 78,
		AudioListener					= 81,
		AudioSource						= 82,
		AudioClip						= 83,
		RenderTexture					= 84,
		CustomRenderTexture				= 86,
		MeshParticleEmitter				= 87,
		ParticleEmitter					= 88,
		Cubemap							= 89,
		Avatar							= 90,
		AnimatorController				= 91,
		GUILayer						= 92,
		RuntimeAnimatorController		= 93,
		ScriptMapper					= 94,
		Animator						= 95,
		TrailRenderer					= 96,
		DelayedCallManager				= 98,
		TextMesh						= 102,
		RenderSettings					= 104,
		Light							= 108,
		CGProgram						= 109,
		BaseAnimationTrack				= 110,
		Animation						= 111,
		MonoBehaviour					= 114,
		MonoScript						= 115,
		MonoManager						= 116,
		Texture3D						= 117,
		NewAnimationTrack				= 118,
		Projector						= 119,
		LineRenderer					= 120,
		Flare							= 121,
		Halo							= 122,
		LensFlare						= 123,
		FlareLayer						= 124,
		HaloLayer						= 125,
		NavMeshLayers					= -126,
		NavMeshAreas					= -126,
		NavMeshProjectSettings			= 126,
		HaloManager						= 127,
		Font							= 128,
		PlayerSettings					= 129,
		NamedObject						= 130,
		GUITexture						= 131,
		GUIText							= 132,
		GUIElement						= 133,
		PhysicMaterial					= 134,
		SphereCollider					= 135,
		CapsuleCollider					= 136,
		SkinnedMeshFilter				= -137,
		SkinnedMeshRenderer				= 137,
		FixedJoint						= 138,
		RaycastCollider					= 140,
		BuildSettings					= 141,
		AssetBundle						= 142,
		CharacterController				= 143,
		CharacterJoint					= 144,
		SpringJoint						= 145,
		WheelCollider					= 146,
		ResourceManager					= 147,
		NetworkView						= 148,
		NetworkManager					= 149,
		PreloadData						= 150,
		MovieTexture					= 152,
		ConfigurableJoint				= 153,
		TerrainCollider					= 154,
		MasterServerInterface			= 155,
		TerrainData						= 156,
		LightmapSettings				= 157,
		WebCamTexture					= 158,
		EditorSettings					= 159,
		InteractiveCloth				= 160,
		ClothRenderer					= 161,
		EditorUserSettings				= 162,
		SkinnedCloth					= 163,
		AudioReverbFilter				= 164,
		AudioHighPassFilter				= 165,
		AudioChorusFilter				= 166,
		AudioReverbZone					= 167,
		AudioEchoFilter					= 168,
		AudioLowPassFilter				= 169,
		AudioDistortionFilter			= 170,
		SparseTexture					= 171,
		AudioBehaviour					= 180,
		AudioFilter						= 181,
		WindZone						= 182,
		Cloth							= 183,
		SubstanceArchive				= 184,
		ProceduralMaterial				= 185,
		ProceduralTexture				= 186,
		Texture2DArray					= 187,
		CubemapArray					= 188,
		OffMeshLink						= 191,
		OcclusionArea					= 192,
		Tree							= 193,
		NavMesh							= -194,
		NavMeshObsolete					= 194,
		NavMeshAgent					= 195,
		NavMeshSettings					= 196,
		LightProbesLegacy				= 197,
		ParticleSystem					= 198,
		ParticleSystemRenderer			= 199,
		ShaderVariantCollection			= 200,
		LODGroup						= 205,
		BlendTree						= 206,
		Motion							= 207,
		NavMeshObstacle					= 208,
		SortingGroup					= 210,
		//TerrainInstance				= 210,
		SpriteRenderer					= 212,
		Sprite							= 213,
		CachedSpriteAtlas				= 214,
		ReflectionProbe					= 215,
		ReflectionProbes				= 216,
		Terrain							= 218,
		LightProbeGroup					= 220,
		AnimatorOverrideController		= 221,
		CanvasRenderer					= 222,
		Canvas							= 223,
		RectTransform					= 224,
		CanvasGroup						= 225,
		BillboardAsset					= 226,
		BillboardRenderer				= 227,
		SpeedTreeWindAsset				= 228,
		AnchoredJoint2D					= 229,
		Joint2D							= 230,
		SpringJoint2D					= 231,
		DistanceJoint2D					= 232,
		HingeJoint2D					= 233,
		SliderJoint2D					= 234,
		WheelJoint2D					= 235,
		ClusterInputManager				= 236,
		BaseVideoTexture				= 237,
		NavMeshData						= 238,
		AudioMixer						= 240,
		AudioMixerController			= 241,
		AudioMixerGroupController		= 243,
		AudioMixerEffectController		= 244,
		AudioMixerSnapshotController	= 245,
		PhysicsUpdateBehaviour2D		= 246,
		ConstantForce2D					= 247,
		Effector2D						= 248,
		AreaEffector2D					= 249,
		PointEffector2D					= 250,
		PlatformEffector2D				= 251,
		SurfaceEffector2D				= 252,
		BuoyancyEffector2D				= 253,
		RelativeJoint2D					= 254,
		FixedJoint2D					= 255,
		FrictionJoint2D					= 256,
		TargetJoint2D					= 257,
		LightProbes						= 258,
		LightProbeProxyVolume			= 259,
		SampleClip						= 271,
		AudioMixerSnapshot				= 272,
		AudioMixerGroup					= 273,
		NScreenBridge					= 280,
		AssetBundleManifest				= 290,
		UnityAdsManager					= 292,
		RuntimeInitializeOnLoadManager	= 300,
		CloudWebServicesManager			= 301,
		UnityAnalyticsManager			= 303,
		CrashReportManager				= 304,
		PerformanceReportingManager		= 305,
		UnityConnectSettings			= 310,
#warning TODO: merge with AvatarMask
		AvatarMask1						= 319,
		PlayableDirector				= 320,
		VideoPlayer						= 328,
		VideoClip						= 329,
		ParticleSystemForceField		= 330,
		SpriteMask						= 331,
		WorldAnchor						= 362,
		OcclusionCullingData			= 363,

		// =====================================
		// Editor section
		// =====================================

		DataTemplate					= -1001,
		PrefabInstance					= 1001,
		EditorExtensionImpl				= 1002,
		AssetImporter					= 1003,
		AssetDatabase					= 1004,
		Mesh3DSImporter					= 1005,
		TextureImporter					= 1006,
		ShaderImporter					= 1007,
		ComputeShaderImporter			= 1008,
		AvatarMask						= 1011,
		AudioImporter					= 1020,
		HierarchyState					= 1026,
		GUIDSerializer					= 1027,
		AssetMetaData					= 1028,
		DefaultAsset					= 1029,
		DefaultImporter					= 1030,
		TextScriptImporter				= 1031,
		SceneAsset						= 1032,
		NativeFormatImporter			= 1034,
		MonoImporter					= 1035,
		AssetServerCache				= 1037,
		LibraryAssetImporter			= 1038,
		ModelImporter					= 1040,
		FBXImporter						= 1041,
		TrueTypeFontImporter			= 1042,
		MovieImporter					= 1044,
		EditorBuildSettings				= 1045,
		DDSImporter						= 1046,
		InspectorExpandedState			= 1048,
		AnnotationManager				= 1049,
		MonoAssemblyImporter			= -1050,
		PluginImporter					= 1050,
		EditorUserBuildSettings			= 1051,
		PVRImporter						= 1052,
		ASTCImporter					= 1053,
		KTXImporter						= 1054,
		IHVImageFormatImporter			= 1055,
		AnimatorStateTransition			= 1101,
		AnimatorState					= 1102,
		HumanTemplate					= 1105,
		AnimatorStateMachine			= 1107,
		PreviewAssetType				= 1108,
		AnimatorTransition				= 1109,
		SpeedTreeImporter				= 1110,
		AnimatorTransitionBase			= 1111,
		SubstanceImporter				= 1112,
		EnlightenSystemBuildParameters	= -1113,
		LightmapParameters				= 1113,
		LightmapSnapshot				= -1120,
		LightingDataAsset				= 1120,
		GISRaster						= 1121,
		GISRasterImporter				= 1122,
		CadImporter						= 1123,
		SketchUpImporter				= 1124,
		BuildReport						= 1125,
		PackedAssets					= 1126,
		VideoClipImporter				= 1127,
		/// <summary>
		/// ???
		/// </summary>
		ActivationLogComponent			= 2000,
		
		// =====================================
		// Custom section
		// =====================================

		@int							= 100000,
		@bool							= 100001,
		@float							= 100002,
		MonoObject						= 100003,
		Collision						= 100004,
		Vector3f						= 100005,
		RootMotionData					= 100006,
		Collision2D						= 100007,
		AudioMixerLiveUpdateFloat		= 100008,
		AudioMixerLiveUpdateBool		= 100009,
		Polygon2D						= 100010,
		@void							= 100011,

		// =====================================
		// New editor section
		// =====================================
		
		TilemapCollider2D									= 19719996,
		AssetImporterLog									= 41386430,
		VFXRenderer											= 73398921,
		SerializableManagedRefTestClass						= 76251197,
		Grid												= 156049354,
		Preset												= 181963792,
		EmptyObject											= 277625683,
		IConstraint											= 285090594,
		TestObjectWithSpecialLayoutOne						= 293259124,
		AssemblyDefinitionReferenceImporter					= 294290339,
		SiblingDerived										= 334799969,
		TestObjectWithSerializedMapStringNonAlignedStruct	= 342846651,
		SubDerived											= 367388927,
		AssetImportInProgressProxy							= 369655926,
		EditorProjectAccess									= 426301858,
		PrefabImporter										= 468431735,
		TestObjectWithSerializedArray						= 478637458,
		TestObjectWithSerializedAnimationCurve				= 478637459,
		TilemapRenderer										= 483693784,
		SpriteAtlasDatabase									= 638013454,
		CachedSpriteAtlasRuntimeData						= 644342135,
		RendererFake										= 646504946,
		AssemblyDefinitionReferenceAsset					= 662584278,
		BuiltAssetBundleInfoSet								= 668709126,
		SpriteAtlas											= 687078895,
		PlatformModuleSetup									= 877146078,
		AimConstraint										= 895512359,
		VFXManager											= 937362698,
		VisualEffectSubgraph								= 994735392,
		VisualEffectSubgraphOperator						= 994735403,
		VisualEffectSubgraphBlock							= 994735404,
		Prefab												= 1001480554,
		LocalizationImporter								= 1027052791,
		Derived												= 1091556383,
		PropertyModificationsTargetTestObject				= 1111377672,
		ReferencesArtifactGenerator							= 1114811875,
		AssemblyDefinitionAsset								= 1152215463,
		SceneVisibilityState								= 1154873562,
		LookAtConstraint									= 1183024399,
		MultiArtifactTestImporter							= 1223240404,
		GameObjectRecorder									= 1268269756,
		LightingDataAssetParent								= 1325145578,
		PresetManager										= 1386491679,
		TestObjectWithSpecialLayoutTwo						= 1392443030,
		StreamingManager									= 1403656975,
		LowerResBlitTexture									= 1480428607,
		StreamingController									= 1542919678,
		TestObjectVectorPairStringBool						= 1628831178,
		GridLayout											= 1742807556,
		AssemblyDefinitionImporter							= 1766753193,
		ParentConstraint									= 1773428102,
		FakeComponent										= 1803986026,
		PositionConstraint									= 1818360608,
		RotationConstraint									= 1818360609,
		ScaleConstraint										= 1818360610,
		Tilemap												= 1839735485,
		PackageManifest										= 1896753125,
		PackageManifestImporter								= 1896753126,
		TerrainLayer										= 1953259897,
		SpriteShapeRenderer									= 1971053207,
		NativeObjectType									= 1977754360,
		TestObjectWithSerializedMapStringBool				= 1981279845,
		SerializableManagedHost								= 1995898324,
		VisualEffectAsset									= 2058629509,
		VisualEffectImporter								= 2058629510,
		VisualEffectResource								= 2058629511,
		VisualEffectObject									= 2059678085,
		VisualEffect										= 2083052967,
		LocalizationAsset									= 2083778819,
		ScriptedImporter									= 2089858483,
	}
	#endregion

	
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
		
		public static char[] kongge = {' ','&'};
		
		
		
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
		
		
		public YAMLfile(string metapath,yElement metain,string[] txt)
		{
			yType = yObjType.File;
			Load(metapath,metain,txt);
			
		}
		
		public void Load(string metapath,yElement metain,string[] txt)
		{
			int len=UnityYAMLAsset.GameBase.Length+1;
			
			Path = metapath.Substring(len,metapath.Length-len-5);
			meta = metain;
			
			
			
			
			List<int> addrlist = new List<int>();
			List<ulong> idlist =  new List<ulong>();
			len = txt.Length;
			for(int i=2;i<len;i++)
			{
				if(txt[i].StartsWith("--- !u!"))
				{
					addrlist.Add(i);
					string[] paar = txt[i].Substring(7).Split(kongge,StringSplitOptions.RemoveEmptyEntries);
					ulong idd = ulong.Parse(paar[1]);
					idlist.Add(idd);
					Blocks[idd] = new yBlock(this,int.Parse(paar[0]),idd);
				}
			}
			
			addrlist.Add(len);
			len = addrlist.Count-1;
			
			
			
			for(int i=0;i<len;i++)
			{
				int start = addrlist[i];
				int end = addrlist[i+1];
				
				
				
				Blocks[idlist[i]].Load(StringGroup.Parse(txt,start+1,end),Blocks);
			}
			isDummy = false;
		}
		
		
		public static YAMLfile Create(string path)
		{
			if(!File.Exists(path))
				path = UnityYAMLAsset.GameBase+'\\'+path;
			
			string azet = path.Substring(0,path.Length-5).ToLower();
			
			if(azet.EndsWith(".png")||azet.EndsWith(".jpg")||azet.EndsWith(".tga")||azet.EndsWith(".shader")||azet.EndsWith(".cginc")||azet.EndsWith(".cg")||azet.EndsWith(".fbx")||azet.EndsWith(".cs"))
				return null;
			if(!File.Exists(azet))
				return null;
			
			
			
			
			
			string[] txt = File.ReadAllLines(azet);
			
			if(!txt[0].StartsWith("%YAML"))
				return null;
			
			
			
			var tmpmeta = new yElement(StringGroup.Parse(File.ReadAllLines(path),0,-1).substr,null,null,0);
			
			azet = ((yValue)tmpmeta.Fields["guid"]).ToString();
			
			YAMLfile yaoi;
			if(!UnityYAMLAsset.Guid2File.TryGetValue(azet,out yaoi))
			{
				yaoi = new YAMLfile(path,tmpmeta,txt);
				UnityYAMLAsset.Guid2File[azet] = yaoi;
			}
			
			if(yaoi.isDummy)
				yaoi.Load(path,tmpmeta,txt);
			
			return yaoi;
			
			
			
		}
	}
	
	
	
	public class yBlock : yKlass
	{
		public YAMLfile BaseFile;
		public static yBlock fZero = new yBlock();
		
		public string ClassStr;
		public ClassIDType ClassID;
		public yElement content;
		public ulong LocalID;
		
		public yBlock(YAMLfile yfile,int klasid,ulong Lid)
		{
			yType = yObjType.Block;
			ClassID = (ClassIDType)klasid;
			BaseFile=yfile;
			LocalID = Lid;
		}
		
		public yBlock()
		{
			yType = yObjType.Block;
			LocalID=0xFFFFFFFFFFFFFFFF;
			isDummy = true;
		}
		
		public void Load(StringGroup sgp,Dictionary<ulong,yBlock> LookUp)
		{

			qnum = sgp.topstr_n;
			ClassStr = sgp.topstr;
			content = new yElement(sgp.substr,LookUp,this,sgp.topstr_n+1);
		}
		
		public override string ToString()
		{
			return ClassStr;
		}
	}
	
	
	
	public class yElement : yObject
	{
		public const string indentx2 = "  ";
		public static char[] Maohao = {':'};
		
		public static string mkindhead(int n)
		{
			string[] comb = new string[n];
			for(int i=0;i<n;i++)
				comb[i]=indentx2;
			
			return String.Concat(comb);
		}
		
		public Dictionary<string,yObject> Fields = new Dictionary<string, yObject>();
		
		public static yObject mkLinks(string g,Dictionary<ulong,yBlock> LookUp,yObject sender)
		{
				string[] fyd = g.Split(yMultiValue.kuohao,StringSplitOptions.RemoveEmptyEntries);
				if(fyd.Length==2)
				{
					return UnityYAMLAsset.LookUpBlock(fyd[1],LookUp,sender);
					
				}
				else if(fyd.Length==6)
				{
					return UnityYAMLAsset.LookUpFile(fyd,sender);
				}
				
				return null;
		}
		
		public static yObject mkpurevalue(string g,Dictionary<ulong,yBlock> LookUp,yObject sender)
		{
			g = g.Trim();
			if(g=="{}")
			{
				return new yMultiValue();
			}
			else if(g=="[]")
			{
				return new yArray();
			}
			else if(g.StartsWith("{fileID:"))
			{
				return mkLinks(g,LookUp,sender);
						
			}
			else if(g.StartsWith("{"))
			{
				return new yMultiValue(g);
			}
			else
			{
				return new yValue(g);
			}
			
			
		}
		
		public string[] isArrayStr(StringGroup[] Zulist)
		{
			List<string> purevalues = new List<string>();
			foreach(var zuv in Zulist)
			{
				if(zuv.topstr[0]!='>')
					return null;
				
				if(zuv.substr.Length!=1)
					return null;
				
				
				purevalues.Add(zuv.substr[0].topstr.Trim());
			}
			
			return purevalues.ToArray();
		}
		
		public yElement(StringGroup[] sgplist,Dictionary<ulong,yBlock> LookUp,yObject sender,int qpos)
		{
			qnum=qpos;
			Parent = sender;
			yType = yObjType.Element;
			
			foreach(var sg in sgplist)
			{
				if(sg.substr==null)
				{
					var keypair = sg.topstr.Split(Maohao,2,StringSplitOptions.RemoveEmptyEntries);
					
					if(keypair.Length==1)
						Fields[keypair[0].Trim()] = null;
					else
					{
						var jj = mkpurevalue(keypair[1],LookUp,this);
						jj.qnum = sg.topstr_n;
						Fields[keypair[0].Trim()] =jj;
					}
						
				}
				else if(sg.substr[0].topstr[0]=='>')
				{
					string[] strarr = isArrayStr(sg.substr);
					if(strarr==null)
					{
						Fields[sg.topstr.Trim()] = new yArray(sg.substr,LookUp,this,sg.topstr_n);
					}
					else
					{
						Fields[sg.topstr.Trim()] = new yArray(strarr,LookUp,this);
					}
				}
				else
				{
					Fields[sg.topstr.Trim()] = new yElement(sg.substr,LookUp,this,sg.topstr_n);
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
		
		public yArray(StringGroup[] mixed,Dictionary<ulong,yBlock> LookUp,yObject sender,int qpos)
		{
			qnum = qpos;
			Parent = sender;
			yType = yObjType.Array;
			int yd = mixed.Length;
			array = new yObject[yd];
			
			for(int i=0;i<yd;i++)
			{
				array[i] = new yElement(mixed[i].substr,LookUp,this,mixed[i].substr[0].topstr_n);
			}
		}
		

		
		public yArray(string[] pure,Dictionary<ulong,yBlock> LookUp,yObject sender)
		{
			Parent = sender;
			yType = yObjType.Array;
			string oldkey = string.Empty;
			int yd = pure.Length;
			bool nohead = true;
			PureValueHead = new string[yd];
			array = new yObject[yd];
			
			for(int i=0;i<yd;i++)
			{
				if(pure[i].StartsWith("{fileID:"))
				{
					array[i] = yElement.mkLinks(pure[i],LookUp,this);
				}
				else if(pure[i].StartsWith("{"))
				{
					array[i] = new yMultiValue(pure[i]);
				}
				else if(pure[i].StartsWith("'"))
				{
					array[i] = new yValue(pure[i]);
				}
				else
				{
					var keypair = pure[i].Split(yElement.Maohao,2,StringSplitOptions.RemoveEmptyEntries);
					
					if(keypair.Length==2)
					{
						nohead = false;
						var ky = keypair[0].Trim();
						if(!String.Equals(oldkey,ky))
						{
							oldkey=ky;
						}
						
						PureValueHead[i] = oldkey;
						
						
						array[i] =  yElement.mkpurevalue(keypair[1],LookUp,this);
					}
					else
					{
						array[i] = new yValue(pure[i]);
					}
					
					
				}
			}
			
			if(nohead)
				PureValueHead = null;
		}
	}
	
	public class yValue: yObject
	{
		public string value;
		
		public yValue(string g)
		{
			yType = yObjType.Value;
			value=g;
		}
		
		public override string ToString()
		{
			return value;
		}

	}
	
	public class yMultiValue: yObject
	{
		public Dictionary<string,string> values;
		public static char[] kuohao = {'{',':',',','}'};
		public yMultiValue()
		{
			yType = yObjType.Multivale;
		}
		
		public yMultiValue(string g)
		{
			yType = yObjType.Multivale;
			
			values = new Dictionary<string, string>();
			
			string [] paar = g.Split(kuohao,StringSplitOptions.RemoveEmptyEntries);
			if(paar.Length%2!=0)
				UnityEngine.Debug.Log("MultiValueBug");
			
			int len = paar.Length/2;
			for(int i=0;i<len;i++)
			{
				values[paar[i*2].Trim()]=paar[i*2+1].Trim();
			}
		}
	}
	
	
	
	[StructLayout(LayoutKind.Explicit, Size = 16), Serializable]
	public unsafe struct yGUID
	{
		[NonSerialized]
		[FieldOffset(0)] public fixed byte Text[16];
		
		[FieldOffset(0)] public bswap8 g0;
		[FieldOffset(8)] public bswap8 g1;
		
		
		
		public override string ToString()
		{
			
			bswap8 tmp = g0;
			string rett = tmp._1u().ToString("x16");
			
			tmp = g1;
			return rett+tmp._1u().ToString("x16");
			
			/*
			string[] a16 = new string[16];
			fixed(byte* hyy = Text) {
			for(int i=0;i<16;i++)
			{
				a16[i]=hyy[i].ToString("x2");
			}
			}
			
			return string.Concat(a16);
			
			*/
		}
		
		public yGUID(string g)
		{
			g0 = new bswap8();
			g1 = new bswap8();
			
			
			g=g.Trim();
			
			g0.bulong = ulong.Parse(g.Substring(0,16),NumberStyles.HexNumber);
			g1.bulong = ulong.Parse(g.Substring(16,16),NumberStyles.HexNumber);
			
			g0._1();
			g1._1();
			
			/*
			fixed(byte* hyy = Text) {
			for(int i=0;i<16;i++)
			{
				
				hyy[i]=Byte.Parse(g.Substring(i*2,2), NumberStyles.HexNumber);
			}
			}
			*/
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
			bswap bs  = new bswap();
			
			for(int i=0;i<256;i++)
			{
				if(g[i*2]!=' '||g[i*2+1]!=' ')
				{
					
					
					if(g[i*2]=='-')
					{
						bs.a=(byte)(i+1);
						bs.b = 1;
						return bs;
					}
					else
					{
						bs.a=(byte)(i);
						return bs;
					}
				}
			}
			
			return bs;
		}
		public static qStringGroup GetNParent(qStringGroup sg,int n)
		{
			qStringGroup rt=sg;
			
			if(sg.isArrayHead)
				rt = sg.parent;

				
			
			for(int i=0;i<n;i++)
			{
				rt = rt.parent;
				
			}
			return rt;
		}
		
		
		
		public static void setmoth(qStringGroup moth,qStringGroup nxsg)
		{
			if(moth.isArrayHead)
					nxsg.parent = moth.parent;
			else
					nxsg.parent = moth;
		}
		
		
		
		public static qStringGroup MakeZu(qStringGroup moth,qStringGroup nxsg)
		{
			var grphd = new qStringGroup(-1);
			grphd.isArrayHead = true;
			setmoth(moth,grphd);
						
			grphd.substr.Add(nxsg);
			setmoth(moth,nxsg);
						
			moth.substr.Add(grphd);
			return grphd;
		}
		
		
	}
	
	public class StringGroup
	{
		public string topstr;
		public int topstr_n;
		public StringGroup[] substr;
		
		public StringGroup(string[] g,int y)
		{
			topstr_n=y;
			if(y==-1)
			{
				topstr=">";
				return;
			}
			if(y==-2)
			{
				topstr="<";
				return;
			}
				
			
			
			topstr = g[y];
		}
		
		
		public StringGroup(string y)
		{
			topstr_n=-1;
			topstr = y;
		}
		
		public static int findprev(bool[] pos,int kk)
		{
			for(int i=kk;i>=0;i--)
			{
				if(pos[i])
					return i;
			}
			return 0;
		}
		
		public static void fixqs(qStringGroup qs)
		{
			int y = qs.substr.Count;
			bool[] pos = new bool[y];
			List<qStringGroup> nyusub = new List<qStringGroup>();
			for(int i=0;i<y;i++)
			{
				if(qs.substr[i].topstr==-1)
				{
					pos[i]=true;
					nyusub.Add(qs.substr[i]);
				}
			}
			
			for(int i=0;i<y;i++)
			{
				var valu = qs.substr[i];
				if(valu.topstr!=-1)
				{
					qs.substr[findprev(pos,i)].substr.Add(valu);
				}
			}
			
			qs.substr = nyusub;
			
		}
		
		public static void q2str(string[] g,qStringGroup qs,StringGroup sg)
		{
			
			int y = qs.substr.Count;
			if(y==0)
				return;
			
			if(y>1)
			{
				if(qs.substr[0].topstr==-1&&qs.substr[1].topstr!=-1)
				{
					fixqs(qs);
					y= qs.substr.Count;
				}
			}
			
			
			sg.substr = new StringGroup[y];
			for(int i=0;i<y;i++)
			{
				var nsg = new StringGroup(g,qs.substr[i].topstr);
				sg.substr[i] = nsg;
				q2str(g,qs.substr[i],nsg);
			}
		}
		
		
		public static StringGroup Parse(string[] g,int start,int endpos)
		{
			if(endpos<0)
				endpos = g.Length;
			
			qStringGroup root = new qStringGroup(-2);
			qStringGroup moth = root;
			qStringGroup lastt = root;
			
			int curind = qStringGroup.CountInd(g[start]).a;
			for(int i=start;i<endpos;i++)
			{
				bswap nxindbs = qStringGroup.CountInd(g[i]);
				int nxind = nxindbs.a;
				
				g[i]=g[i].Substring(nxind*2);
				qStringGroup nxsg = new qStringGroup(i);

				if(nxind > curind)
					moth = lastt;
				else if(nxind < curind)
					moth = qStringGroup.GetNParent(moth,curind-nxind);
				
				
				if(nxindbs.b==1)
				{
					if(moth.isArrayHead)
						moth = moth.parent;
					
					moth=qStringGroup.MakeZu(moth,nxsg);
				}
				else
				{
						moth.substr.Add(nxsg);
						qStringGroup.setmoth(moth,nxsg);
				}
				
				
				
				
				lastt = nxsg;
				curind = nxind;
				
			}
			
			if(root.substr.Count==1)
			{
				var vroot = root.substr[0];
				root.substr = null;
				root =vroot;
				root.parent = null;
			}
			
			StringGroup Realroot = new StringGroup(g,root.topstr);
			
			q2str(g,root,Realroot);
			
			return Realroot;
		}
		
		public override string ToString()
		{
			
			if(substr!=null)
			{
				return topstr+"\t//"+substr.Length;
			}
			else
			{
				return topstr;
			}
			
			
		}

	}
}