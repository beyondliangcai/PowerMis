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
    public partial class Frm_AreaPowerSupply : Form
    {
        public Frm_AreaPowerSupply()
        {
            InitializeComponent();
        }

        private void Frm_AreaPowerSupply_Load(object sender, EventArgs e)
        {
            initArea();
        }

        private void clearForm()
        {
            txtStartCode.Text = "";
            txtEndCode.Text = "";
            txtMulti.Text = "";
            txtOfferPower.Text = "";
        }
        private void initArea()
        {
            string strSql = "SELECT AreaNo,AreaName FROM AreaInfo order By AreaPosition";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbArea.Items.Add(dt.Rows[i]["AreaNo"]+" "+dt.Rows[i]["AreaName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxAmmeter.Items.Clear();
            clearForm();
            string AreaNo = cmbArea.Text.Split(' ')[0];
            string strSql = "SELECT AmmeterNo, AmmeterName FROM AreaAmmeterInfo WHERE AreaNo=" + AreaNo + "";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lbxAmmeter.Items.Add(dt.Rows[i]["AmmeterNo"] + " " + dt.Rows[i]["AmmeterName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lbxAmmeter_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearForm();
            showAmmeterInfo();
        }

        private void showAmmeterInfo()
        {
            string AreaNo = cmbArea.Text.Split(' ')[0];
            string AmmeterNo = lbxAmmeter.Text.Split(' ')[0];
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            string lastValue = "0";
            string thisValue = "0";
            string ammeterMutil = "";
            string offerPower = "0";
            DataTable dt = null;
            try
            {
                strSql = "SELECT TOP 1 AreaAmmeterValue FROM AreaAmmeterValue Where  AreaNo = '" + AreaNo + "'"
                        + " And AmmeterNo = '" + AmmeterNo + "' And CopyDate < '" + time + "'ORDER BY CopyDate DESC ";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    lastValue = dt.Rows[0][0].ToString();
                }

                strSql = "SELECT TOP 1 AreaAmmeterValue FROM AreaAmmeterValue Where  AreaNo = '" + AreaNo + "'"
                        + " And AmmeterNo = '" + AmmeterNo + "' And CopyDate = '" + time + "'ORDER BY CopyDate DESC ";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    thisValue = dt.Rows[0][0].ToString();
                }

                strSql = "Select AreaAmmeterMulti from AreaAmmeterinfo where AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    ammeterMutil = dt.Rows[0][0].ToString();
                }

                strSql = "Select OfferPower from AreaPower where AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And CountDate = '" + time + "'";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0)
                {
                    offerPower = dt.Rows[0][0].ToString();
                }

                txtStartCode.Text = lastValue;
                txtEndCode.Text = thisValue;
                txtMulti.Text = ammeterMutil;
                txtOfferPower.Text = offerPower;

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
            if (dateTimePicker.Value.Year != DateTime.Now.Year || dateTimePicker.Value.Month != DateTime.Now.Month)
            {
                MessageBox.Show("只能修改本月数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //saveFlag = false;
                return;
            }
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
            string AreaNo = cmbArea.Text.Split(' ')[0];
            string AmmeterNo = lbxAmmeter.Text.Split(' ')[0];
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
            txtOfferPower.Text = OfferPower.ToString().Split('.')[0];
            string strSql = "";
            string strSql1 = "";
            string strSql2 = "";
            string strSql3 = "";
            DataSet ds = null;
            //DataTable dt = null;
            try
            {
                strSql = "Select * From AreaAmmeterValue Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + time + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql1 = "Insert into AreaAmmeterValue values('" + AreaNo + "','" + AmmeterNo + "','" + thisValue + "','" + time + "')";
                }
                else
                {
                    strSql1 = "Update AreaAmmeterValue Set AreaAmmetervalue='" + thisValue + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + time + "'";
                }
                /*
                int year = dateTimePicker.Value.Year;
                int month = dateTimePicker.Value.Month - 1;
                string lastTime = year + "-" + month + "-01";
                strSql = "Select * From AreaAmmeterValue Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + lastTime + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql2 = "Insert into AreaAmmeterValue values('" + AreaNo + "','" + AmmeterNo + "','" + lastValue + "','" + lastTime + "')";
                }
                else
                {
                    strSql2 = "Update AreaAmmeterValue Set AreaAmmetervalue='" + lastValue + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + lastTime + "'";
                }
                */

                strSql = "Select * From AreaPower Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And CountDate = '" + time + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql3 = "Insert into AreaPower values('" + AreaNo + "','" + AmmeterNo + "','" + lastValue + "','" + thisValue + "','" + AmmeterMulti + "','" + OfferPower + "','" + time + "')";
                }
                else
                {
                    strSql3 = "Update AreaPower Set StartCode='" + lastValue + "',EndCode='" + thisValue + "',Multipile='" + AmmeterMulti + "',OfferPower='" + OfferPower + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And CountDate = '" + time + "'";
                }

                List<String> sqlList = new List<string>();
                sqlList.Add(strSql1);
                sqlList.Add(strSql2);
                sqlList.Add(strSql3);
                SQLUtl.ExecuteSqlTran(sqlList); //实务
                
            }
            catch(Exception ex)
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

        private void update()
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
            string AreaNo = cmbArea.Text.Split(' ')[0];
            string AmmeterNo = lbxAmmeter.Text.Split(' ')[0];
            float AmmeterMulti = float.Parse(txtMulti.Text.ToString());
            float lastValue = float.Parse(txtStartCode.Text.ToString());
            float thisValue = float.Parse(txtEndCode.Text.ToString());
            if (thisValue < lastValue)
            {
                if (MessageBox.Show("确定止码小于起码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
            float OfferPower = (thisValue - lastValue) * AmmeterMulti;
            //止码小于起码处理
            if (thisValue < lastValue)
            {
                if (thisValue < 9999)
                {
                    OfferPower = (10000 - lastValue + thisValue) * AmmeterMulti;
                }
                else if (thisValue < 99999)
                {
                    OfferPower = (100000 - lastValue + thisValue) * AmmeterMulti;
                }
                else if (thisValue < 999999)
                {
                    OfferPower = (1000000 - lastValue + thisValue) * AmmeterMulti;
                }

            }
            txtOfferPower.Text = OfferPower.ToString().Split('.')[0];
            string strSql = "";
            string strSql1 = "";
            string strSql2 = "";
            string strSql3 = "";
            DataSet ds = null;
            //DataTable dt = null;
            try
            {
                strSql = "Select * From AreaAmmeterValue Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + time + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql1 = "Insert into AreaAmmeterValue values('" + AreaNo + "','" + AmmeterNo + "','" + thisValue + "','" + time + "')";
                }
                else
                {
                    strSql1 = "Update AreaAmmeterValue Set AreaAmmetervalue='" + thisValue + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + time + "'";
                }

                int year = dateTimePicker.Value.Year;
                int month = dateTimePicker.Value.Month - 1;
                string lastTime = year + "-" + month + "-01";
                strSql = "Select * From AreaAmmeterValue Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + lastTime + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql2 = "Insert into AreaAmmeterValue values('" + AreaNo + "','" + AmmeterNo + "','" + lastValue + "','" + lastTime + "')";
                }
                else
                {
                    strSql2 = "Update AreaAmmeterValue Set AreaAmmetervalue='" + lastValue + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And copyDate = '" + lastTime + "'";
                }


                strSql = "Select * From AreaPower Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And CountDate = '" + time + "'";
                ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    strSql3 = "Insert into AreaPower values('" + AreaNo + "','" + AmmeterNo + "','" + lastValue + "','" + thisValue + "','" + AmmeterMulti + "','" + OfferPower + "','" + time + "')";
                }
                else
                {
                    strSql3 = "Update AreaPower Set StartCode='" + lastValue + "',EndCode='" + thisValue + "',Multipile='" + AmmeterMulti + "',OfferPower='" + OfferPower + "' "
                            + " Where  AreaNo = '" + AreaNo + "' And AmmeterNo = '" + AmmeterNo + "' And CountDate = '" + time + "'";
                }

                List<String> sqlList = new List<string>();
                sqlList.Add(strSql1);
                sqlList.Add(strSql2);
                sqlList.Add(strSql3);
                SQLUtl.ExecuteSqlTran(sqlList); //实务

            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message.ToString());
            }

            if (!flag)
            { MessageBox.Show("数据更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

    }
}
