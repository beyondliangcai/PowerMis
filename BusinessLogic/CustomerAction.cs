using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Data;
using BusinessModel;
using System.Data.SqlClient;
using DBUtility;
using Common;


namespace BusinessLogic
{
    public class CustomerAction
    {
        /**
         * 根据customerNo获取客户信息
         * @param       string customerNo：客户编号
         * @param       ref Customer customer：客户信息类的引用
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int getCustomerById(string customerNo, ref Customer customer)
        {
            if ("" != customerNo.Trim())
            {
                DataSet dataSet;
                try
                {
                    string strSql = "select * from CustomerInfo where CustomerNo = '" + customerNo.Trim() + "'";
                    dataSet = SQLUtl.Query(strSql);
                }
                catch (Exception)
                {
                   // MessageBox.Show(ex.Message.ToString());
                    return Constant.ERROR;
                }

                if (dataSet.Tables.Count != 0)
                {
                    DataTable dt = dataSet.Tables[0];
                    customer.setCustomerNo(dt.Rows[0].ItemArray[0] != null ? dt.Rows[0].ItemArray[0].ToString(): "");
                    customer.setCustomerName(dt.Rows[0].ItemArray[1] != null ? dt.Rows[0].ItemArray[1].ToString() : "");
                    //增加身份证信息
                    customer.setIdentificationCard(dt.Rows[0].ItemArray[2] != null ? dt.Rows[0].ItemArray[2].ToString() : "");
                    
                    customer.setCustomerAddress(dt.Rows[0].ItemArray[3] != null ? dt.Rows[0].ItemArray[3].ToString() : "");
                    customer.setLine(dt.Rows[0].ItemArray[4] != null ? dt.Rows[0].ItemArray[4].ToString() : "");
                    customer.setArea(dt.Rows[0].ItemArray[5] != null ? dt.Rows[0].ItemArray[5].ToString() : "");
                    /*
                    try
                    {
                        if (null != dt.Rows[0].ItemArray[3])
                        {
                            string lineSql = "select LineName from LineInfo where LineNum = " + dt.Rows[0].ItemArray[3].ToString();

                            DataSet lineSet = SQLUtl.Query(lineSql);
                            if (lineSet.Tables.Count != 0)

                                customer.setLine(lineSet.Tables[0].Rows[0].ItemArray[0] != null ? lineSet.Tables[0].Rows[0].ItemArray[0].ToString() : "");
                            else
                                customer.setLine("");

                        }
                        else
                            customer.setLine("");
                    }
                    catch (Exception)
                    {
                        customer.setLine("");
                    }

                    try
                    {
                        //customer.setArea(Int32.Parse(dt.Rows[0].ItemArray[4] != null ? dt.Rows[0].ItemArray[4].ToString() : ""));
                        if (null != dt.Rows[0].ItemArray[4])
                        {
                            string areaSql = "select AreaName from AreaInfo where AreaNo = " + dt.Rows[0].ItemArray[4];

                            DataSet areaSet = SQLUtl.Query(areaSql);
                            if (areaSet.Tables.Count != 0)

                                customer.setArea(areaSet.Tables[0].Rows[0].ItemArray[0] != null ? areaSet.Tables[0].Rows[0].ItemArray[0].ToString() : "");
                            else
                                customer.setArea("");

                        }
                        else
                            customer.setArea("");
                    }
                    catch (Exception)
                    {
                        customer.setArea("");
                    }
                    
                    */
                    customer.setInvoiceType(dt.Rows[0].ItemArray[6] != null ? dt.Rows[0].ItemArray[6].ToString() : "");
                    customer.setElectriCharacterName(dt.Rows[0].ItemArray[7] != null ? dt.Rows[0].ItemArray[7].ToString() : "");
                    customer.setVoltageFlag(dt.Rows[0].ItemArray[8] != null ? dt.Rows[0].ItemArray[8].ToString() : "");
                    customer.setAmmeterType(dt.Rows[0].ItemArray[9] != null ? dt.Rows[0].ItemArray[9].ToString() : "");
                    customer.setAmmeter("");
                    customer.setAmmeterNo(dt.Rows[0].ItemArray[10] != null ? dt.Rows[0].ItemArray[10].ToString() : "");
                    /*
                    try
                    {
                        //customer.setAmmeter(Int32.Parse(dt.Rows[0].ItemArray[9] != null ? dt.Rows[0].ItemArray[9].ToString() : "0"));
                        customer.setAmmeter("");
                    }
                    catch (Exception)
                    {
                    }

                    */
                    customer.setBankCode(dt.Rows[0].ItemArray[11] != null ? dt.Rows[0].ItemArray[11].ToString() : "");
                    customer.setBankName(dt.Rows[0].ItemArray[12] != null ? dt.Rows[0].ItemArray[12].ToString() : "");
                    customer.setBankAccount(dt.Rows[0].ItemArray[13] != null ? dt.Rows[0].ItemArray[13].ToString() : "");
                    //Console.Write("bankAccount:"+dt.Rows[0].ItemArray[12]);
                    customer.setSignFlag(dt.Rows[0].ItemArray[14] != null ? dt.Rows[0].ItemArray[14].ToString() : "0");

                    
                    customer.setTradeCode(dt.Rows[0].ItemArray[15] != null ? dt.Rows[0].ItemArray[15].ToString() : "");
                    customer.setValueAddTaxNo(dt.Rows[0].ItemArray[16] != null ? dt.Rows[0].ItemArray[16].ToString() : "");
                    customer.setOrganFlag(dt.Rows[0].ItemArray[17] != null ? dt.Rows[0].ItemArray[17].ToString() : "0");
                    customer.setEspecialFlag(dt.Rows[0].ItemArray[18] != null ? dt.Rows[0].ItemArray[18].ToString() : "0");
                    /*
                    try
                    {
                        //customer.setOrganFlag(Int32.Parse(dt.Rows[0].ItemArray[15] != null ? dt.Rows[0].ItemArray[15].ToString() : ""));
                        customer.setOrganFlag("");
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        //customer.setEspecialFlag(Int32.Parse(dt.Rows[0].ItemArray[16] != null ? dt.Rows[0].ItemArray[16].ToString() : ""));
                        customer.setEspecialFlag("");
                    }
                    catch (Exception)
                    {
                    }

                    */
                    customer.setLowProtectFlag(dt.Rows[0].ItemArray[19] != null ? dt.Rows[0].ItemArray[19].ToString() : "0");
                    customer.setTranslossOrBaseprice(dt.Rows[0].ItemArray[20] != null ? dt.Rows[0].ItemArray[20].ToString() : "0");

                    customer.setCustomerInfoDate(dt.Rows[0].ItemArray[21] != null ? dt.Rows[0].ItemArray[21].ToString() : "");

                    customer.setPassword(dt.Rows[0].ItemArray[22] != null ? dt.Rows[0].ItemArray[22].ToString() : "");
                    customer.setCustomerPosition((decimal)dt.Rows[0].ItemArray[23] != 0 ? (decimal)dt.Rows[0].ItemArray[23] : 0);
                    customer.setPhoneNum(dt.Rows[0].ItemArray[24] != null ? dt.Rows[0].ItemArray[24].ToString() : "");    
                 }
                 return Constant.OK;
             
            }
            else
                return Constant.ERROR;


        }


        /**
         * 根据customerNo获取客户的位置信息
         * @param       string customerNo：客户编号
         * @param       ref string customerPosition：客户位置信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int getCustomerPositionById(string customerNo, ref decimal customerPostion)
        {
            if ("" != customerNo.Trim())
            {
                string strSql = "select CustomerPosition from CustomerInfo where CustomerNo = '" + customerNo + "'";
                DataSet ds = null;
                try
                {

                    ds = SQLUtl.Query(strSql);
                }
                catch (Exception)
                {
                    return Constant.ERROR;
                }
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    //customerPostion = dt.Rows[0].ItemArray[0].ToString().Trim();
                    customerPostion = (decimal)dt.Rows[0].ItemArray[0];
                    return Constant.OK;
                }
                else
                {
                    return Constant.ERROR;
                }

            }
            else
                return Constant.ERROR;
        }

        /**
         * 根据customerNo获取客户计费的相关信息
         * @param       string customerNo：客户编号
         * @param       ref Countfeeinfo countfeeinfo：客户计费信息类的引用
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/ 
        public int getCountFeeInfoById(string customerNo, ref Countfeeinfo countfeeinfo)
        {
            string strSql = "select * from CountFeeInfo where CustomerNo = '" + customerNo + "'";
            DataSet ds = null;
            try
            {
                ds = SQLUtl.Query(strSql);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

            if (null != ds && ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = ds.Tables[0];

                countfeeinfo.setCustomerNo(null != dt.Rows[0].ItemArray[0] ? dt.Rows[0].ItemArray[0].ToString() : "");
                //countfeeinfo.setTransformerNo(null != dt.Rows[0].ItemArray[1] && dt.Rows[0].ItemArray[1].ToString().Trim() != "0".Trim() ? dt.Rows[0].ItemArray[1].ToString() : "空");

                /*查找变压器编号所对应的变压器名称*/
                /*
                if (!(countfeeinfo.getTransformerNo().Trim() == "0".Trim() || countfeeinfo.getTransformerNo().Trim() == "空".Trim()))
                {
                    string sqlForTF = "select * from TransformerInfo where TransformerNo = " + countfeeinfo.getTransformerNo().Trim() + "";
                    DataSet ds_tf = null;

                    try
                    {
                        ds_tf = SQLUtl.Query(sqlForTF);
                    }
                    catch (Exception)
                    {
                        //
                    }
                    if (null != ds_tf && ds_tf.Tables[0].Rows.Count != 0)
                    {
                        countfeeinfo.setTransformerNo(null != ds_tf.Tables[0].Rows[0].ItemArray[1] ? ds_tf.Tables[0].Rows[0].ItemArray[1].ToString() : "");
                    }

                }*/
                countfeeinfo.setTransformerNo(null != dt.Rows[0].ItemArray[1] ? dt.Rows[0].ItemArray[1].ToString() : "");
                countfeeinfo.setAmmeterMulti(null != dt.Rows[0].ItemArray[2] ? dt.Rows[0].ItemArray[2].ToString() : "");
                countfeeinfo.setAmmeterVolume(null != dt.Rows[0].ItemArray[3] ? dt.Rows[0].ItemArray[3].ToString() : "");
                countfeeinfo.setLineLoseRate(null != dt.Rows[0].ItemArray[4] ? dt.Rows[0].ItemArray[4].ToString() : "");
                countfeeinfo.setEssenceFee(null != dt.Rows[0].ItemArray[5] ? dt.Rows[0].ItemArray[5].ToString() : "");
                countfeeinfo.setCountFeeInfoDate(null != dt.Rows[0].ItemArray[6] ? dt.Rows[0].ItemArray[6].ToString() : "");
                countfeeinfo.setDiscountRate(null != dt.Rows[0].ItemArray[7] ? dt.Rows[0].ItemArray[7].ToString() : "");
                return Constant.OK;
            }
            else
            {
                return Constant.ERROR;
            }

            
        }


        /**
         * 将list中的信息更新到数据库中，list中对象排布为：CustomerInfo， CountFeeInfo， PowerRate（1到四个）
         * @param       string customerNo:客户编号
         * @param       ref List<Object> list 存储要更新的数据
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/
        public int updateInfoById(string customerNo, ref List<Object> list)
        {
            /* 更新数据的SQL语句 */
            string customerSql;
            string countfeeSql;
            

            List<string> sqlList = new List<string>();

            Customer customer = (Customer)list[0];
            /* 构造customerSql更新语句 */

            //[CustomerInfoDate] [datetime] NULL,	[password] [varchar](20) COLLATE Chinese_PRC_CI_AS NULL,
	        //[CustomerPosition] [decimal](30, 24) NOT NULL  这些信息没有更新
            customerSql = "update CustomerInfo set CustomerName = '" + customer.getCustomerName() + "', CustomerAddress = '" + customer.getCustomerAddress() + "', identificationCard = '" + customer.getIdentificationCard() + "',PhoneNum = '" + customer.getPhoneNum() + "', LineNum = " + customer.getLine() + ", AreaNo = " + customer.getArea() + ", InvoiceType = '" + customer.getInvoiceType() + "', ElectriCharacterName = '" + customer.getElectriCharacterName() + "', VoltageFlag = '" + customer.getVoltageFlag() + "', AmmeterType = '" + customer.getAmmeterType() + "',AmmeterNo = '" + customer.getAmmeterNo() + "',BankCode = '" + customer.getBankCode() + "',BankName = '" + customer.getBankName() + "',BankAccount = '" + customer.getBankAccount() + "',SignFlag = '" + customer.getSignFlag() + "', OrganFlag = " + customer.getOrganFlag() + ", EspecialFlag = " + customer.getEspecialFlag() + ", LowProtectFlag = " + customer.getLowProtectFlag() + ", TranslossOrBaseprice = " + customer.getTranslossOrBaseprice() + "  where CustomerNo = '" + customerNo + "'";
           // Console.Write(customerSql);
            sqlList.Add(customerSql);

            Countfeeinfo countfeeinfo = (Countfeeinfo)list[1];
            /* 构造countfeeinfo更新语句 */
            int count = SQLUtl.Query("select CustomerNo from CountFeeInfo where CustomerNo = '" + customerNo + "'").Tables[0].Rows.Count;
            if (count == 0)
            {
                countfeeSql = "insert into CountFeeInfo(CustomerNo,TransformerNo,AmmeterMulti,LineLoseRate) values('" + customerNo + "'," + countfeeinfo.getTransformerNo() + "," + countfeeinfo.getAmmeterMulti() + "," + countfeeinfo.getLineLoseRate() + ") ";
            }
            else
            {
                countfeeSql = "update CountFeeInfo set TransformerNo = " + countfeeinfo.getTransformerNo() + ", AmmeterMulti = " + countfeeinfo.getAmmeterMulti() + ", LineLoseRate = " + countfeeinfo.getLineLoseRate() + ", DiscountRate= " + countfeeinfo.getDiscountRate() + "  where CustomerNo = '" + customerNo + "'";
            }
            sqlList.Add(countfeeSql);

            
            int i = list.Count;
            if (i > 2)
            {
                string sqlDelete = "delete from PriceRate where CustomerNo = '" + customerNo + "'";
                sqlList.Add(sqlDelete);

                /* 由于修改的数目不定，根据list中存入多少个PriceRate对象来生成SQL语句 */
                while ((i - 2) != 0)
                {
                    string sqlInsert = "insert into PriceRate values ('" + customerNo + "', " + ((PriceRate)list[i - 1]).PriceNo + ", " + ((PriceRate)list[i - 1]).Rate + ", null)";
                    sqlList.Add(sqlInsert);
                    i = i - 1;
                }
            }
             

            try
            {
                SQLUtl.ExecuteSqlTran(sqlList);
            }
            catch (Exception e)
            {
               // MessageBox.show(e.ToString());
               // Console.Write(e.ToString());
              //  DebugCustomerAction debugCustomerAction = new DebugCustomerAction(e);
              //  debugCustomerAction.Show();
                return Constant.ERROR;
            }

            
            

            return Constant.OK;
        }

        /**
         * 添加客户信息到数据库中
         * @param       List<Object> list：添加到数据库中的数据集
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/
        public int addCustomerInfo(ref List<Object> list)
        {
            Customer customer = (Customer)list[0];
            Countfeeinfo countfeeinfo = (Countfeeinfo)list[1];
            PriceRate priceRate = null;

            //在CustomerInfo表中新增signFlag项
            string customerSql = "insert into CustomerInfo values('" + customer.getCustomerNo() + "', '" + customer.getCustomerName() + "', '" + customer.getIdentificationCard() + "',, '" + customer.getCustomerAddress() + "', " + customer.getLine() + ", " + customer.getArea() + ", '" + customer.getInvoiceType() + "', '" + customer.getElectriCharacterName() + "', '" + customer.getVoltageFlag() + "', '" + customer.getAmmeterType() + "', '" + customer.getAmmeterNo() + "', '" + customer.getBankCode() + "', '" + customer.getBankName() + "', '" + customer.getBankAccount() + "', '" + customer.getSignFlag() + "', '" + customer.getTradeCode() + "', '" + customer.getValueAddTaxNo() + "', " + customer.getOrganFlag() + ", " + customer.getEspecialFlag() + "," + customer.getLowProtectFlag() + "," + customer.getTranslossOrBaseprice() + ", getdate(), null, " + customer.getCustomerPosition() + ",'" + customer.getPhoneNum() + "')";
            string countfeeSql = "insert into CountFeeInfo values('" + countfeeinfo.getCustomerNo() + "', " + countfeeinfo.getTransformerNo() + ", " + countfeeinfo.getAmmeterMulti() + ", null, " + countfeeinfo.getLineLoseRate() + ", 0, getDate(), " + countfeeinfo.getDiscountRate() + ")";

            List<string> sqlList = new List<string>();
            sqlList.Add(customerSql);
            sqlList.Add(countfeeSql);

            int nums = list.Count;

            while (nums > 2)
            {
                priceRate = (PriceRate)list[nums - 1];

                string priceSql = "insert into PriceRate values('" + priceRate.PriceName + "'," + priceRate.PriceNo + ", " + priceRate.Rate + ", getdate())";
                sqlList.Add(priceSql);
                nums = nums - 1;
            }
            try
            {
                SQLUtl.ExecuteSqlTran(sqlList);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 判断customerNo的编号是否存在
         * @param       string customerNo：客户编号
         * @param       ref bool state:值为True：是唯一性的；值为False：该编号已存在；
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/
        public int customerNoChecking(string customerNo, ref bool state)
        {
            string strSql = "select count(*) from CustomerInfo where CustomerNo = '" + customerNo + "'";

            DataSet ds = null;

            try
            {
                ds = SQLUtl.Query(strSql);
                if (int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim()) > 0)
                    state = false;
                else
                    state = true;
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

        }

        /**
         * 判断customerNo的所在账簿是否存在
         * @param       string customerNo：客户编号
         * @param       ref bool state:值为True：不存在；值为False：该编号已存在；
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/
        public int bookTypeChecking(string customerNo, ref bool state)
        {
            string bookType = customerNo.Substring(0, 5);
            string strSql = "select count(*) from CustomerInfo where SUBSTRING(CustomerNo, 1, 5) = '" + bookType + "'";
            DataSet ds = null;

            try
            {
                ds = SQLUtl.Query(strSql);
                if (int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim()) > 0)
                    state = false;
                else
                    state = true;
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

        }


        /**
         * 删除该客户信息
         * @param       string customerNo：客户编号
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/ 
        public int deleteCustomerInfo(string customerNo)
        {
            string deleteCustomer = "delete from CustomerInfo where CustomerNo = '" + customerNo + "'";
            string deleteCountfeeinfo = "delete from CountFeeInfo where CustomerNo = '" +customerNo + "'";
            string deletePriceInfo = "delete from PriceRate where CustomerNo = '" + customerNo + "'";
            string deleteAmmeterValue = "delete from AmmeterValue where CustomerNo = '" + customerNo + "'";
            string deleteCountfee = "delete from Countfee where CustomerNo = '" + customerNo + "'";

            List<string> list = new List<string>();

            list.Add(deleteCustomer);
            list.Add(deleteCountfeeinfo);
            list.Add(deletePriceInfo);

            try
            {
                SQLUtl.ExecuteSqlTran(list);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

        }

        /**
         * 更改用户的账簿
         * @param       string customerNo_old：客户原有编号
         * @param       string customerNo_new：客户编号新编号
         * @param       string position：位置信息
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         **/ 
        public int changeCustomerSection(string customerNo_old, string customerNo_new, decimal position)
        {
            List<string> list = new List<string>();
            
            string updateCustomer = "update CustomerInfo set CustomerNo = '" + customerNo_new + "', CustomerPosition = " + position + " where CustomerNo = '" + customerNo_old + "'" ; //更新客户信息表
            string updateCountfeeinfo = "update countfeeinfo set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'"; //更新客户信息表
            string updateAbnormityFee = "update abnormityfee set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateAmmeterValue = "update ammetervalue set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateCountFee = "update countfee set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateCountryInvoice = "update countryinvoice set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updatePriceRate = "update pricerate set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updatePrinttable = "update printtable set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateTemp_CityArrear = "update temp_cityarrear set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateTemp_CityInvoice = "update temp_countryinvoice set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updateTemp_CountryInvoice = "update temp_countryinvoice set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updatetemp_hingepower = "update temp_hingepower set CustomerNo = '" + customerNo_new + "' where CustomerNo = '" + customerNo_old + "'";
            string updatetemp_powerfeelist = "update temp_powerfeelist set CustomerNo1 = '" + customerNo_new + "' where CustomerNo1 = '" + customerNo_old + "'";
            
            list.Add(updateCustomer);
            list.Add(updateCountfeeinfo);
            list.Add(updateAbnormityFee);
            list.Add(updateAmmeterValue);
            list.Add(updateCountFee);
            
            list.Add(updateCountryInvoice);
            list.Add(updatePriceRate);
            list.Add(updatePrinttable);
            list.Add(updateTemp_CityArrear);
            list.Add(updateTemp_CityInvoice);
            list.Add(updateTemp_CountryInvoice);
            list.Add(updatetemp_hingepower);
            list.Add(updatetemp_powerfeelist);
            
            

            try
            {
                SQLUtl.ExecuteSqlTran(list);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

            return Constant.OK;
        }



        /**
         * 校验客户帐号是否存在
         * @param       string customerNo：客户编号
         * @param       ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkCustomerNo(string customerNo, ref bool state)
        {
            string strCheck = "select count(*) from CustomerInfo where CustomerNo = '" + customerNo + "'";
            try
            {
                DataSet ds = SQLUtl.Query(strCheck);
                if (int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim()) > 0)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

        }


        /**
         * 添加计费信息到数据库中
         * @param       ref Countfeeinfo countfeeinfo：计费信息对象
         * @return      int 值为Constant.OK:执行成功，值为Constant.ERROR为执行出错
         * @author      Rick
         *
        public int addCountfeeinfo(ref Countfeeinfo countfeeinfo)
        {
            
            try
            {
                if (SQLUtl.ExecuteSql(insertSql) > 0 )
                    return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
            
        }*/



    }
}
