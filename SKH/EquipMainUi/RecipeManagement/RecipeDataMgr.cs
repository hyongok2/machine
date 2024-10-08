using MongoDB.Driver;
using MongoDB.Driver.Builders;
using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipMainUi.Log;

namespace EquipMainUi.RecipeManagement
{
    public static class RecipeDataMgr
    {
        public const string CurLPM1Recipe = "CurRecipe1"; // Recipe Class의 Name을 해당 설정값으로 하고 Desc에 백업할 Recipe명을 저장하여 백업한다.
        public const string CurLPM2Recipe = "CurRecipe2"; // Recipe Class의 Name을 해당 설정값으로 하고 Desc에 백업할 Recipe명을 저장하여 백업한다.

        private static MongoCollection<Recipe> _recipes;

        public static bool Initialize(Equipment equip)
        {
            try
            {
                string connString = "mongodb://localhost";
                MongoClient cli = new MongoClient(connString);

                var dbSKHynix = cli.GetServer().GetDatabase("SKHynix_Wuxi");

                _recipes = dbSKHynix.GetCollection<Recipe>("Recipes");                

                
                _recipes.CreateIndex(
                    IndexKeys<Recipe>.Ascending(c => c.Name),
                    IndexOptions.SetSparse(true).SetUnique(true));

                Recipe cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM1Recipe);
                if (cur == null)
                {
                    RecipeDataMgr.Insert(RecipeDataMgr.CurLPM1Recipe);
                    cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM1Recipe);
                }

                cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM2Recipe);
                if (cur == null)
                {
                    RecipeDataMgr.Insert(RecipeDataMgr.CurLPM2Recipe);
                    cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM2Recipe);
                }

                return true;
            }
            catch (Exception ex)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0730_DATABASE_INITIAL_FAIL);
                Logger.ExceptionLog.AppendLine(ex.Message + "\n{0}", EquipStatusDump.CallStackLog());
                return false;
            }

        }

        #region DB Access
        public static bool Insert(Recipe rcp)
        {
            try
            {
                _recipes.Insert(rcp);
                Logger.TransferDataLog.AppendLine("[Insert Recipe] Name : {0}  Model : {1} Desc : {2}", rcp.Name, rcp.Model, rcp.Desc);

                return true;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(ex.Message + "\n{0}", EquipStatusDump.CallStackLog());
                string str = ex.Message;
                return false;
            }
        }
        public static bool Insert(string name)
        {
            return Insert(new Recipe() { Name = name });
        }
        public static bool Update(Recipe cst)
        {
            try
            {
                _recipes.Save(cst);
                Logger.TransferDataLog.AppendLine("[Modify Recipe] Name : {0}  Model : {1} Desc : {2}", cst.Name, cst.Model, cst.Desc);
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                Logger.ExceptionLog.AppendLine(ex.Message + "\n{0}", EquipStatusDump.CallStackLog());
                return false;
            }
        }
        public static bool Delete(string name)
        {
            try
            {
                IMongoQuery query = Query<Recipe>.EQ(c => c.Name, name);
                _recipes.Remove(query);
                Logger.TransferDataLog.AppendLine("[Delete Recipe] Name : {0}  ", name);
                return true;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(ex.Message + "\n{0}", EquipStatusDump.CallStackLog());
                return false;
            }
        }
        public static List<Recipe> GetRecipes()
        {
            var cursor = _recipes.FindAll();
            List<Recipe> list = cursor.ToList<Recipe>();

            return list;
        }
        public static bool IsExist(string name)
        {
            return GetRecipe(name) != null;            
        }
        public static Recipe GetRecipe(string name)
        {
            if (name == string.Empty)
                return new Recipe();
            var query = Query.And(
                Query<Recipe>.EQ(w => w.Name, name)
                );

            Recipe result = _recipes.FindOne(query);

            return result;
        }

        public static string GetCurRecipeName(int idx)
        {
            Recipe cur = idx == 0 ? GetRecipe(CurLPM1Recipe) : GetRecipe(CurLPM2Recipe);
            if (cur == null)
                return string.Empty;
            return cur.Desc;
        }
        #endregion
    }
}
