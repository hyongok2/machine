using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.HSMS.ReportStruct
{
    public class HsmsRecipeInfo
    {
        public RecipeMode RecipeMode { get; set; }
        public string RecipeID { get; set; }
        public Ack IsOK { get; set; }
    }
    public enum RecipeMode
    {
        RECIPE_SELECT = 1,
        PORT_1_RECIPE_SELECT,
        PORT_2_RECIPE_SELECT
    }
    public enum Ack
    {
        OK,
        OVERLAP,
        NACK
    }
}
