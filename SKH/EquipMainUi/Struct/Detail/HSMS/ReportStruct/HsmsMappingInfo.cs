using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.ReportStruct
{
    public class HsmsMappingInfo
    {
        public string CstID { get; set; }
        public int LoadPortNum { get; set; }
        public string MappingData { get; set; }
        public int WaferCount {  get { return MappingData.Count(c => c != '0'); } }
    }
}
