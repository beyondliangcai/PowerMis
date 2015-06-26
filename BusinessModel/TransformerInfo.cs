using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class TransformerInfo
    {
        private String _strTransformerNo = String.Empty;
        private String _strTransformerName = String.Empty;
        private String _strTransformerLoseNo = String.Empty;
        //private String _strAreaDate = String.Empty;
        public string TransformerNo
        {
            get
            {
                return _strTransformerNo;
            }
            set
            {
                _strTransformerNo = value;
            }
        }

        public string TransformerName
        {
            get
            {
                return _strTransformerName;
            }
            set
            {
                _strTransformerName = value;
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
    }
}
