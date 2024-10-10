using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EquipSimulator
{
    public static class UserExtension
    {
        public static System.Boolean IsNumeric(this string it)
        {
            try
            {
                Double.Parse(it);
                return true;
            }
            catch { }
            return false;
        }
  
        
        public static bool GetBit(this short it, int bit)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(it));
            return bitArray[bit];
        }
        public static short[] GetBits(this short it)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(it));
            short[] bits = new short[16];

            for (int iPos = 0; iPos < 16; iPos++)
                bits[iPos] = (short)(bitArray[iPos] ? 1 : 0);

            return bits;
        }
        public static BitArray ToBitArray(this short it)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(it));
            return bitArray;
        }

        public static byte GetByte(this short it, int start)
        {
            byte[] bytes = BitConverter.GetBytes(it);
            return bytes[start];
        }
        public static string GetPlcAscii(this short[] it, int start, int length)
        {
            if ((it == null) || (it.Length < start + length))
                return "";

            byte[] data = new byte[length * 2];
            for (int iPos = 0; iPos < length; iPos++)
            {
                byte[] bytes = BitConverter.GetBytes(it[start + iPos]);
                data[iPos * 2 + 0] = bytes[0];
                data[iPos * 2 + 1] = bytes[1];
            }

            return Encoding.ASCII.GetString(data).Trim(new char[] { '\0', '\r', '\n', '\t', ' ' });
        }
        public static void SetPlcAscii(this short[] it, int start, int length, string text)
        {
            if ((it == null) || (it.Length < start + length))
                return;

            string str = text.PadRight(length * 2, '\0');

            //byte[] data = new byte[length * 2];
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            for (int iPos = 0; iPos < length; iPos++)
            {
                it[start + iPos] = (short)(bytes[iPos * 2 + 1] * 0x100 + bytes[iPos * 2]);
            }
        }
    }
}
