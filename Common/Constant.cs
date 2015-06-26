using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessModel;

namespace Common
{
    

    public class Constant
    {
        private static User loginUser = null;
        private static DataBase dataBase = null;

        public const int OK = 0;
        public const int ERROR = -1;
   

        

        /// <summary>
        /// 用户信息的静态变量类
        /// </summary>
        public static User LoginUser
        {
            get
            {
                return loginUser;
            }
            set
            {
                loginUser = value;
            }
        }

        /// <summary>
        /// 数据库信息的静态变量类
        /// </summary>
        public static DataBase DB
        {
            get
            {
                return dataBase;
            }
            set
            {
                dataBase = value;
            }
        }
    }
}
