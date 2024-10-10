using Dit.Framework.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct
{
    public class AlarmSolutionMgr
    {
        public readonly string PATH_SETTING = Path.Combine(GG.StartupPath, "AlarmSolution", "AlarmSolution.xml");
        private List<AlarmSolutionItem> AlarmList { get; set; }
        public List<AlarmSolutionItem> GetAlarmList { get { return AlarmList; } }
        public AlarmSolutionMgr()
        {
            AlarmList = new List<AlarmSolutionItem>();
        }
        public bool Modify(int index, string cause, string action, string memo)
        {
            AlarmList[index].Cause = cause;
            AlarmList[index].Action = action;
            AlarmList[index].Memo = memo;
            Save();

            return true;
        }

        private bool Save()
        {
            try
            {
                bool result = XmlFileManager<AlarmSolutionMgr>.TrySaveXml(PATH_SETTING, this);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Load()
        {
            try
            {
                if (File.Exists(PATH_SETTING))
                {
                    AlarmSolutionMgr n;
                    bool result = XmlFileManager<AlarmSolutionMgr>.TryLoadData(PATH_SETTING, out n);

                    if (result)
                    {
                        CopyFrom(n);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    string folderPath = Path.Combine(GG.StartupPath, "AlarmSolution");
                    DirectoryInfo di = new DirectoryInfo(folderPath);

                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                    File.Create(PATH_SETTING);

                    foreach (var alarm in AlarmMgr.Instance.HappenAlarms)
                    {
                        AlarmList.Add(new AlarmSolutionItem() { Index = (int)alarm.Key, Name = alarm.Value.ID.ToString(), Action = "^\n", Cause = "^\n", Memo ="^\n" });
                    }
                    Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void CopyFrom(AlarmSolutionMgr src)
        {
            this.AlarmList.Clear();
            foreach (var s in src.AlarmList)
                this.AlarmList.Add(s);
        }
    }

    public class AlarmSolutionItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Cause { get; set; }
        public string Action { get; set; }
        public string Memo { get; set; }

        public AlarmSolutionItem()
        {

        }
        public AlarmSolutionItem(int _index, string _name, string _cause, string _action, string _memo)
        {
            Index = _index;
            Name = _name;
            Cause = _cause;
            Action = _action;
            Memo = _memo;
        }
    }
}
