using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowGUI
{
    /// <summary>
    /// 32-bit DLL to allow calling of system functions that cannot be called directly
    /// </summary>
    public class WindowGUIEditor
    {

        //Methods to allow calling of these P/Invokes from the 64 bit application
        #region Methods

        public IntPtr WindowFromPointm(Int32 x, Int32 y)
        {
            return WindowFromPoint(new Point(x, y));
        }

        public IntPtr ChildWindowFromPointm(IntPtr hWndParent, Point Point)
        {
            return ChildWindowFromPoint(hWndParent, Point);
        }

        public IntPtr ChildWindowFromPointExm(IntPtr hWndParent, Point pt, uint uFlags)
        {
            return ChildWindowFromPointEx(hWndParent, pt, uFlags);
        }

        public IntPtr RealChildWindowFromPointm(IntPtr hwndParent, Point ptParentClientCoords)
        {
            return RealChildWindowFromPoint(hwndParent, ptParentClientCoords);
        }

        public bool ShowWindowAsyncm(IntPtr hWnd, int nCmdShow)
        {
            return ShowWindowAsync(hWnd, nCmdShow);
        }

        public bool SetForegroundWindowm(IntPtr hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

        public bool IsIconicm(IntPtr hWnd)
        {
            return IsIconic(hWnd);
        }

        public bool IsZoomedm(IntPtr hWnd)
        {
            return IsZoomed(hWnd);
        }

        public IntPtr GetParentm(IntPtr hwnd)
        {
            return GetParent(hwnd);
        }

        public int GetWindowTextm(IntPtr hWnd, StringBuilder lpString, int nMaxCount)
        {
            return GetWindowText(hWnd, lpString, nMaxCount);
        }

        public int GetWindowTextLengthm(IntPtr hWnd)
        {
            return GetWindowTextLength(hWnd);
        }

        public bool SetWindowTextm(IntPtr hWnd, string lpString)
        {
            return SetWindowText(hWnd, lpString);
        }

        public int GetClassNamem(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount)
        {
            return GetClassName(hWnd, lpClassName, nMaxCount);
        }

        public uint GetWindowThreadProcessIdm(IntPtr hWnd, out uint lpdwProcessId)
        {
            return GetWindowThreadProcessId(hWnd, out lpdwProcessId);
        }

        public bool SetWindowPosm(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags)
        {
            return SetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags);
        }

        public bool GetWindowRectm(IntPtr hWnd, out RECT lpRect)
        {
            return GetWindowRect(hWnd, out lpRect);
        }

        public bool GetWindowPlacementm(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl)
        {
            return GetWindowPlacement(hWnd, out lpwndpl);
        }

        public int ShowWindowm(IntPtr hwnd, int nCmdShow)
        {
            return ShowWindow(hwnd, nCmdShow);
        }

        public bool EnableWindowm(IntPtr HWnd, bool Enabled)
        {
            return EnableWindow(HWnd, Enabled);
        }

        public bool PostMessagem(IntPtr handleWnd, UInt32 Msg, Int32 wParam, Int32 lParam)
        {
            return PostMessage(handleWnd, Msg, wParam, lParam);
        }

        #endregion

        #region P/Invokes
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static internal extern IntPtr WindowFromPoint(Point Point);
        [DllImport("user32.dll")]
        static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point Point);
        [DllImport("user32.dll")]
        static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, Point pt, uint uFlags);
        [DllImport("user32.dll")]
        static extern IntPtr RealChildWindowFromPoint(IntPtr hwndParent, Point ptParentClientCoords);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hwnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("USER32.DLL")]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);


        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr HWnd, bool Enabled);
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr handleWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        #endregion

        #region Structures
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public enum CommonEvents
        {
            BN_CLICKED,
            WM_ACTIVATE,
            WM_SETFOCUS,
            WM_KEYDOWN,
            WM_MOUSEMOVE,
            WM_LBUTTONDOWN,
            WM_QUIT,
        }

        /// <summary>Enumeration of the different ways of showing a window using 
        /// ShowWindow</summary>
        public enum WindowShowStyle
        {
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

        #endregion

    }
}