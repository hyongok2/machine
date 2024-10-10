using Dit.Framework.Analog;
using Dit.Framework.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail
{
    public class ADConverter_AJ65SBT2B_64TD : AnalogBase
    {
        public PlcAddr[] YB_EnableFlag { get; set; }                //CHANEL 변환 활성화 플래그
        public PlcAddr[] YB_Sampling { get; set; }                  //CHANEL 샘플링 처리 평균  
        public PlcAddr YB_Offset { get; set; }                      //오프셋/이득 값 선택 플래그
        public PlcAddr YB_InitDataSetReqFlag { get; set; }          //초기 데이터 설정 요청 플래그
        public PlcAddr XB_InitDataSetCompleteFlag { get; set; }     //초기 데이터 설정 완료 플래그        
        public PlcAddr YB_InitDataProcessCompleteFlag { get; set; } //초기 데이터 처리 완료 플래그 
        public PlcAddr XB_InitDataProcessReqFlag { get; set; }      //초기 데이터 처리 요청 플래그
        public PlcAddr YB_ErrorReset { get; set; }                  //오류 리셋 플래그 
        public PlcAddr XB_ErrorStatusFlag { get; set; }             //에러 상태 플래그
        public float[] ReadDataBuf { get; set; }
        public PlcAddr[] XB_A2DCompleteFlag { get; set; }

        public PlcAddr XB_RemoteReady { get; set; }

        private PlcAddr _startAddrX = null;
        private PlcAddr _startAddrY = null;
        private PlcAddr _startAddrWr = null;

        public AnalogSetting TDSetting { get; set; }
        private int _stepNum = 0;
        private int _nextStepNum = 0;

        private int _channelCount = 0;
        public int ChannelCount { get { return _channelCount; } }

        private bool[] _useChannelList;
        //채널별 사용유무
        //4 채널 4개 사용 true, true, true, ture
        //4 채널 2개 사용 true, false, true, false 
        public bool[] UseChannelList { get { return _useChannelList; } }

        public ADConverter_AJ65SBT2B_64TD(int channelCount, bool[] useChannelList = null)
        {
            _channelCount = channelCount;
            if (useChannelList == null || useChannelList.Length != channelCount)
                _useChannelList = Enumerable.Repeat(true, _channelCount).ToArray();
            else
                _useChannelList = useChannelList;

            TDSetting = new AnalogSetting();

            YB_EnableFlag                       /* */ = new PlcAddr[_channelCount];
            YB_Sampling                             /* */ = new PlcAddr[_channelCount];
            ReadDataBuf                             /* */ = new float[_channelCount];
            XB_A2DCompleteFlag                      /* */ = new PlcAddr[_channelCount];
            Wr_AnalogReadDataBuf                    /* */ = new PlcAddr[_channelCount];

            _stepNum = 0;
            IsErrorResetOk = false;

            for (int iter = 0; iter < _channelCount; iter++)
                ReadDataBuf[iter] = ERROR_VALUE;
        }
        public void SetAddress(int stationNo, int analogStartAddr, IVirtualMem plc, short channel = 81)
        {
            _startAddrX                                             /* */ = new PlcAddr(PlcMemType.X, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo - 1], 0, 1);
            _startAddrY                                             /* */ = new PlcAddr(PlcMemType.Y, AnalogBase.STATION_ADDRESS_XY_DEF[stationNo - 1], 0, 1);
            _startAddrWr                                            /* */ = new PlcAddr(PlcMemType.Wr, analogStartAddr, 0, 1);

            YB_Offset                                               /* */ = new PlcAddr(PlcMemType.Y, 0x17, 0, 1) + _startAddrY;
            YB_InitDataProcessCompleteFlag                          /* */ = new PlcAddr(PlcMemType.Y, 0x18, 0, 1) + _startAddrY;
            YB_InitDataSetReqFlag                                   /* */ = new PlcAddr(PlcMemType.Y, 0x19, 0, 1) + _startAddrY;
            YB_ErrorReset                                           /* */ = new PlcAddr(PlcMemType.Y, 0x1A, 0, 1) + _startAddrY;

            XB_InitDataProcessReqFlag                               /* */ = new PlcAddr(PlcMemType.X, 0x18, 0, 1) + _startAddrX;
            XB_InitDataSetCompleteFlag                              /* */ = new PlcAddr(PlcMemType.X, 0x19, 0, 1) + _startAddrX;
            XB_ErrorStatusFlag                                      /* */ = new PlcAddr(PlcMemType.X, 0x1A, 0, 1) + _startAddrX;
            XB_RemoteReady                                          /* */ = new PlcAddr(PlcMemType.X, 0x1B, 0, 1) + _startAddrX;

            int addrY = 0x00;
            int addrX = 0x00;
            int addrWw = 0x04;
            int addrWr = 0x00;
            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                YB_EnableFlag[iPos]                                 /* */ = new PlcAddr(PlcMemType.Y, addrY, 0, 1) + _startAddrY;
                YB_Sampling[iPos]                                   /* */ = new PlcAddr(PlcMemType.Y, addrY + 0x04, 0, 1) + _startAddrY;
                XB_A2DCompleteFlag[iPos]                            /* */ = new PlcAddr(PlcMemType.X, addrX, 0, 1) + _startAddrX;
                Wr_AnalogReadDataBuf[iPos]                          /* */ = new PlcAddr(PlcMemType.Wr, addrWr, 0, 1) + _startAddrWr;
                _startAddrY += 0x01;
                addrX += 0x01;
                addrWw += 0x01;
                addrWr += 0x01;
            }

            YB_Offset.PLC = plc;
            YB_InitDataProcessCompleteFlag.PLC = plc;
            YB_InitDataSetReqFlag.PLC = plc;
            YB_ErrorReset.PLC = plc;

            XB_InitDataProcessReqFlag.PLC = plc;
            XB_InitDataSetCompleteFlag.PLC = plc;
            XB_ErrorStatusFlag.PLC = plc;
            XB_RemoteReady.PLC = plc;

            for (int iPos = 0; iPos < ChannelCount; iPos++)
            {
                XB_A2DCompleteFlag[iPos].PLC = plc;
                YB_EnableFlag[iPos].PLC = plc;
                YB_Sampling[iPos].PLC = plc;
                Wr_AnalogReadDataBuf[iPos].PLC = plc;
            }
        }

        public override void LogicWorking()
        {
            //Error 상황
            if (XB_ErrorStatusFlag.vBit == true)
            {
                _stepNum = -1;

                if (IsErrorResetOk == true)
                {
                    YB_ErrorReset.vBit = true;
                    _stepNum = 0;
                }
                else
                {
                    for (int iter = 0; iter < _channelCount; iter++)
                        ReadDataBuf[iter] = ERROR_VALUE;
                }
            }
            else
            {
                YB_ErrorReset.vBit = false;
                IsErrorResetOk = false;
            }
            //초기 데이터 설정 완료 플래그(x199) 확인
            if (_stepNum == 0)
            {
                if (XB_InitDataSetCompleteFlag.vBit == true)
                {
                    YB_InitDataSetReqFlag.vBit = false;
                    _stepNum = 0;
                }

                if (ReadData() == false)
                    _stepNum = 10;
            }
            else if (_stepNum == 10)
            {
                SetEnableFlag();

                _stepNum = 20;
            }

            else if (_stepNum == 20)
            {
                //X198 Off -> X199 On 설정 시작
                if (XB_InitDataProcessReqFlag.vBit == false)
                {
                    _stepNum = 30;
                }
                //X198 On -> X198 Off 설정 시작.
                else
                    _stepNum = 40;
            }
            //초기 데이터 처리 요청 플래그(X198) OFF 전환시.
            //샘플링, Offset 초기 데이터 처리 완료 플래그 Reset
            //초기 데이터 설정 요청 플래그 Set 
            else if (_stepNum == 30)
            {
                for (int iPos = 0; iPos < _channelCount; iPos++)
                    YB_Sampling[iPos].vBit = false;

                YB_Offset.vBit = false;
                YB_InitDataProcessCompleteFlag.vBit = false;
                YB_InitDataSetReqFlag.vBit = true;
                _stepNum = 0;
            }
            //Chanel 변환 활성 플래그 Set
            //오프셋 / 이득 값 선택 플래그 Set
            //샘플링 처리/평균 Set
            //초기 데이터 처리 완료 플래그 Set
            else if (_stepNum == 40)
            {
                SetEnableFlag();

                if (XB_InitDataProcessReqFlag.vBit == true)
                {
                    for (int iPos = 0; iPos < _channelCount; iPos++)
                        YB_Sampling[iPos].vBit = true;

                    YB_Offset.vBit = true;
                    YB_InitDataProcessCompleteFlag.vBit = true;
                }
                _stepNum = 0;
            }
        }

        private void SetEnableFlag()
        {
            for (int iPos = 0; iPos < _channelCount; iPos++)
            {
                if (iPos < _useChannelList.Length && _useChannelList[iPos] == true)
                    YB_EnableFlag[iPos].vBit = true;
                else
                    YB_EnableFlag[iPos].vBit = false;
            }
        }

        public bool ReadData()
        {
            if (XB_RemoteReady.vBit == false)
            {
                return false;
            }
            else
            {
                for (int iPos = 0; iPos < _channelCount; iPos++)
                {
                    if (XB_A2DCompleteFlag[iPos].vBit == false)
                        continue;
                    ReadDataBuf[iPos] = (Wr_AnalogReadDataBuf[iPos].vShort / 10f) + TDSetting.GetOffset(iPos);
                }
            }

            return true;
        }
        public void ErrorReset()
        {
            IsErrorResetOk = true;
        }
    }
}
