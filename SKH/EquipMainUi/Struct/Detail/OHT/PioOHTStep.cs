using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.Struct.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.OHT
{
    public enum EmOHTSendStep
    {
        S000_WAIT,
        S005_OHT_START,

        S010_WAIT_CS_0_ON,
        S020_WAIT_VALID_ON,
        S030_LOAD_REQUEST_ON,
        S040_WAIT_TR_REQUEST_ON,
        S050_READY_ON,
        S060_WAIT_BUSY_ON,
        S070_LOAD_REQUEST_OFF,
        S080_WAIT_BUSY_OFF,
        S090_WAIT_TR_REQUEST_OFF,
        S100_WAIT_COMPT_ON,
        S110_READY_OFF,
        S120_WAIT_VALID_OFF,
        S130_WAIT_COMPT_OFF,
        S140_WAIT_CS_0_OFF,

        S150_OHT_COMPLETE,
    }
    public enum EmOHTtype
    {
        NONE,
        LOAD,
        UNLOAD,
    }


    public class PioOHTStep : StepBase
    {
        EFEMLPMUnit loadport;

        #region PIO Signal
        //Active
        Sensor OHT_VALID;
        Sensor OHT_CS_0;
        Sensor OHT_CS_1;
        Sensor OHT_AM_AVBL;
        Sensor OHT_TR_REQ;
        Sensor OHT_BUSY;
        Sensor OHT_COMPT;
        Sensor OHT_CONT;
        //Switch A_NC;
        //Passive
        Switch EQ_L_REQ;
        Switch EQ_U_REQ;
        Switch EQ_VA;
        Switch EQ_READY;
        Switch EQ_VS_0;
        Switch EQ_VS_1;
        Switch EQ_HO_AVBL;
        Switch EQ_ES;
        //Sensor P_NC;
        #endregion
        EmOHTSendStep _ohtStep;
        public EmOHTSendStep Step { get { return _ohtStep; } }
        public bool IsRunning { get { return Step > EmOHTSendStep.S010_WAIT_CS_0_ON; } }
        EmOHTtype _type;
        public bool LightCurtainIO
        {
            set
            {
                if (EQ_ES != null)
                    EQ_ES.OnOff(GG.Equip, value);
            }
        }
        public void Initalize(Equipment equip, EmEfemPort port)
        {
            loadport = equip.Efem.LoadPort1;
            if (port == EmEfemPort.LOADPORT1)
            {
                loadport = equip.Efem.LoadPort1;

                EQ_L_REQ = equip.OHT.LP_2_OUT1;
                EQ_U_REQ = equip.OHT.LP_2_OUT2;
                EQ_VA = equip.OHT.LP_2_OUT3;
                EQ_READY = equip.OHT.LP_2_OUT4;
                EQ_VS_0 = equip.OHT.LP_2_OUT5;
                EQ_VS_1 = equip.OHT.LP_2_OUT6;
                EQ_HO_AVBL = equip.OHT.LP_2_OUT7;
                EQ_ES = equip.OHT.LP_2_OUT8;

                OHT_VALID = equip.OHT.LP_2_IN1;
                OHT_CS_0 = equip.OHT.LP_2_IN2;
                OHT_CS_1 = equip.OHT.LP_2_IN3;
                OHT_AM_AVBL = equip.OHT.LP_2_IN4;
                OHT_TR_REQ = equip.OHT.LP_2_IN5;
                OHT_BUSY = equip.OHT.LP_2_IN6;
                OHT_COMPT = equip.OHT.LP_2_IN7;
                OHT_CONT = equip.OHT.LP_2_IN8;

                this.Name = "LPM1 OHT";
            }
            else
            {
                loadport = equip.Efem.LoadPort2;
                //Y
                EQ_L_REQ = equip.OHT.LP_1_OUT1;
                EQ_U_REQ = equip.OHT.LP_1_OUT2;
                EQ_VA = equip.OHT.LP_1_OUT3;
                EQ_READY = equip.OHT.LP_1_OUT4;
                EQ_VS_0 = equip.OHT.LP_1_OUT5;
                EQ_VS_1 = equip.OHT.LP_1_OUT6;
                EQ_HO_AVBL = equip.OHT.LP_1_OUT7;
                EQ_ES = equip.OHT.LP_1_OUT8;
                //X
                OHT_VALID = equip.OHT.LP_1_IN1;
                OHT_CS_0 = equip.OHT.LP_1_IN2;
                OHT_CS_1 = equip.OHT.LP_1_IN3;
                OHT_AM_AVBL = equip.OHT.LP_1_IN4;
                OHT_TR_REQ = equip.OHT.LP_1_IN5;
                OHT_BUSY = equip.OHT.LP_1_IN6;
                OHT_COMPT = equip.OHT.LP_1_IN7;
                OHT_CONT = equip.OHT.LP_1_IN8;

                this.Name = "LPM2 OHT";
            }
        }
        public bool OHTStart(EmOHTtype type)
        {
            _type = type;

            if (_type == EmOHTtype.LOAD)
                _IsLDComplete = false;
            else if (_type == EmOHTtype.UNLOAD)
                _IsULDComplete = false;
            else
            {
                Logger.CIMLog.AppendLine(LogLevel.Error, "OHT Load, Unload 미지정");
                return false;
            }

            if(GG.OHTTestMode)
            {
                if (_type == EmOHTtype.LOAD)
                    _IsLDComplete = true;
                else if (_type == EmOHTtype.UNLOAD)
                    _IsULDComplete = true;

                return true;
            }

            _ohtStep = EmOHTSendStep.S005_OHT_START;
            return true;
        }
        public bool StepReset()
        {
            _IsLDComplete = false;
            _IsULDComplete = false;
            _ohtStep = EmOHTSendStep.S000_WAIT;
            _type = EmOHTtype.NONE;

            EQ_L_REQ.OnOff(GG.Equip, false);
            EQ_U_REQ.OnOff(GG.Equip, false);
            EQ_VA.OnOff(GG.Equip, false);
            EQ_READY.OnOff(GG.Equip, false);
            EQ_VS_0.OnOff(GG.Equip, false);
            EQ_VS_1.OnOff(GG.Equip, false);
            EQ_HO_AVBL.OnOff(GG.Equip, false);
            EQ_ES.OnOff(GG.Equip, false);

            return true;
        }
        private bool _IsLDComplete, _IsULDComplete;
        public bool IsComplete(EmOHTtype type)
        {
            return 
                _ohtStep == EmOHTSendStep.S000_WAIT && 
                (type == EmOHTtype.LOAD ? _IsLDComplete : _IsULDComplete);
        }
        public void OHTLoadComplete()
        {
            _IsLDComplete = true;
        }
        public void OHTUnLoadComplete()
        {
            _IsULDComplete = true;
        }
        public override void LogicWorking(Equipment equip)
        {
            LdLogic(equip);
        }
        PlcTimerEx OhtTimeover = new PlcTimerEx("OHT TIMOVER DELAY");
        EmOHTSendStep _ohtStepOld;
        private void SeqLogging(Equipment equip)
        {
            SeqStepStr = _ohtStep.ToString();

            if (_ohtStep != _ohtStepOld)
                _ohtStepOld = _ohtStep;

            base.LogicWorking(equip);
        }
        private void LdLogic(Equipment equip)
        {
            LightCurtainIO = GG.Equip.Efem.LPMLightCurtain.Detect.IsOn != true;
            SeqLogging(equip);
            if (OhtTimeover && _ohtStep > EmOHTSendStep.S010_WAIT_CS_0_ON)
            {
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0618_OHT_INTERFACE_TIMEOVER);
                OhtTimeover.Stop();
            }
            switch (_ohtStep)
            {
                case EmOHTSendStep.S000_WAIT:
                    OhtTimeover.Stop();
                    break;
                case EmOHTSendStep.S005_OHT_START:

                    EQ_HO_AVBL.OnOff(equip, true);
                    //EQ_ES.OnOff(equip, true);

                    _ohtStep = EmOHTSendStep.S010_WAIT_CS_0_ON;
                    break;
                case EmOHTSendStep.S010_WAIT_CS_0_ON:
                    if(OHT_CS_0.IsOn)
                    {
                        OhtTimeover.Start(45);
                        _ohtStep = EmOHTSendStep.S020_WAIT_VALID_ON;
                    }
                    break;
                case EmOHTSendStep.S020_WAIT_VALID_ON:
                    if (OHT_VALID.IsOn)
                    {
                        _ohtStep = EmOHTSendStep.S030_LOAD_REQUEST_ON;
                    }
                    break;
                case EmOHTSendStep.S030_LOAD_REQUEST_ON:
                    if(_type == EmOHTtype.LOAD)
                    {
                        EQ_L_REQ.OnOff(equip, true);
                        _ohtStep = EmOHTSendStep.S040_WAIT_TR_REQUEST_ON;
                    }
                    else if(_type == EmOHTtype.UNLOAD)
                    {
                        EQ_U_REQ.OnOff(equip, true);
                        _ohtStep = EmOHTSendStep.S040_WAIT_TR_REQUEST_ON;
                    }
                    break;
                case EmOHTSendStep.S040_WAIT_TR_REQUEST_ON:
                    if(OHT_TR_REQ.IsOn)
                    {
                        _ohtStep = EmOHTSendStep.S050_READY_ON;
                    }
                    break;
                case EmOHTSendStep.S050_READY_ON:
                    EQ_READY.OnOff(equip, true);
                    _ohtStep = EmOHTSendStep.S060_WAIT_BUSY_ON;
                    break;
                case EmOHTSendStep.S060_WAIT_BUSY_ON:
                    if(OHT_BUSY.IsOn)
                    {
                        _ohtStep = EmOHTSendStep.S070_LOAD_REQUEST_OFF;
                    }
                    break;
                case EmOHTSendStep.S070_LOAD_REQUEST_OFF:
                    if (_type == EmOHTtype.LOAD)
                    {
                        EQ_L_REQ.OnOff(equip, false);
                    }
                    else if (_type == EmOHTtype.UNLOAD)
                    {
                        EQ_U_REQ.OnOff(equip, false);
                    }
                    _ohtStep = EmOHTSendStep.S080_WAIT_BUSY_OFF;
                    break;
                case EmOHTSendStep.S080_WAIT_BUSY_OFF:
                    if (OHT_BUSY.IsOn == false)
                    {
                        _ohtStep = EmOHTSendStep.S090_WAIT_TR_REQUEST_OFF;
                    }
                    break;
                case EmOHTSendStep.S090_WAIT_TR_REQUEST_OFF:
                    if (OHT_TR_REQ.IsOn == false)
                    {
                        _ohtStep = EmOHTSendStep.S100_WAIT_COMPT_ON;
                    }
                    break;
                case EmOHTSendStep.S100_WAIT_COMPT_ON:
                    if (OHT_COMPT.IsOn)
                    {
                        _ohtStep = EmOHTSendStep.S110_READY_OFF;
                    }
                    break;
                case EmOHTSendStep.S110_READY_OFF:
                    EQ_READY.OnOff(equip, false);
                    _ohtStep = EmOHTSendStep.S120_WAIT_VALID_OFF;
                    break;
                case EmOHTSendStep.S120_WAIT_VALID_OFF:
                    if (OHT_VALID.IsOn == false)
                    {
                        _ohtStep = EmOHTSendStep.S130_WAIT_COMPT_OFF;
                    }
                    break;
                case EmOHTSendStep.S130_WAIT_COMPT_OFF:
                    if (OHT_COMPT.IsOn == false)
                    {
                        _ohtStep = EmOHTSendStep.S140_WAIT_CS_0_OFF;
                    }
                    break;
                case EmOHTSendStep.S140_WAIT_CS_0_OFF:
                    if (OHT_CS_0.IsOn == false)
                    {
                        _ohtStep = EmOHTSendStep.S150_OHT_COMPLETE;
                    }
                    break;
                case EmOHTSendStep.S150_OHT_COMPLETE:
                    OhtTimeover.Stop();
                    EQ_HO_AVBL.OnOff(equip, false);
                    //EQ_ES.OnOff(equip, false);

                    if (_type == EmOHTtype.LOAD)
                    {
                        Logger.Log.AppendLine("OHT인터페이스 완료, LOAD COMPLETE");
                        _IsLDComplete = true;
                    }
                    else if (_type == EmOHTtype.UNLOAD)
                    {
                        Logger.Log.AppendLine("OHT인터페이스 완료, UNLOAD COMPLETE");
                        _IsULDComplete = true;
                    }

                    _type = EmOHTtype.NONE;
                    _ohtStep = EmOHTSendStep.S000_WAIT;
                    break;
            }
        }
    }
}
