using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace Dit.Framework.PMAC
{
    public class PowerPmac
    {
        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprOpen(UInt32 dwIPAddress, Int32 nPortNo);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprClose(UInt32 uDeviceID);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprConnect(UInt32 uDeviceID);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprDisconnect(UInt32 uDeviceID);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprIsConnected(UInt32 uDeviceID);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprGetDPRMem(UInt32 uDeviceID, UInt32  lStartAddr, UInt32 lLength, IntPtr val);

        //[DllImport("PPMACDPRLib.dll")]
        //public static extern UInt32 PPmacDprSetDPRMem(UInt32 uDeviceID, UInt32 lStartAddr, UInt32 lLength, IntPtr val);



        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprOpen(UInt32 dwIPAddress, Int32 nPortNo);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprClose(UInt32 uDeviceID);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprConnect(UInt32 uDeviceID);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprDisconnect(UInt32 uDeviceID);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprIsConnected(UInt32 uDeviceID);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprGetDPRMem(UInt32 uDeviceID, UInt32 lStartAddr, UInt32 lLength, IntPtr val);

        [DllImport("PPMACDPRLib_64.dll")]
        public static extern UInt32 PPmacDprSetDPRMem(UInt32 uDeviceID, UInt32 lStartAddr, UInt32 lLength, IntPtr val);


        public static UInt32 ToInt(string addr)
        {
            //64*2^24 + 233*2^16 + 187*2^8 + 99= 1089059683
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (UInt32)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(addr).Address);
        }
        public static string ToAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
            // This also works:
            // return new IPAddress((uint) IPAddress.HostToNetworkOrder(
            //    (int) address)).ToString();
        }
    }
}
