using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.ReportStruct
{    
    public class HsmsAlarmInfo
    {
        public bool IsSet { get; set; }
        public EM_AL_LST ID { get; set; }
        /// <summary>
        /// length 50
        /// </summary>
        public string Desc { get; set; }
    }
}
