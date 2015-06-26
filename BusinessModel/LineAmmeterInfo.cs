using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class LineAmmeterInfo
    {
        private String _strLineNum = String.Empty;
        private String _strAmmeterNo = String.Empty;
        private String _strAmmeterName = String.Empty;
        private String _strLineAmmeterMulti = String.Empty;
        private String _strLineAmmeterDate = String.Empty;



        public string LineNum
        {
            get
            {
                return _strLineNum;
            }
            set
            {
                _strLineNum = value;
            }
        }

        public string AmmeterNo
        {
            get
            {
                return _strAmmeterNo;
            }
            set
            {
                _strAmmeterNo = value;
            }
        }

        public string AmmeterName
        {
            get
            {
                return _strAmmeterName;
            }
            set
            {
                _strAmmeterName = value;
            }
        }

        public string LineAmmeterMulti
        {
            get
            {
                return _strLineAmmeterMulti;
            }
            set
            {
                _strLineAmmeterMulti = value;
            }
        }

        public string LineAmmeterDate
        {
            get
            {
                return _strLineAmmeterDate;
            }
            set
            {
                _strLineAmmeterDate = value;
            }
        }
    }
}
