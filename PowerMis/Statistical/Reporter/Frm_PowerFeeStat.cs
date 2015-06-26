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

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_PowerFeeStat : Form
    {
        private int repType;
        public Frm_PowerFeeStat()
        {
            InitializeComponent();
        }

        private void Frm_PowerFeeStat_Load(object sender, EventArgs e)
        {
            cbxRepType.Items.Add("包含局内用户");
            cbxRepType.Items.Add("不含局内用户");
            cbxRepType.SelectedIndex = 0;
        }

        private void calcMonthFee()
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            if (cbxRepType.SelectedIndex == 0)
            { repType = 0; }
            if (cbxRepType.SelectedIndex == 1)
            { repType = 1; }
            SqlParameter[] sqlParams1 = { new SqlParameter("@repType", repType), new SqlParameter("@selectMonth", time) };
            SqlParameter[] sqlParams2 = { new SqlParameter("@repType", repType), new SqlParameter("@selectMonth", time) };
            try
            {
                SQLUtl.RunProcedure("dt_ClacMonthPower",CommandType.StoredProcedure,"dataSet",sqlParams1);
                SQLUtl.RunProcedure("dt_ClacYearPower", CommandType.StoredProcedure, "dataSet", sqlParams2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string selectMonth = dateTimePicker1.Value.ToShortDateString().Split('-')[1];
            double totalMoney = 0;
            double countryAnnex = 0;
            float countFeeAmount = 0;
            calcMonthFee();
            string strSql = "";
            DataTable dt = null;
            try
            {
                strSql = "select * from countrycityannexinfo order by annexdate DESC";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                countryAnnex = double.Parse(dt.Rows[0]["CountryAnnex"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

            try
            {
                strSql = "select * from temp_MonthPowerFee where invoicetype='增值税发票'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                countFeeAmount = float.Parse(dt.Rows[0]["CountAmount"].ToString());
                totalMoney = double.Parse(dt.Rows[0]["TotalFee"].ToString());
                totalMoney = totalMoney - countryAnnex * countFeeAmount;

                strSql = "update temp_MonthPowerFee set TotalFee=" + totalMoney + " where invoicetype='增值税发票'";
                SQLUtl.ExecuteSql(strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

            try
            {
                strSql = "select * from temp_MonthPowerFee where invoicetype='普通发票'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                //countFeeAmount = float.Parse(dt.Rows[0]["CountAmount"].ToString());
                totalMoney = double.Parse(dt.Rows[0]["TotalFee"].ToString());
                totalMoney = totalMoney + countryAnnex * countFeeAmount;

                strSql = "update temp_MonthPowerFee set TotalFee=" + totalMoney + " where invoicetype='普通发票'";
                SQLUtl.ExecuteSql(strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

            if (repType == 0)
            {
                Frm_PowerFeeStatRep1 frm1 = new Frm_PowerFeeStatRep1(selectMonth);
                frm1.Show();
            }

            if (repType == 1)
            {
                Frm_PowerFeeStatRep2 frm2 = new Frm_PowerFeeStatRep2(selectMonth);
                frm2.Show();
            }
            //this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
