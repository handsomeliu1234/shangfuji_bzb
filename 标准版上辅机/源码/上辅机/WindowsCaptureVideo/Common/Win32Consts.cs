using System;
using System.ComponentModel;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x0200000D RID: 13
	public sealed class Win32Consts
	{
		// Token: 0x02000016 RID: 22
		public enum DibColorMode : uint
		{
			// Token: 0x0400014A RID: 330
			DIB_RGB_COLORS,
			// Token: 0x0400014B RID: 331
			DIB_PAL_COLORS,
			// Token: 0x0400014C RID: 332
			DIB_PAL_INDICES
		}

		// Token: 0x02000017 RID: 23
		public enum BitmapCompressionMode : uint
		{
			// Token: 0x0400014E RID: 334
			BI_RGB,
			// Token: 0x0400014F RID: 335
			BI_RLE8,
			// Token: 0x04000150 RID: 336
			BI_RLE4,
			// Token: 0x04000151 RID: 337
			BI_BITFIELDS,
			// Token: 0x04000152 RID: 338
			BI_JPEG,
			// Token: 0x04000153 RID: 339
			BI_PNG
		}

		// Token: 0x02000018 RID: 24
		public enum RasterOperationMode : uint
		{
			// Token: 0x04000155 RID: 341
			SRCCOPY = 13369376U,
			// Token: 0x04000156 RID: 342
			SRCPAINT = 15597702U,
			// Token: 0x04000157 RID: 343
			SRCAND = 8913094U,
			// Token: 0x04000158 RID: 344
			SRCINVERT = 6684742U,
			// Token: 0x04000159 RID: 345
			SRCERASE = 4457256U,
			// Token: 0x0400015A RID: 346
			NOTSRCCOPY = 3342344U,
			// Token: 0x0400015B RID: 347
			NOTSRCERASE = 1114278U,
			// Token: 0x0400015C RID: 348
			MERGECOPY = 12583114U,
			// Token: 0x0400015D RID: 349
			MERGEPAINT = 12255782U,
			// Token: 0x0400015E RID: 350
			PATCOPY = 15728673U,
			// Token: 0x0400015F RID: 351
			PATPAINT = 16452105U,
			// Token: 0x04000160 RID: 352
			PATINVERT = 5898313U,
			// Token: 0x04000161 RID: 353
			DSTINVERT = 5570569U,
			// Token: 0x04000162 RID: 354
			BLACKNESS = 66U,
			// Token: 0x04000163 RID: 355
			WHITENESS = 16711778U,
			// Token: 0x04000164 RID: 356
			CAPTUREBLT = 1073741824U
		}

		// Token: 0x02000019 RID: 25
		public enum PrintWindowMode : uint
		{
			// Token: 0x04000166 RID: 358
			[Description("Only the client area of the window is copied to hdcBlt. By default, the entire window is copied.")]
			PW_CLIENTONLY = 1U,
			// Token: 0x04000167 RID: 359
			[Description("works on windows that use DirectX or DirectComposition")]
			PW_RENDERFULLCONTENT
		}
	}
}
