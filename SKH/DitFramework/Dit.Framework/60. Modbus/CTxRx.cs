using System;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Modbus
{ 
    internal class CTxRx
    {
        private bool _License;
        private Modbus_Mode _Mode;
        private byte[] _RxBuf = new byte[600];
        private int _RxBufSize;
        private int _Timeout = 0x3e8;
        private byte[] _TxBuf = new byte[600];
        private int _TxBufSize;
        private TcpClient client;
        private string Error = "";
        private bool firstTime = true;
        private int Time;
        private int timeStamp;
        private ushort TransactionID;

        private void DiscardReadBuffer()
        {
            byte[] buffer = new byte[100];
            if (this.client.GetStream().CanRead)
            {
                while (this.client.GetStream().DataAvailable)
                {
                    this.client.GetStream().Read(buffer, 0, 100);
                }
            }
        }

        public string GetErrorMessage()
        {
            return this.Error;
        }

        public int GetRxBuffer(byte[] byteArray)
        {
            if (byteArray.GetLength(0) >= this._RxBufSize)
            {
                Array.Copy(this._RxBuf, byteArray, this._RxBufSize);
                return this._RxBufSize;
            }
            return 0;
        }

        public int GetTxBuffer(byte[] byteArray)
        {
            if (byteArray.GetLength(0) >= this._TxBufSize)
            {
                Array.Copy(this._TxBuf, byteArray, this._TxBufSize);
                return this._TxBufSize;
            }
            return 0;
        }

        private bool ResponseTimeout()
        {
            int num = Environment.TickCount & 0x7fffffff;
            int millisecondsTimeout = this._Timeout / 100;
            if (millisecondsTimeout == 0)
            {
                millisecondsTimeout = 1;
            }
            while (!this.client.GetStream().DataAvailable)
            {
                int num3 = Environment.TickCount & 0x7fffffff;
                if (Math.Abs((int) (num3 - num)) > this._Timeout)
                {
                    return true;
                }
                Thread.Sleep(millisecondsTimeout);
            }
            return false;
        }

        public void SetClient(TcpClient _client)
        {
            this.client = _client;
        }

        public Modbus_Result TxRx(byte[] TXBuf, int QueryLength, byte[] RXBuf, int ResponseLength)
        {
            Modbus_Result sUCCESS;
            int num = Environment.TickCount & 0x7fffffff;
            this._TxBufSize = 0;
            this._RxBufSize = 0;
            if ((num - this.Time) < 20)
            {
                Thread.Sleep(4);
            }
            if (this.client == null)
            {
                return Modbus_Result.ISCLOSED;
            }
            try
            {
                if (!this.client.Connected)
                {
                    return Modbus_Result.ISCLOSED;
                }
            }
            catch (Exception exception)
            {
                this.Error = exception.Message;
                return Modbus_Result.ISCLOSED;
            }
            switch (this._Mode)
            {
                case Modbus_Mode.TCP_IP:
                    if (ResponseLength == 0x7fffffff)
                    {
                        return Modbus_Result.ILLEGAL_FUNCTION;
                    }
                    sUCCESS = this.TxRxTCP(TXBuf, QueryLength, RXBuf, ResponseLength);
                    break;

                case Modbus_Mode.RTU_OVER_TCP_IP:
                    sUCCESS = this.TxRxRTUOverTCP(TXBuf, QueryLength, RXBuf, ResponseLength);
                    break;

                case Modbus_Mode.ASCII_OVER_TCP_IP:
                    sUCCESS = this.TxRxASCIIOverTCP(TXBuf, QueryLength, RXBuf, ResponseLength);
                    break;

                default:
                    sUCCESS = Modbus_Result.SUCCESS;
                    break;
            }
            this.Time = Environment.TickCount;
            return sUCCESS;
        }

        private Modbus_Result TxRxASCIIOverTCP(byte[] TXBuf, int QueryLength, byte[] RXBuf, int ResponseLength)
        {
            int offset = 0;
            byte[] nAscii = new byte[0x213];
            byte[] buffer = new byte[0x20b];
            CAscii.RTU2ASCII(TXBuf, QueryLength, nAscii);
            byte n = CAscii.LRC(TXBuf, QueryLength);
            nAscii[0] = 0x3a;
            nAscii[(QueryLength * 2) + 1] = CAscii.Num2Ascii(ByteAccess.HI4BITS(n));
            nAscii[(QueryLength * 2) + 2] = CAscii.Num2Ascii(ByteAccess.LO4BITS(n));
            nAscii[(QueryLength * 2) + 3] = 13;
            nAscii[(QueryLength * 2) + 4] = 10;
            this.DiscardReadBuffer();
            if (this.client.GetStream().CanWrite)
            {
                try
                {
                    this.client.GetStream().Write(nAscii, 0, (QueryLength * 2) + 5);
                    this._TxBufSize = (QueryLength * 2) + 5;
                    Array.Copy(nAscii, this._TxBuf, this._TxBufSize);
                    if (TXBuf[0] != 0)
                    {
                        if (this.client.GetStream().CanRead)
                        {
                            try
                            {
                                do
                                {
                                    if (this.ResponseTimeout())
                                    {
                                        return Modbus_Result.RESPONSE_TIMEOUT;
                                    }
                                    int num3 = this.client.GetStream().Read(buffer, offset, 11 - offset);
                                    offset += num3;
                                    if (offset == 0)
                                    {
                                        return Modbus_Result.RESPONSE_TIMEOUT;
                                    }
                                }
                                while ((11 - offset) > 0);
                            }
                            catch (Exception exception)
                            {
                                this.Error = exception.Message;
                                return Modbus_Result.READ;
                            }
                            finally
                            {
                                this._RxBufSize = offset;
                                Array.Copy(buffer, this._RxBuf, this._RxBufSize);
                            }
                            if (CAscii.HiLo4BitsToByte(CAscii.Ascii2Num(buffer[3]), CAscii.Ascii2Num(buffer[4])) > 0x80)
                            {
                                if (!CAscii.VerifyRespLRC(buffer, 11))
                                {
                                    return Modbus_Result.CRC;
                                }
                                return (Modbus_Result) CAscii.HiLo4BitsToByte(CAscii.Ascii2Num(buffer[5]), CAscii.Ascii2Num(buffer[6]));
                            }
                            if (ResponseLength == 0x7fffffff)
                            {
                                if (CAscii.HiLo4BitsToByte(CAscii.Ascii2Num(buffer[3]), CAscii.Ascii2Num(buffer[4])) != 0x11)
                                {
                                    this.DiscardReadBuffer();
                                    return Modbus_Result.RESPONSE;
                                }
                                ResponseLength = CAscii.HiLo4BitsToByte(CAscii.Ascii2Num(buffer[5]), CAscii.Ascii2Num(buffer[6])) + 3;
                            }
                            try
                            {
                                int num4 = (ResponseLength * 2) + 5;
                                do
                                {
                                    if (this.ResponseTimeout())
                                    {
                                        return Modbus_Result.RESPONSE_TIMEOUT;
                                    }
                                    int num5 = this.client.GetStream().Read(buffer, offset, num4 - offset);
                                    offset += num5;
                                }
                                while ((num4 - offset) > 0);
                            }
                            catch (Exception exception2)
                            {
                                this.Error = exception2.Message;
                                return Modbus_Result.READ;
                            }
                            finally
                            {
                                this._RxBufSize = offset;
                                Array.Copy(buffer, this._RxBuf, this._RxBufSize);
                            }
                        }
                        else
                        {
                            this.Error = "You cannot read from this NetworkStream.";
                            return Modbus_Result.READ;
                        }
                        if (!CAscii.VerifyRespLRC(buffer, offset))
                        {
                            return Modbus_Result.CRC;
                        }
                        if ((buffer[offset - 2] != 13) || (buffer[offset - 1] != 10))
                        {
                            return Modbus_Result.RESPONSE;
                        }
                        int num6 = (offset - 5) / 2;
                        for (int i = 0; i < num6; i++)
                        {
                            RXBuf[i] = CAscii.HiLo4BitsToByte(CAscii.Ascii2Num(buffer[1 + (i * 2)]), CAscii.Ascii2Num(buffer[2 + (i * 2)]));
                        }
                    }
                    return Modbus_Result.SUCCESS;
                }
                catch (Exception exception3)
                {
                    this.Error = exception3.Message;
                    return Modbus_Result.WRITE;
                }
            }
            this.Error = "You cannot write to this NetworkStream.";
            return Modbus_Result.WRITE;
        }

        private Modbus_Result TxRxRTUOverTCP(byte[] TXBuf, int QueryLength, byte[] RXBuf, int ResponseLength)
        {
            int offset = 0;
            byte[] buffer = CCRC16.CRC16(TXBuf, QueryLength);
            TXBuf[QueryLength] = buffer[0];
            TXBuf[QueryLength + 1] = buffer[1];
            this.DiscardReadBuffer();
            if (this.client.GetStream().CanWrite)
            {
                try
                {
                    this.client.GetStream().Write(TXBuf, 0, QueryLength + 2);
                    this._TxBufSize = QueryLength + 2;
                    Array.Copy(TXBuf, this._TxBuf, this._TxBufSize);
                    if (TXBuf[0] == 0)
                    {
                        return Modbus_Result.SUCCESS;
                    }
                    if (this.client.GetStream().CanRead)
                    {
                        try
                        {
                            do
                            {
                                if (this.ResponseTimeout())
                                {
                                    return Modbus_Result.RESPONSE_TIMEOUT;
                                }
                                int num2 = this.client.GetStream().Read(RXBuf, offset, 5 - offset);
                                offset += num2;
                                if (offset == 0)
                                {
                                    return Modbus_Result.RESPONSE_TIMEOUT;
                                }
                            }
                            while ((5 - offset) > 0);
                        }
                        catch (Exception exception)
                        {
                            this.Error = exception.Message;
                            return Modbus_Result.READ;
                        }
                        finally
                        {
                            this._RxBufSize = offset;
                            Array.Copy(RXBuf, this._RxBuf, this._RxBufSize);
                        }
                        if (RXBuf[1] > 0x80)
                        {
                            byte[] buffer2 = CCRC16.CRC16(RXBuf, 5);
                            if ((buffer2[0] == 0) && (buffer2[1] == 0))
                            {
                                return (Modbus_Result) RXBuf[2];
                            }
                            return Modbus_Result.CRC;
                        }
                        if (ResponseLength == 0x7fffffff)
                        {
                            if (RXBuf[1] != 0x11)
                            {
                                this.DiscardReadBuffer();
                                return Modbus_Result.RESPONSE;
                            }
                            ResponseLength = RXBuf[2] + 3;
                        }
                        try
                        {
                            int num3 = ResponseLength + 2;
                            do
                            {
                                if (this.ResponseTimeout())
                                {
                                    return Modbus_Result.RESPONSE_TIMEOUT;
                                }
                                int num4 = this.client.GetStream().Read(RXBuf, offset, num3 - offset);
                                offset += num4;
                            }
                            while ((num3 - offset) > 0);
                        }
                        catch (Exception exception2)
                        {
                            this.Error = exception2.Message;
                            return Modbus_Result.READ;
                        }
                        finally
                        {
                            this._RxBufSize = offset;
                            Array.Copy(RXBuf, this._RxBuf, this._RxBufSize);
                        }
                    }
                    else
                    {
                        this.Error = "You cannot read from this NetworkStream.";
                        return Modbus_Result.READ;
                    }
                    byte[] buffer3 = CCRC16.CRC16(RXBuf, ResponseLength + 2);
                    if ((buffer3[0] == 0) && (buffer3[1] == 0))
                    {
                        return Modbus_Result.SUCCESS;
                    }
                    return Modbus_Result.CRC;
                }
                catch (Exception exception3)
                {
                    this.Error = exception3.Message;
                    return Modbus_Result.WRITE;
                }
            }
            this.Error = "You cannot write to this NetworkStream.";
            return Modbus_Result.WRITE;
        }

        private Modbus_Result TxRxTCP(byte[] TXBuf, int QueryLength, byte[] RXBuf, int ResponseLength)
        {
            int offset = 0;
            this.TransactionID = (ushort) (this.TransactionID + 1);
            for (int i = 0; i < 5; i++)
            {
                this._TxBuf[i] = 0;
            }
            this._TxBuf[0] = (byte) (this.TransactionID >> 8);
            this._TxBuf[1] = (byte) (this.TransactionID & 0xff);
            this._TxBuf[4] = (byte) (QueryLength >> 8);
            this._TxBuf[5] = (byte) (QueryLength & 0xff);
            for (int j = 0; j < QueryLength; j++)
            {
                this._TxBuf[6 + j] = TXBuf[j];
            }
            if (this.client.GetStream().CanWrite)
            {
                try
                {
                    this.client.GetStream().Write(this._TxBuf, 0, QueryLength + 6);
                    this._TxBufSize = QueryLength + 6;
                    if (this.client.GetStream().CanRead)
                    {
                        try
                        {
                            try
                            {
                                do
                                {
                                    if (this.ResponseTimeout())
                                    {
                                        return Modbus_Result.RESPONSE_TIMEOUT;
                                    }
                                    offset += this.client.GetStream().Read(this._RxBuf, offset, (ResponseLength + 6) - offset);
                                    if (offset == 0)
                                    {
                                        return Modbus_Result.RESPONSE_TIMEOUT;
                                    }
                                    if ((offset == 9) && (this._TxBuf[7] > 0x80))
                                    {
                                        this._RxBufSize = offset;
                                        return (Modbus_Result) this._TxBuf[8];
                                    }
                                }
                                while (offset < (ResponseLength + 6));
                                if (this.TransactionID != ByteAccess.MakeWord(this._RxBuf[0], this._RxBuf[1]))
                                {
                                    this.DiscardReadBuffer();
                                    this._RxBufSize = offset;
                                    return Modbus_Result.TRANSACTIONID;
                                }
                            }
                            catch (Exception exception)
                            {
                                this.Error = exception.Message;
                                return Modbus_Result.READ;
                            }
                            if ((offset - 6) < ResponseLength)
                            {
                                return Modbus_Result.RESPONSE_TIMEOUT;
                            }
                            for (int k = 0; k < (Math.Min(offset, ResponseLength + 6) - 6); k++)
                            {
                                RXBuf[k] = this._RxBuf[k + 6];
                            }
                            return Modbus_Result.SUCCESS;
                        }
                        finally
                        {
                            this._RxBufSize = offset;
                        }
                    }
                    this.Error = "You cannot read from this NetworkStream.";
                    return Modbus_Result.READ;
                }
                catch (Exception exception2)
                {
                    this.Error = exception2.Message;
                    return Modbus_Result.WRITE;
                }
            }
            this.Error = "You cannot write to this NetworkStream.";
            return Modbus_Result.WRITE;
        }

        public bool License
        {
            get
            {
                return this._License;
            }
            set
            {
                this._License = value;
            }
        }

        public Modbus_Mode Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                this._Mode = value;
            }
        }

        public int Timeout
        {
            get
            {
                return this._Timeout;
            }
            set
            {
                this._Timeout = value;
            }
        }
    }
}

