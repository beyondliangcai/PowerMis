using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using Common;
using BusinessLogic;
using BusinessModel;

namespace PowerMis.CustomInfo
{
    public partial class SearchCustom : Form
    {
        public SearchCustom()
        {
            InitializeComponent();
            this.customerAction = new CustomerAction();
        }

        /**
        * 点击CustomerInfoList中选项时，显示选中客户编号的客户信息
        * @param   object sender
        * @param   EvenArgs e
        * @return  void
        * @author  Rick
        **/
        private void CustomInfoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.CommonInfo
            //this.data_clear_all();
            //获取选中的客户编号值
            string customerNo = this.CustomInfoList.SelectedItem.ToString().Split(' ')[3];
            //List<string> list = new List<string>();
            //list.Add(customerNo);
            this.showData(customerNo, 1);            
            
        }


        /**
         * 展示list中包含的客户编号的所有信息
         * @param   string key ：查询用的关键字，type = 1 ：客户编号；type = 2：客户姓名。
         * @author  Rick
         **/ 
        private int showData(string key, int type)
        {
            if (1 == type)
            {
                string sql = "select CustomerNo, Customername,identificationCard,customeraddress,PhoneNum, lineName, areaname, invoicetype, electricharactername, voltageflag, ammetertype, ammeterNo,Lowprotectflag, translossorbaseprice, customerinfodate from customerinfo, lineinfo, areaInfo where customerInfo.linenum = lineinfo.linenum and customerinfo.areano = areaInfo.areano and  CustomerNo = '" + key + "'";
                try
                {
                    DataSet ds = SQLUtl.Query(sql);
                    DataTable dt = ds.Tables[0];
                    this.CommonInfo.DataSource = dt;
                    this.CommonInfo.Columns["CustomerNo"].HeaderText = "客户编号";
                    this.CommonInfo.Columns["Customername"].HeaderText = "客户姓名";

                    //添加身份证信息
                    this.CommonInfo.Columns["identificationCard"].HeaderText = "身份证"; 

                    this.CommonInfo.Columns["customeraddress"].HeaderText = "客户地址";                 

                    this.CommonInfo.Columns["PhoneNum"].HeaderText = "联系电话";
                    this.CommonInfo.Columns["lineName"].HeaderText = "线路";
                    this.CommonInfo.Columns["areaname"].HeaderText = "台区";
                    this.CommonInfo.Columns["invoicetype"].HeaderText = "发票类型";
                    this.CommonInfo.Columns["electricharactername"].HeaderText = "用电类型";
                    this.CommonInfo.Columns["voltageflag"].HeaderText = "高压低压";
                    this.CommonInfo.Columns["ammetertype"].HeaderText = "电表类型";
                    this.CommonInfo.Columns["ammeterNo"].HeaderText = "电表号";
                    this.CommonInfo.Columns["Lowprotectflag"].HeaderText = "是否低保户";
                    this.CommonInfo.Columns["translossorbaseprice"].HeaderText = "是否收取基本电费";
                    this.CommonInfo.Columns["customerinfodate"].HeaderText = "录入或更新日期";
                    //return Constant.OK;
                }
                catch(Exception)
                {
                    return Constant.ERROR;
                }

                string priceSql = "select customerNo, PowerPriceName, PowerPrice, PriceRate from PriceRate, PowerPriceInfo where PowerPriceInfo.PowerPriceNo = PriceRate.PowerPriceNo and CustomerNo = '" + key + "'";
                try
                {
                    DataSet ds = SQLUtl.Query(priceSql);
                    DataTable dt = ds.Tables[0];
                    this.PriceInfo.DataSource = dt;
                    this.PriceInfo.Columns["CustomerNo"].HeaderText = "客户编号";
                    this.PriceInfo.Columns["PowerPriceName"].HeaderText = "电价名称";
                    this.PriceInfo.Columns["PowerPrice"].HeaderText = "电价";
                    this.PriceInfo.Columns["PriceRate"].HeaderText = "电价比率(%)";
                   

                    
                }
                catch (Exception)
                {
                    return Constant.ERROR;
                }

                string countSql = "select CustomerNo, TransformerNo, AmmeterMulti, LineLoseRate, EssenceFee from CountFeeInfo where CustomerNo = '" + key + "'";
                try
                {
                    DataSet ds = SQLUtl.Query(countSql);
                    DataTable dt = ds.Tables[0];
                    this.Countfeeinfo.DataSource = dt;


                    this.Countfeeinfo.Columns["CustomerNo"].HeaderText = "客户编号";
                    this.Countfeeinfo.Columns["TransformerNo"].HeaderText = "电压器类型";
                    this.Countfeeinfo.Columns["AmmeterMulti"].HeaderText = "电表倍率";
                    this.Countfeeinfo.Columns["LineLoseRate"].HeaderText = "线损率(%)";
                    this.Countfeeinfo.Columns["EssenceFee"].HeaderText = "基本电费";

                }
                catch (Exception)
                {
                    return Constant.ERROR;
                }
                
                string bankSql = "select bankcode, bankname, bankaccount,signflag, especialflag from customerinfo where CustomerNo = '" + key + "'";
                try
                {
                    DataSet ds = SQLUtl.Query(bankSql);
                    DataTable dt = ds.Tables[0];
                    this.otherInfo.DataSource = dt;

                    this.otherInfo.Columns["bankcode"].HeaderText = "银行代码";
                    this.otherInfo.Columns["bankname"].HeaderText = "银行名称";
                    this.otherInfo.Columns["bankaccount"].HeaderText = "银行账号";
                    this.otherInfo.Columns["signflag"].HeaderText = "是否签约";
                 
                    //   this.otherInfo.Columns["tradecode"].HeaderText = "交易号";
                  //  this.otherInfo.Columns["valueaddTaxno"].HeaderText = "税号";
                 //   this.otherInfo.Columns["organflag"].HeaderText = "sdg";
                    this.otherInfo.Columns["especialflag"].HeaderText = "是否局内";
                    //this.otherInfo.Columns[""].HeaderText = "";
                }
                catch (Exception)
                {
                    return Constant.ERROR;
                }
                
                return Constant.OK;
            }
            else if (2 == type)
            {
                DataTable dt = null;
                string sql = "select CustomerNo, Customername,identificationCard, customeraddress, PhoneNum, lineName, areaname, invoicetype, electricharactername, voltageflag, ammetertype, ammeterNo,Lowprotectflag, translossorbaseprice, customerinfodate from customerinfo, lineinfo, areaInfo where customerInfo.linenum = lineinfo.linenum and customerinfo.areano = areaInfo.areano and  CustomerName like '%" + key + "%' order by CustomerNo asc";
                //string bankSql = "select bankcode, bankname, bankaccount,signflag, especialflag from customerinfo where CustomerNo = '%" + key + "%' order by CustomerNo asc";
                //string countSql = "select CustomerNo, TransformerNo, AmmeterMulti, LineLoseRate, EssenceFee from CountFeeInfo where CustomerNo = '%" + key + "%' order by CustomerNo asc";
                //string priceSql = "select customerNo, PowerPriceName, PowerPrice, PriceRate from PriceRate, PowerPriceInfo where PowerPriceInfo.PowerPriceNo = PriceRate.PowerPriceNo and CustomerNo = '%" + key + "%' order by CustomerNo asc";
              
                try
                {
                    DataSet ds = SQLUtl.Query(sql);
                    dt = ds.Tables[0];
                    this.CommonInfo.DataSource = dt;
                    this.CommonInfo.Columns["CustomerNo"].HeaderText = "客户编号";
                    this.CommonInfo.Columns["Customername"].HeaderText = "客户姓名";
                    //添加身份证信息
                    this.CommonInfo.Columns["identificationCard"].HeaderText = "身份证"; 
                    this.CommonInfo.Columns["customeraddress"].HeaderText = "客户地址";
                    this.CommonInfo.Columns["PhoneNum"].HeaderText = "联系电话";
                    this.CommonInfo.Columns["lineName"].HeaderText = "线路";
                    this.CommonInfo.Columns["areaname"].HeaderText = "台区";
                    this.CommonInfo.Columns["invoicetype"].HeaderText = "发票类型";
                    this.CommonInfo.Columns["electricharactername"].HeaderText = "用电类型";
                    this.CommonInfo.Columns["voltageflag"].HeaderText = "高压低压";
                    this.CommonInfo.Columns["ammetertype"].HeaderText = "电表类型";
                    this.CommonInfo.Columns["ammeterNo"].HeaderText = "电表号";
                    this.CommonInfo.Columns["Lowprotectflag"].HeaderText = "是否低保户";
                    this.CommonInfo.Columns["translossorbaseprice"].HeaderText = "是否收取基本电费";
                    this.CommonInfo.Columns["customerinfodate"].HeaderText = "录入或更新日期";


                   
                }
                catch (Exception)
                {
                    return Constant.ERROR;
                }

                if (dt != null && dt.Rows.Count > 0)
                {

                    int i = dt.Rows.Count;
                    int j = 0;
                    string selectPriceInfo = "select customerNo, PowerPriceName, PowerPrice, PriceRate from PriceRate, PowerPriceInfo where PowerPriceInfo.PowerPriceNo = PriceRate.PowerPriceNo and ( ";
                    string selectCountfeeinfo = "select CustomerNo, TransformerNo, AmmeterMulti, LineLoseRate, EssenceFee from CountFeeInfo where ";
                    string selectBankInfo = "select bankcode, bankname, bankaccount,signflag, especialflag from customerinfo where ";
               
                    if (i > 0)
                    {
                        selectPriceInfo = selectPriceInfo + "CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        selectCountfeeinfo = selectCountfeeinfo + "CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        selectBankInfo = selectBankInfo + "CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        
                        i = i - 1;
                        j = j + 1;
                    }
                    
                    while (i > 0)
                    {
                        selectPriceInfo = selectPriceInfo + " or CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        selectCountfeeinfo = selectCountfeeinfo + " or CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        selectBankInfo = selectBankInfo + " or CustomerNo = '" + dt.Rows[j].ItemArray[0].ToString().Trim() + "'";
                        j = j + 1;
                        i = i - 1;
                    }
                    selectPriceInfo = selectPriceInfo + ") order by CustomerNo asc";

                    selectCountfeeinfo = selectCountfeeinfo + " order by CustomerNo asc";
                    selectBankInfo = selectBankInfo + " order by CustomerNo asc";
                    try
                    {
                        DataSet ds_1 = SQLUtl.Query(selectPriceInfo);
                        DataTable _dt_1 = ds_1.Tables[0];
                        this.PriceInfo.DataSource = _dt_1;
                        this.PriceInfo.Columns["CustomerNo"].HeaderText = "客户编号";
                        this.PriceInfo.Columns["PowerPriceName"].HeaderText = "电价名称";
                        this.PriceInfo.Columns["PowerPrice"].HeaderText = "电价";
                        this.PriceInfo.Columns["PriceRate"].HeaderText = "电价比率(%)";

                        DataSet ds_2 = SQLUtl.Query(selectCountfeeinfo);
                        DataTable _dt_2 = ds_2.Tables[0];
                        this.Countfeeinfo.DataSource = _dt_2;                       
                        this.Countfeeinfo.Columns["CustomerNo"].HeaderText = "客户编号";
                        this.Countfeeinfo.Columns["TransformerNo"].HeaderText = "电压器类型";
                        this.Countfeeinfo.Columns["AmmeterMulti"].HeaderText = "电表倍率";
                        this.Countfeeinfo.Columns["LineLoseRate"].HeaderText = "线损率(%)";
                        this.Countfeeinfo.Columns["EssenceFee"].HeaderText = "基本电费";

                        DataSet ds_3 = SQLUtl.Query(selectBankInfo);
                        DataTable _dt_3 = ds_3.Tables[0];
                        this.otherInfo.DataSource = _dt_3;
                        this.otherInfo.Columns["bankcode"].HeaderText = "银行代码";
                        this.otherInfo.Columns["bankname"].HeaderText = "银行名称";
                        this.otherInfo.Columns["bankaccount"].HeaderText = "银行账号";
                        this.otherInfo.Columns["signflag"].HeaderText = "是否签约";
                        this.otherInfo.Columns["especialflag"].HeaderText = "是否局内";
                       
                    }
                    catch (Exception)
                    {
                        return Constant.ERROR;
                    }
                    return Constant.OK;

                }
                else
                    return Constant.OK;
                //return Constant.OK;
            }
            else
            {
                return Constant.ERROR;
            }

        }

        /**
         * 当BookType列表框中内容改变时，发生此事件，主要功能是根据变化的值，显示相对应的客户编号
         * @param object sender
         * @param EvenArgs e
         * @return void
         * @author Rick
         **/
        private void BookType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            string key = this.BookType.SelectedItem.ToString();


            if ("" != key.Trim())
            {

                this.customerinfolist_data_fill(key);
                this.CustomInfoList.Refresh();
            }
        }

        /**
         * 填充账本信息
         * @return      int 返回值为0，则表示执行成功；返回值为负数，则表示执行失败。 
         * @author      Rick
         */
        private int booktype_data_fill()
        {
            try
            {
                //查询所有册的编号
                string strSql = "select distinct SUBSTRING(CustomerNo, 1, 5) as t   from CustomerInfo  order by  t asc";
                DataSet dataSet = SQLUtl.Query(strSql);
                DataTable dataTable = dataSet.Tables["dataSet"];
                if (dataTable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        this.BookType.Items.Add(dr.ItemArray[0].ToString());
                    }
                    this.BookType.SelectedIndex = 0;
                    this.BookType.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return Constant.ERROR;
            }
            return Constant.OK;
        }

        /**
         * 根据账本号显示相应的客户信息
         * @param       string 账本号编码
         * @return      int 返回值为0，则表示执行成功；返回值为负数，则表示执行失败。
         * @authoer     Rick
         */
        private int customerinfolist_data_fill(string booktype)
        {
            string No = "";
            if ("" == booktype.Trim() || booktype == null)
            {
                string defaultKey = "";

                //获取列表中第一个账本编号
                try
                {
                    string strSql = "select distinct TOP 1  SUBSTRING(CustomerNo, 1, 5) as t   from CustomerInfo  order by  t asc";
                    DataSet dataSet = SQLUtl.Query(strSql);
                    DataTable dataTable = dataSet.Tables["dataSet"];

                    if (dataTable.Rows.Count != 0)
                        defaultKey = dataTable.Rows[0].ItemArray[0].ToString();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                    return Constant.ERROR;
                }
                try
                {
                    //查询第一个账本号的客户编号集
                    if ("" != defaultKey.Trim())
                    {
                        string strSql = "select CustomerNo from CustomerInfo  where substring(CustomerNo, 1, 5) = '" + defaultKey + "' order by CustomerPosition asc";
                        DataSet dataSet = SQLUtl.Query(strSql);
                        DataTable dataTable = dataSet.Tables["dataSet"];

                        if (dataTable.Rows.Count != 0)
                        {
                            int i = 1;
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                if (i < 10)
                                {
                                    No = "00" + i;
                                }
                                else if (i >= 10 && i < 100)
                                {
                                    No = "0" + i;
                                }
                                else if (i >= 100 & i < 1000)
                                {
                                    No = "" + i;
                                }
                                this.CustomInfoList.Items.Add( No + "   " + dr.ItemArray[0].ToString());
                                i++;
                                //this.lbxNo.Items.Add(i++);
                            }
                        }
                        this.CustomInfoList.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return Constant.ERROR;
                }
                return Constant.OK;
            }
            else
            {
                try
                {
                    //查询给定账本号的客户编号集
                    string strSql = "select CustomerNo from CustomerInfo  where substring(CustomerNo, 1, 5) = '" + booktype + "' order by CustomerPosition asc";
                    DataSet dataSet = SQLUtl.Query(strSql);
                    DataTable dataTable = dataSet.Tables["dataSet"];
                    this.CustomInfoList.Items.Clear();
                    if (dataTable.Rows.Count != 0)
                    {
                        int i = 1;
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            if (i < 10)
                            {
                                No = "00" + i;
                            }
                            else if (i >= 10 && i < 100)
                            {
                                No = "0" + i;
                            }
                            else if (i >= 100 & i < 1000)
                            {
                                No = "" + i;
                            }
                            this.CustomInfoList.Items.Add(No + "   " + dr.ItemArray[0].ToString());
                            i++;
                            //this.CustomInfoList.Items.Add(dr.ItemArray[0].ToString());
                        }
                    }
                    this.CustomInfoList.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return Constant.ERROR;
                }
            }
            return Constant.OK;
        }
        private void SearchCustom_Load(object sender, EventArgs e)
        {
            this.booktype_data_fill();
            //this.customerinfolist_data_fill("");
        }

        private CustomerAction customerAction;

        private void search_button_Click(object sender, EventArgs e)
        {
            if ("" != this.CustomerName.Text.ToString().Trim())
            {
                this.showData(this.CustomerName.Text.ToString().Trim(), 2);
                this.CustomerName.Text = "";
            }
            else
            {
                MessageBox.Show("请输入要查询的客户姓名！");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BookType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                customerinfolist_data_fill(this.BookType.Text.ToString());
            }
        }

        private void search_button2_Click(object sender, EventArgs e)
        {
            if ("" != this.txtCustomerNo.Text.ToString().Trim())
            {
                this.showData(this.txtCustomerNo.Text.ToString().Trim(), 1);
                this.txtCustomerNo.Text = "";
            }
            else
            {
                MessageBox.Show("请输入要查询的客户编号！");
                return;
            }
        }



    }
}
