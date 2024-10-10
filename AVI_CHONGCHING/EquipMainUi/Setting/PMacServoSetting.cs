using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using Dit.Framework.Xml;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EziStep;

namespace EquipMainUi.Setting
{
    public class ServoPosiInfo : ICloneable
    {
        public int No;
        public string Name;
        public float Position;
        public float Speed;
        public float Accel;
        public bool IsSetting;

        public ServoMotorPmac Servo = null;
        public StepMotorEzi StepMotor = null;

        public ServoPosiInfo()
        {
            No = 0;
            Position = 0;
            Speed = 0;
            Accel = 0;
        }

        public object Clone()
        {
            return new ServoPosiInfo()
            {
                No = this.No,
                Name = this.Name,
                Position = this.Position,
                Speed = this.Speed,
                Accel = this.Accel,
                IsSetting = this.IsSetting
            };
        }
    }

    public class PMacServoSetting : ICloneable
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "ServoPositionInfo.ini");
        public static bool UseAutoBackup = true;
        public static string BackupFolderName = "AutoBackup";
        public static int BackupCount = 10;
        public string Section { get; set; }
        /**
         * [관리항목]
         * 원점 이동속도
         * 조그 속도
         * 정의된 위치/속도 * 7개
        */

        public ServoPosiInfo[] LstServoPosiInfo = new ServoPosiInfo[0];
        public int LoadRate;

        public PMacServoSetting()
        {
        }
        public bool Load()
        {
            IniReader ini = new IniReader(PATH_SETTING);
            for (int iPos = 0; iPos < LstServoPosiInfo.Length; iPos++)
            {
                ServoPosiInfo info = new ServoPosiInfo()
                {
                    No = ini.GetInteger(Section, string.Format("NO_{0}", iPos), 0),
                    Name = ini.GetStringTrim(Section, string.Format("NAME_{0}", iPos)),
                    Position = (float)ini.GetDouble(Section, string.Format("POSITION_{0}", iPos), 0),
                    Speed = (float)ini.GetDouble(Section, string.Format("SPEED_{0}", iPos), 0),
                    Accel = (float)ini.GetDouble(Section, string.Format("ACCEL_{0}", iPos), 0),
                };

                //if (LstServoPosiInfo[info.No].No == info.No && LstServoPosiInfo[info.No].Name == info.Name && LstServoPosiInfo[info.No].IsSetting == false)
                //if (LstServoPosiInfo[iPos].No == info.No && LstServoPosiInfo[info.No].IsSetting == false)
                {
                    LstServoPosiInfo[iPos].Position = info.Position;
                    LstServoPosiInfo[iPos].Speed = info.Speed;
                    LstServoPosiInfo[iPos].Accel = info.Accel;
                    LstServoPosiInfo[iPos].IsSetting = true;
                }

                if (string.IsNullOrEmpty(info.Name))
                    return false;
            }
            LoadRate = ini.GetInteger(Section, "LOADRATE", 0);
            return true;
        }
        public bool Save()
        {
            IniReader ini = new IniReader(PATH_SETTING);
            for (int iPos = 0; iPos < LstServoPosiInfo.Length; iPos++)
            {
                ini.SetInteger(Section, string.Format("NO_{0}", iPos), LstServoPosiInfo[iPos].No);
                ini.SetString(Section, string.Format("NAME_{0}", iPos), LstServoPosiInfo[iPos].Name.Replace(" ", "_"));
                ini.SetDouble(Section, string.Format("POSITION_{0}", iPos), LstServoPosiInfo[iPos].Position);
                ini.SetDouble(Section, string.Format("SPEED_{0}", iPos), LstServoPosiInfo[iPos].Speed);
                ini.SetDouble(Section, string.Format("ACCEL_{0}", iPos), LstServoPosiInfo[iPos].Accel);
            }
            ini.SetInteger(Section, "LOADRATE", LoadRate);
            return true;
        }
        public static void AutoBackup()
        {
            string dest;
            string src = PATH_SETTING;
            dest = Path.Combine(Path.GetDirectoryName(src), BackupFolderName,
                string.Format("{0}({1}_Saved){2}", Path.GetFileNameWithoutExtension(src), DateTime.Now.ToString("yyyyMMdd_HH-mm-ss"), Path.GetExtension(src)));
            if (Directory.Exists(Path.GetDirectoryName(dest)) == false)
                Directory.CreateDirectory(Path.GetDirectoryName(dest));
            if (File.Exists(src) == true)
            {
                File.Copy(src, dest, true);
            }
            BaseSetting.RemoveOldBackup(dest, Path.GetFileNameWithoutExtension(src), BackupCount);         
        }

        public object Clone()
        {
            PMacServoSetting s = new PMacServoSetting();
            s.Section = this.Section;
            s.LstServoPosiInfo = (ServoPosiInfo[])this.LstServoPosiInfo.Clone();
            for (int i = 0; i < s.LstServoPosiInfo.Count(); ++i)
            {
                s.LstServoPosiInfo[i].Servo = this.LstServoPosiInfo[i].Servo;
            }
            s.LoadRate = this.LoadRate;

            return s;
        }
    }
}
