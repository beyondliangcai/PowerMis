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
    public partial class Frm_CityInvoice : Form
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

        public Frm_CityInvoice()
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            Print = new Print();
        }

        private void Frm_CityInvoice_Load(object sender, EventArgs e)
        {
            fillBookNo();
            try
            {
                string strSQL = "select CountryAnnex from CountryCityAnnexinfo order by AnnexDate asc";
                DataSet dataSet = SQLUtl.Query(strSQL);
                DataTable dataTable = dataSet.Tables["dataSet"];
                CountryAnnex = float.Parse(dataTable.Rows[0]["CountryAnnex"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Margins margin = new Margins(50, 50, 50, 50);
            this.printDocument.DefaultPageSettings.Margins = margin;
            PaperSize paperSize = new PaperSize("Custum", 800, 367);
            this.printDocument.DefaultPageSettings.PaperSize = paperSize;                  
        }

        private void clearForm()
        {
            txtCustomerNo.Text = "";
            txtPreFee.Text = "";
            txtLastBalance.Text = "";
            txtFactFee.Text = "";
            txtThisBalance.Text = "";
            txtAnnex.Text = "";
            CustomerNopayList.Items.Clear();
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

        private void lbxCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CustomerNo = "";
            CustomerNo = lbxCustomerList.Text.Split(' ').GetValue(0).ToString();                   
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
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            DataTable dt = null;
            string invoiceType = "";
            try 
            {
                strSql = "select AdvanceDeposit from CountFee where CustomerNo='" + CustomerNo + "'"
                             + " and Invoiceflag=1 and CountFeeDate<='" + time + "' order by CountFeeDate desc";
                DataTable dt2 = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt2.Rows.Count > 0 && dt2.Rows[0]["AdvanceDeposit"].ToString() != "")
                {
                    lastBalance = float.Parse(dt2.Rows[0]["AdvanceDeposit"].ToString());
                }

                strSql = "select CountfeeAmount,TotalMoney,CountFeeDate,InvoiceType from V_CityInvoice "
                        + "where CustomerNo ='" + CustomerNo + "' and CountFeeDate <= '" + time + "'  order by Countfeedate Desc";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];            
                if (dt.Rows.Count > 0)
                {
                    nopayDate = new string[dt.Rows.Count];
                    nopayFee = new float[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        invoiceType = dt.Rows[i]["InvoiceType"].ToString();
                        nopayDate[i] = dt.Rows[i]["CountFeeDate"].ToString();
                        string date = nopayDate[i].Split('-')[0] + "年" + nopayDate[i].Split('-')[1] + "月";
                        float totalMoney = float.Parse(dt.Rows[i]["TotalMoney"].ToString());
                        if (invoiceType.Equals("增值税发票"))
                        { 
                            totalMoney = float.Parse(dt.Rows[i]["CountfeeAmount"].ToString()) * CountryAnnex;
                        }
                        string totalFee = String.Format("{0:#####0.00}", totalMoney);
                        string msg = date + "电费未交！  金额：" + totalFee + "元";
                        CustomerNopayList.Items.Add(msg);
                        CustomerNopayList.SetItemChecked(i, true);
                        nopayFee[i] = totalMoney;

                        if (invoiceType.Equals("增值税发票"))
                        {
                            preFee = preFee + float.Parse(dt.Rows[i]["CountfeeAmount"].ToString()) * CountryAnnex;
                        }
                        else
                        { preFee = preFee + float.Parse(dt.Rows[i]["TotalMoney"].ToString()); }
                    }

                }

                preFee = preFee - lastBalance;      //应交电费=本月电费-上期预存
                txtCustomerNo.Text = CustomerNo;
                txtPreFee.Text = String.Format("{0:#####0.00}", preFee);
                txtLastBalance.Text = String.Format("{0:#####0.00}", lastBalance);
                txtFactFee.Text = String.Format("{0:#####0.00}", preFee);
                txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance);
                txtAnnex.Text = String.Format("{0:#####0.00}", annex);
                if (preFee < 0)
                {
                    preFee = 0 - preFee;
                    txtPreFee.Text = "0.00";
                    txtFactFee.Text = "0.00";
                    txtThisBalance.Text = String.Format("{0:#####0.00}", preFee);
                } 
                          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"dgfdf");
            }
        }

        private void txtCustomerNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxBookNo.Text = txtCustomerNo.Text.Substring(0, 5);
                int i = lbxCustomerList.FindString(txtCustomerNo.Text);
                lbxCustomerList.SelectedIndex = i;
                lbxCustomerList.TopIndex = i;

            }
        }

        private void txtFactFee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CustomerNopayList.CheckedItems.Count == 0)
                {
                    MessageBox.Show("请选择缴费月份！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!CustomerNopayList.GetItemChecked(CustomerNopayList.Items.Count - 1))
                {
                    MessageBox.Show("必须先交清较低月份的电费才能交较高月份的电费！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!Regex.IsMatch(txtFactFee.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || float.Parse(txtFactFee.Text) < 0)
                {
                    MessageBox.Show("电费必须是大于0的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (float.Parse(txtFactFee.Text) < float.Parse(txtPreFee.Text))
                {
                    MessageBox.Show("实收金额不能小于应收金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                thisBalance = float.Parse(txtFactFee.Text) - float.Parse(txtPreFee.Text) + float.Parse(txtThisBalance.Text);
                txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance);
                printData();
            }
        }
       
        private void CustomerNopayList_SelectedIndexChanged(object sender, EventArgs e)
        {
            preFee = 0;
            thisBalance = 0;
            annex = 0;
            for (int i = 0; i < CustomerNopayList.Items.Count; i++)
            {
                if (CustomerNopayList.GetItemChecked(i))
                {
                    preFee = preFee + nopayFee[i];
                }
            }
            preFee = preFee - lastBalance;
            txtPreFee.Text = String.Format("{0:#####0.00}", preFee);
            txtFactFee.Text = String.Format("{0:#####0.00}", preFee);
            txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance);
            txtAnnex.Text = String.Format("{0:#####0.00}", annex);
            if (preFee < 0)
            {
                preFee = 0 - preFee;
                txtPreFee.Text = "0.00";
                txtFactFee.Text = "0.00";
                txtThisBalance.Text = String.Format("{0:#####0.00}", preFee);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
            if(CustomerNopayList.CheckedItems.Count == 0)
            {
               MessageBox.Show("请选择缴费月份！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                //preSave();
            }
            if (!CustomerNopayList.GetItemChecked(CustomerNopayList.Items.Count - 1))
            {
                MessageBox.Show("必须先交清较低月份的电费才能交较高月份的电费！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(txtFactFee.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || float.Parse(txtFactFee.Text) < 0)
            {
                MessageBox.Show("电费必须是大于0的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (float.Parse(txtFactFee.Text) < float.Parse(txtPreFee.Text))
            {
                MessageBox.Show("实收金额不能小于应收金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            thisBalance = float.Parse(txtFactFee.Text) - float.Parse(txtPreFee.Text) + float.Parse(txtThisBalance.Text);
            txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance);
            printData();
        }

        public void printData()
        {
            
            int temp;
            int tempmode;
            preFee = 0;
            factFee = 0;
            lastBalance = 0;
            thisBalance = 0;
            annex = 0;
            CustomerNo = txtCustomerNo.Text.Trim();
            int printCount = CustomerNopayList.CheckedItems.Count;
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";

            string[] countFeeDate = null ;
            //int j = 0;
            string strSql = "";
            DataTable dt = null;
            for (int i = CustomerNopayList.Items.Count - 1; i >= 0; i--)
            {
                if (CustomerNopayList.GetItemChecked(i))
                {
                    countFeeDate = nopayDate[i].Split('-') ;
                    string time = countFeeDate[0] + "-" + countFeeDate[1] + "-01";
                    try
                    {
                        strSql = "select CustomerName,CustomerAddress,StartCode,MonthCopyAmount,CountFeeAmount,AveragePowerPrice,CityAnnex,"
                                + "TotalMoney,BaseFeeMoney,AbnormityAmount,LastMonthStart,InvoiceType,AmmeterMulti,AmmeterValue,TransFormerLose,CountFeeDate from V_CityInvoice "
                                + " where CustomerNo ='" + CustomerNo + "' and CountFeeDate = '" + time + "'  order by Countfeedate Desc";
                        dt = SQLUtl.Query(strSql).Tables["dataSet"];
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        CustomerAddress = dt.Rows[0]["CustomerAddress"].ToString();
                        startCode = float.Parse(dt.Rows[0]["StartCode"].ToString());
                        if (!dt.Rows[0]["AbnormityAmount"].ToString().Equals("0"))
                        {
                            startCode = float.Parse(dt.Rows[0]["LastMonthStart"].ToString());
                        }
                        endCode = float.Parse(dt.Rows[0]["AmmeterValue"].ToString());
                        multiple = float.Parse(dt.Rows[0]["AmmeterMulti"].ToString());
                        copyPowerValue = float.Parse(dt.Rows[0]["MonthCopyAmount"].ToString());
                        countPowerValue = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString());
                        transFormerLose = float.Parse(dt.Rows[0]["TransFormerLose"].ToString());
                       // averagePrice = float.Parse(dt.Rows[0]["AveragePowerPrice"].ToString());
                        baseFee = float.Parse(dt.Rows[0]["BaseFeeMoney"].ToString());
                        cityAnnex = float.Parse(dt.Rows[0]["CityAnnex"].ToString());
                        totalMoney = float.Parse(dt.Rows[0]["TotalMoney"].ToString());
                        if ("增值税发票".Equals(dt.Rows[0]["InvoiceType"].ToString()))
                        {
                            //string strSql1 = "SELECT TotalMoney FROM CountFee WHERE CustomerNo='" + CustomerNo + "' AND CountFeeDate='" + time + "'";
                            totalMoney = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString()) * CountryAnnex;
                        }
                        payDate = countFeeDate[0] + "年" + countFeeDate[1] + "月";
                        /* 
                        lastBalance = float.Parse(txtLastBalance.Text);
                        thisBalance = lastBalance;
                        preFee = nopayFee[i];
                        factFee = nopayFee[i];
o                        
                        payDate = countFeeDate[0] + "年" + countFeeDate[1] + "月";
                        j++;
                        if (j == printCount)
                        {
                            preFee = nopayFee[i] - lastBalance;
                            thisBalance = float.Parse(txtThisBalance.Text);
                            factFee = preFee + thisBalance;
                        }
                        */
                        if (i == CustomerNopayList.Items.Count - 1)
                        {
                            lastBalance = float.Parse(txtLastBalance.Text);
                            factFee = float.Parse(txtFactFee.Text);
                            thisBalance = factFee + lastBalance - nopayFee[i];
                        }
                        else
                        {
                            lastBalance = thisBalance;
                            factFee = 0;
                            thisBalance = lastBalance - nopayFee[i];
                        }
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
                        //MessageBox.Show(million + hundredThousand+ tenThousand+ thousand+ hundren+ten+ one+ fens+ tenFen);

                        totalFactFee = float.Parse(txtFactFee.Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString()+"111");
                        return;
                    }
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
                    
                    //更改发票标志
                   
                    try
                    {
                        string fac = String.Format("{0:#####0.00}", factFee);
                        string advance = String.Format("{0:#####0.00}", thisBalance);
                        strSql = "UPDATE CountFee SET InvoiceFlag=1,FactRec='" + fac + "',AdvanceDeposit='" + advance + "',InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate= left(GETDATE(),10) "
                        + " WHERE CustomerNo ='" + CustomerNo + "' and CountFeeDate ='" + time + "' and negativeinvoiceflag=0";
                        int count = SQLUtl.ExecuteSql(strSql);
                        if (count == 0)
                        {
                            MessageBox.Show("更改" + CustomerNo + "发票标志失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    
                   
                }
                
            }
            init_SelectedCustomerInfo(CustomerNo);
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
            g.DrawString(String.Format("{0:#####0.00}",startCode), printFont, myBrush, 120, 155);
            //g.DrawString("止码：", printFont, myBrush, 285, 110);
            g.DrawString(String.Format("{0:#####0.00}",endCode), printFont, myBrush, 120, 135);
            //g.DrawString("电表倍率：", printFont, myBrush, 420, 110);
            g.DrawString(String.Format("{0:#####0.00}",multiple), printFont, myBrush, 120, 170);
            //g.DrawString("抄表电量：", printFont, myBrush, 150, 140);
            g.DrawString(String.Format("{0:#####0.00}",copyPowerValue), printFont, myBrush, 120, 190);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPreSave_Click(object sender, EventArgs e)
        {
            if (lbxCustomerList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个用户","预存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Frm_PreSave frmPreSave = new Frm_PreSave(txtCustomerNo.Text);
                frmPreSave.ShowDialog();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (lbxCustomerList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个用户", "增加预存失败",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Frm_AddPreSave frmAddPreSave = new Frm_AddPreSave(txtCustomerNo.Text);
                frmAddPreSave.Show();
            }
        }
        private void tsbPrinterSet_Click(object sender, EventArgs e)
        {
            Print.ShowPrintSetupDialog(printDocument);
        }

        private void tsbPageSet_Click(object sender, EventArgs e)
        {
            Print.ShowPageSetupDialog(printDocument);
        }

        private void tsbPrintView_Click(object sender, EventArgs e)
        {
            this.printDocument.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            Print.ShowPrintPreviewDialog(printDocument);
        }    
        
        private void preSave()
        {
            int temp;
            int tempmode;
            preFee = 0;
            factFee = 0;
            lastBalance = 0;
            thisBalance = 0;
            annex = 0;
            CustomerNo = txtCustomerNo.Text.Trim();
            //int printCount = CustomerNopayList.CheckedItems.Count;
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";
            int month = int.Parse(str[1].ToString()) - 1;
            string time2 = str[0] + "-" + month + "-01";
            //string[] countFeeDate = null;
            //int j = 0;
            string strSql = "";
            DataTable dt = null;
           
                    //countFeeDate = nopayDate[i].Split('-');
                    string time = str[0] + "-" + str[1] + "-01";
                    try
                    {
                        strSql = "select CustomerName,CustomerAddress,StartCode,MonthCopyAmount,CountFeeAmount,AveragePowerPrice,CityAnnex,"
                                + "TotalMoney,BaseFeeMoney,AbnormityAmount,LastMonthStart,InvoiceType,AmmeterMulti,EndCode,TransFormerLose,CountFeeDate from countfee "
                                + " where CustomerNo ='" + CustomerNo + "' and CountFeeDate <= '" + time + "'  order by Countfeedate Desc";
                        dt = SQLUtl.Query(strSql).Tables["dataSet"];
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        CustomerAddress = dt.Rows[0]["CustomerAddress"].ToString();
                        startCode  = float.Parse(dt.Rows[0]["EndCode"].ToString());
                        multiple = float.Parse(dt.Rows[0]["AmmeterMulti"].ToString());
                        //copyPowerValue = float.Parse(dt.Rows[0]["MonthCopyAmount"].ToString());
                        //countPowerValue = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString());
                        //transFormerLose = float.Parse(dt.Rows[0]["TransFormerLose"].ToString());
                        // averagePrice = float.Parse(dt.Rows[0]["AveragePowerPrice"].ToString());
                        //baseFee = float.Parse(dt.Rows[0]["BaseFeeMoney"].ToString());
                        //cityAnnex = float.Parse(dt.Rows[0]["CityAnnex"].ToString());
                        //totalMoney = float.Parse(dt.Rows[0]["TotalMoney"].ToString());
                        lastBalance = float.Parse(txtLastBalance.Text);
                        
                        //preFee = nopayFee[i];
                        //factFee = nopayFee[i];

                        //payDate = countFeeDate[0] + "年" + countFeeDate[1] + "月";
                        //j++;
                       
                           // preFee = nopayFee[i] - lastBalance;
                            //thisBalance = float.Parse(txtThisBalance.Text);
                            factFee = float.Parse(txtFactFee.Text.ToString());
                            thisBalance = lastBalance + factFee;
                        
                        // powerFeeCN = Print.moneyToCN(factFee.ToString());
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
                        //MessageBox.Show(million + hundredThousand+ tenThousand+ thousand+ hundren+ten+ one+ fens+ tenFen);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString() + "111");
                        return;
                    }
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

                    //更改发票标志

                    try
                    {
                        string fac = String.Format("{0:#####0.00}", factFee);
                        string advance = String.Format("{0:#####0.00}", thisBalance);
                        strSql = "UPDATE CountFee SET FactRec='" + fac + "',AdvanceDeposit='" + advance + "',InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate= left(GETDATE(),10) "
                        + " WHERE CustomerNo ='" + CustomerNo + "' and CountFeeDate ='" + time2 + "'";
                        int count = SQLUtl.ExecuteSql(strSql);
                        if (count == 0)
                        {
                            MessageBox.Show("预存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
            init_SelectedCustomerInfo(CustomerNo);
        }


        //缴费但不打印发票
        private void btconfirm_Click(object sender, EventArgs e)
        {

            if (CustomerNopayList.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择缴费月份！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                //preSave();
            }
            if (!CustomerNopayList.GetItemChecked(CustomerNopayList.Items.Count - 1))
            {
                MessageBox.Show("必须先交清较低月份的电费才能交较高月份的电费！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(txtFactFee.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || float.Parse(txtFactFee.Text) < 0)
            {
                MessageBox.Show("电费必须是大于0的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (float.Parse(txtFactFee.Text) < float.Parse(txtPreFee.Text))
            {
                MessageBox.Show("实收金额不能小于应收金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            thisBalance = float.Parse(txtFactFee.Text) - float.Parse(txtPreFee.Text) + float.Parse(txtThisBalance.Text);
            txtThisBalance.Text = String.Format("{0:#####0.00}", thisBalance);

            int temp;
            int tempmode;
            preFee = 0;
            factFee = 0;
            lastBalance = 0;
            thisBalance = 0;
            annex = 0;
            CustomerNo = txtCustomerNo.Text.Trim();
            int printCount = CustomerNopayList.CheckedItems.Count;
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";
            string[] countFeeDate = null;
            //int j = 0;
            string strSql = "";
            DataTable dt = null;
            for (int i = CustomerNopayList.Items.Count - 1; i >= 0; i--)
            {
                if (CustomerNopayList.GetItemChecked(i))
                {
                    countFeeDate = nopayDate[i].Split('-');
                    string time = countFeeDate[0] + "-" + countFeeDate[1] + "-01";
                    try
                    {
                        strSql = "select CustomerName,CustomerAddress,StartCode,MonthCopyAmount,CountFeeAmount,AveragePowerPrice,CityAnnex,"
                                + "TotalMoney,BaseFeeMoney,AbnormityAmount,LastMonthStart,InvoiceType,AmmeterMulti,AmmeterValue,TransFormerLose,CountFeeDate from V_CityInvoice "
                                + " where CustomerNo ='" + CustomerNo + "' and CountFeeDate = '" + time + "'  order by Countfeedate Desc";
                        dt = SQLUtl.Query(strSql).Tables["dataSet"];
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        CustomerAddress = dt.Rows[0]["CustomerAddress"].ToString();
                        startCode = float.Parse(dt.Rows[0]["StartCode"].ToString());
                        if (!dt.Rows[0]["AbnormityAmount"].ToString().Equals("0"))
                        {
                            startCode = float.Parse(dt.Rows[0]["LastMonthStart"].ToString());
                        }
                        endCode = float.Parse(dt.Rows[0]["AmmeterValue"].ToString());
                        multiple = float.Parse(dt.Rows[0]["AmmeterMulti"].ToString());
                        copyPowerValue = float.Parse(dt.Rows[0]["MonthCopyAmount"].ToString());
                        countPowerValue = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString());
                        transFormerLose = float.Parse(dt.Rows[0]["TransFormerLose"].ToString());
                        // averagePrice = float.Parse(dt.Rows[0]["AveragePowerPrice"].ToString());
                        baseFee = float.Parse(dt.Rows[0]["BaseFeeMoney"].ToString());
                        cityAnnex = float.Parse(dt.Rows[0]["CityAnnex"].ToString());
                        totalMoney = float.Parse(dt.Rows[0]["TotalMoney"].ToString());
                        if ("增值税发票".Equals(dt.Rows[0]["InvoiceType"].ToString()))
                        {
                            //string strSql1 = "SELECT TotalMoney FROM CountFee WHERE CustomerNo='" + CustomerNo + "' AND CountFeeDate='" + time + "'";
                            totalMoney = float.Parse(dt.Rows[0]["CountFeeAmount"].ToString()) * CountryAnnex;
                        }
                        payDate = countFeeDate[0] + "年" + countFeeDate[1] + "月";
                        /*
                        lastBalance = float.Parse(txtLastBalance.Text);
                        thisBalance = lastBalance;
                        preFee = nopayFee[i];
                        factFee = nopayFee[i];
                        
                        payDate = countFeeDate[0] + "年" + countFeeDate[1] + "月";
                        j++;
                        if (j == printCount)
                        {
                            preFee = nopayFee[i] - lastBalance;
                            thisBalance = float.Parse(txtThisBalance.Text);
                            factFee = preFee + thisBalance;
                        }
                        */
                        if (i == CustomerNopayList.Items.Count - 1)
                        {
                            lastBalance = float.Parse(txtLastBalance.Text);
                            factFee = float.Parse(txtFactFee.Text);
                            thisBalance = factFee + lastBalance - nopayFee[i];
                        }
                        else
                        {
                            lastBalance = thisBalance;
                            factFee = 0;
                            thisBalance = lastBalance - nopayFee[i];
                        }
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
                        //MessageBox.Show(million + hundredThousand+ tenThousand+ thousand+ hundren+ten+ one+ fens+ tenFen);

                        totalFactFee = float.Parse(txtFactFee.Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString() + "111");
                        return;
                    }
                  
                    //更改发票标志
                    try
                    {
                        string fac = String.Format("{0:#####0.00}", factFee);
                        string advance = String.Format("{0:#####0.00}", thisBalance);
                        strSql = "UPDATE CountFee SET InvoiceFlag=1,FactRec='" + fac + "',AdvanceDeposit='" + advance + "',InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate= left(GETDATE(),10) "
                        + " WHERE CustomerNo ='" + CustomerNo + "' and CountFeeDate ='" + time + "' and negativeinvoiceflag=0";
                        int count = SQLUtl.ExecuteSql(strSql);
                        if (count == 0)
                        {
                            MessageBox.Show("更改" + CustomerNo + "发票标志失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }



                }

            }

            MessageBox.Show("该用户已成功缴纳费用！");
            init_SelectedCustomerInfo(CustomerNo);
          

      
        }
    }
}
