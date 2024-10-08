using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Dit.Framework.PLC;

/*
 * 68ADVN & 68ADIN 통합 사용 클래스
 * VER : 1.0 
 * DATE : 2017-12-19
*/
namespace Dit.Framework.Analog
{
    public class ADConverter_AJ65BTCU_68ADVN : AnalogBase
    {
        private int _stepNum { get; set; }

        public PlcAddr YB_DataInitCompleteFlag { get; set; }
        public PlcAddr XB_DataInitReqFlag { get; set; }
        public PlcAddr YB_DataSetReqFlag { get; set; }
        public PlcAddr XB_DataSetCompleteFlag { get; set; }
        public PlcAddr YB_ErrorResetFlag { get; set; }
        public PlcAddr XB_ErrorStatusFlag { get; set; }
        public PlcAddr XB_RemoteReady { get; set; }
        public PlcAddr[] XB_A2DCompleteFlag { get; set; }

        public PlcAddr Ww_ChangePerMitAndProhibitBit { get; set; }
        public PlcAddr[] Ww_RangeSettingBit { get; set; }
        public PlcAddr Ww_AvgProcessBit { get; set; }
        public PlcAddr[] Ww_AvgCountSetBit { get; set; }

        public PlcAddr Wr_ErrorCode { get; set; }

        private PlcAddr _startAddrX = null;
        private PlcAddr _startAddrY = null;
        private PlcAddr _startAddrWr = null;
        private PlcAddr _startAddrWw = null;

        public float[] ReadDataBuf { get; set; }

        private int _channelCount = 0;

        public int ChannelCount { get { return _channelCount; } }

        private int _inputRangeCH1_4 = 0x00;
        private int _inputRangeCH5_8 = 0x00;
        private int _avgTime = 1000;

        public AnalogSetting ADSetting { get; set; }
        private Stopwatch _tmrInitDelay = new Stopwatch(); 

       
        /// <summary>
        /// Parameter Infomation        
        /// inputRange : 각 채널별 InputRange 세팅 값          
        /// channelCount : 해당 카드 채널 개수
        /// 
        /// /* Input Range Information (ADVN)
        /// * *               -10V ~ 10V : 0x00  / OUTPUT : -4000 ~ 4000
        /// * *                 0V ~  5V : 0x01  / OUTPUT :     0 ~ 4000
        /// * *                 1V ~  5V : 0x02  / OUTPUT :     0 ~ 4000
        /// * * UserSetting (-10V ~ 10V) : 0x03  / OUTPUT : -4000 ~ 4000
        /// * * UserSetting (  0V ~  5V) : 0x04  / OUTPUT :     0 ~ 4000
        /// * ex : CH1 : 0x00 / CH2 : 0x01 / CH3 : 0x02 / CH4 : 0x03 : inputRangeCh1_4 : 0x0123
        /// * ex : ch5 : 0x01 / CH6 : 0x03 / CH7 : 0x01 / CH8 : 0x02 : inputRangeCh5_8 : 0x1312
        /// */
        /// 
        /// /* Input Range Information (ADIN)
        /// * *              4mA ~ 20mA  : 0x00  / OUTPUT : 0 ~ 4000
        /// * *              0mA ~ 20mA  : 0x01  / OUTPUT : 0 ~ 4000
        /// * * UserSetting (0mA ~ 20mA) : 0x03  / OUTPUT : 0 ~ 4000
        /// */
        /// 
        /// /*
        /// * CEC PANDA : ISE30A-C : 0(0.6)~5V / -0.105 ~ 1.05MPa  Ratio1 : 5.56 / Ratio2 : 800
        /// * */
        /// /// </summary>
        /// <param name="inputRangeCh1_4"></param>
        /// <param name="inputRangeCh5_8"></param>
        /// <param name="channelCount"></param>
        /// <param name="avgTime"></param>
        public ADConverter_AJ65BTCU_68ADVN(int inputRangeCh1_4, int inputRangeCh5_8, int channelCount, int avgTime = 1000)
            :base()
        {
            _inputRangeCH1_4 = inputRangeCh1_4;
            _inputRangeCH5_8 = inputRangeCh5_8;
            _channelCount = channelCount;

            ADSetting = new AnalogSetting();

            XB_A2DCompleteFlag           /* */ = new PlcAddr[_channelCount];
            Ww_RangeSettingBit           /* */ = new PlcAddr[2];
            Ww_AvgCountSetBit            /* */ = new PlcAddr[_channelCount];
            ReadDataBuf                  /* */ = new float[_channelCount];

            Wr_AnalogReadDataBuf         /* */ = new PlcAddr[_channelCount];

            _stepNum = 0;
            IsErrorResetOk = false;

            for (int iPos = 0; iPos < _channelCount; iPos++)
                ReadDataBuf[iPos] = ERROR_VALUE;
            
        }
        /* Parameter Infomation
         * StaNo : 해당 카드 StationNo
         * analogStartAddr : 해당 카드 W영역 시작 주소        
        */
        public override void SetAddress(int stationNo, int analogStartAddr, IVirtualMem plc)
        {
            _startAddrX                                             /* */ = new PlcAddr(PlcMemType.X, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo-1], 0, 1);
            _startAddrY                                             /* */ = new PlcAddr(PlcMemType.Y, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo-1], 0, 1);
            _startAddrWr                                            /* */ = new PlcAddr(PlcMemType.Wr, analogStartAddr, 0, 1);
            _startAddrWw                                            /* */ = new PlcAddr(PlcMemType.Ww, analogStartAddr, 0, 1);

            YB_DataInitCompleteFlag                                /* */ = new PlcAddr(PlcMemType.Y, 0x18, 0, 1) + _startAddrY;
            YB_DataSetReqFlag                                      /* */ = new PlcAddr(PlcMemType.Y, 0x19, 0, 1) + _startAddrY;
            YB_ErrorResetFlag                                      /* */ = new PlcAddr(PlcMemType.Y, 0x1A, 0, 1) + _startAddrY;

            XB_DataInitReqFlag                                     /* */ = new PlcAddr(PlcMemType.X, 0x18, 0, 1) + _startAddrX;
            XB_DataSetCompleteFlag                                 /* */ = new PlcAddr(PlcMemType.X, 0x19, 0, 1) + _startAddrX;
            XB_ErrorStatusFlag                                     /* */ = new PlcAddr(PlcMemType.X, 0x1A, 0, 1) + _startAddrX;
            XB_RemoteReady                                         /* */ = new PlcAddr(PlcMemType.X, 0x1B, 0, 1) + _startAddrX;

            int addrX = 0x00;
            int addrWw = 0x04;
            int addrWr = 0x00;
            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                XB_A2DCompleteFlag[iPos]                           /* */ = new PlcAddr(PlcMemType.X, addrX, 0, 1) + _startAddrX;
                Ww_AvgCountSetBit[iPos]                            /* */ = new PlcAddr(PlcMemType.Ww, addrWw, 0, 1) + _startAddrWw;
                Wr_AnalogReadDataBuf[iPos]                         /* */ = new PlcAddr(PlcMemType.Wr, addrWr, 0, 1) + _startAddrWr;
                addrX += 0x01;
                addrWw += 0x01;
                addrWr += 0x01;
            }

            Ww_ChangePerMitAndProhibitBit                          /* */ = new PlcAddr(PlcMemType.Ww, 0x00, 0, 1) + _startAddrWw;
            Ww_RangeSettingBit[0]                                  /* */ = new PlcAddr(PlcMemType.Ww, 0x01, 0, 1) + _startAddrWw;
            Ww_RangeSettingBit[1]                                  /* */ = new PlcAddr(PlcMemType.Ww, 0x02, 0, 1) + _startAddrWw;
            Ww_AvgProcessBit                                       /* */ = new PlcAddr(PlcMemType.Ww, 0x03, 0, 1) + _startAddrWw;

            Wr_ErrorCode                                           /* */ = new PlcAddr(PlcMemType.Wr, 0x08, 0, 1) + _startAddrWr;

            YB_DataInitCompleteFlag.PLC = plc;
            YB_DataSetReqFlag      .PLC = plc;
            YB_ErrorResetFlag      .PLC = plc;
                                  
            XB_DataInitReqFlag     .PLC = plc;
            XB_DataSetCompleteFlag .PLC = plc;
            XB_ErrorStatusFlag     .PLC = plc;
            XB_RemoteReady         .PLC = plc;

            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                XB_A2DCompleteFlag[iPos]  .PLC = plc;          
                Ww_AvgCountSetBit[iPos]   .PLC = plc;
                Wr_AnalogReadDataBuf[iPos].PLC = plc;          
            }

            Ww_ChangePerMitAndProhibitBit.PLC = plc; 
            Ww_RangeSettingBit[0]        .PLC = plc; 
            Ww_RangeSettingBit[1]        .PLC = plc; 
            Ww_AvgProcessBit             .PLC = plc; 

            Wr_ErrorCode                 .PLC = plc; 
        }
        private bool _isFirst = true;
        public override void LogicWorking()
        {
            if (XB_ErrorStatusFlag.vBit == true)
            {
                _stepNum = -1;

                for (int iPos = 0; iPos < _channelCount; iPos++)
                    ReadDataBuf[iPos] = ERROR_VALUE;

                YB_DataInitCompleteFlag.vBit = false;
                YB_DataSetReqFlag.vBit = false;

                YB_ErrorResetFlag.vBit = true;


                /*
                if (IsErrorResetOk == true)
                {
                    YB_DataInitCompleteFlag.vBit = false;
                    YB_DataSetReqFlag.vBit = false;

                    YB_ErrorResetFlag.vBit = true;
                    _stepNum = 0;
                }
                else
                {
                    for (int iPos = 0; iPos < _channelCount; iPos++)
                        ReadDataBuf[iPos] = ERROR_VALUE;
                }*/
            }
            else
            {
                YB_ErrorResetFlag.vBit = false;
                IsErrorResetOk = false;

                if (_stepNum == -1)
                    _stepNum = 0;
            }

            if (_stepNum == 0)
            {
                YB_DataInitCompleteFlag.vBit = false;
                YB_DataSetReqFlag.vBit = false;

                Ww_RangeSettingBit[0].vFloat = _inputRangeCH1_4;
                Ww_RangeSettingBit[1].vFloat = _inputRangeCH5_8;

                if (ReadData() == false/* || _isFirst == true*/)
                    _stepNum = 10;
            }
            else if (_stepNum == 10)
            {
                _isFirst = false;
                _tmrInitDelay.Restart();
                YB_DataInitCompleteFlag.vBit = true;
                _stepNum = 20;
            }
            else if (_stepNum == 20)
            {
                YB_DataSetReqFlag.vBit = true;
                _tmrInitDelay.Restart();
                _stepNum = 30;
                
            }
            else if (_stepNum == 30)
            {
                Ww_ChangePerMitAndProhibitBit.vFloat = 0x00FF;
                _stepNum = 40;
            }
            else if (_stepNum == 40)
            {
                Ww_RangeSettingBit[0].vFloat = _inputRangeCH1_4;
                Ww_RangeSettingBit[1].vFloat = _inputRangeCH5_8;
                _stepNum = 50;
            }
            else if(_stepNum == 50)
            {
                Ww_AvgProcessBit.vFloat = 0xFFFF;

                for (int iPos = 0; iPos < _channelCount; iPos++)
                    Ww_AvgCountSetBit[iPos].vFloat = _avgTime;

                _stepNum = 60;
            }
            else if (_stepNum == 60)
            {
                YB_DataInitCompleteFlag.vBit = false;
                YB_DataSetReqFlag.vBit = false;
                _stepNum = 0;
            }
        }
        private bool ReadData()
        {
            Func<string, float> convert = delegate(string value)
            {
               //if (_AirFlag == true)
               //    return value == "MPa" ? 1000.0f : 10.0f;
               //else
                    return value == "MPa" ? 1000.0f : -10.0f;
            };

            if (XB_RemoteReady.vBit == true)
            {
                for (int iPos = 0; iPos < _channelCount; iPos++)
                {
                    if (XB_A2DCompleteFlag[iPos].vBit == false)
                    {
                        ReadDataBuf[iPos] = ERROR_VALUE;
                        _stepNum = 0;
                        continue;
                    }  

                    ReadDataBuf[iPos] = Wr_AnalogReadDataBuf[iPos].vFloat;

                    //Gain - Offset
                    ReadDataBuf[iPos] -= ADSetting.GetOffset(iPos);

                    ReadDataBuf[iPos] /= ADSetting.GetRatio(iPos);

                    //ReadDataBuf[iPos] = (float)Math.Truncate((double)ReadDataBuf[iPos]);
                }
                return true;
            }
            else
                return false;
        }

        public void ErrorReset()
        {
            IsErrorResetOk = true;
            //_stepNum = 20;
        }
        public short GetErrorCode
        {
            get
            {
                return Wr_ErrorCode.vShort;
            }
        }
    }
}
