using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Dit.Framework.PLC
{
    public enum MelsecNetDevice
    {
        R = 22,
        ZR = 220,
        B = 23,
        W = 24,
        D = 13,
        M = 4,
    }

    public class MelsecNet
    {
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdOpen(short Chan, short Mode, ref int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdClose(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdSend(int Path, short Stno, short Devtyp, short devno, ref short size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdReceive(int Path, short Stno, short Devtyp, short devno, ref short size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdDevSet(int Path, short Stno, short Devtyp, short devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdDevRst(int Path, short Stno, short Devtyp, short devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdRandW(int Path, short Stno, ref short dev, ref short buf, short bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdRandR(int Path, short Stno, ref short dev, ref short buf, short bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdControl(int Path, short Stno, short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdTypeRead(int Path, short Stno, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdLedRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdModRead(int Path, ref short Mode);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdModSet(int Path, short Mode);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdRst(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdSwRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdVerRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdInit(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdWaitBdEvent(int Path, ref short eventno, int timeout, ref short signaledno, ref short details);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdSendEx(int Path, int Netno, int Stno, int Devtyp, int devno, ref int size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdReceiveEx(int Path, int Netno, int Stno, int Devtyp, int devno, ref int size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdDevSetEx(int Path, int Netno, int Stno, int Devtyp, int devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdDevRstEx(int Path, int Netno, int Stno, int Devtyp, int devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdRandWEx(int Path, int Netno, int Stno, ref int dev, ref short buf, int bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdRandREx(int Path, int Netno, int Stno, ref int dev, ref short buf, int bufsiz);

        private int channelPath = 0;
        private short stNo = 0;
        private short stNoEx = 0;
        private short netNoEx = 0; // = 1;   // PLC Network No


        private short GetChannelNum(string channelText)
        {
            short result = -1;
            switch (channelText)
            {
                case "A3A/A3N": result = 0; break;
                case "RS4": result = 1; break;
                case "MNET2-1": result = 21; break;
                case "MNET2-2": result = 22; break;
                case "MNET10-1": result = 51; break;
                case "MNET10-2": result = 52; break;
                case "MNET10-3": result = 53; break;
                case "MNET10-4": result = 54; break;
                case "MNETG-1": result = 151; break;
                case "MNETG-2": result = 152; break;
                case "MNETG-3": result = 153; break;
                case "MNETG-4": result = 154; break;
            }
            return result;
        }
        private short GetMnetModeNum(string mnetModeText)
        {
            short result = -1;
            switch (mnetModeText)
            {
                case "MELSECNET(usual)": result = 0; break;
                case "MELSECNET-II": result = 1; break;
                case "MELSECNET(mixed)": result = 2; break;
            }
            return result;
        }


        private int BitSet(short networkNo, short stationNo, MelsecNetDevice device, int address)
        {
            return mdDevSetEx(channelPath, networkNo, stationNo, (int)device, address);
        }
        private int BitRst(short networkNo, short stationNo, MelsecNetDevice device, int address)
        {
            return mdDevRstEx(channelPath, networkNo, stationNo, (int)device, address);
        }
        private int SendText(short networkNo, short stationNo, MelsecNetDevice device, int start, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            byte[] bytes = Encoding.Default.GetBytes(text + "\0");
            short[] data = new short[bytes.Length / 2];
            int sendSize = data.Length * 2;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (short)(bytes[i * 2 + 1] * 0x100 + bytes[i * 2]);
            }

            return mdSendEx(channelPath, networkNo, stationNo, (int)device, start, ref sendSize, ref data[0]);
        }

        private int SendShort(short networkNo, short stationNo, MelsecNetDevice device, int start, short value)
        {
            int sendSize = 2;
            return mdSendEx(channelPath, networkNo, stationNo, (int)device, start, ref sendSize, ref value);
        }

        private bool[] ReceiveBit(short networkNo, short stationNo, MelsecNetDevice dev, int start, int size)
        {
            if (size <= 0)
            {
                return new bool[0];
            }

            bool[] result = new bool[size];
            short[] data = new short[(size + 15) / 16];
            int recvSize = data.Length * 2;

            if (mdReceiveEx(channelPath, networkNo, stationNo, (int)dev, start, ref recvSize, ref data[0]) != 0)
            {
                return null;
            }

            for (int i = 0; i < size; i++)
            {
                int index1 = i / 16;
                int index2 = i % 16;

                switch (index2)
                {
                    case 0: if ((data[index1] & 0x1) == 0x1) result[i] = true; break;
                    case 1: if ((data[index1] & 0x2) == 0x2) result[i] = true; break;
                    case 2: if ((data[index1] & 0x4) == 0x4) result[i] = true; break;
                    case 3: if ((data[index1] & 0x8) == 0x8) result[i] = true; break;
                    case 4: if ((data[index1] & 0x10) == 0x10) result[i] = true; break;
                    case 5: if ((data[index1] & 0x20) == 0x20) result[i] = true; break;
                    case 6: if ((data[index1] & 0x40) == 0x40) result[i] = true; break;
                    case 7: if ((data[index1] & 0x80) == 0x80) result[i] = true; break;
                    case 8: if ((data[index1] & 0x100) == 0x100) result[i] = true; break;
                    case 9: if ((data[index1] & 0x200) == 0x200) result[i] = true; break;
                    case 10: if ((data[index1] & 0x400) == 0x400) result[i] = true; break;
                    case 11: if ((data[index1] & 0x800) == 0x800) result[i] = true; break;
                    case 12: if ((data[index1] & 0x1000) == 0x1000) result[i] = true; break;
                    case 13: if ((data[index1] & 0x2000) == 0x2000) result[i] = true; break;
                    case 14: if ((data[index1] & 0x4000) == 0x4000) result[i] = true; break;
                    case 15: if ((data[index1] & 0x8000) == 0x8000) result[i] = true; break;
                }
            }

            return result;
        }
        private short[] ReceiveWord(short networkNo, short stationNo, MelsecNetDevice dev, int start, int size)
        {
            if (size <= 0)
            {
                return new short[0];
            }

            short[] result = new short[size];
            int recvSize = result.Length * 2;
            if (mdReceiveEx(channelPath, networkNo, stationNo, (int)dev, start, ref recvSize, ref result[0]) != 0)
            {
                return null;
            }

            return result;
        }

        public void InitNetworks(short networkNo, short stationNo)
        {
            netNoEx = networkNo;
            stNoEx = stationNo;
            stNo = (short)((networkNo * 0x100) | (stationNo & 0xFF));
        }
        public short Connect(string channel, string mnetMode)
        {

#if DEBUG
            return 0;
#endif
            short mCH = GetChannelNum(channel);
            short mMD = GetMnetModeNum(mnetMode);

            return mdOpen(mCH, mMD, ref channelPath);
        }
        public short Disconnect()
        {
            return mdClose(channelPath);
        }
        
        public int BitSet(MelsecNetDevice device, int address)
        {
            return BitSet(netNoEx, stNoEx, device, address);
        }
        public int BitRst(MelsecNetDevice device, int address)
        {
            return BitRst(netNoEx, stNoEx, device, address);
        }
        public int SendText(MelsecNetDevice device, int start, string text)
        {
            return SendText(netNoEx, stNoEx, device, start, text);
        }

        public int SendShort(MelsecNetDevice device, int start, short value)
        {
            return SendShort(netNoEx, stNoEx, device, start, value);
        }

        //public int SendText2(MelsecNetDevice device, int start, string text)
        //{
        //    return SendText(Global.NETWORK_NO, Global.STATION_NO_INDEX, device, start, text);
        //}
        //public int SendShort2(MelsecNetDevice device, int start, short value)
        //{
        //    return SendShort(Global.NETWORK_NO, Global.STATION_NO_INDEX, device, start, value);
        //}

        public bool[] ReceiveBit(MelsecNetDevice device, int start, int size)
        {

            return ReceiveBit(netNoEx, stNoEx, device, start, size);
        }
        public short[] ReceiveWord(MelsecNetDevice device, int start, int size)
        {
            return ReceiveWord(netNoEx, stNoEx, device, start, size);
        }

        public int OwnBitSet(MelsecNetDevice device, int address)
        {
            return BitSet(0, 0xFF, device, address);
        }
        public int OwnBitRst(MelsecNetDevice device, int address)
        {
            return BitRst(0, 0xFF, device, address);
        }
        public int OwnSendText(MelsecNetDevice device, int start, string text)
        {
            return SendText(0, 0xFF, device, start, text);
        }
        public int OwnSendShort(MelsecNetDevice device, int start, short value)
        {
            return SendShort(0, 0xFF, device, start, value);
        }
        public bool[] OwnReceiveBit(MelsecNetDevice device, int start, int size)
        {
            return ReceiveBit(0, 0xFF, device, start, size);
        }
        public short[] OwnReceiveWord(MelsecNetDevice device, int start, int size)
        {
            return ReceiveWord(0, 0xFF, device, start, size);
        }
        //public short[] OwnReceiveWord2(MelsecNetDevice device, int start, int size)
        //{
        //    return ReceiveWord(Global.NETWORK_NO, Global.STATION_NO_INDEX, device, start, size);
        //}

    }
}
