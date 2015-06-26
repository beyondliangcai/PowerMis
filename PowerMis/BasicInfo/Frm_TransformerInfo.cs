using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;
using BusinessModel;
using Common;

namespace PowerMis.BasicInfo
{
    public partial class Frm_TransformerInfo : Form
    {
        public Frm_TransformerInfo()
        {
            InitializeComponent();
            this.t_info_action = new TransformerInfoAction();
            this.t_lose_action = new TransformerLoseAction();
        }


        private void T_List_init()
        {
            DataSet ds = new DataSet();
            this.t_info_action.getTInfo(ref ds);

            DataTable dt = ds.Tables[0];
            //this.T_List.DataSource = dt;

            //初始化T_List里面的项目
            /*
            this.T_List.DisplayMember = "TransformerName";
            this.T_List.ValueMember = "TransformerNo";
            */
            foreach (DataRow dr in dt.Rows)
            {
                this.T_List.Items.Add(dr.ItemArray[0].ToString().Trim() + " " + dr.ItemArray[1].ToString().Trim());
            }
            this.T_List.SelectedIndex = -1;
             
        }

        private void T_Lose_init()
        {
            DataSet ds = new DataSet();
            if (Constant.OK == this.t_lose_action.getTLose(ref ds))
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.T_Lose_Type.Items.Add(dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + " " + dr.ItemArray[2].ToString());
                    }
                    this.T_Lose_Type.SelectedIndex = -1;
                    this.T_Lose_Type.Refresh();


                    foreach (DataRow dr in dt.Rows)
                    {
                        this.T_Lose_Add.Items.Add(dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + " " + dr.ItemArray[2].ToString());
                    }
                    this.T_Lose_Add.SelectedIndex = -1;
                    this.T_Lose_Add.Refresh();
                }
            }
            else
            {
                MessageBox.Show("数据库出错！");
                return;
            }
        }


        private void T_Update_Click(object sender, EventArgs e)
        {
            TransformerInfo t_info = new TransformerInfo();

            if (-1 == this.T_List.SelectedIndex)
            {
                MessageBox.Show("请选择要更新的变压器！");
                return;
            }
            t_info.TransformerNo = this.T_List.SelectedItem.ToString().Trim().Split(' ')[0]; 

            if (this.T_Name.Text.Trim() == "")
            {
                MessageBox.Show("变压器名称不能为空！");
                return;
            }
            t_info.TransformerName = this.T_Name.Text.Trim();

            if (-1 == this.T_Lose_Type.SelectedIndex)
            {
                MessageBox.Show("请选择变损类型！");
                return;
            }

            t_info.TransformerLoseNo = this.T_Lose_Type.SelectedItem.ToString().Split(' ')[0];

            if (Constant.OK == this.t_info_action.updateTInfo(t_info.TransformerNo, ref t_info))
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

        private void T_Delete_Click(object sender, EventArgs e)
        {
            if (this.T_List.SelectedIndex != -1)
            {
                if (MessageBox.Show("确定要删除该变压器信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    if (Constant.OK == this.t_info_action.deleteTInfoById(this.T_List.SelectedItem.ToString().Trim().Split(' ')[0]))
                    {
                        MessageBox.Show("删除成功！");
                        //this.MaintainCustom_Load();
                        this.T_List_init();
                        this.T_Name.Text = "";
                        this.T_Lose_Type.SelectedIndex = -1;
                        this.T_Code.Text = "";

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
                MessageBox.Show("请选择要删除的变压器编号！");
            }
        }

        private void T_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void T_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransformerInfo transfomerInfo = new TransformerInfo();
            TransformerLose transformerLose = new TransformerLose();

            if (-1 != this.T_List.SelectedIndex)
            {
                if (Constant.OK == this.t_info_action.getTInfoById(this.T_List.SelectedItem.ToString().Trim().Split(' ')[0], ref transfomerInfo))
                {
                    this.T_Code.Text = transfomerInfo.TransformerNo;
                    this.T_Code.Enabled = false;
                    this.T_Name.Text = transfomerInfo.TransformerName;
                    if ("" != transfomerInfo.TransformerLoseNo)
                    {
                        if (Constant.OK == this.t_lose_action.getTLoseTypeById(transfomerInfo.TransformerLoseNo, ref transformerLose))
                        {
                            string item = transformerLose.TransformerLoseNo + " " + transformerLose.TransformerType + " " + transformerLose.StandarVolume;
                            this.T_Lose_Type.SelectedItem = item;
                        }
                    }else
                    {
                        //do nothing
                    }
                   
                   
                }
            }
        }


        /* 初始化窗口中的各个组件中的信息 */
        private void TransformerInfo_Load(object sender, EventArgs e)
        {
            this.T_List_init();
            this.T_Lose_init();
            this.Refresh();

        }
        private TransformerInfoAction t_info_action;

        private TransformerLoseAction t_lose_action;

        private void T_Add_Button_Click(object sender, EventArgs e)
        {
            if ("" == this.T_Code_Add.Text.Trim())
            {
                MessageBox.Show("变压器代码不能为空！");
                return;
            }

            bool state = false;
            this.t_info_action.checkTInfoNo(this.T_Code_Add.Text.Trim(), ref state);
            if (!state)
            {
                MessageBox.Show("该变压器代码已经存在！");
                return;
            }

            TransformerInfo t_info = new TransformerInfo();
            t_info.TransformerNo = this.T_Code_Add.Text.Trim();

            if ("" == this.T_Name_Add.Text.Trim())
            {
                MessageBox.Show("变压器名称不能为空！");
                return;
            }
            t_info.TransformerName = this.T_Name_Add.Text.Trim();

            if (-1 == this.T_Lose_Add.SelectedIndex)
            {
                MessageBox.Show("请选择所属变损类型！");
                return;
            }
            t_info.TransformerLoseNo = this.T_Lose_Add.SelectedIndex.ToString().Trim().Split(' ')[0];


            if (Constant.OK == this.t_info_action.addTInfo(ref t_info))
            {
                MessageBox.Show("添加成功！");
                this.T_List_init();
                this.T_Name_Add.Text = "";
                this.T_Lose_Add.SelectedIndex = -1;
                this.T_Code_Add.Text = "";
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }

        private void T_Close_Add_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
