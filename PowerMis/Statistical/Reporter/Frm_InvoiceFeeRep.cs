using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using DBUtility;
using Common;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_InvoiceFeeRep : Form
    {
        string[] date;
        string time;
        string year;
        string month;
        public Frm_InvoiceFeeRep(string[] date)
        {
            InitializeComponent();
            this.date = date;
            time = date[0] + "-" + date[1] + "-01";
            year = date[0];
            month = date[1];
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_InvoiceFeeRep_Load(object sender, EventArgs e)
        {
            float countryAnnex = 0;
            double ruralAddFee = 0;
            string type;
            double totalMoney;
            string strSql = "";
            DataTable dt = null;
            try
            {
                strSql = "select * from countrycityannexinfo order by annexdate asc";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count > 0)
                    countryAnnex = float.Parse(dt.Rows[0]["CountryAnnex"].ToString());

                strSql = "Delete From temp_invoicetotal";
                SQLUtl.ExecuteSql(strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"111");
                return;
            }

            try
            {
                strSql = "SELECT InvoiceTypeInfo.InvoiceType, IsNull(TotalMoney, 0) TotalMoney ,ISNULL(CountFeeAmount,0) CountFeeAmount FROM InvoiceTypeInfo "
                        +"left  join (select * from V_Invoice_Fee  Where InvoicePrintDate = '"+time+"') V on InvoiceTypeInfo.InvoiceType = V.InvoiceType";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    type = dt.Rows[i]["InvoiceType"].ToString();
                    totalMoney = double.Parse(dt.Rows[i]["TotalMoney"].ToString());
                    if (type.Equals("增值税发票"))
                    {
                        ruralAddFee = double.Parse(dt.Rows[i]["CountFeeAmount"].ToString()) * countryAnnex;
                        totalMoney = totalMoney - ruralAddFee;
                    }
                    strSql = "Insert Into temp_invoicetotal(InvoiceType, TotalMoney) Values('"+type+"', '"+totalMoney+"')";
                    SQLUtl.ExecuteSql(strSql);
                }

                strSql = "Select * From temp_invoicetotal Where InvoiceType = '普通发票' ";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count > 0)
                {
                    totalMoney = double.Parse(dt.Rows[0]["TotalMoney"].ToString()) + ruralAddFee;
                    SQLUtl.ExecuteSql("Update temp_invoicetotal Set TotalMoney='" + totalMoney + "' Where InvoiceType = '普通发票'");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"222");
                return;
            }

            strSql = "Select * From temp_invoicetotal";
            dt = SQLUtl.Query(strSql).Tables["dataSet"];
            InvoiceFeeRep invoiceFeeRep = new InvoiceFeeRep();
            invoiceFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = invoiceFeeRep;

            TextObject txtMonth = (TextObject)invoiceFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)invoiceFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;

            strSql = "Select Count(*) AS pt From CountFee Where InvoiceType = '普通发票' "
                    +" AND InvoiceFlag = 1 AND year(InvoicePrintDate) = '"+year+"' And Month(InvoicePrintDate) = '"+month+"'";
            dt = SQLUtl.Query(strSql).Tables["dataSet"];
            TextObject txtP = (TextObject)invoiceFeeRep.ReportDefinition.ReportObjects["txtP"];
            txtP.Text = dt.Rows[0]["pt"].ToString();

            strSql = "Select Count(*) AS zz From CountFee Where InvoiceType = '增值税发票' "
                   + " AND InvoiceFlag = 1 AND year(InvoicePrintDate) = '" + year + "' And Month(InvoicePrintDate) = '" + month + "'";
            dt = SQLUtl.Query(strSql).Tables["dataSet"];
            TextObject txtZ = (TextObject)invoiceFeeRep.ReportDefinition.ReportObjects["txtZ"];
            txtZ.Text = dt.Rows[0]["zz"].ToString();

            strSql = "Select Count(*) AS nw From CountFee Where InvoiceType = '农网发票' "
                   + " AND InvoiceFlag = 1 AND year(InvoicePrintDate) = '" + year + "' And Month(InvoicePrintDate) = '" + month + "'";
            dt = SQLUtl.Query(strSql).Tables["dataSet"];
            TextObject txtN = (TextObject)invoiceFeeRep.ReportDefinition.ReportObjects["txtN"];
            txtN.Text = dt.Rows[0]["nw"].ToString();

        }
    }
}
