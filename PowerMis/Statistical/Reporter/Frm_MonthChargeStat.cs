using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBUtility;
using Common;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_MonthChargeStat : Form
    {
        public Frm_MonthChargeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] date = dateTimePicker1.Value.ToShortDateString().Split('-');
            int chracter = cbxChracter.SelectedIndex;
            float money = 0;
            fillTable(date, Constant.LoginUser.UserName);

            string time = date[0] + "-" + date[1] + "-01";
            string type = cbxChracter.Text;
            SqlParameter[] sqlParams = { new SqlParameter("@fDate", time), new SqlParameter("@fType", type), SQLUtl.MakeOutParam("@Deposit", SqlDbType.Float, 0) };
            try
            {
                string deposit = SQLUtl.ExecuteProcedure("gaoxiong_pDeposit", sqlParams, "@Deposit");
                money = float.Parse(deposit);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"111");
                return;
            }
            Frm_MonthChargeRep frmMonthChargeRep = new Frm_MonthChargeRep(date, chracter,money);
            frmMonthChargeRep.Show();
        }

        private void Frm_MonthChargeStat_Load(object sender, EventArgs e)
        {
            cbxChracter.Items.Add("城网");
            cbxChracter.Items.Add("农网");
            cbxChracter.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillTable(string[] date, string printMan)
        {
            int customerCount = 0;
            double MonthAdvanceDeposit = 0;
            double MonthTotalMoney = 0;
            double AllTotalMoney = 0;
            double RedValue = 0;
            double aTemp = 0;
            string year = date[0];
            string month = date[1];
            string strSql = "";
            DataTable dt = null;

            strSql = "Delete From Temp_MonthCharge";
            SQLUtl.ExecuteSql(strSql);
            strSql = "select UserName from System";
      
            try
            {
                DataTable dt1 = SQLUtl.Query(strSql).Tables["dataSet"];
                for(int j=0; j<dt1.Rows.Count; j++)
                {
                    customerCount = 0;
                    MonthAdvanceDeposit = 0;
                    MonthTotalMoney = 0;
                    AllTotalMoney = 0;
                    RedValue = 0;
                    aTemp = 0;

                    strSql = "select CustomerNo,CountFeeDate,ISNULL(totalMoney,0) as MonthCharge ,ISNULL(accountrec,0) as AccountRec,ISNULL(FactRec,0) as FactRec,isnull(advancedeposit,0) as Deposit "
                        + "from V_DayCharge where ElectriCharacterName='" + cbxChracter.SelectedItem.ToString() + "' and year(InvoicePrintDate) ='" + year + "' and  month(InvoicePrintDate) ='" + month + "' and InvoicePrintMan='" + dt1.Rows[j]["UserName"] + "'";
                    dt = SQLUtl.Query(strSql).Tables["dataSet"]; 
                    customerCount = dt.Rows.Count;
                    if (customerCount != 0)
                    {
                        for (int i = 0; i < customerCount; i++)
                        {
                            string[] time = dt.Rows[i]["CountFeeDate"].ToString().Split('-');
                            string countFeeDate = time[0] + "-" + time[1] + "-01";
                            strSql = "select top 1 AdvanceDeposit,CountFeeDate from V_DayCharge "
                                    + "where CustomerNo='" + dt.Rows[i]["CustomerNo"].ToString() + "' and CountFeeDate<'" + countFeeDate + "' order by CountFeeDate desc";
                            DataTable dt2 = SQLUtl.Query(strSql).Tables["dataSet"];
                            AllTotalMoney = AllTotalMoney + double.Parse(dt.Rows[i]["MonthCharge"].ToString());
                            MonthTotalMoney = MonthTotalMoney + double.Parse(dt.Rows[i]["Deposit"].ToString());
                            MonthAdvanceDeposit = MonthAdvanceDeposit + double.Parse(dt.Rows[i]["AccountRec"].ToString());
                            RedValue = RedValue + double.Parse(dt.Rows[i]["FactRec"].ToString());
                            if (dt2.Rows.Count > 0 && dt2.Rows[0]["AdvanceDeposit"].ToString() != "")
                            {
                                aTemp = aTemp + double.Parse(dt2.Rows[0]["AdvanceDeposit"].ToString());
                            }
                        }

                        if (cbxChracter.SelectedIndex == 0)
                        {
                            strSql = "insert into temp_monthcharge (username,customerCount,monthcharge,monthadvancedeposit,FactRec,AccountRec) "
                                 + "values ('" + dt1.Rows[j]["UserName"] + "'," + customerCount + "," + aTemp + "," + MonthTotalMoney + "," + RedValue + "," + MonthAdvanceDeposit + ")";
                        }
                        else
                        {
                            strSql = "insert into temp_monthcharge (username,customerCount,monthcharge,monthadvancedeposit,FactRec,AccountRec) "
                                + "values ('" + dt1.Rows[j]["UserName"] + "'," + customerCount + "," + aTemp + "," + MonthTotalMoney + "," + RedValue + "," + AllTotalMoney + ")";
                        }
                        
                        SQLUtl.ExecuteSql(strSql);
                    }
                  
                }
                //如果发现AddPreSaveTimes 当天信息不为空，则修改customerCount=customerCount+times
         //       int times;
         //       string SelectaddPreSaveTimesSql = "select * from AddPreSaveTimes where  year(AddDate)='" + year + "' and month(AddDate)='" + month + "'";
         //       DataTable AddPreSaveTimesTable = SQLUtl.Query(SelectaddPreSaveTimesSql).Tables["dataSet"];
         //       if (AddPreSaveTimesTable.Rows.Count == 0)
         //       {

         //       }
                //如果当天预存有记录
         //       else
         //       {
         //           MessageBox.Show("lastcustomerCount:" + customerCount);
         //           for (int k = 0; k < AddPreSaveTimesTable.Rows.Count; k++)
         //           {
         //               times = int.Parse(AddPreSaveTimesTable.Rows[k]["Times"].ToString().Trim());
         //               customerCount = customerCount + times;
         //           }
         //           MessageBox.Show("customerCount:" + customerCount);

         //           string Updatetemp_monthcharge = "update temp_monthcharge set customerCount='" + customerCount + "'";
         //           SQLUtl.ExecuteSql(Updatetemp_monthcharge);
         //       }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"222");
                return;
            }
        }
    }
}
