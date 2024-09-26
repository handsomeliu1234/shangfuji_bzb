using AForge.Video.FFMPEG;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsCaptureVideo.Common;

namespace WindowsCaptureVideo
{
    public class CaptureVideoFFpeng
    {
        private string DirName;

        private System.Threading.Timer timer1;

        private VideoFileWriter videoFileWriter;

        private int fps = 15;

        private double TimerInterval = 1000.0;

        private bool savePicture;

        private string videoname = "";

        public static object sync = new object();

        //传递过来的界面
        private Control mCtrl;

        //传递过来的句柄
        private IntPtr _hWnd = IntPtr.Zero;

        private IntPtr _hScrDc = IntPtr.Zero;

        private IntPtr _hMemDc = IntPtr.Zero;

        private IntPtr _hBitmap = IntPtr.Zero;

        private IntPtr _hOldBitmap = IntPtr.Zero;

        private IntPtr _bitsPtr = IntPtr.Zero;

        private Win32Types.Rect _windowRect;

        private Win32Types.Rect _clientRect;

        private bool isStarting = false;

        private int maxday = 100;

        public int Fps
        {
            get
            {
                return this.fps;
            }
            set
            {
                fps = value;
            }
        }

        public CaptureVideoFFpeng(string dirname, int fps = 10, int MaxSaveDay = 100, bool savePicture = false)
        {
            DirName = dirname;
            Fps = fps;
            this.savePicture = savePicture;
            maxday = MaxSaveDay;
            TimerInterval = 1000.0 / (double)fps;
            videoFileWriter = new VideoFileWriter();
        }

        public bool IsStarting
        {
            get
            {
                return this.isStarting;
            }
            set
            {
                isStarting = value;
            }
        }

        public int MaxDay
        {
            get
            {
                return maxday;
            }
            set
            {
                maxday = value;
            }
        }

        public void ReStart()
        {
            Cleanup();
            Thread.Sleep(1000);
            Start(_hWnd, mCtrl);
        }

        public void Start(IntPtr handle, Control ctrl)
        {
            mCtrl = ctrl;
            _hWnd = handle;
            bool flag = !Win32Funcs.GetWindowRect(_hWnd, out _windowRect) || !Win32Funcs.GetClientRect(_hWnd, out _clientRect) || IsStarting;

            if (!flag)
            {
                _hScrDc = Win32Funcs.GetWindowDC(_hWnd);
                _hMemDc = Win32Funcs.CreateCompatibleDC(_hScrDc);
                _hBitmap = Win32Funcs.CreateCompatibleBitmap(_hScrDc, _windowRect.Width, _windowRect.Height);
                _hOldBitmap = Win32Funcs.SelectObject(_hMemDc, _hBitmap);
                timer1 = new System.Threading.Timer(new TimerCallback(CaptureFunc), null, 0, 1000 / fps);
                IsStarting = true;
            }
        }

        private void CaptureFunc(object state)
        {
            try
            {
                object obj = sync;
                lock (obj)
                {
                    if (isStarting)
                    {
                        Bitmap bitmap = Capture();
                        DateTime now = DateTime.Now;
                        string text = Path.Combine(CreateDir(now), now.ToString("yyyyMMdd-HH")) + ".avi";
                        bool flag = !text.Equals(videoname);
                        if (flag)
                        {
                            videoname = text;
                            bool flagSeach = File.Exists(videoname);
                            if (flagSeach)
                            {
                                text = text.Substring(0, text.Length - 4) + now.ToString("mmss") + ".avi";
                            }
                            bool flagOpen = !videoFileWriter.IsOpen;
                            if (flagOpen)
                            {
                                videoFileWriter.Open(text, _windowRect.Width, _windowRect.Height, Fps, 0);
                            }
                            else
                            {
                                videoFileWriter.Close();
                                DeleteDir(now);
                                videoFileWriter.Open(text, _windowRect.Width, _windowRect.Height, Fps, 0);
                            }
                        }
                        bool flagResult = bitmap != null && videoFileWriter.IsOpen;

                        Task.Run(() =>
                        {
                            if (flagResult)
                            {
                                if (savePicture)
                                {
                                    bitmap.Save(text.Substring(0, text.Length - 4) + now.ToString("mmss") + ".png");
                                }
                                videoFileWriter.WriteVideoFrame(bitmap);
                                bitmap.Dispose();
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public Bitmap Capture()
        {
            try
            {
                Bitmap bitmap = null;
                bool flag = Win32Funcs.BitBlt(_hMemDc, 0, 0, _windowRect.Width, _windowRect.Height, _hScrDc, 0, 0, 13369376);
                Action method = delegate ()
                {
                    bitmap = DrawToBitmap(mCtrl);
                };
                mCtrl.Invoke(method);
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Bitmap DrawToBitmap(Control ctrl)
        {
            Bitmap bitmap = new Bitmap(_windowRect.Width, _windowRect.Height);
            ctrl.DrawToBitmap(bitmap, new Rectangle(0, 0, _windowRect.Width, _windowRect.Height));
            return bitmap;
        }

        private Bitmap PrintClientRectangleToImage(Control ctrl)
        {
            Bitmap bitmap = new Bitmap(_windowRect.Width, _windowRect.Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                IntPtr hdc = graphics.GetHdc();
                using (Graphics graphics2 = Graphics.FromHwnd(ctrl.Handle))
                {
                    IntPtr hdc2 = graphics2.GetHdc();
                    BitBlt(hdc, 0, 0, _windowRect.Width, _windowRect.Height, hdc2, 0, 0, 13369376);
                    graphics2.ReleaseHdc(hdc2);
                }
                graphics.ReleaseHdc(hdc);
            }
            return bitmap;
        }

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

        public void Cleanup()
        {
            bool flag = _hBitmap.Equals(IntPtr.Zero);
            if (!flag)
            {
                Win32Funcs.SelectObject(_hMemDc, _hOldBitmap);
                Win32Funcs.DeleteObject(_hBitmap);
                Win32Funcs.DeleteDC(_hMemDc);
                Win32Funcs.ReleaseDC(_hWnd, _hScrDc);
                //_hWnd = IntPtr.Zero;
                _hScrDc = IntPtr.Zero;
                _hMemDc = IntPtr.Zero;
                _hBitmap = IntPtr.Zero;
                _hOldBitmap = IntPtr.Zero;
                _bitsPtr = IntPtr.Zero;
                bool flagObj = videoFileWriter != null && videoFileWriter.IsOpen;
                if (flagObj)
                {
                    videoFileWriter.Close();
                    DeleteDir(DateTime.Now);
                    isStarting = false;
                }
            }
            videoname = "";
        }

        private string CreateDir(DateTime dt)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), string.Concat(new string[]
            {
                DirName,
                "//",
                dt.ToString("yyyy-MM-dd"),
                "//",
                dt.ToString("HH")
            })));
            bool flag = !directoryInfo.Exists;
            if (flag)
            {
                directoryInfo.Create();
            }
            return directoryInfo.FullName;
        }

        private void DeleteDir(DateTime dt)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), DirName));
            bool exists = directoryInfo.Exists;
            if (exists)
            {
                bool flag = maxday < 30;
                if (flag)
                {
                    maxday = 30;
                }
                (from s in directoryInfo.GetDirectories()
                 where s.CreationTime < dt.AddDays(-(double)this.maxday)
                 select s).ToList().ForEach(delegate (DirectoryInfo s)
                 {
                     s.Delete(true);
                 });
            }
        }
    }
}