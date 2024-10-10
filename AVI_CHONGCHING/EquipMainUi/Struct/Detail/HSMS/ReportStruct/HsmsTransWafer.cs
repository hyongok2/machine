using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.ReportStruct
{
    public class HsmsTransWafer
    {
        public WaferInfoKey WaferKey { get; set; }
        public EmEfemDBPort Port { get; set; }
    }
}
