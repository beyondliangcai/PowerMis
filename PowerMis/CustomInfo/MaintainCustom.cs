using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBUtility;
using BusinessModel;
using Common;
using BusinessLogic;

namespace PowerMis.CustomInfo
{
    public partial class MaintainCustom : Form
    {
        private CustomerAction customerAction = null;
        public MaintainCustom()
        {
            InitializeComponent();
            customerAction = new CustomerAction();

        }


        /**
         * 使得各个文本区的内容不可改变
         * @author      Rick
         **/ 
        public void make_textfield_unchange()
        {
            
        }

        private int data_clear_all()
        {
            this.customerName.Text = "";
            this.CustomerAddress.Text = "";
            this.PowerRate_1.Text = "";
            this.PowerRate_2.Text = "";
            this.PowerRate_3.Text = "";
            this.PowerRate_4.Text = "";
            this.lineChange.Text = "";
            this.ammeterMutiple.Text = "";


            this.InvoiceType.SelectedIndex = -1;
            this.Transformer.SelectedIndex = -1;
            this.ElectriType.SelectedIndex = -1;
            this.LineBox.SelectedIndex = -1;
            this.AreaBox.SelectedIndex = -1;
            this.VoltageFlag.SelectedIndex = -1;
            this.cbxAmmeterType1.SelectedIndex = -1;
            this.txtAmmeterNo1.Text = "";
            this.PowerPrice_1.SelectedIndex = -1;
            this.PowerPrice_2.SelectedIndex = -1;
            this.PowerPrice_3.SelectedIndex = -1;
            this.PowerPrice_4.SelectedIndex = -1;


            return Constant.OK;
            
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
                    foreach(DataRow dr in dataTable.Rows)
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
                                this.CustomInfoList.Items.Add(No + "   " + dr.ItemArray[0].ToString());
                                i++;
                                //this.CustomInfoList.Items.Add(dr.ItemArray[0].ToString());
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

        /**
         * 拉取线路信息
         * @return int 返回Constant.OK 执行成功，返回Constant.ERROR 执行失败
         * @author Rick
         **/
        private int lineBox_data_fill()
        {
            string sql = "select LineNum, LineName from LineInfo";
            DataSet ds;
            try
            {

                ds = SQLUtl.Query(sql);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
            this.LineBox.DataSource = ds.Tables[0];
            this.LineBox.DisplayMember = "LineName";
            this.LineBox.ValueMember = "LineNum";
            //this.LineBox.Items.Add("");
            this.LineBox.SelectedIndex = -1;

            return Constant.OK;
        }

        /**
         * 拉取台区信息列表
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/ 
        private int areaBox_data_fill()
        {
            string sql = "select AreaNo, AreaName from AreaInfo";
            DataSet ds;
            try
            {

                ds = SQLUtl.Query(sql);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
            
            this.AreaBox.DataSource = ds.Tables[0];
            this.AreaBox.DisplayMember = "AreaName";
            this.AreaBox.ValueMember = "AreaNo";
            this.AreaBox.SelectedIndex = -1;
            /*
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.AreaBox.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.AreaBox.SelectedIndex = -1;*/
            return Constant.OK;
        
        }


        /**
         * 获取变压器的信息列表
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/ 
        private int transformer_data_fill()
        {
            string sql = "select TransformerNo, TransformerName from TransformerInfo";
            DataSet ds;
            try
            {

               ds = SQLUtl.Query(sql);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
            this.Transformer.DataSource = ds.Tables[0];
            this.Transformer.DisplayMember = "TransformerName";
            this.Transformer.ValueMember = "TransformerNo";
            this.Transformer.SelectedIndex = -1;

            return Constant.OK;
        }

        /**
         * 获取电压类型
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/
        private int voltageFlag_data_fill()
        {
            this.VoltageFlag.Items.Add("高");
            this.VoltageFlag.Items.Add("低");
            this.VoltageFlag.SelectedIndex = -1;
            return Constant.OK;
        }
        /**
         * 获取发票类型
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/ 
        private int invoice_data_fill()
        {
            this.InvoiceType.Items.Add("普通发票");
            this.InvoiceType.Items.Add("增值税发票");
            this.InvoiceType.Items.Add("农网发票");
            this.InvoiceType.SelectedIndex = -1;
            return Constant.OK;
        }

        /**
         * 获取用电所在网的类型
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/ 
        private int electriType_data_fill()
        {
            this.ElectriType.Items.Add("农网");
            this.ElectriType.Items.Add("城网");
            this.ElectriType.Items.Add("局内");
            this.ElectriType.SelectedIndex = -1;
            return Constant.OK;
        }

        private int ammeterType_data_fill()
        {
            this.cbxAmmeterType1.Items.Add("单相");
            this.cbxAmmeterType1.Items.Add("三相");
            this.ElectriType.SelectedIndex = -1;
            return Constant.OK;
        }
        /**
         * 获取用电类型比例
         * @return int 返回Constant.OK 执行成功， 返回Constant.ERROR 执行失败
         * @author Rick
         **/ 
        private int powerRate_data_fill()
        {
            string sql = "select PowerPriceNo, PowerPriceName from PowerPriceInfo";
            DataSet ds;
            try
            {

                ds = SQLUtl.Query(sql);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

            DataTable dt_1 = new DataTable();
            dt_1 = ds.Tables[0].Copy();
            this.PowerPrice_1.DataSource = dt_1;
            this.PowerPrice_1.DisplayMember = "PowerPriceName";
            this.PowerPrice_1.ValueMember = "PowerPriceNo";
            this.PowerPrice_1.SelectedIndex = -1;

            DataTable dt_2 = new DataTable();
            dt_2 = ds.Tables[0].Copy();
            this.PowerPrice_2.DataSource = dt_2;
            this.PowerPrice_2.DisplayMember = "PowerPriceName";
            this.PowerPrice_2.ValueMember = "PowerPriceNo";
            this.PowerPrice_2.SelectedIndex = -1;

            DataTable dt_3 = new DataTable();
            dt_3 = ds.Tables[0].Copy();
            this.PowerPrice_3.DataSource = dt_3;
            this.PowerPrice_3.DisplayMember = "PowerPriceName";
            this.PowerPrice_3.ValueMember = "PowerPriceNo";
            this.PowerPrice_3.SelectedIndex = -1;

            DataTable dt_4 = new DataTable();
            dt_4 = ds.Tables[0].Copy();
            this.PowerPrice_4.DataSource = dt_4;
            this.PowerPrice_4.DisplayMember = "PowerPriceName";
            this.PowerPrice_4.ValueMember = "PowerPriceNo";
            this.PowerPrice_4.SelectedIndex = -1;

            return Constant.OK;
        }
        private void MaintainCustom_Load(object sender, EventArgs e)
        {
            this.booktype_data_fill();
            //this.customerinfolist_data_fill("");
            this.invoice_data_fill();
            this.voltageFlag_data_fill();
            this.areaBox_data_fill();
            this.lineBox_data_fill();
            this.transformer_data_fill();
            this.electriType_data_fill();
            this.powerRate_data_fill();
            this.ammeterType_data_fill();
            this.make_textfield_unchange();

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

            this.data_clear_all();
            //获取选中的客户编号值
            string customerNo = this.CustomInfoList.SelectedItem.ToString().Split(' ')[3];

            this.customer = new Customer();
            this.countfeeinfo = new Countfeeinfo();
           

            //查询该客户的信息
            if ("" != customerNo)
            {
                if (Constant.OK == this.customerAction.getCustomerById(customerNo, ref customer))
                {
                    //将Customer类中的值显示在各个文本区中
                    //MessageBox.Show(customer.getCustomerName());
                    this.customerName.Text = customer.getCustomerName();
                    this.CustomerAddress.Text = customer.getCustomerAddress();

                    this.LineBox.SelectedValue = customer.getLine();
                    this.AreaBox.SelectedValue = customer.getArea();

                    this.InvoiceType.SelectedItem = customer.getInvoiceType();

                    this.ElectriType.SelectedItem = customer.getElectriCharacterName();

                    this.VoltageFlag.SelectedItem = customer.getVoltageFlag();

                    this.cbxAmmeterType1.SelectedItem = customer.getAmmeterType();
                    this.txtAmmeterNo1.Text = customer.getAmmeterNo();

                    this.BankCode.Text = customer.getBankCode();
                    this.Bank.Text = customer.getBankName();
                    this.BankAccount.Text = customer.getBankAccount();
                    if ("1" == customer.getSignFlag())
                    {
                        this.Sign.Checked = true;
                        this.UnSign.Checked = false;
                    }
                    else {
                        this.Sign.Checked = false;
                        this.UnSign.Checked = true;
                    }                    

                
                    this.txtPhoneNum.Text = customer.getPhoneNum();

                    if ("1" == customer.getLowProtectFlag().Trim())
                    {
                        this.LowProtect.Checked = true;
                    }
                    else
                    {
                        this.LowProtect.Checked = false;
                    }

                    if ("1" == customer.getTranslossOrBaseprice().Trim())
                    {
                        this.BasicEFee.Checked = true;
                    }
                    else
                    {
                        this.BasicEFee.Checked = false;
                    }

                    //this.line.Text = customer.getLine();
                    //this.area.Text = customer.getArea();
                    //this.invoiceNo.Text = customer.getInvoiceType();
                    //this.electriType.Text = customer.getElectriCharacterName();
                    //this.voltage.Text = customer.getVoltageFlag();


                }
                //else
                    //MessageBox.Show("error");
                if (Constant.OK == this.customerAction.getCountFeeInfoById(customerNo, ref countfeeinfo))
                {
                    this.ammeterMutiple.Text = countfeeinfo.getAmmeterMulti();
                    this.lineChange.Text = countfeeinfo.getLineLoseRate();
                    this.Transformer.SelectedValue = countfeeinfo.getTransformerNo();

                }
                //else //调试用
                  ///  MessageBox.Show("error");

                /* 查询电价信息 */

                string priceSQL = "select PowerPriceNo, PriceRate from PriceRate where CustomerNo = '" + customerNo.Trim() + "'";

                try
                {
                    DataSet ds = SQLUtl.Query(priceSQL);
                    DataTable dt = ds.Tables[0];

                    int rows = dt.Rows.Count;
                    
                    while (rows != 0)
                    {
                        //MessageBox.Show(rows.ToString());
                        switch (rows)
                        {
                            case 4:
                                {
                                    this.PowerPrice_4.SelectedValue = dt.Rows[3].ItemArray[0];
                                    this.PowerRate_4.Text = dt.Rows[3].ItemArray[1].ToString().Trim();
                                    rows = rows - 1;
                                    break;
                                }
                            case 3:
                                {
                                    this.PowerPrice_3.SelectedValue = dt.Rows[2].ItemArray[0];
                                    this.PowerRate_3.Text = dt.Rows[2].ItemArray[1].ToString().Trim();
                                    rows = rows - 1;
                                    break;
                                }
                            case 2:
                                {
                                    this.PowerPrice_2.SelectedValue = dt.Rows[1].ItemArray[0];
                                    this.PowerRate_2.Text = dt.Rows[1].ItemArray[1].ToString().Trim();
                                    rows = rows - 1;
                                    break;
                                }
                            case 1:
                                {
                                    this.PowerPrice_1.SelectedValue = dt.Rows[0].ItemArray[0];
                                    this.PowerRate_1.Text = dt.Rows[0].ItemArray[1].ToString().Trim();
                                    rows = rows - 1;
                                    break;
                                }
                            default:
                                {
                                    //do nothing;
                                    break;
                                }
                        }


                    }
                    
                }
                catch (Exception)
                {
                    //do nothing
                }

            }
        }
        private void CloseInfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 验证电话号码
        public bool IsTelephone(string str_telephone) 
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        //验证手机号码
        public bool IsHandset(string str_handset) {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
        }
        public bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }
        public Boolean checkInfo() 
        {
            if (!IsTelephone(txtPhoneNum.Text) && !IsHandset(txtPhoneNum.Text)) {
                MessageBox.Show("请输入正确的联系方式，手机号或者电话号码！");
                return false;
            }
            if (Sign.Checked == true)
            {
                if (!IsIDcard(IdentificationCard.Text))
                {
                    MessageBox.Show("请输入正确的身份证！");
                    return false;
                }
                label28.Visible = true;
            }
            else {
                label28.Visible = false;
            }

            if(LineBox.SelectedItem==null){
                MessageBox.Show("请选择所属线路！");
                return false;
            }
            if (Transformer.SelectedItem == null)
            {
                MessageBox.Show("请选择变压器！");
                return false;
            }
            if (AreaBox.SelectedItem == null)
            {
                MessageBox.Show("请选择所属台区！");
                return false;
            }

            if (VoltageFlag.SelectedItem == null)
            {
                MessageBox.Show("请选择所属电压！");
                return false;
            }

            if (ElectriType.SelectedItem == null)
            {
                MessageBox.Show("请选择用电性质！");
                return false;
            }
          
            if (InvoiceType.SelectedItem == null)
            {
                MessageBox.Show("请选择用电性质！");
                return false;
            }

            double PowerRate1, PowerRate2, PowerRate3, PowerRate4;
            if (PowerRate_1.Text == "")
            {
                PowerRate1 = 0;
            }
            else {
                PowerRate1 = double.Parse(PowerRate_1.Text);
            }
            if (PowerRate_2.Text == "")
            {
                PowerRate2 = 0;
            }
            else {
                PowerRate2 = double.Parse(PowerRate_2.Text);
            }
            if (PowerRate_3.Text == "")
            {
                PowerRate3= 0;
            }
            else
            {
                PowerRate3 = double.Parse(PowerRate_3.Text);
            }
            if (PowerRate_4.Text == "")
            {
                PowerRate4 = 0;
            }
            else
            {
                PowerRate4 = double.Parse(PowerRate_4.Text);
            }
            if ((PowerRate1+PowerRate2+PowerRate3+PowerRate4)!=100) {
                MessageBox.Show("请正确填写电价比率信息！");
                return false;
            }
            return true;
        }
        private void UpdateInfo_Click(object sender, EventArgs e)
        {
            Boolean success = false;

            success= checkInfo();
            //如果验证不成功
            if (success == false) {

            }
            else{
            List<Object> list =  new List<object>();

            if ("" != this.CustomInfoList.SelectedItem.ToString().Split(' ')[3])
            {
                /*this.customerName.Text = customer.getCustomerName();
                //this.ammeterMutiple.Text = customer.getAmmeterType();
                this.customer.setLine(this.line.Text.ToString());
                this.customer.seArea this.area.Text = customer.getArea();
                this.invoiceNo.Text = customer.getInvoiceType();
                this.electriType.Text = customer.getElectriCharacterName();
                this.voltage.Text = customer.getVoltageFlag();
                this.ammeterMutiple.Text = countfeeinfo.getAmmeterMulti();
                this.lineChange.Text = countfeeinfo.getLineLoseRate();
                this.transformer.Text = countfeeinfo.getTransformerNo();*/
                customer.setCustomerName(customerName.Text.ToString().Trim());     
                

                this.customer.setLine(this.LineBox.SelectedValue.ToString());       //线路

                this.customer.setArea(this.AreaBox.SelectedValue.ToString());       //台区

                this.customer.setElectriCharacterName(this.ElectriType.SelectedItem.ToString());       //用电性质
           
                this.customer.setInvoiceType(this.InvoiceType.SelectedItem.ToString());     //发票类型
            
                this.customer.setVoltageFlag(this.VoltageFlag.SelectedItem.ToString());     //是否高压电

                customer.setAmmeterType(this.cbxAmmeterType1.SelectedItem.ToString());
                customer.setAmmeterNo(this.txtAmmeterNo1.Text.ToString());

                this.customer.setBankAccount(this.BankAccount.Text.ToString().Trim());      //银行账号

                this.customer.setBankCode(this.BankCode.Text.ToString().Trim());            //银行代码
                /**
                 * 另外一些数据后面再补
                 **/

                //设置身份证号
                customer.setIdentificationCard(this.IdentificationCard.Text.ToString().Trim());

                //设置签约标志信息
                if (Sign.Checked)
                {
                    customer.setSignFlag("1");
                }
                else
                {
                    customer.setSignFlag("0");
                }



                if (this.Transformer.SelectedIndex != -1 && this.Transformer.SelectedItem.ToString().Trim() != "")
                {
                    this.countfeeinfo.setTransformerNo(this.Transformer.SelectedValue.ToString());
                }
                else
                {
                    this.countfeeinfo.setTransformerNo("0");
                }

                this.customer.setCustomerName(this.customerName.Text.ToString());

                if (this.LowProtect.Checked)
                {
                    this.customer.setLowProtectFlag("1");

                }
                else
                {
                    this.customer.setLowProtectFlag("0");
                }

                if (this.BasicEFee.Checked)
                {
                    this.customer.setTranslossOrBaseprice("1");

                }
                else
                {
                    this.customer.setTranslossOrBaseprice("0");
                }
                this.countfeeinfo.setAmmeterMulti(this.ammeterMutiple.Text.ToString());
                this.countfeeinfo.setLineLoseRate(this.lineChange.Text.ToString());
                this.customer.setCustomerAddress(this.CustomerAddress.Text.ToString().Trim());
                this.customer.setPhoneNum(this.txtPhoneNum.Text.ToString().Trim());

                if ("" == this.discountRate.Text.ToString().Trim())
                {
                    countfeeinfo.setDiscountRate("0");
                }
                else
                {
                    countfeeinfo.setDiscountRate(this.discountRate.Text.ToString().Trim());
                }

                list.Add((Object)this.customer);
                list.Add((Object)this.countfeeinfo);

                if (this.PowerPrice_1.SelectedIndex != -1)
                {
                    PriceRate pr_1 = new PriceRate();
                    pr_1.PriceNo = int.Parse(this.PowerPrice_1.SelectedValue.ToString());
                    pr_1.Rate = float.Parse(this.PowerRate_1.Text.ToString().Trim());
                    list.Add((Object)pr_1);
                }
                if (this.PowerPrice_2.SelectedIndex != -1)
                {
                    PriceRate pr_2 = new PriceRate();
                    pr_2.PriceNo = int.Parse(this.PowerPrice_2.SelectedValue.ToString());
                    pr_2.Rate = float.Parse(this.PowerRate_2.Text.ToString().Trim());
                    list.Add((Object)pr_2);
                }
                if (this.PowerPrice_3.SelectedIndex != -1)
                {
                    PriceRate pr_3 = new PriceRate();
                    pr_3.PriceNo = int.Parse(this.PowerPrice_3.SelectedValue.ToString());
                    pr_3.Rate = float.Parse(this.PowerRate_3.Text.ToString().Trim());
                    list.Add((Object)pr_3);
                }
                if (this.PowerPrice_4.SelectedIndex != -1)
                {
                    PriceRate pr_4 = new PriceRate();
                    pr_4.PriceNo = int.Parse(this.PowerPrice_4.SelectedValue.ToString());
                    pr_4.Rate = float.Parse(this.PowerRate_4.Text.ToString().Trim());
                    list.Add((Object)pr_4);
                }

                //MessageBox.Show(this.CustomInfoList.SelectedItem.ToString().Split(' ')[3]); 
                if (Constant.OK == this.customerAction.updateInfoById(this.CustomInfoList.SelectedItem.ToString().Split(' ')[3], ref list))
                {
                    MessageBox.Show("更新成功！");
                }
                else
                {
                    MessageBox.Show("更新失败！");
                    
                }

          
            }
            }
        }

        private Customer customer;
        private Countfeeinfo countfeeinfo;


        /**
         * 当BookType列表框中内容改变时，发生此事件，主要功能是根据变化的值，显示相对应的客户编号
         * @param object sender
         * @param EvenArgs e
         * @return void
         * @author Rick
         **/
        private void BookType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string key = this.BookType.SelectedItem.ToString();
            

            if ("" != key.Trim())
            {
               
                this.customerinfolist_data_fill(key);
                this.CustomInfoList.Refresh();
            }
        }

        private void DeleteInfo_Click(object sender, EventArgs e)
        {
            if (this.CustomInfoList.SelectedIndex != -1)
            {
                if (MessageBox.Show("确定要删除该用户信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    if (Constant.OK == this.customerAction.deleteCustomerInfo(this.CustomInfoList.SelectedItem.ToString().Split(' ')[3]))
                    {
                        MessageBox.Show("删除成功！");
                        //this.MaintainCustom_Load();
                        this.customerinfolist_data_fill(this.BookType.SelectedItem.ToString().Trim());
                        this.data_clear_all();
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }


                }
            }
            else
            {
                MessageBox.Show("请选择要删除的客户编号！");
            }
                    
        }

        private void BookType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                customerinfolist_data_fill(this.BookType.Text.ToString());
            }
        }

        private void txtCustomerNoKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BookType.Text = txtCustomerNoKey.Text.Substring(0, 5);
                string no = txtCustomerNoKey.Text.Trim();
                string strSQL = "select Count(*) FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + BookType.Text.Trim() + "' ";
                DataSet dataSet = SQLUtl.Query(strSQL);
                int k = (int)dataSet.Tables[0].Rows[0][0];
                for (int i = 0; i < k; i++)
                {

                    CustomInfoList.SelectedIndex = i;
                    if (CustomInfoList.SelectedItem.ToString().Contains(no))
                    {
                        CustomInfoList.TopIndex = i;
                        break;
                    }
                    if (i + 1 == k)
                    {
                        CustomInfoList.SelectedIndex = 0;
                        MessageBox.Show("没有找到该编号的客户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                }
            }
        }

        private void UnSign_CheckedChanged(object sender, EventArgs e)
        {
            if (UnSign.Checked == true)
            {
                Sign.Checked = false;
                label28.Visible = false;
            }
            else
            {
                Sign.Checked = true;
                label28.Visible = true;
            }

        }

        private void Sign_CheckedChanged(object sender, EventArgs e)
        {
            if (Sign.Checked == true)
            {
                UnSign.Checked = false;
                label28.Visible = true;
            }
            else
            {
                UnSign.Checked = true;
                label28.Visible = false;
            }
        }

    }
}
