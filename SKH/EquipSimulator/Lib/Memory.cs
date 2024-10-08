using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DitCim.Lib
{
    public class Memory
    {
        public static short GetShort(short[] array, int start)
        {
            if ((array == null) || (array.Length < start + 1))
            {
                return 0;
            }

            return array[start];
        }
        public static int GetInt(short[] array, int start)
        {
            if ((array == null) || (array.Length < start + 2))
            {
                return 0;
            }

            byte[] data1 = BitConverter.GetBytes(array[start]);
            byte[] data2 = BitConverter.GetBytes(array[start + 1]);
            byte[] data = new byte[4] { data1[0], data1[1], data2[0], data2[1] };

            return BitConverter.ToInt32(data, 0);
        }
        public static int GetByte(short[] array, int start, int dataindex)
        {
            if ((array == null) || (array.Length < start + 2))
            {
                return 0;
            }

            byte[] data1 = BitConverter.GetBytes(array[start]);
            byte[] data2 = BitConverter.GetBytes(array[start + 1]);

            if (dataindex == 1)
                return data1[0];
            else
                return data1[1];
        }
        public static float GetFloat(short[] array, int start)
        {
            if ((array == null) || (array.Length < start + 2))
            {
                return 0.0f;
            }

            byte[] data1 = BitConverter.GetBytes(array[start]);
            byte[] data2 = BitConverter.GetBytes(array[start + 1]);
            byte[] data = new byte[4] { data1[0], data1[1], data2[0], data2[1] };

            return BitConverter.ToSingle(data, 0);
        }
        public static string GetAscii(short[] array, int start, int length)
        {
            if ((array == null) || (array.Length < start + length))
            {
                return "";
            }

            byte[] data = new byte[length * 2];
            for (int i = 0; i < length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(array[start + i]);
                data[i * 2] = bytes[0];
                data[i * 2 + 1] = bytes[1];
            }

            return Encoding.ASCII.GetString(data).Trim(new char[] { '\0', '\r', '\n', '\t', ' ' });
        }
        public static string GetBit(short[] array, int start, int length)
        {
            if ((array == null) || (array.Length < start + length))
            {
                return "";
            }

            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += Convert.ToString(array[start + i], 2).PadLeft(8, '0');
            }

            return result;
        }
        //public static string GetValue(short[] array, int start, int length, int type, string rate, string format)
        //{
        //    if (type == 0) // Dec
        //    {
        //        string value = "";

        //        if (length == 1) value = GetShort(array, start).ToString();
        //        else value = GetInt(array, start).ToString();

        //        // 비율 변경
        //        if (rate != "")
        //        {
        //            value = (CoreParse.ToDouble(value) * CoreParse.ToDouble(rate)).ToString();
        //        }

        //        // 형식 변경
        //        if (format != "")
        //        {
        //            value = string.Format("{0:" + format + "}", CoreParse.ToDouble(value));
        //        }

        //        return value;
        //    }
        //    else if (type == 1) // Ascii
        //    {
        //        return GetAscii(array, start, length);
        //    }
        //    else if (type == 2) // Float
        //    {
        //        if (length == 2) return GetFloat(array, start).ToString();
        //    }
        //    else if (type == 3) // Bit
        //    {
        //        return GetBit(array, start, length);
        //    }

        //    return "";
        //}
    }
}
