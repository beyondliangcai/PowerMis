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
    public partial class Frm_ArrearYearsSumRep : Form
    {
        public string minyear, maxyear;
        public Frm_ArrearYearsSumRep(string minyear,string maxyear)
        {
            InitializeComponent();
            this.minyear = minyear;
            this.maxyear = maxyear;
        }

        private void Frm_ArrearYearsSumRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_ArrearYearsSum order by VolumeNo,Year";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            ArrearYearsSumRep arrearYearsSumRep = new ArrearYearsSumRep();
           
            arrearYearsSumRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = arrearYearsSumRep;
            TextObject txtMinYear = (TextObject)arrearYearsSumRep.ReportDefinition.ReportObjects["txtMinYear"];
            txtMinYear.Text = minyear;
            TextObject txtMaxYear = (TextObject)arrearYearsSumRep.ReportDefinition.ReportObjects["txtMaxYear"];
            txtMaxYear.Text = maxyear;
            TextObject txtPeople = (TextObject)arrearYearsSumRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
        }

    }
}
