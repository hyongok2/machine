using Dit.Framework.Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.FileInterface
{
    public class ECIDDataSetting : BaseSetting
    {
        [IniAttribute("1", "-", "-")] public ECIDDataSet ECID1 { get; set; }
        [IniAttribute("2", "-", "-")] public ECIDDataSet ECID2 { get; set; }
        [IniAttribute("3", "-", "-")] public ECIDDataSet ECID3 { get; set; }
        [IniAttribute("4", "-", "-")] public ECIDDataSet ECID4 { get; set; }
        [IniAttribute("5", "-", "-")] public ECIDDataSet ECID5 { get; set; }
        [IniAttribute("6", "-", "-")] public ECIDDataSet ECID6 { get; set; }
        [IniAttribute("7", "-", "-")] public ECIDDataSet ECID7 { get; set; }
        [IniAttribute("8", "-", "-")] public ECIDDataSet ECID8 { get; set; }
        [IniAttribute("9", "-", "-")] public ECIDDataSet ECID9 { get; set; }
        [IniAttribute("10", "-", "-")] public ECIDDataSet ECID10 { get; set; }
        [IniAttribute("11", "-", "-")] public ECIDDataSet ECID11 { get; set; }
        [IniAttribute("12", "-", "-")] public ECIDDataSet ECID12 { get; set; }
        [IniAttribute("13", "-", "-")] public ECIDDataSet ECID13 { get; set; }
        [IniAttribute("14", "-", "-")] public ECIDDataSet ECID14 { get; set; }
        [IniAttribute("15", "-", "-")] public ECIDDataSet ECID15 { get; set; }
        [IniAttribute("16", "-", "-")] public ECIDDataSet ECID16 { get; set; }

        public class ECIDDataSet : IniDataSet
        {
            [IniAttribute("-", "ECNAME", "\\")]
            public string Name { get; set; }
            [IniAttribute("-", "ECDEF", 0)]
            public double Default { get; set; }
            [IniAttribute("-", "ECMAX", 0)]
            public double Max { get; set; }
            [IniAttribute("-", "ECMIN", 0)]
            public double Min { get; set; }
            [IniAttribute("-", "UNIT", 0)]
            public string Unit { get; set; }
        }

        public ECIDDataSetting()
        {
            ECID1 = new ECIDDataSet();
            ECID2 = new ECIDDataSet();
            ECID3 = new ECIDDataSet();
            ECID4 = new ECIDDataSet();
            ECID5 = new ECIDDataSet();
            ECID6 = new ECIDDataSet();
            ECID7 = new ECIDDataSet();
            ECID8 = new ECIDDataSet();
            ECID9 = new ECIDDataSet();
            ECID10 = new ECIDDataSet();
            ECID11 = new ECIDDataSet();
            ECID12 = new ECIDDataSet();
            ECID13 = new ECIDDataSet();
            ECID14 = new ECIDDataSet();
            ECID15 = new ECIDDataSet();
            ECID16 = new ECIDDataSet();
            UseAutoBackup = false;
        }

        /**
        *@param : path - null : 기본 경로
        *            not null : 경로 변경
        *               */
        public override bool Save(string path)
        {
            if (!Directory.Exists(Path.GetPathRoot(path)))
                Directory.CreateDirectory(path.Remove(path.LastIndexOf('\\')));

            return base.Save(path);
        }

        /**
         *@param : path - null : 기본 경로
         *            not null : 경로 변경
         *               */
        public override bool Load(string path)
        {
            return base.Load(path);
        }
        public void SetData(int idx, string name, double def, double min, double max, string unit)
        {
            ECIDDataSet to = GetEcid(idx);
            if (to == null) throw new Exception("ECID INDEX OVER");
            to.Name = name;
            to.Default = double.Parse(def.ToString("N2"));
            to.Max = max;
            to.Min = min;
            to.Unit = unit;
        }
        public ECIDDataSet GetEcid(int idx)
        {
            idx = idx - 1;
            ECIDDataSet[] lst = new ECIDDataSet[]
            {
                ECID1 ,
                ECID2 ,
                ECID3 ,
                ECID4 ,
                ECID5 ,
                ECID6 ,
                ECID7 ,
                ECID8 ,
                ECID9 ,
                ECID10,
                ECID11,
                ECID12,
                ECID13,
                ECID14,
                ECID15,
                ECID16,
            };
            if (idx < lst.Length)
                return lst[idx];
            else
                return null;
        }
    }
}
