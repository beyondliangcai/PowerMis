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

namespace PowerMis.Statistical
{
    public partial class Frm_NegativeSearch : Form
    {
        public Frm_NegativeSearch()
        {
            InitializeComponent();
        }

        private void Frm_NegativeSearch_Load(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "SELECT * FROM NegativeInvoice";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                dgvInfo.DataSource = dataTable;

                dgvInfo.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            string customerNo = txtCustomerNo.Text;
            string printDate = txtPrintDate.Text;
            try
            {
                string strSQL = "";
                if (customerNo != "" && printDate != "")
                {
                    strSQL = "SELECT * FROM NegativeInvoice where CustomerNo = '" + customerNo + "' and PrintDate = '"+printDate+"' ";
                }
                else if (customerNo != "" && printDate == "")
                {
                    strSQL = "SELECT * FROM NegativeInvoice where CustomerNo = '" + customerNo + "' ";
                }
                else if (customerNo == "" && printDate != "")
                {
                    strSQL = "SELECT * FROM NegativeInvoice where  PrintDate = '" + printDate + "' ";
                }
                else if (customerNo == "" && printDate == "")
                {
                    MessageBox.Show("请输入查询条件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                dgvInfo.DataSource = dataTable;

                dgvInfo.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
