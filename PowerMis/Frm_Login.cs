using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessModel;
using BusinessLogic;
using Common;

namespace PowerMis
{
    public partial class Frm_Login : Form
    {
        LoginAction loginAction = null;
        public Frm_Login()
        {
            InitializeComponent();
            loginAction = new LoginAction();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void login_Click(object sender, EventArgs e)
        {
            int isLogin = 0;
            try
            {
                isLogin = loginAction.Login(txtUserName.Text.Trim(), txtUserPassword.Text.Trim());

                if (isLogin == 1) // 成功登陆
                {
                    User user = loginAction.GetUser(txtUserName.Text.Trim());
                    Constant.LoginUser = user;
                    this.Hide();

                    DateTime time = loginAction.GetServerTime();
                    // 打开主界面
                    Frm_Main frmmain = new Frm_Main(time);
                    frmmain.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败！"+ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            DataBase database = new DataBase();
            database = FileReadWrite.ReadDatabaseXml();
            Constant.DB = database;        
        }

        private void DBSetting_Click(object sender, EventArgs e)
        {
            Frm_DBConfig frmDB = new Frm_DBConfig();
            frmDB.ShowDialog();
        }

    }
}
