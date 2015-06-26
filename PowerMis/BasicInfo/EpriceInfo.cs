using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using System.Text.RegularExpressions;//引用正则表达式类的命名空间

namespace PowerMis.BasicInfo
{
    public partial class EpriceInfo : Form
    {
        public EpriceInfo()
        {
            InitializeComponent();
        }

        private void EpriceInfo_Load(object sender, EventArgs e)
        {
            fillListBox();
            fillLadderPrice();
        }

        private void fillListBox()      //填充列表框
        {
            listPriceNo.Items.Clear();
            try
            {
                string strSQL = "SELECT PowerPriceNo,PowerPriceName FROM PowerPriceInfo";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    listPriceNo.Items.Add(dataTable.Rows[i][0] +" "+ dataTable.Rows[i][1]);
                    
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void fillLadderPrice()      //填充阶梯电价信息
        {
            try
            {
                string strSQL = "SELECT * FROM LadderPrice";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                txtPowerValue1.Text = dataTable.Rows[0][0].ToString();
                txtPowerPrice1.Text = dataTable.Rows[0][1].ToString();
                txtPowerValue2.Text = dataTable.Rows[0][2].ToString();
                txtPowerPrice2.Text = dataTable.Rows[0][3].ToString();
                txtPowerValue3.Text = dataTable.Rows[0][2].ToString();
                txtPowerPrice3.Text = dataTable.Rows[0][4].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void listPriceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string priceNo = listPriceNo.Text.Split(' ').GetValue(0).ToString();
            string strSql = "SELECT * FROM PowerPriceInfo WHERE PowerPriceNo= '"+ priceNo +"' ";
            try
            {
                DataSet dataSet = SQLUtl.Query(strSql);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                page2TxtPriceName.Text = dataTable.Rows[0][1].ToString();
                page2TxtAbbreviation.Text = dataTable.Rows[0][2].ToString();
                page2TxtPrice.Text = dataTable.Rows[0][3].ToString();
                page2TxtCountryAnnex.Text = dataTable.Rows[0][6].ToString();
                page2TxtRepNo.Text = dataTable.Rows[0][7].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPowerValue2_TextChanged(object sender, EventArgs e)
        {
            txtPowerValue3.Text = txtPowerValue2.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtPowerValue1.Text == "" || txtPowerPrice1.Text == "" || txtPowerValue2.Text == "" 
                                            || txtPowerPrice2.Text == "" || txtPowerPrice3.Text =="")
            {
                MessageBox.Show("信息不完整，不允许有空值！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(txtPowerValue1.Text.Trim(), "^[1-9][0-9]*$") || !Regex.IsMatch(txtPowerValue2.Text.Trim(), "^[1-9][0-9]*$"))
            {
                MessageBox.Show("所有电量值必须为数字！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(txtPowerPrice1.Text.Trim(), "^[0-9]+[.]?[0-9]*$") || !Regex.IsMatch(txtPowerPrice2.Text.Trim(), "^[0-9]+[.]?[0-9]*$") | !Regex.IsMatch(txtPowerPrice3.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("所有电价必须为数字！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                String strSQLUpdate = "update LadderPrice set PowerValue1='" + txtPowerValue1.Text.Trim() + "', "
                                                             +" PowerPrice1='" + txtPowerPrice1.Text.Trim() + "',"
                                                             +" PowerValue2='" + txtPowerValue2.Text.Trim() + "',"
                                                             +" PowerPrice2='" + txtPowerPrice2.Text.Trim() + "',"
                                                             +" PowerPrice3='" + txtPowerPrice3.Text.Trim() + "'";
                int count = SQLUtl.ExecuteSql(strSQLUpdate);
                if (count != 0)
                {
                    MessageBox.Show("修改数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (page1TxtPriceNo.Text == "")
            {
                MessageBox.Show("电价代码不允许为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page1TxtPriceNo.Text.Trim(), "^[1-9][0-9]*$"))
            {
                MessageBox.Show("电价代码必须是大于1的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page1TxtPrice.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("电价必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page1TxtCountryAnnex.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("城镇附加费必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (page1TxtRepNo.Text == "")
            {
                MessageBox.Show("报表顺序不允许为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page1TxtRepNo.Text.Trim(), "^[0-9]*$"))
            {
                MessageBox.Show("报表顺序必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                //判断电价类别是否已存在
                string strSql = "SELECT * FROM PowerPriceInfo WHERE PowerPriceNo = '" + page1TxtPriceNo.Text.Trim() + "'";
                DataSet ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("该电价代码已经存在，请更换电价代码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    page1TxtPriceNo.Focus();
                    return;
                }
                string strSQLInsert = "insert into PowerPriceInfo values ('" + page1TxtPriceNo.Text.Trim() + "',"
                                                                    + " '" + page1TxtPriceName.Text.Trim() + "',"
                                                                    + " '" + page1TxtAbbreviation.Text.Trim() + "',"
                                                                    + " '" + page1TxtPrice.Text.Trim() + "',null,GETDATE(),"
                                                                    + " '" + page1TxtCountryAnnex.Text.Trim() +"' ,"
                                                                    + " '" + page1TxtRepNo.Text.Trim() + "')";
                int count = SQLUtl.ExecuteSql(strSQLInsert);
                if (count != 0)
                {
                    fillListBox();
                    MessageBox.Show("添加数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(page2TxtPrice.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("电价必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page2TxtCountryAnnex.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("城镇附加费必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (page2TxtRepNo.Text == "")
            {
                MessageBox.Show("报表顺序不允许为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(page2TxtRepNo.Text.Trim(), "^[0-9]*$"))
            {
                MessageBox.Show("报表顺序必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string priceNo = listPriceNo.Text.Split(' ').GetValue(0).ToString();
                string strSQLUpdate = "UPDATE PowerPriceInfo SET PowerPriceName='"+ page2TxtPriceName.Text.Trim() +"',"
                                                             +" Abbreviation='" + page2TxtAbbreviation.Text.Trim() + "',"  
                                                             +" PowerPrice='" + page2TxtPrice.Text.Trim() + "',"
                                                             +" PowerPriceDate= GETDATE(),"
                                                             +" CountryAnnex='" + page2TxtCountryAnnex.Text.Trim() + "',"
                                                             +" ReportOrder='" + page2TxtRepNo.Text.Trim() + "'"
                                                             +" WHERE PowerPriceNo='" + priceNo + "'";
                int count = SQLUtl.ExecuteSql(strSQLUpdate);
                if (count != 0)
                {
                    MessageBox.Show("修改数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillListBox();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该电价信息吗？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    string priceNo = listPriceNo.Text.Split(' ').GetValue(0).ToString();
                    string strSql = "DELETE FROM PowerPriceInfo WHERE PowerPriceNo = '" + priceNo + "'";
                    int count = SQLUtl.ExecuteSql(strSql);
                    if (count != 0)
                    {                 
                        MessageBox.Show("删除数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillListBox();
                        page2TxtPriceName.Clear();
                        page2TxtAbbreviation.Clear();
                        page2TxtPrice.Clear();
                        page2TxtCountryAnnex.Clear();
                        page2TxtRepNo.Clear();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
