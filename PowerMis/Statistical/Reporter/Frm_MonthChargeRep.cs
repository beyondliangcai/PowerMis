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
    public partial class Frm_MonthChargeRep : Form
    {
        private string[] date;
        private int chracter;
        private float money;
        public Frm_MonthChargeRep(string[] date, int chracter, float money)
        {
            InitializeComponent();
            this.date = date;
            this.chracter = chracter;
            this.money = money;
        }

        private void Frm_MonthChargeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_MonthCharge ";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            MonthChargeRep monthChargeRep = new MonthChargeRep();
            monthChargeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = monthChargeRep;
            TextObject txtAddInfo = (TextObject)monthChargeRep.ReportDefinition.ReportObjects["txtAddInfo"];
            if (chracter == 0)
            {
                txtAddInfo.Text = "城网预存金额为：" + String.Format("{0:###0.00}", money);
            }
            else
            {
                txtAddInfo.Text = "农网未收电费为：" + String.Format("{0:###0.00}", money);
            }
            TextObject txtMonth = (TextObject)monthChargeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = date[0]+"年"+date[1]+"月";

        }
    }
}
