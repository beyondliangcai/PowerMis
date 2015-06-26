using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    public class PriceRate
    {
        private int _priceNo ;
        private string _priceName = String.Empty;
        private double _price ;
        private float _priceRate ;

        public int PriceNo
        {
            get
            {
                return _priceNo;
            }
            set
            {
                _priceNo = value;
            }
        }

        public string PriceName
        {
            get
            {
                return _priceName;
            }
            set
            {
                _priceName = value;
            }
        }

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        public float Rate
        {
            get
            {
                return _priceRate;
            }
            set
            {
                _priceRate = value;
            }
        }

       
    }
}
