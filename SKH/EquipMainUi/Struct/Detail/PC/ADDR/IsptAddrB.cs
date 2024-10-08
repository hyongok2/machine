using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;


namespace EquipMainUi.Struct.Detail.PC
{
    public class IsptAddrB
    {
        #region CTRL -> INSP
        //STATE
        public static PlcAddr YB_ControlAlive                       /*   */ = new PlcAddr(PlcMemType.S, 0, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_GlassIn                            /*   */ = new PlcAddr(PlcMemType.S, 0, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_AutoMode                           /*   */ = new PlcAddr(PlcMemType.S, 0, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewMode                         /*   */ = new PlcAddr(PlcMemType.S, 0, 3, 1, PlcValueType.BIT);        
        public static PlcAddr YB_AutoRecipeChange                   /*   */ = new PlcAddr(PlcMemType.S, 0, 4, 1, PlcValueType.BIT);
        public static PlcAddr YB_CimMode                            /*   */ = new PlcAddr(PlcMemType.S, 0, 5, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewManualMode                            /*   */ = new PlcAddr(PlcMemType.S, 0, 6, 1, PlcValueType.BIT);

        public static PlcAddr YB_MotorInterlockOffState                /*   */ = new PlcAddr(PlcMemType.S, 1, 3, 1, PlcValueType.BIT);
        public static PlcAddr YB_TTTMMode                           /*   */ = new PlcAddr(PlcMemType.S, 1, 4, 1, PlcValueType.BIT);
        public static PlcAddr YB_CtrlLoginState                     /*   */ = new PlcAddr(PlcMemType.S, 2, 1, 1, PlcValueType.BIT); //jys::todo 로그인기능 추가 시.

        //REQ
        public static PlcAddr YB_Loading				            /*   */ = new PlcAddr(PlcMemType.S, 20, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AlignStart                         /*   */ = new PlcAddr(PlcMemType.S, 20, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_InspStart                          /*   */ = new PlcAddr(PlcMemType.S, 20, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_InspEnd                            /*   */ = new PlcAddr(PlcMemType.S, 20, 3, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewStart                        /*   */ = new PlcAddr(PlcMemType.S, 20, 4, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewEnd                          /*   */ = new PlcAddr(PlcMemType.S, 20, 5, 1, PlcValueType.BIT);
        public static PlcAddr YB_Unloading                          /*   */ = new PlcAddr(PlcMemType.S, 20, 6, 1, PlcValueType.BIT);
        public static PlcAddr YB_TTTMStart                          /*   */ = new PlcAddr(PlcMemType.S, 20, 7, 1, PlcValueType.BIT);
        public static PlcAddr YB_StatsToolStart                     /*   */ = new PlcAddr(PlcMemType.S, 21, 0, 1, PlcValueType.BIT);


        //ACK
        public static PlcAddr YB_LoadingCompleteAck                 /*   */ = new PlcAddr(PlcMemType.S, 40, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AlignCompleteAck                   /*   */ = new PlcAddr(PlcMemType.S, 40, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_InspCompleteAck                    /*   */ = new PlcAddr(PlcMemType.S, 40, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewCompleteAck                  /*   */ = new PlcAddr(PlcMemType.S, 40, 3, 1, PlcValueType.BIT);
        public static PlcAddr YB_UnloadingCompleteAck               /*   */ = new PlcAddr(PlcMemType.S, 40, 4, 1, PlcValueType.BIT);
        public static PlcAddr YB_TTTMCompleteAck                    /*   */ = new PlcAddr(PlcMemType.S, 40, 5, 1, PlcValueType.BIT);

        #endregion
        #region INSP -> CTRL        
        //STATE
        public static PlcAddr XB_ServerAlive    				    /*   */ = new PlcAddr(PlcMemType.S, 5000, 0, 1, PlcValueType.BIT);

        public static PlcAddr XB_ServerLoginState    				/*   */ = new PlcAddr(PlcMemType.S, 5000, 3, 1, PlcValueType.BIT);//jys::todo 로그인기능 추가 시.

        public static PlcAddr XB_PPIDErrAlarm				        /*   */ = new PlcAddr(PlcMemType.S, 5002, 0, 1, PlcValueType.BIT);

        public static PlcAddr XB_ModulePcAlarm				        /*   */ = new PlcAddr(PlcMemType.S, 5002, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_ServerOverflowAlarm			    /*   */ = new PlcAddr(PlcMemType.S, 5002, 3, 1, PlcValueType.BIT);
        public static PlcAddr XB_InspectorOverflowAlarm			    /*   */ = new PlcAddr(PlcMemType.S, 5002, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_AlignAlarm				            /*   */ = new PlcAddr(PlcMemType.S, 5002, 5, 1, PlcValueType.BIT);
        public static PlcAddr XB_LightValueAlarm				    /*   */ = new PlcAddr(PlcMemType.S, 5002, 6, 1, PlcValueType.BIT);

        public static PlcAddr XB_DieAlignWarningAlarm               /*   */ = new PlcAddr(PlcMemType.S, 5003, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_PtOverSizeAlarm			        /*   */ = new PlcAddr(PlcMemType.S, 5003, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_WaferNGWarningAlarm			    /*   */ = new PlcAddr(PlcMemType.S, 5003, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewScheduleFailAlarm			/*   */ = new PlcAddr(PlcMemType.S, 5003, 3, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewSequenceFailAlarm			/*   */ = new PlcAddr(PlcMemType.S, 5003, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewProcessFailAlarm			    /*   */ = new PlcAddr(PlcMemType.S, 5003, 5, 1, PlcValueType.BIT);
        public static PlcAddr XB_AfmErrorAlarm                      /*   */ = new PlcAddr(PlcMemType.S, 5003, 6, 1, PlcValueType.BIT);
        public static PlcAddr XB_GrabFailAlarm                     /*   */ = new PlcAddr(PlcMemType.S, 5003, 7, 1, PlcValueType.BIT);

        public static PlcAddr XB_FtpIsNotConnectAlarm               /*   */ = new PlcAddr(PlcMemType.S, 5004, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_FtpUploadFailAlarm                 /*   */ = new PlcAddr(PlcMemType.S, 5004, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_DieAlignFailAlarm                  /*   */ = new PlcAddr(PlcMemType.S, 5004, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_NoRecipe                           /*   */ = new PlcAddr(PlcMemType.S, 5004, 3, 1, PlcValueType.BIT);

        public static PlcAddr XB_TTTMLightAlarm                     /*   */ = new PlcAddr(PlcMemType.S, 5004, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_TTTMFocusAlarm                     /*   */ = new PlcAddr(PlcMemType.S, 5004, 5, 1, PlcValueType.BIT);
        public static PlcAddr XB_TTTMMatchAlarm                     /*   */ = new PlcAddr(PlcMemType.S, 5004, 6, 1, PlcValueType.BIT);
        public static PlcAddr XB_TTTMFailAlarm                      /*   */ = new PlcAddr(PlcMemType.S, 5004, 7, 1, PlcValueType.BIT);

        public static PlcAddr XB_RecipeReadFail                     /*   */ = new PlcAddr(PlcMemType.S, 5005, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_RecipeRoiError                     /*   */ = new PlcAddr(PlcMemType.S, 5005, 1, 1, PlcValueType.BIT);

        //ACK
        public static PlcAddr XB_LoadingAck				            /*   */ = new PlcAddr(PlcMemType.S, 5020, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_AlignStartAck				        /*   */ = new PlcAddr(PlcMemType.S, 5020, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_InspStartAck                       /*   */ = new PlcAddr(PlcMemType.S, 5020, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_InspEndAck                         /*   */ = new PlcAddr(PlcMemType.S, 5020, 3, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewStartAck                     /*   */ = new PlcAddr(PlcMemType.S, 5020, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewEndAck                       /*   */ = new PlcAddr(PlcMemType.S, 5020, 5, 1, PlcValueType.BIT);
        public static PlcAddr XB_UnloadingAck                       /*   */ = new PlcAddr(PlcMemType.S, 5020, 6, 1, PlcValueType.BIT);
        public static PlcAddr XB_TTTMStartAck                       /*   */ = new PlcAddr(PlcMemType.S, 5020, 7, 1, PlcValueType.BIT);
        public static PlcAddr XB_StatsToolStartAck                  /*   */ = new PlcAddr(PlcMemType.S, 5021, 0, 1, PlcValueType.BIT);


        //REQ
        public static PlcAddr XB_LoadingComplete                    /*   */ = new PlcAddr(PlcMemType.S, 5040, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_AlignComplete                      /*   */ = new PlcAddr(PlcMemType.S, 5040, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_InspComplete                       /*   */ = new PlcAddr(PlcMemType.S, 5040, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewComplete                     /*   */ = new PlcAddr(PlcMemType.S, 5040, 3, 1, PlcValueType.BIT);        
        public static PlcAddr XB_UnloadingComplete                  /*   */ = new PlcAddr(PlcMemType.S, 5040, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_TTTMComplete                       /*   */ = new PlcAddr(PlcMemType.S, 5040, 5, 1, PlcValueType.BIT);

        #endregion
        static IsptAddrB()
        {
        }

        public static void Initialize(IVirtualMem plc)
        {
            YB_ControlAlive.PLC = plc;
            YB_GlassIn.PLC = plc;
            YB_AutoMode.PLC = plc;
            YB_ReviewMode.PLC = plc;
            YB_AutoRecipeChange.PLC = plc;
            YB_CimMode.PLC = plc;
            YB_ReviewManualMode.PLC = plc;
            YB_MotorInterlockOffState.PLC = plc;
            YB_CtrlLoginState.PLC = plc;
            YB_Loading.PLC = plc;
            YB_AlignStart.PLC = plc;
            YB_InspStart.PLC = plc;
            YB_InspEnd.PLC = plc;
            YB_ReviewStart.PLC = plc;
            YB_ReviewEnd.PLC = plc;
            YB_Unloading.PLC = plc;
            YB_LoadingCompleteAck.PLC = plc;
            YB_AlignCompleteAck.PLC = plc;
            YB_InspCompleteAck.PLC = plc;
            YB_ReviewCompleteAck.PLC = plc;
            YB_UnloadingCompleteAck.PLC = plc;
            XB_ServerAlive.PLC = plc;
            XB_ServerLoginState.PLC = plc;
            XB_PPIDErrAlarm.PLC = plc;
            XB_ModulePcAlarm.PLC = plc;
            XB_ServerOverflowAlarm.PLC = plc;
            XB_InspectorOverflowAlarm.PLC = plc;
            XB_AlignAlarm.PLC = plc;
            XB_LightValueAlarm.PLC = plc;
            XB_DieAlignWarningAlarm.PLC = plc;
            XB_PtOverSizeAlarm.PLC = plc;
            XB_WaferNGWarningAlarm.PLC = plc;
            XB_ReviewScheduleFailAlarm.PLC = plc;
            XB_ReviewSequenceFailAlarm.PLC = plc;
            XB_ReviewProcessFailAlarm.PLC = plc;
            XB_AfmErrorAlarm.PLC = plc;
            XB_GrabFailAlarm.PLC = plc;
            XB_FtpIsNotConnectAlarm.PLC = plc;
            XB_FtpUploadFailAlarm.PLC = plc;
            XB_DieAlignFailAlarm.PLC = plc;
            XB_NoRecipe.PLC = plc;
            XB_LoadingAck.PLC = plc;
            XB_AlignStartAck.PLC = plc;
            XB_InspStartAck.PLC = plc;
            XB_InspEndAck.PLC = plc;
            XB_ReviewStartAck.PLC = plc;
            XB_ReviewEndAck.PLC = plc;
            XB_UnloadingAck.PLC = plc;
            XB_LoadingComplete.PLC = plc;
            XB_AlignComplete.PLC = plc;
            XB_InspComplete.PLC = plc;
            XB_ReviewComplete.PLC = plc;
            XB_UnloadingComplete.PLC = plc;
            YB_TTTMMode.PLC = plc;
            YB_TTTMStart.PLC = plc;
            YB_StatsToolStart.PLC = plc;
            YB_TTTMCompleteAck.PLC = plc;
            XB_TTTMComplete.PLC = plc;
            XB_TTTMStartAck.PLC = plc;
            XB_StatsToolStartAck.PLC = plc;
            XB_TTTMLightAlarm.PLC = plc;
            XB_TTTMFocusAlarm.PLC = plc;
            XB_TTTMMatchAlarm.PLC = plc;
            XB_TTTMFailAlarm.PLC = plc;
            XB_RecipeReadFail.PLC = plc;
            XB_RecipeRoiError.PLC = plc;
        }
    }
}