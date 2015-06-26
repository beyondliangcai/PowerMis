using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using BusinessLogic;
using BusinessModel;
using Common;

namespace PowerMis.BasicInfo
{
    public partial class Frm_LineInfo : Form
    {
        public Frm_LineInfo()
        {
            InitializeComponent();
            this.lineInfoAction = new LineInfoAction();
            this.lineInfo = new LineInfo();
        }

        public int LineList_init()
        {
            DataSet ds =  new DataSet();
            if (Constant.OK == this.lineInfoAction.getLineInfo(ref ds))
            {
                this.LineList.DataSource = ds.Tables[0];
                this.LineList.DisplayMember = "LineName";
                this.LineList.ValueMember = "LineNum";
                this.LineList.SelectedIndex = -1;
                return Constant.OK;
            }
            else
            {
                MessageBox.Show("数据初始化失败！");
                return Constant.ERROR;
            }
            
        }

        public int PowerPlace_init()
        {
            this.PowerPlace.Items.Add("西斋");
            this.PowerPlace.Items.Add("大岩咀");
            this.PowerPlace.SelectedIndex = -1;

            this.PowerStation_add.Items.Add("西斋");
            this.PowerStation_add.Items.Add("大岩咀");
            this.PowerStation_add.SelectedIndex = -1;
            return Constant.OK;
        }

        private void LineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LineInfo lineInfo = new LineInfo();
            string lineNo = "";
            if (this.LineList.SelectedIndex != -1)
            {
                
                    lineNo = this.LineList.SelectedValue.ToString().Trim();
                
                    if (Constant.OK == this.lineInfoAction.getLineInfoById(lineNo, ref this.lineInfo))
                    {
                        this.LineName.Text = lineInfo.LineName;
                        //if ("0" == lineInfo.PowerPlaceNo.Trim())
                        //{
                        this.PowerPlace.SelectedIndex = int.Parse(lineInfo.PowerPlaceNo.Trim());
                        //}
                        this.ReportOrder.Text = lineInfo.ReportOrder;
                        return;
                    }
                    else
                    {
                        //MessageBox.Show("数据库出错！");
                        return;
                    }
                
                
            }
        }

        private void LineInfo_Load(object sender, EventArgs e)
        {
            this.LineList_init();
            this.PowerPlace_init();
            this.Refresh();
        }

        private void UpdateInfo_Click(object sender, EventArgs e)
        {
            //LineInfo lineInfo = new LineInfo();

            if (this.LineList.SelectedIndex != -1)
            {
                this.lineInfo.LineNum = this.LineList.SelectedValue.ToString().Trim();
            }
            else
            {
                MessageBox.Show("请选择要更新的线区！");
                return;
            }

            if (this.LineName.Text.Trim() != "")
            {
                this.lineInfo.LineName = this.LineName.Text.Trim();
            }
            else
            {
                MessageBox.Show("线区名称不能为空！");
                return;
            }

            if (this.PowerPlace.SelectedIndex != -1)
            {
                this.lineInfo.PowerPlaceNo = this.PowerPlace.SelectedIndex.ToString().Trim();
            }
            else
            {
                MessageBox.Show("所属供电站不能为空！");
                return;
            }

            if ("" != this.ReportOrder.Text.Trim())
            {
                this.lineInfo.ReportOrder = this.ReportOrder.Text.Trim();
            }
            else
            {
                MessageBox.Show("报表顺序不能为空！");
                return;
            }

            if (this.lineInfoAction.updateLineInfo(this.lineInfo.LineNum, ref this.lineInfo) == Constant.OK)
            {
                MessageBox.Show("线区信息更新成功！");
                
                return;
            }
            else
            {
                MessageBox.Show("线区信息更新失败！");
                return;
            }

            
        }

        private void DeleteInfo_Click(object sender, EventArgs e)
        {
            if (this.LineList.SelectedIndex != -1)
            {
                if (MessageBox.Show("确定要删除该线区信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    if (Constant.OK == this.lineInfoAction.deleteLineInfo(this.LineList.SelectedValue.ToString().Trim()))
                    {
                        MessageBox.Show("删除成功！");
                        //this.MaintainCustom_Load();
                        this.LineList_init();
                        this.LineName.Text = "";
                        this.PowerPlace.SelectedIndex = -1;
                        this.ReportOrder.Text = "";

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
                MessageBox.Show("请选择要删除的线区编号！");
            }


            
           

        
        }

        private void CloseInfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private LineInfoAction lineInfoAction;
        private LineInfo lineInfo;

        private void Close_add_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
       * 添加线区信息
       * @author      Rick
       **/ 
        private void AddButton_Click(object sender, EventArgs e)
        {
            if ("" == this.LineNum_add.Text.Trim())
            {
                MessageBox.Show("线路代码不能为空！");
                return;
            }

            bool state = false;
            this.lineInfoAction.checkLineNum(this.LineNum_add.Text.Trim(), ref state);
            if (!state)
            {
                MessageBox.Show("该线路代码已经存在！");
                return;
            }

            LineInfo _lineinfo = new LineInfo();
            _lineinfo.LineNum = this.LineNum_add.Text.Trim();

            if ("" == this.LineName_add.Text.Trim())
            {
                MessageBox.Show("线区名称不能为空！");
                return;
            }
            _lineinfo.LineName = this.LineName_add.Text.Trim();

            if (-1 == this.PowerStation_add.SelectedIndex)
            {
                MessageBox.Show("请选择所属线区！");
                return;
            }
            _lineinfo.PowerPlaceNo = this.PowerStation_add.SelectedIndex.ToString().Trim();

            _lineinfo.ReportOrder = this.ReportOrder_add.Text.Trim();
            if (Constant.OK == this.lineInfoAction.addLineInfo(ref _lineinfo))
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}
