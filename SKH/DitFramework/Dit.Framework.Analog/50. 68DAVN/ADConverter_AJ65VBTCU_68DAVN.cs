using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace Dit.Framework.Analog
{
    //Vacuum
    public class ADConverter_AJ65VBTCU_68DAVN : AnalogBase
    {
        public static int THICKNESS_COUNT = 8;
        public static int CHANNEL_COUNT = 8;

        private int _stepNum { get; set; }

        public PlcAddr YB_DataInitCompleteFlag { get; set; }            //Y2D8
        public PlcAddr XB_DataInitReqFlag { get; set; }                 //X2D8

        public PlcAddr YB_DataSetReqFlag { get; set; }                  //Y2D9
        public PlcAddr XB_DataSetCompleteFlag { get; set; }             //X2D9

        public PlcAddr YB_ErrorResetFlag { get; set; }                  //Y2DA           
        public PlcAddr XB_ErrorStatusFlag { get; set; }                 //X2DA

        public PlcAddr XB_RemoteReady { get; set; }                     //X2DB

        public PlcAddr[] YB_OutputPermitBit { get; set; }               //Y2C0 Y2C1 Y2C2 Y2C3 Y2C4

        public PlcAddr Ww_ChangePermitAndProhibitBit { get; set; }      //Ww3C
        public PlcAddr[] Ww_RangeSettingBit { get; set; }               //Ww3D Ww3E

        public PlcAddr Wr_ErrorCode { get; set; }                       //

        private PlcAddr _startAddrX = null;
        private PlcAddr _startAddrY = null;
        private PlcAddr _startAddrWr = null;
        private PlcAddr _startAddrWw = null;

        public float[] Offset { get; set; }
        public float[] DivSet { get; set; }
        public float[] WriteDataBuf { get; set; }

        public float[] WriteValue = new float[CHANNEL_COUNT];

        public DigitalSetting[] DASetting { get; set; }

        private int _outputRangeCH1_4 = 0x00;
        private int _outputRangeCH5_8 = 0x00;

        private int _channelCount = 0;
        public int ChannelCount { get { return _channelCount; } }

        public int DaNo { get; set; }

        private bool _isFirat = true;

        /* Parameter Infomation        
         * OutputRange : 각 채널별 OutputRange 세팅 값          
         * channelCount : 해당 카드 채널 개수
        */

        /* output Range Information (DAVN)
         * *               -10V ~ 10V : 0x00  / OUTPUT : -4000 ~ 4000
         * *                 0V ~  5V : 0x01  / OUTPUT :     0 ~ 4000
         * *                 1V ~  5V : 0x02  / OUTPUT :     0 ~ 4000
         * * UserSetting (-10V ~ 10V) : 0x03  / OUTPUT : -4000 ~ 4000
         * * UserSetting (  0V ~  5V) : 0x04  / OUTPUT :     0 ~ 4000
         * ex : CH1 : 0x00 / CH2 : 0x01 / CH3 : 0x02 / CH4 : 0x03 : inputRangeCh1_4 : 0x0123
         * ex : ch5 : 0x01 / CH6 : 0x03 / CH7 : 0x01 / CH8 : 0x02 : inputRangeCh5_8 : 0x1312
        */

        /*
         * CEC PANDA : ITV1050-222CS : 0~5V / 0~0.9MPa  Ratio1 : 5.56 / Ratio2 : 800
         * */
        public ADConverter_AJ65VBTCU_68DAVN(int daNo, int outputRangeCh1_4, int outputRangeCh5_8, int channelCount, int avgTime = 1000)
        {
            this.DaNo = daNo;

            _channelCount = channelCount;
            _outputRangeCH1_4 = outputRangeCh1_4;
            _outputRangeCH5_8 = outputRangeCh5_8;

            Ww_RangeSettingBit           /* */ = new PlcAddr[2];
            WriteDataBuf                 /* */ = new float[_channelCount];

            YB_OutputPermitBit           /* */ = new PlcAddr[_channelCount];
            Wr_AnalogReadDataBuf         /* */ = new PlcAddr[_channelCount];
            Ww_AnalogWriteDataBuf        /* */ = new PlcAddr[_channelCount];

            DASetting = new DigitalSetting[THICKNESS_COUNT];

            for (int iPos = 0; iPos < CHANNEL_COUNT; iPos++)
                DASetting[iPos] = new DigitalSetting();

            Offset = new float[_channelCount];

            _stepNum = 0;
            IsErrorResetOk = false;

            for (int iPos = 0; iPos < _channelCount; iPos++)
                WriteDataBuf[iPos] = 0;
        }
        public override void SetAddress(int stationNo, int analogStartAddr, IVirtualMem plc)
        {
            _startAddrX                                             /* */ = new PlcAddr(PlcMemType.X, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo - 1], 0, 1);
            _startAddrY                                             /* */ = new PlcAddr(PlcMemType.Y, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo - 1], 0, 1);
            _startAddrWr                                            /* */ = new PlcAddr(PlcMemType.Wr, analogStartAddr, 0, 1);
            _startAddrWw                                            /* */ = new PlcAddr(PlcMemType.Ww, analogStartAddr, 0, 1);

            YB_DataInitCompleteFlag                                /* */ = new PlcAddr(PlcMemType.Y, 0x18, 0, 1) + _startAddrY;
            YB_DataSetReqFlag                                      /* */ = new PlcAddr(PlcMemType.Y, 0x19, 0, 1) + _startAddrY;
            YB_ErrorResetFlag                                      /* */ = new PlcAddr(PlcMemType.Y, 0x1A, 0, 1) + _startAddrY;

            XB_DataInitReqFlag                                     /* */ = new PlcAddr(PlcMemType.X, 0x18, 0, 1) + _startAddrX;
            XB_DataSetCompleteFlag                                 /* */ = new PlcAddr(PlcMemType.X, 0x19, 0, 1) + _startAddrX;
            XB_ErrorStatusFlag                                     /* */ = new PlcAddr(PlcMemType.X, 0x1A, 0, 1) + _startAddrX;
            XB_RemoteReady                                         /* */ = new PlcAddr(PlcMemType.X, 0x1B, 0, 1) + _startAddrX;

            int addrY = 0x00;
            int addrWw = 0x00;
            int addrWr = 0x00;
            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                YB_OutputPermitBit[iPos]                           /* */ = new PlcAddr(PlcMemType.Y, addrY, 0, 1) + _startAddrY;
                Ww_AnalogWriteDataBuf[iPos]                        /* */ = new PlcAddr(PlcMemType.Ww, addrWw, 0, 1) + _startAddrWw;
                Wr_AnalogReadDataBuf[iPos]                         /* */ = new PlcAddr(PlcMemType.Wr, addrWr, 0, 1) + _startAddrWr;
                addrWw += 0x01;
                addrWr += 0x01;
                addrY += 0x01;
            }

            Ww_ChangePermitAndProhibitBit                          /* */ = new PlcAddr(PlcMemType.Ww, 0x08, 0, 1) + _startAddrWw;
            Ww_RangeSettingBit[0]                                  /* */ = new PlcAddr(PlcMemType.Ww, 0x09, 0, 1) + _startAddrWw;
            Ww_RangeSettingBit[1]                                  /* */ = new PlcAddr(PlcMemType.Ww, 0x0A, 0, 1) + _startAddrWw;

            Wr_ErrorCode                                           /* */ = new PlcAddr(PlcMemType.Wr, 0x08, 0, 1) + _startAddrWr;

            YB_DataInitCompleteFlag.PLC = plc;
            YB_DataSetReqFlag.PLC = plc;
            YB_ErrorResetFlag.PLC = plc;

            XB_DataInitReqFlag.PLC = plc;
            XB_DataSetCompleteFlag.PLC = plc;
            XB_ErrorStatusFlag.PLC = plc;
            XB_RemoteReady.PLC = plc;

            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                YB_OutputPermitBit[iPos].PLC = plc;
                Ww_AnalogWriteDataBuf[iPos].PLC = plc;
                Wr_AnalogReadDataBuf[iPos].PLC = plc;
            }

            Ww_ChangePermitAndProhibitBit.PLC = plc;
            Ww_RangeSettingBit[0].PLC = plc;
            Ww_RangeSettingBit[1].PLC = plc;

            Wr_ErrorCode.PLC = plc;
        }
        public override void LogicWorking()
        {
            if (XB_ErrorStatusFlag.vBit == true)
            {
                _stepNum = -1;

                if (IsErrorResetOk == true)
                {
                    YB_ErrorResetFlag.vBit = true;
                    _stepNum = 0;
                }
                else
                {
                    for (int iPos = 0; iPos < _channelCount; iPos++)
                        YB_OutputPermitBit[iPos].vBit = false;
                }
            }
            else
            {
                YB_ErrorResetFlag.vBit = false;
                IsErrorResetOk = false;
            }        
            if (_stepNum == 0)
            {
                Ww_RangeSettingBit[0].vFloat = _outputRangeCH1_4;
                Ww_RangeSettingBit[1].vFloat = _outputRangeCH5_8;

                if (WriteData() == false || _isFirat == true)
                    _stepNum = 10;
                else
                {
                    YB_DataInitCompleteFlag.vBit = false;
                    YB_DataSetReqFlag.vBit = false;
                }
            }
            else if (_stepNum == 10)
            {
                _isFirat = false;
                YB_DataInitCompleteFlag.vBit = true;
                _stepNum = 20;
            }
            else if (_stepNum == 20)
            {
                YB_DataSetReqFlag.vBit = true;
                _stepNum = 30;
            }
            else if (_stepNum == 30)
            {
                Ww_ChangePermitAndProhibitBit.vFloat = 0x00;
                Ww_RangeSettingBit[0].vFloat = _outputRangeCH1_4;
                Ww_RangeSettingBit[1].vFloat = _outputRangeCH5_8;
                _stepNum = 40;
            }
            else if (_stepNum == 40)
            {
                if (XB_DataInitReqFlag.vBit == false)
                    YB_DataInitCompleteFlag.vBit = false;

                if (XB_DataSetCompleteFlag.vBit == true)
                    YB_DataSetReqFlag.vBit = false;

                _stepNum = 0;
            }
        }
        public bool WriteData()
        {
            if (XB_RemoteReady.vBit == false)
                return false;

            for (int iPos = 0; iPos < _channelCount; iPos++)
            {
                YB_OutputPermitBit[iPos].vBit = true;

                if (Ww_AnalogWriteDataBuf[iPos].vFloat != WriteDataBuf[iPos])
                {
                    Ww_AnalogWriteDataBuf[iPos].vFloat = WriteDataBuf[iPos];
                    
                }
                    /*
                else
                    YB_OutputPermitBit[iPos].vBit = false;*/
            }
            return true;
        }
        public void SetValue(int index, float value)
        {
            WriteValue[index] = value;
            //가공 후 적용~
            value = value * DASetting[0].GetRatio(index) + DASetting[0].GetOffset(index);

            WriteDataBuf[index] = value;
            //if (value == 0)
            //    WriteDataBuf[index] = 0;
            //else
            //    WriteDataBuf[index] = value + DASetting[0].GetOffset(index);   

            _stepNum = 0;
        }
        public void Initalize(int glassThickness)
        {
            float[] writeValue = new float[]
                {
                    DASetting[glassThickness].GetValue(0),
                    DASetting[glassThickness].GetValue(1),
                    DASetting[glassThickness].GetValue(2),
                    DASetting[glassThickness].GetValue(3),
                    DASetting[glassThickness].GetValue(4),
                    DASetting[glassThickness].GetValue(5),
                    DASetting[glassThickness].GetValue(6),
                    DASetting[glassThickness].GetValue(7),
                };

            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                SetValue(iPos, writeValue[iPos]);
            }
        }
        public void ErrorReset()
        {
            IsErrorResetOk = true;
        }
    }
}
