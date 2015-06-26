using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class LineLose
    {
        private String _strLineNum = String.Empty;
        private String _strLineName = String.Empty;
        private String _strPowerPlaceNo = String.Empty;
        private String _strReportOrder = String.Empty;
        private String _strLineDate = String.Empty;



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

        public string LineName
        {
            get
            {
                return _strLineName;
            }
            set
            {
                _strLineName = value;
            }
        }

        public string PowerPlaceNo
        {
            get
            {
                return _strPowerPlaceNo;
            }
            set
            {
                _strPowerPlaceNo = value;
            }
        }

        public string ReportOrder
        {
            get
            {
                return _strReportOrder;
            }
            set
            {
                _strReportOrder = value;
            }
        }

        public string LineDate
        {
            get
            {
                return _strLineDate;
            }
            set
            {
                _strLineDate = value;
            }
        }
    }
}
