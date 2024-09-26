using System;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x02000008 RID: 8
	internal static class IntPtrEnumHelper
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000030B4 File Offset: 0x000012B4
		public static bool HasFlags(IntPtr val, object flag)
		{
			return EnumHelper.HasFlag(val.ToInt64(), (long)flag);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000030D8 File Offset: 0x000012D8
		public static IntPtr SetFlag(IntPtr val, object flag)
		{
			return new IntPtr(EnumHelper.SetFlag(val.ToInt64(), (long)flag));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003104 File Offset: 0x00001304
		public static IntPtr UnsetFlag(IntPtr val, object flag)
		{
			return new IntPtr(EnumHelper.UnsetFlag(val.ToInt64(), (long)flag));
		}
	}
}
