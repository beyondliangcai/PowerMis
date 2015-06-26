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
    public partial class Frm_RuralOweRep : Form
    {
        string date;
        string month;
        float money = 0;
        public Frm_RuralOweRep(float money, string date)
        {
            InitializeComponent();
            this.date = date;
            this.month = date.Split('-')[1];
            this.money = money;
        }

        private void Frm_RuralOweRep_Load(object sender, EventArgs e)
        {
            
            ruralOweRep ruralOweRep = new ruralOweRep();
            crystalReportViewer1.ReportSource = ruralOweRep;
            TextObject txtMonth = (TextObject)ruralOweRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)ruralOweRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            TextObject txtMoney = (TextObject)ruralOweRep.ReportDefinition.ReportObjects["txtMoney"];
            txtMoney.Text = String.Format("{0:###0.00}", money);

            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }
    }
}
