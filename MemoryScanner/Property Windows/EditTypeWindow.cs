using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aecial.MemoryScanner;

namespace AecialEngine
{
    public partial class TypeWindow : Form
    {

        #region Initialization / Variables

        public ScanDataType ScanType;

        public TypeWindow(ScanDataType ScanType)
        {
            InitializeComponent();
            this.ScanType = ScanType;
        }

        private void TypeWindow_Load(object sender, EventArgs e)
        {
            ValueTypeComboBox.SelectedIndex = (int)ScanType;
            AcceptTypeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(ValueTypeComboBox, "New value type to replace the old.");
            GUIToolTip.SetToolTip(AcceptTypeButton, "Updates the value type of the address.");
            GUIToolTip.SetToolTip(CancelTypeButton, "Closes window and doesn't update value type.");
        }

        #endregion

        #region Events
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            ScanType = (ScanDataType)ValueTypeComboBox.SelectedIndex;
        }

        #endregion

    }
}