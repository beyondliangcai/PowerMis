using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using DBUtility;

namespace PowerMis.Bank
{
    public partial class Frm_BankExport : Form
    {
        public Frm_BankExport()
        {
            InitializeComponent();
        }

        private void Frm_BankExport_Load(object sender, EventArgs e)
        {

            fillYear();//填充数据库有记录的年份
            fillMonth();//填充月份
        }

      
        private void fillYear()
        {
             int currentYear=DateTime.Today.Year;
            string strSql = "Select * From V_Year Order by year0 desc";
            System.Data.DataTable dt = DBUtility.SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bankExportYear.Items.Add(dt.Rows[i]["year0"].ToString());
                //默认选择当前年
                if (dt.Rows[i]["year0"].ToString()==currentYear.ToString())
                {
                    bankExportYear.SelectedIndex = i;
                }
            }

            
        }

        private void fillMonth()
        {
            //获取当前日期年份
            int currentYear=DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;


            if (bankExportYear.SelectedItem == null)
            {
                MessageBox.Show("请选择导出年份");
            }
            else
            {
                //如果是本年
                if (int.Parse(bankExportYear.SelectedItem.ToString()) == currentYear)
                {
                    for (int i = 0; i < currentMonth; i++)
                    {
                        bankExportMonth.Items.Add((i + 1).ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        bankExportMonth.Items.Add((i + 1).ToString());
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

           
            if (bankExportYear.SelectedItem == null || bankExportMonth.SelectedItem == null)
            {
                MessageBox.Show("请选择导出的年份或月份");
            }
              FolderBrowserDialog exportExcelFolderDialog = new FolderBrowserDialog();
            string exportPath = "";
            if (exportExcelFolderDialog.ShowDialog() == DialogResult.OK)
            {
                exportPath = exportExcelFolderDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("选择文件夹错误");
            }     
            
             exportRuralData();      //导出农网数据
             exportCityData(int.Parse(bankExportYear.SelectedItem.ToString()), int.Parse(bankExportMonth.SelectedItem.ToString()), exportPath);       //导出城网数据
            
        }

        private void initBankExcel(Worksheet workSheet)
        {

            workSheet.Cells[1, 1] = "用户号";
            workSheet.Cells[1, 2] = "用户名";
            workSheet.Cells[1, 3] = "身份证";
            workSheet.Cells[1, 4] = "银行卡号";
            workSheet.Cells[1, 5] = "联系方式";
            workSheet.Cells[1, 6] = "月份";
            //   excel.Cells[1, 7] = "电表号";
            workSheet.Cells[1, 7] = "初始值";
            workSheet.Cells[1, 8] = "结束值";
            workSheet.Cells[1, 9] = "金额";
            //  cityExcel.Cells[1, 10] = "是否交钱";

        }

        //导出城网数据
        private void exportCityData(int exportYear ,int exportMonth,string exportPath)
        {
            //实例化一个Excel.Application对象  
            string tempSql = "";
            string tempSql2 = "";
            int flag = 0;   //标志变量，判断该用户的欠费时间是否已经生成worksheet
            int tmpPosition=1;
            int tempWorksheetYear;
            int tempWorksheetMonth;

            Worksheet tempCityDataSheet;
            string time = exportYear.ToString() + "-" + exportMonth.ToString() + "-1";
            string filePath = exportPath + "\\城网" + exportYear.ToString() + "年" + exportMonth.ToString() + "月导出数据报表.xls";

            Microsoft.Office.Interop.Excel.Application cityExcel = new Microsoft.Office.Interop.Excel.Application();
            cityExcel.Visible = true;
            object misValue=System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook cityWorkbook = cityExcel.Application.Workbooks.Add(misValue); //创建excel工作簿
                       
            //Worksheet以年月命名,tempCityDataSheet是第一张Worksheet
            tempCityDataSheet = (Worksheet)cityWorkbook.Sheets[1];
            tempCityDataSheet.Name = exportYear.ToString() + "-" + exportMonth.ToString() ;
            cityExcel.Caption = "城网" + exportYear + "年" + exportMonth + "月导出数据报表";           
            initBankExcel(tempCityDataSheet); //初始化城网数据excel
            


            try
            {
                //查找本月没有缴纳费用的签约用户and signflag=1,该用户上个月有欠费信息
                string cityDataSql = "SELECT CustomerInfo.CustomerNo, CustomerInfo.CustomerName, CustomerInfo.identificationCard, CustomerInfo.BankAccount, CustomerInfo.PhoneNum,CONVERT(VARCHAR(12),CountFee.CountFeeDate,23), CountFee.StartCode, CountFee.EndCode, CountFee.AccountRec, CustomerInfo.ElectriCharacterName" +
                                      " FROM CountFee INNER JOIN  CustomerInfo ON CountFee.CustomerNo = CustomerInfo.CustomerNo " +
                                      " WHERE (CustomerInfo.ElectriCharacterName = '城网') AND (InvoiceFlag =0) AND (CountFee.FactRec > 0) AND (YEAR(CountFee.CountFeeDate) = '" + exportYear + "') AND (MONTH(CountFee.CountFeeDate) = '" + exportMonth + "')";
                System.Data.DataTable cityDataDt = DBUtility.SQLUtl.Query(cityDataSql).Tables[0];

                //查找本月没有缴纳费用的签约用户and signflag=1，该用户上个月无欠费信息
                string cityDataSql2 = "SELECT CustomerInfo.CustomerNo, CustomerInfo.CustomerName, CustomerInfo.identificationCard, CustomerInfo.BankAccount, CustomerInfo.PhoneNum,CONVERT(VARCHAR(12),CountFee.CountFeeDate,23), CountFee.StartCode, CountFee.EndCode, CountFee.TotalMoney, CustomerInfo.ElectriCharacterName" +
                                      " FROM CountFee INNER JOIN  CustomerInfo ON CountFee.CustomerNo = CustomerInfo.CustomerNo " +
                                      " WHERE (CustomerInfo.ElectriCharacterName = '城网') AND (InvoiceFlag =0) AND (CountFee.FactRec > 0) AND (YEAR(CountFee.CountFeeDate) = '" + exportYear + "') AND (MONTH(CountFee.CountFeeDate) = '" + exportMonth + "')";
                System.Data.DataTable cityDataDt2 = DBUtility.SQLUtl.Query(cityDataSql2).Tables[0];
                
                int rowNum = cityDataDt.Rows.Count;  //行数
                int columnNum = 9;                   //列数
                int tempRows=2;
                int temp = 0;
                string tempTimeYear;
                string tempTimeMonth;
                Range tempRange;

                //当月没有用户欠费信息
                if(rowNum<=0){
                    MessageBox.Show("当月没有用户欠费信息");
                    this.Close();
                }

                for (int i = 1; i <= rowNum; i++)
                {
                    //添加一张Sheet
                    tempCityDataSheet = (Worksheet)cityWorkbook.Sheets[1];
                    
                    // AND (SignFlag =1)


                    tempSql = "SELECT CustomerInfo.CustomerNo, CustomerInfo.CustomerName, CustomerInfo.identificationCard, CustomerInfo.BankAccount, CustomerInfo.PhoneNum,CONVERT(VARCHAR(12),CountFee.CountFeeDate,23) as CountFeeDate, CountFee.StartCode, CountFee.EndCode, CountFee.TotalMoney, CustomerInfo.ElectriCharacterName" +
                                  " FROM CountFee INNER JOIN  CustomerInfo ON CountFee.CustomerNo = CustomerInfo.CustomerNo " +
                                  " WHERE (CustomerInfo.ElectriCharacterName = '城网') AND (CountFeeAmount <> 0) AND (InvoiceFlag =0) AND (CountFee.FactRec > 0) AND CountFee.CustomerNo ='" + cityDataDt.Rows[i - 1]["CustomerNo"] + "' AND CountFee.CountFeeDate<'" + time + "'" +
                                  " order by CountFee.CountFeeDate desc ";
                    System.Data.DataTable tempDt = DBUtility.SQLUtl.Query(tempSql).Tables[0];

                    tempSql2 = "SELECT CustomerInfo.CustomerNo, CustomerInfo.CustomerName, CustomerInfo.identificationCard, CustomerInfo.BankAccount, CustomerInfo.PhoneNum,CONVERT(VARCHAR(12),CountFee.CountFeeDate,23) as CountFeeDate, CountFee.StartCode, CountFee.EndCode, CountFee.AccountRec, CustomerInfo.ElectriCharacterName" +
                                " FROM CountFee INNER JOIN  CustomerInfo ON CountFee.CustomerNo = CustomerInfo.CustomerNo " +
                                " WHERE (CustomerInfo.ElectriCharacterName = '城网') AND (CountFeeAmount <> 0) AND (InvoiceFlag =0) AND (CountFee.FactRec > 0) AND CountFee.CustomerNo ='" + cityDataDt.Rows[i - 1]["CustomerNo"] + "' AND CountFee.CountFeeDate<'" + time + "'" +
                                " order by CountFee.CountFeeDate desc ";
                    System.Data.DataTable tempDt2 = DBUtility.SQLUtl.Query(tempSql2).Tables[0];

                    //如果有用户上个月没有欠费信息,那么导出的excel的第一张表中的金额=CountFee.AccountRec
                    if (tempDt.Rows.Count == 0)
                    {
                        for (int j = 1; j <= columnNum; j++)
                        {
                            tempCityDataSheet.Cells[i + 1, j] = cityDataDt.Rows[i - 1][j - 1].ToString();
                            tempRange = (Range)tempCityDataSheet.Cells[i + 1, j];
                            // tempRange.NumberFormatLocal = "@";//设置文本格式
                            tempRange.EntireColumn.AutoFit();
                        }
                    }
                    //如果有用户上个月还欠费，那么导出的excel第一张表中的金额=CountFee.CountFeeAmount，还需要导出到另外一张表
                    //数据库数据存在问题，只能每次把worksheet遍历一次，效率极低
                    else {
                                              
                   /*     if (tempDt.Rows.Count>temp)
                      {
                            for (int k = temp; k < tempDt.Rows.Count;k++ )
                           {
                              //添加一张Worksheet
                                
                               tempCityDataSheet = (Worksheet)cityExcel.Application.Worksheets.Add(misValue,misValue,misValue,misValue);
                               cityWorkbook.Sheets.Move(misValue,cityWorkbook.Worksheets[cityWorkbook.Worksheets.Count]);
                               tempTimeYear = tempDt.Rows[k]["CountFeeDate"].ToString().Split('-').GetValue(0).ToString();
                               tempTimeMonth = tempDt.Rows[k]["CountFeeDate"].ToString().Split('-').GetValue(1).ToString();
                               tempCityDataSheet.Name = tempTimeYear+"年"+tempTimeMonth+"月";
                               initBankExcel(tempCityDataSheet);
                             // tempCityDataSheet = (Worksheet)cityWorkbook.Sheets[k + 2];    
                           }
                            temp = tempDt.Rows.Count;
                        }
                    * */


                       //如果有用户上个月还欠费，那么导出的excel表中的金额=CountFee.CountFeeAmount                       
                            for (int j = 1; j <= columnNum; j++)
                            {
                                tempCityDataSheet.Cells[i + 1, j] = cityDataDt2.Rows[i - 1][j - 1].ToString();
                                tempRange = (Range)tempCityDataSheet.Cells[i + 1, j];
                                // tempRange.NumberFormatLocal = "@";//设置文本格式
                                tempRange.EntireColumn.AutoFit();
                            }                        


                             flag = 0;
                      
                            for (int k = 0; k < tempDt.Rows.Count; k++)
                            {
                                //如果是该用户的第一次欠费信息，应该加上上月余额，excel表中金额应该是CountFee.AccountRec
                                if (k == tempDt.Rows.Count - 1)
                                {
                                    tempDt = tempDt2;
                                }
                               
                                    tempTimeYear = tempDt.Rows[k]["CountFeeDate"].ToString().Split('-').GetValue(0).ToString();
                                    tempTimeMonth = tempDt.Rows[k]["CountFeeDate"].ToString().Split('-').GetValue(1).ToString();

                                    foreach (Worksheet tempWorksheet in cityWorkbook.Worksheets)
                                    {
                                        if (tempWorksheet.Name == tempTimeYear + "-" + tempTimeMonth)
                                        {
                                            tempRows = tempWorksheet.UsedRange.Rows.Count + 1;
                                            for (int m = 1; m <= columnNum; m++)
                                            {
                                                tempWorksheet.Cells[tempRows, m] = tempDt.Rows[k][m - 1].ToString();
                                                tempRange = (Range)tempWorksheet.Cells[tempRows, m];
                                                //  tempRange.NumberFormatLocal = "@";//设置文本格式
                                                tempRange.EntireColumn.AutoFit();
                                            }
                                            flag = 1;

                                        }

                                    }

                                    //添加一张Worksheet，根据时间插入到workbook中
                                    if (flag == 0)
                                    {
                                        tmpPosition = 0;


                                        //插入到正确的位置
                                        foreach (Worksheet tempWorksheet in cityWorkbook.Worksheets)
                                        {
                                            tempWorksheetYear = int.Parse(tempWorksheet.Name.ToString().Split('-').GetValue(0).ToString());
                                            tempWorksheetMonth = int.Parse(tempWorksheet.Name.ToString().Split('-').GetValue(1).ToString());

                                            if (tempWorksheetYear < int.Parse(tempTimeYear) || (tempWorksheetMonth < int.Parse(tempTimeMonth) && tempWorksheetYear <= int.Parse(tempTimeYear)))
                                            {
                                                break;       //找到合适的位置，跳出循环                                    
                                            }
                                            tmpPosition++;

                                        }
                                        tempCityDataSheet = (Worksheet)cityExcel.Application.Worksheets.Add(misValue, cityWorkbook.Worksheets[tmpPosition], misValue, misValue);
                                        tempCityDataSheet.Name = tempTimeYear + "-" + tempTimeMonth;
                                        initBankExcel(tempCityDataSheet);
                                        // tempCityDataSheet.Move(cityWorkbook.Worksheets[tmpPosition],misValue );

                                        tempRows = tempCityDataSheet.UsedRange.Rows.Count + 1;
                                        for (int m = 1; m <= columnNum; m++)
                                        {
                                            tempCityDataSheet.Cells[tempRows, m] = tempDt.Rows[k][m - 1].ToString();
                                            tempRange = (Range)tempCityDataSheet.Cells[tempRows, m];
                                            // tempRange.NumberFormatLocal = "@";//设置文本格式
                                            tempRange.EntireColumn.AutoFit();
                                        }


                                    }
                                }                      
                         
                        
                       
                        }

                    }

                    //tempCityDataSheet 
                   // tempCityDataSheet = (Worksheet)cityWorkbook.Sheets[temp];
                cityWorkbook.Save();
                cityWorkbook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12, null, null, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
               
                cityExcel.Quit();
                }
                               
            
            catch (Exception e) {
                MessageBox.Show("导出excel数据失败！" +e.ToString());
            }
        
        }

        private void exportRuralData()
        {
            //throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
