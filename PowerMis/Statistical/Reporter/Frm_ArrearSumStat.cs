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
    public partial class Frm_ArrearSumStat : Form
    {
        public Frm_ArrearSumStat()
        {
            InitializeComponent();
        }

        private void Frm_ArrearSumStat_Load(object sender, EventArgs e)
        {
            fillYear();
        }

        private void fillYear()
        {
            string strSql = "Select * From V_Year Order by year0 desc";
            DataTable dt = SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbxYear.Items.Add(dt.Rows[i]["year0"].ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string year = cbxYear.Text;
            fillTempTable(year);
            Frm_ArrearSumRep frmArrearSumRep = new Frm_ArrearSumRep(year);
            frmArrearSumRep.Show();
        }

        public void fillTempTable(string year)
        {
            string VolumeNo = "";
            string strSql = "";
            List<string> strSQL = new List<string>();
            DataTable dt = null;
            try
            {
                strSql = "Delete From  Temp_ArrearSum";
                SQLUtl.ExecuteSql(strSql);

                strSql = "Select VolumeNo From V_LotteArrear where year(CountFeeDate)='" + year + "' order by VolumeNo";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql = "insert into Temp_ArrearSum(VolumeNo) values('" + dt.Rows[i]["VolumeNo"].ToString() + "')";
                    if (!VolumeNo.Equals(dt.Rows[i]["VolumeNo"].ToString()))
                    {
                        SQLUtl.ExecuteSql(strSql);
                        VolumeNo = dt.Rows[i]["VolumeNo"].ToString();
                    }
                }
                for (int i = 1; i <= 12; i++)
                {
                    strSql = "Select Sum(TotalMoney) As TotalMoneySum,VolumeNo From V_LotteArrear Where year(CountFeeDate)='" + year + "' and month(CountFeeDate)='" + i + "' Group By VolumeNo";
                    dt = SQLUtl.Query(strSql).Tables["dataSet"];
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == 1)
                        {
                            strSql = "Update Temp_ArrearSum Set JanArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 2)
                        {
                            strSql = "Update Temp_ArrearSum Set FebArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 3)
                        {
                            strSql = "Update Temp_ArrearSum Set MarArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 4)
                        {
                            strSql = "Update Temp_ArrearSum Set AprArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 5)
                        {
                            strSql = "Update Temp_ArrearSum Set MayArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 6)
                        {
                            strSql = "Update Temp_ArrearSum Set JunArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 7)
                        {
                            strSql = "Update Temp_ArrearSum Set JulArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 8)
                        {
                            strSql = "Update Temp_ArrearSum Set AugArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 9)
                        {
                            strSql = "Update Temp_ArrearSum Set SepArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 10)
                        {
                            strSql = "Update Temp_ArrearSum Set OctArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 11)
                        {
                            strSql = "Update Temp_ArrearSum Set NovArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                        else if (i == 12)
                        {
                            strSql = "Update Temp_ArrearSum Set DecArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'";
                            strSQL.Add(strSql);
                        }
                    }
                }
                SQLUtl.ExecuteSqlTran(strSQL);
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
