using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class AdditionInfo
    {
        private String _strCountryAnnex = String.Empty;
        private String _strEssenceFee = String.Empty;
        private String _strEssenceFeeRate = String.Empty;
        private String _strAnnexDate = String.Empty;



        public string CountryAnnex
        {
            get
            {
                return _strCountryAnnex;
            }
            set
            {
                _strCountryAnnex = value;
            }
        }

        public string EssenceFee
        {
            get
            {
                return _strEssenceFee;
            }
            set
            {
                _strEssenceFee = value;
            }
        }

        public string EssenceFeeRate
        {
            get
            {
                return _strEssenceFeeRate;
            }
            set
            {
                _strEssenceFeeRate = value;
            }
        }

        public string AnnexDate
        {
            get
            {
                return _strAnnexDate;
            }
            set
            {
                _strAnnexDate = value;
            }
        }
    }
}
