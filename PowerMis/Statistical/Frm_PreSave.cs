using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DBUtility;
using Common;

namespace PowerMis.Statistical
{
    public partial class Frm_PreSave : Form
    {
        string CustomerNo = "";
        string CustomerName = "";
        string printDate = "";
        private Print Print = null;
        private PrintDocument printDocument = null;
        private string million, hundredThousand, tenThousand, thousand, hundren, ten, one, fens, tenFen;
        public Frm_PreSave(string customerNo)
        {
            InitializeComponent();
            this.CustomerNo = customerNo;
            Print = new Print();
            printDocument = new PrintDocument();
        }

        private void Frm_PreSave_Load(object sender, EventArgs e)
        {
            string strSql = "";
            DataTable dt = null;
            txtCustomerNo.Text = CustomerNo;
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            strSql = "select AdvanceDeposit,CustomerName from CountFee where CustomerNo='" + CustomerNo + "'"
                               + " and Invoiceflag=1 and CountFeeDate<='" + time + "' order by CountFeeDate desc";
            try
            {
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if(dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["AdvanceDeposit"].ToString() != "")
                    {
                        txtLastBalance.Text = dt.Rows[0]["AdvanceDeposit"].ToString();
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                    }
                   
                }
                else { txtLastBalance.Text = "0.00"; }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + (int.Parse(str[1]) - 1) + "-01";
            if (!Regex.IsMatch(txtThisBalance.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || float.Parse(txtThisBalance.Text) < 0)
            {
                MessageBox.Show("本次预存金额必须是大于0的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strSql =  "select CustomerNo From CountFee where CustomerNo='" + CustomerNo + "' and CountFeeDate='" + time + "' and NegativeInvoiceFlag=0";
            try
            {
                int count = SQLUtl.Query(strSql).Tables["dataSet"].Rows.Count;
                if (count <= 0)
                {
                    MessageBox.Show("该用户上月用电信息未录入！暂时不能预存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            strSql = "select CustomerNo From CountFee where CustomerNo='" + CustomerNo + "' and CountFeeDate='" + time + "' and NegativeInvoiceFlag=0 and InvoiceFlag=1";
            try
            {
                int count1 = SQLUtl.Query(strSql).Tables["dataSet"].Rows.Count;
                if (count1 > 0)
                {
                    MessageBox.Show("该用户已打印过发票！暂时不能预存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            printData();
            
        }

        public void printData()
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + (int.Parse(str[1]) - 1) + "-01";
            string date = str[0] + "-" + str[1] +"-"+ str[2]; 
            printDate = str[0] + "年" + str[1] + "月" + str[2] + "日";
            float factRec = float.Parse(txtThisBalance.Text);
            float preSave = factRec + float.Parse(txtLastBalance.Text);
            txtPreSave.Text = String.Format("{0:#####0.00}", preSave);

            //打印

            int temp;
            int tempmode;
            temp = (int)((preSave) / 1000000);
            tempmode = (int)(preSave) % 1000000;
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

            fens = Print.ConverMoneTofees((preSave).ToString(), true);
            tenFen = Print.ConverMoneTofees((preSave).ToString(), false);

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



            //存数据库
            string strSql = "update CountFee set InvoiceFlag=1,AdvanceDeposit='" + preSave + "',FactRec='" + factRec + "',InvoicePrintMan='" + Constant.LoginUser.UserName + "',InvoicePrintDate='"+date+"' "
                            +" where CustomerNo='"+CustomerNo+"' and CountFeeDate='"+time+"' and NegativeInvoiceFlag=0";
            try
            {
                int count = SQLUtl.ExecuteSql(strSql);
                if (count > 0)
                {
                    MessageBox.Show("预存电费成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("预存电费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //g.DrawString("应收：", printFont, myBrush, 150, 200);
            g.DrawString("预存", printFont, myBrush, 550, 285);
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
            g.DrawString(String.Format("{0:#####0.00}", txtThisBalance.Text), printFont, myBrush, 150, 265);

            g.DrawString("上次预存：", printFont, myBrush, 215, 265);
            g.DrawString(String.Format("{0:#####0.00}", txtLastBalance.Text), printFont, myBrush, 285, 265);

           
            g.DrawString("实收金额：", printFont, myBrush, 470, 265);
            g.DrawString(txtPreSave.Text, printFont, myBrush, 550, 265);
            //g.DrawString("打印日期：", printFont, myBrush, 150, 260);
            g.DrawString(printDate, printFont, myBrush, 320, 50);
            //g.DrawString("打印人员：", printFont, myBrush, 420, 260);
            g.DrawString(Constant.LoginUser.UserName, printFont, myBrush, 410, 310);

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
