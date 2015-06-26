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

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_DayChargeStat : Form
    {
        public Frm_DayChargeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-') ;
            string date = str[0] + "-" + str[1] + "-" + str[2];
            string printMan = Constant.LoginUser.UserName;
            fillTable(date, printMan);
            Frm_DayChargeRep frmDayChargeRep = new Frm_DayChargeRep(date);
            frmDayChargeRep.Show();

        }

        private void fillTable(string date, string printMan)
        {
            int customerCount = 0;
            double DayAdvanceDeposit = 0;
            double AllTotalMoney = 0;
            //float aTemp = 0;
            float factRec = 0;
            double lastRec = 0;
            string strSql = "";
            DataTable dt = null;
            string oldCustomerNo = "";
            strSql = "Delete From Temp_DayCharge";
            SQLUtl.ExecuteSql(strSql);
            try
            {
                strSql = "select CustomerNo,CountFeeDate,ROUND(ISNULL(TotalMoney,0),2) as DayCharge ,ISNULL(AccountRec,0) as AccountRec,ISNULL(FactRec,0) as FactRec,isnull(advancedeposit,0) as Deposit "
                        + "from V_DayCharge where InvoicePrintDate = '" + date + "' and InvoicePrintMan='" + printMan + "'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                customerCount = dt.Rows.Count;
                if (customerCount > 0)
                {
                    for (int i = 0; i < customerCount; i++)
                    {
                        string[] time = dt.Rows[i]["CountFeeDate"].ToString().Split('-') ;
                        string countFeeDate = time[0] + "-" + time[1] + "-01";

                        AllTotalMoney = AllTotalMoney + double.Parse(dt.Rows[i]["DayCharge"].ToString());
                        //DayAdvanceDeposit = DayAdvanceDeposit + float.Parse(dt.Rows[i]["Deposit"].ToString());
                        factRec = factRec + float.Parse(dt.Rows[i]["FactRec"].ToString());
                       
                        if (dt.Rows[i]["CustomerNo"].ToString() != oldCustomerNo)
                        {
                            strSql = "select top 1 AdvanceDeposit,CountFeeDate from V_DayCharge "
                                     + "where CustomerNo='" + dt.Rows[i]["CustomerNo"].ToString() + "' and CountFeeDate<'" + countFeeDate + "' order by CountFeeDate desc";
                            DataTable dt1 = SQLUtl.Query(strSql).Tables["dataSet"];
                            if (dt1.Rows.Count > 0)
                            {
                                lastRec = lastRec + float.Parse(dt1.Rows[0]["AdvanceDeposit"].ToString());
                            }
                            oldCustomerNo = dt.Rows[i]["CustomerNo"].ToString();
                        }
                        DayAdvanceDeposit = factRec + lastRec - AllTotalMoney;
                    }
                }
                strSql = "insert into temp_daycharge (customerCount,daycharge,DayAdvanceDeposit,AccountRec,FactRec) "
                         + "values (" + customerCount + "," + AllTotalMoney + "," + DayAdvanceDeposit + "," + lastRec + "," + factRec + ")";

                SQLUtl.ExecuteSql(strSql);

                //如果发现AddPreSaveTimes 当天信息不为空，则修改customerCount=customerCount+times
      //          int times;
      //          string SelectaddPreSaveTimesSql = "select * from AddPreSaveTimes where  AddDate='" + date + "'";
      //          DataTable AddPreSaveTimesTable = SQLUtl.Query(SelectaddPreSaveTimesSql).Tables["dataSet"];
      //          if (AddPreSaveTimesTable.Rows.Count == 0)
      //          {
      //              
      //          }
                //如果当天预存有记录
       //         else
       //         {
       //             MessageBox.Show("lastcustomerCount:" + customerCount);
       //             for (int k = 0; k < AddPreSaveTimesTable.Rows.Count; k++)
       //             {
       //                 times = int.Parse(AddPreSaveTimesTable.Rows[k]["Times"].ToString().Trim());
       //                 customerCount = customerCount + times;
       //             }
       //             MessageBox.Show("customerCount:" + customerCount);

       //             string Updatetemp_daycharge = "update temp_daycharge set customerCount='" + customerCount + "'";
       //             SQLUtl.ExecuteSql(Updatetemp_daycharge);
       //         }
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
