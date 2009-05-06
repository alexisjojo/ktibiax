using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Keyrox.Shared.Controls;

namespace KTibiaX.UI.Controls {
    public class HostPanel : Panel {

        /// <summary>
        /// Initializes a new instance of the <see cref="HostPanel"/> class.
        /// </summary>
        public HostPanel() {
            SizeChanged += HostPanel_SizeChanged;
        }

        /// <summary>
        /// Handles the SizeChanged event of the HostPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HostPanel_SizeChanged(object sender, EventArgs e) {
            if (CurrentProcess != null) {
                CenterProcesswindow();
            }
        }

        /// <summary>
        /// Track if the application has been created
        /// </summary>
        public bool AppCreated { get; set; }

        /// <summary>
        /// Gets or sets the current process.
        /// </summary>
        /// <value>The current process.</value>
        public Process CurrentProcess { get; set; }

        /// <summary>
        /// Gets or sets the command line.
        /// </summary>
        /// <value>The command line.</value>
        public string CommandLine { get; set; }

        #region "[rgn] Pinvoke Signatures "
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        private static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        [DllImport("user32.dll")]
        static extern IntPtr CreateWindowEx(
           uint dwExStyle,
           string lpClassName,
           string lpWindowName,
           uint dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CLOSE = 0x10;
        private const int WS_CHILD = 0x40000000;

        private enum WindowShowStyle : uint {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        internal const int
          LBS_NOTIFY = 0x00000001,
          HOST_ID = 0x00000002,
          LISTBOX_ID = 0x00000001,
          WS_VSCROLL = 0x00200000,
          WS_BORDER = 0x00800000;
        public const uint WS_POPUP = 0x80000000;
        public const int GWL_EXSTYLE = (-20);
        public const uint WS_EX_CLIENTEDGE = 0x00000200;

        #endregion

        /// <summary>
        /// Force redraw of control when size changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected override void OnSizeChanged(EventArgs e) {
            Invalidate();
            base.OnSizeChanged(e);
            if (CurrentProcess != null) {
                CenterProcesswindow();
            }
        }

        /// <summary>
        /// Hosts the process.
        /// </summary>
        public void HostProcess(Process process) {
            CurrentProcess = process;
            CurrentProcess.WaitForInputIdle(500);
            Invoke(new Callback(delegate() {
                
                SetParent(CurrentProcess.MainWindowHandle, Handle);
                SetWindowLong(CurrentProcess.MainWindowHandle, GWL_STYLE, WS_VISIBLE);
                CenterProcesswindow();

                MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                    SetParent(CurrentProcess.MainWindowHandle, Handle);
                }), 2000);
            }));
        }

        /// <summary>
        /// Fix the Host Process.
        /// </summary>
        public void FixHostProcess() {
            SetParent(CurrentProcess.MainWindowHandle, Handle);
            SetWindowLong(CurrentProcess.MainWindowHandle, GWL_STYLE, WS_VISIBLE);
            CenterProcesswindow();
        }

        /// <summary>
        /// Hosts the process.
        /// </summary>
        public void HostProcess() {
            CurrentProcess.WaitForInputIdle(500);
            if (CurrentProcess != null) {
                HostProcess(CurrentProcess);
            }
            else { throw new ArgumentException("Current Process is null!"); }
        }

        /// <summary>
        /// Closes the process.
        /// </summary>
        public void CloseProcess() {
            try {
                if (CurrentProcess != null && !CurrentProcess.HasExited) {
                    PostMessage(CurrentProcess.MainWindowHandle, WM_CLOSE, 0, 0);
                    CurrentProcess = null;
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e) {
            CloseProcess();
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// Update display of the executable
        /// </summary>
        /// <param name="e">Not used</param>
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            if (CurrentProcess != null) {
                CenterProcesswindow();
            }
        }

        /// <summary>
        /// Centers the processwindow.
        /// </summary>
        public void CenterProcesswindow() {
            try {
                MoveWindow(CurrentProcess.MainWindowHandle, -5, -5, Width + 5, Height + 5, true);
                long style = GetWindowLong(CurrentProcess.MainWindowHandle, Convert.ToInt32(GWL_STYLE));
                long exStyle = GetWindowLong(CurrentProcess.MainWindowHandle, Convert.ToInt32(GWL_EXSTYLE));
                style &= ~WS_BORDER;
                exStyle &= ~WS_EX_CLIENTEDGE;
                SetWindowLong(CurrentProcess.MainWindowHandle, GWL_STYLE, style);
                SetWindowLong(CurrentProcess.MainWindowHandle, GWL_EXSTYLE, exStyle);
                Invalidate();
            }
            catch { }
        }

    }

}