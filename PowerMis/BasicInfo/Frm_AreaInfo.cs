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
    public partial class Frm_AreaInfo : Form
    {
        public Frm_AreaInfo()
        {
            InitializeComponent();
            this.areaInfoAction = new AreaInfoAction();
        }
        private void AreaList_init()
        {
            DataSet ds = new DataSet();
            this.areaInfoAction.getAreaInfo(ref ds);

            DataTable dt = ds.Tables[0];
            this.AreaList.DataSource = dt;
            this.AreaList.DisplayMember = "AreaNo";
            this.AreaList.ValueMember = "AreaNo";
            this.AreaList.SelectedIndex = -1;
        }

        private void AreaType_init()
        {
            this.AreaType.Items.Add("直供台区");
            this.AreaType.Items.Add("农村台区");
            this.AreaType.Items.Add("企业台区");
            this.AreaType.SelectedIndex = -1;


            this.AreaType_add.Items.Add("直供台区");
            this.AreaType_add.Items.Add("农村台区");
            this.AreaType_add.Items.Add("企业台区");
            this.AreaType_add.SelectedIndex = -1;
        }

        /**
         * 更新台区信息
         **/ 
        private void button1_Click(object sender, EventArgs e)
        {
            AreaInfo areaInfo = new AreaInfo();

            if (-1 == this.AreaList.SelectedIndex)
            {
                MessageBox.Show("请选择要更新的台区！");
                return;
            }
            areaInfo.AreaNo = this.AreaList.SelectedValue.ToString().Trim();

            if (this.AreaName.Text.Trim() == "")
            {
                MessageBox.Show("台区名称不能为空！");
                return;
            }
            areaInfo.AreaName = this.AreaName.Text.Trim();

            if (-1 == this.AreaType.SelectedIndex)
            {
                MessageBox.Show("请选择台区性质！");
                return;
            }

            areaInfo.AreaFlag = this.AreaType.SelectedIndex.ToString().Trim();

            if (Constant.OK == this.areaInfoAction.updateAreaInfo(areaInfo.AreaNo, ref areaInfo))
            {
                MessageBox.Show("更新成功！");
                return;
            }
            else
            {
                MessageBox.Show("更新失败！");
                return;
            }

            
        }

        /**
         * 删除台区信息
         **/ 
        private void button2_Click(object sender, EventArgs e)
        {

            if (this.AreaList.SelectedIndex != -1)
            {
                if (MessageBox.Show("确定要删除该台区信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    if (Constant.OK == this.areaInfoAction.deleteAreaInfoById(this.AreaList.SelectedValue.ToString().Trim()))
                    {
                        MessageBox.Show("删除成功！");
                        //this.MaintainCustom_Load();
                        this.AreaList_init();
                        this.AreaName.Text = "";
                        this.AreaType.SelectedIndex = -1;
                        //this.Text = "";

                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }


                }
            }
            else
            {
                MessageBox.Show("请选择要删除的台区编号！");
            }
        }

        /**
         * 关闭窗口
         **/ 
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AreaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AreaInfo areaInfo = new AreaInfo();
            if (-1 != this.AreaList.SelectedIndex)
            {
                if (Constant.OK == this.areaInfoAction.getAreaInfoById(this.AreaList.SelectedValue.ToString().Trim(), ref areaInfo))
                {
                    this.AreaName.Text = areaInfo.AreaName;
                    if ("" != areaInfo.AreaFlag)
                    {
                        if (int.Parse(areaInfo.AreaFlag) < 3)
                        {
                            this.AreaType.SelectedIndex = int.Parse(areaInfo.AreaFlag);
                        }
                    }
                }
            }
        }

        private void Frm_AreaInfo_Load(object sender, EventArgs e)
        {
            this.AreaType_init();
            this.AreaList_init();
            this.Refresh();
        }

        private AreaInfoAction areaInfoAction;

        private void add_button_Click(object sender, EventArgs e)
        {
            AreaInfo areaInfo = new AreaInfo();
            if (this.AreaNum_add.Text.Trim() == "")
            {
                MessageBox.Show("台区代码为空！");
                return;
            }
            bool state = false;
            this.areaInfoAction.checkAreaNo(this.AreaNum_add.Text.Trim(), ref state);

            if (!state)
            {
                MessageBox.Show("台区代码已经存在！");
                return;
            }
            areaInfo.AreaNo = this.AreaNum_add.Text.Trim();

            if ("" == this.AreaName_add.Text.Trim())
            {
                MessageBox.Show("台区名称不能为空！");
                return;
            }
            areaInfo.AreaName = this.AreaName_add.Text.Trim();

            if (-1 == this.AreaType_add.SelectedIndex)
            {
                MessageBox.Show("请选择台区性质！");
                return;
            }
            areaInfo.AreaFlag = this.AreaType_add.SelectedIndex.ToString();
            

            if (Constant.OK == this.areaInfoAction.addAreaInfo(ref areaInfo))
            {
                MessageBox.Show("添加台区信息成功！");
                AreaList_init();
                return;
            }
            else
            {
                MessageBox.Show("添加台区信息失败！");
                return;
            }

        }

        private void add_close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
