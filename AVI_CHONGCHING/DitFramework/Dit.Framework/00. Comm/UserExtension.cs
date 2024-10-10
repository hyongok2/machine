using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Dit.Framework.Comm
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

        //BYTE 배열에서 형변환.
        public static bool GetBit(this byte it, int bit)
        {
            short nBit = (short)(1 << bit);
            bool result = ((it & nBit) == nBit);

            return result;
        }
        public static byte SetBit(this byte it, int bit, bool value)
        {
            short nBit = (short)(1 << bit);
            if (value)
            {
                return (byte)(it | nBit);
            }
            else
            {
                return (byte)(it & ~nBit);
            }
        }
        public static bool GetBit(this short it, int bit)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(it));
            return bitArray[bit];
        }
        public static short SetBit(this short it, int bit, bool value)
        {
            short nBit = (short)(1 << bit);
            if (value)
            {
                return (short)(it | nBit);
            }
            else
            {
                return (short)(it & ~nBit);
            }
        }

        public static bool OnBitValue(this int it, int value)
        {
            return (it & value) == value;
        }

        public static bool GetBit(this int it, int bit)
        {
            int nBit = (int)(1 << bit);
            bool result = ((it & nBit) == nBit);

            return result;
        }
        public static int SetBit(this int it, int bit, bool value)
        {
            int nBit = (int)(1 << bit);
            if (value)
            {
                return (int)(it | nBit);
            }
            else
            {
                return (int)(it & ~nBit);
            }
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
        
        //SHORT 배열에서 형변환.
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
        public static int IndexOfAny(this byte[] it, byte[] anyOf, int startIndex, int count)
        {
            if (anyOf == null)
                throw new Exception("anyOf");

            if (startIndex < 0 || startIndex > it.Length)
                throw new Exception("startIndex");

            if (count < 0 || startIndex > it.Length - count)
                throw new Exception("count");

            for (int iPos = startIndex; iPos < it.Length - count; iPos++)
            {
                bool find = true;
                for (int jPos = 0; jPos < count; jPos++)
                {
                    if (it[iPos + jPos] != anyOf[jPos])
                    {
                        find = false;
                        break;
                    }
                }

                if (find == true)
                    return iPos;
            }

            return -1;
        }
        public static int IndexOfAny(this byte[] it, byte anyOf)
        {
            if (anyOf == null)
                throw new Exception("anyOf");

            for (int iPos = 0; iPos < it.Length; iPos++)
            {
                bool find = true;
                if (it[iPos] != anyOf)
                {
                    find = false;
                    continue;
                }

                if (find == true)
                    return iPos;
            }

            return -1;
        }
        public static string GetAscii(this short[] it, int start, int length)
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
        public static void SetAscii(this short[] it, int start, int length, string text)
        {
            if ((it == null) || (it.Length < start + length))
                return;

            string str = text.PadRight(length * 2, '\0');
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            for (int iPos = 0; iPos < length; iPos++)
            {
                it[start + iPos] = (short)(bytes[iPos * 2 + 1] * 0x100 + bytes[iPos * 2]);
            }
        }
        public static int GetInt32R(this short[] it, int start)
        {
            byte[] bytes = new byte[4];
            bytes[0] = it[start + 1].GetByte(0);
            bytes[1] = it[start + 1].GetByte(1);
            bytes[2] = it[start + 0].GetByte(0);
            bytes[3] = it[start + 0].GetByte(1);

            return BitConverter.ToInt32(bytes, 0);
        }
        public static int GetInt32(this short[] it, int start)
        {
            byte[] bytes = new byte[4];
            bytes[0] = it[start + 0].GetByte(0);
            bytes[1] = it[start + 0].GetByte(1);
            bytes[2] = it[start + 1].GetByte(0);
            bytes[3] = it[start + 1].GetByte(1);

            return BitConverter.ToInt32(bytes, 0);
        }
        public static void SetInt32(this short[] it, int start, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            it[start] = (short)(bytes[0] + bytes[1] * 0x100);
            it[start + 1] = (short)(bytes[2] + bytes[3] * 0x100);
        }   
        public static short GetBCD(this short it, int bitPosi, int bitSize)
        {            
            short value1 = 0;
            for (int iPos = 0; iPos < bitSize; iPos++)
                value1 = (short)(value1 + (1 << iPos));

            short value2 = (short)(it >> bitPosi & value1);
            return value2;
        }
        public static short GetBCD(this short it, int bitPosi)
        {
            short value1 = 0;
            for (int iPos = 0; iPos < 4; iPos++)
                value1 = (short)(value1 + (1 << iPos));

            short value2 = (short)(it >> bitPosi & value1);
            return value2;
        }
        public static short SetBCDToHex(this short it, int bitPosi, short value)
        {
            if (value < 10)
            {
                return it.SetBCDInner(bitPosi, value);
            }
            else
            {
                short vv = it.SetBCDInner(bitPosi, (short)(value % 10));
                return vv.SetBCDInner(bitPosi + 4, (short)(Math.Truncate(value / 10f)));
            }
        }
        public static short SetBCD(this short it, int bitPosi, short value)
        {
            return it.SetBCDInner(bitPosi, value);            
        }

        public static short SetBCDInner(this short it, int bitPosi, short value)
        {
            short value1 = (short)(value << bitPosi);
            short value2 = (short)(it | value1);
            return value2;
        }
        public static string GetAscii(this byte[] it, int start, int length)
        {
            if ((it == null) || (it.Length < start + length))
                return "";

            byte[] data = new byte[length];
            for (int iPos = 0; iPos < length; iPos++)
                data[iPos] = it[start + iPos];

            return Encoding.ASCII.GetString(data).Trim(new char[] { '\0', '\r', '\n', '\t', ' ' });
        }
        public static void SetAscii(this byte[] it, int start, int length, string text)
        {
            if ((it == null) || (it.Length * 2 < start + length))
                return;

            string str = text.PadRight(length, '\0');
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            for (int iPos = 0; iPos < length; iPos++)
                it[start + iPos] = bytes[iPos];
        }
        public static void SetShort(this byte[] it, int start, short vaule)
        {
            if ((it == null) || (it.Length < start))
                return;

            byte[] data = BitConverter.GetBytes(vaule);
            it[start] = data[0];
            it[start + 1] = data[1];
        }
        public static short GetShort(this byte[] it, int start)
        {
            if ((it == null) || (it.Length < start))
                throw new Exception("GetShort");

            return BitConverter.ToInt16(it, start);
        }
        public static short[] GetShorts(this bool[] it, int start, int wordSize)
        {
            short[] shorts = new short[wordSize];
            int ss = start;

            for (int iPos = 0; iPos < wordSize; iPos++)
            {
                for (int jPos = 0; jPos < 16; jPos++)
                {
                    if (it[ss])
                    {
                        short nBit = (short)(1 << jPos);
                        shorts[iPos] = (short)(shorts[iPos] | nBit);
                    }
                    ss++;
                }
            }
            return shorts;
        }
        public static byte[] GetBytes(this bool[] it, int start, int byteLen)
        {
            byte[] bytes = new byte[byteLen];
            int ss = start;

            for (int iPos = 0; iPos < byteLen; iPos++)
            {
                for (int jPos = 0; jPos < 8; jPos++)
                {
                    if (it[ss])
                    {
                        byte nBit = (byte)(1 << jPos);
                        bytes[iPos] = (byte)(bytes[iPos] | nBit);
                    }
                    ss++;
                }
            }
            return bytes;
        }
        public static void SetShorts(this byte[] it, int start, short[] vaule)
        {
            if ((it == null) || (it.Length < start))
                return;
            for (int iPos = 0; iPos < vaule.Length; iPos++)
            {
                byte[] data = BitConverter.GetBytes(vaule[iPos]);
                it[iPos * 2 + start] = data[0];
                it[iPos * 2 + start + 1] = data[1];
            }
        }

        public static byte GetCheckSum_XOR(this byte[] it, int startIndex, int endIndex)
        {
            byte checkSum = 0xFF;
            for (int iPos = startIndex; iPos <= endIndex; iPos++)
                checkSum ^= it[iPos];

            return checkSum;
        }
        public static string GetString(this byte[] it, int startIndex, int count)
        {
            return Encoding.UTF8.GetString(it, startIndex, count);
        }
        public static string[] GetStrings(this byte[] it, int startIndex, int size, int count)
        {
            string[] value = new string[count];
            for (int iPos = 0; iPos < count; iPos++)
            {
                value[iPos] = it.GetString(startIndex + iPos * size, size);
            }

            return value;
        }
        public static int GetDigit(this byte[] it, int startIndex, int count)
        {
            int value = 0;
            for (int iPos = startIndex; iPos < startIndex + count; iPos++)
            {
                value = it[iPos] * 10 ^ (count - iPos - startIndex);
            }

            return value;
        }
        public static int SetDigit(this byte[] it, int value, int startIndex, int count)
        {
            for (int iPos = startIndex; iPos < startIndex + count; iPos++)
            {
                it[iPos] = (byte)Math.Truncate(value / (10 ^ (count - iPos - startIndex)) * 1f);
            }

            return value;
        }
        public static int[] GetDigits(this byte[] it, int startIndex, int size, int count)
        {
            int[] value = new int[count];

            for (int iPos = 0; iPos < count; iPos++)
            {
                value[iPos] = it.GetDigit(startIndex + iPos * size, size);
            }
            return value;
        }
    }
}
