using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using BusinessModel;



namespace BusinessLogic
{
    public class LoginAction
    {
        public int Login(string userCode, string passwd)
        {
            if (userCode == "" || passwd == "")
            {
                return 0;
            }
            try
            {
                string SQLString = "SELECT UserNo FROM System WHERE UserNo = '" + userCode + "' AND Password = '" + passwd + "'";
                DataSet dataset = SQLUtl.Query(SQLString);
                if (dataset.Tables[0].Rows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public User GetUser(string userCode)
        {
            User user = new User();
            try
            {
                string strSql = " SELECT UserNo,UserName,Permission,Password FROM System WHERE UserNo = '" + userCode + "'";

                // 取得用户的DataSet
                DataSet dataset = SQLUtl.Query(strSql);
                DataRow datarow = dataset.Tables[0].Rows[0];

                user.UserNo = datarow["UserNo"].ToString();
                user.UserName = datarow["UserName"].ToString();
                user.Password = datarow["Password"].ToString();
                user.Permission = datarow["Permission"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }


        /// 获取服务器时间    
        public DateTime GetServerTime()
        {
            try
            {
                string strSQL = "SELECT GETDATE() AS Time ";
                return (DateTime)SQLUtl.Query(strSQL).Tables[0].Rows[0]["Time"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
