using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class ReceiveAmmeterInfo
    {
        private String _strReceiveNo = String.Empty;
        private String _strReceiveName = String.Empty;
        private String _strMultipile = String.Empty;
        



        public string ReceiveNo
        {
            get
            {
                return _strReceiveNo;
            }
            set
            {
                _strReceiveNo = value;
            }
        }

        public string ReceiveName
        {
            get
            {
                return _strReceiveName;
            }
            set
            {
                _strReceiveName = value;
            }
        }

        public string Multipile
        {
            get
            {
                return _strMultipile;
            }
            set
            {
                _strMultipile = value;
            }
        }

        
    }
}
