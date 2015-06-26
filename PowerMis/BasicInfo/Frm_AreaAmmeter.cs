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

namespace PowerMis.BasicInfo
{
    public partial class Frm_AreaAmmeter : Form
    {
        public Frm_AreaAmmeter()
        {
            InitializeComponent();

            this.areaAmmeterAction = new AreaAmmeterAction();
            this.areaInfoAction = new AreaInfoAction();
        }

        /* 初始化线路信息下拉表格 */
        private void LineNoList_init()
        {
            
            DataSet ds = new DataSet();

            this.areaInfoAction.getAreaInfo(ref ds);

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.AreaNoList.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.AreaNoList.SelectedIndex = -1;

            foreach (DataRow dr in dt.Rows)
            {
                this.AreaNoList_Add.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }

            this.AreaNoList_Add.SelectedIndex = -1;
        }

        private void AmmeterList_init(string key)
        {
            this.AmmeterList.Items.Clear();
            if ("" != key)
            {
                DataSet ds = new DataSet();

                this.areaAmmeterAction.getAreaAmmeterByAreaNo(key, ref ds);
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
            string areaNo = "";
            string ammeterNo = "";

            if (-1 == this.AreaNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            areaNo = this.AreaNoList.SelectedItem.ToString().Trim().Split(' ')[0];
            ammeterNo = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];

            AreaAmmeterInfo raInfo = new AreaAmmeterInfo();
            if (Constant.OK == this.areaAmmeterAction.getAAInfoById(areaNo, ammeterNo, ref raInfo))
            {
                //MessageBox.Show("")
                this.AmmeterNo.Text = raInfo.AmmeterNo;
                this.AmmeterName.Text = raInfo.AmmeterName;
                this.AmmeterMulti.Text = raInfo.AreaAmmeterMulti;
            }
        }



        private void UpdateAmmeter_Click(object sender, EventArgs e)
        {
            string areaNo = "";
            string ammeterNo = "";

            if (-1 == this.AreaNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            areaNo = this.AreaNoList.SelectedItem.ToString().Trim().Split(' ')[0];
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

            AreaAmmeterInfo raInfo = new AreaAmmeterInfo();

            raInfo.AreaNo = areaNo;
            raInfo.AmmeterNo = ammeterNo;
            raInfo.AmmeterName = this.AmmeterName.Text.ToString().Trim();
            raInfo.AreaAmmeterMulti = this.AmmeterMulti.Text.ToString().Trim();

            if (Constant.OK == this.areaAmmeterAction.updateAAInfo(areaNo, ammeterNo, ref raInfo))
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
            string areaNo = "";
            string ammeterNo = "";

            if (-1 == this.AreaNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            if (-1 == this.AmmeterList.SelectedIndex)
            {
                MessageBox.Show("请选择电表！");
                return;
            }

            areaNo = this.AreaNoList.SelectedItem.ToString().Trim().Split(' ')[0];
            ammeterNo = this.AmmeterList.SelectedItem.ToString().Trim().Split(' ')[0];
            if (MessageBox.Show("确定要删除该线区信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Constant.OK == this.areaAmmeterAction.deleteAAInfoById(areaNo, ammeterNo))
                {
                    MessageBox.Show("删除成功！");
                    if (-1 != this.AreaNoList.SelectedIndex)
                    {
                        string key = this.AreaNoList.SelectedItem.ToString().Trim().Split(' ')[0];
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
            string areaNo = "";
            if (this.AreaNoList_Add.SelectedIndex == -1)
            {
                MessageBox.Show("请选择线路！");
                return;
            }
            areaNo = this.AreaNoList_Add.SelectedItem.ToString().Trim().Split(' ')[0];

            if (this.AmmeterNo_Add.Text.ToString().Trim() == "")
            {
                MessageBox.Show("请填写电表编号！");
                return;
            }
            string ammeterNo = this.AmmeterNo_Add.Text.ToString().Trim();
            bool state = false;
            if (Constant.ERROR == this.areaAmmeterAction.checkAANo(areaNo, ammeterNo, ref state))
            {
                return;
            }

            if (!state)
            {
                MessageBox.Show("该电表编号已经存在！");
                return;
            }

            AreaAmmeterInfo raInfo = new AreaAmmeterInfo();
            raInfo.AreaNo = areaNo;
            raInfo.AmmeterNo = ammeterNo;

            if ("" == this.AmmeterName_Add.Text.ToString().Trim())
            {
                MessageBox.Show("请填写电表名称！");
                return;
            }

            raInfo.AmmeterName = this.AmmeterName_Add.Text.ToString().Trim();

            if ("" == this.AmmeterMulti_Add.Text.ToString().Trim())
            {
                MessageBox.Show("请填写电表倍率！");
                return;

            }
            raInfo.AreaAmmeterMulti = this.AmmeterMulti_Add.Text.ToString().Trim();

            if (Constant.OK == this.areaAmmeterAction.addAAInfo(ref raInfo))
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

        private void AreaNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AmmeterList.Items.Clear();
            string key = "";
            if (-1 == this.AreaNoList.SelectedIndex)
            {
                MessageBox.Show("请选择线路号！");
                return;
            }

            key = this.AreaNoList.SelectedItem.ToString().Trim().Split(' ')[0];

            if ("" != key)
            {
                this.AmmeterList_init(key);
            }
        }
        private AreaAmmeterAction areaAmmeterAction;
        private AreaInfoAction areaInfoAction;

        private void Frm_AreaAmmeter_Load(object sender, EventArgs e)
        {
            this.LineNoList_init();
            this.AmmeterNo.Enabled = false;
        }

    }
}
