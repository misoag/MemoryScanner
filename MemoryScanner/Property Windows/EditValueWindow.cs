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
    public partial class ValueWindow : Form
    {
        #region Variables/Initialization
        public object Value;
        private ScanDataType ScanType;
        private bool failed = false;

        public ValueWindow(ScanDataType ScanType, object Value)
        {
            InitializeComponent();
            AcceptValueButton.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.ScanType = ScanType;
            this.Value = Value;

            ValueTextBox.Text = Value.ToString();
        }

        #endregion

        #region Events

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string ValueString = ValueTextBox.Text;

            switch (ScanType)
            {
                case ScanDataType.Binary:
                    if (CheckSyntax.BinaryValue(ValueString))
                        Value = Conversions.ToUnsigned(ValueString, ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Byte:
                    //Check syntax
                    if (CheckSyntax.ByteValue(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            //Take unsigned value
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else //Take unsigned value after converting it from hex
                            Value = Conversions.ToUnsigned(Conversions.HexToByte(ValueString).ToString(), ScanType);
                    else //Invalid syntax
                        failed = true;
                    break;
                case ScanDataType.Int16:
                    if (CheckSyntax.Int32Value(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else
                            Value = Conversions.ToUnsigned(Conversions.HexToShort(ValueString).ToString(), ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Int32:
                    if (CheckSyntax.Int32Value(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else
                            Value = Conversions.ToUnsigned(Conversions.HexToInt(ValueString).ToString(), ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Int64:
                    if (CheckSyntax.Int64Value(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else
                            Value = Conversions.ToUnsigned(Conversions.HexToUInt64(ValueString).ToString(), ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Single:
                    if (CheckSyntax.SingleValue(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else
                            Value = Conversions.ToUnsigned(Conversions.HexToSingle(ValueString).ToString(), ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Double:
                    if (CheckSyntax.DoubleValue(ValueString, IsHexCB.Checked))
                        if (!IsHexCB.Checked)
                            Value = Conversions.ToUnsigned(ValueString, ScanType);
                        else
                            Value = Conversions.ToUnsigned(Conversions.HexToDouble(ValueString).ToString(), ScanType);
                    else
                        failed = true;
                    break;
                case ScanDataType.Text:
                    //TODO: everything
                    Value = ValueString;
                    break;
            }

            if (failed)
                MessageBox.Show("Invalid value format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ValueWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //User messed up address input; cancel and let them correct this
            if (failed)
                e.Cancel = true;

            //reset failed state
            failed = false;
        }

        private void convertToHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValueTextBox.Text = Conversions.DecToHex(ValueTextBox.Text, false);
            IsHexCB.Checked = true;
        }

        private void convertToDecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValueTextBox.Text = Conversions.HexToDec(ValueTextBox.Text, false);
            IsHexCB.Checked = false;
        }
        #endregion

    }
}