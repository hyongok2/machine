using EquipMainUi.Struct;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.PreAligner.Recipe
{
    public static class PreAlignerRecipeDataMgr
    {
        private static MongoCollection<PreAlignerRecipe> _recipes;

        public static bool Initialize(Equipment equip)
        {
            try
            {
                string connString = "mongodb://localhost";
                MongoClient cli = new MongoClient(connString);

                var dbSKHynix = cli.GetServer().GetDatabase("SKHynix");

                _recipes = dbSKHynix.GetCollection<PreAlignerRecipe>("PreAlignerRecipes");

                _recipes.CreateIndex(
                    IndexKeys<PreAlignerRecipe>.Ascending(c => c.Name),
                    IndexOptions.SetSparse(true).SetUnique(true));

                return true;
            }
            catch (Exception)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0730_DATABASE_INITIAL_FAIL);
                return false;
            }

        }

        #region DB Access
        public static bool Insert(PreAlignerRecipe rcp)
        {
            try
            {
                _recipes.Insert(rcp);

                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }
        public static bool Insert(string name)
        {
            return Insert(new PreAlignerRecipe() { Name = name });
        }
        public static bool Update(PreAlignerRecipe cst)
        {
            try
            {
                _recipes.Save(cst);
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }
        public static bool Delete(string name)
        {
            try
            {
                IMongoQuery query = Query<PreAlignerRecipe>.EQ(c => c.Name, name);
                _recipes.Remove(query);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<PreAlignerRecipe> GetRecipes()
        {
            var cursor = _recipes.FindAll();
            List<PreAlignerRecipe> list = cursor.ToList<PreAlignerRecipe>();

            return list;
        }
        public static bool IsExist(string name)
        {
            return GetRecipe(name) != null;
        }
        public static void InitialDefault()
        {
            var cursor = _recipes.FindAll();
            List<PreAlignerRecipe> list = cursor.ToList<PreAlignerRecipe>();

            foreach (var pre in list)
            {
                pre.UseInspect = true;
                pre.InspectMargin = 3;
                pre.InspectFilterArea = 10;
                pre.InspectFilterRatio = 0.5;

                _recipes.Save(pre);
            }

        }
        public static PreAlignerRecipe GetRecipe(string name)
        {
            if (name == string.Empty)
                return new PreAlignerRecipe();
            var query = Query.And(
                Query<PreAlignerRecipe>.EQ(w => w.Name, name)
                );

            PreAlignerRecipe result = _recipes.FindOne(query);

            return result;
        }
        #endregion
    }
}
