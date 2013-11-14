using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Aecial.MemoryScanner;
using Aecial.Conversions;
using System.Runtime.InteropServices;

using System.Globalization;
using System.Drawing.Drawing2D;

/*
 * Notes: Selection is stored as in int but addresses will be able to go to 0xFFFFFFFFFFFFFFFF
 * which will cause issues. We probably need is as a long with proper unchecked tags to prevent
 * possible crashes.
 * 
 * TODO:
 * -Above note
 * -Overwrite on keypress
 * -Right click menu for simple data controls
 *      -editing value/ascii/unicode table
 *      -goto address
 * -Unicode display support
 */

namespace AecialEngine
{
    public partial class MemoryViewer : Form
    {
        #region Variables / Initialization

        //Amount of data to show (x1-4) depending on screen width
        private int DisplayPanels = 4;
        private int TotalAddresses = 10;
        private int ItemHeight = 0; //Used to determine number of addresses onscreen

        //Range of data displayed (0x01000000 chosen arbitrarily)
        public ulong StartAddress = 0x01000000;//0x0100579C;
        public ulong EndAddress = 0x010001C0;
        private int RowSize = 0x08 * 4; //Will always be 0x08*DisplayPanels

        private bool DisplayAscii = true;

        //Used to grab mouse input
        private GrabMouse GrabMouse = new GrabMouse();
        private Panel TestShit = new Panel();

        //Stores how wide panels were (displayPanels starts as 4 and shrinks
        //depending on font size, screen size, etc)
        private int[] PanelSizes = new int[4];

        ProcessMemoryEditor reader = new ProcessMemoryEditor();
        private byte[] ReadValues;
        private byte[] NewReadValues;

        public MemoryViewer(MemoryEditor wholeMemScan)
        {
            InitializeComponent();
            this.wholeMemoryScan = wholeMemScan;

        }
        public MemoryEditor WholeMemoryScan
        {
            get { return wholeMemoryScan; }
        }
        MemoryEditor wholeMemoryScan;

        private void Memory_Viewer_Load(object sender, EventArgs e)
        {
            DataListView.Items.Clear();
            //Activate double buffering to reduce flicker
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            GrabMouse.MouseDown += new MouseEventHandler(DataStartSelection);
            GrabMouse.MouseMove += new MouseEventHandler(DataMouseCapture);
            this.Controls.Add(GrabMouse);
            GrabMouse.BringToFront();

            //Used to test the height of an individual item in the listbox
            string[] HeightTest = new string[] { "", "" };
            DataListView.Items.Add(HeightTest[0]).SubItems.AddRange(HeightTest);
            //Set new height
            ItemHeight = DataListView.Items[0].Bounds.Height;
            ColumnHeight = DataListView.Items[0].Bounds.Y;

            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    //Measuring seems to be off, but finding the difference between
                    //Characters that have a length difference of 1 gets the correct value
                    FontWidth = g.MeasureString("W", DataListView.Font).Width;
                    FontWidth = g.MeasureString("WW", DataListView.Font).Width - FontWidth;
                }
            }

            //Get process ID
            reader.ReadProcess = wholeMemoryScan.Process;
            reader.OpenProcess();

            //Set total addresses
            SetAddressTotal();
            //Read data within address range
            ReadDataMemory();
            //Update addresses in listview
            UpdateAddresses(0, TotalAddresses);
            //Update hex (and ascii called within function)
            UpdateHex(0, TotalAddresses);
            //Size everything correctly
            SizeControls();

            UpdateAssemblyViewer();
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message (supposed to reduce flicker)
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        #endregion

        private void UpdateAssemblyViewer()
        {
           
        }


        #region Methods

        /// <summary>
        ///  Used to figure out total number of needed addresses on screen
        /// </summary>
        private void SetAddressTotal()
        {
            string[] BlankItem = new string[] { "4", "3" };
            //Number of addresses based on left-hand side calculations
            TotalAddresses = (DataListView.Height - ItemHeight) / ItemHeight;

            //Update number in DataListView to match
            while (TotalAddresses < DataListView.Items.Count)
                DataListView.Items.Remove(DataListView.Items[DataListView.Items.Count - 1]);

            while (TotalAddresses > DataListView.Items.Count)
                DataListView.Items.Add(BlankItem[0]).SubItems.AddRange(BlankItem);

        }

        /// <summary>
        /// Scrolls through our hex & char data
        /// </summary>
        private void ScrollData(int Amount)
        {
            //Stupid that this check even needs to exist (event still triggered with no scrollbar movement)
            if (Amount == 0 || NewReadValues == null)
                return;
            //Prevent out of bounds errors
            if (Amount > NewReadValues.Length)
                Amount = NewReadValues.Length;
            if (Amount < -1 * ReadValues.Length)
                Amount = -1 * NewReadValues.Length;

            //"Scroll" address by amount
            StartAddress += (ulong)Amount;

            //Determines number of rows to shift data
            int RowOffset = Amount / (DisplayPanels * 0x08);

            //Scroll our selection to match
            DataSelection.StartRow -= RowOffset;
            DataSelection.EndRow -= RowOffset;

            ////////////////////
            //Scroll Address
            ////////////////////

            RowSize = 0x08 * DisplayPanels;

            ulong currentAddress = 0x00000000;
            //Determine next addresses from the first
            for (int Row = 0; Row < TotalAddresses; Row++)
            {
                currentAddress = StartAddress + (ulong)Row * (ulong)RowSize;
                DataListView.Items[Row].SubItems[0].Text = Convert.ToString(Conversions.ToAddress(currentAddress));
            }
            EndAddress = currentAddress;

            ////////////////////
            //Scroll Hex && chars
            ////////////////////

            //Direction of scroll
            if (Amount < 0) //Upward scrolling
                for (int Row = TotalAddresses - 1; Row >= 0; Row--)
                {
                    //See if the row we want exists
                    if (Row + RowOffset >= 0 && Row + RowOffset < TotalAddresses)
                    {
                        //Fill row data with data from [Row+RowOffset]
                        DataListView.Items[Row].SubItems[1].Text = DataListView.Items[Row + RowOffset].SubItems[1].Text;
                        DataListView.Items[Row].SubItems[2].Text = DataListView.Items[Row + RowOffset].SubItems[2].Text;
                        //Data is being shifted, no need to reread anything (yet)
                    }
                    else  //The offset of the row we want to access is not in bounds.
                    {
                        //Assume at first that the row is just filled with 0s
                        DataListView.Items[Row].SubItems[1].Text = FillZeros(DisplayPanels);
                        DataListView.Items[Row].SubItems[2].Text = FillPeriods(DisplayPanels);

                        //Shift our array (Amount will be negative)
                        Array.Copy(ReadValues, 0, ReadValues, -Amount, ReadValues.Length + Amount);
                        //Assume values that haven't been updated yet are 0
                        for (int ecx = 0; ecx < -Amount; ecx++)
                            ReadValues[ecx] = 0;
                    }
                }
            else //Downward scrolling
                for (int Row = 0; Row < TotalAddresses; Row++)
                {
                    //See if the row we want exists
                    if (Row + RowOffset >= 0 && Row + RowOffset < TotalAddresses)
                    {
                        //Fill row data with data from [Row-RowOffset]
                        DataListView.Items[Row].SubItems[1].Text = DataListView.Items[Row + RowOffset].SubItems[1].Text;
                        DataListView.Items[Row].SubItems[2].Text = DataListView.Items[Row + RowOffset].SubItems[2].Text;
                        //Data is being shifted, no need to reread anything (yet)
                    }
                    else   //The offset of the row we want to access is not in bounds.
                    {
                        //Assume at first that the row is just filled with 0s
                        DataListView.Items[Row].SubItems[1].Text = FillZeros(DisplayPanels);
                        DataListView.Items[Row].SubItems[2].Text = FillPeriods(DisplayPanels);

                        //Shift read data in array and store in ReadValues (so that differences are
                        //caught in next timer cycle in NewReadValues)
                        Array.Copy(ReadValues, Amount, ReadValues, 0, ReadValues.Length - Amount);
                        for (int ecx = ReadValues.Length - Amount; ecx < ReadValues.Length; ecx++)
                            ReadValues[ecx] = 0;
                    }
                }

            //Reread memory to find values that we scrolled to
            ReReadDataMemory();
        }

        /// <summary>
        /// Updates addresses on left hand side into memory viewer
        /// </summary>
        private void UpdateAddresses(int RowMin, int RowMax)
        {
            //DataListView.Items.Clear();
            //Used to add new blank items to our listview to overwrite later
            //string[] BlankItem = new string[] { "5", "5" };
            RowSize = 0x08 * DisplayPanels;
            ulong currentAddress = 0x00000000;

            //Determine next addresses from the first
            for (int Row = 0; Row < TotalAddresses; Row++)
            {
                //Clear old info
                //DataListView.Items[Row].SubItems.Clear();
                //DataListView.Items.Add(BlankItem[0]).SubItems.AddRange(BlankItem);
                //Add new info
                currentAddress = StartAddress + (ulong)Row * (ulong)RowSize;
                DataListView.Items[Row].SubItems[0].Text = Convert.ToString(Conversions.ToAddress(currentAddress));
            }
            //Last address is the value of the last one we ended on
            EndAddress = currentAddress;
        }

        /// <summary>
        /// Updates byte data in data list view
        /// </summary>
        private void UpdateHex(int RowMin, int RowMax)
        {
            string RowHexData; //Used to parse bytes and convert to hex values
            string next = "";

            //Use data to create ascii/unicode info
            UpdateCharData(RowMin, RowMax);

            //Update & format hex
            for (int Row = RowMin; Row < RowMax; Row++)
            {
                RowHexData = "";
                for (int ecx = 0; ecx < RowSize; ecx++)
                {
                    //Get next hex byte
                    next = Conversions.ToHex(NewReadValues[Row * RowSize + ecx].ToString());
                    //Precede with 0 if less than 16 in hex
                    if (NewReadValues[Row * RowSize + ecx] < 0x10)
                        next = "0" + next;
                    next += " "; //Space between each byte
                    RowHexData += next;
                }

                //Add it
                DataListView.Items[Row].SubItems[1].Text = RowHexData;
            }

        }

        /// <summary>
        /// Updates unicode or ascii data in data list view
        /// </summary>
        private void UpdateCharData(int RowMin, int RowMax)
        {
            //TODO use a second byte & get unicode
            byte[] nextBytes = new byte[1];

            for (int Row = RowMin; Row < RowMax; Row++)
            {
                string data = "";

                for (int ecx = 0; ecx < RowSize; ecx++)
                {
                    //Get next byte from array
                    nextBytes[0] = NewReadValues[Row * RowSize + ecx];

                    //Convert to ascii
                    data += Conversions.FormatAscii(nextBytes);
                }
                DataListView.Items[Row].SubItems[2].Text = data;
            }
        }

        /// <summary>
        /// Used to read all byte data in the current address range
        /// </summary>
        private void ReadDataMemory()
        {
            //Holds read bytes from left hand addresses
            ReadValues = new byte[TotalAddresses * RowSize];
            NewReadValues = new byte[TotalAddresses * RowSize];

            //Read data based addresses listed to the left
            ReadValues = reader.ReadAOB((IntPtr)StartAddress, (uint)(TotalAddresses * RowSize));

            //Initialize it with current data to prevent some incredibly unlikely crashes
            NewReadValues = ReadValues;
        }

        /// <summary>
        /// Rereads memory and makes proper adjustments to data listview
        /// </summary>
        private void ReReadDataMemory()
        {
            if (reader.ReadProcess == null)
                return;

            //NewReadValues = new byte[TotalAddresses * RowSize];
            NewReadValues = reader.ReadAOB((IntPtr)StartAddress, (uint)(TotalAddresses * RowSize));

            byte[] NewByteValue = new byte[1];

            //Only bother if they are same size
            if (NewReadValues.Length == ReadValues.Length)
            {
                for (int Row = 0; Row < TotalAddresses; Row++)
                {
                    for (int ecx = 0; ecx < RowSize; ecx++)
                    {
                        //Check if array of bytes for two rows are the different
                        if (NewReadValues[Row * RowSize + ecx] != ReadValues[Row * RowSize + ecx])
                        {
                            //Clear the data in this row
                            DataListView.Items[Row].SubItems[1].Text = "";
                            DataListView.Items[Row].SubItems[2].Text = "";

                            //Update the hex for that row only
                            UpdateHex(Row, Row + 1);

                            //No need to compare further for this row
                            break;
                        }
                    }
                }
            }
            else //New array and old aren't same size. All data must be reread.
            {
                //Not sure if this would ever occur. Depends on how I choose to manage things.
                //TODO
            }

            //Now that comparisons are done, set read values to new so the process can repeat.
            ReadValues = NewReadValues;
        }

        /// <summary>
        /// Sets bounds and figures out how much information to display on-screen
        /// </summary>
        private void SizeControls()
        {
            ////////////////
            //Assembly list view resize
            ////////////////

            AssemblyListView.Width = this.ClientSize.Width;

            ////////////////
            //Data list view resize
            ////////////////

            //Fit to bounds (docking property manages other bounds)

            DataListView.Width = this.ClientSize.Width;
            DataListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            int ColumnsWidth = DataListView.Columns[0].Width + DataListView.Columns[1].Width + DataListView.Columns[2].Width;

            if (ColumnsWidth >= this.ClientSize.Width && DisplayPanels > 1)
            {
                DisplayPanels--;
                PanelSizes[DisplayPanels] = ColumnsWidth;

                SetAddressTotal();
                UpdateAddresses(0, TotalAddresses); //TODO make function to use readarray to recompute rather than reread
                DataListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            else if (PanelSizes[DisplayPanels - 1] <= this.ClientSize.Width && DisplayPanels < 4)
            {
                DisplayPanels++;

                SetAddressTotal();
                UpdateAddresses(0, TotalAddresses); //here too
                DataListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            ////////////////
            //Mouse Grabber resize
            ////////////////
            GrabMouse.SetBounds(DataListView.Bounds.X,// + DataListView.Items[0].SubItems[1].Bounds.X,
               DataListView.Bounds.Y + 0,
               DataListView.Items[0].SubItems[2].Bounds.X + DataListView.Items[0].SubItems[2].Bounds.Width,// - DataListView.Items[0].SubItems[1].Bounds.X,
               DataListView.Bounds.Height);

        }

        private string FillZeros(int count)
        {
            string Zeros = "";
            for (int ecx = 0; ecx < count; ecx++)
                Zeros += "00 00 00 00 00 00 00 00 ";
            return Zeros;
        }

        private string FillPeriods(int count)
        {
            string Periods = "";
            for (int ecx = 0; ecx < count; ecx++)
                Periods += "........";
            return Periods;
        }

        #endregion

        #region Form Events

        private void MemoryViewer_Resize(object sender, EventArgs e)
        {
            //SizeControls(); //TODO address issue of to do after or during resize
        }

        private void MemoryViewer_ResizeEnd(object sender, EventArgs e)
        {
            SizeControls();
        }

        //Calls all updates to hexBox
        private void updateFoundTimer_Tick(object sender, EventArgs e)
        {
            ReReadDataMemory();
        }

        //Form captures key presses to overwrite memory in data view
        private void MemoryViewer_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //Keeps track of previous scrollbar position
        private int PreviousValue = 5000;
        /// <summary>
        /// Scrolls through data section
        /// </summary>
        private void dataScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            unchecked //Got to include this to subtract from an unsigned value
            {
                //TODO read all memory on a page at once perhaps, maybe more like ollydbg
                //With page navigation6
                ScrollData((e.NewValue - PreviousValue) * 0x08 * DisplayPanels);
            }

            //Set previous value (not using e.OldValue due to issues created by our position reset)
            PreviousValue = e.NewValue;
            //And reset our scrollbar position
            e.NewValue = (int)(DataScrollBar.Maximum / 2);

            //Fixes issue where with these types where the bar would scroll & unscroll
            if (e.Type == ScrollEventType.LargeDecrement ||
                e.Type == ScrollEventType.LargeIncrement ||
                e.Type == ScrollEventType.SmallDecrement ||
                e.Type == ScrollEventType.SmallIncrement)
            {
                PreviousValue = e.NewValue;
            }

        }

        #endregion

        #region Data Highlighting

        private SelectionRange DataSelection = new SelectionRange(0, 0, 0, 0);
        private int ColumnHeight;
        float FontWidth;

        private void DataStartSelection(object sender, MouseEventArgs e)
        {
            //Figure out where the user is clicking on the data listview
            int Row = (e.Y - ColumnHeight) / ItemHeight;
            int RowIndex = 0;
            if (e.X > DataListView.Items[0].SubItems[1].Bounds.X && e.X < DataListView.Items[0].SubItems[1].Bounds.X + DataListView.Items[0].SubItems[1].Bounds.Width)
            {
                RowIndex = (int)((e.X - DataListView.Items[0].SubItems[1].Bounds.X) / FontWidth);
            }

            //Update the range (including end position since were selecting one character so far)
            DataSelection.StartIndex = RowIndex;
            DataSelection.EndIndex = RowIndex;

            DataSelection.StartRow = Row;
            DataSelection.EndRow = DataSelection.StartRow;

            //Force a redraw
            DataListView.Refresh();
        }

        private void DataMouseCapture(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int Row = (e.Y - ColumnHeight) / ItemHeight;
                int RowIndex = 0;

                //Check if selecting hex section
                if (e.X > DataListView.Items[0].SubItems[1].Bounds.X && e.X < DataListView.Items[0].SubItems[1].Bounds.X + DataListView.Items[0].SubItems[1].Bounds.Width)
                {
                    RowIndex = (int)((e.X - DataListView.Items[0].SubItems[1].Bounds.X) / FontWidth) + 1;
                }

                //Check if row or index has changed for selection
                if (DataSelection.EndRow != Row || DataSelection.EndIndex != RowIndex)
                {
                    //Update with new values and refresh
                    DataSelection.EndRow = Row;
                    DataSelection.EndIndex = RowIndex;
                    DataListView.Refresh();
                }

            }
        }

        public struct SelectionRange
        {
            public int StartIndex, EndIndex, StartRow, EndRow;

            public SelectionRange(int StartIndex, int EndIndex, int StartRow, int EndRow)
            {
                this.StartIndex = StartIndex;
                this.EndIndex = EndIndex;
                this.StartRow = StartRow;
                this.EndRow = EndRow;
            }

        }


        #endregion

        #region Draw
        /*
         * We set OwnerDraw to true for the DataListView because we need to be able to
         * highlight text we wish to select (which is unsupported by the listview)
         */

        //Stores text to draw later to prevent some stupid issues
        List<ListViewTextItem> TextQueue = new List<ListViewTextItem>();

        /// <summary>
        /// Draws text for the listview item
        /// </summary>
        private void DataListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Draw the item text for views other than the Details view. 
            if (DataListView.View != View.Details)
                e.DrawText();
        }

        /// <summary>
        /// Draws SubItem text
        /// </summary>
        private void DataListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //Correct values for when the end of a selection comes before the beginning
            //(eg user started selection and highlighted upwards)
            SelectionRange ActualDataSelection = DataSelection;
            //Reverse rows if end precedes start
            if (ActualDataSelection.EndRow < ActualDataSelection.StartRow)
            {
                ActualDataSelection.EndRow = DataSelection.StartRow;
                ActualDataSelection.StartRow = DataSelection.EndRow;
            }
            //Reverse last and first index if end precedes start (in some cases)
            if (ActualDataSelection.EndIndex < ActualDataSelection.StartIndex)
            {
                //Only reverse if attempting to highlight upwards, otherwise
                //The start and end indexes are supposed to be what they are
                if (DataSelection.StartRow >= DataSelection.EndRow)
                {
                    ActualDataSelection.EndIndex = DataSelection.StartIndex;
                    ActualDataSelection.StartIndex = DataSelection.EndIndex;
                    //And more minor corrections...
                    ActualDataSelection.EndIndex++;
                    ActualDataSelection.StartIndex--;
                }
            }

            //We dont want to highlight the address column
            if (e.ColumnIndex > 0)
            {
                if (e.ColumnIndex == 1)
                {
                    //Check if first item selected is a space, if so don't highlight it.
                    // string FilteredText = e.Item.SubItems[1].Text;
                    //  FilteredText = FilteredText.Substring(ActualDataSelection.StartIndex, 1);
                    //  if (FilteredText == " ")
                    //      ActualDataSelection.StartIndex++;
                    //2,5,8,11,14,17,20,23
                }

                //Check if currently the ascii/unicode column
                if (e.ColumnIndex == 2)
                {
                    if (DisplayAscii == true)
                    {
                        ///Ascii end index of our selection
                        string FilteredText = e.Item.SubItems[1].Text;
                        FilteredText = FilteredText.Substring(0, ActualDataSelection.StartIndex + 1);
                        int Length = FilteredText.Length;
                        FilteredText = FilteredText.Replace(" ", "");
                        Length = Length - FilteredText.Length;
                        ActualDataSelection.StartIndex = Length;

                        ///Ascii end index of our selection
                        FilteredText = e.Item.SubItems[1].Text;
                        FilteredText = FilteredText.Substring(0, ActualDataSelection.EndIndex + 3);
                        Length = FilteredText.Length;
                        FilteredText = FilteredText.Replace(" ", "");
                        Length = Length - FilteredText.Length;
                        ActualDataSelection.EndIndex = Length;
                    }
                    else //Unicode
                    {
                        //Same thing but divide by 4
                    }
                }

                //New empty rectangle
                Rectangle rect = new Rectangle();

                //Depending on rows, selection indices, etc, set the rectangle size that needs highlighting

                //If within (but not the same as) the start/end rows, the row is completely highlighted
                if (e.Item.Index > ActualDataSelection.StartRow && e.ItemIndex < ActualDataSelection.EndRow)
                {
                    rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                }
                //Check if the current row is the same as the start row
                else if (e.Item.Index == ActualDataSelection.StartRow)
                {
                    //Set our rectangle starting at the first selected index all the way to the end
                    rect = new Rectangle(e.Bounds.X + (int)(FontWidth * ActualDataSelection.StartIndex), e.Bounds.Y, e.Bounds.Width - (int)(FontWidth * ActualDataSelection.StartIndex), e.Bounds.Height);

                    //However if the row is also the end row, we don't want to go to the end,
                    //But instead to the last selected index
                    if (ActualDataSelection.StartRow == ActualDataSelection.EndRow)
                        rect.Width = (int)(FontWidth * ActualDataSelection.EndIndex - FontWidth * ActualDataSelection.StartIndex);

                }
                //Check if the current row is the same as the end row
                else if (e.Item.Index == ActualDataSelection.EndRow)
                    //Highlight from start to the last selected index
                    rect = new Rectangle(e.Bounds.X, e.Bounds.Y, (int)(FontWidth * ActualDataSelection.EndIndex), e.Bounds.Height);

                rect.X += 2;
                rect.Width -= 6;

                if (ActualDataSelection.StartRow != ActualDataSelection.EndRow &&
                    ActualDataSelection.StartIndex != ActualDataSelection.EndIndex)
                {
                    //Apply updates values for our rectangle (if there were any)
                    e.Graphics.SetClip(rect);
                    //Highlight our selected text
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rect);
                    e.Graphics.ResetClip(); //Not sure if this is even needed
                }
            }


            /*
             * Draws the SubItem text. To prevent an issue where the text draws
             * behind the background at certain points we must add anything from the
             * first two columns (address & hex) to a queue and then draw them when the
             * second column begins to draw so that they draw over the background correctly.
             */

            //Only draw on the last column (0,1,2)
            if (e.Header.DisplayIndex == 2)
            {
                //Draw the text for this subitem of the last column
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font,
                    new Point(e.Bounds.X, e.Bounds.Y), SystemColors.ControlText);

                //All of these will draw after the first item in last column draws
                foreach (ListViewTextItem Next in TextQueue)
                {
                    TextRenderer.DrawText(e.Graphics, Next.Text, DataListView.Font,
                        new Point(Next.Coords.X, Next.Coords.Y), SystemColors.ControlText);
                }
                //Clear since we just drew everything
                TextQueue.Clear();

            }
            else //Add it too the queue to draw, were not drawing the last column text yet
                TextQueue.Add(new ListViewTextItem(new Point(e.Bounds.X, e.Bounds.Y), e.SubItem.Text));
        }

        /// <summary>
        /// Draws Column Header text
        /// </summary>
        private void DataListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //Draw the standard header background.
            e.DrawBackground();
            //Draw header text
            using (StringFormat sf = new StringFormat())
                e.Graphics.DrawString(e.Header.Text, DataListView.Font,
                    Brushes.Black, e.Bounds, sf);
        }

        //Used to store basic information to draw later
        private struct ListViewTextItem
        {
            public ListViewTextItem(Point Coords, String Text)
            {
                this.Coords = Coords;
                this.Text = Text;
            }
            public Point Coords;
            public String Text;
        }

        #endregion

    }
}