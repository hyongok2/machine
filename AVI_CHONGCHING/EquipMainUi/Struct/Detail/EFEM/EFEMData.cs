using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public class EFEMData
    {
        string BOC { get; set; }
        public string Command { get; private set; }
        public string Port { get; private set; }
        string ClfSignStart { get; set; }
        public string Data { get; private set; }
        string ClfSignEnd { get; set; }
        public string CheckSum { get; private set; }
        string CR { get; set; }

        public EFEMData(string _command, string _port, string _data = null)
        {
            BOC = "@";
            Command = _command;
            Port = _port;
            ClfSignStart = "[";
            Data = _data;
            ClfSignEnd = "]";
            CheckSum = "00";
            CR = "\r";
        }
        public EFEMData(string recvPacket)
        {
            BOC = recvPacket[0].ToString();
            Command = recvPacket.Substring(1, 5);
            Port = recvPacket[6].ToString();
            if (BOC.Equals("#")
                || BOC.Equals(">"))
            {
                Data = recvPacket.Substring(7, recvPacket.Length - 10);
            }
            CheckSum = recvPacket.Substring(recvPacket.Length - 3, 2);
            CR = recvPacket[recvPacket.Length - 1].ToString();
        }

        public bool CheckReceiveDataCS(string packet)
        {
            //packet = "@LPLED1[01]FB\r";
            string packetRemoveCR = packet.Split('\r')[0];

            string checkSum = packetRemoveCR.Substring(packetRemoveCR.Length-2, 2);

            if (checkSum.Equals(GetCheckSum(packetRemoveCR.Substring(0, packetRemoveCR.Length - 2))))
            {
                return true;
            }
            return false;
        }

        private string GetCheckSum(string packet)
        {
            //string str = "#INIT~00000000";

            int checksum = 0;

            foreach (char c in packet)
            {
                checksum += c;
            }
            string temp = string.Format("{0:X4}", checksum);
            temp = temp.Substring(temp.Length - 2, 2);

            return temp;
        }

        public override string ToString()
        {
            string str = string.Empty;

            str += BOC;
            str += Command.Replace('_', '~');
            str += Port;

            if(Data != "")
            {
                str += ClfSignStart;
                str += Data;
                str += ClfSignEnd;
            }

            str += GetCheckSum(str);
            str += CR;

            return str;
        }
    }
}
