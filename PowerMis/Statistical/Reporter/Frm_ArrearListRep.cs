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
    public partial class Frm_ArrearListRep : Form
    {
        private string month;
        private string volumNo;
        public Frm_ArrearListRep(string month, string volumNo)
        {
            InitializeComponent();
            this.month = month;
            this.volumNo = volumNo;
        }

        private void Frm_ArrearListRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_CityArrear ORDER BY CustomerPosition";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            ArrearListRep arrearListRep = new ArrearListRep();
            arrearListRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = arrearListRep;
            TextObject txtMonth = (TextObject)arrearListRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtVolumNo = (TextObject)arrearListRep.ReportDefinition.ReportObjects["txtVolumeNo"];
            txtVolumNo.Text = volumNo;
            TextObject txtPeople = (TextObject)arrearListRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
        }
    }
}
