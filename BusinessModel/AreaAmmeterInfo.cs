using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class AreaAmmeterInfo
    {
        private String _strAreaNo = String.Empty;
        private String _strAmmeterNo = String.Empty;
        private String _strAmmeterName = String.Empty;
        private String _strAreaAmmeterMulti = String.Empty;
        private String _strAreaAmmeterDate = String.Empty;



        public string AreaNo
        {
            get
            {
                return _strAreaNo;
            }
            set
            {
                _strAreaNo = value;
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

        public string AreaAmmeterMulti
        {
            get
            {
                return _strAreaAmmeterMulti;
            }
            set
            {
                _strAreaAmmeterMulti = value;
            }
        }

        public string AreaAmmeterDate
        {
            get
            {
                return _strAreaAmmeterDate;
            }
            set
            {
                _strAreaAmmeterDate = value;
            }
        }
    }
}
