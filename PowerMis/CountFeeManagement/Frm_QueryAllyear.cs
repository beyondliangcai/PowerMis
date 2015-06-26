using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;

namespace PowerMis.CountFeeManagement
{
    public partial class Frm_QueryAllyear : Form
    {
        public Frm_QueryAllyear()
        {
            InitializeComponent();
        }

        private void Frm_QueryAllyear_Load(object sender, EventArgs e)
        {
            fillBookNo();
        }

        private void clearForm()
        {
            txtMultiple.Text = "";
            txtLastYearValue.Text = "";
            txtAmmeterValue1.Text = "";
            txtAmmeterValue2.Text = "";
            txtAmmeterValue3.Text = "";
            txtAmmeterValue4.Text = "";
            txtAmmeterValue5.Text = "";
            txtAmmeterValue6.Text = "";
            txtAmmeterValue7.Text = "";
            txtAmmeterValue8.Text = "";
            txtAmmeterValue9.Text = "";
            txtAmmeterValue10.Text = "";
            txtAmmeterValue11.Text = "";
            txtAmmeterValue12.Text = "";
            txtAmmeterAmount1.Text = "";
            txtAmmeterAmount2.Text = "";
            txtAmmeterAmount3.Text = "";
            txtAmmeterAmount4.Text = "";
            txtAmmeterAmount5.Text = "";
            txtAmmeterAmount6.Text = "";
            txtAmmeterAmount7.Text = "";
            txtAmmeterAmount8.Text = "";
            txtAmmeterAmount9.Text = "";
            txtAmmeterAmount10.Text = "";
            txtAmmeterAmount11.Text = "";
            txtAmmeterAmount12.Text = "";
        }

        private void fillBookNo()
        {          
            try
            {
                string strSQL = "SELECT DISTINCT Left(CustomerNo, 5) AS BookNo FROM CustomerInfo ORDER BY BookNo";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    cbxBookNo.Items.Add(dataTable.Rows[i][0].ToString());               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbxBookNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string No = "";
            lbxCustomerList.Items.Clear();
            try
            {
                string strSQL = "SELECT CustomerNo,CustomerName FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' order by CustomerPosition asc";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                int k = 1;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //lbxCustomerList.Items.Add(dataTable.Rows[i][0] + " " + dataTable.Rows[i][1]);
                    if (k < 10)
                    {
                        No = "00" + k;
                    }
                    else if (k >= 10 && k < 100)
                    {
                        No = "0" + k;
                    }
                    else if (k >= 100 & k < 1000)
                    {
                        No = "" + k;
                    }
                    lbxCustomerList.Items.Add(No + "   " + dataTable.Rows[i][0] + " " + dataTable.Rows[i][1]);
                    k++;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void init_SelectedCustomerInfo(string customerNo)
        {
            clearForm();
            int year = dtpBirthday.Value.Year;
            string startTime = year + "-01-01";
            string endTime = year + "-12-31";
            string strSql = "";
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                strSql = "select * from AmmeterValue Where CustomerNo = '" + customerNo + "'"
                       + " and CopyValueDate  >= '" + startTime + "' and CopyValueDate<='" + endTime + "'";
                ds = SQLUtl.Query(strSql);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++ )
                    {
                        int month = Convert.ToDateTime(dt.Rows[i]["CopyValueDate"].ToString()).Month;
                        string AmmeterValue = dt.Rows[i]["AmmeterValue"].ToString();
                        switch(month)
                        {
                            case 1:
                                txtAmmeterValue1.Text = AmmeterValue;
                                break;
                            case 2:
                                txtAmmeterValue2.Text = AmmeterValue;
                                 break;
                            case 3:
                                txtAmmeterValue3.Text = AmmeterValue;
                                break;
                            case 4:
                                txtAmmeterValue4.Text = AmmeterValue;
                                break;
                            case 5:
                                txtAmmeterValue5.Text = AmmeterValue;
                                break;
                            case 6:
                                txtAmmeterValue6.Text = AmmeterValue;
                                break;
                            case 7:
                                txtAmmeterValue7.Text = AmmeterValue;
                                break;
                            case 8:
                                txtAmmeterValue8.Text = AmmeterValue;
                                break;
                            case 9:
                                txtAmmeterValue9.Text = AmmeterValue;
                                break;
                            case 10:
                                txtAmmeterValue10.Text = AmmeterValue;
                                break;
                            case 11:
                                txtAmmeterValue11.Text = AmmeterValue;
                                break;
                            case 12:
                                txtAmmeterValue12.Text = AmmeterValue;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                int lastyear = year - 1;
                string lastStartTime = lastyear + "-12-01";
                string lastEndTime = lastyear + "-12-31";
                strSql = "select * from AmmeterValue Where CustomerNo = '" + customerNo + "'"
                       + " and CopyValueDate  >= '" + lastStartTime + "' and CopyValueDate<='" + lastEndTime + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtLastYearValue.Text = ds.Tables[0].Rows[0]["AmmeterValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());            
            }

            try
            {
                strSql = "select CustomerName,CountfeeAmount,AmmeterMulti,CountFeeDate from CountFee Where CustomerNo = '" + customerNo + "'"
                       + " and CountFeeDate  >= '" + startTime + "' and CountFeeDate<='" + endTime + "' and NegativeInvoiceFlag=0";
                ds = SQLUtl.Query(strSql);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
                    txtMultiple.Text = dt.Rows[0]["AmmeterMulti"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int month = Convert.ToDateTime(dt.Rows[i]["CountFeeDate"].ToString()).Month;
                        string CountfeeAmount = dt.Rows[i]["CountfeeAmount"].ToString();
                        switch (month)
                        {
                            case 1:
                                txtAmmeterAmount1.Text = CountfeeAmount;
                                break;
                            case 2:
                                txtAmmeterAmount2.Text = CountfeeAmount;
                                break;
                            case 3:
                                txtAmmeterAmount3.Text = CountfeeAmount;
                                break;
                            case 4:
                                txtAmmeterAmount4.Text = CountfeeAmount;
                                break;
                            case 5:
                                txtAmmeterAmount5.Text = CountfeeAmount;
                                break;
                            case 6:
                                txtAmmeterAmount6.Text = CountfeeAmount;
                                break;
                            case 7:
                                txtAmmeterAmount7.Text = CountfeeAmount;
                                break;
                            case 8:
                                txtAmmeterAmount8.Text = CountfeeAmount;
                                break;
                            case 9:
                                txtAmmeterAmount9.Text = CountfeeAmount;
                                break;
                            case 10:
                                txtAmmeterAmount10.Text = CountfeeAmount;
                                break;
                            case 11:
                                txtAmmeterAmount11.Text = CountfeeAmount;
                                break;
                            case 12:
                                txtAmmeterAmount12.Text = CountfeeAmount;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void cbxBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string bookNo = cbxBookNo.Text;
                int i = cbxBookNo.FindString(bookNo);
                cbxBookNo.SelectedIndex = i;
            }
        }

        private void lbxCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerNo = "";
            if (lbxCustomerList.Items.Count != 0)
            {
                customerNo = lbxCustomerList.Text.Split(' ').GetValue(3).ToString();
            }
            txtCustomerNo.Text = customerNo;
            init_SelectedCustomerInfo(customerNo);
        }

        private void txtCustomerNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxBookNo.Text = txtCustomerNo.Text.Substring(0, 5);
                string no = txtCustomerNo.Text.Trim();
                string strSQL = "select Count(*) FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' ";
                DataSet dataSet = SQLUtl.Query(strSQL);
                int k = (int)dataSet.Tables[0].Rows[0][0];
                for (int i = 0; i < k; i++)
                {

                    lbxCustomerList.SelectedIndex = i;
                    if (lbxCustomerList.SelectedItem.ToString().Contains(no))
                    {
                        lbxCustomerList.TopIndex = i;
                        break;
                    }
                    if (i + 1 == k)
                    {
                        lbxCustomerList.SelectedIndex = 0;
                        MessageBox.Show("没有找到该编号的客户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                }
            }
        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerNo.Text != "")
            {
                init_SelectedCustomerInfo(txtCustomerNo.Text);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
