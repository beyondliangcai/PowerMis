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
    public partial class Frm_VolumPowerFeeRep : Form
    {
        string[] date;        
        int chracter;
        string time;
        string year;
        string month;
        string type;
        public Frm_VolumPowerFeeRep(string[] date, int chracter)
        {
            InitializeComponent();
            this.date = date;
            this.chracter = chracter;
            time = date[0] + "-" + date[1] + "-01";
            year = date[0];
            month = date[1];
            if (Constant.LoginUser.Permission.Trim().Equals("一般操作员"))
            {
                crystalReportViewer1.ShowPrintButton = false;
            }
        }

        private void Frm_VolumPowerFeeRep_Load(object sender, EventArgs e)
        {
            string strSql = "" ;
            string strSql1 = "";
            DataTable dt = null ;
            if (chracter == 0)
            {
                type = "城网";
                strSql = "Select VolumnNo, ISNULL(CountNum, 0) As CountNum , ISNull(CountFeeAmount, 0) AS CountFeeAmount,"
                        +"ISNull(FactMoney, 0) AS TotalMoney ,getDate() as CountFeeDate  From "
                        +"( select * from V_Volumn_List1 Where VolumnNo like '000%' and  CountFeeDate = '"+time+"') T order by VolumnNo asc";
                
                strSql1 = "Select ISNull(Sum(CountFeeAmount), 0) As SumCountFeeAmount, ISNull(Sum(FactMoney), 0) As SumTotalMoney  From V_Volumn_List1" 
                         + " where VolumnNo like '000%' and CountFeeDate <= '"+time+"' And Year(CountFeeDate) = '"+year+"'";
            }
            else if (chracter == 1)
            {
                type = "农网";
                strSql = "Select VolumnNo, ISNULL(CountNum, 0) As CountNum , ISNull(CountFeeAmount, 0) AS CountFeeAmount,"
                        + "ISNull(TotalMoney, 0) AS TotalMoney ,getDate() as CountFeeDate  From "
                        + "V_Volumn_List1 Where volumnNo not like '000%' and  CountFeeDate = '" + time + "' order by VolumnNo asc";
                
                /*strSql1 = "Select ISNull(Sum(CountFeeAmount), 0) As SumCountFeeAmount, ISNull(Sum(TotalMoney), 0) As SumTotalMoney  From V_Volumn_List1"
                        + " where Volumnno like '000%' and CountFeeDate <= '" + time + "' And Year(CountFeeDate) = '" + year + "'";*/
                strSql1 = "Select ISNull(Sum(CountFeeAmount), 0) As SumCountFeeAmount, ISNull(Sum(TotalMoney), 0) As SumTotalMoney  From V_Volumn_List1"
                        + " where Volumnno in ('01011','01022','02011','02021','03011','03021','03031','04011','04021','05011','05021','05031', '06011','06021','06031','07011','07021','08011','08021','08031','09011','09021','10011','10021','10031','21011','21022','21032','22011','22021', '23011','23022','23031','23051','23043','28011','28021','29011','29021','29031','30011','30021','30031','31011','31021','31021','32011','32021') and CountFeeDate <= '" + time + "' And Year(CountFeeDate) = '" + year + "'";

            
            }
            try
            {
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            VolumPowerFeeRep volumPowerFeeRep = new VolumPowerFeeRep();
            volumPowerFeeRep.SetDataSource(dt);
            crystalReportViewer1.ReportSource = volumPowerFeeRep;

            try
            {
                dt = SQLUtl.Query(strSql1).Tables["dataSet"];
                TextObject txtCountFeeAmount = (TextObject)volumPowerFeeRep.ReportDefinition.ReportObjects["txtCountFeeAmount"];
                txtCountFeeAmount.Text = String.Format("{0:#,##0}", dt.Rows[0]["SumCountFeeAmount"]);
                TextObject txtTotalMoney = (TextObject)volumPowerFeeRep.ReportDefinition.ReportObjects["txtTotalMoney"];
                txtTotalMoney.Text = String.Format("{0:#,##0.00}", dt.Rows[0]["SumTotalMoney"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            TextObject txtMonth = (TextObject)volumPowerFeeRep.ReportDefinition.ReportObjects["txtMonth"];
            txtMonth.Text = month;
            TextObject txtPeople = (TextObject)volumPowerFeeRep.ReportDefinition.ReportObjects["txtPeople"];
            txtPeople.Text = Constant.LoginUser.UserName;
            TextObject txtType = (TextObject)volumPowerFeeRep.ReportDefinition.ReportObjects["txtType"];
            txtType.Text = type;
        }
    }
}
