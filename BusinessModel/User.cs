using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class User
    {
        private String _strUserNo = String.Empty;
        private String _strUserName = String.Empty;
        private String _strPassword = String.Empty;
        private String _strPermission = String.Empty;

        public string UserNo
        {
            get
            {
                return _strUserNo;
            }
            set
            {
                _strUserNo = value;
            }
        }

        public string UserName
        {
            get
            {
                return _strUserName;
            }
            set
            {
                _strUserName = value;
            }
        }

        public string Password
        {
            get
            {
                return _strPassword;
            }
            set
            {
                _strPassword = value;
            }
        }

        public string Permission
        {
            get
            {
                return _strPermission;
            }
            set
            {
                _strPermission = value;
            }
        }
    }
}
