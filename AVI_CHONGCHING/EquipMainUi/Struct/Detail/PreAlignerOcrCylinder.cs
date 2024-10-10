using Dit.Framework.PLC;
using EquipMainUi.Struct.BaseUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail
{
    public class PreAlignerOcrCylinder
    {
        public PlcAddr XB_UpComplete { get; set; }
        public PlcAddr XB_DownComplete { get; set; }
        public PlcAddr YB_UpCmd { get; set; }

        public PreAlignerOcrCylinder()
        {

        }

        public void Up()
        {
            YB_UpCmd.vBit = false;
            Logger.Log.AppendLine("Ocr Cylinder Up");
        }
        public void Down()
        {
            YB_UpCmd.vBit = true;
            Logger.Log.AppendLine("Ocr Cylinder Down");
        }
        public bool IsUp
        {
            get
            {
                return XB_UpComplete.vBit == true && XB_DownComplete.vBit == false && YB_UpCmd.vBit == false;
            }
        }
        public bool IsDown
        {
            get
            {
                return XB_UpComplete.vBit == false && XB_DownComplete.vBit == true && YB_UpCmd.vBit == true;
            }
        }
    }
}
