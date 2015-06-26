using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using System.Data.SqlClient;

namespace PowerMis.Statistical
{
    public partial class Frm_NegativeInvoiceDelete : Form
    {
        public Frm_NegativeInvoiceDelete()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string printdate = dateTimePicker.Value.ToShortDateString();
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            DataTable dt = null;
            if (txtCustomerNo.Text == "")
            {
                MessageBox.Show("请输入客户编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                strSql = "select * from CountFee Where CustomerNo ='" + txtCustomerNo.Text.Trim() + "'  and CountFeeDate ='" + time + "'  and NegativeInvoiceFlag =1";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count > 0)
                {
                    strSql = "DELETE CountFee WHERE CustomerNo ='" + txtCustomerNo.Text + "'  and CountFeeDate ='" + time + "'  and NegativeInvoiceFlag =1";
                    string strSql1 = "Delete NegativeInvoice where CustomerNo ='" + txtCustomerNo.Text + "' and Printdate = '"+ printdate +"'";
                    List<string> sql = new List<string>();
                    sql.Add(strSql);
                    sql.Add(strSql1);
                    try
                    {
                        SQLUtl.ExecuteSqlTran(sql);
                        MessageBox.Show("负数发票删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("负数发票删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //int count = SQLUtl.ExecuteSql(strSql);
                    //MessageBox.Show(count + "条记录被删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("没有该客户的负数发票信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
