using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using Common;
using CrystalDecisions.CrystalReports.Engine;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_NegativeInvoiceRep : Form
    {
        private string[] date;
        public Frm_NegativeInvoiceRep(string[] date)
        {
            InitializeComponent();
            this.date = date;
        }

        private void Frm_NegativeInvoiceRep_Load(object sender, EventArgs e)
        {
            string strSql = "";
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                strSql = "select * from NegativeInvoice where Printman = '" + Constant.LoginUser.UserName + "' and Year(PrintDate)= '"+ date[0] +"' and Month(PrintDate)= '"+ date[1] +"'";
            }
            else
            {
                strSql = "select * from NegativeInvoice where Year(PrintDate)= '" + date[0] + "' and Month(PrintDate)= '" + date[1] + "'";
            }

            DataTable dt = SQLUtl.Query(strSql).Tables["dataSet"];
            NegativeInvoiceRep negativeInvoiceRep = new NegativeInvoiceRep();
            negativeInvoiceRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = negativeInvoiceRep;

            TextObject txtMonth = (TextObject)negativeInvoiceRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = date[0] + "年" + date[1] + "月";
            TextObject txtPeople = (TextObject)negativeInvoiceRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            TextObject txtNum = (TextObject)negativeInvoiceRep.ReportDefinition.ReportObjects["txtNum"];
            txtNum.Text = dt.Rows.Count.ToString();
        }
    }
}
