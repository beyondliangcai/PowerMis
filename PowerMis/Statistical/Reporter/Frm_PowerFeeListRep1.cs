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
    public partial class Frm_PowerFeeListRep1 : Form
    {
        private string month;
        private string volumNo;
        public Frm_PowerFeeListRep1(string month, string volumNo)
        {
            InitializeComponent();
            this.month = month;
            this.volumNo = volumNo;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_PowerFeeListRep1_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_PowerFeeList order by Index1";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            PowerFeeListRep1 powerFeeListRep = new PowerFeeListRep1();
            powerFeeListRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = powerFeeListRep;
            TextObject txtMonth = (TextObject)powerFeeListRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtVolumNo = (TextObject)powerFeeListRep.ReportDefinition.ReportObjects["txtVolumNo"];
            txtVolumNo.Text = volumNo;
            TextObject txtPeople = (TextObject)powerFeeListRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
        }
    }
}
