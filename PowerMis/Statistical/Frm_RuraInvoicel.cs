using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using Common;
using BusinessModel;
using DBUtility;

namespace PowerMis.Statistical
{
    public partial class Frm_RuraInvoicel : Form
    {
        private PrintDocument printDocument = null;
        private Common.Print Print = null;
        private Customer customer = null;
        DataTable pdt = null;
        DataTable mydt = null;
        DataTable mydt2 = null;
        int page = 0;
        private string startCode = "";
        private string endCode = "";
        private string powerValue = "";     //电量
        private string multiple = "";
        private string advanceSave = "";    //上期预存
        private string powerFee = "";       //本月电费
        private string powerFeeCN = "";     //金额汉字大写
        private string actFee = "";         //实收
        private string balance = "";        //结余
        private string printDate = "";      //发票打印日期
        private string[] powerPrice;
        private string[] priceRate;
        string[] str ;                      //用来存日历控件值
        string time = "";
        public Frm_RuraInvoicel()
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            Print = new Print();
            customer = new Customer();
            powerPrice = new string[4] { "","","",""};
            priceRate = new string[4] { "","","",""};
        }


        private void Frm_RuraInvoicel_Load(object sender, EventArgs e)
        {
            Margins margin = new Margins(50, 50, 50, 50);
            this.printDocument.DefaultPageSettings.Margins = margin;
            PaperSize paperSize = new PaperSize("Custum", 600, 300);
            this.printDocument.DefaultPageSettings.PaperSize = paperSize;

            //初始化cmbVolumeNo控件
            try 
            {
                string strSql = "select distinct Left(CustomerNo,5) as VolumeNo from CustomerInfo order by VolumeNo";
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbVolumeNo.Items.Add(dt.Rows[i]["VolumeNo"]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            page = 0;
            string VolumeNo = cmbVolumeNo.Text;
            str = dateTimePicker1.Value.ToShortDateString().Split('-');
            time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            string strSql2 = "";
            try
            {
                strSql = "select * from V_CountryInvoice Where  Left(CustomerNo,5) ='" + VolumeNo + "' and CountFeeDate ='" + time + "' Order By CustomerNo";
                pdt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (pdt.Rows.Count > 0)
                {
                    //更改发票标志

                    //如果该册有增值税用户不能修改增值税用户的InvoiceFlag

                    //查询有农网用户的册号，遍历该册如果该册有增值税用户则不修改InvoiceFlag
                    strSql2 = "select * from CountFee where  Left(CustomerNo,5) ='" + VolumeNo + "' and CountFeeDate ='" + time + "'";
                    mydt = SQLUtl.Query(strSql2).Tables["dataSet"];
                    for (int i = 0; i < mydt.Rows.Count; i++)
                    {
                        if ("增值税发票".Equals(mydt.Rows[i]["InvoiceType"].ToString()))
                        {
                        }
                        else {
                           strSql = "UPDATE countfee SET InvoiceFlag=1,InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate=left(GETDATE(),10) "
                           + " WHERE CustomerNo='" + mydt.Rows[i]["CustomerNo"].ToString() + "' and CountFeeDate ='" + time + "' and totalmoney>0 and InvoiceFlag=0";
                           try
                           {
                               SQLUtl.ExecuteSql(strSql);
                           }
                           catch (Exception myex)
                           {
                               MessageBox.Show("更改" + mydt.Rows[i]["CustomerNo"].ToString() + "发票标志失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                        }

                    }
                    
                   
                }
                else
                {
                    MessageBox.Show("没有数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "1111");
                return;
            }
            //打印
            try
            {
                /*
                this.printDocument.Dispose();
                this.printDocument = new PrintDocument();
                Margins margin = new Margins(50, 50, 50, 50);
                this.printDocument.DefaultPageSettings.Margins = margin;
                PaperSize paperSize = new PaperSize("Custum", 600, 300);
                this.printDocument.DefaultPageSettings.PaperSize = paperSize;
                */
                this.printDocument.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
                //Print.ShowPrintPreviewDialog(printDocument);
                printDocument.Print();
                this.printDocument.PrintPage -= new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
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

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if (txtStartNo.Text != "" && txtEndNo.Text == "")
            {
                txtEndNo.Text = txtStartNo.Text;
            }

            page = 0;
            str = dateTimePicker2.Value.ToShortDateString().Split('-');
            time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            try
            {
                strSql = "select * from V_CountryInvoice Where CustomerNo >='" + txtStartNo.Text + "' and CustomerNo<='" + txtEndNo.Text + "' and CountFeeDate ='" + time + "' Order By CustomerNo";
                pdt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (pdt.Rows.Count > 0)
                {
                    //更改发票标志

                    //如果该册有增值税用户不能修改增值税用户的InvoiceFlag

                    //查询有农网用户的册号，遍历该册如果该册有增值税用户则不修改InvoiceFlag

                 string   strSql2 = "select * from CountFee Where CustomerNo >='" + txtStartNo.Text + "' and CustomerNo<='" + txtEndNo.Text + "' and CountFeeDate ='" + time + "' Order By CustomerNo";
                    mydt2 = SQLUtl.Query(strSql2).Tables["dataSet"];
                    for (int i = 0; i < mydt2.Rows.Count; i++)
                    {
                        if ("增值税发票".Equals(mydt2.Rows[i]["InvoiceType"].ToString()))
                        {
                        }
                        else
                        {
                            strSql = "UPDATE countfee SET InvoiceFlag=1,InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate=left(GETDATE(),10) "
                            + " WHERE CustomerNo='" + mydt2.Rows[i]["CustomerNo"].ToString() + "' and CountFeeDate ='" + time + "' and totalmoney>0 and InvoiceFlag=0";
                            try
                            {
                                SQLUtl.ExecuteSql(strSql);
                            }
                            catch (Exception myex)
                            {
                                MessageBox.Show("更改" + mydt2.Rows[i]["CustomerNo"].ToString() + "发票标志失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                    }
             
                }
                else
                {
                    MessageBox.Show("没有数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "1111");
                return;
            }
            //打印
            try
            {              
                this.printDocument.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
                printDocument.Print();
                this.printDocument.PrintPage -= new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            }
            catch (Exception excep)
            {
                MessageBox.Show("不能正确打印,请检查打印机是否故障！", "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printDocument.PrintController.OnEndPrint(printDocument, new PrintEventArgs());
                return;
            } 
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            string CustomerNo = pdt.Rows[page]["CustomerNo"].ToString();
            string strSql1 = "select  AmmeterValue from AmmeterValue where CustomerNo ='" + CustomerNo + "' and CopyValueDate <'" + time + " ' ORDER BY AmmeterValue.CopyValueDate DESC ";
            DataTable dt2 = SQLUtl.Query(strSql1).Tables["dataSet"];
            if (dt2.Rows.Count != 0)
            {
                startCode = dt2.Rows[0]["AmmeterValue"].ToString();
            }

            strSql1 = "select AdvanceDeposit from CountFee where CustomerNo='" + CustomerNo + "'"
                   + " and Invoiceflag=1 and CountFeeDate<'" + time + "' order by CountFeeDate desc";
            DataTable dt3 = SQLUtl.Query(strSql1).Tables["dataSet"];
            if (dt3.Rows.Count != 0 && dt3.Rows[0]["AdvanceDeposit"].ToString() != "")
            {
                float lastBalance = float.Parse(dt3.Rows[0]["AdvanceDeposit"].ToString());
                advanceSave = String.Format("{0:#####0.00}", lastBalance);
            }

            if (!pdt.Rows[page]["AbnormityAmount"].ToString().Equals("0"))
            {
                startCode = pdt.Rows[page]["LastMonthStart"].ToString();
            }
            endCode = pdt.Rows[page]["AmmeterValue"].ToString();
            customer.setCustomerNo(CustomerNo);
            customer.setCustomerName(pdt.Rows[page]["CustomerName"].ToString());
            customer.setCustomerAddress(pdt.Rows[page]["CustomerAddress"].ToString());
            multiple = pdt.Rows[page]["AmmeterMulti"].ToString();
            powerValue = pdt.Rows[page]["CountfeeAmount"].ToString();
            powerFee = String.Format("{0:#####0.00}", Math.Round(float.Parse(pdt.Rows[page]["TotalMoney"].ToString()),2).ToString());
            powerFeeCN = Print.moneyToCN(powerFee);
            actFee = pdt.Rows[page]["FactRec"].ToString();
            float thisBalance = float.Parse(pdt.Rows[page]["AdvanceDeposit"].ToString());
            balance = String.Format("{0:#####0.00}", thisBalance);
            printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";
            if (!pdt.Rows[page]["PowerPrice1"].ToString().Equals("0"))
            {
                powerPrice[0] = String.Format("{0:###0.00####}", float.Parse(pdt.Rows[page]["PowerPrice1"].ToString()));
                priceRate[0] = String.Format("{0:###0.}", float.Parse(pdt.Rows[page]["PriceRate1"].ToString()));
            }
            if (!pdt.Rows[page]["PowerPrice2"].ToString().Equals("0"))
            {
                powerPrice[1] = String.Format("{0:###0.00####}", float.Parse(pdt.Rows[page]["PowerPrice2"].ToString()));
                priceRate[1] = String.Format("{0:###0.}", float.Parse(pdt.Rows[page]["PriceRate2"].ToString()));
            }
            if (!pdt.Rows[page]["PowerPrice3"].ToString().Equals("0"))
            {
                powerPrice[2] = String.Format("{0:###0.00####}", float.Parse(pdt.Rows[page]["PowerPrice3"].ToString()));
                priceRate[2] = String.Format("{0:###0.}", float.Parse(pdt.Rows[page]["PriceRate3"].ToString()));
            }
            if (!pdt.Rows[page]["PowerPrice4"].ToString().Equals("0"))
            {
                powerPrice[3] = String.Format("{0:###0.00####}", float.Parse(pdt.Rows[page]["PowerPrice4"].ToString()));
                priceRate[3] = String.Format("{0:###0.}", float.Parse(pdt.Rows[page]["PriceRate4"].ToString()));
            }


            Font printFont = new Font(new FontFamily("宋体"), 10); //打印字体  
            SolidBrush myBrush = new SolidBrush(Color.Black);//刷子
            Graphics g = e.Graphics; //获得绘图对象
            //g.DrawString("编号：", printFont, myBrush, 50, 50);
            g.DrawString(customer.getCustomerNo(), printFont, myBrush, 120, 115);
            //g.DrawString("姓名：", printFont, myBrush, 50, 90);
            g.DrawString(customer.getCustomerName(), printFont, myBrush, 120, 75);
            //g.DrawString("地址：", printFont, myBrush, 50, 130);
            g.DrawString(customer.getCustomerAddress(), printFont, myBrush, 120, 150);
            //g.DrawString("本月电量：", printFont, myBrush, 50, 170);
            g.DrawString(powerValue, printFont, myBrush, 130, 185);
            //g.DrawString("本月电费：", printFont, myBrush, 50, 210);
            g.DrawString(powerFee + "  元", printFont, myBrush, 130, 220);
            g.DrawString("实收：", printFont, myBrush, 70, 240);
            g.DrawString(actFee + "  元", printFont, myBrush, 130, 240);
            //g.DrawString("打印日期：", printFont, myBrush, 50, 290);
            g.DrawString(printDate, printFont, myBrush, 100, 255);

            g.DrawString(customer.getCustomerNo(), printFont, myBrush, 380, 70);
            g.DrawString(customer.getCustomerName(), printFont, myBrush, 260, 70);
            //g.DrawString("地址：", printFont, myBrush, 300, 75);
            g.DrawString(customer.getCustomerAddress(), printFont, myBrush, 250, 90);
            g.DrawString("起码", printFont, myBrush, 260, 115);
            g.DrawString(startCode, printFont, myBrush, 300, 115);
            g.DrawString("止码", printFont, myBrush, 350, 115);
            g.DrawString(endCode, printFont, myBrush, 400, 115);
            g.DrawString("电量", printFont, myBrush, 260, 130);
            g.DrawString(powerValue, printFont, myBrush, 300, 130);
            g.DrawString("倍率", printFont, myBrush, 350, 130);
            g.DrawString(multiple, printFont, myBrush, 400, 130);
            g.DrawString("电价", printFont, myBrush, 260, 150);
            g.DrawString(powerPrice[0], printFont, myBrush, 300, 150);
            g.DrawString(powerPrice[1], printFont, myBrush, 350, 150);
            g.DrawString(powerPrice[2], printFont, myBrush, 400, 150);
            g.DrawString(powerPrice[3] + "    元", printFont, myBrush, 450, 150);
            g.DrawString("电价比", printFont, myBrush, 260, 165);
            g.DrawString(priceRate[0], printFont, myBrush, 300, 165);
            g.DrawString(priceRate[1], printFont, myBrush, 350, 165);
            g.DrawString(priceRate[2], printFont, myBrush, 400, 165);
            g.DrawString(priceRate[3] + "     %", printFont, myBrush, 450, 165);
            //g.DrawString("本月电费：", printFont, myBrush, 300, 200);
            g.DrawString(powerFee, printFont, myBrush, 310, 210);

            g.DrawString("实收", printFont, myBrush, 360, 210);
            g.DrawString(actFee + "  元", printFont, myBrush, 400, 210);
            //g.DrawString("金额大写：", printFont, myBrush, 310, 230);
            g.DrawString(powerFeeCN, printFont, myBrush, 300, 238);

            g.DrawString("上期预存", printFont, myBrush, 250, 180);
            g.DrawString(advanceSave, printFont, myBrush, 310, 180);
            g.DrawString("余额", printFont, myBrush, 350, 180);
            g.DrawString(balance, printFont, myBrush, 400, 180);
            //g.DrawString("打印日期：", printFont, myBrush, 300, 300);
            g.DrawString(printDate, printFont, myBrush, 230, 45);
            g.DrawString(Constant.LoginUser.UserName, printFont, myBrush, 250, 255);

            page++;
            if (page < pdt.Rows.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                return;
            }
        }
    }
}
