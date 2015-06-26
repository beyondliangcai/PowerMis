using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class AreaInfo
    {
        private String _strAreaNo = String.Empty;
        private String _strAreaName = String.Empty;
        private String _strAreaFlag = String.Empty;
        private String _strAreaDate = String.Empty;



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

        public string AreaName
        {
            get
            {
                return _strAreaName;
            }
            set
            {
                _strAreaName = value;
            }
        }

        public string AreaFlag
        {
            get
            {
                return _strAreaFlag;
            }
            set
            {
                _strAreaFlag = value;
            }
        }

        public string AreaDate
        {
            get
            {
                return _strAreaDate;
            }
            set
            {
                _strAreaDate = value;
            }
        }
    }
}
