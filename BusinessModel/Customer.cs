using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{
    /**
     * 客户信息类
     * @author      Rick
     */ 
    public class Customer
    {
        public void setCustomerNo(string _custmerNo)
        {
            this.customerNo = _custmerNo;
        }

        public string getCustomerNo()
        {
            return this.customerNo;
        }

        public void setCustomerName(string _customerName)
        {
            this.customerName = _customerName;
        }

        public string getCustomerName()
        {
            return this.customerName;
        }

        public void setCustomerAddress(string _customerAddress)
        {
            this.customerAddress = _customerAddress;
        }

        public string getCustomerAddress()
        {
            return this.customerAddress;
        }

        public void setLine(string _line)
        {
            this.line = _line;
        }

        public string getLine()
        {
            return this.line;
        }

        public void setArea(string _area)
        {
            this.area = _area;
        }

        public string getArea()
        {
            return this.area;
        }

        public void setInvoiceType(string _invoiceType)
        {
            this.invoiceType = _invoiceType;
        }

        public string getInvoiceType()
        {
            return this.invoiceType;
        }

        public void setElectriCharacterName(string _electriCharacterName)
        {
            this.electriCharacterName = _electriCharacterName;
        }

        public string getElectriCharacterName()
        {
            return this.electriCharacterName;
        }

        public void setVoltageFlag(string _voltageFlag)
        {
            this.voltageFlag = _voltageFlag;
        }

        public string getVoltageFlag()
        {
            return this.voltageFlag;
        }

        public void setAmmeterType(string _ammeterType)
        {
            this.ammeterType = _ammeterType;
        }

        public string getAmmeterType()
        {
            return this.ammeterType;
        }

        public void setAmmeter(string _ammeter)
        {
            this.ammeter = _ammeter;
        }

        public string getAmmeter()
        {
            return this.ammeter;
        }

        public void setBankCode(string _bankCode)
        {
            this.bankCode = _bankCode;
        }

        public string getBankCode()
        {
            return this.bankCode;
        }

        public void setBankName(string _bankName)
        {
            this.bankName = _bankName;
        }

        public string getBankName()
        {
            return this.bankName;
        }

        public void setBankAccount(string _bankAccount)
        {
            this.bankAccount = _bankAccount;
        }

        public string getBankAccount()
        {
            return this.bankAccount;
        }

        public void setTradeCode(string _tradeCode)
        {
            this.tradeCode = _tradeCode;
        }

        public string getTradeCode()
        {
            return this.tradeCode;
        }

        public void setValueAddTaxNo(string _valueAddTaxNo)
        {
            this.valueAddTaxNo = _valueAddTaxNo;
        }

        public string getValueAddTaxNo()
        {
            return this.valueAddTaxNo;
        }

        public void setOrganFlag(string _organFlag)
        {
            this.organFlag = _organFlag;
        }

        public string getOrganFlag()
        {
            return this.organFlag;
        }

        public void setEspecialFlag(string _especialFlag)
        {
            this.especialFlag = _especialFlag;
        }

        public string getEspecialFlag()
        {
            return this.especialFlag;
        }

        public void setLowProtectFlag(string _lowProtectFlag)
        {
            this.lowProtectFlag = _lowProtectFlag;
        }

        public string getLowProtectFlag()
        {
            return this.lowProtectFlag;
        }

        public void setTranslossOrBaseprice(string _translossOrBaseprice)
        {
            this.translossOrBaseprice = _translossOrBaseprice;
        }

        public string getTranslossOrBaseprice()
        {
            return this.translossOrBaseprice;
        }

        public void setCustomerInfoDate(string _dt)
        {
            this.customerInofDate = _dt;
        }

        public string getCustomerInfoDate()
        {
            return this.customerInofDate;
        }

        public void setPassword(string _password)
        {
            this.password = _password;
        }

        public string getPassword()
        {
            return this.password;
        }

        public decimal getCustomerPosition()
        {
            return this.customerPosition;
        }

        public void setCustomerPosition(decimal _customerPosition)
        {
            this.customerPosition = _customerPosition;
        }

        public string getPhoneNum()
        {
            return this.phoneNum;
        }

        public void setPhoneNum(string _phoneNum)
        {
            this.phoneNum = _phoneNum;
        }

        public void setAmmeterNo(string _ammeterNo)
        {
            this.ammeterNo = _ammeterNo;
        }

        public string getAmmeterNo()
        {
            return this.ammeterNo;
        }

        public string getIdentificationCard()
        {
            return this.identificationCard;
        }

        public void setIdentificationCard(string _identificationCard)
        {
            this.identificationCard = _identificationCard;
        }

        public string getSignFlag()
        {
            return this.signFlag;
        }

        public void setSignFlag(string _signFlag)
        {
            this.signFlag = _signFlag;
        }
        private string      customerNo;
        private string      customerName;
        private string      customerAddress;
        private string         line;
        private string         area;
        private string      invoiceType;
        private string      electriCharacterName;
        private string      voltageFlag;
        private string      ammeterType;
        private string         ammeter;
        private string      bankCode;
        private string      bankName;
        private string      bankAccount;
        private string      tradeCode;
        private string      valueAddTaxNo;
        private string         organFlag;
        private string         especialFlag;
        private string lowProtectFlag;
        private string translossOrBaseprice;
        private string    customerInofDate;
        private string      password;
        private decimal customerPosition;
        private string phoneNum;
        private string ammeterNo;

        //身份证号
        private string identificationCard;
        
        //是否和银行签约
        private string signFlag;
    }
}
