using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    /**
     * 计费的相关信息
     * @author  Rick
     **/ 
    public class Countfeeinfo
    {
        public string getCustomerNo()
        {
            return this.customerNo;
        }
        public void setCustomerNo(string _customerNo)
        {
            this.customerNo = _customerNo;
        }
        public string getTransformerNo()
        {
            return this.transformerNo;
        }
        public void setTransformerNo(string _transformerNo)
        {
            this.transformerNo = _transformerNo;
        }
        public string getAmmeterMulti()
        {
            return this.ammeterMulti;
        }
        public void setAmmeterMulti(string _ammeterMulti)
        {
            this.ammeterMulti = _ammeterMulti;
        }
        public string getAmmeterVolume()
        {
            return this.ammeterVolume;
        }
        public void setAmmeterVolume(string _ammeterVolume)
        {
            this.ammeterVolume = _ammeterVolume;
        }
        public string getLineLoseRate()
        {
            return this.lineLoseRate;
        }
        public void setLineLoseRate(string _lineLoseRate)
        {
            this.lineLoseRate = _lineLoseRate;
        }
        public string getEssenceFee()
        {
            return this.essenceFee;
        }
        public void setEssenceFee(string _essenceFee)
        {
            this.essenceFee = _essenceFee;
        }
        public string getCountFeeInfoDate()
        {
            return this.countFeeInfoDate;
        }
        public void setCountFeeInfoDate(string _countFeeInfoDate)
        {
            this.countFeeInfoDate = _countFeeInfoDate;
        }
        public string getDiscountRate()
        {
            return this.discountRate;
        }
        public void setDiscountRate(string _discountRate)
        {
            this.discountRate = _discountRate;
        }
        private string customerNo;          /*客户编号*/
        private string transformerNo;       /*变压器编号，默认为零值*/
        private string ammeterMulti;        /*电表倍率，普通的为一倍*/
        private string ammeterVolume;       
        private string lineLoseRate;        /*线损*/
        private string essenceFee;          /*基本费*/
        private string countFeeInfoDate;    /*计费日期*/
        private string discountRate;        /*电费折扣*/
    }
}
