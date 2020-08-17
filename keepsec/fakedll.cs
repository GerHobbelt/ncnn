/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2019/7/4
 * Time: 上午 11:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace UnityEngine
{
	
	
	public static class Debug
	{
		public static void Log(object message)
		{
			Console.WriteLine(message);
			//Console.ReadKey();
		}
	}
	
	/// <summary>
	///   <para>Format used when creating textures from scripts.</para>
	/// </summary>
	// Token: 0x02000636 RID: 1590
	public enum TextureFormat
	{
		/// <summary>
		///   <para>Alpha-only texture format.</para>
		/// </summary>
		// Token: 0x0400146F RID: 5231
		Alpha8 = 1,
		/// <summary>
		///   <para>A 16 bits/pixel texture format. Texture stores color with an alpha channel.</para>
		/// </summary>
		// Token: 0x04001470 RID: 5232
		ARGB4444,
		/// <summary>
		///   <para>Color texture format, 8-bits per channel.</para>
		/// </summary>
		// Token: 0x04001471 RID: 5233
		RGB24,
		/// <summary>
		///   <para>Color with alpha texture format, 8-bits per channel.</para>
		/// </summary>
		// Token: 0x04001472 RID: 5234
		RGBA32,
		/// <summary>
		///   <para>Color with alpha texture format, 8-bits per channel.</para>
		/// </summary>
		// Token: 0x04001473 RID: 5235
		ARGB32,
		/// <summary>
		///   <para>A 16 bit color texture format.</para>
		/// </summary>
		// Token: 0x04001474 RID: 5236
		RGB565 = 7,
		/// <summary>
		///   <para>Single channel (R) texture format, 16 bit integer.</para>
		/// </summary>
		// Token: 0x04001475 RID: 5237
		R16 = 9,
		/// <summary>
		///   <para>Compressed color texture format.</para>
		/// </summary>
		// Token: 0x04001476 RID: 5238
		DXT1,
		/// <summary>
		///   <para>Compressed color with alpha channel texture format.</para>
		/// </summary>
		// Token: 0x04001477 RID: 5239
		DXT5 = 12,
		/// <summary>
		///   <para>Color and alpha  texture format, 4 bit per channel.</para>
		/// </summary>
		// Token: 0x04001478 RID: 5240
		RGBA4444,
		/// <summary>
		///   <para>Color with alpha texture format, 8-bits per channel.</para>
		/// </summary>
		// Token: 0x04001479 RID: 5241
		BGRA32,
		/// <summary>
		///   <para>Scalar (R)  texture format, 16 bit floating point.</para>
		/// </summary>
		// Token: 0x0400147A RID: 5242
		RHalf,
		/// <summary>
		///   <para>Two color (RG)  texture format, 16 bit floating point per channel.</para>
		/// </summary>
		// Token: 0x0400147B RID: 5243
		RGHalf,
		/// <summary>
		///   <para>RGB color and alpha texture format, 16 bit floating point per channel.</para>
		/// </summary>
		// Token: 0x0400147C RID: 5244
		RGBAHalf,
		/// <summary>
		///   <para>Scalar (R) texture format, 32 bit floating point.</para>
		/// </summary>
		// Token: 0x0400147D RID: 5245
		RFloat,
		/// <summary>
		///   <para>Two color (RG)  texture format, 32 bit floating point per channel.</para>
		/// </summary>
		// Token: 0x0400147E RID: 5246
		RGFloat,
		/// <summary>
		///   <para>RGB color and alpha texture format,  32-bit floats per channel.</para>
		/// </summary>
		// Token: 0x0400147F RID: 5247
		RGBAFloat,
		/// <summary>
		///   <para>A format that uses the YUV color space and is often used for video encoding or playback.</para>
		/// </summary>
		// Token: 0x04001480 RID: 5248
		YUY2,
		/// <summary>
		///   <para>RGB HDR format, with 9 bit mantissa per channel and a 5 bit shared exponent.</para>
		/// </summary>
		// Token: 0x04001481 RID: 5249
		RGB9e5Float,
		/// <summary>
		///   <para>Compressed one channel (R) texture format.</para>
		/// </summary>
		// Token: 0x04001482 RID: 5250
		BC4 = 26,
		/// <summary>
		///   <para>Compressed two-channel (RG) texture format.</para>
		/// </summary>
		// Token: 0x04001483 RID: 5251
		BC5,
		/// <summary>
		///   <para>HDR compressed color texture format.</para>
		/// </summary>
		// Token: 0x04001484 RID: 5252
		BC6H = 24,
		/// <summary>
		///   <para>High quality compressed color texture format.</para>
		/// </summary>
		// Token: 0x04001485 RID: 5253
		BC7,
		/// <summary>
		///   <para>Compressed color texture format with Crunch compression for smaller storage sizes.</para>
		/// </summary>
		// Token: 0x04001486 RID: 5254
		DXT1Crunched = 28,
		/// <summary>
		///   <para>Compressed color with alpha channel texture format with Crunch compression for smaller storage sizes.</para>
		/// </summary>
		// Token: 0x04001487 RID: 5255
		DXT5Crunched,
		/// <summary>
		///   <para>PowerVR (iOS) 2 bits/pixel compressed color texture format.</para>
		/// </summary>
		// Token: 0x04001488 RID: 5256
		PVRTC_RGB2,
		/// <summary>
		///   <para>PowerVR (iOS) 2 bits/pixel compressed with alpha channel texture format.</para>
		/// </summary>
		// Token: 0x04001489 RID: 5257
		PVRTC_RGBA2,
		/// <summary>
		///   <para>PowerVR (iOS) 4 bits/pixel compressed color texture format.</para>
		/// </summary>
		// Token: 0x0400148A RID: 5258
		PVRTC_RGB4,
		/// <summary>
		///   <para>PowerVR (iOS) 4 bits/pixel compressed with alpha channel texture format.</para>
		/// </summary>
		// Token: 0x0400148B RID: 5259
		PVRTC_RGBA4,
		/// <summary>
		///   <para>ETC (GLES2.0) 4 bits/pixel compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x0400148C RID: 5260
		ETC_RGB4,
		// Token: 0x0400148D RID: 5261
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.ATC_RGB4 has been deprecated. Use ETC_RGB4 instead (UnityUpgradable) -> ETC_RGB4", true)]
		ATC_RGB4 = -127,
		// Token: 0x0400148E RID: 5262
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.ATC_RGBA8 has been deprecated. Use ETC2_RGBA8 instead (UnityUpgradable) -> ETC2_RGBA8", true)]
		ATC_RGBA8 = -127,
		/// <summary>
		///   <para>ETC2  EAC (GL ES 3.0) 4 bitspixel compressed unsigned single-channel texture format.</para>
		/// </summary>
		// Token: 0x0400148F RID: 5263
		EAC_R = 41,
		/// <summary>
		///   <para>ETC2  EAC (GL ES 3.0) 4 bitspixel compressed signed single-channel texture format.</para>
		/// </summary>
		// Token: 0x04001490 RID: 5264
		EAC_R_SIGNED,
		/// <summary>
		///   <para>ETC2  EAC (GL ES 3.0) 8 bitspixel compressed unsigned dual-channel (RG) texture format.</para>
		/// </summary>
		// Token: 0x04001491 RID: 5265
		EAC_RG,
		/// <summary>
		///   <para>ETC2  EAC (GL ES 3.0) 8 bitspixel compressed signed dual-channel (RG) texture format.</para>
		/// </summary>
		// Token: 0x04001492 RID: 5266
		EAC_RG_SIGNED,
		/// <summary>
		///   <para>ETC2 (GL ES 3.0) 4 bits/pixel compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x04001493 RID: 5267
		ETC2_RGB,
		/// <summary>
		///   <para>ETC2 (GL ES 3.0) 4 bits/pixel RGB+1-bit alpha texture format.</para>
		/// </summary>
		// Token: 0x04001494 RID: 5268
		ETC2_RGBA1,
		/// <summary>
		///   <para>ETC2 (GL ES 3.0) 8 bits/pixel compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x04001495 RID: 5269
		ETC2_RGBA8,
		/// <summary>
		///   <para>ASTC (4x4 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x04001496 RID: 5270
		ASTC_RGB_4x4,
		/// <summary>
		///   <para>ASTC (5x5 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x04001497 RID: 5271
		ASTC_RGB_5x5,
		/// <summary>
		///   <para>ASTC (6x6 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x04001498 RID: 5272
		ASTC_RGB_6x6,
		/// <summary>
		///   <para>ASTC (8x8 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x04001499 RID: 5273
		ASTC_RGB_8x8,
		/// <summary>
		///   <para>ASTC (10x10 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x0400149A RID: 5274
		ASTC_RGB_10x10,
		/// <summary>
		///   <para>ASTC (12x12 pixel block in 128 bits) compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x0400149B RID: 5275
		ASTC_RGB_12x12,
		/// <summary>
		///   <para>ASTC (4x4 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x0400149C RID: 5276
		ASTC_RGBA_4x4,
		/// <summary>
		///   <para>ASTC (5x5 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x0400149D RID: 5277
		ASTC_RGBA_5x5,
		/// <summary>
		///   <para>ASTC (6x6 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x0400149E RID: 5278
		ASTC_RGBA_6x6,
		/// <summary>
		///   <para>ASTC (8x8 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x0400149F RID: 5279
		ASTC_RGBA_8x8,
		/// <summary>
		///   <para>ASTC (10x10 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x040014A0 RID: 5280
		ASTC_RGBA_10x10,
		/// <summary>
		///   <para>ASTC (12x12 pixel block in 128 bits) compressed RGBA texture format.</para>
		/// </summary>
		// Token: 0x040014A1 RID: 5281
		ASTC_RGBA_12x12,
		/// <summary>
		///   <para>ETC 4 bits/pixel compressed RGB texture format.</para>
		/// </summary>
		// Token: 0x040014A2 RID: 5282
		//[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGB4_3DS,
		/// <summary>
		///   <para>ETC 4 bitspixel RGB + 4 bitspixel Alpha compressed texture format.</para>
		/// </summary>
		// Token: 0x040014A3 RID: 5283
		//[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGBA8_3DS,
		/// <summary>
		///   <para>Two color (RG) texture format, 8-bits per channel.</para>
		/// </summary>
		// Token: 0x040014A4 RID: 5284
		RG16,
		/// <summary>
		///   <para>Single channel (R) texture format, 8 bit integer.</para>
		/// </summary>
		// Token: 0x040014A5 RID: 5285
		R8,
		/// <summary>
		///   <para>Compressed color texture format with Crunch compression for smaller storage sizes.</para>
		/// </summary>
		// Token: 0x040014A6 RID: 5286
		ETC_RGB4Crunched,
		/// <summary>
		///   <para>Compressed color with alpha channel texture format using Crunch compression for smaller storage sizes.</para>
		/// </summary>
		// Token: 0x040014A7 RID: 5287
		ETC2_RGBA8Crunched,
		// Token: 0x040014A8 RID: 5288
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.PVRTC_2BPP_RGB has been deprecated. Use PVRTC_RGB2 instead (UnityUpgradable) -> PVRTC_RGB2", true)]
		PVRTC_2BPP_RGB = -127,
		// Token: 0x040014A9 RID: 5289
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.PVRTC_2BPP_RGBA has been deprecated. Use PVRTC_RGBA2 instead (UnityUpgradable) -> PVRTC_RGBA2", true)]
		PVRTC_2BPP_RGBA = -127,
		// Token: 0x040014AA RID: 5290
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.PVRTC_4BPP_RGB has been deprecated. Use PVRTC_RGB4 instead (UnityUpgradable) -> PVRTC_RGB4", true)]
		PVRTC_4BPP_RGB = -127,
		// Token: 0x040014AB RID: 5291
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//[Obsolete("Enum member TextureFormat.PVRTC_4BPP_RGBA has been deprecated. Use PVRTC_RGBA4 instead (UnityUpgradable) -> PVRTC_RGBA4", true)]
		PVRTC_4BPP_RGBA = -127
	}



	public struct BoneWeight : IEquatable<BoneWeight>
	{
		
		public float weight0 {
			get {
				return this.m_Weight0;
			}
			set {
				this.m_Weight0 = value;
			}
		}

		
		public float weight1 {
			get {
				return this.m_Weight1;
			}
			set {
				this.m_Weight1 = value;
			}
		}

		
		public float weight2 {
			get {
				return this.m_Weight2;
			}
			set {
				this.m_Weight2 = value;
			}
		}

		
		public float weight3 {
			get {
				return this.m_Weight3;
			}
			set {
				this.m_Weight3 = value;
			}
		}

		
		public int boneIndex0 {
			get {
				return this.m_BoneIndex0;
			}
			set {
				this.m_BoneIndex0 = value;
			}
		}

		
		public int boneIndex1 {
			get {
				return this.m_BoneIndex1;
			}
			set {
				this.m_BoneIndex1 = value;
			}
		}

		
		public int boneIndex2 {
			get {
				return this.m_BoneIndex2;
			}
			set {
				this.m_BoneIndex2 = value;
			}
		}

		
		public int boneIndex3 {
			get {
				return this.m_BoneIndex3;
			}
			set {
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000178D0 File Offset: 0x00015AD0
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ this.boneIndex1.GetHashCode() << 2 ^ this.boneIndex2.GetHashCode() >> 2 ^ this.boneIndex3.GetHashCode() >> 1 ^ this.weight0.GetHashCode() << 5 ^ this.weight1.GetHashCode() << 4 ^ this.weight2.GetHashCode() >> 4 ^ this.weight3.GetHashCode() >> 3;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000179A0 File Offset: 0x00015BA0
		public override bool Equals(object other)
		{
			return other is BoneWeight && this.Equals((BoneWeight)other);
		}

		

		// Token: 0x06001080 RID: 4224 RVA: 0x00017A9C File Offset: 0x00015C9C
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00017B48 File Offset: 0x00015D48
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(BoneWeight other)
		{
			bool result;
			if (this.boneIndex0.Equals(other.boneIndex0) && this.boneIndex1.Equals(other.boneIndex1) && this.boneIndex2.Equals(other.boneIndex2) && this.boneIndex3.Equals(other.boneIndex3)) {
				Vector4 vector = new Vector4(this.weight0, this.weight1, this.weight2, this.weight3);
				result = vector.Equals(new Vector4(other.weight0, other.weight1, other.weight2, other.weight3));
			} else {
				result = false;
			}
			return result;
		}
		
		
		
		private float m_Weight0;

		
		private float m_Weight1;

		
		private float m_Weight2;

		
		private float m_Weight3;

		
		private int m_BoneIndex0;

		
		private int m_BoneIndex1;

		
		private int m_BoneIndex2;

		
		private int m_BoneIndex3;
	}



	public struct Quaternion
	{
		public float x;

		// Token: 0x040000E0 RID: 224
		public float y;

		// Token: 0x040000E1 RID: 225
		public float z;

		// Token: 0x040000E2 RID: 226
		public float w;
		public override string ToString()
		{
			return String.Format("{0:G5}, {1:G5}, {2:G5}, {3:G5}", new object[] {
				x,
				y,
				z,
				w,
			});
		}
	}
	
	public struct Vector2
	{
		public float x;
		public float y;
		public override string ToString()
		{
			return String.Format("{0:G5}, {1:G5}", new object[] {
				x,
				y,

			});
		}
		
	}
	public struct Vector3
	{
		public float x;

		
		public float y;

		
		public float z;
		
		public override string ToString()
		{
			return String.Format("{0:G5}, {1:G5}, {2:G5}", new object[] {
				x,
				y,
				z
			});
		}
	}
	
	public struct Vector4
	{
		public float x;

		
		public float y;

		
		public float z;
		public float w;
		
		public override string ToString()
		{
			return String.Format("{0:G5}, {1:G5}, {2:G5}, {3:G5}", new object[] {
				x,
				y,
				z,
				w,
			});
		}
		
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
	}
	
	public struct Color
	{
		public float r;

		
		public float g;

		
		public float b;
		public float a;
	}
	
	public struct Matrix4x4
	{
		//[NativeName("m_Data[0]")]
		public float m00;

		// Token: 0x04001903 RID: 6403
		//[NativeName("m_Data[1]")]
		public float m10;

		// Token: 0x04001904 RID: 6404
		//[NativeName("m_Data[2]")]
		public float m20;

		// Token: 0x04001905 RID: 6405
		//[NativeName("m_Data[3]")]
		public float m30;

		// Token: 0x04001906 RID: 6406
		//[NativeName("m_Data[4]")]
		public float m01;

		// Token: 0x04001907 RID: 6407
		//[NativeName("m_Data[5]")]
		public float m11;

		// Token: 0x04001908 RID: 6408
		//[NativeName("m_Data[6]")]
		public float m21;

		// Token: 0x04001909 RID: 6409
		//[NativeName("m_Data[7]")]
		public float m31;

		// Token: 0x0400190A RID: 6410
		//[NativeName("m_Data[8]")]
		public float m02;

		// Token: 0x0400190B RID: 6411
		//[NativeName("m_Data[9]")]
		public float m12;

		// Token: 0x0400190C RID: 6412
		//[NativeName("m_Data[10]")]
		public float m22;

		// Token: 0x0400190D RID: 6413
		//[NativeName("m_Data[11]")]
		public float m32;

		// Token: 0x0400190E RID: 6414
		//[NativeName("m_Data[12]")]
		public float m03;

		// Token: 0x0400190F RID: 6415
		//[NativeName("m_Data[13]")]
		public float m13;

		// Token: 0x04001910 RID: 6416
		//[NativeName("m_Data[14]")]
		public float m23;

		// Token: 0x04001911 RID: 6417
		//[NativeName("m_Data[15]")]
		public float m33;
	}
}
