using Dit.Framework.Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.FileInterface
{
    public class WaferMapDataSetting : BaseSetting
    {
        [IniAttribute("INFO", "WaferId", "-")] public string WaferId { get; set; }
        [IniAttribute("INFO", "LotId", "-")] public string LotId { get; set; }
        [IniAttribute("INFO", "IdType", "-")] public string IdType { get; set; }
        [IniAttribute("INFO", "ProductCode", "-")] public string ProductCode { get; set; }
        [IniAttribute("INFO", "Columns", "0")] public string Columns { get; set; }
        [IniAttribute("INFO", "Row", "0")] public string Row { get; set; }
        [IniAttribute("INFO", "BinGoodQty", "-")] public string BinGoodQty { get; set; }
        [IniAttribute("INFO", "FlatZone", "-")] public string FlatZone { get; set; }

        [IniAttribute("ROW", "-", "-")] public string WaferMap1 { get; set; }
        [IniAttribute("ROW", "-", "-")] public string WaferMap2 { get; set; }
        [IniAttribute("ROW", "-", "-")] public string WaferMap3 { get; set; }
        [IniAttribute("ROW", "-", "-")] public string WaferMap4 { get; set; }
        [IniAttribute("ROW", "-", "-")] public string WaferMap5 { get; set; }
        [IniAttribute("ROW", "-", "-")] public string WaferMap6 { get; set; }
        public override bool Load(string path)
        {
            return base.Load(path);
        }
    }
}
