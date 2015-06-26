using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Common
{
    public class Print
    {
        public PageSettings ShowPageSetupDialog(PrintDocument printDocument)
        {
            //声明返回值的PageSettings
            PageSettings ps = new PageSettings();
            try
            {
                //申明并实例化PageSetupDialog
                PageSetupDialog psDlg = new PageSetupDialog();

                //相关文档及文档页面默认设置
                psDlg.Document = printDocument;
                psDlg.PageSettings = printDocument.DefaultPageSettings;

                //显示对话框
                DialogResult result = psDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ps = psDlg.PageSettings;
                    printDocument.DefaultPageSettings = psDlg.PageSettings;
                }

            }
            catch (InvalidPrinterException e)
            {
                MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ps;
        }

        public PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
        {
            //声明返回值的PrinterSettings
            PrinterSettings ps = new PrinterSettings();
            try
            {
                //申明并实例化PrintDialog
                PrintDialog pDlg = new PrintDialog();
                //可以选定页
                pDlg.AllowSomePages = true;
                //指定打印文档
                pDlg.Document = printDocument;
                //显示对话框
                DialogResult result = pDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //保存打印设置
                    ps = pDlg.PrinterSettings;
                    //打印
                    printDocument.Print();
                }

            }
            catch (InvalidPrinterException e)
            {
                MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！", "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ps;
        }

        public void ShowPrintPreviewDialog(PrintDocument printDocument)
        {
           try
           {
                //申明并实例化PrintPreviewDialog
                PrintPreviewDialog ppDlg = new PrintPreviewDialog();            
                //指定打印文档
                ppDlg.Document = printDocument;          
                //显示对话框
                DialogResult result = ppDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    printDocument.Print();
                }
           }
           catch(InvalidPrinterException e)
           {
                MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！","打印",MessageBoxButtons.OK,MessageBoxIcon.Warning);
           }
           catch(Exception ex)
           {
                MessageBox.Show(ex.Message,"打印",MessageBoxButtons.OK,MessageBoxIcon.Warning);
           }
        }

        public string moneyToCN(string str)
        {

            char[] ch = new char[1];

            ch[0] = '.'; //小数点

            string[] splitstr = null; //定义按小数点分割后的字符串数组

            splitstr = str.Split(ch[0]);//按小数点分割字符串

            if (splitstr.Length == 1) //只有整数部分

                return ConvertData(str) + "圆整";

            else //有小数部分
            {
                string rstr;

                rstr = ConvertData(splitstr[0]) + "圆";//转换整数部分

                rstr += ConvertXiaoShu(splitstr[1]);//转换小数部分

                return rstr;
            }

        }

       
        ///
        /// 转换数字（整数）
        ///
        /// 需要转换的整数数字字符串
        /// 转换成中文大写后的字符串
        public string ConvertData(string str)
        {
            string tmpstr = "";
            string rstr = "";
            int strlen = str.Length;
            if (strlen <= 4)//数字长度小于四位
            {
                rstr = ConvertDigit(str);
            }
           else
            {
                if (strlen <= 8)//数字长度大于四位，小于八位
                {
                    tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                    rstr = ConvertDigit(tmpstr);//转换最后四位数字
                    tmpstr = str.Substring(0, strlen - 4);//截取其余数字
                    //将两次转换的数字加上萬后相连接
                    rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                    rstr = rstr.Replace("零萬", "萬");
                    rstr = rstr.Replace("零零", "零");
                }

                else if (strlen <= 12)//数字长度大于八位，小于十二位
                    {
                        tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                        rstr = ConvertDigit(tmpstr);//转换最后四位数字
                        tmpstr = str.Substring(strlen - 8, 4);//再截取四位数字
                        rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                        tmpstr = str.Substring(0, strlen - 8);
                        rstr = String.Concat(ConvertDigit(tmpstr) + "億", rstr);
                        rstr = rstr.Replace("零億", "億");
                        rstr = rstr.Replace("零萬", "萬");
                        rstr = rstr.Replace("零零", "零");
                        rstr = rstr.Replace("零零", "零");
                        rstr = rstr.Replace("億萬", "億");
                    }
            }
            strlen = rstr.Length;
            if (strlen >= 2)
            {
                switch (rstr.Substring(strlen - 2, 2))
                {
                    case "佰零": rstr = rstr.Substring(0, strlen - 2) + "佰"; break;
                    case "仟零": rstr = rstr.Substring(0, strlen - 2) + "仟"; break;
                    case "萬零": rstr = rstr.Substring(0, strlen - 2) + "萬"; break;
                    case "億零": rstr = rstr.Substring(0, strlen - 2) + "億"; break;
                }
            }
            return rstr;
        }

        ///
        /// 转换数字（小数部分）
        ///
        /// 需要转换的小数部分数字字符串
        /// 转换成中文大写后的字符串
        public string ConvertXiaoShu(string str)
        {
            int strlen = str.Length;
            string rstr;
            if (strlen == 1)
            {
                rstr = ConvertChinese(str) + "角整";
                return rstr;
            }
            else
            {
                string tmpstr = str.Substring(0, 1);
                rstr = ConvertChinese(tmpstr) + "角";
                tmpstr = str.Substring(1, 1);
                rstr += ConvertChinese(tmpstr) + "分";
                rstr = rstr.Replace("零角", "零");
                rstr = rstr.Replace("零零分", "整");
                rstr = rstr.Replace("零分", "");
                return rstr;
            }
        }

        ///
        /// 转换数字
        ///
        /// 转换的字符串（四位以内）
        ///
        public string ConvertDigit(string str)
        {

            int strlen = str.Length;

            string rstr = "";

            switch (strlen)
            {

                case 1: rstr = ConvertChinese(str); break;

                case 2: rstr = Convert2Digit(str); break;

                case 3: rstr = Convert3Digit(str); break;

                case 4: rstr = Convert4Digit(str); break;

            }

            rstr = rstr.Replace("拾零", "拾");

            strlen = rstr.Length;



            return rstr;

        }





        ///

        /// 转换四位数字

        ///

        public string Convert4Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string str3 = str.Substring(2, 1);

            string str4 = str.Substring(3, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "仟";

            rstring += ConvertChinese(str2) + "佰";

            rstring += ConvertChinese(str3) + "拾";

            rstring += ConvertChinese(str4);

            rstring = rstring.Replace("零仟", "零");

            rstring = rstring.Replace("零佰", "零");

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        ///

        /// 转换三位数字

        ///

        public string Convert3Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string str3 = str.Substring(2, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "佰";

            rstring += ConvertChinese(str2) + "拾";

            rstring += ConvertChinese(str3);

            rstring = rstring.Replace("零佰", "零");

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        ///

        /// 转换二位数字

        ///

        public string Convert2Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "拾";

            rstring += ConvertChinese(str2);

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        ///

        /// 将一位数字转换成中文大写数字

        ///

        public string ConvertChinese(string str)
        {

            //"零壹贰叁肆伍陆柒捌玖拾佰仟萬億圆整角分"

            string cstr = "";

            switch (str)
            {

                case "0": cstr = "零"; break;

                case "1": cstr = "壹"; break;

                case "2": cstr = "贰"; break;

                case "3": cstr = "叁"; break;

                case "4": cstr = "肆"; break;

                case "5": cstr = "伍"; break;

                case "6": cstr = "陆"; break;

                case "7": cstr = "柒"; break;

                case "8": cstr = "捌"; break;

                case "9": cstr = "玖"; break;

            }

            return (cstr);

        }

        public string MoneyNumToStr(int num) 
        {
            string  MoneyNumToStr="";
            switch(num)
            {
               case 0:
                    MoneyNumToStr = "零";
                    break;
               case 1:
                    MoneyNumToStr = "壹";
                    break;
               case 2:
                    MoneyNumToStr = "贰";
                    break;
               case 3:
                    MoneyNumToStr = "叁";
                    break;
               case 4:
                    MoneyNumToStr = "肆";
                    break;
               case 5:
                    MoneyNumToStr = "伍";
                    break;
               case 6:
                    MoneyNumToStr = "陆";
                    break;
               case 7:
                    MoneyNumToStr = "柒";
                    break;
               case 8:
                    MoneyNumToStr = "捌";
                    break;
               case 9:
                    MoneyNumToStr = "玖";
                    break;
               }  
            return MoneyNumToStr;
        }

        public string ConverMoneTofees(string money, Boolean flag)
        {
            string fenMoney = "";
            string[] str = money.Split('.');
            if (str.Length == 2)
            {
                if (flag)
                {
                    fenMoney = MoneyNumToStr(int.Parse(str[1].Substring(0, 1)));
                }
                else
                {
                    if (str[1].Length == 2)
                    {
                        fenMoney = MoneyNumToStr(int.Parse(str[1].Substring(1, 1)));
                    }
                    else { fenMoney = "零"; }
                }
            }
            else {
                fenMoney = "零";
            }
            return fenMoney;
        }
         
    }
}
