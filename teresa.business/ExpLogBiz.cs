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
   public class ExpLogBiz
    {
        public bool Insert(ExpLogInfo entity)
        {
            var result = true;
            try{
                var db = new ExpLogDB();
                db.Insert(entity);

            }
            catch
            {
                result = false;
            }
            return result;

        }


        public bool Update(ExpLogInfo entity)
        {
            var result = true;
            try
            {
                var db = new ExpLogDB();
                db.Update(entity);

            }
            catch
            {
                result = false;
            }
            return result;

        }

        public bool Delete(int SID)
        {
            var result = true;
            try
            {
                var db = new ExpLogDB();
                db.Delete(SID);

            }
            catch
            {
                result = false;
            }
            return result;

        }

        public ExpLogInfo Load(int SID)
        {
            var result = new ExpLogInfo();
            try
            {
                var db = new ExpLogDB();
               result= db.Load(SID);

            }
            catch
            {
                result =null;
            }
            return result;
        }

        public DataTable Load()
        {
            DataTable result = new DataTable();
            try
            {
                var db = new ExpLogDB();
                result = db.Load();

            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
