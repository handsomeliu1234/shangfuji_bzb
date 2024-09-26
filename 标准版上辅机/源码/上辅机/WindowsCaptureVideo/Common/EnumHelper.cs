using System;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x02000007 RID: 7
	internal static class EnumHelper
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000306C File Offset: 0x0000126C
		public static bool HasFlag(long val, long flag)
		{
			return (val & flag) == flag;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003084 File Offset: 0x00001284
		public static long SetFlag(long val, long flag)
		{
			return val | flag;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000309C File Offset: 0x0000129C
		public static long UnsetFlag(long val, long flag)
		{
			return val & ~flag;
		}
	}
}
