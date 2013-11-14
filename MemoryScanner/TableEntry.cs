using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Aecial.MemoryScanner;

namespace AecialEngine
{

    /// <summary>
    /// Contains information regarding an entry on the table
    /// </summary>
    [Serializable()] //Yay for C# and serialization of classes -- easy file saving for me.
    class TableEntry
    {
        public string Description;
        public UInt64 Address;
        public ScanDataType ScanType;
        public object Value;
        public bool CheckState;

        public bool IsSigned;
        public bool ValueAsHex;
        public bool AddressAsHex;
        public string ASMScript = "";

        public TableEntry(string Description, UInt64 Address, ScanDataType ScanType, bool IsSigned, bool ValueAsHex, bool AddressAsHex, string ASMScript)
        {
            //Load in initial data
            this.Description = Description;
            this.Address = Address;
            this.ScanType = ScanType;
            this.IsSigned = IsSigned;
            this.ValueAsHex = ValueAsHex;
            this.AddressAsHex = AddressAsHex;
            this.ASMScript = ASMScript;

            //Determined later
            Value = null;
            CheckState = false;
        }
    }
}