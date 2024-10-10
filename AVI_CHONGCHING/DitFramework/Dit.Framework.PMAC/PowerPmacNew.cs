using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace Dit.Framework.PMAC
{
    enum DTK_MODE_TYPE
    {
        DM_GPASCII = 0,
        DM_GETSENDS_0 = 1,
        DM_GETSENDS_1 = 2,
        DM_GETSENDS_2 = 3,
        DM_GETSENDS_3 = 4,
        DM_GETSENDS_4 = 5,
        DM_SECURE_SHELL = 10
    };

    enum DTK_STATUS
    {
        DS_Ok = 0,
        DS_Exception = 1,
        DS_TimeOut = 2,
        DS_Connected = 3,
        DS_NotConnected = 4,
        DS_Failed = 5,
        DS_InvalidDevice = 11,
        DS_DataRemains = 21,
        DS_CmdLengthExceeds = 23,
        DS_ResLengthExceeds = 24,
        DS_RunningDownload = 41,
        DS_RunningRead = 42,
        DS_DATimeOut = 102,
        DS_DANotConnected = 104,
        DS_DAFailed = 105,
    };

    enum DTK_RESET_TYPE
    {
        DR_Reset = 0,
        DR_FullReset = 1
    };

    enum DTK_DOWNLOAD_TYPE
    {
        DT_Progress = 0,
        DT_StringA = 1,
        DT_StringW = 2
    };

    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public UInt32 cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    public class PowerPmacNew
    {
        //const string dllName = "PowerPmac32.dll";   // 32Bit 플랫폼일 때 활성화
        const string dllName = "PowerPmac64.dll";   // 64Bit 플랫폼일 때 활성화

        public delegate void PDOWNLOAD_PROGRESS(Int32 nPercent);
        public delegate void PDOWNLOAD_MESSAGE_A(String lpMessage);
        public delegate void PRECEIVE_PROC_A(String lpReveive);

        public const UInt32 WM_MESSAGE_DOWNLOAD = 0x1216;

        // 라이브러리 오픈
        // 인자를 NULL로 할 경우 DTKDeviceSelect 함수를 사용하여 장치를 연결해야 한다.
        [DllImport(dllName)]
        public static extern UInt32 DTKPowerPmacOpen(UInt32 dwIPAddress, UInt32 uMode);

        // 라이브리리 클로즈
        [DllImport(dllName)]
        public static extern UInt32 DTKPowerPmacClose(UInt32 uDeviceID);

        // 등록된 디바이스 갯수
        [DllImport(dllName)]
        public static extern UInt32 DTKGetDeviceCount(out Int32 pnDeviceCount);

        // IP Address 확인
        [DllImport(dllName)]
        public static extern UInt32 DTKGetIPAddress(UInt32 uDeviceID, out UInt32 pdwIPAddress);

        // 장치를 연결
        [DllImport(dllName)]
        public static extern UInt32 DTKConnect(UInt32 uDeviceID);

        // 장치를 해제
        [DllImport(dllName)]
        public static extern UInt32 DTKDisconnect(UInt32 uDeviceID);

        // 장치가 연결되었는지 확인
        [DllImport(dllName)]
        public static extern UInt32 DTKIsConnected(UInt32 uDeviceID, out Int32 pbConnected);

        // Echo Mode 설정
        [DllImport(dllName)]
        public static extern UInt32 DTKSetEchoMode(UInt32 uDeviceID, UInt32 uEchoMode);

        // Echo Mode 확인
        [DllImport(dllName)]
        public static extern UInt32 DTKGetEchoMode(UInt32 uDeviceID, out UInt32 puEchoMode);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetResponseA(UInt32 uDeviceID, Byte[] lpCommand, Byte[] lpResponse, Int32 nLength);

        [DllImport(dllName)]
        public static extern UInt32 DTKSendCommandA(UInt32 uDeviceID, Byte[] lpCommand);

        [DllImport(dllName)]
        public static extern UInt32 DTKAbort(UInt32 uDeviceID);
        [DllImport(dllName)]
        public static extern UInt32 DTKDownloadA(UInt32 uDeviceID, Byte[] lpDownload, Int32 bDowoload, IntPtr hDownloadWnd, PDOWNLOAD_PROGRESS lpDownloadProgress, PDOWNLOAD_MESSAGE_A lpDownloadMessage);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetReceiveA(UInt32 uDeviceID, IntPtr hReceiveWnd, PRECEIVE_PROC_A lpReveiveProc);

        // 아래의 함수군은 CPU 통신셋업후 사용 가능
        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMem(UInt32 uDeviceID, UInt32 uAddress, Int32 nSize, SByte[] pValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMem(UInt32 uDeviceID, UInt32 uAddress, Int32 nSize, SByte[] pValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMemChar(UInt32 uDeviceID, UInt32 uAddress, out Byte pchValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMemChar(UInt32 uDeviceID, UInt32 uAddress, Byte chValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMemShort(UInt32 uDeviceID, UInt32 uAddress, out short pnValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMemShort(UInt32 uDeviceID, UInt32 uAddress, short nValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMemInteger(UInt32 uDeviceID, UInt32 uAddress, out Int32 pnValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMemInteger(UInt32 uDeviceID, UInt32 uAddress, Int32 nValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMemFloat(UInt32 uDeviceID, UInt32 uAddress, out float pfVlaue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMemFloat(UInt32 uDeviceID, UInt32 uAddress, float fValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKGetUserMemDouble(UInt32 uDeviceID, UInt32 uAddress, out Double pdValue);

        [DllImport(dllName)]
        public static extern UInt32 DTKSetUserMemDouble(UInt32 uDeviceID, UInt32 uAddress, Double dValue);

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