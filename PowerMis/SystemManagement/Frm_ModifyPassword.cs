using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DBUtility;

namespace PowerMis.SystemManagement
{
    public partial class Frm_ModifyPassword : Form
    {
        public Frm_ModifyPassword()
        {
            InitializeComponent();
        }

        private void Frm_ModifyPassword_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = Constant.LoginUser.UserNo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from System where UserNo='" + txtUserNo.Text + "' and Password='" + txtUserOldPwd.Text.Trim() + "'";
            DataSet ds = SQLUtl.Query(sqlString);
           
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("用户旧密码输入错误，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserOldPwd.Focus();
                return;
            }
            if (txtUserNewPwd.Text.Trim() != txtUserNewPwd2.Text.Trim())
            {
                MessageBox.Show("两次新密码输入不一致，请重新输入！");
                txtUserNewPwd.Focus();
                return;
            }
            try
            {
                String strSql = "update System set Password='" + txtUserNewPwd.Text.Trim() + "' where UserNo='" + txtUserNo.Text.Trim() + "'";
                int count = SQLUtl.ExecuteSql(strSql);
                if (count != 0)
                {

                    MessageBox.Show("密码修改成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
        }
    }
}
