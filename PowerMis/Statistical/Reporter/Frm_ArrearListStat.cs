using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_ArrearListStat : Form
    {
        public Frm_ArrearListStat()
        {
            InitializeComponent();
        }

        private void Frm_ArrearListStat_Load(object sender, EventArgs e)
        {
            string strSql = "Select Distinct Left(CustomerNo, 5) as volumNo From CustomerInfo Order By volumNo";
            DataTable dt = SQLUtl.Query(strSql).Tables["DataSet"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbxVolumNo.Items.Add(dt.Rows[i]["volumNo"].ToString());
            }
            cbxVolumNo.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] date = dateTimePicker1.Value.ToShortDateString().Split('-');
            string selectMonth = date[0] + "-" + date[1] + "-" + date[2];
            string volumNo = cbxVolumNo.Text.Trim();
            fillTempTable(volumNo, date);
            Frm_ArrearListRep frmArrearListRep = new Frm_ArrearListRep(selectMonth, volumNo);
            frmArrearListRep.Show();
        }

        public void fillTempTable(string volumNo, string[] date)
        {
            string year = date[0];
            string month = date[1];
            string CustomerNo = "";
            string strSql = "";
            string strSql1 = "";
            float totalMoney = 0;
            float lastAdvance = 0;
            string time = date[0] + "-" + date[1] + "-01";
            DataTable dt = null;
            DataTable dt1 = null;
            try
            {
                strSql = "Delete From  Temp_CityArrear";
                SQLUtl.ExecuteSql(strSql);

                strSql = "select V_CityArrear.CustomerNo,V_CityArrear.CustomerName,V_CityArrear.TotalMoney,V_CityArrear.CountFeeDate,CustomerInfo.CustomerPosition from V_CityArrear,CustomerInfo where Left(V_CityArrear.CustomerNo,5)='" + volumNo + "' "
                        + "and year(V_CityArrear.CountFeeDate) = " + year + " and month(V_CityArrear.countfeedate)<=" + month + " and V_CityArrear.CustomerNo=CustomerInfo.CustomerNo order by CustomerInfo.CustomerPosition";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    strSql = "insert into Temp_CityArrear(CustomerNo,CustomerName,CustomerPosition) values('" + dt.Rows[i]["CustomerNo"].ToString() + "','" + dt.Rows[i]["CustomerName"].ToString() + "','" + dt.Rows[i]["CustomerPosition"].ToString() + "')";
                    
                    strSql1 = "select AdvanceDeposit from CountFee where CustomerNo='" + dt.Rows[i]["CustomerNo"].ToString() + "'"
                             + " and Invoiceflag=1 and CountFeeDate<='" + time + "' order by CountFeeDate desc";
                    if (!CustomerNo.Equals(dt.Rows[i]["CustomerNo"].ToString()))
                    {
                        SQLUtl.ExecuteSql(strSql);                      
                        CustomerNo = dt.Rows[i]["CustomerNo"].ToString();
                        dt1 = SQLUtl.Query(strSql1).Tables["dataSet"];
                        lastAdvance = 0;
                        if (dt1.Rows.Count != 0 && dt1.Rows[0]["AdvanceDeposit"].ToString() != "")
                        {
                            lastAdvance = float.Parse(dt1.Rows[0]["AdvanceDeposit"].ToString());
                        }
                    }

                    totalMoney = float.Parse(dt.Rows[i]["TotalMoney"].ToString());
                    
                    if (totalMoney != 0)
                    {
                        totalMoney = totalMoney - lastAdvance;
                        lastAdvance = 0;
                        if (totalMoney < 0)
                        {
                            lastAdvance = 0 - totalMoney;
                            totalMoney = 0;
                            
                        }
                    }
                    string[] CountFeeDate = dt.Rows[i]["CountFeeDate"].ToString().Split('-');
                    int m = int.Parse(CountFeeDate[1]);
                    switch (m)
                    {
                        case 1:
                            strSql = "Update Temp_CityArrear set JanArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 2:
                            strSql = "Update Temp_CityArrear set FebArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 3:
                            strSql = "Update Temp_CityArrear set MarArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 4:
                            strSql = "Update Temp_CityArrear set AprArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 5:
                            strSql = "Update Temp_CityArrear set MayArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 6:
                            strSql = "Update Temp_CityArrear set JunArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 7:
                            strSql = "Update Temp_CityArrear set JulArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 8:
                            strSql = "Update Temp_CityArrear set AugArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 9:
                            strSql = "Update Temp_CityArrear set SepArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 10:
                            strSql = "Update Temp_CityArrear set OctArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 11:
                            strSql = "Update Temp_CityArrear set NovArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                        case 12:
                            strSql = "Update Temp_CityArrear set DecArrearMoney=" + totalMoney + " where CustomerNo='" + CustomerNo + "'";

                            break;
                    }
                    SQLUtl.ExecuteSql(strSql);
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
    }
}
