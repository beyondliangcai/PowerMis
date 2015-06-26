using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Common;
using BusinessModel;
using DBUtility;



namespace PowerMis.Statistical
{
    public partial class Frm_CityIvoicePrint : Form
    {
        private PrintDocument printDocument = null;
        private Print Print = null;
        private string CustomerNo = "";
        private string CustomerName = "";
        private string CustomerAddress = "";
        private float CountryAnnex = 0;      //农网附加比率
        private float startCode = 0;
        private float endCode = 0;
        private float copyPowerValue = 0;   //抄表电量
        private float countPowerValue = 0;  //计费电量
        private float transFormerLose = 0;
        //private float averagePrice = 0;
        private float multiple = 0;
        private float lastBalance = 0;      //上期预存
        //private float powerFee = 0;         //本月电费
        //private string powerFeeCN = "";     //金额汉字大写
        private float totalMoney = 0;
        private float factFee = 0;          //实收
        private float totalFactFee = 0;
        private float preFee = 0;           //预收
        private float thisBalance = 0;      //本期结余
        private float annex = 0;            //违约金
        private float baseFee = 0;          //基本电费
        private float cityAnnex = 0;        //城镇附加费
        private string printDate = "";      //发票打印日期
        private string payDate = "";        //缴费年月
        private string[] nopayDate = null;  //欠费日期
        private float[] nopayFee = null;    //欠费金额
        private string million, hundredThousand, tenThousand, thousand, hundren, ten, one, fens, tenFen;

        public Frm_CityIvoicePrint()
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            Print = new Print();
        }

        private void Frm_CityIvoicePrint_Load(object sender, EventArgs e)
        {
            fillYear();//填充数据库有记录的年份
            fillMonth();//填充月份
            fillBookNo(); //填充账本号

        }

        private void fillBookNo()
        {
            try
            {
                string strSQL = "SELECT DISTINCT Left(CustomerNo, 5) AS BookNo FROM CustomerInfo ORDER BY BookNo";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    cbxBookNo.Items.Add(dataTable.Rows[i][0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void fillYear()
        {
            int currentYear = DateTime.Today.Year;
            string strSql = "Select * From V_Year Order by year0 desc";
            System.Data.DataTable dt = DBUtility.SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cityInvoicePrintYear.Items.Add(dt.Rows[i]["year0"].ToString());
                //默认选择当前年
                if (dt.Rows[i]["year0"].ToString() == currentYear.ToString())
                {
                    cityInvoicePrintYear.SelectedIndex = i;
                }
            }


        }

        private void fillMonth()
        {
            //获取当前日期年份
            int currentYear = DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;


            if (cityInvoicePrintYear.SelectedItem == null)
            {
                MessageBox.Show("请选择导出年份");
            }
            else
            {
                //如果是本年
                if (int.Parse(cityInvoicePrintYear.SelectedItem.ToString()) == currentYear)
                {
                    for (int i = 0; i < currentMonth; i++)
                    {
                        cityInvoicePrintMonth.Items.Add((i + 1).ToString());

                        //默认选择当前月
                        if ((i + 1) == currentMonth)
                        {
                            cityInvoicePrintMonth.SelectedIndex = i;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        cityInvoicePrintMonth.Items.Add((i + 1).ToString());
                    }
                }
            }
        }

        private void clearForm()
        {
            txtCustomerNo.Text = "";
            txtPreFee.Text = "";
            txtLastBalance.Text = "";
            txtFactFee.Text = "";
            txtThisBalance.Text = "";
            txtAnnex.Text = "";
            //CustomerNopayList.Items.Clear();
        }

        private void lbxCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CustomerNo = lbxCustomerList.Text.Split(' ').GetValue(0).ToString();

            CustomerName = lbxCustomerList.Text.Split(' ').GetValue(1).ToString();  
            init_SelectedCustomerInfo(CustomerNo);
        }

        private void init_SelectedCustomerInfo(string CustomerNo)
        {
            clearForm();
            preFee = 0;
            lastBalance = 0;
            thisBalance = 0;
            annex = 0;
            nopayDate = null;
            nopayFee = null;
            //string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            int printYear = int.Parse(cityInvoicePrintYear.SelectedItem.ToString());
            int printMonth = int.Parse(cityInvoicePrintMonth.SelectedItem.ToString());
            string time = printYear.ToString() + "-" + printMonth.ToString() + "-01";
            string strSql = "";
            DataTable dt = null;
            string invoiceType = "";

            

            try
            {
                //Invoiceflag=1 表示这个月已经交过钱了
                string printCityInvoiceSql1 = "select AdvanceDeposit from CountFee where CustomerNo='" + CustomerNo + "'"
                        + " and Invoiceflag=1 and CountFeeDate<'" + time + "' order by CountFeeDate desc";

                DataTable printDt = SQLUtl.Query(printCityInvoiceSql1).Tables["dataSet"];
                if (printDt.Rows.Count != 0 && printDt.Rows[0]["AdvanceDeposit"].ToString() != "")
                {
                    //上次预存
                    lastBalance = float.Parse(printDt.Rows[0]["AdvanceDeposit"].ToString());
                    txtLastBalance.Text = lastBalance.ToString();
                }
                else {
                    MessageBox.Show("连接数据库失败！");
                }

                string printCityInvoiceSql2 = "select customerinfo.CustomerNo,customerinfo.CustomerName,customerinfo.CustomerAddress,StartCode,EndCode,CountfeeAmount,TotalMoney,FactRec,CountFeeDate,AdvanceDeposit,CityAnnex,customerinfo.InvoiceType,AccountRec,InvoicePrintDate,AmmeterMulti,MonthCopyAmount,transFormerLose,CountFeeAmount,BaseFeeMoney,TotalMoney from countfee,customerinfo "
                         + "where  countfee.CustomerNo =customerinfo.CustomerNo and Invoiceflag=1 and countfee.CustomerNo ='" + CustomerNo + "' and year(CountFeeDate)='" + printYear + "' and month(CountFeeDate) = '" + printMonth + "' order by Countfeedate Desc";
                    dt = SQLUtl.Query(printCityInvoiceSql2).Tables["dataSet"];
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("该用户这个月没有电费或者没有缴纳电费，请确认！");
                        this.Close();
                    }
                    else
                    {
                        //用户号
                        CustomerNo = dt.Rows[0]["CustomerNo"].ToString();
                        txtCustomerNo.Text = CustomerNo;
                        //应收金额
                        preFee = float.Parse(dt.Rows[0]["AccountRec"].ToString());
                        txtPreFee.Text = String.Format("{0:#####0.00}", preFee.ToString());
                        //实收金额
                        factFee = float.Parse(dt.Rows[0]["FactRec"].ToString());
                        txtFactFee.Text = String.Format("{0:#####0.00}", factFee.ToString());
                        //本次结余
                        thisBalance = float.Parse(dt.Rows[0]["AdvanceDeposit"].ToString());
                        txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance.ToString());
                        //违约金
                        
                        cityAnnex = float.Parse(dt.Rows[0]["CityAnnex"].ToString());
                        txtAnnex.Text = String.Format("{0:#####0.00}", annex.ToString());

                        //string[] countFeeDate = null;
                        //payDate = dt.Rows[0]["InvoicePrintDate"].ToString();
                        payDate =cityInvoicePrintYear.SelectedItem.ToString()+"年"+cityInvoicePrintMonth.SelectedItem.ToString()+"月";
                        
                        string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
                        printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";

                        CustomerAddress = dt.Rows[0]["CustomerAddress"].ToString();
                        startCode = float.Parse(dt.Rows[0]["startCode"].ToString());
                        endCode = float.Parse(dt.Rows[0]["endCode"].ToString());
                        multiple = float.Parse(dt.Rows[0]["AmmeterMulti"].ToString()); 
                        copyPowerValue = float.Parse(dt.Rows[0]["MonthCopyAmount"].ToString());
                        transFormerLose = float.Parse(dt.Rows[0]["transFormerLose"].ToString());
                        countPowerValue = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString());
                        baseFee = float.Parse(dt.Rows[0]["BaseFeeMoney"].ToString());
                        //本月金额
                        totalMoney = float.Parse(dt.Rows[0]["TotalMoney"].ToString());
                        txtTotalmoney.Text = totalMoney.ToString();
                    }

            }
            catch(Exception e){
                MessageBox.Show("连接数据库失败！"+e.ToString());
            }

        }

        private void cbxBookNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxCustomerList.Items.Clear();

            try
            {
                string strSQL = "SELECT CustomerNo,CustomerName FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' order by CustomerNo";
                DataSet dataSet = SQLUtl.Query(strSQL);
                DataTable dataTable = dataSet.Tables["dataSet"];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    lbxCustomerList.Items.Add(dataTable.Rows[i][0] + " " + dataTable.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = new Font(new FontFamily("宋体"), 12); //打印字体  
            SolidBrush myBrush = new SolidBrush(Color.Black);//刷子
            Graphics g = e.Graphics; //获得绘图对象

            //g.DrawString("客户编号：", printFont, myBrush, 150, 50);
            g.DrawString(CustomerNo, printFont, myBrush, 120, 50);
           
            //g.DrawString("客户名称：", printFont, myBrush, 420, 50);
            g.DrawString(CustomerName, printFont, myBrush, 120, 70);
            //g.DrawString("客户地址：", printFont, myBrush, 150, 80);
            g.DrawString(CustomerAddress, printFont, myBrush, 120, 90);
            //g.DrawString("缴费年月：", printFont, myBrush, 420, 80);
            g.DrawString(payDate, printFont, myBrush, 320, 70);
            //g.DrawString("起码：", printFont, myBrush, 150, 110);
            g.DrawString(String.Format("{0:#####0.00}", startCode), printFont, myBrush, 120, 155);
            //g.DrawString("止码：", printFont, myBrush, 285, 110);
            g.DrawString(String.Format("{0:#####0.00}", endCode), printFont, myBrush, 120, 135);
            //g.DrawString("电表倍率：", printFont, myBrush, 420, 110);
            g.DrawString(String.Format("{0:#####0.00}", multiple), printFont, myBrush, 120, 170);
            //g.DrawString("抄表电量：", printFont, myBrush, 150, 140);
            g.DrawString(String.Format("{0:#####0.00}", copyPowerValue), printFont, myBrush, 120, 190);
            g.DrawString(String.Format("{0:#####0.00}", transFormerLose), printFont, myBrush, 150, 210);
            //g.DrawString("计费电量：", printFont, myBrush, 285, 140);
            g.DrawString(String.Format("{0:#####0.00}", countPowerValue), printFont, myBrush, 150, 230);
            // g.DrawString("平均电价：", printFont, myBrush, 420, 140);
            // g.DrawString(String.Format("{0:#####0.00}", averagePrice), printFont, myBrush, 500, 140);
            //g.DrawString("基本电费：", printFont, myBrush, 150, 170);
            g.DrawString(String.Format("{0:#####0.00}", baseFee), printFont, myBrush, 550, 95);
            //g.DrawString("城镇附加：", printFont, myBrush, 285, 170);
            g.DrawString(String.Format("{0:#####0.00}", cityAnnex), printFont, myBrush, 550, 230);

            //g.DrawString("应收：", printFont, myBrush, 150, 200);
            g.DrawString(String.Format("{0:#####0.00}", totalMoney), printFont, myBrush, 550, 285);
            //g.DrawString("金额大写：", printFont, myBrush, 285, 200);
            g.DrawString(million, printFont, myBrush, 165, 285);
            g.DrawString(hundredThousand, printFont, myBrush, 195, 285);
            g.DrawString(tenThousand, printFont, myBrush, 225, 285);
            g.DrawString(thousand, printFont, myBrush, 255, 285);
            g.DrawString(hundren, printFont, myBrush, 285, 285);
            g.DrawString(ten, printFont, myBrush, 315, 285);
            g.DrawString(one, printFont, myBrush, 345, 285);
            g.DrawString(fens, printFont, myBrush, 375, 285);
            g.DrawString(tenFen, printFont, myBrush, 405, 285);


            g.DrawString("本次预存：", printFont, myBrush, 80, 265);
            g.DrawString(String.Format("{0:#####0.00}", thisBalance), printFont, myBrush, 150, 265);

            g.DrawString("上次预存：", printFont, myBrush, 215, 265);
            g.DrawString(String.Format("{0:#####0.00}", lastBalance), printFont, myBrush, 285, 265);

            g.DrawString("违约金：", printFont, myBrush, 350, 265);
            g.DrawString(String.Format("{0:#####0.00}", annex), printFont, myBrush, 410, 265);
            g.DrawString("实收金额：", printFont, myBrush, 470, 265);
            g.DrawString(String.Format("{0:#####0.00}", factFee), printFont, myBrush, 550, 265);
            //g.DrawString("打印日期：", printFont, myBrush, 150, 260);
            g.DrawString(printDate, printFont, myBrush, 320, 50);
            //g.DrawString("打印人员：", printFont, myBrush, 420, 260);
            g.DrawString(Constant.LoginUser.UserName, printFont, myBrush, 410, 310);
            //g.DrawString("实收总额", printFont, myBrush, 470, 310);
            //g.DrawString(String.Format("{0:#####0.00}", totalFactFee), printFont, myBrush, 550, 310);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int temp;
            int tempmode;
            temp = (int)(totalMoney / 1000000);
            tempmode = (int)totalMoney % 1000000;
            million = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 100000);
            tempmode = tempmode % 100000;
            hundredThousand = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 10000);
            tempmode = tempmode % 10000;
            tenThousand = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 1000);
            tempmode = tempmode % 1000;
            thousand = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 100);
            tempmode = tempmode % 100;
            hundren = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 10);
            tempmode = tempmode % 10;
            ten = Print.MoneyNumToStr(temp);
            temp = (int)(tempmode / 1);
            tempmode = tempmode % 1;
            one = Print.MoneyNumToStr(temp);

            fens = Print.ConverMoneTofees(totalMoney.ToString(), true);
            tenFen = Print.ConverMoneTofees(totalMoney.ToString(), false);
            //打印
            try
            {
                this.printDocument.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
                printDocument.Print();
                //Print.ShowPrintPreviewDialog(printDocument);
            }
            catch (Exception excep)
            {
                MessageBox.Show("不能正确打印,请检查打印机是否故障！", "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printDocument.PrintController.OnEndPrint(printDocument, new PrintEventArgs());
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 /*       private void cityInvoicePrintYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CustomerNo = "";
            CustomerNo = lbxCustomerList.Text.Split(' ').GetValue(0).ToString();
            init_SelectedCustomerInfo(CustomerNo);
        }

        private void cityInvoicePrintMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CustomerNo = "";
            CustomerNo = lbxCustomerList.Text.Split(' ').GetValue(0).ToString();
            init_SelectedCustomerInfo(CustomerNo);
        }
  */
       
    }
}
