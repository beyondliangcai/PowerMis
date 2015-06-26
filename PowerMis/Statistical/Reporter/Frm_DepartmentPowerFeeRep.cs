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
    public partial class Frm_DepartmentPowerFeeRep : Form
    {
        string month;
        public Frm_DepartmentPowerFeeRep(string month)
        {
            InitializeComponent();
            this.month = month;
        }

        private void Frm_DepartmentPowerFeeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From TEMP_HINGEPOWER ORDER BY CustomerNo";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            DepartmentPowerFeeRep depPowerFeeRep = new DepartmentPowerFeeRep();
            depPowerFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = depPowerFeeRep;
            TextObject txtMonth = (TextObject)depPowerFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)depPowerFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }
    }
}
