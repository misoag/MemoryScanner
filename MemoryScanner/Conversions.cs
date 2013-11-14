using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aecial.MemoryScanner;
using AecialEngine;

/// <summary>
/// Conversions to different types of commonly used values
/// </summary>
namespace Aecial.Conversions
{
    public class Conversions
    {
        //Converts to 8 digit address format
        public static string ToAddress(string value)
        {
            if (CheckSyntax.Int32Value(value, false))
                return String.Format("{0:X8}", Convert.ToUInt32(value));
            else if (CheckSyntax.Int64Value(value, false))
                return String.Format("{0:X16}", Convert.ToUInt64(value));
            else
                return "??";
        }
        public static string ToAddress(Int32 value)
        {
            return String.Format("{0:X8}", value);
        }
        public static string ToAddress(UInt64 value)
        {
            if (value <= 0xFFFFFFFF)
                return ToAddress((Int32)value);
            return String.Format("{0:X16}", value);
        }

        //Converts to hex value, with no specific number of digits
        public static string ToHex(string value)
        {
            return String.Format("{0:X}", Convert.ToUInt64(value));
        }

        public static DataTypeSize ScanDataTypeToDataTypeSize(ScanDataType ScanDataType)
        {
            switch (ScanDataType)
            {
                case ScanDataType.Byte:
                    return DataTypeSize.Byte;
                case ScanDataType.Int16:
                    return DataTypeSize.Int16;
                case ScanDataType.Int32:
                    return DataTypeSize.Int32;
                case ScanDataType.Int64:
                    return DataTypeSize.Int64;
                case ScanDataType.Single:
                    return DataTypeSize.Single;
                case ScanDataType.Double:
                    return DataTypeSize.Double;
            }
            return DataTypeSize.Int32;
        }

        public static ScanDataType ObjectTypeToDataType(Type Type)
        {
            string TypeAsString = Type.ToString().Remove(0, 7).ToLower();
            switch (TypeAsString) // Remove "System."
            {
                case "byte":
                case "sbyte":
                    return ScanDataType.Byte;
                case "uint16":
                case "int16":
                    return ScanDataType.Int16;
                case "uint32":
                case "int32":
                    return ScanDataType.Int32;
                case "uint64":
                case "int64":
                    return ScanDataType.Int64;
                case "single":
                    return ScanDataType.Single;
                case "double":
                    return ScanDataType.Double;
                default:
                    throw new Exception(TypeAsString + " is an unsupported type.");

            }
        }

        public static ScanCompareType StringToScanType(string value)
        {
            int CompareValue = 0;
            switch (value)
            {
                case "=":
                    CompareValue = 0;
                    break;
                case "Ø":
                    CompareValue = 1;
                    break;
                case "±":
                    CompareValue = 2;
                    break;
                case "≠":
                    CompareValue = 3;
                    break;
                case "+":
                    CompareValue = 4;
                    break;
                case "-":
                    CompareValue = 5;
                    break;
                case "+x":
                    CompareValue = 6;
                    break;
                case "-x":
                    CompareValue = 7;
                    break;
                case "<":
                    CompareValue = 8;
                    break;
                case ">":
                    CompareValue = 9;
                    break;
                case "≤":
                    CompareValue = 10;
                    break;
                case "≥":
                    CompareValue = 11;
                    break;
                case "> <":
                    CompareValue = 12;
                    break;
                case "≥ ≤":
                    CompareValue = 13;
                    break;
                case "??":
                    CompareValue = 14;
                    break;
                default:
                    throw new Exception(value + " is an unsupported type.");

            }

            return (ScanCompareType)CompareValue;
        }

        public static string DecToHex(string value, bool isAddress)
        {
            //Convert to hex

            //Check if valid syntax of an integer address
            if (CheckSyntax.Int32Value(value, false))
                if (isAddress)
                    value = Conversions.ToAddress(value);
                else
                    value = Conversions.ToHex(value);

            return value;
        }

        public static string HexToDec(string value, bool isAddress)
        {
            //Check if valid syntax of a hex address
            if (CheckSyntax.Int64Value(value, true))
                value = Conversions.HexToUInt64(value).ToString();

            //Return updated string
            return value;
        }

        public static object ToUnsigned(string value, ScanDataType ScanType)
        {
            object ScanValue = 0;
            bool signed = false;
            if (value.Substring(0, 1) == "-")
                signed = true;

            switch (ScanType)
            {
                case ScanDataType.Binary:
                    break;
                case ScanDataType.Byte:
                    if (signed) //if signed convert to unsigned (easier to deal with)
                    {
                        sbyte signedByte = Convert.ToSByte(value);
                        ScanValue = (byte)signedByte;
                    }
                    else
                        ScanValue = Convert.ToByte(value);
                    break;
                case ScanDataType.Int16:
                    if (signed)
                    {
                        Int16 signedInt16 = Convert.ToInt16(value);
                        ScanValue = (UInt16)signedInt16;
                    }
                    else
                        ScanValue = Convert.ToUInt16(value);
                    break;
                case ScanDataType.Int32:
                    if (signed)
                    {
                        Int32 signedInt32 = Convert.ToInt32(value);
                        ScanValue = (UInt32)signedInt32;
                    }
                    else
                        ScanValue = Convert.ToUInt32(value);
                    break;
                case ScanDataType.Int64:
                    if (signed)
                    {
                        Int64 signedInt64 = Convert.ToInt64(value);
                        ScanValue = (UInt64)signedInt64;
                    }
                    else
                        ScanValue = Convert.ToUInt64(value);
                    break;
                case ScanDataType.Single:
                    ScanValue = Convert.ToSingle(value);
                    break;
                case ScanDataType.Double:
                    ScanValue = Convert.ToDouble(value);
                    break;
                case ScanDataType.Text:
                    ScanValue = value;
                    break;
                case ScanDataType.AOB:
                    break;
                case ScanDataType.All:
                    break;
                default:
                    break;
            }

            return ScanValue;
        }

        public static string FormatAscii(byte[] bytes)
        {
            //Ascii 0-31 are all formatting related, so just change them to "."
            if (bytes[0] < 32)
                bytes[0] = 46; //ascii for "."
            //127+ are extended ascii characters, just display as "."
            if (bytes[0] > 126)
                bytes[0] = 46; //ascii for "."

            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string FormatUnicode(byte[] bytes)
        {
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        public static string BinaryToHex(int BinInt)
        {
            string Bin = BinInt.ToString();
            BinInt = Convert.ToInt32(Bin, 2);
            string Hex = BinInt.ToString("X");
            return Hex;
        }

        public static Byte HexToByte(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);
            return Byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        public static Int16 HexToShort(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);
            return Int16.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        public static Int32 HexToInt(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);
            return Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        public static UInt64 HexToUInt64(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);
            return UInt64.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        public static Single HexToSingle(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);

            //Int32 has 4 bytes just like a single; convert our hex number to an int32
            Int32 Int32Val = Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);

            //Convert those bytes to a Single value
            return BitConverter.ToSingle(BitConverter.GetBytes(Int32Val), 0); ;
        }

        public static Double HexToDouble(string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2);

            Int64 result64;
            //Grab int64 value (int 64 has same bytes as a double)
            Int64.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result64);
            //Convert int64 bits to double value
            return BitConverter.Int64BitsToDouble(result64);
        }

        public static string ScanTypeToName(ScanDataType _ScanType)
        {
            switch (_ScanType)
            {
                case ScanDataType.AOB:
                    return "Array of Bytes";
                case ScanDataType.All:
                    return ScanDataType.All.ToString();
                default:
                    return _ScanType.ToString();
            }
        }

        public static ScanDataType NameToScanType(string name)
        {
            switch (name)
            {
                case "Binary":
                    return ScanDataType.Binary;
                case "Byte":
                    return ScanDataType.Byte;
                case "Int16":
                    return ScanDataType.Int16;
                case "Int32":
                    return ScanDataType.Int32;
                case "Int64":
                    return ScanDataType.Int64;
                case "Single":
                    return ScanDataType.Single;
                case "Double":
                    return ScanDataType.Double;
                case "Text":
                    return ScanDataType.Text;
                case "Array of Bytes":
                    return ScanDataType.AOB;
                case "All":
                    return ScanDataType.All;
            }
            return ScanDataType.Int32;
        }
    }
}