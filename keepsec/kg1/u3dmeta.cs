
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using U3D.Class;

namespace U3D
{
	public class uMeta
	{
		public ClassIDType ClassID;
		public long Offset;
		public int Size;
		public SerializedFile src;
		public SerializedType MB_type;
		
		public uObject obj;
		
		public List<uMeta> refs;
		
		public uMeta(SerializedFile ser)
		{
			src=ser;
		}
		
		public void addref(uMeta reff)
		{
			if(refs==null)
				refs = new List<uMeta>();
			
			refs.Add(reff);
		}
		
		public uObject Load(Stream reader)
		{
			if(Offset<0)
				return null;
			
			switch (ClassID) {
				case ClassIDType.Light:
					obj = new Light();
					break;
				case ClassIDType.ActivationLogComponent:
					obj = new ActivationLogComponent();
					break;
				case ClassIDType.AimConstraint:
					obj = new AimConstraint();
					break;
				case ClassIDType.AnchoredJoint2D:
					obj = new AnchoredJoint2D();
					break;
				case ClassIDType.Animation:
					obj = new Animation();
					break;
				case ClassIDType.AnimationClip:
					obj = new AnimationClip();
					break;
				case ClassIDType.AnimationManager:
					obj = new AnimationManager();
					break;
				case ClassIDType.Animator:
					obj = new Animator();
					break;
				case ClassIDType.AnimatorController:
					obj = new AnimatorController();
					break;
				case ClassIDType.AnimatorOverrideController:
					obj = new AnimatorOverrideController();
					break;
				case ClassIDType.AnimatorState:
					obj = new AnimatorState();
					break;
				case ClassIDType.AnimatorStateMachine:
					obj = new AnimatorStateMachine();
					break;
				case ClassIDType.AnimatorStateTransition:
					obj = new AnimatorStateTransition();
					break;
				case ClassIDType.AnimatorTransition:
					obj = new AnimatorTransition();
					break;
				case ClassIDType.AnimatorTransitionBase:
					obj = new AnimatorTransitionBase();
					break;
				case ClassIDType.AnnotationManager:
					obj = new AnnotationManager();
					break;
				case ClassIDType.AreaEffector2D:
					obj = new AreaEffector2D();
					break;
				case ClassIDType.AssemblyDefinitionAsset:
					obj = new AssemblyDefinitionAsset();
					break;
				case ClassIDType.AssemblyDefinitionImporter:
					obj = new AssemblyDefinitionImporter();
					break;
				case ClassIDType.AssemblyDefinitionReferenceAsset:
					obj = new AssemblyDefinitionReferenceAsset();
					break;
				case ClassIDType.AssemblyDefinitionReferenceImporter:
					obj = new AssemblyDefinitionReferenceImporter();
					break;
				case ClassIDType.AssetBundle:
					obj = new AssetBundle();
					break;
				case ClassIDType.AssetBundleManifest:
					obj = new AssetBundleManifest();
					break;
				case ClassIDType.AssetDatabase:
					obj = new AssetDatabase();
					break;
				case ClassIDType.AssetImporter:
					obj = new AssetImporter();
					break;
				case ClassIDType.AssetImporterLog:
					obj = new AssetImporterLog();
					break;
				case ClassIDType.AssetImportInProgressProxy:
					obj = new AssetImportInProgressProxy();
					break;
				case ClassIDType.AssetMetaData:
					obj = new AssetMetaData();
					break;
				case ClassIDType.AssetServerCache:
					obj = new AssetServerCache();
					break;
				case ClassIDType.ASTCImporter:
					obj = new ASTCImporter();
					break;
				case ClassIDType.AudioBehaviour:
					obj = new AudioBehaviour();
					break;
				case ClassIDType.AudioChorusFilter:
					obj = new AudioChorusFilter();
					break;
				case ClassIDType.AudioClip:
					obj = new AudioClip();
					break;
				case ClassIDType.AudioDistortionFilter:
					obj = new AudioDistortionFilter();
					break;
				case ClassIDType.AudioEchoFilter:
					obj = new AudioEchoFilter();
					break;
				case ClassIDType.AudioFilter:
					obj = new AudioFilter();
					break;
				case ClassIDType.AudioHighPassFilter:
					obj = new AudioHighPassFilter();
					break;
				case ClassIDType.AudioImporter:
					obj = new AudioImporter();
					break;
				case ClassIDType.AudioListener:
					obj = new AudioListener();
					break;
				case ClassIDType.AudioLowPassFilter:
					obj = new AudioLowPassFilter();
					break;
				case ClassIDType.AudioManager:
					obj = new AudioManager();
					break;
				case ClassIDType.AudioMixer:
					obj = new AudioMixer();
					break;
				case ClassIDType.AudioMixerController:
					obj = new AudioMixerController();
					break;
				case ClassIDType.AudioMixerEffectController:
					obj = new AudioMixerEffectController();
					break;
				case ClassIDType.AudioMixerGroup:
					obj = new AudioMixerGroup();
					break;
				case ClassIDType.AudioMixerGroupController:
					obj = new AudioMixerGroupController();
					break;
				case ClassIDType.AudioMixerLiveUpdateBool:
					obj = new AudioMixerLiveUpdateBool();
					break;
				case ClassIDType.AudioMixerLiveUpdateFloat:
					obj = new AudioMixerLiveUpdateFloat();
					break;
				case ClassIDType.AudioMixerSnapshot:
					obj = new AudioMixerSnapshot();
					break;
				case ClassIDType.AudioMixerSnapshotController:
					obj = new AudioMixerSnapshotController();
					break;
				case ClassIDType.AudioReverbFilter:
					obj = new AudioReverbFilter();
					break;
				case ClassIDType.AudioReverbZone:
					obj = new AudioReverbZone();
					break;
				case ClassIDType.AudioSource:
					obj = new AudioSource();
					break;
				case ClassIDType.Avatar:
					obj = new Avatar();
					break;
				case ClassIDType.AvatarMask:
					obj = new AvatarMask();
					break;
				case ClassIDType.AvatarMask1:
					obj = new AvatarMask1();
					break;
				case ClassIDType.BaseAnimationTrack:
					obj = new BaseAnimationTrack();
					break;
				case ClassIDType.BaseBehaviourManager:
					obj = new BaseBehaviourManager();
					break;
				case ClassIDType.BaseVideoTexture:
					obj = new BaseVideoTexture();
					break;
				case ClassIDType.Behaviour:
					obj = new Behaviour();
					break;
				case ClassIDType.BehaviourManager:
					obj = new BehaviourManager();
					break;
				case ClassIDType.BillboardAsset:
					obj = new BillboardAsset();
					break;
				case ClassIDType.BillboardRenderer:
					obj = new BillboardRenderer();
					break;
				case ClassIDType.BlendTree:
					obj = new BlendTree();
					break;
				case ClassIDType.BoxCollider:
					obj = new BoxCollider();
					break;
				case ClassIDType.BoxCollider2D:
					obj = new BoxCollider2D();
					break;
				case ClassIDType.BuildReport:
					obj = new BuildReport();
					break;
				case ClassIDType.BuildSettings:
					obj = new BuildSettings();
					break;
				case ClassIDType.BuiltAssetBundleInfoSet:
					obj = new BuiltAssetBundleInfoSet();
					break;
				case ClassIDType.BuoyancyEffector2D:
					obj = new BuoyancyEffector2D();
					break;
				case ClassIDType.CachedSpriteAtlas:
					obj = new CachedSpriteAtlas();
					break;
				case ClassIDType.CachedSpriteAtlasRuntimeData:
					obj = new CachedSpriteAtlasRuntimeData();
					break;
				case ClassIDType.CadImporter:
					obj = new CadImporter();
					break;
				case ClassIDType.Camera:
					obj = new Camera();
					break;
				case ClassIDType.Canvas:
					obj = new Canvas();
					break;
				case ClassIDType.CanvasGroup:
					obj = new CanvasGroup();
					break;
				case ClassIDType.CanvasRenderer:
					obj = new CanvasRenderer();
					break;
				case ClassIDType.CapsuleCollider:
					obj = new CapsuleCollider();
					break;
				case ClassIDType.CapsuleCollider2D:
					obj = new CapsuleCollider2D();
					break;
				case ClassIDType.CGProgram:
					obj = new CGProgram();
					break;
				case ClassIDType.CharacterController:
					obj = new CharacterController();
					break;
				case ClassIDType.CharacterJoint:
					obj = new CharacterJoint();
					break;
				case ClassIDType.CircleCollider2D:
					obj = new CircleCollider2D();
					break;
				case ClassIDType.Cloth:
					obj = new Cloth();
					break;
				case ClassIDType.ClothRenderer:
					obj = new ClothRenderer();
					break;
				case ClassIDType.CloudWebServicesManager:
					obj = new CloudWebServicesManager();
					break;
				case ClassIDType.ClusterInputManager:
					obj = new ClusterInputManager();
					break;
				case ClassIDType.Collider:
					obj = new Collider();
					break;
				case ClassIDType.Collider2D:
					obj = new Collider2D();
					break;
				case ClassIDType.Collision:
					obj = new Collision();
					break;
				case ClassIDType.Collision2D:
					obj = new Collision2D();
					break;
				case ClassIDType.Component:
					obj = new Component();
					break;
				case ClassIDType.CompositeCollider2D:
					obj = new CompositeCollider2D();
					break;
				case ClassIDType.ComputeShader:
					obj = new ComputeShader();
					break;
				case ClassIDType.ComputeShaderImporter:
					obj = new ComputeShaderImporter();
					break;
				case ClassIDType.ConfigurableJoint:
					obj = new ConfigurableJoint();
					break;
				case ClassIDType.ConstantForce:
					obj = new ConstantForce();
					break;
				case ClassIDType.ConstantForce2D:
					obj = new ConstantForce2D();
					break;
				case ClassIDType.CrashReportManager:
					obj = new CrashReportManager();
					break;
				case ClassIDType.Cubemap:
					obj = new Cubemap();
					break;
				case ClassIDType.CubemapArray:
					obj = new CubemapArray();
					break;
				case ClassIDType.CustomRenderTexture:
					obj = new CustomRenderTexture();
					break;
				case ClassIDType.DataTemplate:
					obj = new DataTemplate();
					break;
				case ClassIDType.DDSImporter:
					obj = new DDSImporter();
					break;
				case ClassIDType.DefaultAsset:
					obj = new DefaultAsset();
					break;
				case ClassIDType.DefaultImporter:
					obj = new DefaultImporter();
					break;
				case ClassIDType.DelayedCallManager:
					obj = new DelayedCallManager();
					break;
				case ClassIDType.Derived:
					obj = new Derived();
					break;
				case ClassIDType.DistanceJoint2D:
					obj = new DistanceJoint2D();
					break;
				case ClassIDType.EdgeCollider2D:
					obj = new EdgeCollider2D();
					break;
				case ClassIDType.EditorBuildSettings:
					obj = new EditorBuildSettings();
					break;
				case ClassIDType.EditorExtension:
					obj = new EditorExtension();
					break;
				case ClassIDType.EditorExtensionImpl:
					obj = new EditorExtensionImpl();
					break;
				case ClassIDType.EditorProjectAccess:
					obj = new EditorProjectAccess();
					break;
				case ClassIDType.EditorSettings:
					obj = new EditorSettings();
					break;
				case ClassIDType.EditorUserBuildSettings:
					obj = new EditorUserBuildSettings();
					break;
				case ClassIDType.EditorUserSettings:
					obj = new EditorUserSettings();
					break;
				case ClassIDType.Effector2D:
					obj = new Effector2D();
					break;
				case ClassIDType.EllipsoidParticleEmitter:
					obj = new EllipsoidParticleEmitter();
					break;
				case ClassIDType.EmptyObject:
					obj = new EmptyObject();
					break;
				case ClassIDType.EnlightenSystemBuildParameters:
					obj = new EnlightenSystemBuildParameters();
					break;
				case ClassIDType.FakeComponent:
					obj = new FakeComponent();
					break;
				case ClassIDType.FBXImporter:
					obj = new FBXImporter();
					break;
				case ClassIDType.Filter:
					obj = new Filter();
					break;
				case ClassIDType.FixedBehaviourManager:
					obj = new FixedBehaviourManager();
					break;
				case ClassIDType.FixedJoint:
					obj = new FixedJoint();
					break;
				case ClassIDType.FixedJoint2D:
					obj = new FixedJoint2D();
					break;
				case ClassIDType.Flare:
					obj = new Flare();
					break;
				case ClassIDType.FlareLayer:
					obj = new FlareLayer();
					break;
				case ClassIDType.Font:
					obj = new Font();
					break;
				case ClassIDType.FrictionJoint2D:
					obj = new FrictionJoint2D();
					break;
				case ClassIDType.GameManager:
					obj = new GameManager();
					break;
				case ClassIDType.GameObject:
					obj = new GameObject();
					break;
				case ClassIDType.GameObjectRecorder:
					obj = new GameObjectRecorder();
					break;
				case ClassIDType.GISRaster:
					obj = new GISRaster();
					break;
				case ClassIDType.GISRasterImporter:
					obj = new GISRasterImporter();
					break;
				case ClassIDType.GlobalGameManager:
					obj = new GlobalGameManager();
					break;
				case ClassIDType.GraphicsSettings:
					obj = new GraphicsSettings();
					break;
				case ClassIDType.Grid:
					obj = new Grid();
					break;
				case ClassIDType.GridLayout:
					obj = new GridLayout();
					break;
				case ClassIDType.GUIDSerializer:
					obj = new GUIDSerializer();
					break;
				case ClassIDType.GUIElement:
					obj = new GUIElement();
					break;
				case ClassIDType.GUILayer:
					obj = new GUILayer();
					break;
				case ClassIDType.GUIText:
					obj = new GUIText();
					break;
				case ClassIDType.GUITexture:
					obj = new GUITexture();
					break;
				case ClassIDType.Halo:
					obj = new Halo();
					break;
				case ClassIDType.HaloLayer:
					obj = new HaloLayer();
					break;
				case ClassIDType.HaloManager:
					obj = new HaloManager();
					break;
				case ClassIDType.HierarchyState:
					obj = new HierarchyState();
					break;
				case ClassIDType.HingeJoint:
					obj = new HingeJoint();
					break;
				case ClassIDType.HingeJoint2D:
					obj = new HingeJoint2D();
					break;
				case ClassIDType.HumanTemplate:
					obj = new HumanTemplate();
					break;
				case ClassIDType.IConstraint:
					obj = new IConstraint();
					break;
				case ClassIDType.IHVImageFormatImporter:
					obj = new IHVImageFormatImporter();
					break;
				case ClassIDType.InputManager:
					obj = new InputManager();
					break;
				case ClassIDType.InspectorExpandedState:
					obj = new InspectorExpandedState();
					break;
				case ClassIDType.InteractiveCloth:
					obj = new InteractiveCloth();
					break;
				case ClassIDType.Joint:
					obj = new Joint();
					break;
				case ClassIDType.Joint2D:
					obj = new Joint2D();
					break;
				case ClassIDType.KTXImporter:
					obj = new KTXImporter();
					break;
				case ClassIDType.LateBehaviourManager:
					obj = new LateBehaviourManager();
					break;
				case ClassIDType.LensFlare:
					obj = new LensFlare();
					break;
				case ClassIDType.LevelGameManager:
					obj = new LevelGameManager();
					break;
				case ClassIDType.LibraryAssetImporter:
					obj = new LibraryAssetImporter();
					break;
				case ClassIDType.LightingDataAsset:
					obj = new LightingDataAsset();
					break;
				case ClassIDType.LightingDataAssetParent:
					obj = new LightingDataAssetParent();
					break;
				case ClassIDType.LightmapParameters:
					obj = new LightmapParameters();
					break;
				case ClassIDType.LightmapSettings:
					obj = new LightmapSettings();
					break;
				case ClassIDType.LightmapSnapshot:
					obj = new LightmapSnapshot();
					break;
				case ClassIDType.LightProbeGroup:
					obj = new LightProbeGroup();
					break;
				case ClassIDType.LightProbeProxyVolume:
					obj = new LightProbeProxyVolume();
					break;
				case ClassIDType.LightProbes:
					obj = new LightProbes();
					break;
				case ClassIDType.LightProbesLegacy:
					obj = new LightProbesLegacy();
					break;
				case ClassIDType.LineRenderer:
					obj = new LineRenderer();
					break;
				case ClassIDType.LocalizationAsset:
					obj = new LocalizationAsset();
					break;
				case ClassIDType.LocalizationImporter:
					obj = new LocalizationImporter();
					break;
				case ClassIDType.LODGroup:
					obj = new LODGroup();
					break;
				case ClassIDType.LookAtConstraint:
					obj = new LookAtConstraint();
					break;
				case ClassIDType.LowerResBlitTexture:
					obj = new LowerResBlitTexture();
					break;
				case ClassIDType.MasterServerInterface:
					obj = new MasterServerInterface();
					break;
				case ClassIDType.Material:
					obj = new Material();
					break;
				case ClassIDType.Mesh:
					obj = new Mesh();
					break;
				case ClassIDType.Mesh3DSImporter:
					obj = new Mesh3DSImporter();
					break;
				case ClassIDType.MeshCollider:
					obj = new MeshCollider();
					break;
				case ClassIDType.MeshFilter:
					obj = new MeshFilter();
					break;
				case ClassIDType.MeshParticleEmitter:
					obj = new MeshParticleEmitter();
					break;
				case ClassIDType.MeshRenderer:
					obj = new MeshRenderer();
					break;
				case ClassIDType.ModelImporter:
					obj = new ModelImporter();
					break;
				case ClassIDType.MonoAssemblyImporter:
					obj = new MonoAssemblyImporter();
					break;
				case ClassIDType.MonoBehaviour:
					obj = new MonoBehaviour();
					break;
				case ClassIDType.MonoImporter:
					obj = new MonoImporter();
					break;
				case ClassIDType.MonoManager:
					obj = new MonoManager();
					break;
				case ClassIDType.MonoObject:
					obj = new MonoObject();
					break;
				case ClassIDType.MonoScript:
					obj = new MonoScript();
					break;
				case ClassIDType.Motion:
					obj = new Motion();
					break;
				case ClassIDType.MovieImporter:
					obj = new MovieImporter();
					break;
				case ClassIDType.MovieTexture:
					obj = new MovieTexture();
					break;
				case ClassIDType.MultiArtifactTestImporter:
					obj = new MultiArtifactTestImporter();
					break;
				case ClassIDType.NamedObject:
					obj = new NamedObject();
					break;
				case ClassIDType.NativeFormatImporter:
					obj = new NativeFormatImporter();
					break;
				case ClassIDType.NativeObjectType:
					obj = new NativeObjectType();
					break;
				case ClassIDType.NavMesh:
					obj = new NavMesh();
					break;
				case ClassIDType.NavMeshAgent:
					obj = new NavMeshAgent();
					break;
				case ClassIDType.NavMeshAreas:
					obj = new NavMeshAreas();
					break;
				case ClassIDType.NavMeshData:
					obj = new NavMeshData();
					break;
				case ClassIDType.NavMeshObsolete:
					obj = new NavMeshObsolete();
					break;
				case ClassIDType.NavMeshObstacle:
					obj = new NavMeshObstacle();
					break;
				case ClassIDType.NavMeshProjectSettings:
					obj = new NavMeshProjectSettings();
					break;
				case ClassIDType.NavMeshSettings:
					obj = new NavMeshSettings();
					break;
				case ClassIDType.NetworkManager:
					obj = new NetworkManager();
					break;
				case ClassIDType.NetworkView:
					obj = new NetworkView();
					break;
				case ClassIDType.NewAnimationTrack:
					obj = new NewAnimationTrack();
					break;
				case ClassIDType.NotificationManager:
					obj = new NotificationManager();
					break;
				case ClassIDType.NScreenBridge:
					obj = new NScreenBridge();
					break;
				case ClassIDType.OcclusionArea:
					obj = new OcclusionArea();
					break;
				case ClassIDType.OcclusionCullingData:
					obj = new OcclusionCullingData();
					break;
				case ClassIDType.OcclusionCullingSettings:
					obj = new OcclusionCullingSettings();
					break;
				case ClassIDType.OcclusionPortal:
					obj = new OcclusionPortal();
					break;
				case ClassIDType.OffMeshLink:
					obj = new OffMeshLink();
					break;
				case ClassIDType.PackageManifest:
					obj = new PackageManifest();
					break;
				case ClassIDType.PackageManifestImporter:
					obj = new PackageManifestImporter();
					break;
				case ClassIDType.PackedAssets:
					obj = new PackedAssets();
					break;
				case ClassIDType.ParentConstraint:
					obj = new ParentConstraint();
					break;
				case ClassIDType.ParticleAnimator:
					obj = new ParticleAnimator();
					break;
				case ClassIDType.ParticleEmitter:
					obj = new ParticleEmitter();
					break;
				case ClassIDType.ParticleRenderer:
					obj = new ParticleRenderer();
					break;
				case ClassIDType.ParticleSystem:
					obj = new ParticleSystem();
					break;
				case ClassIDType.ParticleSystemForceField:
					obj = new ParticleSystemForceField();
					break;
				case ClassIDType.ParticleSystemRenderer:
					obj = new ParticleSystemRenderer();
					break;
				case ClassIDType.PerformanceReportingManager:
					obj = new PerformanceReportingManager();
					break;
				case ClassIDType.PhysicMaterial:
					obj = new PhysicMaterial();
					break;
				case ClassIDType.Physics2DManager:
					obj = new Physics2DManager();
					break;
				case ClassIDType.Physics2DSettings:
					obj = new Physics2DSettings();
					break;
				case ClassIDType.PhysicsManager:
					obj = new PhysicsManager();
					break;
				case ClassIDType.PhysicsMaterial2D:
					obj = new PhysicsMaterial2D();
					break;
				case ClassIDType.PhysicsUpdateBehaviour2D:
					obj = new PhysicsUpdateBehaviour2D();
					break;
				case ClassIDType.Pipeline:
					obj = new Pipeline();
					break;
				case ClassIDType.PipelineManager:
					obj = new PipelineManager();
					break;
				case ClassIDType.PlatformEffector2D:
					obj = new PlatformEffector2D();
					break;
				case ClassIDType.PlatformModuleSetup:
					obj = new PlatformModuleSetup();
					break;
				case ClassIDType.PlayableDirector:
					obj = new PlayableDirector();
					break;
				case ClassIDType.PlayerSettings:
					obj = new PlayerSettings();
					break;
				case ClassIDType.PluginImporter:
					obj = new PluginImporter();
					break;
				case ClassIDType.PointEffector2D:
					obj = new PointEffector2D();
					break;
				case ClassIDType.Polygon2D:
					obj = new Polygon2D();
					break;
				case ClassIDType.PolygonCollider2D:
					obj = new PolygonCollider2D();
					break;
				case ClassIDType.PositionConstraint:
					obj = new PositionConstraint();
					break;
				case ClassIDType.Prefab:
					obj = new Prefab();
					break;
				case ClassIDType.PrefabImporter:
					obj = new PrefabImporter();
					break;
				case ClassIDType.PrefabInstance:
					obj = new PrefabInstance();
					break;
				case ClassIDType.PreloadData:
					obj = new PreloadData();
					break;
				case ClassIDType.Preset:
					obj = new Preset();
					break;
				case ClassIDType.PresetManager:
					obj = new PresetManager();
					break;
				case ClassIDType.PreviewAssetType:
					obj = new PreviewAssetType();
					break;
				case ClassIDType.ProceduralMaterial:
					obj = new ProceduralMaterial();
					break;
				case ClassIDType.ProceduralTexture:
					obj = new ProceduralTexture();
					break;
				case ClassIDType.Projector:
					obj = new Projector();
					break;
				case ClassIDType.PropertyModificationsTargetTestObject:
					obj = new PropertyModificationsTargetTestObject();
					break;
				case ClassIDType.PVRImporter:
					obj = new PVRImporter();
					break;
				case ClassIDType.QualitySettings:
					obj = new QualitySettings();
					break;
				case ClassIDType.RaycastCollider:
					obj = new RaycastCollider();
					break;
				case ClassIDType.RectTransform:
					obj = new RectTransform();
					break;
				case ClassIDType.ReferencesArtifactGenerator:
					obj = new ReferencesArtifactGenerator();
					break;
				case ClassIDType.ReflectionProbe:
					obj = new ReflectionProbe();
					break;
				case ClassIDType.ReflectionProbes:
					obj = new ReflectionProbes();
					break;
				case ClassIDType.RelativeJoint2D:
					obj = new RelativeJoint2D();
					break;
				case ClassIDType.Renderer:
					obj = new Renderer();
					break;
				case ClassIDType.RendererFake:
					obj = new RendererFake();
					break;
				case ClassIDType.RenderLayer:
					obj = new RenderLayer();
					break;
				case ClassIDType.RenderManager:
					obj = new RenderManager();
					break;
				case ClassIDType.RenderSettings:
					obj = new RenderSettings();
					break;
				case ClassIDType.RenderTexture:
					obj = new RenderTexture();
					break;
				case ClassIDType.ResourceManager:
					obj = new ResourceManager();
					break;
				case ClassIDType.Rigidbody:
					obj = new Rigidbody();
					break;
				case ClassIDType.Rigidbody2D:
					obj = new Rigidbody2D();
					break;
				case ClassIDType.RootMotionData:
					obj = new RootMotionData();
					break;
				case ClassIDType.RotationConstraint:
					obj = new RotationConstraint();
					break;
				case ClassIDType.RuntimeAnimatorController:
					obj = new RuntimeAnimatorController();
					break;
				case ClassIDType.RuntimeInitializeOnLoadManager:
					obj = new RuntimeInitializeOnLoadManager();
					break;
				case ClassIDType.SampleClip:
					obj = new SampleClip();
					break;
				case ClassIDType.ScaleConstraint:
					obj = new ScaleConstraint();
					break;
				case ClassIDType.Scene:
					obj = new Scene();
					break;
				case ClassIDType.SceneAsset:
					obj = new SceneAsset();
					break;
				case ClassIDType.SceneVisibilityState:
					obj = new SceneVisibilityState();
					break;
				case ClassIDType.Script:
					obj = new Script();
					break;
				case ClassIDType.ScriptedImporter:
					obj = new ScriptedImporter();
					break;
				case ClassIDType.ScriptMapper:
					obj = new ScriptMapper();
					break;
				case ClassIDType.SerializableManagedHost:
					obj = new SerializableManagedHost();
					break;
				case ClassIDType.SerializableManagedRefTestClass:
					obj = new SerializableManagedRefTestClass();
					break;
				case ClassIDType.Shader:
					obj = new Shader();
					break;
				case ClassIDType.ShaderImporter:
					obj = new ShaderImporter();
					break;
				case ClassIDType.ShaderVariantCollection:
					obj = new ShaderVariantCollection();
					break;
				case ClassIDType.SiblingDerived:
					obj = new SiblingDerived();
					break;
				case ClassIDType.SketchUpImporter:
					obj = new SketchUpImporter();
					break;
				case ClassIDType.SkinnedCloth:
					obj = new SkinnedCloth();
					break;
				case ClassIDType.SkinnedMeshFilter:
					obj = new SkinnedMeshFilter();
					break;
				case ClassIDType.SkinnedMeshRenderer:
					obj = new SkinnedMeshRenderer();
					break;
				case ClassIDType.Skybox:
					obj = new Skybox();
					break;
				case ClassIDType.SliderJoint2D:
					obj = new SliderJoint2D();
					break;
				case ClassIDType.SortingGroup:
					obj = new SortingGroup();
					break;
				case ClassIDType.SparseTexture:
					obj = new SparseTexture();
					break;
				case ClassIDType.SpeedTreeImporter:
					obj = new SpeedTreeImporter();
					break;
				case ClassIDType.SpeedTreeWindAsset:
					obj = new SpeedTreeWindAsset();
					break;
				case ClassIDType.SphereCollider:
					obj = new SphereCollider();
					break;
				case ClassIDType.SpringJoint:
					obj = new SpringJoint();
					break;
				case ClassIDType.SpringJoint2D:
					obj = new SpringJoint2D();
					break;
				case ClassIDType.Sprite:
					obj = new Sprite();
					break;
				case ClassIDType.SpriteAtlas:
					obj = new SpriteAtlas();
					break;
				case ClassIDType.SpriteAtlasDatabase:
					obj = new SpriteAtlasDatabase();
					break;
				case ClassIDType.SpriteCollider2D:
					obj = new SpriteCollider2D();
					break;
				case ClassIDType.SpriteMask:
					obj = new SpriteMask();
					break;
				case ClassIDType.SpriteRenderer:
					obj = new SpriteRenderer();
					break;
				case ClassIDType.SpriteShapeRenderer:
					obj = new SpriteShapeRenderer();
					break;
				case ClassIDType.StreamingController:
					obj = new StreamingController();
					break;
				case ClassIDType.StreamingManager:
					obj = new StreamingManager();
					break;
				case ClassIDType.SubDerived:
					obj = new SubDerived();
					break;
				case ClassIDType.SubstanceArchive:
					obj = new SubstanceArchive();
					break;
				case ClassIDType.SubstanceImporter:
					obj = new SubstanceImporter();
					break;
				case ClassIDType.SurfaceEffector2D:
					obj = new SurfaceEffector2D();
					break;
				case ClassIDType.TagManager:
					obj = new TagManager();
					break;
				case ClassIDType.TargetJoint2D:
					obj = new TargetJoint2D();
					break;
				case ClassIDType.Terrain:
					obj = new Terrain();
					break;
				case ClassIDType.TerrainCollider:
					obj = new TerrainCollider();
					break;
				case ClassIDType.TerrainData:
					obj = new TerrainData();
					break;
				case ClassIDType.TerrainLayer:
					obj = new TerrainLayer();
					break;
				case ClassIDType.TestObjectVectorPairStringBool:
					obj = new TestObjectVectorPairStringBool();
					break;
				case ClassIDType.TestObjectWithSerializedAnimationCurve:
					obj = new TestObjectWithSerializedAnimationCurve();
					break;
				case ClassIDType.TestObjectWithSerializedArray:
					obj = new TestObjectWithSerializedArray();
					break;
				case ClassIDType.TestObjectWithSerializedMapStringBool:
					obj = new TestObjectWithSerializedMapStringBool();
					break;
				case ClassIDType.TestObjectWithSerializedMapStringNonAlignedStruct:
					obj = new TestObjectWithSerializedMapStringNonAlignedStruct();
					break;
				case ClassIDType.TestObjectWithSpecialLayoutOne:
					obj = new TestObjectWithSpecialLayoutOne();
					break;
				case ClassIDType.TestObjectWithSpecialLayoutTwo:
					obj = new TestObjectWithSpecialLayoutTwo();
					break;
				case ClassIDType.TextAsset:
					obj = new TextAsset();
					break;
				case ClassIDType.TextMesh:
					obj = new TextMesh();
					break;
				case ClassIDType.TextScriptImporter:
					obj = new TextScriptImporter();
					break;
				case ClassIDType.Texture:
					obj = new Texture();
					break;
				case ClassIDType.Texture2D:
					obj = new Texture2D();
					break;
				case ClassIDType.Texture2DArray:
					obj = new Texture2DArray();
					break;
				case ClassIDType.Texture3D:
					obj = new Texture3D();
					break;
				case ClassIDType.TextureImporter:
					obj = new TextureImporter();
					break;
				case ClassIDType.Tilemap:
					obj = new Tilemap();
					break;
				case ClassIDType.TilemapCollider2D:
					obj = new TilemapCollider2D();
					break;
				case ClassIDType.TilemapRenderer:
					obj = new TilemapRenderer();
					break;
				case ClassIDType.TimeManager:
					obj = new TimeManager();
					break;
				case ClassIDType.TrailRenderer:
					obj = new TrailRenderer();
					break;
				case ClassIDType.Transform:
					obj = new Transform();
					break;
				case ClassIDType.Tree:
					obj = new Tree();
					break;
				case ClassIDType.TrueTypeFontImporter:
					obj = new TrueTypeFontImporter();
					break;
				case ClassIDType.UnityAdsManager:
					obj = new UnityAdsManager();
					break;
				case ClassIDType.UnityAnalyticsManager:
					obj = new UnityAnalyticsManager();
					break;
				case ClassIDType.UnityConnectSettings:
					obj = new UnityConnectSettings();
					break;
				case ClassIDType.UpdateManager:
					obj = new UpdateManager();
					break;
				case ClassIDType.Vector3f:
					obj = new Vector3f();
					break;
				case ClassIDType.VFXManager:
					obj = new VFXManager();
					break;
				case ClassIDType.VFXRenderer:
					obj = new VFXRenderer();
					break;
				case ClassIDType.VideoClip:
					obj = new VideoClip();
					break;
				case ClassIDType.VideoClipImporter:
					obj = new VideoClipImporter();
					break;
				case ClassIDType.VideoPlayer:
					obj = new VideoPlayer();
					break;
				case ClassIDType.VisualEffect:
					obj = new VisualEffect();
					break;
				case ClassIDType.VisualEffectAsset:
					obj = new VisualEffectAsset();
					break;
				case ClassIDType.VisualEffectImporter:
					obj = new VisualEffectImporter();
					break;
				case ClassIDType.VisualEffectObject:
					obj = new VisualEffectObject();
					break;
				case ClassIDType.VisualEffectResource:
					obj = new VisualEffectResource();
					break;
				case ClassIDType.VisualEffectSubgraph:
					obj = new VisualEffectSubgraph();
					break;
				case ClassIDType.VisualEffectSubgraphBlock:
					obj = new VisualEffectSubgraphBlock();
					break;
				case ClassIDType.VisualEffectSubgraphOperator:
					obj = new VisualEffectSubgraphOperator();
					break;
				case ClassIDType.WebCamTexture:
					obj = new WebCamTexture();
					break;
				case ClassIDType.WheelCollider:
					obj = new WheelCollider();
					break;
				case ClassIDType.WheelJoint2D:
					obj = new WheelJoint2D();
					break;
				case ClassIDType.WindZone:
					obj = new WindZone();
					break;
				case ClassIDType.WorldAnchor:
					obj = new WorldAnchor();
					break;
				case ClassIDType.WorldParticleCollider:
					obj = new WorldParticleCollider();
					break;
				default:
					return null;
					
			}
			obj.meta = this;
			reader.Position = Offset;
			
			byte[] buf = reader.ReadBytes(Size);
			obj.Load(buf);
			return obj;
		}
		
		public override string ToString()
		{
			return ClassID.ToString();
		}
	}
}
