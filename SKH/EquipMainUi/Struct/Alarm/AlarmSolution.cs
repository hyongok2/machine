using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;

namespace EquipMainUi.Struct
{
    /// <summary>
    /// 파일이 없는 경우, 빈 경우 예외처리
    /// date 180201
    /// 
    /// PC제어프로그램 사용
    /// 1. 폴더경로설정
    /// 2. 알람조치사항을 모두 읽어서 리스트로 만듦 (Initialize)
    /// 3. 조치사항 확인 시 리스트에서 얻어서 UI 표기 (GetAlarmSolution)
    /// date 170614
    /// </summary>
    public class AlarmSolution
    {
        public static string BasePath = Path.Combine(GG.StartupPath, "Setting", "AlarmSolutions");
        private string _fileName = string.Empty;

        #region Field
        public int Number { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public string Solution { get; set; }
        #endregion
        public AlarmSolution()
        {
            Number = -1;
            Name = "NONE";
            Cause = "NONE";
            Solution = "NONE";
        }
        public AlarmSolution(int alarmNumber)
        {
            Number = alarmNumber;
            Name = "NONE";
            Cause = "NONE";
            Solution = "NONE";
            _fileName = Number.ToString("D4") + ".xml";
        }

        public void Initialize(int alarmNumber)
        {
            Number = alarmNumber;
            Name = "NONE";
            Cause = "NONE";
            Solution = "NONE";
            _fileName = Number.ToString("D4") + ".xml";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">null시 기본경로</param>
        /// <returns></returns>
        public bool Save(string path = null)
        {
            bool retVal = true;
            string fullPath = string.Empty;
            if (null != path)
                BasePath = path;
            fullPath = Path.Combine(BasePath, _fileName);
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);

            if (File.Exists(fullPath) == false)
                return false;

            XmlSerializer ser = null;
            TextWriter writer = null;
            try
            {
                TransformNewLine(Cause);
                TransformNewLine(Solution);
                ser = new XmlSerializer(typeof(AlarmSolution));
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Truncate, FileAccess.Write))
                {
                    writer = new StreamWriter(fileStream, Encoding.UTF8);
                    ser.Serialize(writer, this);
                }
            }
            catch (System.Exception ex)
            {
                if (writer != null)
                    writer.Close();
                Debug.WriteLine("{0}", ex.Message);
                retVal = false;
            }
            return retVal;
        }

        private void TransformNewLine(string str)
        {
            str = str.Replace("\r\n", "\n"); //UTF-8 포맷 개행문자
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">null시 기본경로</param>
        /// <returns></returns>
        public bool Load(string path = null)
        {
            bool retVal = true;
            string fullPath = string.Empty;
            if (null != path)
                BasePath = path;
            fullPath = Path.Combine(BasePath, _fileName);
            if (File.Exists(fullPath) == false)
                return false;
            if ((new FileInfo(fullPath)).Length == 0)            
                return false;            

            AlarmSolution temp = null;
            XmlSerializer ser = null;
            TextReader reader = null;
            try
            {
                temp = new AlarmSolution(this.Number);
                ser = new XmlSerializer(typeof(AlarmSolution));
                using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    reader = new StreamReader(fileStream, Encoding.UTF8);
                    temp = ser.Deserialize(reader) as AlarmSolution;
                    Name = temp.Name;
                    Cause = temp.Cause;
                    Solution = temp.Solution;
                }
            }
            catch (System.Exception ex)
            {
                if (reader != null)
                    reader.Close();
                Debug.WriteLine("{0}", ex.Message);
                retVal = false;
            }
            return retVal;          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">null시 기본경로</param>
        /// <returns></returns>
        public bool CreateNewFile(string path = null)
        {
            bool retVal = true;
            string fullPath = string.Empty;
            if (null != path)
                BasePath = path;
            fullPath = Path.Combine(BasePath, _fileName);

            XmlSerializer ser = null;
            TextWriter writer = null;
            
            TransformNewLine(Cause);
            TransformNewLine(Solution);
            ser = new XmlSerializer(typeof(AlarmSolution));
            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    writer = new StreamWriter(fileStream, Encoding.UTF8);
                    ser.Serialize(writer, this);
                }
            }
            catch (Exception)
            {
                
            }
            

            return retVal;
        }
    }
}
