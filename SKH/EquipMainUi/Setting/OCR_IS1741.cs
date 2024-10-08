using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    /// <summary>
    /// Congex 사 OCR Insight 1741 제품
    /// TCP/IP 사용
    /// 
    /// since 19.06.09
    /// </summary>
    public class OCR_IS1741
    {
        protected TcpClient _comm = null;
        protected bool _isGetting = false;
        public bool IsReadComplete { get { return !_isGetting; } }

        protected string _readValue;
        public string Readvalue { get { return _readValue; } }
        private Regex _readValVerify = new Regex("^*$");
        public bool IsReadValueError
        {
            get
            {
                return _readValue == null
                    || _readValue == string.Empty
                    || _readValue.Length == 0                     
                    || _readValVerify.IsMatch(_readValue) == false
                    ;
            }
        }
        public bool IsConnected {  get { return _comm.Connected; } }

        public delegate void ReadCompeleteEvent(OCR_IS1741 sender);
        public event ReadCompeleteEvent ReadCompleted;       

        private AsyncCallback ReadCallBack;
        private OCRIS1741AsyncObject ReadAsync;

        public OCR_IS1741()
        {
            _comm = new TcpClient();
            ReadCallBack = new AsyncCallback(ReadCallBackFunc);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqularExcpress"></param>
        /// <returns>정규식 생성 실패 시 false</returns>
        public bool SetReadValueRegex(string reqularExcpress)
        {
            try
            {
                _readValVerify = new Regex(reqularExcpress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Open(string ip, int port)
        {
            try
            {
                IPAddress addr;
                _comm = new TcpClient();
                if (_comm.Connected == false)
                {
                    if (IPAddress.TryParse(ip, out addr) == true)
                    {
                        _comm.Connect(addr, port);

                        if (_comm.Connected == true)
                        {
                            ReadAsync = new OCRIS1741AsyncObject(128);
                            ReadAsync.Comm = _comm;
                            return true;
                        }
                    }
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
        public bool Close()
        {
            if (_comm.Connected == true)
            {
                _comm.Close();
                _comm = null;
                return true;
            }
            else
                return true;
        }

        private void ReadCallBackFunc(IAsyncResult ar)
        {
            OCRIS1741AsyncObject t = ar.AsyncState as OCRIS1741AsyncObject;
            int recvBytes;

            try
            {
                recvBytes = t.Comm.GetStream().EndRead(ar);

            }
            catch (Exception ex)
            {
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (recvBytes > 2)
            {
                byte[] msgByte = new byte[recvBytes - 2];
                Array.Copy(t.Buffer, msgByte, recvBytes - 2);
                _readValue = Encoding.UTF8.GetString(msgByte);
                _isGetting = false;
                if (ReadCompleted != null)
                    ReadCompleted(this);
            }
        }
        public bool Read()
        {
            if (IsConnected == false)
                return false;

            if (_comm.GetStream().CanWrite)
            {
                string read = "READ(-1)\r\n";
                byte[] cmdByte = Encoding.UTF8.GetBytes(read);

                _readValue = string.Empty;
                _isGetting = true;
                _comm.GetStream().WriteAsync(cmdByte, 0, cmdByte.Length); // .NET 4.5이상 지원
                _comm.GetStream().BeginRead(ReadAsync.Buffer, 0, ReadAsync.Buffer.Length, ReadCallBack, ReadAsync);
                return true;
            }
            return false;
        }
    }

    public class OCRIS1741AsyncObject
    {
        public byte[] Buffer;
        public TcpClient Comm;

        public OCRIS1741AsyncObject(int bufLen)
        {
            Buffer = new byte[bufLen];
        }
    }
}
