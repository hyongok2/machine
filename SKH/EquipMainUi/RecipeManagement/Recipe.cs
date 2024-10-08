using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.ComponentModel;

namespace EquipMainUi.RecipeManagement
{
    public class Recipe : ICloneable
    {
        [Browsable(false)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Desc { get; set; }

        public bool Update()
        {
            return RecipeDataMgr.Update(this);
        }

        public object Clone()
        {
            return new Recipe()
            {
                Name = this.Name,
                Model = this.Model,
                Desc = this.Desc
            };
        }
    }
}
