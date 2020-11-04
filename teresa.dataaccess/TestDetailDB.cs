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
    public class TestDetailDB : baseDB
    {
        public TestDetailDB()
        {
            base.DBInstanceName = "CONNCLEAN";
        }
        public int Insert(TestDetailInfo entity)
        {

            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();
            sbCmd.Append(@"
INSERT INTO [TestDetail]
           ([ID]
           ,[MasterID]
           ,[A]
           ,[B]
           ,[C]
           ,[D]
           ,[E]
           ,[F]
           ,[G])
     VALUES
           
           (@ID
           ,@MasterID
           ,@A
           ,@B
           ,@C
           ,@D
           ,@E
           ,@F
           ,@G)
");
            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter
            db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            db.AddInParameter(dbCommand, "@MasterID", DbType.String, entity.MasterID);
            db.AddInParameter(dbCommand, "@A", DbType.String, entity.A);
            db.AddInParameter(dbCommand, "@B", DbType.String, entity.B);
            db.AddInParameter(dbCommand, "@C", DbType.String, entity.C);
            db.AddInParameter(dbCommand, "@D", DbType.String, entity.D);
            db.AddInParameter(dbCommand, "@E", DbType.String, entity.E);
            db.AddInParameter(dbCommand, "@F", DbType.String, entity.F);
            db.AddInParameter(dbCommand, "@G", DbType.String, entity.G);


            #endregion

            return db.ExecuteNonQuery(dbCommand);
        }

        public int Update(int? SID, string ID, string MasterID, TestDetailInfo entity)
        {
            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(MasterID)) return 0;
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();
            sbCmd.Append(@"
             UPDATE [TestDetail]
             SET 
             [A] = @A
             ,[B] = @B
             ,[C] = @C
             ,[D] = @D    
             ,[E] = @E
             ,[F] = @F
             ,[G] = @G
             WHERE (1=1)
");
            if (SID.HasValue) sbCmd.Append("AND SID=@SID");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID");
            if (!string.IsNullOrEmpty(MasterID)) sbCmd.Append("AND MasterID=@MasterID");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());


            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, entity.ID);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@MasterID", DbType.String, entity.MasterID);

            db.AddInParameter(dbCommand, "@A", DbType.String, entity.A);
            db.AddInParameter(dbCommand, "@B", DbType.String, entity.B);
            db.AddInParameter(dbCommand, "@C", DbType.String, entity.C);
            db.AddInParameter(dbCommand, "@D", DbType.String, entity.D);
            db.AddInParameter(dbCommand, "@E", DbType.String, entity.E);
            db.AddInParameter(dbCommand, "@F", DbType.String, entity.F);
            db.AddInParameter(dbCommand, "@G", DbType.String, entity.G);
            return db.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int? SID, string ID, string MasterID)
        {

            if (!SID.HasValue & string.IsNullOrEmpty(ID) & string.IsNullOrEmpty(MasterID)) return 0;
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	DELETE from[TestDetail]		");
            sbCmd.Append("	WHERE (1=1)");

            if (SID.HasValue) sbCmd.Append("AND SID=@SID");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID");
            if (!string.IsNullOrEmpty(MasterID)) sbCmd.Append("AND MasterID=@MasterID");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter

            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);

            #endregion

            return db.ExecuteNonQuery(dbCommand);

        }
        public TestDetailInfo Load(int? SID, string ID, string MasterID)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("	SELECT * FROM [TestDetail] WITH (MasterIDlock) WHERE (1=1) ");

            if (SID.HasValue) sbCmd.Append("AND SID=@SID");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID");
            if (!string.IsNullOrEmpty(MasterID)) sbCmd.Append("AND MasterID=@MasterID");

            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            #region Add In Parameter


            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);
            #endregion

            DataTable dtTemp = db.ExecuteDataSet(dbCommand).Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                DataRow dr = dtTemp.Rows[0];
                return new TestDetailInfo
                {
                    SID = Convert.ToInt32(dr["SID"]),
                    ID = Convert.ToString(dr["ID"]),
                    MasterID = Convert.ToString(dr["MasterID"]),
                    A = Convert.ToString(dr["A"]),
                    B = Convert.ToString(dr["B"]),
                    C = Convert.ToString(dr["C"]),
                    D = Convert.ToString(dr["D"]),
                    E = Convert.ToString(dr["E"]),
                    F = Convert.ToString(dr["F"]),
                    G = Convert.ToString(dr["G"])
                };
            }
            return null;
        }

        public DataTable Load(int? SID,string ID = null, string MasterID = null, 
            string A = null, string B = null, string C = null,
           string D = null, string E = null,
             string F = null, string G = null)
        {
            Database db = base.GetDatabase();
            StringBuilder sbCmd = new StringBuilder();

            sbCmd.Append("SELECT * FROM [TestDetail] WITH (Nolock) WHERE (1=1) ");

            if (SID.HasValue) sbCmd.Append("AND SID=@SID");
            if (!string.IsNullOrEmpty(ID)) sbCmd.Append("AND ID=@ID");
            if (!string.IsNullOrEmpty(MasterID)) sbCmd.Append("AND MasterID=@MasterID");
            if (!string.IsNullOrEmpty(A)) sbCmd.Append("AND A=@A");
            if (!string.IsNullOrEmpty(B)) sbCmd.Append("AND B=@B");
            if (!string.IsNullOrEmpty(C)) sbCmd.Append("AND C=@C");
            if (!string.IsNullOrEmpty(D)) sbCmd.Append("AND D=@D");
            if (!string.IsNullOrEmpty(E)) sbCmd.Append("AND E=@E");
            if (!string.IsNullOrEmpty(F)) sbCmd.Append("AND F=@F");
            if (!string.IsNullOrEmpty(F)) sbCmd.Append("AND G=@G");



            DbCommand dbCommand = db.GetSqlStringCommand(sbCmd.ToString());

            if (SID.HasValue) db.AddInParameter(dbCommand, "@SID", DbType.Int32, SID.Value);
            if (!string.IsNullOrEmpty(ID)) db.AddInParameter(dbCommand, "@ID", DbType.String, ID);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@MasterID", DbType.String, MasterID);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@A", DbType.String, A);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@B", DbType.String, B);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@C", DbType.String, C);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@D", DbType.String, D);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@E", DbType.String, E);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@F", DbType.String, F);
            if (!string.IsNullOrEmpty(MasterID)) db.AddInParameter(dbCommand, "@G", DbType.String, G);
            return ExecuteDataSet(db, dbCommand).Tables[0];
        }
 
    }

}


