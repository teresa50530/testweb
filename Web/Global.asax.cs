using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 應用程式啟動時執行的程式碼 

            string SecIniPath = ConfigurationManager.AppSettings["SecIniPath"].ToString();
            string SystemID = ConfigurationManager.AppSettings["SystemID"].ToString();
            string ConnList = ConfigurationManager.AppSettings["ConnList"].ToString();
            string[] system = SystemID.Split(new char[',']);
            string[] conns = ConnList.Split(new char[',']);
            #region 設定Key1 and Key2
            //讀ini檔
            Vista.SEC.Coder coder = new Vista.SEC.Coder();
            Vista.SEC.IniUtil INI = new Vista.SEC.IniUtil(SecIniPath);
            Application.Add("SECKey1", coder.Decrypt(INI.ReadValue("Main", "Key1")));
            Application.Add("SECKey2", coder.Decrypt(INI.ReadValue("Main", "Key2")));
            #endregion

            #region 設定Connection Pool

            Vista.SEC.ConnectionPool CP = new Vista.SEC.ConnectionPool(SecIniPath);
            foreach(var conn in conns)
            {
                CP.SetConnection(conn);
            }

            //Vista.DBSSEC.ConnectionPool CP;
            //CP = new Vista.DBSSEC.ConnectionPool(SecIniPath);
            //CP.SetConnection(SystemID, ConnList);


            //for (int i = 0; i < system.Length; i++)
            //{
            //    Application.Add(system[i], Vista.DBSSEC.ConnectionPool.GetConnection(conns[i]));
            //}
            //Application.Add("CONNSEC", Vista.DBSSEC.ConnectionPool.GetConnection("CONNSEC"));
            //Application.Add("CONNPIPA", Vista.DBSSEC.ConnectionPool.GetConnection("CONNPIPA"));
            #endregion
        }
    }
}




