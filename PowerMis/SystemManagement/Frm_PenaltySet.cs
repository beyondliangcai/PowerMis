using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;

namespace PowerMis.SystemManagement
{
    public partial class Frm_PenaltySet : Form
    {
        public Frm_PenaltySet()
        {
            InitializeComponent();
        }

        private void Frm_PenaltySet_Load(object sender, EventArgs e)
        {
            string strSql = "Select * From PenaltySet";
            DataTable dt = SQLUtl.Query(strSql).Tables["dataSet"];
            if (dt.Rows.Count > 0)
            {
                txtPercent1.Text = float.Parse(dt.Rows[0]["Percent1"].ToString()).ToString();
                txtPercent2.Text = float.Parse(dt.Rows[0]["Percent2"].ToString()).ToString();
                txtPercent3.Text = float.Parse(dt.Rows[0]["Percent3"].ToString()).ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string percent1 = txtPercent1.Text;
            string percent2 = txtPercent2.Text;
            string percent3 = txtPercent3.Text;
            try
            {
                string strSql = "Update PenaltySet Set Percent1 = '" + percent1 + "', Percent2 = '" + percent2 + "', Percent3 = '" + percent3 + "'";
                int count = SQLUtl.ExecuteSql(strSql);
                if (count > 0)
                {
                    MessageBox.Show("设置违约金信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("设置违约金信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
