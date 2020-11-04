using Microsoft.Practices.EnterpriseLibrary.Data;
using teresa.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teresa.information;

namespace teresa.dataaccess
{
    public class TestMasterDB : baseDB
    {
        public TestMasterDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }
        public int Insert(TestMasterInfo entity)
        {

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();
            sbCmd.Append(@"
INSERT INTO [TestMaster]
           ([ID]
           ,[NO]
           ,[Name]
           ,[Phone]
           ,[Address]
           ,[Birthday]
           ,[Age]
           ,[CreateTime]
           ,[UpdaueTime])
     VALUES
           
           (@ID
           ,@NO
           ,@Name
           ,@Phone
           ,@Address
           ,@Birthday
           ,@Age
           ,@CreateTime
           ,@UpdaueTime)
");
            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            db.AddInParameter(dbCommand, "@NO", DbType.String, entity.NO);
            db.AddInParameter(dbCommand, "@Name", DbType.String, entity.Name);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, entity.Phone);
            db.AddInParameter(dbCommand, "@Address", DbType.String, entity.Address);
            db.AddInParameter(dbCommand, "@Birthday", DbType.DateTime, entity.Birthday);
            db.AddInParameter(dbCommand, "@Age", DbType.Decimal, entity.Age);
            db.AddInParameter(dbCommand, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(dbCommand, "@UpdaueTime", DbType.DateTime, entity.UpdaueTime);
            #endregion

            return db.ExecuteNonQuery(dbCommand);
        }

        //public int Update(int? SID, string ID, string NO, TestMasterInfo entity)
        //        {
        //            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO)) return 0;
        //            Database db = base.GetDatabase();
        //            StringBuilder sbCmd = new StringBuilder();
        //            sbCmd.Append(@"
        //             UPDATE [TestMaster]
        //             SET 
        //             [Name] = @Name
        //             ,[Phone] = @Phone
        //             ,[Address] = @Address
        //             ,[Birthday] = @Birthday
        //             ,[Age] = @Age
        //             ,[UpdaueTime] = @UpdaueTime
        //             WHERE (1=1)
        //");
        //    if (SID.HasValue) sbCmd.Append(" AND SID=@SID ");
        //    if (!string.IsNullOrEmpty(ID)) sbCmd.Append(" AND ID=@ID ");
        //    if (!string.IsNullOrEmpty(NO)) sbCmd.Append(" AND NO=@NO ");

        //    DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());


        //    if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
        //    if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
        //    if (!string.IsNullOrEmpty(NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, NO);

        //    db.AddInParameter(dbCommand, "@Name", DbType.String, entity.Name);
        //    db.AddInParameter(dbCommand, "@Phone", DbType.String, entity.Phone);
        //    db.AddInParameter(dbCommand, "@Address", DbType.String, entity.Address);
        //    db.AddInParameter(dbCommand, "@Birthday", DbType.DateTime, entity.Birthday);
        //    db.AddInParameter(dbCommand, "@Age", DbType.Decimal, entity.Age);
        //    db.AddInParameter(dbCommand, "@UpdaueTime", DbType.DateTime, DateTime.Now);

        //    return db.ExecuteNonQuery(dbCommand);
        //}
        public int Update(TestMasterInfo entity)
        {
            if (entity==null || string.IsNullOrEmpty(entity.ID)) return 0;
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();
            sbCmd.Append(@"
             UPDATE [TestMaster]
             SET 
             [Name] = @Name
             ,[Phone] = @Phone
             ,[Address] = @Address
             ,[Birthday] = @Birthday
             ,[Age] = @Age
             ,[UpdaueTime] = @UpdaueTime
             WHERE (1=1)
");
            //if (entity.SID) sbCmd.Append(" AND SID=@SID ");
            if (!string.IsNullOrEmpty(entity.ID)) sbCmd.Append(" AND ID=@ID ");
            //ID當key寫法二在上兩句 sql where ID=@id
        
            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());


            //if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            //if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(entity.NO)) db.AddInParameter(dbCommand, "@NO", DbType.String,entity.NO);

            db.AddInParameter(dbCommand, "@Name", DbType.String, entity.Name);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, entity.Phone);
            db.AddInParameter(dbCommand, "@Address", DbType.String, entity.Address);
            db.AddInParameter(dbCommand, "@Birthday", DbType.DateTime, entity.Birthday);
            db.AddInParameter(dbCommand, "@Age", DbType.Decimal, entity.Age);
            db.AddInParameter(dbCommand, "@UpdaueTime", DbType.DateTime, DateTime.Now);

            return db.ExecuteNonQuery(dbCommand);
        }
        public int Delete(int? SID, string ID, string NO)
        {

            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO)) return 0;
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	DELETE from[TestMaster]		");
            sbCmd.Append("	WHERE (1=1) 		");

            if (SID.HasValue) sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID ");
            if (!string.IsNullOrEmpty(NO)) sbCmd.Append("AND NO=@NO ");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, NO);

            #endregion

            return db.ExecuteNonQuery(dbCommand);

        }
        
        //原本有的 拿欄位去用 回傳物件
        public TestMasterInfo Load( int? SID , string ID,string NO)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(NO)) return null;
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [TestMaster] WITH (Nolock) WHERE (1=1) ");

            if (SID.HasValue) sbCmd.Append("AND SID=@SID ");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID ");
            if (!string.IsNullOrEmpty(NO)) sbCmd.Append("AND NO=@NO ");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter


            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, NO);
            #endregion

            DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                DataRow dr = dtTemp.Rows[0];
                return new TestMasterInfo
                {
                    SID = Convert.ToInt32(dr["SID"]),
                    ID = Convert.ToString(dr["ID"]),
                    NO = Convert.ToString(dr["NO"]),
                    Name = Convert.ToString(dr["Name"]),
                    Address = Convert.ToString(dr["Address"]),
                    Age = dr["Age"] == DBNull.Value ? new Nullable<decimal>() : Convert.ToDecimal(dr["AGE"]),
                    Birthday = dr["Birthday"] == DBNull.Value ? new DateTime?() : Convert.ToDateTime(dr["Birthday"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    CreateTime = Convert.ToDateTime(dr["CreateTime"]),
                    UpdaueTime = Convert.ToDateTime(dr["UpdaueTime"])

                };
            }
            return null;
        }
        
        //自己創新的 再拿去用 可以塞值 回傳物件
        public DataTable Load(ref PageInationInfo pagination, int? SID, string ID = null, string NO = null, string Name = null, string Phone = null,
                   string Address = null, DateTime? BirthdayFrom = null, DateTime? BirthdayTo = null,
                   decimal? AgeFrom = null, decimal? AgeTo = null)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                SELECT ROW_NUMBER() over(order by updauetime desc, SID desc) rowno,
                * 
                FROM [TestMaster] WITH (Nolock)  
                WHERE (1=1) "
                );
            #region 條件式

            if (SID.HasValue) sbCmd.Append(" AND SID=@SID");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append(" AND ID=@ID");
            if (!string.IsNullOrEmpty(NO)) sbCmd.Append(" AND NO=@NO");
            if (!string.IsNullOrEmpty(Name)) sbCmd.Append(" AND Name like @Name");
            if (!string.IsNullOrEmpty(Phone)) sbCmd.Append(" AND Phone=@Phone");
            if (!string.IsNullOrEmpty(Address)) sbCmd.Append(" AND Address=@Address");
            if (BirthdayFrom.HasValue) sbCmd.Append(" AND Cast(Birthday as date) >= @BirthdayFrom");
            if (BirthdayTo.HasValue) sbCmd.Append(" AND Cast(Birthday as date) <= @BirthdayTo");
            if (AgeFrom.HasValue) sbCmd.Append(" AND Age >= @AgeFrom");
            if (AgeTo.HasValue) sbCmd.Append(" AND Age <= @AgeTo");

            #endregion

            string sql = sbCmd.ToString();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            #region Add In Parameter

            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, NO);
            if (!string.IsNullOrEmpty(Name)) db.AddInParameter(dbCommand, "@Name", DbType.String, $"%{Name}%");
            if (!string.IsNullOrEmpty(Phone)) db.AddInParameter(dbCommand, "@Phone", DbType.String, Phone);
            if (!string.IsNullOrEmpty(Address)) db.AddInParameter(dbCommand, "@Address", DbType.String, Address);
            if (BirthdayFrom.HasValue) db.AddInParameter(dbCommand, "@BirthdayFrom", DbType.DateTime, BirthdayFrom.Value.Date);
            if (BirthdayTo.HasValue) db.AddInParameter(dbCommand, "@BirthdayTo", DbType.DateTime, BirthdayTo.Value.Date);
            if (AgeFrom.HasValue) db.AddInParameter(dbCommand, "@AgeFrom", DbType.Decimal, AgeFrom.Value);
            if (AgeTo.HasValue) db.AddInParameter(dbCommand, "@AgeTo", DbType.Decimal, AgeTo.Value);

            #endregion

            if (pagination.Total == 0)
            {
                dbCommand.CommandText = "with x as ( " + sql + " ) select count(*) from x";
                pagination.Total = (int)ExecuteScalar(db, dbCommand);
            }

            dbCommand.CommandText = "with x as (" + sql + ") select * from x where rowno between @s and @e";
            db.AddInParameter(dbCommand, "@s", DbType.Int32, (pagination.Index - 1) * pagination.Size + 1);
            db.AddInParameter(dbCommand, "@e", DbType.Int32, pagination.Size * pagination.Index);

            return ExecuteDataSet(db, dbCommand).Tables[0];
        }

        public DataTable Load(ref PageInationInfo pagination,TestMasterInfo.Condtions Condtions)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append(@"
                SELECT ROW_NUMBER() over(order by updauetime desc, SID desc) rowno,
                * 
                FROM [TestMaster] WITH (Nolock)  
                WHERE (1=1) "
                );
            #region 條件式

            if (Condtions.SID.HasValue) sbCmd.Append(" AND SID=@SID");
            if (!string.IsNullOrEmpty(Condtions.ID)) sbCmd.Append(" AND ID=@ID");
            if (!string.IsNullOrEmpty(Condtions.NO)) sbCmd.Append(" AND NO=@NO");
            if (!string.IsNullOrEmpty(Condtions.Name)) sbCmd.Append(" AND Name like @Name");
            if (!string.IsNullOrEmpty(Condtions.Phone)) sbCmd.Append(" AND Phone=@Phone");
            if (!string.IsNullOrEmpty(Condtions.Address)) sbCmd.Append(" AND Address=@Address");
            if (Condtions.BirthdayFrom.HasValue) sbCmd.Append(" AND Cast(Birthday as date) >= @BirthdayFrom");
            if (Condtions.BirthdayTo.HasValue) sbCmd.Append(" AND Cast(Birthday as date) <= @BirthdayTo");
            if (Condtions.AgeFrom.HasValue) sbCmd.Append(" AND Age >= @AgeFrom");
            if (Condtions.AgeTo.HasValue) sbCmd.Append(" AND Age <= @AgeTo");

            #endregion

            string sql = sbCmd.ToString();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            #region Add In Parameter

            if (Condtions.SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, Condtions.SID.Value);
            if (!string.IsNullOrEmpty(Condtions.ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, Condtions.ID);
            if (!string.IsNullOrEmpty(Condtions.NO)) db.AddInParameter(dbCommand, "@NO", DbType.String, Condtions.NO);
            if (!string.IsNullOrEmpty(Condtions.Name)) db.AddInParameter(dbCommand, "@Name", DbType.String, $"%{Condtions.Name}%");
            if (!string.IsNullOrEmpty(Condtions.Phone)) db.AddInParameter(dbCommand, "@Phone", DbType.String, Condtions.Phone);
            if (!string.IsNullOrEmpty(Condtions.Address)) db.AddInParameter(dbCommand, "@Address", DbType.String, Condtions.Address);
            if (Condtions.BirthdayFrom.HasValue) db.AddInParameter(dbCommand, "@BirthdayFrom", DbType.DateTime, Condtions.BirthdayFrom.Value.Date);
            if (Condtions.BirthdayTo.HasValue) db.AddInParameter(dbCommand, "@BirthdayTo", DbType.DateTime, Condtions.BirthdayTo.Value.Date);
            if (Condtions.AgeFrom.HasValue) db.AddInParameter(dbCommand, "@AgeFrom", DbType.Decimal, Condtions.AgeFrom.Value);
            if (Condtions.AgeTo.HasValue) db.AddInParameter(dbCommand, "@AgeTo", DbType.Decimal, Condtions.AgeTo.Value);

            #endregion

            if (pagination.Total == 0)
            {
                dbCommand.CommandText = "with x as ( " + sql + " ) select count(*) from x";
                pagination.Total = (int)ExecuteScalar(db, dbCommand);
            }

            dbCommand.CommandText = "with x as (" + sql + ") select * from x where rowno between @s and @e";
            db.AddInParameter(dbCommand, "@s", DbType.Int32, (pagination.Index - 1) * pagination.Size + 1);
            db.AddInParameter(dbCommand, "@e", DbType.Int32, pagination.Size * pagination.Index);

            return ExecuteDataSet(db, dbCommand).Tables[0];
        }
    }

}


//SELECT* FROM EXPLOG
//Select ROW_NUMBER() over(order by uPDAUETIME
//DESC) sERIALnO ,* from  TestMaster
