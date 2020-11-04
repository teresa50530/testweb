using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teresa.dataaccess;
using teresa.information;

namespace teresa.business
{
   public class TestBiz
    {
        public class Master
        {
            /// <summary>
            /// 新增
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Insert(TestMasterInfo entity)
            {
                var result = 0;
                try
                {
              
                    //int x = 1;
                    //int y = 0;
                    //using(TransactionS)
                    var dbMaster = new TestMasterDB();
                    int  rMaster = dbMaster.Insert(entity);

                    var dbDetail = new TestDetailDB();
                    int rDetail = dbDetail.Insert(new TestDetailInfo
                    {
                        ID = Guid.NewGuid().ToString(),
                        MasterID = entity.ID,
                        A = "asdfad"
                    });
                    result = rMaster + rDetail ;
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Insert", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 修改
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Update(TestMasterInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Update(entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Update", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 刪除
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public int Delete(int? SID, string ID, string NO)
            {
                var result = 0;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Delete(SID, ID, NO);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Delete", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 取一筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public TestMasterInfo Load(int? SID, string ID , string NO )
            {
                TestMasterInfo result = null;
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(SID, ID, NO);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 依條件取多筆資料
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <param name="Name"></param>
            /// <param name="Phone"></param>
            /// <param name="Address"></param>
            /// <param name="BirthdayFrom"></param>
            /// <param name="BirthdayTo"></param>
            /// <param name="AgeFrom"></param>
            /// <param name="AgeTo"></param>
            /// <returns></returns>
            public DataTable Load(ref PageInationInfo pagination, int ?SID = null, string ID = null, string NO = null, string Name = null, string Phone = null,
                string Address = null, DateTime? BirthdayFrom = null, DateTime? BirthdayTo = null,
                decimal? AgeFrom = null, decimal? AgeTo = null)
            {
                DataTable result = new DataTable();
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(ref pagination, SID, ID: ID, NO: NO, Name: Name, Phone: Phone, Address: Address,
                        BirthdayFrom: BirthdayFrom, BirthdayTo: BirthdayTo, AgeFrom: AgeFrom, AgeTo: AgeTo);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }

            public DataTable Load(ref PageInationInfo pagination, TestMasterInfo.Condtions Condtions)
            {
                DataTable result = new DataTable();
                try
                {
                    var db = new TestMasterDB();
                    result = db.Load(ref pagination, Condtions.SID, ID: Condtions.ID, NO: Condtions.NO, Name: Condtions.Name, Phone: Condtions.Phone
                        , Address: Condtions.Address,
                        BirthdayFrom: Condtions.BirthdayFrom, BirthdayTo: Condtions.BirthdayTo, AgeFrom: Condtions.AgeFrom, AgeTo: Condtions.AgeTo);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Master", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
        }

        public class Detail
        {
            /// <summary>
            /// 新增
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Insert(TestDetailInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestDetailDB();
                    result = db.Insert(entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Insert", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 修改
            /// </summary>
    
            /// <returns></returns>
            public int Update(int? SID, string ID, string MasterID, TestDetailInfo entity)
            {
                var result = 0;
                try
                {
                    var db = new TestDetailDB();
                    result = db.Update(SID, ID, MasterID, entity);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Update", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 刪除
            /// </summary>
            /// <param name="SID"></param>
            /// <param name="ID"></param>
            /// <param name="NO"></param>
            /// <returns></returns>
            public int Delete(int? SID, string MasterID, string ID)
            {
                var result = 0;
                try
                {
                    var db = new TestDetailDB();
                    result = db.Delete(SID, ID, MasterID);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Delete", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 取一筆資料
        
            /// <returns></returns>
            public TestDetailInfo Load(int? SID, string MasterID, string ID)
            {
                TestDetailInfo result = null;
                try
                {
                    var db = new TestDetailDB();
                    result = db.Load(SID, ID, MasterID);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            }
            /// <summary>
            /// 依條件取多筆資料
            /// </summary>
   
            /// <returns></returns>
            public DataTable Load (int? SID = null, string MasterID = null, string ID = null,
            string A = null, string B = null, string C = null,
           string D = null, string E = null,
             string F = null, string G = null)
            {
                DataTable result = new DataTable();
                try
                {
                    var db = new TestDetailDB();
                    result = db.Load(SID, ID: ID, MasterID: MasterID, A: A, B: B, C: C,
                        D: D, E: E, F: F, G: G);
                }
                catch (Exception ex)
                {
                    var dbExpLog = new ExpLogDB();
                    dbExpLog.Insert(new ExpLogInfo { ClassName = "TestBiz.Detail", MethodName = "Load", ErrMsg = ex.Message });
                }
                return result;
            } 
        }
    }
}
