using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AecialEngine
{
    public partial class DescriptionWindow : Form
    {
        public string Description;

        public DescriptionWindow(string description)
        {
            InitializeComponent();
            AcceptDescriptionButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            DescriptionTextBox.Text = description;
            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(DescriptionTextBox, "New description to replace the old.");
            GUIToolTip.SetToolTip(AcceptDescriptionButton, "Updates the description of the address.");
            GUIToolTip.SetToolTip(CancelDescriptionButton, "Closes window and doesn't update the description.");
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            Description = DescriptionTextBox.Text;
        }
    }
}