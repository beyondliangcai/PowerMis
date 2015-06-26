using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DBUtility;
using BusinessModel;

namespace PowerMis.Statistical
{
    public partial class Frm_ValueAddedTax : Form
    {
        private Customer customer = null;
        private float LastMonthStart = 0;       //上月止码
        private float CountryAnnex = 0;         //农网附加比率
        private int[] PowerPriceNo = null;
        private double[] PowerPrice = null;
        private float[] PowerPriceRate = null;

        public Frm_ValueAddedTax()
        {
            InitializeComponent();
            customer = new Customer();
            PowerPriceNo = new int[4];
            PowerPrice = new double[4];
            PowerPriceRate = new float[4];
        }

        private void Frm_ValueAddedTax_Load(object sender, EventArgs e)
        {
            fillBookNo();
            try
            {
                string strSQL = "select CountryAnnex from CountryCityAnnexinfo order by AnnexDate asc";
                DataSet dataSet = SQLUtl.Query(strSQL);
                DataTable dataTable = dataSet.Tables["dataSet"];
                CountryAnnex = float.Parse(dataTable.Rows[0]["CountryAnnex"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void fillBookNo()
        {
            try
            {
                string strSQL = "SELECT DISTINCT Left(CustomerNo, 5) AS BookNo FROM CustomerInfo Where InvoiceType = '增值税发票' ORDER BY BookNo";

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
            lbxCustomerList.Items.Clear();
            try
            {
                string strSQL = "SELECT CustomerNo,CustomerName FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' and InvoiceType ='增值税发票'";
                DataSet dataSet = SQLUtl.Query(strSQL);
                DataTable dataTable = dataSet.Tables["dataSet"];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    lbxCustomerList.Items.Add(dataTable.Rows[i][0] + " " + dataTable.Rows[i][1]);
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

        private void txtCustomerNoKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxBookNo.Text = txtCustomerNoKey.Text.Substring(0, 5);
                int i = lbxCustomerList.FindString(txtCustomerNoKey.Text);
                lbxCustomerList.SelectedIndex = i;
                lbxCustomerList.TopIndex = i;
            }
        }

        private void clearForm()
        {
            txtPowerValue.Text = "";
            txtMultiple.Text = "";
            txtCountPowerValue.Text = "";
            txtCityPrice.Text = "";
            txtBasePrice.Text = "";
            txtTotalPrice.Text = "";
            txtPowerPrice1.Text = "";
            txtPowerRate1.Text = "";
            txtPowerPrice2.Text = "";
            txtPowerRate2.Text = "";
            txtPowerPrice3.Text = "";
            txtPowerRate3.Text = "";
            txtPowerPrice4.Text = "";
            txtPowerRate4.Text = "";
        }

        private void init_SelectedCustomerInfo(string customerNo)
        {
            clearForm();
            float lastMonthValue = 0;
            float thisMonthValue = 0;
            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            //DateTime TimeNow = Convert.ToDateTime(time);
            string strSql1 = "SELECT CustomerName,AmmeterValue FROM CustomerInfo,AmmeterValue"
                            + " WHERE CustomerInfo.CustomerNo='" + customerNo + "' AND CopyValueDate < '" + time + "'"
                            + "AND CustomerInfo.CustomerNo=AmmeterValue.CustomerNo ORDER BY CopyValueDate DESC";
            try
            {
                DataSet dataSet1 = SQLUtl.Query(strSql1);
                // 将Dataset里的datatable取出来返回
                DataTable dataTable1 = dataSet1.Tables["dataSet"];
                if (dataTable1.Rows.Count == 0)
                {
                    string strSql2 = "SELECT CustomerName FROM CustomerInfo WHERE CustomerNo='" + customerNo + "'";
                    DataSet dataSet2 = SQLUtl.Query(strSql2);
                    DataTable dataTable2 = dataSet2.Tables["dataSet"];
                    customer.setCustomerName(dataTable2.Rows[0][0].ToString());
                }
                else
                {
                    customer.setCustomerName(dataTable1.Rows[0][0].ToString());
                    lastMonthValue = float.Parse(dataTable1.Rows[0][1].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"00001");
            }

            string strSql3 = "SELECT AmmeterValue FROM CustomerInfo,AmmeterValue"
                            + " WHERE CustomerInfo.CustomerNo='" + customerNo + "' AND CopyValueDate = '" + time + "'"
                            + " AND CustomerInfo.CustomerNo=AmmeterValue.CustomerNo ORDER BY CopyValueDate DESC";
            try
            {
                DataSet dataSet3 = SQLUtl.Query(strSql3);
                // 将Dataset里的datatable取出来返回
                DataTable dataTable3 = dataSet3.Tables["dataSet"];
                if (dataTable3.Rows.Count != 0)
                {
                    thisMonthValue = float.Parse(dataTable3.Rows[0][0].ToString());
                }
                else
                {
                    MessageBox.Show("此月电费尚未录入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"00002");
            }

            txtCustomerName.Text = customer.getCustomerName();
            txtLastMonthValue.Text = lastMonthValue.ToString();
            LastMonthStart = lastMonthValue;
            txtThisMonthValue.Text = thisMonthValue.ToString();

            string strSql4 = "SELECT * FROM CountFee WHERE CustomerNo='" + customerNo + "' AND CountFeeDate='" + time + "'";
            try
            {
                DataSet dataSet4 = SQLUtl.Query(strSql4);
                if (dataSet4.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet4.Tables[0].Rows[0];
                    //txtPowerValue.Text = dataRow["MonthCopyAmount"].ToString();
                    float powervalue = float.Parse(dataRow["MonthCopyAmount"].ToString());
                    txtPowerValue.Text = String.Format("{0:###0.00}", powervalue);
                    txtMultiple.Text = dataRow["AmmeterMulti"].ToString();
                    txtCountPowerValue.Text = dataRow["CountfeeAmount"].ToString();
                    //txtCityPrice.Text = dataRow["CityAnnex"].ToString();
                    float CityAnnex = float.Parse(dataRow["CityAnnex"].ToString());
                    txtCityPrice.Text = String.Format("{0:###0.00}", CityAnnex);
                    //txtBasePrice.Text = dataRow["BaseFeeMoney"].ToString();
                    float basePrice = float.Parse(dataRow["BaseFeeMoney"].ToString());
                    txtBasePrice.Text = String.Format("{0:###0.00}", basePrice);
                    float totalMoney = float.Parse(dataRow["TotalMoney"].ToString()) - float.Parse(dataRow["CountfeeAmount"].ToString()) * CountryAnnex;
                    txtTotalPrice.Text = String.Format("{0:###0.00}", totalMoney);
                    if (int.Parse(dataRow["PriceRate1"].ToString()) > 0)
                    {
                        txtPowerPrice1.Text = dataRow["PowerPrice1"].ToString();
                        txtPowerRate1.Text = dataRow["PriceRate1"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate2"].ToString()) > 0)
                    {
                        txtPowerPrice2.Text = dataRow["PowerPrice2"].ToString();
                        txtPowerRate2.Text = dataRow["PriceRate2"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate3"].ToString()) > 0)
                    {
                        txtPowerPrice3.Text = dataRow["PowerPrice3"].ToString();
                        txtPowerRate3.Text = dataRow["PriceRate3"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate4"].ToString()) > 0)
                    {
                        txtPowerPrice4.Text = dataRow["PowerPrice4"].ToString();
                        txtPowerRate4.Text = dataRow["PriceRate4"].ToString();
                    }
                    if (dataRow["AbnormityAmount"].ToString() != "" && dataRow["AbnormityAmount"].ToString() != "0")
                    {
                        txtLastMonthValue.Text = dataRow["LastMonthStart"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"00003");
            }
        }

        private void lbxCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerNo = "";
            if (lbxCustomerList.Items.Count != 0)
            {
                customerNo = lbxCustomerList.Text.Split(' ').GetValue(0).ToString();
            }
            txtCustomerNoKey.Text = customerNo;
            init_SelectedCustomerInfo(customerNo);
        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerNoKey.Text != "")
            {
                init_SelectedCustomerInfo(txtCustomerNoKey.Text);
            }
        }


    }
}
