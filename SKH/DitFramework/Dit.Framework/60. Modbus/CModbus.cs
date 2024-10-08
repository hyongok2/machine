using System;
using System.Runtime.InteropServices;

namespace Dit.Framework.Modbus
{
    internal class CModbus
    {
        private CTxRx TxRx;

        public CModbus(CTxRx Tx)
        {
            this.TxRx = Tx;
        }

        public Modbus_Result MaskWriteRegister(byte unitId, ushort address, ushort ANDMask, ushort ORMask)
        {
            byte[] tXBuf = new byte[10];
            byte[] rXBuf = new byte[10];
            tXBuf[0] = unitId;
            tXBuf[1] = 0x16;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(ANDMask);
            tXBuf[5] = ByteAccess.LoByte(ANDMask);
            tXBuf[6] = ByteAccess.HiByte(ORMask);
            tXBuf[7] = ByteAccess.LoByte(ORMask);
            Modbus_Result rESPONSE = this.TxRx.TxRx(tXBuf, 8, rXBuf, 8);
            if ((rESPONSE == Modbus_Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int i = 0; i < 8; i++)
                {
                    if (tXBuf[i] != rXBuf[i])
                    {
                        rESPONSE = Modbus_Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }

        public Modbus_Result ReadFlags(byte unitId, byte function, ushort address, ushort quantity, bool[] Bools, int offset)
        {
            ushort num = 0;
            ushort num2 = 0;
            if ((function < 1) || (function > 0x7f))
            {
                return Modbus_Result.FUNCTION;
            }
            if ((quantity < 1) || (quantity > 0x7d0))
            {
                return Modbus_Result.QUANTITY;
            }
            if ((quantity + offset) > Bools.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[0x105];
            tXBuf[0] = unitId;
            tXBuf[1] = function;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(quantity);
            tXBuf[5] = ByteAccess.LoByte(quantity);
            int responseLength = ((quantity + 7) / 8) + 3;
            Modbus_Result result = this.TxRx.TxRx(tXBuf, 6, rXBuf, responseLength);
            if (result == Modbus_Result.SUCCESS)
            {
                if ((tXBuf[0] != rXBuf[0]) || (tXBuf[1] != rXBuf[1]))
                {
                    return Modbus_Result.RESPONSE;
                }
                if ((responseLength - 3) != rXBuf[2])
                {
                    return Modbus_Result.BYTECOUNT;
                }
                int num4 = rXBuf[3];
                for (int i = 0; i < quantity; i++)
                {
                    Bools[i + offset] = (num4 & 1) == 1;
                    num4 = num4 >> 1;
                    num = (ushort) (num + 1);
                    if (num == 8)
                    {
                        num2 = (ushort) (num2 + 1);
                        num = 0;
                        num4 = rXBuf[3 + num2];
                    }
                }
            }
            return result;
        }

        public Modbus_Result ReadRegisters(byte unitId, ushort function, ushort address, ushort quantity, short[] registers, int offset)
        {
            if ((function < 1) || (function > 0x7f))
            {
                return Modbus_Result.FUNCTION;
            }
            if ((quantity < 1) || (quantity > 0x7d))
            {
                return Modbus_Result.QUANTITY;
            }
            if ((quantity + offset) > registers.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[0x105];
            tXBuf[0] = unitId;
            tXBuf[1] = (byte) (function & 0xff);
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(quantity);
            tXBuf[5] = ByteAccess.LoByte(quantity);
            int responseLength = 3 + (quantity * 2);
            Modbus_Result result = this.TxRx.TxRx(tXBuf, 6, rXBuf, responseLength);
            if (result == Modbus_Result.SUCCESS)
            {
                if ((tXBuf[0] != rXBuf[0]) || (tXBuf[1] != rXBuf[1]))
                {
                    return Modbus_Result.RESPONSE;
                }
                if ((quantity * 2) != rXBuf[2])
                {
                    return Modbus_Result.BYTECOUNT;
                }
                for (int i = 0; i < quantity; i++)
                {
                    registers[i + offset] = (short) ((rXBuf[(2 * i) + 4] & 0xff) | ((rXBuf[(2 * i) + 3] & 0xff) << 8));
                }
            }
            return result;
        }

        public Modbus_Result ReadWriteMultipleRegisters(byte unitId, ushort readAddress, ushort readSize, short[] readRegisters, ushort writeAddress, ushort writeSize, short[] writeRegisters)
        {
            if ((readSize < 1) || (readSize > 0x7b))
            {
                return Modbus_Result.QUANTITY;
            }
            if ((writeSize < 1) || (writeSize > 0x79))
            {
                return Modbus_Result.QUANTITY;
            }
            if (readSize > readRegisters.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            if (writeSize > writeRegisters.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            byte[] tXBuf = new byte[0x10d];
            byte[] rXBuf = new byte[0x105];
            tXBuf[0] = unitId;
            tXBuf[1] = 0x17;
            tXBuf[2] = ByteAccess.HiByte(readAddress);
            tXBuf[3] = ByteAccess.LoByte(readAddress);
            tXBuf[4] = ByteAccess.HiByte(readSize);
            tXBuf[5] = ByteAccess.LoByte(readSize);
            tXBuf[6] = ByteAccess.HiByte(writeAddress);
            tXBuf[7] = ByteAccess.LoByte(writeAddress);
            tXBuf[8] = ByteAccess.HiByte(writeSize);
            tXBuf[9] = ByteAccess.LoByte(writeSize);
            tXBuf[10] = (byte) (writeSize * 2);
            int responseLength = 3 + (readSize * 2);
            for (int i = 0; i < writeSize; i++)
            {
                tXBuf[11 + (i * 2)] = ByteAccess.HiByte((ushort) writeRegisters[i]);
                tXBuf[(11 + (i * 2)) + 1] = ByteAccess.LoByte((ushort) writeRegisters[i]);
            }
            Modbus_Result result = this.TxRx.TxRx(tXBuf, tXBuf[10] + 11, rXBuf, responseLength);
            if (result == Modbus_Result.SUCCESS)
            {
                if ((tXBuf[0] != rXBuf[0]) || (tXBuf[1] != rXBuf[1]))
                {
                    return Modbus_Result.RESPONSE;
                }
                for (int j = 0; j < readSize; j++)
                {
                    readRegisters[j] = (short) ByteAccess.MakeWord(rXBuf[(2 * j) + 4], rXBuf[(2 * j) + 3]);
                }
            }
            return result;
        }

        public Modbus_Result ReportSlaveID(byte unitId, out byte byteCount, byte[] deviceSpecific)
        {
            byte[] tXBuf = new byte[4];
            byte[] rXBuf = new byte[0xff];
            tXBuf[0] = unitId;
            tXBuf[1] = 0x11;
            byteCount = 0;
            Modbus_Result result = this.TxRx.TxRx(tXBuf, 2, rXBuf, 0x7fffffff);
            if (result == Modbus_Result.SUCCESS)
            {
                if (tXBuf[0] != rXBuf[0])
                {
                    return Modbus_Result.RESPONSE;
                }
                byteCount = rXBuf[2];
                int num = Math.Min(rXBuf[2], deviceSpecific.GetLength(0));
                for (int i = 0; i < num; i++)
                {
                    deviceSpecific[i] = rXBuf[i + 3];
                }
            }
            return result;
        }

        public Modbus_Result WriteFlags(byte unitId, byte function, ushort address, ushort quantity, bool[] Bools, int offset)
        {
            if ((function < 1) || (function > 0x7f))
            {
                return Modbus_Result.FUNCTION;
            }
            if ((quantity < 1) || (quantity > 0x7b0))
            {
                return Modbus_Result.QUANTITY;
            }
            if ((quantity + offset) > Bools.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            byte[] tXBuf = new byte[0x109];
            byte[] rXBuf = new byte[8];
            ushort num = 0;
            byte index = 7;
            byte num3 = 0;
            tXBuf[0] = unitId;
            tXBuf[1] = function;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(quantity);
            tXBuf[5] = ByteAccess.LoByte(quantity);
            tXBuf[6] = (byte) ((quantity + 7) / 8);
            for (int i = 0; i < quantity; i++)
            {
                if (Bools[i + offset])
                {
                    num3 = (byte) (num3 | ((byte) (((int) 1) << num)));
                }
                num = (ushort) (num + 1);
                if (num == 8)
                {
                    num = 0;
                    tXBuf[index] = num3;
                    index = (byte) (index + 1);
                    num3 = 0;
                }
            }
            tXBuf[index] = num3;
            Modbus_Result rESPONSE = this.TxRx.TxRx(tXBuf, tXBuf[6] + 7, rXBuf, 6);
            if ((rESPONSE == Modbus_Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int j = 0; j < 6; j++)
                {
                    if (tXBuf[j] != rXBuf[j])
                    {
                        rESPONSE = Modbus_Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }

        public Modbus_Result WriteRegisters(byte unitId, byte function, ushort address, ushort quantity, short[] registers, int offset)
        {
            if ((function < 1) || (function > 0x7f))
            {
                return Modbus_Result.FUNCTION;
            }
            if ((quantity < 1) || (quantity > 0x7b))
            {
                return Modbus_Result.QUANTITY;
            }
            if ((quantity + offset) > registers.GetLength(0))
            {
                return Modbus_Result.QUANTITY;
            }
            byte[] tXBuf = new byte[0x109];
            byte[] rXBuf = new byte[8];
            tXBuf[0] = unitId;
            tXBuf[1] = function;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte(quantity);
            tXBuf[5] = ByteAccess.LoByte(quantity);
            tXBuf[6] = (byte) (quantity * 2);
            for (int i = 0; i < quantity; i++)
            {
                tXBuf[7 + (i * 2)] = ByteAccess.HiByte((ushort) registers[i + offset]);
                tXBuf[(7 + (i * 2)) + 1] = ByteAccess.LoByte((ushort) registers[i + offset]);
            }
            Modbus_Result rESPONSE = this.TxRx.TxRx(tXBuf, tXBuf[6] + 7, rXBuf, 6);
            if ((rESPONSE == Modbus_Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int j = 0; j < 6; j++)
                {
                    if (tXBuf[j] != rXBuf[j])
                    {
                        rESPONSE = Modbus_Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }

        public Modbus_Result WriteSingleCoil(byte unitId, ushort address, bool coil)
        {
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[8];
            tXBuf[0] = unitId;
            tXBuf[1] = 5;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = coil ? ((byte) 0xff) : ((byte) 0);
            tXBuf[5] = 0;
            Modbus_Result rESPONSE = this.TxRx.TxRx(tXBuf, 6, rXBuf, 6);
            if ((rESPONSE == Modbus_Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (tXBuf[i] != rXBuf[i])
                    {
                        rESPONSE = Modbus_Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }

        public Modbus_Result WriteSingleRegister(byte unitId, ushort address, short register)
        {
            byte[] tXBuf = new byte[8];
            byte[] rXBuf = new byte[8];
            tXBuf[0] = unitId;
            tXBuf[1] = 6;
            tXBuf[2] = ByteAccess.HiByte(address);
            tXBuf[3] = ByteAccess.LoByte(address);
            tXBuf[4] = ByteAccess.HiByte((ushort) register);
            tXBuf[5] = ByteAccess.LoByte((ushort) register);
            Modbus_Result rESPONSE = this.TxRx.TxRx(tXBuf, 6, rXBuf, 6);
            if ((rESPONSE == Modbus_Result.SUCCESS) && (tXBuf[0] != 0))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (tXBuf[i] != rXBuf[i])
                    {
                        rESPONSE = Modbus_Result.RESPONSE;
                    }
                }
            }
            return rESPONSE;
        }
    }
}

