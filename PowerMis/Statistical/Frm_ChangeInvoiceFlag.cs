using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;

namespace PowerMis.Statistical
{
    public partial class Frm_ChangeInvoiceFlag : Form
    {
        public Frm_ChangeInvoiceFlag()
        {
            InitializeComponent();
        }

        private void btnUpdate1_Click(object sender, EventArgs e)
        {
            string VolumeNo = txtVolumeNo.Text.Trim();
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            DataTable dt = null;
            try
            {
                strSql = "select * from CountFee Where  Left(CustomerNo,5) ='" + VolumeNo + "' and CountFeeDate ='" + time + "' and NegativeInvoiceFlag =0";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count > 0)
                {
                    strSql = "UPDATE CountFee SET InvoiceFlag=0 WHERE Left(CustomerNo,5) ='" + VolumeNo + "' and CountFeeDate ='" + time + "' and NegativeInvoiceFlag =0";
                    int count = SQLUtl.ExecuteSql(strSql);
                    MessageBox.Show(count+"条记录被更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("此账册本月电费数据未录入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker2.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            DataTable dt = null;
            if (txtStartNo.Text == "" && txtEndNo.Text == "")
            {
                MessageBox.Show("请输入客户编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txtStartNo.Text != "" && txtEndNo.Text == "")
            {
                strSql = "select * from CountFee Where CustomerNo ='" + txtStartNo.Text.Trim() + "'  and CountFeeDate ='" + time + "'  and NegativeInvoiceFlag =0";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count > 0)
                {
                    strSql = "UPDATE CountFee SET InvoiceFlag=0  WHERE CustomerNo ='" + txtStartNo.Text + "'  and CountFeeDate ='" + time + "'  and NegativeInvoiceFlag =0";
                    int count = SQLUtl.ExecuteSql(strSql);
                    MessageBox.Show(count + "条记录被更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("本月没有数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (txtStartNo.Text != "" && txtEndNo.Text != "")
            {
                try
                {
                    strSql = "select * from CountFee Where CustomerNo >='" + txtStartNo.Text.Trim() + "' and CustomerNo<='" + txtEndNo.Text.Trim() + "' and CountFeeDate ='" + time + "'  and NegativeInvoiceFlag =0";
                    dt = SQLUtl.Query(strSql).Tables["dataSet"];
                    if (dt.Rows.Count > 0)
                    {
                        strSql = "UPDATE CountFee SET InvoiceFlag=0  WHERE CustomerNo >='" + txtStartNo.Text + "' and CustomerNo<='" + txtEndNo.Text + "' and CountFeeDate ='" + time + "' and NegativeInvoiceFlag =0";
                        int count = SQLUtl.ExecuteSql(strSql);
                        MessageBox.Show(count + "条记录被更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("本月没有数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }


        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
