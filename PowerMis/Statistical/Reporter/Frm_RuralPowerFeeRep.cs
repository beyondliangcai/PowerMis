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
    public partial class Frm_RuralPowerFeeRep : Form
    {
        private string month;
        public Frm_RuralPowerFeeRep(string month)
        {
            InitializeComponent();
            this.month = month;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_RuralPowerFeeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From TEMP_RAREAPOWER ORDER BY RAreaNo";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            RuralPowerFeeRep ruralPowerFeeRep = new RuralPowerFeeRep();
            ruralPowerFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = ruralPowerFeeRep;
            TextObject txtMonth = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;

            sql = "Select * From TEMP_RAREAPOWERSUM";
            dt = SQLUtl.Query(sql).Tables["dataSet"];

            TextObject txtOfferPowerY = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtOfferPowerY"];
            txtOfferPowerY.Text = String.Format("{0:#,##0}", dt.Rows[0]["OfferPower"]);
            TextObject txtBackPower1Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower1Y"];
            txtBackPower1Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower1"]);
            TextObject txtBackPower2Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower2Y"];
            txtBackPower2Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower2"]);
            TextObject txtBackPower3Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower3Y"];
            txtBackPower3Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower3"]);
            TextObject txtBackPower4Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower4Y"];
            txtBackPower4Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower4"]);
            TextObject txtBackPower5Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower5Y"];
            txtBackPower5Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower5"]);
            TextObject txtBackPower6Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower6Y"];
            txtBackPower6Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower6"]);
            TextObject txtBackPower7Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower7Y"];
            txtBackPower7Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower7"]);
            TextObject txtBackPower8Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower8Y"];
            txtBackPower8Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower8"]);
            TextObject txtBackPower9Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower9Y"];
            txtBackPower9Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower9"]);
            TextObject txtBackPower10Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower10Y"];
            txtBackPower10Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower10"]);
            TextObject txtBackPower11Y = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPower11Y"];
            txtBackPower11Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower11"]);
            TextObject txtBackPowerSumY = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPowerSumY"];
            txtBackPowerSumY.Text = String.Format("{0:#,##0}", dt.Rows[0]["SumBackPower"]);
            TextObject txtBackPercentY = (TextObject)ruralPowerFeeRep.ReportDefinition.ReportObjects["txtBackPercentY"];
            txtBackPercentY.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["SumPercent"]);
            
        }
    }
}
