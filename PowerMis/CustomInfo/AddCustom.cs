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
using BusinessModel;
using BusinessLogic;

namespace PowerMis.CustomInfo
{
    public partial class AddCustom : Form
    {
        public AddCustom()
        {
            InitializeComponent();
            this.customerAction = new CustomerAction();
        }


        /**
         * 初始化该页面上的所有内容
         * @author      Rick
         **/ 
        public void init_content()
        {
            this.CustomerName.Clear();
            this.CustomerCode_2.Clear();
            this.CustomerAddress.Clear();
            this.txtPhoneNum.Clear();
            this.Transformer.SelectedIndex = -1;
            this.IndustryCode.Clear();
            this.Bank.Clear();
            this.BankAccount.Clear();
            this.BankCode.Clear();
            this.ElectriType.SelectedIndex = -1;
            this.Price1.Clear();
            this.Price2.Clear();
            this.Price3.Clear();
            this.Price4.Clear();
            this.PowerPrice_1.SelectedIndex = -1;
            this.PowerPrice_2.SelectedIndex = -1;
            this.PowerPrice_3.SelectedIndex = -1;
            this.PowerPrice_4.SelectedIndex = -1;
            this.Multi.Clear();
            this.Line.SelectedIndex = -1;
            this.AreaType.SelectedIndex = -1;
            this.VoltageFlag.SelectedIndex = -1;
            this.cbxAmmeterType1.SelectedIndex = -1;
            this.txtAmmeterNo1.Clear(); ;
            this.TaxNo.Clear();
            this.LineLoseRate.Clear();
            this.Discount.Clear();
            this.InvoiceType.SelectedIndex = -1;
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
                                //this.CustomInfoList.Items.Add(dr.ItemArray[0].ToString());
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
                            }
                        }
                        /*
                        int count = dataTable.Rows.Count;
                        if (count != 0)
                        {
                            for (int j = 0; j < count; j++)
                            {
                                this.CustomInfoList.Items.Add(dataTable.Rows[j].ItemArray[0].ToString());
                            }
                        }*/
                        
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
                            //this.CustomInfoList.Items.Add(dr.ItemArray[0].ToString());
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
            this.Line.DataSource = ds.Tables[0];
            this.Line.DisplayMember = "LineName";
            this.Line.ValueMember = "LineNum";
            //this.LineBox.Items.Add("");
            this.Line.SelectedIndex = -1;

            this.Line_new.DataSource = ds.Tables[0];
            this.Line_new.DisplayMember = "LineName";
            this.Line_new.ValueMember = "LineNum";
            //this.LineBox.Items.Add("");
            this.Line_new.SelectedIndex = -1;

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

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.AreaType.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.AreaType.SelectedIndex = -1;

            foreach (DataRow dr in dt.Rows)
            {
                this.Area_new.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.Area_new.SelectedIndex = -1;

            /*
            this.AreaType.DataSource = ds.Tables[0];
            this.AreaType.DisplayMember = "AreaName";
            this.AreaType.ValueMember = "AreaNo";
            this.AreaType.SelectedIndex = -1;

            this.Area_new.DataSource = ds.Tables[0];
            this.Area_new.DisplayMember = "AreaName";
            this.Area_new.ValueMember = "AreaNo";
            this.Area_new.SelectedIndex = -1;
             */
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
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.Transformer.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.Transformer.SelectedIndex = -1;

            foreach (DataRow dr in dt.Rows)
            {
                this.Transformer_new.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.Transformer_new.SelectedIndex = -1;

            /*
            this.Transformer.DataSource = ds.Tables[0];
            this.Transformer.DisplayMember = "TransformerName";
            this.Transformer.ValueMember = "TransformerNo";
            this.Transformer.SelectedIndex = -1;

            this.Transformer_new.DataSource = ds.Tables[0];
            this.Transformer_new.DisplayMember = "TransformerName";
            this.Transformer_new.ValueMember = "TransformerNo";
            this.Transformer_new.SelectedIndex = -1;
            */
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

            this.VoltageFlag_new.Items.Add("高");
            this.VoltageFlag_new.Items.Add("低");
            this.VoltageFlag_new.SelectedIndex = -1;
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

            this.InvoiceType_new.Items.Add("普通发票");
            this.InvoiceType_new.Items.Add("增值税发票");
            this.InvoiceType_new.Items.Add("农网发票");
            this.InvoiceType_new.SelectedIndex = -1;
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

            this.Electri_new.Items.Add("农网");
            this.Electri_new.Items.Add("城网");
            this.Electri_new.Items.Add("局内");
            this.Electri_new.SelectedIndex = -1;
            return Constant.OK;
        }

        private int ammeterType_data_fill()
        {
            this.cbxAmmeterType1.Items.Add("单相");
            this.cbxAmmeterType1.Items.Add("三相");
            this.ElectriType.SelectedIndex = -1;

            this.cbxAmmeterType2.Items.Add("单相");
            this.cbxAmmeterType2.Items.Add("三相");
            this.Electri_new.SelectedIndex = -1;
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

            DataTable dt_1_new = new DataTable();
            dt_1_new = ds.Tables[0].Copy();
            this.PowerPrice1_new.DataSource = dt_1_new;
            this.PowerPrice1_new.DisplayMember = "PowerPriceName";
            this.PowerPrice1_new.ValueMember = "PowerPriceNo";
            this.PowerPrice1_new.SelectedIndex = -1;

            DataTable dt_2_new = new DataTable();
            dt_2_new = ds.Tables[0].Copy();
            this.PowerPrice2_new.DataSource = dt_2_new;
            this.PowerPrice2_new.DisplayMember = "PowerPriceName";
            this.PowerPrice2_new.ValueMember = "PowerPriceNo";
            this.PowerPrice2_new.SelectedIndex = -1;

            DataTable dt_3_new = new DataTable();
            dt_3_new = ds.Tables[0].Copy();
            this.PowerPrice3_new.DataSource = dt_3_new;
            this.PowerPrice3_new.DisplayMember = "PowerPriceName";
            this.PowerPrice3_new.ValueMember = "PowerPriceNo";
            this.PowerPrice3_new.SelectedIndex = -1;

            DataTable dt_4_new = new DataTable();
            dt_4_new = ds.Tables[0].Copy();
            this.PowerPrice4_new.DataSource = dt_4_new;
            this.PowerPrice4_new.DisplayMember = "PowerPriceName";
            this.PowerPrice4_new.ValueMember = "PowerPriceNo";
            this.PowerPrice4_new.SelectedIndex = -1;

            return Constant.OK;
        }


        private void AddCustom_Load(object sender, EventArgs e)
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
            this.CustomerCode_1.Enabled = false;
            //MessageBox.Show("asd faf");
           
        }

        private void addInNew_Click(object sender, EventArgs e)
        {

        }

        private void BookType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = this.BookType.SelectedItem.ToString();


            if ("" != key.Trim())
            {

                this.customerinfolist_data_fill(key);
                this.CustomInfoList.Refresh();
            }
        }
        // 验证电话号码
        public bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        //验证手机号码
        public bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
        }
        public bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }
        public Boolean checkInfo()
        {
            if (!IsTelephone(txtPhoneNum.Text) && !IsHandset(txtPhoneNum.Text))
            {
                MessageBox.Show("请输入正确的联系方式，手机号或者电话号码！");
                return false;
            }
            if (sign.Checked ==true)
            {
                if (!IsIDcard(identificationCard.Text))
                {
                    MessageBox.Show("请输入正确的身份证！");
                    return false;
                }
                label62.Visible = true;
            }
            else
            {
                label62.Visible = false;
            }

            if (Line.SelectedItem == null)
            {
                MessageBox.Show("请选择所属线路！");
                return false;
            }
            if (Transformer.SelectedItem == null)
            {
                MessageBox.Show("请选择变压器！");
                return false;
            }
            if (AreaType.SelectedItem == null)
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
            if (Price1.Text == "")
            {
                PowerRate1 = 0;
            }
            else
            {
                PowerRate1 = double.Parse(Price1.Text);
            }
            if (Price2.Text == "")
            {
                PowerRate2 = 0;
            }
            else
            {
                PowerRate2 = double.Parse(Price2.Text);
            }
            if (Price3.Text == "")
            {
                PowerRate3 = 0;
            }
            else
            {
                PowerRate3 = double.Parse(Price3.Text);
            }
            if (Price4.Text == "")
            {
                PowerRate4 = 0;
            }
            else
            {
                PowerRate4 = double.Parse(Price4.Text);
            }
            if ((PowerRate1 + PowerRate2 + PowerRate3 + PowerRate4) != 100)
            {
                MessageBox.Show("请正确填写电价比率信息！");
                return false;
            }
            return true;

        }
        //2015.6增加银行代收功能后，完善用户信息
        private void add_Click(object sender, EventArgs e)
        {
            Boolean success = false;

            success = checkInfo();
            //如果验证不成功
            if (success == false)
            {

            }
            else
            {
                Customer customer = new Customer();
                Countfeeinfo countfeeinfo = new Countfeeinfo();
                List<Object> list = new List<object>();
                //string customerPosition1 = "";
                //string customerPosition2 = "";
                decimal customerPosition1 = 0;
                decimal customerPosition2 = 0;
                if (this.CustomInfoList.SelectedIndex == -1)
                {
                    MessageBox.Show("选择要添加客户成员的位置");
                    return;
                }

                /* 计算新添加客户的位置 */
                if (this.CustomInfoList.Items.Count == (this.CustomInfoList.SelectedIndex + 1))
                {
                    if (this.customerAction.getCustomerPositionById(this.CustomInfoList.SelectedItem.ToString().Split(' ')[3], ref customerPosition1) == Constant.OK)
                    {
                        customer.setCustomerPosition(customerPosition1 + 1);
                    }
                }

                else
                {
                    if (this.customerAction.getCustomerPositionById(this.CustomInfoList.SelectedItem.ToString().Split(' ')[3], ref customerPosition1) == Constant.OK && this.customerAction.getCustomerPositionById(this.CustomInfoList.Items[this.CustomInfoList.SelectedIndex + 1].ToString().Split(' ')[3], ref customerPosition2) == Constant.OK)
                    {
                        customer.setCustomerPosition((customerPosition1 + customerPosition2) / 2);
                    }
                }


                if ("" == this.CustomerCode_1.Text.ToString().Trim())
                {
                    MessageBox.Show("请选择账本信息和所要插入的位置！");
                    return;
                }

                if ("" == this.CustomerCode_2.Text.ToString().Trim())
                {
                    MessageBox.Show("填写客户编码！");
                    return;
                }
                customer.setCustomerNo(this.CustomerCode_1.Text.ToString().Trim() + this.CustomerCode_2.Text.ToString().Trim());

                if ("" == this.CustomerName.Text.ToString().Trim())
                {
                    MessageBox.Show("请输入客户名称");
                    return;
                }
                customer.setCustomerName(this.CustomerName.Text.ToString().Trim());

                customer.setCustomerAddress(this.CustomerAddress.Text.ToString());

                customer.setPhoneNum(this.txtPhoneNum.Text.ToString());

                customer.setLine(this.Line.SelectedValue.ToString().Trim());

                customer.setAmmeterType(this.cbxAmmeterType1.SelectedItem.ToString());
                customer.setAmmeterNo(this.txtAmmeterNo1.Text.ToString());

                customer.setArea(this.AreaType.SelectedItem.ToString().Trim().Split(' ')[0]);


                customer.setElectriCharacterName(this.ElectriType.SelectedItem.ToString().Trim());


                customer.setVoltageFlag(this.VoltageFlag.SelectedItem.ToString().Trim());


                customer.setInvoiceType(this.InvoiceType.SelectedItem.ToString().Trim());

                //设置身份证号
                customer.setIdentificationCard(this.identificationCard.Text.ToString().Trim());

                //设置签约标志信息
                if (this.sign.Checked)
                {
                    customer.setSignFlag("1");
                }
                else
                {
                    customer.setSignFlag("0");
                }

                customer.setOrganFlag("0");

                customer.setEspecialFlag("0");
                if (this.cbxSpecialFlag.Checked)
                {
                    customer.setEspecialFlag("1");
                }


                customer.setBankAccount(this.BankAccount.Text.ToString().Trim());

                customer.setBankCode(this.BankCode.Text.ToString().Trim());

                customer.setBankName(this.Bank.Text.ToString().Trim());

                customer.setTradeCode("");

                customer.setValueAddTaxNo("");

                if (this.LowProtect.Checked)
                {
                    customer.setLowProtectFlag("1");
                }
                else
                {
                    customer.setLowProtectFlag("0");
                }

                if (this.BasicEFee.Checked)
                {
                    customer.setTranslossOrBaseprice("1");
                }
                else
                {
                    customer.setTranslossOrBaseprice("0");
                }

                customer.setPassword(this.CustomerCode_1.Text.ToString().Trim() + this.CustomerCode_2.Text.ToString().Trim());


                countfeeinfo.setCustomerNo(customer.getCustomerNo());
                if (null == this.Transformer.SelectedItem || -1 == int.Parse(this.Transformer.SelectedItem.ToString().Trim().Split(' ')[0]))
                {
                    countfeeinfo.setTransformerNo("0");
                }
                else
                {
                    countfeeinfo.setTransformerNo(this.Transformer.SelectedItem.ToString().Trim().Split(' ')[0]);
                }

                countfeeinfo.setAmmeterMulti(this.Multi.Text.ToString().Trim());

                countfeeinfo.setLineLoseRate(this.LineLoseRate.Text.ToString().Trim());

                if ("" == this.Discount.Text.ToString().Trim())
                {
                    countfeeinfo.setDiscountRate("0");
                }
                else
                {
                    countfeeinfo.setDiscountRate(this.Discount.Text.ToString().Trim());
                }

                list.Add((Object)customer);
                list.Add((Object)countfeeinfo);

                /* 检验该客户编号是否存在 */

                bool state = false;
                if (Constant.OK == this.customerAction.customerNoChecking(customer.getCustomerNo(), ref state))
                {
                    if (!state)
                    {
                        MessageBox.Show("该编号已经存在");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("数据库执行出错");
                    return;
                }


                /* 添加电价比率信息 */
                if (this.PowerPrice_1.SelectedIndex != -1)
                {
                    PriceRate pr_1 = new PriceRate();
                    pr_1.PriceNo = int.Parse(this.PowerPrice_1.SelectedValue.ToString());
                    pr_1.Rate = float.Parse(this.Price1.Text.ToString().Trim());
                    pr_1.PriceName = customer.getCustomerNo();
                    list.Add((Object)pr_1);
                }
                if (this.PowerPrice_2.SelectedIndex != -1)
                {
                    PriceRate pr_2 = new PriceRate();
                    pr_2.PriceNo = int.Parse(this.PowerPrice_2.SelectedValue.ToString());
                    pr_2.Rate = float.Parse(this.Price2.Text.ToString().Trim());
                    pr_2.PriceName = customer.getCustomerNo();
                    list.Add((Object)pr_2);
                }
                if (this.PowerPrice_3.SelectedIndex != -1)
                {
                    PriceRate pr_3 = new PriceRate();
                    pr_3.PriceNo = int.Parse(this.PowerPrice_3.SelectedValue.ToString());
                    pr_3.Rate = float.Parse(this.Price3.Text.ToString().Trim());
                    pr_3.PriceName = customer.getCustomerNo();
                    list.Add((Object)pr_3);
                }
                if (this.PowerPrice_4.SelectedIndex != -1)
                {
                    PriceRate pr_4 = new PriceRate();
                    pr_4.PriceNo = int.Parse(this.PowerPrice_4.SelectedValue.ToString());
                    pr_4.Rate = float.Parse(this.Price4.Text.ToString().Trim());
                    pr_4.PriceName = customer.getCustomerNo();
                    list.Add((Object)pr_4);

                }

                /*添加客户信息*/
                if (Constant.OK == this.customerAction.addCustomerInfo(ref list))
                {
                    MessageBox.Show("客户信息添加成功");
                    this.init_content();
                    return;
                }
                else
                {
                    MessageBox.Show("客户信息添加失败");
                    return;
                }

                /*添加客户计费信息
                if (Constant.OK == this.customerAction.addCountfeeinfo(ref countfeeinfo))
                {
                }
                else
                {
                }
                */
            }
        }

        private CustomerAction customerAction;

        private void CustomInfoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 != this.CustomInfoList.SelectedIndex)
            {
                if (this.BookType.SelectedItem != null)
                {
                    this.CustomerCode_1.Text = this.BookType.SelectedItem.ToString().Trim();
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            Countfeeinfo countfeeinfo = new Countfeeinfo();
            List<Object> list = new List<object>();

            /* 设置客户的位置信息 */
            customer.setCustomerPosition(1);


            
            customer.setCustomerNo(this.CustomerCode_new.Text.ToString().Trim());

            if ("" == this.CustomerName_new.Text.ToString().Trim())
            {
                MessageBox.Show("请输入客户名称！");
                return;
            }
            customer.setCustomerName(this.CustomerName_new.Text.ToString().Trim());


            customer.setCustomerAddress(this.CustomerAddress_new.Text.ToString());

            customer.setPhoneNum(this.txtPhoneNum2.Text.ToString()); 

            customer.setLine(this.Line_new.SelectedValue.ToString().Trim());


            customer.setArea(this.Area_new.SelectedItem.ToString().Trim().Split(' ')[0]);

            customer.setAmmeterType(this.cbxAmmeterType2.SelectedItem.ToString());
            customer.setAmmeterNo(this.txtAmmeterNo2.Text.ToString());
            customer.setElectriCharacterName(this.Electri_new.SelectedItem.ToString().Trim());


            customer.setVoltageFlag(this.VoltageFlag_new.SelectedItem.ToString().Trim());


            customer.setInvoiceType(this.InvoiceType_new.SelectedItem.ToString().Trim());

            //设置身份证号
            customer.setIdentificationCard(this.identificationCard.Text.ToString().Trim());

            //设置签约标志信息
            if (this.sign.Checked)
            {
                customer.setSignFlag("1");
            }
            else
            {
                customer.setSignFlag("0");
            }

            customer.setOrganFlag("0");

            customer.setEspecialFlag("0");
            if (this.cbxSpecialFlag.Checked)
            {
                customer.setEspecialFlag("1");
            }

            
            customer.setBankAccount(this.BankAccount_new.Text.ToString().Trim());

            customer.setBankCode(this.BankCode_new.Text.ToString().Trim());

            customer.setBankName(this.BankName_new.Text.ToString().Trim());

            customer.setTradeCode("");

            customer.setValueAddTaxNo("");

            customer.setPassword(this.CustomerCode_new.Text.ToString().Trim());

            if (this.LowProtect.Checked)
            {
                customer.setLowProtectFlag("1");
            }
            else
            {
                customer.setLowProtectFlag("0");
            }

            if (this.BasicEFee.Checked)
            {
                customer.setTranslossOrBaseprice("1");
            }
            else
            {
                customer.setTranslossOrBaseprice("0");
            }


            countfeeinfo.setCustomerNo(customer.getCustomerNo());
            if (null == this.Transformer_new.SelectedItem || -1 == int.Parse(this.Transformer_new.SelectedItem.ToString().Trim().Split(' ')[0]))
            {
                countfeeinfo.setTransformerNo("0");
            }
            else
            {
                countfeeinfo.setTransformerNo(this.Transformer_new.SelectedItem.ToString().Trim().Split(' ')[0]);
            }


            countfeeinfo.setAmmeterMulti(this.Multip_new.Text.ToString().Trim());


            countfeeinfo.setLineLoseRate(this.LineLoseRate_new.Text.ToString().Trim());

            if ("" == this.Discount_new.Text.ToString().Trim())
            {
                countfeeinfo.setDiscountRate("0");
            }
            else
            {
                countfeeinfo.setDiscountRate(this.Discount_new.Text.ToString().Trim());
            }

            list.Add((Object)customer);
            list.Add((Object)countfeeinfo);

            /* 检验该该账本编号是否存在 */

            bool state = false;
            if (Constant.OK == this.customerAction.bookTypeChecking(customer.getCustomerNo(), ref state))
            {
                if (!state)
                {
                    MessageBox.Show("该账簿号已经存在");
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("数据库执行出错");
                return;
            }


            /* 添加电价比率信息 */
            if (this.PowerPrice1_new.SelectedIndex != -1)
            {
                PriceRate pr_1 = new PriceRate();
                pr_1.PriceNo = int.Parse(this.PowerPrice1_new.SelectedValue.ToString());
                pr_1.Rate = float.Parse(this.Price1_new.Text.ToString().Trim());
                pr_1.PriceName = customer.getCustomerNo();
                list.Add((Object)pr_1);
            }
            if (this.PowerPrice2_new.SelectedIndex != -1)
            {
                PriceRate pr_2 = new PriceRate();
                pr_2.PriceNo = int.Parse(this.PowerPrice2_new.SelectedValue.ToString());
                pr_2.Rate = float.Parse(this.Price2_new.Text.ToString().Trim());
                pr_2.PriceName = customer.getCustomerNo();
                list.Add((Object)pr_2);
            }
            if (this.PowerPrice3_new.SelectedIndex != -1)
            {
                PriceRate pr_3 = new PriceRate();
                pr_3.PriceNo = int.Parse(this.PowerPrice3_new.SelectedValue.ToString());
                pr_3.Rate = float.Parse(this.Price3_new.Text.ToString().Trim());
                pr_3.PriceName = customer.getCustomerNo();
                list.Add((Object)pr_3);
            }
            if (this.PowerPrice4_new.SelectedIndex != -1)
            {
                PriceRate pr_4 = new PriceRate();
                pr_4.PriceNo = int.Parse(this.PowerPrice4_new.SelectedValue.ToString());
                pr_4.Rate = float.Parse(this.Price4_new.Text.ToString().Trim());
                pr_4.PriceName = customer.getCustomerNo();
                list.Add((Object)pr_4);

            }

            /*添加客户信息*/
            if (Constant.OK == this.customerAction.addCustomerInfo(ref list))
            {
                MessageBox.Show("客户信息添加成功");
                this.init_content();
                return;
            }
            else
            {
                MessageBox.Show("客户信息添加失败");
                return;
            }

        }

        private void addCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.booktype_data_fill();
            //MessageBox.Show("changing");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close(); ;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BookType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //customerinfolist_data_fill(this.BookType.Text.ToString());
                string bookNo = this.BookType.Text;
                int i = this.BookType.FindString(bookNo);
                this.BookType.SelectedIndex = i; 
            }
        }

   

        private void sign_CheckedChanged(object sender, EventArgs e)
        {
            if (sign.Checked == true)
            {
                unSign.Checked = false;
                label62.Visible = true;
            }
            else {
                unSign.Checked = true;
                label62.Visible = false;
            }
          
        }

        private void unSign_CheckedChanged(object sender, EventArgs e)
        {
            if (unSign.Checked == true)
            {
                sign.Checked = false;
                label62.Visible = false;
            }
            else
            {
                sign.Checked = true;
                label62.Visible = true;
            }
        }

        private void newVolumeSign_CheckedChanged(object sender, EventArgs e)
        {
            if (newVolumeSign.Checked == true)
            {
                newVolumeUnSign.Checked = false;
            }
            else
            {
                newVolumeUnSign.Checked = true;
            }
        }
       
        private void newVolumeUnSign_CheckedChanged(object sender, EventArgs e)
        {
            if (newVolumeUnSign.Checked == true)
            {
               newVolumeSign.Checked = false;
            }
            else
            {
                newVolumeSign.Checked = true;
            }
        }



  






       /* private void button1_Click(object sender, EventArgs e)
        {
                
      
            string sql = "select CustomerNo from CustomerInfo";
            DataSet ds = null;
            try{
                ds = SQLUtl.Query(sql);
                MessageBox.Show("chenggong");
            }
            catch(Exception)
            {
                
                MessageBox.Show("cuowu");

            }

            DataTable dt = ds.Tables[0];

            int rows = dt.Rows.Count;
            int i = 0;
            List<string> list = new List<string>();
            while (rows > 0)
            {
                string key = dt.Rows[i].ItemArray[0].ToString().Trim();

                string sqlstr = "update CustomerInfo set CustomerPosition = " + i.ToString() + " where CustomerNo = '" + key + "'";
                list.Add(sqlstr);
                rows = rows - 1;
                i = i + 1;
               
            }
            try
            {
                SQLUtl.ExecuteSqlTran(list);
               
            }
            catch (Exception)
            {
                MessageBox.Show("error");

            }
                MessageBox.Show("cehnggong");

       
        }*/


    }
}
