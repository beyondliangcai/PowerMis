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
    public partial class Frm_LinePowerFeeRep : Form
    {
        private string month;
        private string time;
        public Frm_LinePowerFeeRep(string month, string time)
        {
            InitializeComponent();
            this.month = month;
            this.time = time;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_LinePowerFeeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From TEMP_LINEPOWER ORDER BY LineNum";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            LinePowerFeeRep linePowerFeeRep = new LinePowerFeeRep();
            linePowerFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = linePowerFeeRep;
            TextObject txtMonth = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;

            sql = "Select * From TEMP_LINEPOWERSUM";
            dt = SQLUtl.Query(sql).Tables["dataSet"];

            TextObject txtOfferPowerY = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtOfferPowerY"];
            txtOfferPowerY.Text = String.Format("{0:#,##0}", dt.Rows[0]["OfferPower"]);
            TextObject txtBackPower1Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower1Y"];
            txtBackPower1Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower1"]);
            TextObject txtBackPower2Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower2Y"];
            txtBackPower2Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower2"]);
            TextObject txtBackPower3Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower3Y"];
            txtBackPower3Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower3"]);
            TextObject txtBackPower4Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower4Y"];
            txtBackPower4Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower4"]);
            TextObject txtBackPower5Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower5Y"];
            txtBackPower5Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower5"]);
            TextObject txtBackPower6Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower6Y"];
            txtBackPower6Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower6"]);
            TextObject txtBackPower7Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower7Y"];
            txtBackPower7Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower7"]);
            TextObject txtBackPower8Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower8Y"];
            txtBackPower8Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower8"]);
            TextObject txtBackPower9Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower9Y"];
            txtBackPower9Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower9"]);
            TextObject txtBackPower10Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower10Y"];
            txtBackPower10Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower10"]);
            TextObject txtBackPower11Y = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPower11Y"];
            txtBackPower11Y.Text = String.Format("{0:#,##0}", dt.Rows[0]["BackPower11"]);
            TextObject txtBackPowerSumY = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPowerSumY"];
            txtBackPowerSumY.Text = String.Format("{0:#,##0}", dt.Rows[0]["SumBackPower"]);
            TextObject txtBackPercentY = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtBackPercentY"];
            txtBackPercentY.Text = String.Format("{0:#,##0}", dt.Rows[0]["SumPercent"]);

            sql = "Select ISNull(Sum(ReceivePower), 0) as SumReceivePower From ReceivePower Where ReceiveDate = '" +time+ "'";
            dt = SQLUtl.Query(sql).Tables["dataSet"];
            TextObject txtReceivePower = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtReceivePower"];
            txtReceivePower.Text = String.Format("{0:#,##0}", dt.Rows[0]["SumReceivePower"]);
            float recivePower = float.Parse(dt.Rows[0]["SumReceivePower"].ToString());

            sql = "Select ISNull(Sum(SumBackPower), 0) as SumBackPower From TEMP_LINEPOWER";
            dt = SQLUtl.Query(sql).Tables["dataSet"];
            float backPower = float.Parse(dt.Rows[0]["SumBackPower"].ToString());
            float percent = backPower / recivePower * 100;
            TextObject txtReceivePercent = (TextObject)linePowerFeeRep.ReportDefinition.ReportObjects["txtReceivePercent"];
            txtReceivePercent.Text = String.Format("{0:#,##0}", percent);
            
        }
    }
}
