using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using BusinessLogic;
using BusinessModel;
using Common;
namespace PowerMis.CustomInfo
{
    public partial class ChangeSection : Form
    {
        public ChangeSection()
        {
            InitializeComponent();
            this.customerAction = new CustomerAction();
        }

        /**
         * 填充账本信息
         * @return      int 返回值为0，则表示执行成功；返回值为负数，则表示执行失败。 
         * @author      Rick
         */
        private int booktype_old_data_fill()
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
                        this.BookType_old.Items.Add(dr.ItemArray[0].ToString());
                    }
                    this.BookType_old.SelectedIndex = 0;
                    this.BookType_old.Refresh();
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
         * 填充账本信息
         * @return      int 返回值为0，则表示执行成功；返回值为负数，则表示执行失败。 
         * @author      Rick
         */
        private int booktype_new_data_fill()
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
                        this.BookType_new.Items.Add(dr.ItemArray[0].ToString());
                    }
                    this.BookType_new.SelectedIndex = 0;
                    this.BookType_new.Refresh();
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
        private int customerinfolist_old_data_fill(string booktype)
        {
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
                        string strSql = "select CustomerNo from CustomerInfo  where substring(CustomerNo, 1, 5) = '" + defaultKey + "' order by CustomerNo asc";
                        DataSet dataSet = SQLUtl.Query(strSql);
                        DataTable dataTable = dataSet.Tables["dataSet"];

                        if (dataTable.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                this.CustomerList_old.Items.Add(dr.ItemArray[0].ToString());
                            }
                        }
                        this.CustomerList_old.Refresh();
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
                    string strSql = "select CustomerNo from CustomerInfo  where substring(CustomerNo, 1, 5) = '" + booktype + "' order by CustomerNo asc";
                    DataSet dataSet = SQLUtl.Query(strSql);
                    DataTable dataTable = dataSet.Tables["dataSet"];
                    this.CustomerList_old.Items.Clear();
                    if (dataTable.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            this.CustomerList_old.Items.Add(dr.ItemArray[0].ToString());
                        }
                    }
                    this.CustomerList_old.Refresh();
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
        * 根据账本号显示相应的客户信息
        * @param       string 账本号编码
        * @return      int 返回值为0，则表示执行成功；返回值为负数，则表示执行失败。
        * @authoer     Rick
        **/
        private int customerinfolist_new_data_fill(string booktype)
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
                                this.CustomerList_new.Items.Add(No + "   " + dr.ItemArray[0].ToString());
                                i++;
                                //this.CustomerList_new.Items.Add(dr.ItemArray[0].ToString());
                            }
                        }
                        this.CustomerList_new.Refresh();
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
                    this.CustomerList_new.Items.Clear();
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
                            this.CustomerList_new.Items.Add(No + "   " + dr.ItemArray[0].ToString());
                            i++;
                            //this.CustomerList_new.Items.Add(dr.ItemArray[0].ToString());
                        }
                    }
                    this.CustomerList_new.Refresh();
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
         * 初始化窗口中的数据
         * @author      Rick
         **/ 
        private void ChangeSection_Load(object sender, EventArgs e)
        {
            this.customerinfolist_new_data_fill("");
            this.customerinfolist_old_data_fill("");
            this.booktype_new_data_fill();
            this.booktype_old_data_fill();
            //this.BookCode.Enabled = false;

        }


        private void BookType_old_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = this.BookType_old.SelectedItem.ToString();


            if ("" != key.Trim())
            {

                this.customerinfolist_old_data_fill(key);
                this.CustomerList_old.Refresh();
            }
        }

        private void BookType_new_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = this.BookType_new.SelectedItem.ToString();


            if ("" != key.Trim())
            {
                this.txtNewCuatomerNo.Text = key;
                this.customerinfolist_new_data_fill(key);
                this.CustomerList_new.Refresh();
            }
        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewBook_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NewBook.Checked)
            {
                this.BookType_new.Enabled = false;
                this.CustomerList_new.Enabled = false;
                this.txtNewCuatomerNo.Text = "";
                //this.BookCode.Enabled = true;
            }
            else
            {
                this.BookType_new.Enabled = true;
                this.CustomerList_new.Enabled = true;
                //this.BookCode.Enabled = false;
                this.txtNewCuatomerNo.Text = this.BookType_new.SelectedItem.ToString().Trim();
            }
        }

        private void UpdateSection_Click(object sender, EventArgs e)
        {
            string customerNo_new = "";
            string customerNo_old = "";
            //string position = "";
            //string customerPosition1 = "";
            //string customerPosition2 = "";
            decimal position = 0;
            decimal customerPosition1 = 0;
            decimal customerPosition2 = 0;
            if (this.CustomerList_old.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要转账簿的客户编号！");
                return;
            }

            /* 转到原有的账簿中 */
            if (!this.NewBook.Checked)
            {
                if (this.CustomerList_new.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择要转去的位置！");
                    return;
                }
                /* 计算新添加客户的位置 */
                if (this.CustomerList_new.Items.Count == (this.CustomerList_new.SelectedIndex + 1))
                {
                    if (this.customerAction.getCustomerPositionById(this.CustomerList_new.SelectedItem.ToString().Split(' ')[3], ref customerPosition1) == Constant.OK)
                    {
                        //position = (double.Parse(customerPosition1) + 1).ToString();
                        position = customerPosition1 + 1;
                    }
                }

                else
                {
                    if (this.customerAction.getCustomerPositionById(this.CustomerList_new.SelectedItem.ToString().Split(' ')[3], ref customerPosition1) == Constant.OK && this.customerAction.getCustomerPositionById(this.CustomerList_new.Items[this.CustomerList_new.SelectedIndex + 1].ToString().Split(' ')[3], ref customerPosition2) == Constant.OK)
                    {
                        //position = ((double.Parse(customerPosition1) + double.Parse(customerPosition2)) / 2).ToString();
                        position = (customerPosition1 + customerPosition2) / 2;
                    }
                }

                if (this.txtNewCuatomerNo.Text.Length < 6)
                {
                    MessageBox.Show("客户编码错误,客户编码长度必须大于5！");
                    this.txtNewCuatomerNo.Text = this.BookType_new.SelectedItem.ToString().Trim();
                    return;
                }
                bool state = false;
                if (!this.BookType_new.SelectedItem.ToString().Equals(this.BookType_old.SelectedItem.ToString()))
                {
                    this.customerAction.checkCustomerNo(this.txtNewCuatomerNo.Text.Trim(), ref state);

                    if (!state)
                    {
                        MessageBox.Show("该客户编码已经存在！");
                        this.txtNewCuatomerNo.Text = this.BookType_new.SelectedItem.ToString().Trim();
                        return;
                    }
                }
                customerNo_new = this.txtNewCuatomerNo.Text.Trim();

                //string temp = this.CustomerList_old.SelectedItem.ToString().Trim();
                //temp = temp.Substring(5, temp.Length - 5);

                //customerNo_new = this.BookType_new.SelectedItem.ToString().Trim() + temp + "*";

                

            }
            /* 转到新账簿中 */
            else
            {
                /* 判断是否输入了新账簿编号 */
                if (this.txtNewCuatomerNo.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("请输入新的账簿编号！");
                    return;
                }

                if (this.txtNewCuatomerNo.Text.Length != 10)
                {
                     MessageBox.Show("客户编码错误,客户编码长度为10字节！");
                     //this.txtNewCuatomerNo.Text = this.BookType_new.SelectedItem.ToString().Trim();
                     return;
                }
                
                /* 判断该账簿号是否已经存在 */
                bool state = false;
                string newbookcode = "";
                newbookcode = this.txtNewCuatomerNo.Text.Trim().Substring(0, 5);
                if (Constant.OK == this.customerAction.bookTypeChecking(newbookcode,ref state))
                {
                    if (!state)
                    {
                        MessageBox.Show("该账簿号已经存在！");
                        return;
                    }
                    else
                    {
                        //string temp = this.CustomerList_old.SelectedItem.ToString().Trim();
                        //temp = temp.Substring(5, temp.Length - 5);

                       // customerNo_new = this.BookCode.Text.ToString().Trim() + temp + "*";
                        customerNo_new = this.txtNewCuatomerNo.Text.ToString().Trim();
                        position = (decimal)1.0;
                    }
                }
                else
                {
                    MessageBox.Show("账簿号校验出错！");
                    return;
                }

               

            }

            customerNo_old = this.CustomerList_old.SelectedItem.ToString().Trim();
            if (Constant.OK == this.customerAction.changeCustomerSection(customerNo_old, customerNo_new, position))
            {
                MessageBox.Show("账簿号更改成功！");
                //this.booktype_old_data_fill();
                //this.booktype_new_data_fill();
                txtNewCuatomerNo.Text = this.BookType_new.SelectedItem.ToString();
                this.customerinfolist_new_data_fill(BookType_new.SelectedItem.ToString());
                this.customerinfolist_old_data_fill(BookType_old.SelectedItem.ToString());
                this.Refresh();
            }
            else
            {
                MessageBox.Show("账簿号更改出错！");
            }

        }


        private CustomerAction customerAction;
    }
}
