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
    public partial class Frm_DayChargeRep : Form
    {
        string date;
        double TotalMoney = 0;
        public Frm_DayChargeRep(string date)
        {
            InitializeComponent();
            this.date = date;
        }

        private void Frm_DayChargeRep_Load(object sender, EventArgs e)
        {
            string sql = "Select * From Temp_DayCharge ";
            DataTable dt = SQLUtl.Query(sql).Tables["dataSet"];
            DayChargeRep dayChargeRep = new DayChargeRep();
            dayChargeRep.SetDataSource(dt);
            string sql1 = "select ROUND(ISNULL(TotalMoney,0),2) as DayCharge from countfee where InvoicePrintDate = '" + date + "' and InvoicePrintMan='" + Constant.LoginUser.UserName + "' and NegativeInvoiceFlag=1";
            DataTable dt1 = SQLUtl.Query(sql1).Tables["dataSet"];
            int count = dt1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                TotalMoney = TotalMoney + double.Parse(dt1.Rows[i]["DayCharge"].ToString());
            }
                crystalReportViewer1.ReportSource = dayChargeRep;
            TextObject txtMonth = (TextObject)dayChargeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = date;
            TextObject txtPeople = (TextObject)dayChargeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            //TextObject txtDemo = (TextObject)dayChargeRep.ReportDefinition.ReportObjects["txtDemo"];
            //txtDemo.Text = "其中负数发票：" + count + " 张，负数发票总金额：" + TotalMoney + " 元";
        }
    }
}
