using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AecialEngine;

namespace Aecial.MemoryScanner
{
    /// <summary>
    /// Checks syntax for various types (hex, addresses, values by type, etc)
    /// </summary>
    public class CheckSyntax
    {
        private const int MaxAddressLength = 8;
        //Determines if a string contains all zeros
        private static bool IsZeroX(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                //Check each char for a value of 0 if not break & return false
                if (value.Substring(i, 1) != "0")
                    break;

                //Check if all characters were 0 and return true if so
                if (i == value.Length - 1)
                    return true;
            }

            return false;
        }

        //Determines if a string is empty (easy check, but cleaner looking as a method)
        private static bool IsBlank(string value)
        {
            if (value.Length == 0)
                return true;
            return false;
        }

        //Checks if passed value is a valid address
        public static bool Address(string address)
        {
            if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                address = address.Substring(2);

            //Out of bounds
            if (address.Length > MaxAddressLength || IsBlank(address))
                return false;

            //Too short: assume preceding 0s are intended
            while (address.Length < MaxAddressLength)
                address = "0" + address;

            int result;
            if (Int32.TryParse(address, System.Globalization.NumberStyles.HexNumber, null, out result))
                return true; //Valid
            return false;
        }

        //Checks if passed value is a valid hex value
        public static bool HexValue(string value, int bytes)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);

            //Remove leading 0s and check if out of bounds
            while (value.Length > bytes)
            {
                if (value.Substring(0, 1) == "0")
                    value = value.Substring(1);
                else   //Value too large (there were no leading 0s that caused it)
                    return false;
            }

            //Check if string is empty
            if (IsBlank(value))
                return false;

            //Try to read value from hex string
            int result;
            if (Int32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                return true;

            return false; //Invalid
        }

        //Checks a value based on a scantype by calling other syntax checking methods
        public static bool Value(string value, ScanDataType ScanValueType, bool isHex)
        {
            switch (ScanValueType)
            {
                case ScanDataType.Binary:
                    return BinaryValue(value);
                case ScanDataType.Byte:
                    return ByteValue(value, isHex);
                case ScanDataType.Int16:
                    return Int16Value(value, isHex);
                case ScanDataType.Int32:
                    return Int32Value(value, isHex);
                case ScanDataType.Int64:
                    return Int64Value(value, isHex);
                case ScanDataType.Single:
                    return SingleValue(value, isHex);
                case ScanDataType.Double:
                    return DoubleValue(value, isHex);
                case ScanDataType.Text:
                    return true;
            }
            return false;
        }

        //Checks if passed value is a valid binary value
        public static bool BinaryValue(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                //Check each character for a value of 0
                if (value.Substring(i, 1) != "0" || value.Substring(i, 1) != "1")
                    break;

                //Returns true if all characters were 0 or 1
                if (i == value.Length - 1)
                    return true;
            }
            return false;
        }

        //Checks if passed value is a valid value for a byte
        public static bool ByteValue(string value, bool isHex)
        {
            //Remove 0x notation
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            //Check if string is empty
            if (IsBlank(value))
                return false;

            //Try to read a signed value
            SByte result;
            if (isHex)
            {
                //Try to read value from hex string
                if (SByte.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (SByte.TryParse(value, out result))
                    return true;

            //Try to read an unsigned value
            Byte _result;
            if (isHex)
            {
                //Try to read value from hex string
                if (Byte.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (Byte.TryParse(value, out _result))
                    return true;


            return false; //Invalid
        }

        //Checks if passed value is a valid value for a short
        public static bool Int16Value(string value, bool isHex)
        {
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            if (IsBlank(value))
                return false;

            //Try to read a signed value
            Int16 result;
            if (isHex)
            {
                if (Int16.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int16.TryParse(value, out result))
                    return true;

            //Try to read unsigned value
            UInt16 _result;
            if (isHex)
            {
                if (UInt16.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt16.TryParse(value, out _result))
                    return true;

            return false; //Invalid
        }

        //Checks if passed value is a valid value for a int
        public static bool Int32Value(string value, bool isHex)
        {
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            if (IsBlank(value))
                return false;

            //Try to read signed value
            Int32 result;
            if (isHex)
            {
                if (Int32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int32.TryParse(value, out result))
                    return true;

            //Try to read unsigned value
            UInt32 _result;
            if (isHex)
            {
                if (UInt32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt32.TryParse(value, out _result))
                    return true;

            return false; //Invalid
        }

        //Checks if passed value is a valid value for a long
        public static bool Int64Value(string value, bool isHex)
        {
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            if (IsBlank(value))
                return false;

            //Try to read signed value
            Int64 result;
            if (isHex)
            {
                if (Int64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                    return true;
            }
            else
                if (Int64.TryParse(value, out result))
                    return true;

            //Try to read unsigned value
            UInt64 _result;
            if (isHex)
            {
                if (UInt64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out _result))
                    return true;
            }
            else
                if (UInt64.TryParse(value, out _result))
                    return true;

            return false; //Invalid
        }

        //Checks if passed value is a valid value for a single
        public static bool SingleValue(string value, bool isHex)
        {
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            if (IsBlank(value))
                return false;

            Single resultS = 0;
            Int32 result32 = 0;

            if (isHex)
            {
                //Grab int32 value (int32 has same bytes as a single), and since this
                //is checking to see if there is any data, we don't care how those bytes are read
                if (Int32.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result32))
                    return true;
            }
            else
                if (Single.TryParse(value, out resultS))
                    return true;

            return false; //Invalid
        }

        //Checks if passed value is a valid value for a double
        public static bool DoubleValue(string value, bool isHex)
        {
            if (isHex)
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    value = value.Substring(2);

            if (IsBlank(value))
                return false;

            //Try to read value from hex string
            Double resultD = 0;
            Int64 result64 = 0;
            if (isHex)
            {
                //Grab int64 value (int64 has same bytes as a double), and since this
                //is checking to see if there is any data, we don't care how those bytes are read
                if (Int64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result64))
                    return true;
            }
            else
                if (Double.TryParse(value, out resultD))
                    return true;

            return false;
        }

    }
}