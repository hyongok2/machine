using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dit.Framework.Modbus
{
    public class ModbusCom : Component
    {
        private int _ConnectTimeout;
        private Modbus_Mode _Mode;
        private int _ResponseTimeout;
        private TcpClient client;
        private IContainer components;
        private string Error;
        private CModbus Modbus;
        private Modbus_Result Res;
        private CTxRx TxRx;

        public ModbusCom()
        {
            this._ResponseTimeout = 0x3e8;
            this._ConnectTimeout = 0x3e8;
            this.Error = "";
            this.InitializeComponent();
            this.Modbus = new CModbus(this.TxRx = new CTxRx());
            this.TxRx.Mode = Modbus_Mode.TCP_IP;
        }
        public ModbusCom(IContainer container)
        {
            this._ResponseTimeout = 0x3e8;
            this._ConnectTimeout = 0x3e8;
            this.Error = "";
            container.Add(this);
            this.InitializeComponent();
            this.Modbus = new CModbus(this.TxRx = new CTxRx());
            this.TxRx.Mode = Modbus_Mode.TCP_IP;
        }

        public void Close()
        {
            if (this.client != null)
            {
                this.client.Close();
            }
        }

        public Modbus_Result Connect(string IPAddress, int port)
        {
            this.client = new TcpClient();
            this.TxRx.SetClient(this.client);
            this.TxRx.Timeout = this._ResponseTimeout;
            this.client.SendTimeout = 0x7d0;
            this.client.ReceiveTimeout = this._ResponseTimeout;
            try
            {
                IAsyncResult asyncResult = this.client.BeginConnect(IPAddress, port, null, null);
                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds((double) this._ConnectTimeout), false))
                {
                    this.client.Close();
                    return (this.Res = Modbus_Result.CONNECT_TIMEOUT);
                }
                this.client.EndConnect(asyncResult);
                asyncWaitHandle.Close();
            }
            catch (Exception exception)
            {
                this.Error = exception.Message;
                return (this.Res = Modbus_Result.CONNECT_ERROR);
            }
            this.TxRx.Mode = this._Mode;
            return (this.Res = Modbus_Result.SUCCESS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public short[] FloatToRegisters(float value)
        {
            short[] numArray = new short[2];
            byte[] bytes = BitConverter.GetBytes(value);
            numArray[1] = BitConverter.ToInt16(bytes, 0);
            numArray[0] = BitConverter.ToInt16(bytes, 2);
            return numArray;
        }

        public string GetLastErrorString()
        {
            switch (this.Res)
            {
                case Modbus_Result.RESPONSE_TIMEOUT:
                    return "Response timeout.";

                case Modbus_Result.ISCLOSED:
                    return "Connection is closed.";

                case Modbus_Result.CRC:
                    return "CRC Error.";

                case Modbus_Result.RESPONSE:
                    return "Not the expected response received.";

                case Modbus_Result.BYTECOUNT:
                    return "Byte count error.";

                case Modbus_Result.QUANTITY:
                    return "Quantity is out of range.";

                case Modbus_Result.FUNCTION:
                    return "Modbus function code out of range. 1 - 127.";

                case Modbus_Result.DEMO_TIMEOUT:
                    return "Demo mode expired. Restart your application to continue.";

                case Modbus_Result.SUCCESS:
                    return "Success";

                case Modbus_Result.ILLEGAL_FUNCTION:
                    return "Illegal function.";

                case Modbus_Result.ILLEGAL_DATA_ADDRESS:
                    return "Illegal data address.";

                case Modbus_Result.ILLEGAL_DATA_VALUE:
                    return "Illegal data value.";

                case Modbus_Result.SLAVE_DEVICE_FAILURE:
                    return "Slave device failure.";

                case Modbus_Result.ACKNOWLEDGE:
                    return "Acknowledge.";

                case Modbus_Result.SLAVE_DEVICE_BUSY:
                    return "Slave device busy.";

                case Modbus_Result.NEGATIVE_ACKNOWLEDGE:
                    return "Negative acknowledge.";

                case Modbus_Result.MEMORY_PARITY_ERROR:
                    return "Memory parity error.";

                case Modbus_Result.CONNECT_ERROR:
                    return this.Error;

                case Modbus_Result.CONNECT_TIMEOUT:
                    return "Could not connect within the specified time";

                case Modbus_Result.WRITE:
                    return ("Write error. " + this.TxRx.GetErrorMessage());

                case Modbus_Result.READ:
                    return ("Read error. " + this.TxRx.GetErrorMessage());
            }
            return ("Unknown Error - " + this.Res.ToString());
        }

        public int GetRxBuffer(byte[] byteArray)
        {
            return this.TxRx.GetRxBuffer(byteArray);
        }

        public int GetTxBuffer(byte[] byteArray)
        {
            return this.TxRx.GetTxBuffer(byteArray);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public short[] Int32ToRegisters(int value)
        {
            short[] numArray = new short[2];
            byte[] bytes = BitConverter.GetBytes(value);
            numArray[1] = BitConverter.ToInt16(bytes, 0);
            numArray[0] = BitConverter.ToInt16(bytes, 2);
            return numArray;
        }

       
        public Modbus_Result MaskWriteRegister(byte unitId, ushort address, ushort andMask, ushort orMask)
        {
            return (this.Res = this.Modbus.MaskWriteRegister(unitId, address, andMask, orMask));
        }

        public Modbus_Result ReadCoils(byte unitId, ushort address, ushort quantity, bool[] coils)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, 1, address, quantity, coils, 0));
        }

        public Modbus_Result ReadCoils(byte unitId, ushort address, ushort quantity, bool[] coils, int offset)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, 1, address, quantity, coils, offset));
        }

        public Modbus_Result ReadDiscreteInputs(byte unitId, ushort address, ushort quantity, bool[] discreteInputs)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, 2, address, quantity, discreteInputs, 0));
        }

        public Modbus_Result ReadDiscreteInputs(byte unitId, ushort address, ushort quantity, bool[] discreteInputs, int offset)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, 2, address, quantity, discreteInputs, offset));
        }

        public Modbus_Result ReadHoldingRegisters(byte unitId, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, 3, address, quantity, registers, 0));
        }

        public Modbus_Result ReadHoldingRegisters(byte unitId, ushort address, ushort quantity, short[] registers, int offset)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, 3, address, quantity, registers, offset));
        }

        public Modbus_Result ReadInputRegisters(byte unitId, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, 4, address, quantity, registers, 0));
        }

        public Modbus_Result ReadInputRegisters(byte unitId, ushort address, ushort quantity, short[] registers, int offset)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, 4, address, quantity, registers, offset));
        }

        public Modbus_Result ReadUserDefinedCoils(byte unitId, byte function, ushort address, ushort quantity, bool[] coils)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, function, address, quantity, coils, 0));
        }

        public Modbus_Result ReadUserDefinedCoils(byte unitId, byte function, ushort address, ushort quantity, bool[] coils, int offset)
        {
            return (this.Res = this.Modbus.ReadFlags(unitId, function, address, quantity, coils, offset));
        }

        public Modbus_Result ReadUserDefinedRegisters(byte unitId, byte function, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, function, address, quantity, registers, 0));
        }

        public Modbus_Result ReadUserDefinedRegisters(byte unitId, byte function, ushort address, ushort quantity, short[] registers, int offset)
        {
            return (this.Res = this.Modbus.ReadRegisters(unitId, function, address, quantity, registers, offset));
        }

        public Modbus_Result ReadWriteMultipleRegisters(byte unitId, ushort readAddress, ushort readQuantity, short[] readRegisters, ushort writeAddress, ushort writeQuantity, short[] writeRegisters)
        {
            return (this.Res = this.Modbus.ReadWriteMultipleRegisters(unitId, readAddress, readQuantity, readRegisters, writeAddress, writeQuantity, writeRegisters));
        }

        public float RegistersToFloat(short hiReg, short loReg)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(loReg).Concat<byte>(BitConverter.GetBytes(hiReg)).ToArray<byte>(), 0);
        }

        public int RegistersToInt32(short hiReg, short loReg)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(loReg).Concat<byte>(BitConverter.GetBytes(hiReg)).ToArray<byte>(), 0);
        }

        public Modbus_Result ReportSlaveID(byte unitId, out byte byteCount, byte[] deviceSpecific)
        {
            return (this.Res = this.Modbus.ReportSlaveID(unitId, out byteCount, deviceSpecific));
        }

        public Modbus_Result WriteMultipleCoils(byte unitId, ushort address, ushort quantity, bool[] coils)
        {
            return (this.Res = this.Modbus.WriteFlags(unitId, 15, address, quantity, coils, 0));
        }

        public Modbus_Result WriteMultipleCoils(byte unitId, ushort address, ushort quantity, bool[] coils, int offset)
        {
            return (this.Res = this.Modbus.WriteFlags(unitId, 15, address, quantity, coils, offset));
        }

        public Modbus_Result WriteMultipleRegisters(byte unitId, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.WriteRegisters(unitId, 0x10, address, quantity, registers, 0));
        }

        public Modbus_Result WriteMultipleRegisters(byte unitId, ushort address, ushort quantity, short[] registers, int offset)
        {
            return (this.Res = this.Modbus.WriteRegisters(unitId, 0x10, address, quantity, registers, offset));
        }

        public Modbus_Result WriteSingleCoil(byte unitId, ushort address, bool coil)
        {
            return (this.Res = this.Modbus.WriteSingleCoil(unitId, address, coil));
        }

        public Modbus_Result WriteSingleRegister(byte unitId, ushort address, short register)
        {
            return (this.Res = this.Modbus.WriteSingleRegister(unitId, address, register));
        }

        public Modbus_Result WriteUserDefinedCoils(byte unitId, byte function, ushort address, ushort quantity, bool[] coils)
        {
            return (this.Res = this.Modbus.WriteFlags(unitId, function, address, quantity, coils, 0));
        }

        public Modbus_Result WriteUserDefinedCoils(byte unitId, byte function, ushort address, ushort quantity, bool[] coils, int offset)
        {
            return (this.Res = this.Modbus.WriteFlags(unitId, function, address, quantity, coils, offset));
        }

        public Modbus_Result WriteUserDefinedRegisters(byte unitId, byte function, ushort address, ushort quantity, short[] registers)
        {
            return (this.Res = this.Modbus.WriteRegisters(unitId, function, address, quantity, registers, 0));
        }

        public Modbus_Result WriteUserDefinedRegisters(byte unitId, byte function, ushort address, ushort quantity, short[] registers, int offset)
        {
            return (this.Res = this.Modbus.WriteRegisters(unitId, function, address, quantity, registers, offset));
        }

        [Category("Modbus"), Description("Max time to wait for connection 100 - 30000ms.")]
        public int ConnectTimeout
        {
            get
            {
                return this._ConnectTimeout;
            }
            set
            {
                if ((value >= 100) && (value <= 0x7530))
                {
                    this._ConnectTimeout = value;
                }
            }
        }

        [Category("Modbus"), Description("Select which protocol mode to use.")]
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

        [Description("Max time to wait for response 100 - 30000ms."), Category("Modbus")]
        public int ResponseTimeout
        {
            get
            {
                return this._ResponseTimeout;
            }
            set
            {
                if ((value >= 100) && (value <= 0x7530))
                {
                    this._ResponseTimeout = value;
                }
            }
        }
    }
}

