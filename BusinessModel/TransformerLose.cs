using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class TransformerLose
    {
        private String _strTransformerLoseNo = String.Empty;
        private String _strTransformerType = String.Empty;
        private String _strStandarVolume = String.Empty;
        private String _strMonthUsed = String.Empty;
        private String _strTranformerLose = String.Empty;
        private String _strLessOrMoreFlag = String.Empty;
        //private String _strAreaDate = String.Empty;

        public string LessOrMoreFlag
        {
            get
            {
                return _strLessOrMoreFlag;
            }
            set
            {
                _strLessOrMoreFlag = value;
            }
        }

        public string TranformerLose
        {
            get
            {
                return _strTranformerLose;
            }
            set
            {
                _strTranformerLose = value;
            }
        }

        public string MonthUsed
        {
            get
            {
                return _strMonthUsed;
            }
            set
            {
                _strMonthUsed = value;
            }
        }

        public string TransformerLoseNo
        {
            get
            {
                return _strTransformerLoseNo;
            }
            set
            {
                _strTransformerLoseNo = value;
            }
        }

        public string TransformerType
        {
            get
            {
                return _strTransformerType;
            }
            set
            {
                _strTransformerType = value;
            }
        }

        public string StandarVolume
        {
            get
            {
                return _strStandarVolume;
            }
            set
            {
                _strStandarVolume = value;
            }
        }


    }
}
