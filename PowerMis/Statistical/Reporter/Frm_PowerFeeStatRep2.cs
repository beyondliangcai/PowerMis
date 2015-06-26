using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DBUtility;
using Common;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_PowerFeeStatRep2 : Form
    {
        string month = "";
        public Frm_PowerFeeStatRep2(string month)
        {
            InitializeComponent();
            this.month = month;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_PowerFeeStatRep2_Load(object sender, EventArgs e)
        {
            string sql = "Select * From temp_MonthPowerFee";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            PowerFeeStatRep powerFeeStatRep = new PowerFeeStatRep();
            powerFeeStatRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = powerFeeStatRep;
            TextObject txtMonth = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject repNum = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtNum"];
            repNum.Text = "(二)";

            sql = "Select * From temp_YearPowerFee";
            dt = SQLUtl.Query(sql).Tables["dataSet"];

            TextObject txtCountAmount = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtCountAmount"];
            txtCountAmount.Text = String.Format("{0:#,##0}", dt.Rows[0]["CountAmount"]);
            TextObject txtRuralFee = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtRuralFee"];
            txtRuralFee.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["CountryAnnex"]);
            TextObject txtCityFee = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtCityFee"];
            txtCityFee.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["CityAnnex"]);
            TextObject txtBaseFee = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtBaseFee"];
            txtBaseFee.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["BaseFee"]);
            TextObject txtTotalFee = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtTotalFee"];
            txtTotalFee.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["TotalFee"]);

            TextObject txtPnum = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtPnum"];
            txtPnum.Text = dt.Rows[0]["pt"].ToString();
            TextObject txtZnum = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtZnum"];
            txtZnum.Text = dt.Rows[0]["zz"].ToString();
            TextObject txtNnum = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtNnum"];
            txtNnum.Text = dt.Rows[0]["lw"].ToString();
            TextObject txtPeople = (TextObject)powerFeeStatRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
        }
    }
}
