using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using BusinessModel;
using Common;

namespace PowerMis
{
    public partial class Frm_DBConfig : Form
    {
        public Frm_DBConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connectionString = "server ='" + txtServerIP.Text.Trim() + "';uid='" + txtLoginID.Text.Trim() + "'; pwd='" + txtPassword.Text.Trim() + "';database='" + txtDbName.Text.Trim() + "'";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                DataBase database = new DataBase();
                database.ServerIP = txtServerIP.Text.Trim();
                database.LoginID = txtLoginID.Text.Trim();
                database.Password = txtPassword.Text.Trim();
                database.DataBaseName = txtDbName.Text.Trim();
                Constant.DB = database;
                FileReadWrite.UpdateDBXmlFile(database);
                MessageBox.Show("连接成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed && conn != null)
                {
                    conn.Close();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_DBConfig_Load(object sender, EventArgs e)
        {
            DataBase database = new DataBase();
            database = FileReadWrite.ReadDatabaseXml();
            txtServerIP.Text = database.ServerIP;
            txtDbName.Text = database.DataBaseName;
            txtLoginID.Text = database.LoginID;
            txtPassword.Text = database.Password;
        }

    }
}
