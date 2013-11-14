using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aecial.MemoryScanner;
using Aecial.Conversions;

namespace AecialEngine
{
    public partial class AddSpecificWindow : Form
    {
        #region Initialization / Variables

        public ScanDataType ScanType;
        public string Address;
        public string Description;
        public int changeType;

        public AddSpecificWindow(bool isChanged, string address, string description, ScanDataType scanType)
        {
            InitializeComponent();

            if (isChanged == true)
            {
                this.Text = "Change Address";
                AddressTextBox.Text = Address;
                DescriptionTextBox.Text = Description;
                ValueTypeComboBox.SelectedIndex = changeType;
                ScanType = scanType;
                ValueTypeComboBox.SelectedIndex = (int)ScanType;
            }
            else
            {
                this.Text = "Add New Address";
            }

            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(AddressTextBox,"Address to add.");
            GUIToolTip.SetToolTip(IsHexCB,"Determines if address being added is a hex value.");
            GUIToolTip.SetToolTip(DescriptionTextBox,"Description for the address.");
            GUIToolTip.SetToolTip(ValueTypeComboBox,"Value type of the address.");
            GUIToolTip.SetToolTip(AcceptSpecificButton,"Adds the address to the table.");
            GUIToolTip.SetToolTip(CancelSpecificButton,"Closes window and doesn't add address.");
        }

        private void AddSpecificWindow_Load(object sender, EventArgs e)
        {
            ValueTypeComboBox.SelectedIndex = (int)ScanDataType.Int32; //default to int
        }

        #endregion

        #region Events

        private void convertToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressTextBox.Text = Conversions.DecToHex(AddressTextBox.Text, true);
            IsHexCB.Checked = true;
        }

        private void convertToDecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressTextBox.Text = Conversions.HexToDec(AddressTextBox.Text, true);
            IsHexCB.Checked = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (IsHexCB.Checked == true)
            {
                if (!CheckSyntax.Address(AddressTextBox.Text))
                {
                    MessageBox.Show("Invalid address format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Address = AddressTextBox.Text;
            }
            else
            {
                if (!CheckSyntax.Int32Value(AddressTextBox.Text, false))
                {
                    MessageBox.Show("Invalid address format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Address = Conversions.ToHex(AddressTextBox.Text);
            }


            if (DescriptionTextBox.Text == "")
                Description = "No Description";
            else
                Description = DescriptionTextBox.Text;

            switch (ValueTypeComboBox.SelectedIndex)
            {
                case 0:
                    ScanType = ScanDataType.Binary;
                    break;
                case 1:
                    ScanType = ScanDataType.Byte;
                    break;
                case 2:
                    ScanType = ScanDataType.Int16;
                    break;
                case 3:
                    ScanType = ScanDataType.Int32;
                    break;
                case 4:
                    ScanType = ScanDataType.Int64;
                    break;
                case 5:
                    ScanType = ScanDataType.Single;
                    break;
                case 6:
                    ScanType = ScanDataType.Double;
                    break;
                case 7:
                    ScanType = ScanDataType.Text;
                    break;
                case 8:
                    ScanType = ScanDataType.AOB;
                    break;
                case 9:
                    ScanType = ScanDataType.All;
                    break;
            }
            this.Close();
        }
        #endregion
    }
}