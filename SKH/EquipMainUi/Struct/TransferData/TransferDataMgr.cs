using Dit.Framework.Xml;
using EquipMainUi.Log;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.Struct.Step;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EquipMainUi.Struct.TransferData
{
    public class BackupKey
    {
        public ObjectId Id { get; set; }
        public EmEfemDBPort Location;
        public string CstId;
        public int SlotNo;
    }

    public static class TransferDataMgr
    {
        static MongoCollection<CassetteInfo> cassettes;
        static MongoCollection<WaferInfo> wafers;
        static MongoCollection<BackupKey> baks;

        public static bool Initialize(Equipment equip)
        {
            try
            {
                string connString = "mongodb://localhost";
                MongoClient cli = new MongoClient(connString);
                
                var dbSKHynix = cli.GetServer().GetDatabase("SKHynix");

                cassettes = dbSKHynix.GetCollection<CassetteInfo>("Cassettes");
                wafers = dbSKHynix.GetCollection<WaferInfo>("Wafers");
                baks = dbSKHynix.GetCollection<BackupKey>("BackupKeys");

                //cassettes.EnsureIndex(new IndexKeysBuilder().Ascending("CstID"), IndexOptions.SetUnique(true));
                cassettes.CreateIndex(
                    IndexKeys<CassetteInfo>.Ascending(c => c.CstID),
                    IndexOptions.SetSparse(true).SetUnique(true));

                wafers.CreateIndex(
                    IndexKeys<WaferInfo>.Ascending(w => w.CstID).Descending(w => w.SlotNo),
                    IndexOptions.SetSparse(true).SetUnique(true));

                baks.CreateIndex(
                    IndexKeys<BackupKey>.Ascending(b => b.Location),
                    IndexOptions.SetSparse(true).SetUnique(true));

                return true;
            }
            catch (Exception)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0730_DATABASE_INITIAL_FAIL);
                return false;
            }

        }
        
        public static CassetteInfo LoadCassetteToXml(string path)
        {
            CassetteInfo cstinfo;
            XmlFileManager<CassetteInfo>.TryLoadData(path, out cstinfo);
            if (cstinfo != null)
                return cstinfo;
            else
                return null;
        }

        static TransferDataMgr()
        {
           
        }

        #region DB
        public static WaferInfo GetWafer(string cstId, int slot)
        {
            return GetWafer(new WaferInfoKey() { CstID = cstId, SlotNo = slot });
        }
        public static bool IsExistWafer(WaferInfoKey key)
        {
            return GetWafer(key) != null;
        }
        public static bool Shift(WaferInfoKey key, EmEfemDBPort targetPort)
        {
            try
            {
                if (targetPort == EmEfemDBPort.NONE)
                    return false;
                IMongoQuery query = Query.And(
                Query<BackupKey>.EQ(b => b.CstId, key.CstID),
                Query<BackupKey>.EQ(b => b.SlotNo, key.SlotNo)
                );

                var result = baks.FindOne(query);
                result.CstId = "";
                result.SlotNo = 0;

                baks.Save(result);

                query = Query<BackupKey>.EQ(b => b.Location, targetPort);
                result = baks.FindOne(query);
                result.CstId = key.CstID;
                result.SlotNo = key.SlotNo;

                baks.Save(result);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool LPMToRobotShift(WaferInfoKey key, EmEfemDBPort targetPort)
        {
            try
            {
                if (targetPort == EmEfemDBPort.NONE)
                    return false;
                IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, key.CstID),
                Query<WaferInfo>.EQ(w => w.SlotNo, key.SlotNo)
                );

                var result = wafers.FindOne(query);
                result.IsOut = true;
                result.OutputDate = DateTime.Now;
                result.IsComeBack = false;
                wafers.Save(result);

                IMongoQuery query2 = Query<BackupKey>.EQ(b => b.Location, targetPort);

                var orginBackup = baks.FindOne(query2);
                orginBackup.CstId = result.CstID;
                orginBackup.SlotNo = result.SlotNo;

                baks.Save(orginBackup);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool RobotToLPMShift(WaferInfoKey key, EmEfemDBPort targetPort)
        {
            try
            {
                if (targetPort == EmEfemDBPort.NONE)
                    return false;
                IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, key.CstID),
                Query<WaferInfo>.EQ(w => w.SlotNo, key.SlotNo)
                );

                var result = wafers.FindOne(query);
                result.IsComeBack = true;
                wafers.Save(result);

                DeleteBackUpKey(result.ToKey());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteBackUpKey(WaferInfoKey key)
        {
            try
            {
                IMongoQuery query = Query.And(
                Query<BackupKey>.EQ(b => b.CstId, key.CstID),
                Query<BackupKey>.EQ(b => b.SlotNo, key.SlotNo)
                );

                var result = baks.FindOne(query);
                result.CstId = "";
                result.SlotNo = 0;

                baks.Save(result);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static bool DeleteBackUpKey(CassetteInfoKey key)
        {
            try
            {
                IMongoQuery query = Query.And(
                Query<BackupKey>.EQ(b => b.CstId, key.ID),
                Query<BackupKey>.EQ(b => b.SlotNo, 0)
                );

                var result = baks.FindOne(query);
                result.CstId = "";
                result.SlotNo = 0;

                baks.Save(result);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool DeleteAllBackUpKey()
        {
            try
            {
                DeleteBackUpKey(GG.Equip.Efem.Robot.LowerWaferKey);
                DeleteBackUpKey(GG.Equip.Efem.Robot.UpperWaferKey);
                DeleteBackUpKey(GG.Equip.Efem.Aligner.LowerWaferKey);
                DeleteBackUpKey(GG.Equip.TransferUnit.LowerWaferKey);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void RecoveryData(WaferInfoKey lstClearKey, EmEfemDBPort lstClearPort)
        {
            IMongoQuery query = Query<BackupKey>.EQ(b => b.Location, lstClearPort);

            var result = baks.FindOne(query);
            result.CstId = lstClearKey.CstID;
            result.SlotNo = lstClearKey.SlotNo;

            baks.Save(result);
        }

        public static WaferInfo GetWafer(WaferInfoKey key)
        {
            if (key == null)
            {
                Logger.TransferDataLog.AppendLine("[GetWafer] key 값이 null이므로 return new Waferinfo()");
                return new WaferInfo();
            }
            else if(key.CstID == "")
            {
                return null;
            }
            Logger.TransferDataLog.AppendLine("[GetWafer] 요청 CSTID : {0}, SlotNo : {1}", key.CstID, key.SlotNo);

            var query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, key.CstID),
                Query<WaferInfo>.EQ(w => w.SlotNo, key.SlotNo)
                );

            WaferInfo result = wafers.FindOne(query);

            if(result == null)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Error, "[GetWafer] 결과 WaferInfo => null");
                var w = GetWafersIn(key.CstID);
                Logger.Log.AppendLine(LogLevel.Info, "WAFER LIST IN {0} : {1}", key.CstID, string.Join(", ", w.Select(l => l.SlotNo)));
                Logger.TransferDataLog.AppendLine(LogLevel.Error, "[호출 스택] {0}", EquipStatusDump.CallStackLog());                
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
            }

            return result;
        }
        public static CassetteInfo GetCst(string id)
        {
            Logger.TransferDataLog.AppendLine("[GetCst] 요청 CSTID : {0}", id);

            IMongoQuery query = Query<CassetteInfo>.EQ(c => c.CstID, id);
            CassetteInfo result = cassettes.FindOne(query);

            if (result == null)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Error, "[GetCst] DB 검색 결과 없음");
                var s = GetCassettes();
                Logger.Log.AppendLine(LogLevel.Info, "CST LIST : {0}", string.Join(", ", s.Select(l => l.CstID)));
                //새로운 카세트 투입 시에도 호출 되어 주석 처리
                //Logger.TransferDataLog.AppendLine(LogLevel.Error, "[호출 스택] {0}", EquipStatusDump.CallStackLog());
                //AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0659_NO_CST_INFO);
            }

            return result;
        }
        public static CassetteInfo GetCst(CassetteInfoKey key)
        {
            if (key == null) return null;
            if (key.ID == "") return null;
            return GetCst(key.ID);
        }
        public static CassetteInfo GetCst(WaferInfoKey waferKey)
        {
            return GetCst(waferKey.CstID);
        }
        public static bool IsExistCst(CassetteInfoKey key)
        {
            bool result = GetCst(key) != null && key.ID != "";
            return result;
        }
        public static bool IsExistCst(string cstId)
        {
            bool result = GetCst(cstId) != null;
            return result;
        }
        public static List<CassetteInfo> GetCassettes()
        {
            var cursor = cassettes.FindAll();
            List<CassetteInfo> list = cursor.ToList<CassetteInfo>();

            return list;
        }
        public static List<CassetteInfo> GetCassettes(DateTime startDate, DateTime endDate, string CstID)
        {
            DateTime MinimumDt = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime MaximumDt = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            var query = Query.Matches("CstID", BsonRegularExpression.Create(new Regex(CstID)));
            var filterDate = cassettes.Find(query).SetSortOrder(SortBy.Ascending("_id")).Where(w => w.InputDate >= MinimumDt && w.InputDate <= MaximumDt);
            var result = filterDate.ToList();

            return result;
        }
        public static List<WaferInfo> GetWafers()
        {
            var cursor = wafers.FindAll();
            List<WaferInfo> list = cursor.ToList<WaferInfo>();

            return list;
        }
        public static List<WaferInfo> GetWafers(string cstID)
        {
            IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, cstID),
                Query<WaferInfo>.EQ(w => w.Status, EmEfemMappingInfo.Presence));

            try 
            {
                var cursor = wafers.FindAs<WaferInfo>(query).SetSortOrder(SortBy.Ascending("SlotNo")); ;
                List<WaferInfo> list = cursor.ToList<WaferInfo>();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<WaferInfo> GetWafers(DateTime startDate, DateTime endDate, string CstID, string waferID)
        {
            DateTime MinimumDt = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime MaximumDt = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            var query = Query.Matches("CstID", BsonRegularExpression.Create(new Regex(CstID, RegexOptions.IgnoreCase)));
            var FilterDateCst = cassettes.Find(query).SetSortOrder(SortBy.Ascending("OutputDate"));

            List<CassetteInfo> list = FilterDateCst.ToList<CassetteInfo>();

            IEnumerable<WaferInfo> FilterWafer = null;
            List<WaferInfo> waferList = new List<WaferInfo>();
            //var query2 = Query.And(
            //                Query.Matches("WaferID", BsonRegularExpression.Create(new Regex(waferID, RegexOptions.IgnoreCase))),
            //                Query.Matches("CstID", BsonRegularExpression.Create(new Regex(CstID, RegexOptions.IgnoreCase)))
            //             );
            var query2 =
                Query.Matches("WaferID", BsonRegularExpression.Create(new Regex(waferID, RegexOptions.IgnoreCase)));

            foreach (var item in list)
            {
                FilterWafer = wafers.Find(query2).Where(w => (w.OutputDate >= MinimumDt && w.OutputDate <= MaximumDt &&  w.CstID == item.CstID));
                //FilterWafer = wafers.Find(query2).Where(w => w.CstID == item.CstID);
                List<WaferInfo> templist = FilterWafer.ToList<WaferInfo>();
                foreach (var item2 in templist)
                {
                    waferList.Add(item2);
                }
            }
            

            return waferList;
        }

        public static WaferInfoKey NextWaferKey(string cstID)
        {
            Logger.TransferDataLog.AppendLine("[NextWaferKey] 요청 CSTID : {0}", cstID);

            IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, cstID),
                Query<WaferInfo>.EQ(w => w.IsOut, false)
            );

            try
            {
                var c1 = wafers.Find(query);
                if (c1 == null || c1.Count() == 0)
                {
                    //Logger.TransferDataLog.AppendLine(LogLevel.Error, "[NextWaferKey] 결과 WaferInfo => null");
                    //Logger.TransferDataLog.AppendLine(LogLevel.Error, "[호출 스택] {0}", EquipStatusDump.CallStackLog());
                    //AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    return new WaferInfoKey();
                }
                var c2 = c1.OrderBy(w => w.SlotNo);
                var ret = c2.First();

                Logger.TransferDataLog.AppendLine("[NextWaferKey] 결과 CSTID : {0}, SLOT : {1}", ret.ToKey().CstID, ret.ToKey().SlotNo.ToString());
                return ret.ToKey();
            }
            catch (Exception)
            {
                return new WaferInfoKey();
            }
        }
        public static List<WaferInfo> CurrentProgressingWafer(string cstID)
        {
            IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, cstID),
                Query<WaferInfo>.EQ(w => w.IsOut, true),
                Query<WaferInfo>.EQ(w => w.IsComeBack, false)
            );

            try
            {
                var c1 = wafers.Find(query);
                if (c1 == null) return null;
                var c2 = c1.OrderBy(w => w.SlotNo);
                var ret = c2.ToList();
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool IsExistPerfectCompleteWafer(string cstId) { return IsExistInCompleteWafer(new CassetteInfoKey() { ID = cstId }); }
        private static bool IsExistInCompleteWafer(CassetteInfoKey cstKey)
        {
            IMongoQuery query = Query.And(
               Query<WaferInfo>.EQ(w => w.CstID, cstKey.ID),
               Query<WaferInfo>.EQ(w => w.Status, EmEfemMappingInfo.Presence),
               Query<WaferInfo>.EQ(w => w.IsInspComplete, true),
               Query<WaferInfo>.EQ(w => w.IsReviewComplete, true)
               );

            try
            {
                return wafers.Find(query).Count() >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsAllComeBack(CassetteInfoKey cstKey)
        {
            IMongoQuery query = Query.And(
                Query<WaferInfo>.EQ(w => w.CstID, cstKey.ID),
                Query<WaferInfo>.EQ(w => w.IsComeBack, false)
                );

            try
            {
                return wafers.Find(query).Count() == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int NoComebackCount(CassetteInfoKey cstKey)
        {
            IMongoQuery query = Query.And(
             Query<WaferInfo>.EQ(w => w.CstID, cstKey.ID),
             Query<WaferInfo>.EQ(w => w.IsComeBack, false)
             );

            try
            {
                return (int)wafers.Find(query).Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public static bool IsAllOut(CassetteInfoKey cstKey)
        {
            IMongoQuery query = Query.And(
              Query<WaferInfo>.EQ(w => w.CstID, cstKey.ID),
              Query<WaferInfo>.EQ(w => w.IsOut, false)
              );

            try
            {
                return wafers.Find(query).Count() == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool InsertCst(CassetteInfo cst)
        {
            try
            {
                cassettes.Insert(cst);

                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }
        public static bool InsertCst(string cstId, EmEfemPort loadPort, int waferCount)
        {
            CassetteInfo cst = new CassetteInfo(cstId, loadPort == EmEfemPort.LOADPORT1 ? 1 : 2);
            cst.SlotCount = waferCount;
            cst.InputDate = DateTime.Now;

            Logger.TransferDataLog.AppendLine("Insert Cassette : [CST ID : {0}], [Slot Count : {1}]", cstId, waferCount.ToString());

            return InsertCst(cst);
        }
        public static bool InsertWafer(WaferInfo wafer)
        {
            try
            {
                wafers.Insert(wafer);

                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }

        }
        public static bool InitExistWafer(string cstId, int slotNo, EmEfemMappingInfo status = EmEfemMappingInfo.Presence)
        {
            WaferInfo wafer = GetWafer(new WaferInfoKey() { CstID = cstId, SlotNo = slotNo });
            if (wafer == null)
                return false;

            wafer.Clear(cstId, slotNo, status);
            if (status != EmEfemMappingInfo.Presence)
            {
                wafer.IsOut = true;
                wafer.IsComeBack = true;
            }
            return wafer.Update();
        }

        public static bool InsertWafer(string cstId, int slotNo, EmEfemMappingInfo status = EmEfemMappingInfo.Presence)
        {
            WaferInfo wafer = new WaferInfo(cstId, slotNo, status);
            if (status != EmEfemMappingInfo.Presence)
            {
                wafer.IsOut = true;
                wafer.IsComeBack = true;
            }
            return InsertWafer(wafer);
        }

        public static bool UpdateWaferInfo(WaferInfo wafer)
        {
            try
            {
                //IMongoQuery query = Query.And(
                //    Query<WaferInfo>.EQ(w => w.CstID, wafer.CstID),
                //    Query<WaferInfo>.EQ(w => w.SlotNo, wafer.SlotNo)
                //);

                //var update = new UpdateBuilder();

                //update.Set("CurLocation", wafer.CurLocation);
                //update.Set("Notch", wafer.Notch);
                //update.Set("IsAlignComplete", wafer.IsAlignComplete);
                //update.Set("IsInspectEnd", wafer.IsInspComplete);
                //update.Set("OutputDate", wafer.OutputDate);
                //update.Set("InputDate", wafer.InputDate);

                //wafers.Update(query, update);

                wafers.Save(wafer);

                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                Logger.ExceptionLog.AppendLine("Update Wafer Info Exception : {0}", str);
                return false;
            }
        }
        public static bool UpdateCst(CassetteInfo cst)
        {
            try
            {
                cassettes.Save(cst);
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }

        public static bool DeleteCst(string cstID)
        {
            try
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte Cst]Cst ID : {0}", cstID);
                IMongoQuery query = Query<CassetteInfo>.EQ(c => c.CstID, cstID);
                cassettes.Remove(query);

                return true;
            }
            catch (Exception ex)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte Cst 실패]{0}", ex.Message);
                return false;
            }
        }
        public static bool DeleteWafer(string cstID, int slot)
        {
            try
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte Wafer]Cst ID : {0}, slot {1}", cstID, slot);
                IMongoQuery query = Query.And(
                    Query<WaferInfo>.EQ(w => w.CstID, cstID),
                    Query<WaferInfo>.EQ(w => w.SlotNo, slot)
                    );

                wafers.Remove(query);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteWafer(string cstID)
        {
            try
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte Wafer]Cst ID : {0}", cstID);
                IMongoQuery query = Query<WaferInfo>.EQ(w => w.CstID, cstID);

                wafers.Remove(query);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //날짜 지나면 삭제
        public static bool DateToDeleteCst()
        {
            try
            {
                var query = cassettes.FindAll().Where(c => c.InputDate < DateTime.Now.AddDays(-1 * GG.Equip.CtrlSetting.Mongo.DeleteDays));
                List<CassetteInfo> list = query.ToList<CassetteInfo>();
                long nRepeatCount = cassettes.Count() - GG.Equip.CtrlSetting.Mongo.MaxCstCount;
                if (nRepeatCount > 0)
                {
                    if (list.Count() != 0)
                    {
                        for (int i = 0; i < nRepeatCount; i++)
                        {
                            IMongoQuery query1 = Query<CassetteInfo>.Where(c => c.CstID == list[i].CstID);
                            IMongoQuery query2 = Query<WaferInfo>.Where(c => c.CstID == list[i].CstID);
                            Logger.TransferDataLog.AppendLine("[DB 자동삭제 실행]");
                            cassettes.Remove(query1);
                            wafers.Remove(query2);
                        }
                    }
                }
                GG.Equip.CtrlSetting.Mongo.LastCycleTime = DateTime.Now;
                GG.Equip.CtrlSetting.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<WaferInfo> GetWafersIn(string cstID)
        {
            IMongoQuery query = Query<WaferInfo>.EQ(w => w.CstID, cstID);

            var result = wafers.FindAs<WaferInfo>(query);
            var result2 = result.OrderBy(w => w.SlotNo);

            List<WaferInfo> list = result2.ToList<WaferInfo>();

            return list;
        }
        public static List<WaferInfo> FindAll(CassetteInfoKey key)
        {
            IMongoQuery query = Query<WaferInfo>.EQ(w => w.CstID, key.ID);

            var result = wafers.FindAs<WaferInfo>(query);
            var result2 = result.OrderBy(w => w.SlotNo);

            List<WaferInfo> list = result2.ToList<WaferInfo>();

            return list;
        }

        public static bool DeleteAllCassette()
        {
            try
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte All Cst] 실행");
                cassettes.Drop();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteAllWafer()
        {
            try
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[Delte All Wafer] 실행");
                wafers.Drop();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static WaferInfoKey GetWaferOrCreateBak(EmEfemDBPort location)
        {
            WaferInfoKey w = GetWaferBak(location), n;
            if (w == null)
            {
                n = new WaferInfoKey() { CstID = "", SlotNo = 0 };
                UpdateBak(location, n);
                return n;
            }
            else
                return w;
        }
        public static CassetteInfoKey GetCstOrCreateBak(EmEfemDBPort location)
        {
            CassetteInfoKey w = GetCstBak(location), n;
            if (w == null)
            {
                n = new CassetteInfoKey() { ID = "" };
                UpdateBak(location, n);
                return n;
            }
            else
                return w;
        }
        public static WaferInfoKey GetWaferBak(EmEfemDBPort location)
        {
            var query = Query<BackupKey>.EQ(b => b.Location, location);
            BackupKey result = baks.FindOne(query);
            if (result == null) return null;
            return new WaferInfoKey() { CstID = result.CstId, SlotNo = result.SlotNo };
        }
        public static CassetteInfoKey GetCstBak(EmEfemDBPort location)
        {
            var query = Query<BackupKey>.EQ(b => b.Location, location);
            BackupKey result = baks.FindOne(query);
            if (result == null) return null;
            return new CassetteInfoKey() { ID = result.CstId };
        }
        public static bool UpdateBak(EmEfemDBPort location, WaferInfoKey key)
        {
            try
            {
                if (location == EmEfemDBPort.LOADPORT1 || location == EmEfemDBPort.LOADPORT2)
                    throw new Exception("Loadport는 wafer로 Update금지");

                var query = Query<BackupKey>.EQ(b => b.Location, location);
                BackupKey result = baks.FindOne(query);

                if (result == null)
                    baks.Save(new BackupKey() { Location = location, CstId = key.CstID, SlotNo = key.SlotNo});
                else
                {
                    result.Location = location;
                    result.CstId = key.CstID;
                    result.SlotNo = key.SlotNo;
                    baks.Save(result);
                }            
                return true;
            }
            catch(Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, "UpdateBak[WaferKey] Exception : {0}\n{1}", ex.Message, ex.ToString());
                return false;
            }
        }
        public static bool UpdateBak(EmEfemDBPort location, CassetteInfoKey key)
        {
            try
            {
                if (location != EmEfemDBPort.LOADPORT1 && location != EmEfemDBPort.LOADPORT2)
                    throw new Exception("Loadport 외에는 Cassette로 Update금지");

                var query = Query<BackupKey>.EQ(b => b.Location, location);
                BackupKey result = baks.FindOne(query);

                if (result == null)
                    baks.Save(new BackupKey() { Location = location, CstId = key.ID, SlotNo = 0 });
                else
                {
                    result.Location = location;
                    result.CstId = key.ID;
                    result.SlotNo = 0;
                    baks.Save(result);
                }
                return true;
            }
            catch (Exception exx)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, "UpdateBak[CassetteKey] Exception : {0}\n{1}", exx.Message, exx.ToString());
                return false;
            }
        }
        public static bool ClearWaferBak(EmEfemDBPort location)
        {
            return UpdateBak(location, new WaferInfoKey() { CstID = "", SlotNo = 0 });
        }
        public static bool ClearCstBak(EmEfemDBPort location)
        {
            return UpdateBak(location, new CassetteInfoKey() { ID = "" });
        }

        public static bool UpdateWaferKey()
        {
            try
            {
                var result = baks.FindAll().OrderBy(b => b.Location).ToList<BackupKey>();

                GG.Equip.Efem.Robot.UpperWaferKey = new WaferInfoKey() { CstID = result[2].CstId, SlotNo = result[2].SlotNo };
                GG.Equip.Efem.Robot.LowerWaferKey = new WaferInfoKey() { CstID = result[3].CstId, SlotNo = result[3].SlotNo };
                GG.Equip.Efem.Aligner.LowerWaferKey = new WaferInfoKey() { CstID = result[4].CstId, SlotNo = result[4].SlotNo };
                GG.Equip.TransferUnit.LowerWaferKey = new WaferInfoKey() { CstID = result[5].CstId, SlotNo = result[5].SlotNo };

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool UpdateCstKey()
        {
            try
            {
                var result = baks.FindAll().OrderBy(b => b.Location).ToList<BackupKey>();

                GG.Equip.Efem.LoadPort1.CstKey = new CassetteInfoKey() { ID = result[0].CstId};
                GG.Equip.Efem.LoadPort2.CstKey = new CassetteInfoKey() { ID = result[1].CstId};

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInEquip(string cstID)
        {
            return GG.Equip.Efem.Aligner.LowerWaferKey.CstID == cstID
                || GG.Equip.TransferUnit.LowerWaferKey.CstID == cstID
                || GG.Equip.Efem.Robot.LowerWaferKey.CstID == cstID
                || GG.Equip.Efem.Robot.UpperWaferKey.CstID == cstID
                ;
        }

        public static void CopyWaferPickToRobot(Equipment equip, EFEMTRANSDataSet srcData, EmEfemRobotArm destArm)
        {            
            StepBase srcUnit = equip.GetPort(srcData.TargetPort);            
            if (srcUnit != null
                && (srcData.TargetPort == EmEfemPort.LOADPORT1 || srcData.TargetPort == EmEfemPort.LOADPORT2)
                && (srcUnit.LowerWaferKey == null || srcUnit.LowerWaferKey.SlotNo != srcData.Slot))
            {
                (srcUnit as EFEMLPMUnit).UpdateNextWaferKey(srcData.Slot);
                try
                {
                    WaferInfo tempWaferInfo = TransferDataMgr.GetWafer(srcUnit.LowerWaferKey);
                    tempWaferInfo.IsOut = true;
                    tempWaferInfo.IsComeBack = false;
                    tempWaferInfo.OutputDate = DateTime.Now;
                    tempWaferInfo.Update();
                }
                catch (Exception ex)
                {                
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    string cstid = (srcUnit != null && srcUnit.LowerWaferKey != null) ? srcUnit.LowerWaferKey.CstID : "";
                    var w = GetWafersIn(cstid);
                    Logger.Log.AppendLine(LogLevel.Info, "WAFER LIST IN {0} : {1}", cstid, string.Join(", ", w.Select(l => l.SlotNo)));
                }
            }
            else if (srcUnit == null || srcUnit.LowerWaferKey == null)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Warning, "WaferMove, No WaferInfo {0}->{1}", srcData.TargetPort.ToString(), destArm.ToString());
                return;
            }

            if (destArm == EmEfemRobotArm.Lower)
            {                
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "WaferMove {0}.{1}_{2}->{3}", 
                    srcUnit.LowerWaferKey.CstID, srcUnit.LowerWaferKey.SlotNo, srcUnit.DBPort.ToString(), destArm.ToString());
                equip.Efem.Robot.LowerWaferKey = (WaferInfoKey)srcUnit.LowerWaferKey.Clone();
                TransferDataMgr.UpdateBak(EmEfemDBPort.LOWERROBOT, equip.Efem.Robot.LowerWaferKey);
            }
            else
            {
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "WaferMove {0}.{1}_{2}->{3}",
                    srcUnit.LowerWaferKey.CstID, srcUnit.LowerWaferKey.SlotNo, srcUnit.DBPort.ToString(), destArm.ToString());
                equip.Efem.Robot.UpperWaferKey = (WaferInfoKey)srcUnit.LowerWaferKey.Clone();
                TransferDataMgr.UpdateBak(EmEfemDBPort.UPPERROBOT, equip.Efem.Robot.UpperWaferKey);
            }

            if (srcUnit.DBPort != EmEfemDBPort.LOADPORT1 && srcUnit.DBPort != EmEfemDBPort.LOADPORT2)
                TransferDataMgr.ClearWaferBak(srcUnit.DBPort);
            srcUnit.LowerWaferKey.Clear();
        }
        public static void CopyWaferPlaceToPort(Equipment equip, EFEMTRANSDataSet destData, EmEfemRobotArm srcArm)
        {
            StepBase destUnit = equip.GetPort(destData.TargetPort);
            if (destUnit == null)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.Warning, "WaferMove, No WaferInfo {1}->{0}", destData.TargetPort.ToString(), srcArm.ToString());
                return;
            }

            if (srcArm == EmEfemRobotArm.Lower)
            {
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "WaferMove {0}.{1}_{2}->{3}", 
                    equip.Efem.Robot.LowerWaferKey.CstID, equip.Efem.Robot.LowerWaferKey.SlotNo, srcArm.ToString(), destUnit.DBPort.ToString());
                if (destUnit.DBPort != EmEfemDBPort.LOADPORT1 && destUnit.DBPort != EmEfemDBPort.LOADPORT2)
                {
                    destUnit.LowerWaferKey = (WaferInfoKey)equip.Efem.Robot.LowerWaferKey.Clone();
                    TransferDataMgr.UpdateBak(destUnit.DBPort, destUnit.LowerWaferKey);
                }

                if (destUnit.DBPort != EmEfemDBPort.LOADPORT1 && destUnit.DBPort != EmEfemDBPort.LOADPORT2)
                {
                    destUnit.LowerWaferKey = (WaferInfoKey)equip.Efem.Robot.LowerWaferKey.Clone();
                    TransferDataMgr.UpdateBak(destUnit.DBPort, destUnit.LowerWaferKey);
                }
                else
                {
                    RobotToLPM(equip, false);
                }
                TransferDataMgr.ClearWaferBak(EmEfemDBPort.LOWERROBOT);
                equip.Efem.Robot.LowerWaferKey.Clear();
            }
            else
            {
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "WaferMove {0}.{1}_{2}->{3}",
                    equip.Efem.Robot.UpperWaferKey.CstID, equip.Efem.Robot.UpperWaferKey.SlotNo, srcArm.ToString(), destUnit.DBPort.ToString());

                if (destUnit.DBPort != EmEfemDBPort.LOADPORT1 && destUnit.DBPort != EmEfemDBPort.LOADPORT2)
                {
                    destUnit.LowerWaferKey = (WaferInfoKey)equip.Efem.Robot.UpperWaferKey.Clone();
                    TransferDataMgr.UpdateBak(destUnit.DBPort, destUnit.LowerWaferKey);
                }
                else
                {
                    RobotToLPM(equip, true);
                }
                TransferDataMgr.ClearWaferBak(EmEfemDBPort.UPPERROBOT);
                equip.Efem.Robot.UpperWaferKey.Clear();
            }
        }

        private static void RobotToLPM(Equipment equip, bool isUpper)
        {
            try
            {
                WaferInfo tempWaferInfo = TransferDataMgr.GetWafer(isUpper ? equip.Efem.Robot.UpperWaferKey : equip.Efem.Robot.LowerWaferKey);
                tempWaferInfo.IsComeBack = true;
                tempWaferInfo.InputDate = DateTime.Now;
                tempWaferInfo.Update();
            }
            catch (Exception ex)
            {                
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                var srcUnit = TransferDataMgr.GetWafer(isUpper ? equip.Efem.Robot.UpperWaferKey : equip.Efem.Robot.LowerWaferKey);
                string cstid = (srcUnit != null) ? srcUnit.CstID : "";
                var w = GetWafersIn(cstid);
                Logger.Log.AppendLine(LogLevel.Info, "WAFER LIST IN {0} : {1}", cstid, string.Join(", ", w.Select(l => l.SlotNo)));
            }
        }
        #endregion
    }
}
