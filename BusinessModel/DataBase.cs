using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class DataBase
    {
        private String serverIP = String.Empty;
        private String loginID = String.Empty;
        private String password = String.Empty;
        private String databaseName = String.Empty;

        public string ServerIP
        {
            get
            {
                return serverIP;
            }
            set
            {
                serverIP = value;
            }
        }

        public string LoginID
        {
            get
            {
                return loginID;
            }
            set
            {
                loginID = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string DataBaseName
        {
            get
            {
                return databaseName;
            }
            set
            {
                databaseName = value;
            }
        }
    }
}
