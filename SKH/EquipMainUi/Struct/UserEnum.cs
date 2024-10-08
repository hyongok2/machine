using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct
{
    public enum EmEquipRunMode
    {
        Auto,
        Manual
    }
    public enum EmCycleStop
    {
        None,
        Request,
        Complete,
    }
    public enum EmGlassDetect
    {        
        ALL,
        NOT,
        SOME,
    }
    public enum EmGlassCrack
    {
        NOT,
        SOME,
        ORIGIN,
        SEPARATION,
        REAR,
        FRONT,        
    }

    public enum EmReviewSkip
    {
        None,
        Request,
        SkipOn
    }
    public enum EmReviewManual
    {
        None,
        Request,
        ManualOn,
        InterLock
    }

    public enum EMPanelState
    {
        EMPTY = 0,
        IDLE = 1,
        STP = 2,
        PROCESSING = 3,
        DONE = 4,
        //ABORTING = 5,
        ABORTED = 5,
        CANCELED = 6
    }
    public enum EMMCmd
    {
        OFFLINE = 1,
        ONLINE_LOCAL = 2,
        ONLINE_REMOTE = 3
    }
    public enum EMEquipState
    {
        Normal = 1,
        Fault = 2,
        PM = 3,
        Unknown = 0
    }
    public enum EMProcState
    {        
        Idle = 1,              
        Execute = 2,
        Pause = 3,
        Unknown = 0,        
    }
    public enum EMByWho
    {
        ByHost = 1,
        ByOper = 2,
        ByEqp = 3,
        Unknown
    }

    public enum EmLoadType
    {
        Manual,
        OHT
    }
    public enum EmProgressWay
    {
        OnlyLast,
        OnlyFirst,
        Mapping,
        User,
    }
    public enum EmCimReportStatus
    {
        Run = 1,
        Idle,
        Down,
        StandByRun,
    }
}
