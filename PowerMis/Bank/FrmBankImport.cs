using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerMis.Bank
{
    public partial class FrmBankImport : Form
    {
        private string importPath;//导入数据性质
        private object misValue = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Excel.Application importExcel;
        public FrmBankImport()
        {
            InitializeComponent();
        }

        private void FrmBankImport_Load(object sender, EventArgs e)
        {
            fillYear();//填充数据库有记录的年份
            fillMonth();//填充月份
            fillElectriCharacter();//填充用电性质


        }

        private void fillElectriCharacter()
        {
            ElectriCharacter.Items.Add("城网");
            ElectriCharacter.Items.Add("农网");
            ElectriCharacter.SelectedIndex = 0;
        }
        private void fillYear()
        {
            int currentYear = DateTime.Today.Year;
            string strSql = "Select * From V_Year Order by year0 desc";
            System.Data.DataTable dt = DBUtility.SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bankImportYear.Items.Add(dt.Rows[i]["year0"].ToString());
                //默认选择当前年
                if (dt.Rows[i]["year0"].ToString() == currentYear.ToString())
                {
                    bankImportYear.SelectedIndex = i;
                }
            }


        }

        private void fillMonth()
        {
            //获取当前日期年份
            int currentYear = DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;


            if (bankImportYear.SelectedItem == null)
            {
                MessageBox.Show("请选择导出年份");
            }
            else
            {
                //如果是本年
                if (int.Parse(bankImportYear.SelectedItem.ToString()) == currentYear)
                {
                    for (int i = 0; i < currentMonth; i++)
                    {
                        bankImportMonth.Items.Add((i + 1).ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        bankImportMonth.Items.Add((i + 1).ToString());
                    }
                }
                bankImportMonth.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog importExcelFolderDialog = new FolderBrowserDialog();
            importPath = "";
            if (importExcelFolderDialog.ShowDialog() == DialogResult.OK)
            {
                importPath = importExcelFolderDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("选择文件夹错误");
            }     
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bankImportYear.SelectedItem == null || bankImportMonth.SelectedItem == null)
            {
                MessageBox.Show("请选择导入的年份或月份");
            }
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();//lauch excel application  

            if (ElectriCharacter.SelectedIndex == 0)
            {
                importCityExcel(); //导入城网数据
            }
            else {
                importRuralExcel(); //导入农网数据
            }


        }

        private void importRuralExcel()
        {
           importExcel =new Microsoft.Office.Interop.Excel.Application();
           if (importExcel == null) {
               MessageBox.Show("打开excel 失败");
           }

          
        }

        private void importCityExcel()
        {
            
            try
            {
                importExcel = new Microsoft.Office.Interop.Excel.Application();

                importExcel.Visible = true;
                // 以只读的形式打开EXCEL文件 
                Microsoft.Office.Interop.Excel.Workbook importWorkbook = importExcel.Application.Workbooks.Open(importPath, misValue, true, misValue, misValue, misValue,
           misValue, misValue, misValue, true, misValue, misValue, misValue, misValue, misValue);
                //取得第一个工作薄  
                Microsoft.Office.Interop.Excel.Worksheet importWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)importWorkbook.Worksheets.get_Item(1);
                //取得总记录行数    (包括标题列)  
                int rowsint = importWorksheet.UsedRange.Cells.Rows.Count; //得到行数  
                int columnsint = importWorksheet.UsedRange.Cells.Columns.Count;//得到列数  

            }
            catch (Exception e) {
                MessageBox.Show("打开excel 失败"+e.ToString());
            }
        }

    }
}
