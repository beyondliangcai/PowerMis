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
    public partial class Frm_BankImport : Form
    {
        public Frm_BankImport()
        {
            InitializeComponent();
        }

        private void Frm_BankImport_Load(object sender, EventArgs e)
        {

            fillYear();//填充数据库有记录的年份
            fillMonth();//填充月份
        }

        private void fillYear()
        {
            int currentYear = DateTime.Today.Year;
            string strSql = "Select * From V_Year Order by year0 desc";
            System.Data.DataTable dt = DBUtility.SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bankExportYear.Items.Add(dt.Rows[i]["year0"].ToString());
                //默认选择当前年
                if (dt.Rows[i]["year0"].ToString() == currentYear.ToString())
                {
                    bankExportYear.SelectedIndex = i;
                }
            }


        }

        private void fillMonth()
        {
            //获取当前日期年份
            int currentYear = DateTime.Today.Year;
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

        }

    }
}
