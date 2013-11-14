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
    public partial class ScanHistory : Form
    {
        #region Variables & Initialization

        public List<ScanSession> Session = new List<ScanSession>();

        public ScanHistory()
        {
            InitializeComponent();
        }

        private void ScanHistory_Load(object sender, EventArgs e)
        {
            SetToolTips();
        }

        public int MostRecent()
        {
            return Session.Count - 1;
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(ScanSessionComboBox, "Determines what scan session to display. Highest is most recent.");
            GUIToolTip.SetToolTip(RefreshButton, "Updates scan history.");
        }

        #endregion

        #region Methods

        //Data is recleared on open/reclose
        private void ScanHistory_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                LoadSession();
        }

        //Updates combo box to show data
        private void LoadSession()
        {
            ScanSessionComboBox.Items.Clear();
            //Display all sessions in combo box
            for (int ecx = 0; ecx < Session.Count; ecx++)
                ScanSessionComboBox.Items.Add(ecx.ToString() + " - " + Session[ecx].DataType);

            //This will trigger selection change event and call LoadSessionData
            ScanSessionComboBox.SelectedIndex = ScanSessionComboBox.Items.Count - 1;
        }

        //Displays scan history based on session ID
        private void LoadSessionData(int SessionID)
        {
            ScanHistoryListView.Items.Clear();
            if (SessionID == -1)
                return;
            for (int ecx = Session[SessionID].ScanType.Count-1; ecx >= 0; ecx-- )
                ScanHistoryListView.Items.Add(Session[SessionID].ScanType[ecx]).SubItems.Add(Session[SessionID].ScanValue[ecx]);
        }

        private void ScanSessionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSessionData(ScanSessionComboBox.SelectedIndex);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            //Reload session & data
            LoadSession();
            LoadSessionData(ScanSessionComboBox.SelectedIndex);
        }

        //Don't actually close it, simply hide it since this class stores all the info we want
        private void ScanHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        #endregion

        public class ScanSession
        {
            //Data relating to a particular scan session
            public string DataType = "";
            public List<string> ScanType = new List<string>();
            public List<string> ScanValue = new List<string>();

            public ScanSession(string DataType, string ScanType, string ScanValue, string SecondScanValue)
            {
                this.DataType = DataType;
                AddScanType(ScanType);
                AddScanValue(ScanValue, SecondScanValue);
            }

            public void AddScanType(string ScanType)
            {
                this.ScanType.Add(ScanType);
            }

            public void AddScanValue(string ScanValue, string SecondScanValue)
            {
                if (ScanValue != "" && SecondScanValue != "")
                    ScanValue += " / " + SecondScanValue;
                this.ScanValue.Add(ScanValue);
            }

            public bool IsFirstScan()
            {
                if (ScanType.Count > 1)
                    return false;
                return true;
            }
        }

    }
}
