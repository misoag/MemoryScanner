using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Aecial.Conversions;
using Aecial.MemoryScanner;

namespace AecialEngine.Tools
{
    //TODO: redo how things work, lots of inefficiency in loop cycles and event handlers
    //EnumChildWindows() -- this is fancy
    public partial class GUIEditor : Form
    {

        #region Variables
        private InputHook.UserActivityHook actHook;

        private WindowGUIEditor.CommonEvents CommonEvents;
        private WindowGUIEditor WindowEditor;

        private bool GetData = false;

        private IntPtr Child_hwnd = IntPtr.Zero;
        private IntPtr Parent_hwnd = IntPtr.Zero;

        private List<IntPtr> ignoreC = new List<IntPtr>();
        private List<IntPtr> ignoreP = new List<IntPtr>();

        private string childText = "";

        #endregion

        #region Initialization

        public GUIEditor()
        {
            InitializeComponent();
        }

        private void GUIEditor_Load(object sender, EventArgs e)
        {
            //Got to call show & focus because of my awful loop
            this.Show();
            this.Focus();

            WindowEditor = new WindowGUIEditor();
            actHook = new InputHook.UserActivityHook(true, true); // crate an instance with global hooks
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.KeyDown += new KeyEventHandler(GatherData);
            actHook.KeyUp += new KeyEventHandler(GatherStop);

            //Key capturing broke, this magically fixed it
            this.KeyPreview = true;
            GrabTypeComboBox.SelectedIndex = 0;
            MessageComboBox.DataSource = Enum.GetValues(typeof(WindowGUIEditor.CommonEvents));

            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(IgnoreChildButton, "Adds to list of children to skip.");
            GUIToolTip.SetToolTip(IgnoreParentButton, "Adds to list of parents to skip.");
            GUIToolTip.SetToolTip(GrabTypeComboBox, "Method of obtaining handles.");
            GUIToolTip.SetToolTip(ChildHandleLabel, "Handle to the selected child.");
            GUIToolTip.SetToolTip(ParentHandleLabel, "Handle to the selected parent.");
            GUIToolTip.SetToolTip(ProcessIDLabel, "Process ID of selected window.");
            GUIToolTip.SetToolTip(ChildTextLabel, "Text of the selected child.");
            GUIToolTip.SetToolTip(ParentTextLabel, "Text of the selected parent.");
            GUIToolTip.SetToolTip(XTextBox, "X position of selected window");
            GUIToolTip.SetToolTip(YTextBox, "Y position of selected window");
            GUIToolTip.SetToolTip(WTextBox, "W position of selected window");
            GUIToolTip.SetToolTip(HTextBox, "H position of selected window");
            GUIToolTip.SetToolTip(CommonMessageRB, "Send message from a list of common messages.");
            GUIToolTip.SetToolTip(SpecificMessageRB, "Send a message via user specified message ID.");
            GUIToolTip.SetToolTip(WParamTextBox, "L Parameters for the message.");
            GUIToolTip.SetToolTip(LParamTextBox, "W Parameters for the message.");
            GUIToolTip.SetToolTip(SendMsgButton, "Send message to control based on current settings.");
            GUIToolTip.SetToolTip(EnableButton, "Enables target.");
            GUIToolTip.SetToolTip(DisableButton, "Disables target.");
            GUIToolTip.SetToolTip(ToFrontButton, "Brings target to front.");
            GUIToolTip.SetToolTip(ToBackButton, "Sends target to back.");
            GUIToolTip.SetToolTip(ShowButton, "Makes the target visible.");
            GUIToolTip.SetToolTip(HideButton, "Makes the target invisible.");
            GUIToolTip.SetToolTip(MaximizeButton, "Maximizes target.");
            GUIToolTip.SetToolTip(MinimizeButton, "Minimizes target.");
            GUIToolTip.SetToolTip(CloseButton, "Attempts to close target.");
            GUIToolTip.SetToolTip(RestoreButton, "Restores target.");
            GUIToolTip.SetToolTip(PanLeftButton, "Nudges target left by 2px.");
            GUIToolTip.SetToolTip(PanUpButton, "Nudges target up by 2px.");
            GUIToolTip.SetToolTip(PanRightButton, "Nudges target right by 2px.");
            GUIToolTip.SetToolTip(PanDownButton, "Nudges target down by 2px.");
        }

        #endregion

        #region Methods

        private void CoordsUpdate(int x, int y)
        {
            mouseCoordsLabel.Text = String.Format("x={0}  y={1}", x, y);
        }

        private void GetInfo()
        {
            Point MouseCoords = MousePosition;

            Parent_hwnd = (IntPtr)FindRoot(WindowEditor.WindowFromPointm(MouseCoords.X, MouseCoords.Y));

            WindowGUIEditor.WINDOWPLACEMENT placement = new WindowGUIEditor.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            WindowEditor.GetWindowPlacementm(Parent_hwnd, out placement);

            if (RealChildWFPRB.Checked == true)
            {
                Child_hwnd = WindowEditor.RealChildWindowFromPointm(Parent_hwnd, new Point(
                    MouseCoords.X - placement.rcNormalPosition.X - Cursor.Size.Width / 4,
                    MouseCoords.Y - placement.rcNormalPosition.Y - Cursor.Size.Height));
            }
            else if (ChildWFPRB.Checked == true)
            {
                Child_hwnd = WindowEditor.ChildWindowFromPointm(Parent_hwnd, new Point(
                       MouseCoords.X - placement.rcNormalPosition.X - Cursor.Size.Width / 4,
                       MouseCoords.Y - placement.rcNormalPosition.Y - Cursor.Size.Height));
            }
            else if (ChildWFPExRB.Checked == true)
            {
                UInt32 grabFlags;

                switch (GrabTypeComboBox.SelectedIndex)
                {
                    case 0:
                        grabFlags = 0x0000;
                        break;
                    case 1:
                        grabFlags = 0x0001;
                        break;
                    case 2:
                        grabFlags = 0x0002;
                        break;
                    case 3:
                        grabFlags = 0x0004;
                        break;
                    default:
                        grabFlags = 0x0000;
                        break;
                }

                Child_hwnd = WindowEditor.ChildWindowFromPointExm(Parent_hwnd, new Point(
                    MouseCoords.X - placement.rcNormalPosition.X - Cursor.Size.Width / 4,
                    MouseCoords.Y - placement.rcNormalPosition.Y - Cursor.Size.Height), grabFlags);
            }

            GetWindowPosition();

            uint PID;
            PID = WindowEditor.GetWindowThreadProcessIdm(Parent_hwnd, out PID);
            ProcessIDLabel.Text = Conversions.ToHex(PID.ToString());

            //Update data on form
            ChildHandleLabel.Text = Conversions.ToAddress(Child_hwnd.ToString());
            ChildTextLabel.Text = childText;

            ParentHandleLabel.Text = Conversions.ToAddress(Parent_hwnd.ToString());// +" TODO: #ID";
            ParentTextLabel.Text = WindowText((int)Parent_hwnd);
            childText = WindowText((int)Child_hwnd);
        }

        // Return the window's parent.
        private IntPtr FindRoot(IntPtr hWnd)
        {
            do
            {
                IntPtr parent_hwnd = WindowEditor.GetParentm(hWnd);
                if (parent_hwnd.ToInt64() == 0)
                    return hWnd;
                hWnd = parent_hwnd;
            } while (true);
        }

        // Return the window's text.
        private string WindowText(Int32 hWnd)
        {
            if (hWnd == 0)
                return "";

            int text_len = WindowEditor.GetWindowTextLengthm((IntPtr)hWnd);
            if (text_len == 0)
                return "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder(text_len + 1);
            dynamic ret = WindowEditor.GetWindowTextm((IntPtr)hWnd, sb, sb.Capacity);
            if (ret == 0)
                return "";

            return sb.ToString();
        }

        private void GetWindowPosition()
        {
            WindowGUIEditor.WINDOWPLACEMENT placement;

            WindowEditor.GetWindowPlacementm(Child_hwnd, out placement);
            XTextBox.Text = placement.rcNormalPosition.X.ToString();
            YTextBox.Text = placement.rcNormalPosition.Y.ToString();
            WTextBox.Text = (placement.rcNormalPosition.Size.Width - placement.rcNormalPosition.X).ToString();
            HTextBox.Text = (placement.rcNormalPosition.Size.Height - placement.rcNormalPosition.Y).ToString();
        }

        private uint getCommonEventNumber()
        {
            switch (CommonEvents)
            {
                case WindowGUIEditor.CommonEvents.BN_CLICKED:
                    return 0xF5;
                case WindowGUIEditor.CommonEvents.WM_ACTIVATE:
                    return 0x0006;
                case WindowGUIEditor.CommonEvents.WM_SETFOCUS:
                    return 0x0007;
                case WindowGUIEditor.CommonEvents.WM_KEYDOWN:
                    return 0x0100;
                case WindowGUIEditor.CommonEvents.WM_MOUSEMOVE:
                    return 0x0200;
                case WindowGUIEditor.CommonEvents.WM_LBUTTONDOWN:
                    return 0x0201;
                case WindowGUIEditor.CommonEvents.WM_QUIT:
                    return 0x0012;
            }
            return 1;
        }

        private void SendMessage()
        {
            CommonEvents = (WindowGUIEditor.CommonEvents)MessageComboBox.SelectedIndex;
            uint message = getCommonEventNumber();

            int lparam = 0;
            int wparam = 0;

            try
            {
                if (WParamTextBox.Text != "")
                    wparam = Convert.ToInt32(WParamTextBox.Text);
                if (LParamTextBox.Text != "")
                    lparam = Convert.ToInt32(WParamTextBox.Text);

                //Selected from list
                if (CommonMessageRB.Enabled == true)
                    WindowEditor.PostMessagem(Child_hwnd, message, 0, (int)IntPtr.Zero);
                else
                {
                    WindowEditor.PostMessagem(Child_hwnd, Convert.ToUInt32(SpecificTextBox.Text), wparam, lparam);
                }
            }
            catch
            {
                MessageBox.Show("Format Error");
            }
        }

        #endregion

        #region Events

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            if (GetData == true)
                GetInfo();
            CoordsUpdate(e.X, e.Y);
        }

        private void GatherData(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ShiftKey) //This was glitchin
            if (e.KeyValue == 160)
                GetData = true;

            CoordsUpdate(Cursor.Position.X, Cursor.Position.Y);
        }

        private void GatherStop(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ShiftKey)
            if (e.KeyValue == 160)
                GetData = false;
        }

        private void sendMsgButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void GUIEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //comboBox1.DataSource = Enum.GetValues(typeof(MyEnum)); 
            MessageComboBox.Enabled = true;
            SpecificTextBox.Text = "";
            SpecificTextBox.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MessageComboBox.Enabled = false;
            SpecificTextBox.Enabled = true;
        }

        private void enableButton_Click(object sender, EventArgs e)
        {
            WindowEditor.EnableWindowm(Child_hwnd, true);
        }

        private void disableButton_Click(object sender, EventArgs e)
        {
            WindowEditor.EnableWindowm(Child_hwnd, false);
        }

        private void showButton_Click(object sender, EventArgs e)
        {

            WindowEditor.ShowWindowm(Child_hwnd, (int)WindowGUIEditor.WindowShowStyle.ShowNormal);
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            WindowEditor.ShowWindowm(Child_hwnd, (int)WindowGUIEditor.WindowShowStyle.Hide);
        }

        static readonly IntPtr HWND_TOPMOST = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOMMOST = new IntPtr(1);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 BOTTOMMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE;

        private void toFrontButton_Click(object sender, EventArgs e)
        {
            WindowGUIEditor.RECT Rect;
            WindowEditor.GetWindowRectm(Child_hwnd, out Rect);
            WindowEditor.SetWindowPosm(Child_hwnd, HWND_TOPMOST, Rect.Left, Rect.Top, Rect.Right - Rect.Left, Rect.Bottom - Rect.Top, TOPMOST_FLAGS);

        }

        private void toBackButton_Click(object sender, EventArgs e)
        {
            WindowGUIEditor.RECT Rect;
            WindowEditor.GetWindowRectm(Child_hwnd, out Rect);
            WindowEditor.SetWindowPosm(Child_hwnd, HWND_BOTTOMMOST, Rect.Left, Rect.Top, Rect.Right - Rect.Left, Rect.Bottom - Rect.Top, BOTTOMMOST_FLAGS);

        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            WindowEditor.ShowWindowm(Child_hwnd, (int)WindowGUIEditor.WindowShowStyle.Maximize);
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowEditor.ShowWindowm(Child_hwnd, (int)WindowGUIEditor.WindowShowStyle.ForceMinimized);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //DestroyWindow(Child_hwnd);
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            WindowEditor.ShowWindowm(Child_hwnd, (int)WindowGUIEditor.WindowShowStyle.Restore);
        }

        private void realChildWFPRB_CheckedChanged(object sender, EventArgs e)
        {
            GrabTypeComboBox.Enabled = false;
        }

        private void childWFPRB_CheckedChanged(object sender, EventArgs e)
        {
            GrabTypeComboBox.Enabled = false;
        }
        private void childWFPExRB_CheckedChanged(object sender, EventArgs e)
        {
            GrabTypeComboBox.Enabled = true;
        }

        private void xTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GetData == false)
                SetNewCoords(0, 0);
        }

        private void yTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GetData == false)
                SetNewCoords(0, 0);
        }

        private void wTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GetData == false)
                SetNewCoords(0, 0);
        }

        private void hTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GetData == false)
                SetNewCoords(0, 0);
        }

        private void SetNewCoords(int addX, int addY)
        {
            //Check syntax for int of all values
            if (!(CheckSyntax.Int32Value(XTextBox.Text, false) && CheckSyntax.Int32Value(YTextBox.Text, false) &&
                CheckSyntax.Int32Value(WTextBox.Text, false) && CheckSyntax.Int32Value(HTextBox.Text, false)))
                return;

            int x = Convert.ToInt32(XTextBox.Text) + addX;
            int y = Convert.ToInt32(YTextBox.Text) + addY;
            int w = Convert.ToInt32(WTextBox.Text);
            int h = Convert.ToInt32(HTextBox.Text);

            WindowEditor.SetWindowPosm((IntPtr)Child_hwnd, IntPtr.Zero, x, y, w, h, 0);
            this.Focus();
        }

        private void ignoreChild_Click(object sender, EventArgs e)
        {
            ignoreC.Add(Child_hwnd);
        }

        private void ignoreParent_Click(object sender, EventArgs e)
        {
            ignoreP.Add(Parent_hwnd);
        }

        #endregion

        #region Window panning events

        private byte Direction = 0;

        private void panLeftButton_Click(object sender, EventArgs e)
        {
            Direction = 1;
            PanTargetWindow();
        }

        private void panUpButton_Click(object sender, EventArgs e)
        {
            Direction = 2;
            PanTargetWindow();
        }

        private void panRightButton_Click(object sender, EventArgs e)
        {
            Direction = 3;
            PanTargetWindow();
        }

        private void panDownButton_Click(object sender, EventArgs e)
        {
            Direction = 4;
            PanTargetWindow();
        }

        private void PanLeftButton_MouseDown(object sender, MouseEventArgs e)
        {
            Direction = 1;
            PanHoldTimer.Start();
        }

        private void PanLeftButton_MouseLeave(object sender, EventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanLeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanUpButton_MouseDown(object sender, MouseEventArgs e)
        {
            Direction = 2;
            PanHoldTimer.Start();
        }

        private void PanUpButton_MouseLeave(object sender, EventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanUpButton_MouseUp(object sender, MouseEventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanRightButton_MouseDown(object sender, MouseEventArgs e)
        {
            Direction = 3;
            PanHoldTimer.Start();
        }

        private void PanRightButton_MouseLeave(object sender, EventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanRightButton_MouseUp(object sender, MouseEventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanDownButton_MouseDown(object sender, MouseEventArgs e)
        {
            Direction = 4;
            PanHoldTimer.Start();
        }

        private void PanDownButton_MouseLeave(object sender, EventArgs e)
        {
            PanHoldTimer.Stop();
        }

        private void PanDownButton_MouseUp(object sender, MouseEventArgs e)
        {
            PanHoldTimer.Stop();
        }


        private void PanHoldTimer_Tick(object sender, EventArgs e)
        {
            PanTargetWindow();
        }

        private void PanTargetWindow()
        {
            switch (Direction)
            {
                case 1:
                    SetNewCoords(-2, 0);
                    GetWindowPosition();
                    break;
                case 2:
                    SetNewCoords(0, -2);
                    GetWindowPosition();
                    break;
                case 3:
                    SetNewCoords(2, 0);
                    GetWindowPosition();
                    break;
                case 4:
                    SetNewCoords(0, 2);
                    GetWindowPosition();
                    break;
                default:
                    return;
            }
        }

        #endregion
    }

    /// <summary>
    /// Contains methods for directly modifying attributes of windows
    /// </summary>
    public class WindowGUIEditor
    {

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