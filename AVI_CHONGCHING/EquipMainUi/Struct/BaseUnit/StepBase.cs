using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Step
{
    public enum EmEfemRunMode
    {
        Stop,
        Start,
        Pause,
        Home,
        CycleStop
    }
    public class StepBase
    {
        public string Name;

        protected string OldHomeStepStr;
        protected string OldSeqStepStr;
        protected string HomeStepStr;
        protected string SeqStepStr;

        protected DateTime StepStartTime;

        public EmEfemRunMode RunMode = EmEfemRunMode.Stop;
        public bool IsRunMode { get { return RunMode == EmEfemRunMode.Home || RunMode == EmEfemRunMode.Start || RunMode == EmEfemRunMode.CycleStop; } }
        private bool _isStartReserved = false;
        public virtual bool IsReserveStart
        {
            get { return _isStartReserved; }
            set { _isStartReserved = RunMode == EmEfemRunMode.Home ? value : false; }
        }

        public virtual bool IsHomeComplete { get; set; }
        public virtual PioSignalSend PioSend { get; set; }
        public virtual PioSignalRecv PioRecv { get; set; }

        public WaferInfoKey LowerWaferKey;

        protected WaferInfo _tempWaferInfo;
        protected CassetteInfo _tempCstInfo;

        public EmEfemDBPort DBPort;

        public virtual void LogicWorking(Equipment equip)
        {
            if (OldHomeStepStr != HomeStepStr)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "[SEQ] {0} Home Step Changed {2}\t(<-{1})", this.Name, OldHomeStepStr, HomeStepStr);
                OldHomeStepStr = HomeStepStr;
            }

            if (OldSeqStepStr != SeqStepStr)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "[SEQ] {0} Seq Step Changed {2}\t(<-{1})", this.Name, OldSeqStepStr, SeqStepStr);
                OldSeqStepStr = SeqStepStr;
            }
        }
        public virtual void StatusLogicWorking(Equipment equip)
        {

        }
        public virtual void HomeLogicWorking(Equipment equip)
        {

        }
        public virtual void SeqLogicWorking(Equipment equip)
        {

        }
        protected virtual bool IsSafeToHome()
        {
            return true;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        protected void PassStepTimeoverCheck()
        {
            StepStartTime = DateTime.Now;
        }

        public void SetRunMode(EmEfemRunMode mode)
        {
            RunMode = mode;
        }
        protected virtual void Stop()
        {
            if (PioSend != null)
                PioSend.Initailize();
            if (PioRecv != null)
                PioRecv.Initailize();
            SetRunMode(EmEfemRunMode.Stop);            
            IsHomeComplete = false;
            _isStartReserved = false;
        }
    }
}
