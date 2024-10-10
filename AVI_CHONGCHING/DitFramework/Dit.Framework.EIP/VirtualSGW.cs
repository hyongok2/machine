using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;
using OMRON.Compolet.CIPCompolet64.Variable;
using System.Threading;
using Dit.Framework.PLC;


namespace Dit.Framework.EIP
{
    public enum EipInOut
    {
        IN,
        OUT
    }

    public class StructEip
    {
        public ushort[] WORDS = new ushort[0];
        public ushort[] WORDS_TEMP = new ushort[0];

        public EipInOut EipType { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }

        public DateTime ChangedTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public StructEip(string name, int length, EipInOut type)
        {
            this.Name = name;
            this.Length = length;
            this.EipType = type;
            this.WORDS = new ushort[length];
            this.WORDS_TEMP = new ushort[length];


            UpdateTime = DateTime.Now;
            ChangedTime = DateTime.Now.AddSeconds(1);
        }
    }

    public class VirtualSGW : IVirtualMem
    {
        private OMRON.Compolet.CIPCompolet64.Variable.VariableCompolet eipIf;

        private object lockObject = new object();
        private Thread _worker = null;
        private bool _running = false;
        public Dictionary<string, StructEip> EipStructNames = new Dictionary<string, StructEip>();
        public VirtualSGW(string name)
        {
            FrmCtrl ff = new FrmCtrl();
            eipIf = ff.variableCompolet1;
        }

        //메소드 연결
        public override int Open()
        {
            try
            {
                eipIf.PlcEncoding = System.Text.Encoding.GetEncoding("utf-8");
                eipIf.Active = true;

                List<string> lstEipStructNames = eipIf.VariableNames.ToList();
                int noneEipStruct = EipStructNames.Count(f => lstEipStructNames.Contains(f.Key) == false);

                if (noneEipStruct <= 0)
                {
                    _running = true;
                    _worker = new Thread(new ThreadStart(Run));
                    _worker.Start();
                }

                return (noneEipStruct > 0) ? FALSE : TRUE;
            }
            catch (Exception ex)
            {
                return FALSE;
            }
        }
        public override int Close()
        {
            try
            {
                _running = false;
                _worker.Join();
                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }

        public void Run()
        {
            while (_running)
            {
                if (Monitor.TryEnter(lockObject))
                {
                    try
                    {
                        foreach (StructEip eip in EipStructNames.Values)
                        {
                            try
                            {
                                if (eip.EipType != EipInOut.IN) continue;
                                if (eip.ChangedTime != eip.UpdateTime)
                                {
                                    object objValue = eipIf.ReadVariable(eip.Name);

                                    Array arrValue = objValue as Array;

                                    for (int iPos = 0; iPos < eip.Length; iPos++)
                                        eip.WORDS_TEMP[iPos] = (ushort)(int)arrValue.GetValue(iPos);

                                    eip.ChangedTime = eip.UpdateTime;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("SGW ERROR {0}-{1}", eip.Name, eip.Length);
                            }
                        }

                        foreach (StructEip eip in EipStructNames.Values)
                        {
                            try
                            {
                                if (eip.EipType != EipInOut.OUT) continue;
                                if (eip.ChangedTime != eip.UpdateTime)
                                {
                                    eipIf.WriteVariable(eip.Name, eip.WORDS_TEMP);
                                    eip.ChangedTime = eip.UpdateTime;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("SGW ERROR {0}-{1}", eip.Name, eip.Length);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        Monitor.Exit(lockObject);
                    }
                }

                Thread.Sleep(10);
            }
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    foreach (StructEip eip in EipStructNames.Values)
                    {
                        if (eip.EipType != EipInOut.IN) continue;
                        Array.Copy(eip.WORDS_TEMP, 0, eip.WORDS, 0, eip.Length);
                        eip.UpdateTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            }

            return 0;
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    foreach (StructEip eip in EipStructNames.Values)
                    {
                        if (eip.EipType != EipInOut.OUT) continue;
                        Array.Copy(eip.WORDS, 0, eip.WORDS_TEMP, 0, eip.Length);
                        eip.UpdateTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            }

            return 0;
        }

        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
            return false;
        }
        public override void SetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void ClearBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            throw new Exception("미 구현");
        }
        public override void Toggle(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public bool[] GetBists(PlcAddr addr, int wordSize, out int result)
        {
            throw new Exception("미 구현");
        }

        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            throw new Exception("미 구현");
        }

        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            throw new Exception("미 구현");
        }

        //메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            throw new Exception("미 구현");
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            throw new Exception("미 구현");
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            throw new Exception("미 구현");
        }

        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                return ((short)EipStructNames[addr.MemName].WORDS[addr.Addr]).GetBit(addr.Bit);
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.SG)
            {
                short vv = ((short)EipStructNames[addr.MemName].WORDS[addr.Addr]).SetBit(addr.Bit, value);
                EipStructNames[addr.MemName].WORDS[addr.Addr] = (ushort)vv;
                return;
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override short VirGetShort(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                return (short)EipStructNames[addr.MemName].WORDS[addr.Addr];
            }
            throw new Exception("미구현 메모리");
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            if (addr.Type == PlcMemType.SG)
            {
                EipStructNames[addr.MemName].WORDS[addr.Addr] = (ushort)value;
            }
        }

        public override string VirGetAscii(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    short[] vv = new short[addr.Length];
                    for (int iPos = 0; iPos < vv.Length; iPos++)
                        vv[iPos] = (short)EipStructNames[addr.MemName].WORDS[addr.Addr + iPos];

                    return vv.GetAscii(0, addr.Length);
                }
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    short[] vv = new short[addr.Length];
                    vv.SetAscii(0, addr.Length, value);

                    for (int iPos = 0; iPos < vv.Length; iPos++)
                        EipStructNames[addr.MemName].WORDS[addr.Addr + iPos] = (ushort)vv[iPos];

                    return;
                }
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    ushort[] words = EipStructNames[addr.MemName].WORDS;
                    short[] vv = new short[]
                    {
                        (short)EipStructNames[addr.MemName].WORDS[addr.Addr + 0],
                        (short)EipStructNames[addr.MemName].WORDS[addr.Addr + 1]
                    };

                    return vv.GetInt32(0); ;
                }
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    short[] vv = new short[2];
                    vv.SetInt32(0, value);

                    EipStructNames[addr.MemName].WORDS[addr.Addr + 0] = (ushort)vv[0];
                    EipStructNames[addr.MemName].WORDS[addr.Addr + 1] = (ushort)vv[1];

                    return;
                }
            }

            throw new Exception("ADDR TYPE ERROR");
        }

        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            throw new Exception("미구현 메모리");
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    short[] vv = new short[addr.Length];

                    for (int iPos = 0; iPos < addr.Length; iPos++)
                        vv[iPos] = (short)EipStructNames[addr.MemName].WORDS[addr.Addr + iPos];
                    return vv;
                }
            }

            throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetShorts(PlcAddr addr, short[] values)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    for (int iPos = 0; iPos < values.Length; iPos++ )
                        EipStructNames[addr.MemName].WORDS[addr.Addr + iPos] = (ushort)values[iPos];  
                }
            }
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {

                    byte[] bytes = new byte[4];

                    bytes[0] = ((short)EipStructNames[addr.MemName].WORDS[addr.Addr + 0]).GetByte(0);
                    bytes[1] = ((short)EipStructNames[addr.MemName].WORDS[addr.Addr + 0]).GetByte(1);
                    bytes[2] = ((short)EipStructNames[addr.MemName].WORDS[addr.Addr + 1]).GetByte(0);
                    bytes[3] = ((short)EipStructNames[addr.MemName].WORDS[addr.Addr + 1]).GetByte(1);

                    return BitConverter.ToSingle(bytes, 0);
                }
            }
            throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (addr.Type == PlcMemType.SG)
            {
                if (EipStructNames.ContainsKey(addr.MemName))
                {
                    byte[] bytes = BitConverter.GetBytes(value);
                    ushort[] write = new ushort[2];

                    write[0] = (ushort)(bytes[0] + bytes[1] * 0x100);
                    write[1] = (ushort)(bytes[2] + bytes[3] * 0x100);

                    Array.Copy(write, 0, EipStructNames[addr.MemName].WORDS, addr.Addr, 2);

                    return;
                }
            }
            throw new Exception("ADDR TYPE ERROR");
        }
    }
}

