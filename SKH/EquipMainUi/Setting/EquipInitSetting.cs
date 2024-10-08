using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;
using Dit.Framework.PLC;
using Dit.Framework.PMAC;
using System.Net;

namespace EquipMainUi.Setting
{
    public class EquipInitSetting : BaseSetting
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "_EquipInitSetting.ini");

        #region Info
        [IniAttribute("Info", "EquipNo", 1)]
        public int EquipNo { get; set; }
        [IniAttribute("Setting", "EquipmentID", 0)]
        public string EquipmentID { get; set; }      
        [IniAttribute("Info", "Version", "1701011430")]
        public string SwVersion { get; set; }
        #endregion

        #region Setting
        public string ControlLogBasePath { get; set; }
        [IniAttribute("Setting", "ErrorLogPath", "D:\\DitCtrl\\Exec\\Ctrl\\Log")]
        public string ErrorLogBasePath { get; set; }
        [IniAttribute("Setting", "UseFreeLoginAcces", true)]
        public bool UseFreeLoginAccess { get; set; }
        [IniAttribute("Setting", "IsLeftSideEquip", true)]
        public bool IsLeftSideEquip { get; set; }
        [IniAttribute("Setting", "UnloadingThetaWaitTime", 500)]
        public int UnloadingThetaWaitTime { get; set; }
        [IniAttribute("Setting", "UseLiftpinPhysicalSensor", false)]
        public bool UseLiftpinPhysicalSensor { get; set; }
        [IniAttribute("Setting", "UseServerSwZPos", false)]
        public bool UseServerSwZPos { get; set; }          
        #endregion

        #region Communication
        [IniAttribute("Communication", "PmacIP", "192.168.1.210")]
        public string PmacIP { get; set; }
        [IniAttribute("Communication", "PmacPort", 1500)]
        public int PmacPort { get; set; }
        [IniAttribute("Communication", "EfemIP", "192.168.0.200")]
        public string EfemIP { get; set; }
        [IniAttribute("Communication", "EfemPort", 1771)]
        public int EfemPort { get; set; }
        [IniAttribute("Communication", "NumFFU", 3)]
        public int NumFFU { get; set; }
        [IniAttribute("Communication", "EFUPort", "COM11")]
        public string EFUPort { get; set; }
        [IniAttribute("Communication", "OcrIP", "192.168.1.201")]
        public string OcrIP { get; set; }
        [IniAttribute("Communication", "OcrPort", 2000)]
        public int OcrPort { get; set; }
        [IniAttribute("Communication", "BCR1Port", "COM4")]
        public string BCR1Port { get; set; }
        [IniAttribute("Communication", "BCR2Port", "COM5")]
        public string BCR2Port { get; set; }
        [IniAttribute("Communication", "RFR1Port", "COM6")]
        public string RFR1Port { get; set; }
        [IniAttribute("Communication", "RFR2Port", "COM7")]
        public string RFR2Port { get; set; }
        [IniAttribute("Communication", "LightControllerPort", "COM9")]
        public string LightControllerPort { get; set; }
        [IniAttribute("Communication", "EziStepMotorPort", "COM10")]
        public string EziStepMotorPort { get; set; }
        #endregion

        public EquipInitSetting()
        {
            EquipNo =0;
            EquipmentID = "";
            SwVersion = "1701011430";
            ControlLogBasePath = "D:\\DitCtrl\\Exec\\Ctrl\\Log";
            ErrorLogBasePath = "D:\\DitCtrl\\Exec\\Ctrl\\Log";
            UseFreeLoginAccess = false;
            IsLeftSideEquip = true;
            PmacIP = "192.168.1.210";
            PmacPort = 1500;
            OcrIP = "192.168.1.201";
            OcrPort = 2000;
            NumFFU = 3;
            EFUPort = "COM11";
            BCR1Port = "COM4";
            BCR2Port = "COM5";
            RFR1Port = "COM6";
            RFR2Port = "COM7";
            LightControllerPort = "COM9";
            EziStepMotorPort = "COM10";
            UseLiftpinPhysicalSensor = false;
            UseServerSwZPos = false;
        }
        /**
        *@param : path - null : 기본 경로s
        *            not null : 경로 변경
        *               */
        public override bool Save(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            if (!Directory.Exists(Path.GetDirectoryName(PATH_SETTING)))
                Directory.CreateDirectory(PATH_SETTING.Remove(PATH_SETTING.LastIndexOf('\\')));

            return base.Save(PATH_SETTING);
        }
        /**
         *@param : path - null : 기본 경로
         *            not null : 경로 변경
         *               */
        public override bool Load(string path = null)
        {
            bool ret;
            try
            {
                if (null != path)
                    PATH_SETTING = path;
                ret = base.Load(PATH_SETTING);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public bool CheckValueConsistency()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                uint ipaddress = PowerPmac.ToInt(PmacIP);
            }
            catch (Exception ex)
            {
                sb.AppendLine("Pmac IP의 형식이 잘 못 되었습니다");
            }

            IPAddress ipAddr;
            if (IPAddress.TryParse(OcrIP, out ipAddr) == false)
                sb.AppendLine("Ocr IP의 형식이 잘 못 되었습니다");

            if (NumFFU == 0)
                sb.AppendLine("NumFFU는 0보다 커야합니다");

            if (RFR1Port == string.Empty)
                sb.AppendLine("RF Reader1 Port 값이 비어있습니다");

            if (RFR2Port == string.Empty)
                sb.AppendLine("RF Reader2 Port 값이 비어있습니다");

            if (BCR1Port == string.Empty)
                sb.AppendLine("BCR 1 Port 값이 비어있습니다");

            if (BCR2Port == string.Empty)
                sb.AppendLine("BCR 2 Port 값이 비어있습니다");

            if (LightControllerPort == string.Empty)
                sb.AppendLine("LightControllerPort 값이 비어있습니다");

            if (EziStepMotorPort == string.Empty)
                sb.AppendLine("EziStepMotorPort 값이 비어있습니다");

            //if (Enum.GetNames(typeof(GG.EquipmentID)).FirstOrDefault(i => string.Compare(i, EquipmentID, true) == 0) == null)
            //{
            //    errMsg = "EquipmentID 값 이상! 다음 중 하나로 설정해야합니다\n";
            //    foreach (string str in Enum.GetNames(typeof(GG.EquipmentID)))
            //    {
            //        errMsg += str + "\n";
            //    }
            //}

            if (sb.Length != 0)
            {
                MessageBox.Show(string.Format("{0} {1} 확인하세요", sb.ToString(), PATH_SETTING));
                return false;
            }
            return true;
        }
    }
}

