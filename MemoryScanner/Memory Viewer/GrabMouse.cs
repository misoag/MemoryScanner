using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AecialEngine
{
    public partial class GrabMouse : Panel
    {
        public GrabMouse()
        {
            InitializeComponent();
        }

        //Make the panel transparent
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;//WS_EX_TRANSPARENT
                return cp;
            }
        }

        //Dont do anything when it comes time to draw the control
        protected override void OnPaint(PaintEventArgs e)
        {

        }
    }
}
