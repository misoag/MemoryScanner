using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InputHook;
using AecialEngine.Tools.Classes;

namespace AecialEngine.Tools
{
    public partial class Macro : Form
    {
        #region Variables

        //Action / time / specific
        private DateTime lastAction;

        private List<int> ActionTime = new List<int>(); //Time until action is executed
        private List<byte> Action = new List<byte>(); //0 move 1 click 2 keypress
        private List<Point> Data = new List<Point>(); //Additional information

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        bool recording = false;

        #endregion

        #region Initialization
        public Macro()
        {
            InitializeComponent();
        }

        InputHook.UserActivityHook actHook;
        private void Macro_Load(object sender, EventArgs e)
        {
            actHook = new InputHook.UserActivityHook(true, true); // crate an instance with global hooks
            // hang on events
            actHook.OnMouseActivity += new MouseEventHandler(MouseActivity);
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            actHook.KeyUp += new KeyEventHandler(MyKeyUp);

        }
        #endregion

        #region Events
        public void MouseActivity(object sender, MouseEventArgs e)
        {
            //Update text
            label1.Text = String.Format("x={0}  y={1}", e.X, e.Y); //e.Delta

            //Mouse move event
            SetInterval();
            Action.Add(0);
            Data.Add(new Point(e.X, e.Y));

            //Mouse click event
            if (e.Clicks > 0)
            {
                SetInterval();
                Action.Add(1);

                //Mouse click event
                if (actHook.ButtonAction == "LeftDown")
                {
                    Data.Add(new Point(1, 0));
                }
                else if (actHook.ButtonAction == "LeftUp")
                {
                    Data.Add(new Point(2, 0));
                }
                else if (actHook.ButtonAction == "RightDown")
                {
                    Data.Add(new Point(3, 0));
                }
                else if (actHook.ButtonAction == "RightUp")
                {
                    Data.Add(new Point(4, 0));
                }
                else
                    throw new Exception("Message sent that lacks implementation");
            }

        }

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (recording == true)
            {
                SetInterval();
                Action.Add(2);
                Data.Add(new Point(e.KeyValue, 2)); //Key down
            }

        }

        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (recording == true)
            {
                SetInterval();
                Action.Add(2);
                Data.Add(new Point(e.KeyChar, 1)); //Key press
            }
        }

        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            if (recording == true)
            {
                SetInterval();
                Action.Add(2);
                Data.Add(new Point(e.KeyValue, 0)); //Key up
            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            Action.Clear();
            ActionTime.Clear();
            Data.Clear();
            index = 0;

            lastAction = DateTime.Now;
            actHook.Start(true, true);
            recording = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            recording = false;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            recording = false;
            actHook.Stop();

            if (ActionTime.Count > 0)
            {
                playBack.Enabled = true;
                if (ActionTime[0] == 0)
                    ActionTime[0]++;
                playBack.Interval = (int)ActionTime[0];
            }

        }

        int index = 0;
        private void playBack_Tick(object sender, EventArgs e)
        {
            int time;

            if (Action[index] == 0) //0 Mouse Move
            {
                Cursor.Position = Data[index];
            }
            else if (Action[index] == 1) //1 click
            {
                if (Data[index].X == 1)
                    mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                else if (Data[index].X == 2)
                    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                else if (Data[index].X == 3)
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                else if (Data[index].X == 4)
                    mouse_event(MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                else
                    throw new Exception("Attempt to simulate a nonexistent mouse message");

            }
            else if (Action[index] == 2) //2 Keyboard
            {
                //Data: 0 key up; 1 key press; 2 key down

            }

            index++;

            if (index == ActionTime.Count)
            {
                playBack.Enabled = false;
                return;
            }

            time = (int)ActionTime[index];
            if (time == 0)
                time = 1;
            playBack.Interval = time;
        }

        #endregion

        #region Methods/Imports
        private void SetInterval()
        {
            ActionTime.Add((int)(DateTime.Now - lastAction).TotalMilliseconds);
            lastAction = DateTime.Now;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion

    }
}