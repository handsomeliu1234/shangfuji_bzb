using System;
using System.Runtime.InteropServices;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x0200000E RID: 14
	public sealed class Win32Funcs
	{
		// Token: 0x06000038 RID: 56
		[DllImport("User32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000039 RID: 57
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out Win32Types.Rect lpRect);

		// Token: 0x0600003A RID: 58
		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr hWnd, out Win32Types.Rect lpRect);

		// Token: 0x0600003B RID: 59
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		// Token: 0x0600003C RID: 60
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDc);

		// Token: 0x0600003D RID: 61
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		// Token: 0x0600003E RID: 62
		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr hDc);

		// Token: 0x0600003F RID: 63
		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Token: 0x06000040 RID: 64
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDIBSection(IntPtr hdc, ref Win32Types.BitmapInfo bmi, uint usage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x06000041 RID: 65
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);

		// Token: 0x06000042 RID: 66
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x06000043 RID: 67
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, uint dwRop);

		// Token: 0x06000044 RID: 68
		[DllImport("user32.dll")]
		public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
	}
}
