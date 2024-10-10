using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.ReportStruct
{
    public class HsmsPortInfo
    {
        public int LoadportNo { get; set; }
        public string CstID { get; set; }
        public bool IsCstExist { get; set; }
        public PortMode PortMode { get; set; }
    }
    public enum PortMode
    {
        LOAD_REQUEST = 1,
        LOAD_COMPLETE,
        UNLOAD_REQUEST,
        UNLOAD_COMPLETE,
    }

}
