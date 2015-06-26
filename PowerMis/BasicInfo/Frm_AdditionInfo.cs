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


namespace PowerMis.BasicInfo
{
    public partial class Frm_AdditionInfo : Form
    {
        public Frm_AdditionInfo()
        {
            InitializeComponent();
            this.additionInfoAction = new AdditionInfoAction();
        }
        private void form_init()
        {
            //AdditionInfo additionInfo = new AdditionInfo();
            DataSet ds = new DataSet();
            if (Constant.OK == this.additionInfoAction.getAddition(ref ds))
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    this.CountryAddition.Text = dt.Rows[0].ItemArray[0].ToString().Trim();
                    //this.BasicEFee.Text = dt.Rows[0].ItemArray[1].ToString().Trim();
                    this.BasicMulti.Text = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
            }
        }
        private void ModifyAddition_Click(object sender, EventArgs e)
        {
            if ("" == this.CountryAddition.Text.ToString().Trim())
            {
                MessageBox.Show("农网附加费不能为空！");
                return;
            }

            if ("" == this.BasicMulti.Text.ToString().Trim())
            {
                MessageBox.Show("基本电费率不能为空！");
                return;
            }

            AdditionInfo additionInfo = new AdditionInfo();
            additionInfo.CountryAnnex = this.CountryAddition.Text.ToString().Trim();
            //additionInfo.EssenceFee = this.BasicEFee.Text.ToString().Trim();
            additionInfo.EssenceFeeRate = this.BasicMulti.Text.ToString().Trim();

            if (Constant.OK == this.additionInfoAction.updateAddition(ref additionInfo))
            {
                MessageBox.Show("附加信息修改成功！");
                return;
            }
            else
            {
                MessageBox.Show("附加信息修改失败！");
                return;
            }
        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_AdditionInfo_Load(object sender, EventArgs e)
        {
            this.form_init();
        }
        private AdditionInfoAction additionInfoAction;
    }
}
