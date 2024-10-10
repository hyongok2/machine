using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public enum EmEfemPort
    {
        ROBOT = 0,
        LOADPORT1,
        LOADPORT2,        
        EQUIPMENT = 5,
        ALIGNER = 9,
        /// <summary>
        /// F 변환 필요
        /// </summary>
        ETC,

        NONE = -1
    }
    public enum EmWaferIDType
    {
        WaferID,
        WaferRingDI,
    }
    public enum EmWaferMapType
    {
        Normal,
    }
    public enum EmWaferNotchDir
    {
        Bottom,
        Right,
        Top,
        Left,        
    }
    public enum EmEfemDBPort
    {
        LOADPORT1 = 1,
        LOADPORT2,
        UPPERROBOT,
        LOWERROBOT,
        ALIGNER,
        EQUIPMENT,

        NONE,
    }

    public enum EmEfemCommand
    {
        INIT_, // 지정한 Module을 초기화 시킨다, LoadPort는 홈 동작
        RESET, // 지정한 Module의 Error를 초기화 시킨다.
        TRANS, // 지정된 위치의 Wafer를 목표 지점으로 반송시킨다.
        WAITR, // Robot을 지정된 Module(Port) 위치 및 Slot 위치에 대기시킨다.
        STAT_, // 각 Module의 상태를 읽는다
        PAUSE, // Robot 모션 멈춤 기능
        RESUM, // Robot을 Pause 상태로부터 해제, 이전 작업을 완료시킨다
        STOP_, // Robot의 동작을 정지시킨다
        PATRR, // Aligner Rotate CW
        PASRD, // Aligner Send Ready
        PARDY, // Aligner에 Ready 신호를 준다.
        ALIGN, // Wafer를 지정한 각도로 정렬한다.        
        //190624 미사용 CLAMP, // Load Port의 FOUP을 Clamp시킨다.
        //190624 미사용 UCLAM, // Load Port의 FOUP을 UnClamp시킨다.
        //190624 미사용 DOCK_, // Load Port의 FOUP을 Dock 시킨다.
        //190624 미사용 UDOCK, // Load Port의 FOUP을 Un Dock 시킨다.
        OPEN_, // Load Port의 FOUP을 Open 시킨다.
        CLOSE, // Load Port의 FOUP을 Close 시킨다.
        //190624 미사용LOAD_, // Load Port의 FOUP을 Clamp, Docking, Open 시킨다.
        //190624 미사용ULOAD, // Load Port의 FOUP을 Close, UnDocking, UnClamp 시킨다.
        LPMSG, // Load ort에서 발생한 Event를 상위로 전송한다.
        MAPP_, // 지정한 Module의 Mapping 정보를 읽어온다.
        LPLED, // LPM의 LED 램프를 컨트롤 한다.
        CHMDA, // EFEM MODE를 Auto(On Line)로 변경시킨다. - 상위(제어)의 명령을 처리하는 모드
        CHMDM, // EFEM MODE를 Manual(Off Line)로 변경시킨다. - 상위(제어)의 명령을 무시하는 모드
        SIGLM, // Signal Tower의 상태를 변경한다.
        //190624 미사용 EXTND, // Arm 뻗음
        //190624 미사용 ARMFD, // Arm 접음

        NONE_,
    }

    public enum EmEfemRobotArm
    {
        Upper,
        Lower
    }
    public enum EmEfemTransfer
    {
        PICK,
        PLACE,
    }

    public enum EmEfemEvent
    {
        CASSETTE_DETECTED,
        POD_REMOVED,
        LOAD_BUTTON_PUSHED,
        UNLOAD_BUTTON_PUSHED,
    }
    
    public enum EmEfemLampType
    {
        CONTROL_MODE = 0,
        LOAD_LAMP,
        UNLOAD_LAMP,
        AUTO_LAMP,
        MANUAL_LAMP,
        RESERVE_LAMP,
    }

    public enum EmEfemTowerLampState
    {
        None=0,
        Red=1,
        Yellow=2,
        Green=3,
    }

    public enum EmEfemLampBuzzerState
    {
        DISABLE = 0,
        ENABLE,

        OFF = 0,
        ON,
        BLINK,
        KEEP,
    }
    public enum EmEfemAlignerMode
    {
        Manual, // 상위 명령 불가
        Auto
    }
    public enum EmEfemAlignerStatus
    {
        READY,
        RUNNING,
        ERROR,
    }

    public enum EmEfemSafetyPLCState
    {
        /// <summary>
        /// Abnormal
        /// </summary>        
        NEED_RESET,        
        DEVICE_TROUBLE,
        NORMAL,
    }
    public enum EmEfemMappingInfo
    {
        Absence,
        Presence,
        Double,
        Cross,
        Unknown
    }
    public enum EmEfEmByte : byte
    {
        BOCofComplete = (byte)'#',
        BOCofAck = (byte)'$',
        BOCofLPMSG = (byte)'>',

        EOC = (byte)'\r' 
    }

    public enum EmEfemErrorModule
    {
        /// <summary>
        /// System
        /// </summary>
        E0,
        /// <summary>
        /// Robot
        /// </summary>
        E1,
        /// <summary>
        /// LoadPort
        /// </summary>
        E2,
        /// <summary>
        /// Aligner
        /// </summary>
        E3,
    }

    public enum EmSystemError
    {
        SERIAL_PORT_IS_FAILED_TO_OPEN = 001,
        THE_DEVICE_NET_Is_FAILED_TO_OPEN = 2,

        MAIN_MAPPING_MODULE_CDA_ALARM = 190,
        MAIN_MAPPING_MODULE_VACUUM_ALARM = 191,
    }

    public enum EmRobotError
    {
        NO_ERROR = 0,
        DURING_THE_ROBOT_DRIVEN_ANOTHER_EXECUTION_COMMANd_IS_SENT = 201,

        VACUUM_MAPPING_SPEED_HAS_NOT_BEEN_SET = 104,

        UNEXPECTED_ERROR = 999,
    }

    public enum EmLoadPortError
    {

    }

    public enum EmAlignerError
    {

    }
}
