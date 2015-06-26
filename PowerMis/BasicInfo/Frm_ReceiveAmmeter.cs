using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using Common;
using BusinessLogic;
using BusinessModel;

namespace PowerMis
{
    public partial class Frm_ReceiveAmmeter : Form
    {
        public Frm_ReceiveAmmeter()
        {
            InitializeComponent();
            this.receiveAmmeterAction = new ReceiveAmmeterAction();
        }

        private void AmmeterList_init()
        {
            this.AmmeterList.Items.Clear();
            DataSet ds = new DataSet();
            if (Constant.OK == this.receiveAmmeterAction.getRaInfo(ref ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.AmmeterList.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
                }
                this.AmmeterList.SelectedIndex = -1;
            }
        }

        private void UpdateAmmeter_Click(object sender, EventArgs e)
        {
            if (this.AmmeterList.SelectedIndex == -1)
            {
                MessageBox.Show("请选择电表编号！");
                return;
            }

            if (this.AmmeterName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表名称不能为空！");
                return;
            }

            if (this.AmmeterMulti.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表倍率不能为空！");
                return;
            }

            ReceiveAmmeterInfo raInfo = new ReceiveAmmeterInfo();

            raInfo.ReceiveNo = this.AmmeterNo.Text.ToString().Trim();
            raInfo.ReceiveName = this.AmmeterName.Text.ToString().Trim();
            raInfo.Multipile = this.AmmeterMulti.Text.ToString().Trim();

            if (Constant.OK == this.receiveAmmeterAction.updateRaInfo(raInfo.ReceiveNo, ref raInfo))
            {
                MessageBox.Show("数据更新成功！");
                return;
            }
            else
            {
                MessageBox.Show("数据更新失败！");
                return;
            }
        }

        private void DeleteAmmeter_Click(object sender, EventArgs e)
        {
            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择要删除的电表编号！");
                return;
            }
            if (MessageBox.Show("确定要删除该线区信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string key = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];
                if (Constant.OK == this.receiveAmmeterAction.deleteRaInfoById(key))
                {
                    MessageBox.Show("删除成功！");
                    this.AmmeterList_init();
                    this.AmmeterMulti.Text = "";
                    this.AmmeterName.Text = "";
                    this.AmmeterNo.Text = "";
                    return;

                }
                else
                {
                    MessageBox.Show("删除失败！");
                    return;
                }
            }
        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AmmeterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 != this.AmmeterList.SelectedIndex)
            {
                string key = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];
                if ("" != key)
                {
                    ReceiveAmmeterInfo raInfo = new ReceiveAmmeterInfo();
                    if (Constant.OK == this.receiveAmmeterAction.getRaInfoById(key, ref raInfo))
                    {
                        this.AmmeterNo.Text = key;

                        this.AmmeterName.Text = raInfo.ReceiveName;
                        this.AmmeterMulti.Text = raInfo.Multipile;

                    }
                }
            }
        }

        private void Frm_ReceiveAmmeter_Load(object sender, EventArgs e)
        {
            this.AmmeterNo.Enabled = false;
            this.AmmeterList_init();
        }

        private void AddAmmeter_Click(object sender, EventArgs e)
        {
            if ("" == this.AmmeterNo_Add.Text.ToString().Trim())
            {
                MessageBox.Show("电表编号不能为空！");
                return;
            }
            bool state = false;

            if (Constant.OK != this.receiveAmmeterAction.checkRaInfoNo(this.AmmeterNo_Add.Text.ToString().Trim(), ref state))
            {
                MessageBox.Show("数据库出错！");
                return;
            }

            if (!state)
            {
                MessageBox.Show("该编号已经存在！");
                return;
            }
            


            if (this.AmmeterName_Add.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表名称不能为空！");
                return;
            }

            if (this.AmmeterMulti_Add.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表倍率不能为空！");
                return;
            }

            ReceiveAmmeterInfo raInfo = new ReceiveAmmeterInfo();

            raInfo.ReceiveNo = this.AmmeterNo_Add.Text.ToString().Trim();
            raInfo.ReceiveName = this.AmmeterName_Add.Text.ToString().Trim();
            raInfo.Multipile = this.AmmeterMulti_Add.Text.ToString().Trim();

            if (Constant.OK == this.receiveAmmeterAction.addRaInfo(ref raInfo))
            {
                MessageBox.Show("添加成功！");
                this.AmmeterList_init();
                return;
            }
            else
            {
                MessageBox.Show("添加失败！");
                return;
            }
        }

        private void CloseForm_Add_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private ReceiveAmmeterAction receiveAmmeterAction;


    }
}
