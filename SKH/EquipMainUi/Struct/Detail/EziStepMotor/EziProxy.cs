using EquipMainUi.Struct.Detail.EziStepMotor.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.EziStepMotor
{
    public static class EziProxy
    {
        public static bool m_bConnected = false;
        private static byte m_nPortNo = 0;
        public static bool Connect(byte ComportNo, string Baudrate = "57600")
        {
            if (m_bConnected == false)
            {
                uint dwBaud;

                m_nPortNo = ComportNo;
                dwBaud = uint.Parse(Baudrate);

                if (EziMOTIONPlusRLib.FAS_Connect(m_nPortNo, dwBaud) == 0)
                {
                    // Failed to connect
                    return false;
                }
                else
                {
                    // connected.
                    m_bConnected = true;

                    for (byte i = 0; i < EziMOTIONPlusRLib.MAX_SLAVE_NUMS; i++)
                    {
                        if (EziMOTIONPlusRLib.FAS_IsSlaveExist(m_nPortNo, i) != 0)
                        {
                            break;
                        }
                    }
                    return true;
                }
            }
            else
            {
                EziMOTIONPlusRLib.FAS_Close(m_nPortNo);
                m_bConnected = false;
                return false;
            }
        }
    }
}
