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

namespace PowerMis.SystemManagement
{
    public partial class Frm_User : Form
    {
        public Frm_User()
        {
            InitializeComponent();
        }

        int AddOrEdit;//标记是添加或修改了记录
 

        private void ClearText()//清除文本框内容
        {
            txtUserNo.Clear();
            txtUserName.Clear();
            txtUserPwd.Clear();

        }

        private void LockedTextBox()//锁定文本框
        {
            txtUserNo.Enabled = false;
            txtUserName.Enabled = false;
            txtUserPwd.Enabled = false;
            cbxPermission.Enabled = false;
        }

        private void UnLockedTextBox()//解除锁定
        {
            txtUserNo.Enabled = true;
            txtUserName.Enabled = true;
            txtUserPwd.Enabled = true;
            cbxPermission.Enabled = true;
        }


        private void FillDataGridView()//填充表格数据
        {
            try
            {
                string strSQL = "SELECT UserNo,UserName,Password,Permission FROM System  ORDER BY UserNo";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                dgvUser.DataSource = dataTable;
                
                dgvUser.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
            tsbCancel.Enabled = true;
            AddOrEdit = 0;
            ClearText();
            UnLockedTextBox();
            txtUserNo.Focus();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
            tsbCancel.Enabled = true;
            AddOrEdit = 1;
            UnLockedTextBox();
            txtUserNo.Focus();
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该用户信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    String strSql = "DELETE FROM System WHERE UserNo = '"+Convert.ToString(dgvUser[0, dgvUser.CurrentCell.RowIndex].Value).Trim()+"'";
                    int count = SQLUtl.ExecuteSql(strSql);
                    if (count != 0)
                    {
                        MessageBox.Show("删除数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearText();
                        FillDataGridView();
                        LockedTextBox();
                        tsbSave.Enabled = false;
                        tsbCancel.Enabled = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            
            
            if (txtUserNo.Text == "")
            {
                MessageBox.Show("用户账号不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUserPwd.Text == "")
            {
                MessageBox.Show("用户密码不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbxPermission.Text == "")
            {
                MessageBox.Show("用户权限不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (AddOrEdit == 0)
            {
                //添加记录后的保存
                try
                {

                    //判断用户名称是否已存在
                    String strSql = "SELECT * FROM System WHERE UserNo = '" + txtUserNo.Text.Trim() + "'";
                    DataSet ds = SQLUtl.Query(strSql);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        MessageBox.Show("该用账户已存在，请更换账户号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserNo.Focus();
                        return;
                    }

                    String strSqlAdd = "insert into System values('" + txtUserNo.Text.Trim() + "','" + txtUserName.Text.Trim() + "','" + cbxPermission.Text.Trim() + "','" + txtUserPwd.Text.Trim() + "')";
                    int count1 = SQLUtl.ExecuteSql(strSqlAdd);
                    if (count1 != 0)
                    {
                        MessageBox.Show("添加数据成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillDataGridView();
                        LockedTextBox();
                        tsbSave.Enabled = false;
                        tsbCancel.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            if (AddOrEdit == 1)
            {
                //修改记录后的保存
                try
                {
                    String strSQLUpdate = "update System set UserNo='" + txtUserNo.Text.Trim() + "', UserName='" + txtUserName.Text.Trim() + "',Password='" + txtUserPwd.Text.Trim() + "',Permission='" + cbxPermission.Text.Trim() + "' where UserNo= '" + Convert.ToString(dgvUser[0, dgvUser.CurrentCell.RowIndex].Value).Trim() + "'";
                    int count2 = SQLUtl.ExecuteSql(strSQLUpdate);
                    if (count2 != 0)
                    {
                        MessageBox.Show("修改数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillDataGridView();
                        LockedTextBox();
                        tsbSave.Enabled = false;
                        tsbCancel.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            LockedTextBox();
            tsbCancel.Enabled = false;
            tsbSave.Enabled = false;
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_User_Load(object sender, EventArgs e)
        {
            tsbSave.Enabled = false;//保存按钮无效
            tsbCancel.Enabled = false;//取消按钮无效

            cbxPermission.Items.Add("系统管理员");
            cbxPermission.Items.Add("一般操作员");
            

            FillDataGridView();
            LockedTextBox();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LockedTextBox();
            tsbSave.Enabled = false;
            tsbCancel.Enabled = false;
            txtUserNo.Text = Convert.ToString(dgvUser[0, dgvUser.CurrentCell.RowIndex].Value).Trim();
            txtUserName.Text = Convert.ToString(dgvUser[1, dgvUser.CurrentCell.RowIndex].Value).Trim();
            //txtUserPwd.Text = Convert.ToString(dgvUser[2, dgvUser.CurrentCell.RowIndex].Value).Trim();
            cbxPermission.Text = Convert.ToString(dgvUser[3, dgvUser.CurrentCell.RowIndex].Value).Trim();
        }


    }
}
