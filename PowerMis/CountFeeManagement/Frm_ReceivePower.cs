using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DBUtility;

namespace PowerMis.CountFeeManagement
{
    public partial class Frm_ReceivePower : Form
    {
        public Frm_ReceivePower()
        {
            InitializeComponent();
        }

        private void clearForm()
        {
            txtStartCode.Text = "";
            txtEndCode.Text = "";
            txtMulti.Text = "";
            txtOfferPower.Text = "";
        }

        private void Frm_ReceivePower_Load(object sender, EventArgs e)
        {
            string strSql = "SELECT ReceiveNo,ReceiveName FROM ReceiveInfo";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lbxAmmeter.Items.Add(dt.Rows[i]["ReceiveNo"] + " " + dt.Rows[i]["ReceiveName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lbxAmmeter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ReceiveNo = lbxAmmeter.Text.Split(' ')[0];
            txtReceiveNo.Text = ReceiveNo;
            clearForm();
            showAmmeterInfo();
        }

        private void txtReceiveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int i = lbxAmmeter.FindString(txtReceiveNo.Text);
                lbxAmmeter.SelectedIndex = i;
            }
        }

        private void showAmmeterInfo()
        {
            string ReceiveNo = txtReceiveNo.Text;
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            string lastValue = "0";
            string thisValue = "0";
            string ammeterMutil = "";
            string receivePower = "0";
            DataTable dt = null;
            try
            {
                strSql = "Select Multipile From ReceiveInfo Where ReceiveNo= '" + ReceiveNo + "'  ";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    ammeterMutil = dt.Rows[0][0].ToString();
                }

                strSql = "Select ReceiveStart, ReceiveEnd, ReceivePower From ReceivePower  Where ReceiveNo= '" + ReceiveNo + "' And ReceiveDate = '" + time + "'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    lastValue = dt.Rows[0]["ReceiveStart"].ToString();
                    thisValue = dt.Rows[0]["ReceiveEnd"].ToString();
                    receivePower = dt.Rows[0]["ReceivePower"].ToString();
                }
                else
                {
                    strSql = "Select  ReceiveEnd From ReceivePower  Where ReceiveNo= '" + ReceiveNo + "' And ReceiveDate < '" + time + "' ORDER BY ReceiveDate DESC";
                    dt = SQLUtl.Query(strSql).Tables["dataSet"];
                    if (dt.Rows.Count != 0)
                    {
                        lastValue = dt.Rows[0]["ReceiveEnd"].ToString();
                    }
                }

                txtStartCode.Text = lastValue;
                txtEndCode.Text = thisValue;
                txtMulti.Text = ammeterMutil;
                txtOfferPower.Text = receivePower;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            clearForm();
            showAmmeterInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            if (txtStartCode.Text == "" || txtEndCode.Text == "")
            {
                MessageBox.Show("起码和止码都不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(txtStartCode.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || !Regex.IsMatch(txtEndCode.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("起码和止码都必须为数字！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (float.Parse(txtStartCode.Text.ToString()) < 0 || float.Parse(txtEndCode.Text.ToString()) < 0)
            {
                MessageBox.Show("起码和止码都必须大于0！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string ReceiveNo = txtReceiveNo.Text;
            float AmmeterMulti = float.Parse(txtMulti.Text.ToString());
            float lastValue = float.Parse(txtStartCode.Text.ToString());
            float thisValue = float.Parse(txtEndCode.Text.ToString());
            if (thisValue < lastValue)
            {
                if (MessageBox.Show("确定止码小于起码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (lastValue < 999)
                    {
                        MessageBox.Show("表码有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                else
                { return; }

            }
            float OfferPower = thisValue - lastValue;
            //止码小于起码处理
            if (thisValue < lastValue)
            {
                if (thisValue < 9999)
                {
                    OfferPower = 10000 - lastValue + thisValue;
                }
                else if (thisValue < 99999)
                {
                    OfferPower = 100000 - lastValue + thisValue;
                }
                else if (thisValue < 999999)
                {
                    OfferPower = 1000000 - lastValue + thisValue;
                }

            }
            OfferPower = float.Parse(String.Format("{0:###0.00}", OfferPower));
            OfferPower = OfferPower * AmmeterMulti;
            txtOfferPower.Text = OfferPower.ToString();
            string strSql = "";
            DataSet ds = null;
            //DataTable dt = null;
            try
            {
                strSql = "Select * From ReceivePower  Where ReceiveNo= '" + ReceiveNo + "' And ReceiveDate = '" + time + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql = "Insert into ReceivePower values('" + ReceiveNo + "','" + lastValue + "','" + thisValue + "','" + OfferPower + "','" + time + "')";
                }
                else
                {
                    strSql = "Update ReceivePower Set ReceiveStart='" + lastValue + "',ReceiveEnd='" + thisValue + "',ReceivePower='" + OfferPower + "' "
                            + " Where ReceiveNo= '" + ReceiveNo + "' And ReceiveDate = '" + time + "'";
                }
                SQLUtl.ExecuteSql(strSql);
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message.ToString());
            }

            if (flag)
            { MessageBox.Show("数据更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { MessageBox.Show("数据更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
