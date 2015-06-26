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
using BusinessModel;
using BusinessLogic;

namespace PowerMis.BasicInfo
{
    public partial class Frm_LineAmmeter : Form
    {
        public Frm_LineAmmeter()
        {
            InitializeComponent();
            this.lineAmmeterAction = new LineAmmeterAction();
            this.lineInfoAction = new LineInfoAction();
        }

        /* 初始化线路信息下拉表格 */
        private void LineNoList_init()
        {
            
            
            DataSet ds = new DataSet();

            this.lineInfoAction.getLineInfo(ref ds);

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.LineNoList.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.LineNoList.SelectedIndex = -1;

            foreach (DataRow dr in dt.Rows)
            {
                this.LineNoList_Add.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.LineNoList_Add.SelectedIndex = -1;
        }

        private void AmmeterList_init(string key)
        {
            this.AmmeterList.Items.Clear();
            if ("" != key)
            {
                DataSet ds = new DataSet();

                this.lineAmmeterAction.getLineAmmeterByLineNo(key, ref ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.AmmeterList.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
                    }

                }
            }

        }

        private void AmmeterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lineNo = "";
            string ammeterNo = "";
            
            if (-1 == this.LineNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            lineNo = this.LineNoList.SelectedItem.ToString().Trim().Split(' ')[0];
            ammeterNo = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];

            LineAmmeterInfo laInfo = new LineAmmeterInfo();
            if (Constant.OK == this.lineAmmeterAction.getLineAmmeterById(lineNo, ammeterNo, ref laInfo))
            {
                //MessageBox.Show("")
                this.AmmeterNo.Text = laInfo.AmmeterNo;
                this.AmmeterName.Text = laInfo.AmmeterName;
                this.AmmeterMulti.Text = laInfo.LineAmmeterMulti;
            }

        }

        private void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AmmeterList.Items.Clear();
            string key = "";
            if (-1 == this.LineNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            key = this.LineNoList.SelectedItem.ToString().Trim().Split(' ')[0];

            if ("" != key)
            {
                this.AmmeterList_init(key);
            }
        }

        private void UpdateAmmeter_Click(object sender, EventArgs e)
        {
            string lineNo = "";
            string ammeterNo = "";

            if (-1 == this.LineNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            lineNo = this.LineNoList.SelectedItem.ToString().Trim().Split(' ')[0];
            ammeterNo = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];

            if (this.AmmeterName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表名称不能为空！");
                return;
            }

            if (this.AmmeterMulti.Text.ToString().Trim() == "")
            {
                MessageBox.Show("电表倍率不能为空值！");
                return;
            }

            LineAmmeterInfo laInfo = new LineAmmeterInfo();

            laInfo.LineNum = lineNo;
            laInfo.AmmeterNo = ammeterNo;
            laInfo.AmmeterName = this.AmmeterName.Text.ToString().Trim();
            laInfo.LineAmmeterMulti = this.AmmeterMulti.Text.ToString().Trim();

            if (Constant.OK == this.lineAmmeterAction.updateLineAmmeter(lineNo, ammeterNo, ref laInfo))
            {
                MessageBox.Show("电表信息更新成功！");
                return;
            }
            else
            {
                MessageBox.Show("电表信息更新失败！");
                return;
            }
        }

        private void DeleteAmmeter_Click(object sender, EventArgs e)
        {
            string lineNo = "";
            string ammeterNo = "";

            if (-1 == this.LineNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            lineNo = this.LineNoList.SelectedItem.ToString().Trim().Split(' ')[0];
            ammeterNo = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];
            if (MessageBox.Show("确定要删除该线区信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Constant.OK == this.lineAmmeterAction.deleteLANoById(lineNo, ammeterNo))
                {
                    MessageBox.Show("删除成功！");
                    if (-1 != this.LineNoList.SelectedIndex)
                    {
                        string key = this.LineNoList.SelectedItem.ToString().Trim().Split(' ')[0];
                        this.AmmeterList_init(key);
                        this.Refresh();
                    }
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

        private void AddAmmeter_Click(object sender, EventArgs e)
        {
            string lineNo = "";
            if (this.LineNoList_Add.SelectedIndex == -1)
            {
                MessageBox.Show("请选择线路！");
                return;
            }
            lineNo = this.LineNoList_Add.SelectedItem.ToString().Trim().Split(' ')[0];

            if (this.AmmeterNo_Add.Text.ToString().Trim() == "")
            {
                MessageBox.Show("请填写电表编号！");
                return;
            }
            string ammeterNo = this.AmmeterNo_Add.Text.ToString().Trim();
            bool state = false;
            if (Constant.ERROR == this.lineAmmeterAction.checkLANo(lineNo, ammeterNo, ref state))
            {
                return;
            }

            if (!state)
            {
                MessageBox.Show("该电表编号已经存在！");
                return;
            }

            LineAmmeterInfo laInfo = new LineAmmeterInfo();
            laInfo.LineNum = lineNo;
            laInfo.AmmeterNo = ammeterNo;

            if ("" == this.AmmeterName_Add.Text.ToString().Trim())
            {
                MessageBox.Show("请填写电表名称！");
                return;
            }

            laInfo.AmmeterName = this.AmmeterName_Add.Text.ToString().Trim();

            if ("" == this.AmmeterMulti_Add.Text.ToString().Trim())
            {
                MessageBox.Show("请填写电表倍率！");
                return;

            }
            laInfo.LineAmmeterMulti = this.AmmeterMulti_Add.Text.ToString().Trim();

            if (Constant.OK == this.lineAmmeterAction.addLineAmmeter(ref laInfo))
            {
                MessageBox.Show("添加成功！");
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

        private void Frm_LineAmmeter_Load(object sender, EventArgs e)
        {
            this.LineNoList_init();
            this.AmmeterNo.Enabled = false;
            //this.AmmeterList_init();
            this.Refresh();
        }

        private LineInfoAction lineInfoAction;
        private LineAmmeterAction lineAmmeterAction;
    }
}
