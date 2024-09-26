using System;
using System.Runtime.InteropServices;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x02000009 RID: 9
	public class NativeMethods
	{
		// Token: 0x06000029 RID: 41
		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

		// Token: 0x0600002A RID: 42
		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		private static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

		// Token: 0x0600002B RID: 43
		[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
		private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600002C RID: 44
		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
		private static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600002D RID: 45
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

		// Token: 0x0600002E RID: 46
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

		// Token: 0x0600002F RID: 47 RVA: 0x00003130 File Offset: 0x00001330
		public static IntPtr GetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex)
		{
			bool flag = IntPtr.Size == 8;
			bool flag2 = flag;
			IntPtr result;
			if (flag2)
			{
				result = NativeMethods.GetWindowLong64(hWnd, (int)nIndex);
			}
			else
			{
				result = NativeMethods.GetWindowLong32(hWnd, (int)nIndex);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003168 File Offset: 0x00001368
		public static IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong)
		{
			bool flag = IntPtr.Size == 8;
			bool flag2 = flag;
			IntPtr result;
			if (flag2)
			{
				result = NativeMethods.SetWindowLong64(hWnd, (int)nIndex, dwNewLong);
			}
			else
			{
				result = NativeMethods.SetWindowLong32(hWnd, (int)nIndex, dwNewLong);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031A4 File Offset: 0x000013A4
		public static IntPtr SetWindowStyles(IntPtr hWnd, WindowStyles ws)
		{
			IntPtr windowLongPtr = NativeMethods.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE);
			return NativeMethods.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE, IntPtrEnumHelper.SetFlag(windowLongPtr, ws));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031D4 File Offset: 0x000013D4
		public static IntPtr UnsetWindowStyles(IntPtr hWnd, WindowStyles ws)
		{
			IntPtr windowLongPtr = NativeMethods.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE);
			return NativeMethods.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_STYLE, IntPtrEnumHelper.UnsetFlag(windowLongPtr, ws));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003204 File Offset: 0x00001404
		public static IntPtr SetWindowStylesEx(IntPtr hWnd, WindowStylesEx wse)
		{
			IntPtr windowLongPtr = NativeMethods.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE);
			return NativeMethods.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE, IntPtrEnumHelper.SetFlag(windowLongPtr, wse));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003234 File Offset: 0x00001434
		public static IntPtr UnsetWindowStylesEx(IntPtr hWnd, WindowStylesEx wse)
		{
			IntPtr windowLongPtr = NativeMethods.GetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE);
			return NativeMethods.SetWindowLongPtr(hWnd, WindowLongFlags.GWL_EXSTYLE, IntPtrEnumHelper.UnsetFlag(windowLongPtr, wse));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003264 File Offset: 0x00001464
		public static bool SetWindowPos(IntPtr hWnd, SpecialWindowHandles hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags)
		{
			return NativeMethods.SetWindowPos(hWnd, new IntPtr((int)hWndInsertAfter), x, y, cx, cy, uFlags);
		}
	}
}
