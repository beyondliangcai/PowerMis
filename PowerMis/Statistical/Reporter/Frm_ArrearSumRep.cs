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
    public partial class Frm_ArrearSumRep : Form
    {
        string year;
       
        public Frm_ArrearSumRep(string year)
        {
            InitializeComponent();
            this.year = year;
        }

        private void Frm_ArrearSumRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_ArrearSum order by VolumeNo";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            ArrearSumRep arrearSumRep = new ArrearSumRep();
            arrearSumRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = arrearSumRep;
            TextObject txtYear = (TextObject)arrearSumRep.ReportDefinition.ReportObjects["txtYear"];
            txtYear.Text = year;
            TextObject txtPeople = (TextObject)arrearSumRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;

        }
    }
}
