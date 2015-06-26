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
    public partial class Frm_AreaPowerFeeRep : Form
    {
        private string month = "";
        private string top = "";
        public Frm_AreaPowerFeeRep(string month, string top)
        {
            InitializeComponent();
            this.month = month;
            this.top = top;
           
        }

        private void Frm_AreaPowerFeeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From TEMP_CAREAPOWER ORDER BY AreaNo";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            AreaPowerFeeRep areaPowerFeeRep = new AreaPowerFeeRep();
            areaPowerFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = areaPowerFeeRep;
            TextObject txtTop = (TextObject)areaPowerFeeRep.ReportDefinition.ReportObjects["txtTop"];
            txtTop.Text = top;
            TextObject txtMonth = (TextObject)areaPowerFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)areaPowerFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }
    }
}
