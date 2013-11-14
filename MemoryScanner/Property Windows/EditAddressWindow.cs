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
    public partial class AddressWindow : Form
    {

        public UInt64 Address;
        private bool failed = false;

        public AddressWindow(string address)
        {
            InitializeComponent();
            AcceptAddressButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            AddressTextBox.Text = address;
            SetToolTips();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(AddressTextBox, "New address to replace the old.");
            GUIToolTip.SetToolTip(IsHexCB, "Determines if address being changed is a hex value.");
            GUIToolTip.SetToolTip(AcceptAddressButton, "Applies change to the address.");
            GUIToolTip.SetToolTip(CancelAddressButton, "Closes window and doesn't change address.");
        }

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

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string ValueString = AddressTextBox.Text;

            //If not hex we can just convert address to an int and use it
            if (!IsHexCB.Checked)
                //Check if address is a valid non-hex integer
                if (CheckSyntax.Int32Value(ValueString, IsHexCB.Checked))
                    //Convert to a hex string
                    Address = Convert.ToUInt64(ValueString);
                else //Wasn't a valid integer
                {
                    MessageBox.Show("Invalid address format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    failed = true;
                }

            //Check if hex string is valid
            else if (CheckSyntax.Address(ValueString))
                //Valid: proceed to convert it to an int
                Address = Conversions.HexToUInt64(AddressTextBox.Text);
            else //Nope, they screwed up
            {
                MessageBox.Show("Invalid address format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                failed = true;
            }
        }

        private void AddressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //User messed up address input; cancel and let them correct this
            if (failed)
                e.Cancel = true;

            //reset failed state
            failed = false;
        }

    }
}