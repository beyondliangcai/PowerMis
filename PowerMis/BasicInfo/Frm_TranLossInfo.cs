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
    public partial class Frm_TranLossInfo : Form
    {
        public Frm_TranLossInfo()
        {
            InitializeComponent();
            this.t_lose_action = new TransformerLoseAction();
        }

        private void T_List_init()
        {
            DataSet ds = new DataSet();
            if (Constant.OK == this.t_lose_action.getTLose(ref ds))
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.T_List.Items.Add(dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + " " + dr.ItemArray[2].ToString());
                    }
                    this.T_List.SelectedIndex = -1;
                    this.T_List.Refresh();


                    /*
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        this.T_Lose_Add.Items.Add(dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + " " + dr.ItemArray[2].ToString());
                    }
                    this.T_Lose_Add.SelectedIndex = -1;
                    this.T_Lose_Add.Refresh();
                     */
                }
            }
            else
            {
                MessageBox.Show("数据库出错！");
                return;
            }
        }

        private void Close_Form_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Update_Lose_Click(object sender, EventArgs e)
        {
            List<TransformerLose> t_lose_list = new List<TransformerLose>();

           

            if ("" == this.TType.Text.ToString().Trim())
            {
                MessageBox.Show("电压器类型不能为空！");
                return;
            }
            if ("" == this.Limit.Text.ToString().Trim())
            {
                MessageBox.Show("额定容量不能为空！");
                return;
            }

            
            if ("" == this.Less_11.Text.ToString().Trim() || "" == this.Less12.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_1 = new TransformerLose();
                t_lose_1.TransformerLoseNo = this.LoseNo.Text.ToString().Trim();
                t_lose_1.TransformerType = this.TType.Text.ToString().Trim();
                t_lose_1.StandarVolume = this.Limit.Text.ToString().Trim();
                t_lose_1.MonthUsed = this.Less_11.Text.ToString().Trim();
                t_lose_1.TranformerLose = this.Less12.Text.ToString().Trim();
                t_lose_1.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_1);
            }

            if ("" == this.Less22.Text.ToString().Trim() || "" == this.Less21.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_2 = new TransformerLose();
                t_lose_2.TransformerLoseNo = this.LoseNo.Text.ToString().Trim();
                t_lose_2.TransformerType = this.TType.Text.ToString().Trim();
                t_lose_2.StandarVolume = this.Limit.Text.ToString().Trim();
                t_lose_2.MonthUsed = this.Less21.Text.ToString().Trim();
                t_lose_2.TranformerLose = this.Less22.Text.ToString().Trim();
                t_lose_2.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_2);
            }

            if ("" == this.Less32.Text.ToString().Trim() || "" == this.Less31.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_3 = new TransformerLose();
                t_lose_3.TransformerLoseNo = this.LoseNo.Text.ToString().Trim();
                t_lose_3.TransformerType = this.TType.Text.ToString().Trim();
                t_lose_3.StandarVolume = this.Limit.Text.ToString().Trim();
                t_lose_3.MonthUsed = this.Less31.Text.ToString().Trim();
                t_lose_3.TranformerLose = this.Less32.Text.ToString().Trim();
                t_lose_3.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_3);
            }

            if ("" == this.More.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_4 = new TransformerLose();
                t_lose_4.TransformerLoseNo = this.LoseNo.Text.ToString().Trim();
                t_lose_4.TransformerType = this.TType.Text.ToString().Trim();
                t_lose_4.StandarVolume = this.Limit.Text.ToString().Trim();
                t_lose_4.MonthUsed = this.Less31.Text.ToString().Trim();
                t_lose_4.TranformerLose = this.More.Text.ToString().Trim();
                t_lose_4.LessOrMoreFlag = "1";
                t_lose_list.Add(t_lose_4);
            }

            if (Constant.OK == this.t_lose_action.updateTLose(this.LoseNo.Text.ToString().Trim(), ref t_lose_list))
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

        private void Delete_Lose_Click(object sender, EventArgs e)
        {
            if (this.T_List.SelectedIndex != -1)
            {
                if (MessageBox.Show("确定要删除该变损信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    if (Constant.OK == this.t_lose_action.deleteTLoseById(this.LoseNo.Text.ToString().Trim()))
                    {
                        MessageBox.Show("删除成功！");
                        //this.MaintainCustom_Load();
                        this.T_List_init();
                        this.LoseNo.Text = "";
                        this.Limit.Text = "";
                        this.TType.Text = "";
                        this.Less_11.Text = "";
                        this.Less12.Text = "";
                        this.Less21.Text = "";
                        this.Less22.Text = "";
                        this.Less31.Text = "";
                        this.Less32.Text = "";
                        this.More.Text = "";
                       // this.T_List.SelectedIndex = -1;

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

        private void Add_Button_Click(object sender, EventArgs e)
        {
            List<TransformerLose> t_lose_list = new List<TransformerLose>();
            if (this.LoseNo_Add.Text.ToString().Trim() == "")
            {
                MessageBox.Show("请填写变损编号！");
                return;
            }
            bool state = false;
            this.t_lose_action.checkTLoseNo(this.LoseNo_Add.Text.ToString().Trim(), ref state);
            if (!state)
            {
                MessageBox.Show("该变损编号已经存在！");
                return;
            }
            if ("" == this.TType_Add.Text.ToString().Trim())
            {
                MessageBox.Show("电压器类型不能为空！");
                return;
            }
            if ("" == this.Limit_Add.Text.ToString().Trim())
            {
                MessageBox.Show("额定容量不能为空！");
                return;
            }

            
            if ("" == this.Less_Add_11.Text.ToString().Trim() || "" == this.Less_Add_12.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_1 = new TransformerLose();
                t_lose_1.TransformerLoseNo = this.LoseNo_Add.Text.ToString().Trim();
                t_lose_1.TransformerType = this.TType_Add.Text.ToString().Trim();
                t_lose_1.StandarVolume = this.Limit_Add.Text.ToString().Trim();
                t_lose_1.MonthUsed = this.Less_Add_11.Text.ToString().Trim();
                t_lose_1.TranformerLose = this.Less_Add_12.Text.ToString().Trim();
                t_lose_1.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_1);
            }

            if ("" == this.Less_Add_21.Text.ToString().Trim() || "" == this.Less_Add_22.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_2 = new TransformerLose();
                t_lose_2.TransformerLoseNo = this.LoseNo_Add.Text.ToString().Trim();
                t_lose_2.TransformerType = this.TType_Add.Text.ToString().Trim();
                t_lose_2.StandarVolume = this.Limit_Add.Text.ToString().Trim();
                t_lose_2.MonthUsed = this.Less_Add_21.Text.ToString().Trim();
                t_lose_2.TranformerLose = this.Less_Add_22.Text.ToString().Trim();
                t_lose_2.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_2);
            }

            if ("" == this.Less_Add_31.Text.ToString().Trim() || "" == this.Less_Add_32.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_3 = new TransformerLose();
                t_lose_3.TransformerLoseNo = this.LoseNo_Add.Text.ToString().Trim();
                t_lose_3.TransformerType = this.TType_Add.Text.ToString().Trim();
                t_lose_3.StandarVolume = this.Limit_Add.Text.ToString().Trim();
                t_lose_3.MonthUsed = this.Less_Add_31.Text.ToString().Trim();
                t_lose_3.TranformerLose = this.Less_Add_32.Text.ToString().Trim();
                t_lose_3.LessOrMoreFlag = "0";
                t_lose_list.Add(t_lose_3);
            }

            if ("" == this.More_Add.Text.ToString().Trim())
            {
                MessageBox.Show("变损信息没有填写！");
                return;
            }
            else
            {
                TransformerLose t_lose_4 = new TransformerLose();
                t_lose_4.TransformerLoseNo = this.LoseNo_Add.Text.ToString().Trim();
                t_lose_4.TransformerType = this.TType_Add.Text.ToString().Trim();
                t_lose_4.StandarVolume = this.Limit_Add.Text.ToString().Trim();
                t_lose_4.MonthUsed = this.Less_Add_31.Text.ToString().Trim();
                t_lose_4.TranformerLose = this.More_Add.Text.ToString().Trim();
                t_lose_4.LessOrMoreFlag = "1";
                t_lose_list.Add(t_lose_4);
            }

            if (Constant.OK == this.t_lose_action.addTLose(ref t_lose_list))
            {
                MessageBox.Show("添加成功！");

                this.T_List_init();
                this.LoseNo_Add.Text = "";
                this.Limit_Add.Text = "";
                this.TType_Add.Text = "";
                this.Less_Add_11.Text = "";
                this.Less_Add_12.Text = "";
                this.Less_Add_21.Text = "";
                this.Less_Add_22.Text = "";
                this.Less_Add_31.Text = "";
                this.Less_Add_32.Text = "";
                this.More_Add.Text = "";
                // this.T_List.SelectedIndex = -1;

                this.Refresh();
                return;
            }
            else
            {
                MessageBox.Show("添加失败！");
                return;
            }
           
        }



        private void Frm_TranLossInfo_Load(object sender, EventArgs e)
        {
            this.T_List_init();
        }

        private TransformerLoseAction t_lose_action;

        private void T_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TransformerInfo transfomerInfo = new TransformerInfo();
            TransformerLose transformerLose = new TransformerLose();
            List<TransformerLose> t_lose_list = new List<TransformerLose>();

            if (-1 != this.T_List.SelectedIndex)
            {
                //char c = ;
                string key = this.T_List.SelectedItem.ToString().Trim().Split(' ')[0];
                if (key == "")
                {
                    return;
                }
                if (Constant.OK == this.t_lose_action.getTLoseById(key, ref t_lose_list))
                {
                    
                            //string item = transformerLose.TransformerLoseNo + " " + transformerLose.TransformerType + " " + transformerLose.StandarVolume;
                            //this.T_Lose_Type.SelectedItem = item;

                            this.LoseNo.Text = t_lose_list[0].TransformerLoseNo;
                            this.LoseNo.Enabled = false;

                            this.TType.Text = t_lose_list[0].TransformerType;

                            this.Limit.Text = t_lose_list[0].StandarVolume;
                            
                            int count = t_lose_list.Count;
                            switch(count)
                            {
                                case 1:
                                    {
                                        this.Less_11.Text = t_lose_list[0].MonthUsed;
                                        this.Less12.Text = t_lose_list[0].TranformerLose;
                                        break;
                                    }
                                case 2:
                                    {
                                        this.Less_11.Text = t_lose_list[0].MonthUsed;
                                        this.Less12.Text = t_lose_list[0].TranformerLose;
                                        this.Less21.Text = t_lose_list[1].MonthUsed;
                                        this.Less22.Text = t_lose_list[1].TranformerLose;
                                        break;
                                    }
                                case 3:
                                    {
                                        this.Less_11.Text = t_lose_list[0].MonthUsed;
                                        this.Less12.Text = t_lose_list[0].TranformerLose;
                                        this.Less21.Text = t_lose_list[1].MonthUsed;
                                        this.Less22.Text = t_lose_list[1].TranformerLose;
                                        this.Less31.Text = t_lose_list[2].MonthUsed;
                                        this.Less32.Text = t_lose_list[2].TranformerLose;
                                        break;
                                    }
                                case 4:
                                    {
                                        this.Less_11.Text = t_lose_list[0].MonthUsed;
                                        this.Less12.Text = t_lose_list[0].TranformerLose;
                                        this.Less21.Text = t_lose_list[1].MonthUsed;
                                        this.Less22.Text = t_lose_list[1].TranformerLose;
                                        this.Less31.Text = t_lose_list[2].MonthUsed;
                                        this.Less32.Text = t_lose_list[2].TranformerLose;
                                        this.More.Text = t_lose_list[3].TranformerLose;
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            
                            //this.Less_11.Text = t_lose_list[0].TranformerLose;
                            //this.Less12.Text = t_lose_list[0].MonthUsed;
                            
                        }
                }
                else
                {
                        //do nothing
                }
                   
                   
           }
        }

        private void Delete_Add_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
