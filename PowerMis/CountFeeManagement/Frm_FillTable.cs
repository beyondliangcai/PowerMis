using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Common;
using DBUtility;
using BusinessModel;

namespace PowerMis.CountFeeManagement
{
    public partial class Frm_FillTable : Form
    {
        private Customer customer = null;
        
        private float LastMonthStart = 0;       //上月止码
        private double AbnormityAmount = 0;      //异动电量
        private int TransformerVolume = 0;      //变压器额定容量
        private double AmmeterMulti = 0;                //电表倍率
        private double OldAmmeterMulti = 1;             ////电表倍率
        private double LineLoseRate = 0;                //线损率
        private int TransformerNo = 0;                  //变压器号
        private double DiscountRate = 0;                //折扣
        private double averPrice = 0;
        private int[] PowerPriceNo = null;
        private double[] PowerPrice = null;
        private float[] PowerPriceRate = null;
        private int PriceCount = 0;             //记录客户使用电价种数

        public Frm_FillTable()
        {
            InitializeComponent();
            customer = new Customer();
            PowerPriceNo = new int[4];
            PowerPrice = new double[4];
            PowerPriceRate = new float[4];
        }

        private void Frm_FillTable_Load(object sender, EventArgs e)
        {
            fillBookNo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearForm() 
        {
            txtPowerValue.Text = "";
            txtMultiple.Text = "";
            txtLineLoss.Text = "";
            txtChangeLoss.Text = "";
            txtCountPowerValue.Text = "";
            txtRuralPrice.Text = "";
            txtCityPrice.Text = "";
            txtBasePrice.Text = "";
            txtTotalPrice.Text = "";
            txtAbnormityAmount.Text = "";
            txtPowerPrice1.Text = "";
            txtPowerRate1.Text = "";
            txtPowerPrice2.Text = "";
            txtPowerRate2.Text = "";
            txtPowerPrice3.Text = "";
            txtPowerRate3.Text = "";
            txtPowerPrice4.Text = "";
            txtPowerRate4.Text = "";
            txtThisMonthValue.Text = "";
        }

        private void fillBookNo()
        {
            try
            {
                string strSQL = "SELECT DISTINCT Left(CustomerNo, 5) AS BookNo FROM CustomerInfo ORDER BY BookNo";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    cbxBookNo.Items.Add(dataTable.Rows[i][0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void init_SelectedCustomerInfo(string customerNo) 
        {
            clearForm();
            float lastMonthValue = 0;
            float thisMonthValue = 0;
            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            string time = str[0] +"-"+ str[1] + "-01";
            //DateTime TimeNow = Convert.ToDateTime(time);
            string strSql1 = "SELECT CustomerName,AmmeterValue FROM CustomerInfo,AmmeterValue"
                            + " WHERE CustomerInfo.CustomerNo='" + customerNo + "' AND CopyValueDate < '" + time + "'"
                            + "AND CustomerInfo.CustomerNo=AmmeterValue.CustomerNo ORDER BY CopyValueDate DESC";
            try
            {
                DataSet dataSet1 = SQLUtl.Query(strSql1);
                // 将Dataset里的datatable取出来返回
                DataTable dataTable1 = dataSet1.Tables["dataSet"];
                if (dataTable1.Rows.Count == 0)
                {
                    string strSql2 = "SELECT CustomerName FROM CustomerInfo WHERE CustomerNo='" + customerNo + "'";
                    DataSet dataSet2 = SQLUtl.Query(strSql2);
                    DataTable dataTable2 = dataSet2.Tables["dataSet"];
                    customer.setCustomerName(dataTable2.Rows[0][0].ToString());
                }
                else 
                {
                    customer.setCustomerName(dataTable1.Rows[0][0].ToString());
                    lastMonthValue = float.Parse(dataTable1.Rows[0][1].ToString());
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"01");
            }

            string strSql3 = "SELECT AmmeterValue FROM CustomerInfo,AmmeterValue"
                            + " WHERE CustomerInfo.CustomerNo='" + customerNo + "' AND CopyValueDate = '" + time + "'"
                            + " AND CustomerInfo.CustomerNo=AmmeterValue.CustomerNo ORDER BY CopyValueDate DESC";
            try
            {
                DataSet dataSet3 = SQLUtl.Query(strSql3);
                // 将Dataset里的datatable取出来返回
                DataTable dataTable3 = dataSet3.Tables["dataSet"];
                if (dataTable3.Rows.Count != 0)
                {
                    thisMonthValue = float.Parse(dataTable3.Rows[0][0].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            txtCustomerNo.Text = customerNo;
            txtCustomerName.Text = customer.getCustomerName();
            txtLastMonthValue.Text = lastMonthValue.ToString();
            LastMonthStart = lastMonthValue ;
            if (thisMonthValue != 0)
            {
                txtThisMonthValue.Text = thisMonthValue.ToString();
            }

            string strSql4 = "SELECT * FROM CountFee WHERE CustomerNo='" + customerNo + "' AND CountFeeDate='" + time + "'and NegativeInvoiceFlag=0";
            try
            {
                DataSet dataSet4 = SQLUtl.Query(strSql4);
                if (dataSet4.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet4.Tables[0].Rows[0];
                    float powervalue = float.Parse(dataRow["MonthCopyAmount"].ToString());
                    txtPowerValue.Text = String.Format("{0:###0}", powervalue);
                    txtMultiple.Text = dataRow["AmmeterMulti"].ToString();
                    float lineloss = float.Parse(dataRow["LineLose"].ToString());
                    txtLineLoss.Text = String.Format("{0:###0}", lineloss);
                    float transLoss = float.Parse(dataRow["transformerlose"].ToString());
                    txtChangeLoss.Text = String.Format("{0:###0}", transLoss);
                    txtCountPowerValue.Text = dataRow["CountfeeAmount"].ToString();
                    float CountryAnnex = float.Parse(dataRow["CountryAnnex"].ToString());
                    txtRuralPrice.Text = String.Format("{0:###0.00}", CountryAnnex);
                    //txtRuralPrice.Text = dataRow["CountryAnnex"].ToString();
                    float CityAnnex = float.Parse(dataRow["CityAnnex"].ToString());
                    txtCityPrice.Text = String.Format("{0:###0.00}", CityAnnex);
                    float basePrice = float.Parse(dataRow["BaseFeeMoney"].ToString());
                    txtBasePrice.Text = String.Format("{0:###0.00}", basePrice);
                    //txtCityPrice.Text = dataRow["CityAnnex"].ToString();
                    float TotalMoney = float.Parse(dataRow["TotalMoney"].ToString());
                    txtTotalPrice.Text = String.Format("{0:###0.00}", TotalMoney);
                    //txtTotalPrice.Text = dataRow["TotalMoney"].ToString();
                    if (int.Parse(dataRow["PriceRate1"].ToString()) > 0)
                    {
                        txtPowerPrice1.Text = dataRow["PowerPrice1"].ToString();
                        txtPowerRate1.Text = dataRow["PriceRate1"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate2"].ToString()) > 0)
                    {
                        txtPowerPrice2.Text = dataRow["PowerPrice2"].ToString();
                        txtPowerRate2.Text = dataRow["PriceRate2"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate3"].ToString()) > 0)
                    {
                        txtPowerPrice3.Text = dataRow["PowerPrice3"].ToString();
                        txtPowerRate3.Text = dataRow["PriceRate3"].ToString();
                    }
                    if (int.Parse(dataRow["PriceRate4"].ToString()) > 0)
                    {
                        txtPowerPrice4.Text = dataRow["PowerPrice4"].ToString();
                        txtPowerRate4.Text = dataRow["PriceRate4"].ToString();
                    }
                    if (dataRow["AbnormityAmount"].ToString() != "" && dataRow["AbnormityAmount"].ToString() != "0")
                    {
                        txtAbnormityAmount.Text = dataRow["AbnormityAmount"].ToString();
                        txtLastMonthValue.Text = dataRow["LastMonthStart"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbxBookNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string No = "";
            lbxCustomerList.Items.Clear();
            try
            {
                string strSQL = "SELECT CustomerNo,CustomerName,CustomerPosition FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' order by CustomerPosition asc";

                DataSet dataSet = SQLUtl.Query(strSQL);

                // 将Dataset里的datatable取出来返回
                DataTable dataTable = dataSet.Tables["dataSet"];
                int k = 1;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (k < 10)
                    {
                        No = "00" + k;
                    }
                    else if (k >= 10 && k < 100)
                    {
                        No = "0" + k;
                    }
                    else if (k >= 100 & k < 1000)
                    {
                        No = "" + k;
                    }
                    lbxCustomerList.Items.Add(No + "   " + dataTable.Rows[i][0] + " " + dataTable.Rows[i][1]);
                    k++;
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lbxCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerNo = "";
            if(lbxCustomerList.Items.Count != 0)
            {
                customerNo = lbxCustomerList.Text.Split(' ').GetValue(3).ToString();
            }
            txtCustomerNoKey.Text = customerNo;
            init_SelectedCustomerInfo(customerNo);
        }

        private void txtCustomerNoKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cbxBookNo.Text = txtCustomerNoKey.Text.Substring(0,5);
                string no = txtCustomerNoKey.Text.Trim();
                //int i = lbxCustomerList.FindString(txtCustomerNoKey.Text);
                string strSQL = "select Count(*) FROM CustomerInfo WHERE Left(CustomerNo,5) = '" + cbxBookNo.Text.Trim() + "' ";
                DataSet dataSet = SQLUtl.Query(strSQL);
                int k = (int)dataSet.Tables[0].Rows[0][0];
                for (int i = 0; i <k; i++)
                {
                    
                    lbxCustomerList.SelectedIndex = i;
                    if (lbxCustomerList.SelectedItem.ToString().Contains(no))
                    {
                        lbxCustomerList.TopIndex = i;
                        break;
                    }
                    if (i+1 == k)
                    {
                        lbxCustomerList.SelectedIndex = 0;
                        MessageBox.Show("没有找到该编号的客户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    
                }
            }
        }

        private void dtpBirthday_ValueChanged_1(object sender, EventArgs e)
        {
            if (txtCustomerNoKey.Text != "")
            {
                init_SelectedCustomerInfo(txtCustomerNoKey.Text);
            }
        }

        private void cbxBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string bookNo = cbxBookNo.Text;
                int i = cbxBookNo.FindString(bookNo);
                cbxBookNo.SelectedIndex = i;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (lbxCustomerList.SelectedIndex > 0)
            {
                lbxCustomerList.SelectedIndex = lbxCustomerList.SelectedIndex - 1;
            }
            else
            {
                MessageBox.Show("没有上一条了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbxCustomerList.SelectedIndex < lbxCustomerList.Items.Count-1)
            {
                lbxCustomerList.SelectedIndex = lbxCustomerList.SelectedIndex + 1;
            }
            else
            {
                MessageBox.Show("已经是最后一条了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //由月阶梯计费改为年阶梯计费，保持2015-4月以前的收费方式  
        private double calcPowerPrice(string customerNo, int countPowerValue) //计算电费
        {
           // MessageBox.Show("test");
            double PowerFee = 0;
            int powerValue1 = 0;
            double powerPrice1 = 0;
            int powerValue2 = 0;
            double powerPrice2 = 0;
            double powerPrice3 = 0;
            double[] powerFee = new double[4];
            double compairPowerValue1 = 0;
            double compairPowerValue2 = 0;

            //计算今年阶梯电价用的度数
           // double facCountPowerValue = 0;

            string strSql1 = "SELECT * FROM LadderPrice";
            try
            {
                DataSet dataSet1 = SQLUtl.Query(strSql1);
                if (dataSet1.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet1.Tables[0].Rows[0];
                    powerValue1 = int.Parse(dataRow["PowerValue1"].ToString());
                    powerPrice1 = double.Parse(dataRow["PowerPrice1"].ToString());
                    powerValue2 = int.Parse(dataRow["PowerValue2"].ToString());
                    powerPrice2 = double.Parse(dataRow["PowerPrice2"].ToString());
                    powerPrice3 = double.Parse(dataRow["PowerPrice3"].ToString());
                }
                compairPowerValue1 = powerPrice2 - powerPrice1;
                compairPowerValue2 = powerPrice3 - powerPrice1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            //统计当前日期之前本年用的度数综合，因为电表可能会坏，所以不能用当前电表的值减最前一个月的值

          
            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');          

            string time = str[0] + "-" + str[1] + "-01";
            string fristmonth ="";

            //2015从4月开始使用阶梯计费,其他年从1月开始按年计费，注意不能修改2015年以前的电价，收费方式变了
           
            //小于2015按以前方式收费
            if (int.Parse(str[0]) < 2015 || (int.Parse(str[0]) == 2015 && int.Parse(str[1])<4))
            {

                 string strSql4 = "SELECT PriceRate.PowerPriceNo as PowerPriceNo,PowerPriceInfo.PowerPriceName as PowerPriceName,"
                          + "PriceRate.PriceRate as PriceRate,PowerPriceInfo.PowerPrice as PowerPrice FROM PriceRate,PowerPriceInfo"
                          + " WHERE CustomerNo = '" + customerNo + "' AND PriceRate.PowerPriceNo = PowerPriceInfo.PowerPriceNo";
                 try
                 {
                     DataSet dataSet2 = SQLUtl.Query(strSql4);
                     DataTable dataTable2 = dataSet2.Tables["dataSet"];
                     if (dataTable2.Rows.Count == 0)
                     {
                         PowerFee = 0.52 * countPowerValue;
                     }
                     /* if (dataTable2.Rows.Count == 1)
                      {
                          int powerPriceNo = int.Parse(dataTable2.Rows[0]["PowerPriceNo"].ToString());
                          double powerPrice = double.Parse(dataTable2.Rows[0]["PowerPrice"].ToString());
                          if (powerPriceNo == 1 || powerPriceNo == 9)
                          {
                              if (countPowerValue < powerValue1)
                              {
                                  PowerFee = countPowerValue * powerPrice;
                              }
                              if (powerValue1 <= countPowerValue && countPowerValue < powerValue2)
                              {
                                  PowerFee = powerValue1 * powerPrice + (countPowerValue - powerValue1) * (powerPrice + powerPrice2);
                              }

                              if (countPowerValue >= powerValue2)
                              {
                                  PowerFee = powerValue1 * powerPrice + (powerValue2 - powerValue1) * (powerPrice + powerPrice2) + (countPowerValue - powerValue2) * (powerPrice + powerPrice3);
                              }

                          }
                          else 
                          {
                              PowerFee = powerPrice * countPowerValue;
                          }
                      }*/
                     else
                     {
                         PriceCount = dataTable2.Rows.Count;
                         powerValue1=180;
                         powerValue2 = 400;
                         powerPrice2 = 0.05;
                         powerPrice3 = 0.3;
                         for (int i = 0; i < dataTable2.Rows.Count; i++)
                         {
                             PowerPriceNo[i] = int.Parse(dataTable2.Rows[i]["PowerPriceNo"].ToString());
                             PowerPriceRate[i] = float.Parse(dataTable2.Rows[i]["PriceRate"].ToString());
                             PowerPrice[i] = double.Parse(dataTable2.Rows[i]["PowerPrice"].ToString());
                             powerFee[i] = countPowerValue * PowerPrice[i] * PowerPriceRate[i] / 100;

                             if (PowerPriceNo[i] == 1 || PowerPriceNo[i] == 9)
                             {
                                 float compairPowerValue = (float)(countPowerValue * PowerPriceRate[i] / 100);
                                 if (powerValue1 <= compairPowerValue && compairPowerValue < powerValue2)
                                 {
                                     powerFee[i] = powerValue1 * PowerPrice[i] + (compairPowerValue - powerValue1) * (PowerPrice[i] + powerPrice2);
                                 }

                                 if (compairPowerValue >= powerValue2)
                                 {
                                     powerFee[i] = powerValue1 * PowerPrice[i] + (powerValue2 - powerValue1) * (PowerPrice[i] + powerPrice2) + (compairPowerValue - powerValue2) * (PowerPrice[i] + powerPrice3);
                                 }

                             }

                         }
                         for (int i = 0; i < 4; i++)
                         {
                             PowerFee += powerFee[i];
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");

                 }

            return PowerFee ;   


            }
               
             //2015年从4月开始收费
            else if (int.Parse(str[0]) == 2015 && int.Parse(str[1]) >= 4)
            {
             //   MessageBox.Show(str[0]);
              fristmonth = str[0] + "-04-01";
            }
           
            else if (int.Parse(str[0]) > 2015)
            {
             fristmonth = str[0] + "-01-01";            
            }
            string strSql3 = "select * from CountFee where CustomerNo='" + customerNo + "'and CountFeeDate <'" + time + "'and CountFeeDate>='" + fristmonth + "'";
            double lasttotalprice = 0, totalprice = 0;

            //有比例用户的阶梯度数有可能是小数
            double lasttotalcount = 0, totalcount = 0;
            try
            {
                DataSet dataSet3 = SQLUtl.Query(strSql3);
                DataTable dataTable3 = dataSet3.Tables["dataSet"];
                if (dataTable3.Rows.Count == 0)
                {
                    lasttotalcount = 0;
                }
                else
                {
                    //考虑到多种电费的情况,而且不可能出现居民生活电价和农村生活电价（2种阶梯电价）同时出现的情况

                    
                        for (int j = 0; j < dataTable3.Rows.Count; j++)
                        {
                            if (int.Parse(dataTable3.Rows[j]["PowerPriceNo1"].ToString()) == 1 || int.Parse(dataTable3.Rows[j]["PowerPriceNo1"].ToString()) == 9)
                            {
                            lasttotalcount = lasttotalcount + (double.Parse(dataTable3.Rows[j]["CountFeeAmount"].ToString()) * double.Parse(dataTable3.Rows[j]["PriceRate1"].ToString()))/100;
                         //   facCountPowerValue = countPowerValue * float.Parse(dataTable3.Rows[j]["PriceRate1"].ToString()) / 100;
                             }
                            if (int.Parse(dataTable3.Rows[j]["PowerPriceNo2"].ToString()) == 1 || int.Parse(dataTable3.Rows[j]["PowerPriceNo2"].ToString()) == 9)
                            {

                                lasttotalcount = lasttotalcount + double.Parse(dataTable3.Rows[j]["CountFeeAmount"].ToString()) * double.Parse(dataTable3.Rows[j]["PriceRate2"].ToString()) / 100;

                         //           facCountPowerValue = countPowerValue * float.Parse(dataTable3.Rows[j]["PriceRate2"].ToString()) / 100;

                                
                            }
                        }
                   
                }
               
                //如果今年开了负数发票，这里lasttotalcount还需要减去负数发票的度数
                string negativeSql = "select * from NegativeInvoice where CustomerNo='" + customerNo + "'and NegativeDate <'" + time + "'and NegativeDate>='" + fristmonth + "'";
                DataSet negativedataSet = SQLUtl.Query(negativeSql);
                DataTable negativedataTable = negativedataSet.Tables["dataSet"];
              
                string negativeDate ;
                string countfeeNegativeDateSql;
                DataSet countFeeNegativedataSet;
                DataTable countFeeNegativedataTable;
                //根据负数发票里的时间和用户名在countfee表里找到相应的数据

                if(negativedataTable.Rows.Count>0){//如果今年存在负数发票
                    for (int k = 0; k < negativedataTable.Rows.Count;k++ )
                    {
                        negativeDate = negativedataTable.Rows[k]["NegativeDate"].ToString();
                       // MessageBox.Show("negativeDate" + negativeDate);

                        //查找该用户在countfee表里的PriceRate
                        countfeeNegativeDateSql = "select * from countFee where CustomerNo='" + customerNo + "'and countfeedate='" + negativeDate + "'";
                        countFeeNegativedataSet = SQLUtl.Query(countfeeNegativeDateSql);
                        countFeeNegativedataTable=countFeeNegativedataSet.Tables["dataset"];
                        //MessageBox.Show(countFeeNegativedataTable.Rows.Count.ToString());
                       // MessageBox.Show("powerpriceno1" + countFeeNegativedataTable.Rows[0]["PowerPriceNo1"].ToString() + "powerpricerate1:" + countFeeNegativedataTable.Rows[0]["PriceRate1"].ToString());
                        //MessageBox.Show("powerpriceno2" + countFeeNegativedataTable.Rows[0]["PowerPriceNo2"].ToString() + "powerpricerate2:" + countFeeNegativedataTable.Rows[0]["PriceRate2"].ToString());
                        

                        if (int.Parse(countFeeNegativedataTable.Rows[0]["PowerPriceNo1"].ToString()) == 1 || int.Parse(countFeeNegativedataTable.Rows[0]["PowerPriceNo1"].ToString()) == 9)
                        {
                            lasttotalcount = lasttotalcount + (double.Parse(negativedataTable.Rows[k]["NegativeValue"].ToString()) * double.Parse(countFeeNegativedataTable.Rows[0]["PriceRate1"].ToString())) / 100;
                     //       MessageBox.Show("negativedataTable NegativeValue" + negativedataTable.Rows[k]["NegativeValue"].ToString());
                        }
                        if (int.Parse(countFeeNegativedataTable.Rows[0]["PowerPriceNo2"].ToString()) == 1 || int.Parse(countFeeNegativedataTable.Rows[0]["PowerPriceNo2"].ToString()) == 9)
                        {

                            lasttotalcount = lasttotalcount + double.Parse(negativedataTable.Rows[k]["NegativeValue"].ToString()) * double.Parse(countFeeNegativedataTable.Rows[0]["PriceRate2"].ToString()) / 100;

                        }
                    }
                }

              //  MessageBox.Show("lasttotalcount" + lasttotalcount);

                //MessageBox.Show("lasttotalcount" + lasttotalcount);

              //  totalcount = lasttotalcount + countPowerValue;
               // MessageBox.Show("totalcount" + totalcount);
              //  MessageBox.Show(lasttotalcount.ToString() + totalcount.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");

            }


            string strSql2 = "SELECT PriceRate.PowerPriceNo as PowerPriceNo,PowerPriceInfo.PowerPriceName as PowerPriceName,"
                          + "PriceRate.PriceRate as PriceRate,PowerPriceInfo.PowerPrice as PowerPrice FROM PriceRate,PowerPriceInfo"
                          + " WHERE CustomerNo = '" + customerNo + "' AND PriceRate.PowerPriceNo = PowerPriceInfo.PowerPriceNo";
            try
            {
                DataSet dataSet2 = SQLUtl.Query(strSql2);
                DataTable dataTable2 = dataSet2.Tables["dataSet"];

                //如果这个用户没有PowerPriceNo按0.52收费
                if (dataTable2.Rows.Count == 0)
                {
                    PowerFee = 0.52 * countPowerValue;
                }

                else
                {
                    // MessageBox.Show("11111111111");
                    PriceCount = dataTable2.Rows.Count;
                    //  MessageBox.Show("pricecount" + PriceCount.ToString());
                    for (int i = 0; i < dataTable2.Rows.Count; i++)
                    {
                        PowerPriceNo[i] = int.Parse(dataTable2.Rows[i]["PowerPriceNo"].ToString());
                        PowerPriceRate[i] = float.Parse(dataTable2.Rows[i]["PriceRate"].ToString());
                        PowerPrice[i] = double.Parse(dataTable2.Rows[i]["PowerPrice"].ToString());
                        powerFee[i] = countPowerValue * PowerPrice[i] * PowerPriceRate[i] / 100;
                        // MessageBox.Show("PowerPriceNo", PowerPriceNo[i].ToString());

                        //居民生活电价和农村居民照明按阶梯电价收费，其他的按0.52收费
                        //如果是城网用户
                        if (PowerPriceNo[i] == 1 )
                        {
                            totalcount = lasttotalcount + ((double)(countPowerValue * (int)PowerPriceRate[i]))/100;
                       //     MessageBox.Show("totalcount" + totalcount);
                            lasttotalprice = 0;
                            totalprice = 0;
                            //上个月电费总和
                            if (lasttotalcount <= powerValue1)
                            {
                                lasttotalprice = lasttotalcount * powerPrice1 ;

                            }
                            else if (powerValue1 < lasttotalcount && lasttotalcount <= powerValue2)
                            {
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue1) * powerPrice2 ;
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1 ;
                            }
                            else if (lasttotalcount > powerValue2)
                            {
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1;
                                lasttotalprice = lasttotalprice + (powerValue2 - powerValue1) * powerPrice2 ;
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue2) * powerPrice3;
                            }
                        //    MessageBox.Show("lasttotalprice:" + lasttotalprice);

                            if (totalcount <= powerValue1)
                            {
                                totalprice = totalcount * powerPrice1 ;

                            }
                            else if (totalcount > powerValue1 && totalcount <= powerValue2)
                            {
                                totalprice = (totalcount - powerValue1) * powerPrice2 ;
                                totalprice = totalprice + powerValue1 * powerPrice1 ;
                            }
                            else if (totalcount > powerValue2)
                            {
                                totalprice = totalprice + powerValue1 * powerPrice1 ;
                                totalprice = totalprice + (powerValue2 - powerValue1) * powerPrice2 ;
                                totalprice = totalprice + (totalcount - powerValue2) * powerPrice3 ;
                            }
                       //     MessageBox.Show("totalprice:" + totalprice);
                            powerFee[i] = totalprice - lasttotalprice;

                        }

                        //如果是农网用户，价格和城网不一样
                        else  if ( PowerPriceNo[i] == 9)
                        {
                            totalcount = lasttotalcount +((double) (countPowerValue * (int)PowerPriceRate[i])) / 100;
                  //          MessageBox.Show("totalcount" + totalcount);
                          // MessageBox.Show("rural");
                            try
                            {

                             //   MessageBox.Show(PowerPrice[i].ToString());
                          //      string ruralSql = "select * from PowerPriceInfo where PowerPriceNo =9 ";
                           //     DataSet ruralDataSet = SQLUtl.Query(ruralSql);
                           //     DataTable ruralDataTable = ruralDataSet.Tables["dataSet"];
                           //     if (ruralDataTable.Rows.Count == 0)
                           //     {
                           //         powerPrice1 = 0;
                          //      }
                         //       else
                         //       {
                                 powerPrice1 = PowerPrice[i];
                         //       }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");
                            }
                            lasttotalprice = 0;
                            totalprice = 0;

                            powerPrice2 = powerPrice1+compairPowerValue1;
                            powerPrice3 = powerPrice1+compairPowerValue2;
                          //  MessageBox.Show("powerPrice2" + powerPrice2);
                          //  MessageBox.Show("powerPrice3" + powerPrice3);

                            //上个月电费总和
                            if (lasttotalcount <= powerValue1)
                            {
                                lasttotalprice = lasttotalcount * powerPrice1;

                            }
                            else if (powerValue1 < lasttotalcount && lasttotalcount <= powerValue2)
                            {
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue1) * powerPrice2 ;
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1 ;
                            }
                            else if (lasttotalcount > powerValue2)
                            {
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1 ;
                                lasttotalprice = lasttotalprice + (powerValue2 - powerValue1) * powerPrice2 ;
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue2) * powerPrice3 ;
                            }
                       //      MessageBox.Show("lasttotalprice:" + lasttotalprice);

                            if (totalcount <= powerValue1)
                            {
                                totalprice = totalcount * powerPrice1 ;

                            }
                            else if (totalcount > powerValue1 && totalcount <= powerValue2)
                            {
                                totalprice = (totalcount - powerValue1) * powerPrice2 ;
                                totalprice = totalprice + powerValue1 * powerPrice1 ;
                            }
                            else if (totalcount > powerValue2)
                            {
                                totalprice = totalprice + powerValue1 * powerPrice1;
                                totalprice = totalprice + (powerValue2 - powerValue1) * powerPrice2 ;
                                totalprice = totalprice + (totalcount - powerValue2) * powerPrice3 ;
                            }
                   //            MessageBox.Show("totalprice:" + totalprice);
                            powerFee[i] = totalprice - lasttotalprice;

                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        PowerFee += powerFee[i];
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");

            }

            return PowerFee;
        }
        private double calcCityAddValue(string customerNo, int countPowerValue)     //计算城网附加费
        {
            double cityAddValue = 0;
            string ElectriCharacterName = "";
            string strSql1 = "SELECT ElectriCharacterName FROM CustomerInfo WHERE CustomerNo='"+customerNo+"'";
            try
            {
                DataSet ds1 = SQLUtl.Query(strSql1);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    ElectriCharacterName = ds1.Tables[0].Rows[0][0].ToString();
                    customer.setElectriCharacterName(ElectriCharacterName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcCityAddValue1");
            }

            if (ElectriCharacterName.Equals("城网"))
            {
                string strSql2 = "SELECT PriceRate, CountryAnnex FROM PriceRate, PowerPriceInfo"
                                + " WHERE PriceRate.CustomerNo='" + customerNo + "' AND PriceRate.PowerPriceNo = PowerPriceInfo.PowerPriceNo";
                try
                {
                    DataSet ds2 = SQLUtl.Query(strSql2);
                    DataTable dt = ds2.Tables["dataSet"];
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cityAddValue = cityAddValue + countPowerValue * double.Parse(dt.Rows[i]["PriceRate"].ToString()) * double.Parse(dt.Rows[i]["CountryAnnex"].ToString())/100;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "calcCityAddValue2");
                }
            }

            return cityAddValue;
        }

        private double calcTransLoss(int transType, double transAmount)         //计算变损电量
        {
            double transLossValue = 0;
            string strSql1 = " SELECT TOP 1 TransformerLose ,standarVolume From TransformerLoseInfo, TransformerInfo "
                            + " Where TransformerLoseInfo.TransformerLoseNo= TransformerInfo.TransformerLoseNo And "
                            + " TransformerInfo.TransformerNo = '" + transType + "' And MonthUsed >= " + transAmount + " And LessOrMoreFlag = 0 " 
                            + " ORDER BY TransformerLose ASC";
            try
            {
                DataSet ds1 = SQLUtl.Query(strSql1);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    TransformerVolume = int.Parse(ds1.Tables[0].Rows[0]["standarVolume"].ToString());
                    transLossValue = double.Parse(ds1.Tables[0].Rows[0]["TransformerLose"].ToString()); 
                    /*if (TransformerVolume > EssenceFeeVolume && isBigIndustry(txtCustomerNo.Text))
                    { transLossValue = 0; }
                    else
                    { transLossValue = double.Parse(ds1.Tables[0].Rows[0]["TransformerLose"].ToString()); }*/
                }
                else
                {
                    string strSql2 = " SELECT TOP 1 TransformerLose ,standarVolume From TransformerLoseInfo, TransformerInfo "
                                    + " Where TransformerLoseInfo.TransformerLoseNo= TransformerInfo.TransformerLoseNo And "
                                    + " TransformerInfo.TransformerNo = '" + transType + "' And MonthUsed < " + transAmount + " And LessOrMoreFlag = 1"
                                    + " ORDER BY TransformerLose DESC";
                    DataSet ds2 = SQLUtl.Query(strSql2);
                    if (ds2.Tables[0].Rows.Count != 0)
                    {
                        TransformerVolume = int.Parse(ds2.Tables[0].Rows[0]["standarVolume"].ToString());
                        transLossValue = double.Parse(ds2.Tables[0].Rows[0]["TransformerLose"].ToString());
                        /*if (TransformerVolume > EssenceFeeVolume && isBigIndustry(txtCustomerNo.Text))
                        { transLossValue = 0; }
                        else
                        { transLossValue = double.Parse(ds2.Tables[0].Rows[0]["TransformerLose"].ToString()); }*/
                    }
                    else
                    { transLossValue = 0; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcTransLoss");
            }

            return transLossValue;
        }

        /*private Boolean isBigIndustry(string customerNo)      //判断是否是大工业用电
        {
            Boolean flag = false;
            string strSql = "SELECT PowerPriceNo FROM PriceRate WHERE CustomerNo='"+customerNo+"'";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PowerPriceNo"].ToString().Equals("6"))
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "isBigIndustry");
            }
            return flag;
        }*/

        public void calcPowerFee()
        {
            float lastMonthValue = float.Parse(txtLastMonthValue.Text);
            float thisMonthValue = float.Parse(txtThisMonthValue.Text);
            double CountryAnnex = 0;       //农网附加比例
            int EssenceFeeVolume = 0;       //变压器容量参照
            double EssenceFeeRate = 0;      //计算倍率
          
            if (lastMonthValue != LastMonthStart)
            { AbnormityAmount = LastMonthStart - lastMonthValue; }
            

            //获取附加信息：农网附加率，基本电费计算参数
            string strSql1 = "select * from countrycityannexinfo order by annexdate asc";
            try
            {
                DataSet ds1 = SQLUtl.Query(strSql1);
                DataTable dt1 = ds1.Tables["dataSet"];
                if (dt1.Rows.Count > 0)
                {
                    CountryAnnex = double.Parse(dt1.Rows[0]["CountryAnnex"].ToString());
                    EssenceFeeVolume = int.Parse(dt1.Rows[0]["EssenceFeeVolume"].ToString());
                    EssenceFeeRate = double.Parse(dt1.Rows[0]["EssenceFeeRate"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee1");
            }

            //获取顾客信息
            string strSql2 = "select * from CustomerInfo where CustomerNo='" + txtCustomerNo.Text + "'";
            try
            {
                DataSet ds2 = SQLUtl.Query(strSql2);
                DataTable dt2 = ds2.Tables["dataSet"];
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr = dt2.Rows[0];
                    customer.setCustomerName(dr["CustomerName"].ToString());
                    customer.setCustomerAddress(dr["CustomerAddress"].ToString());
                    customer.setInvoiceType(dr["InvoiceType"].ToString());
                    customer.setElectriCharacterName(dr["ElectriCharacterName"].ToString());
                    customer.setLine(dr["LineNum"].ToString());
                    customer.setArea(dr["AreaNo"].ToString());
                    customer.setEspecialFlag(dr["Especialflag"].ToString());
                    customer.setLowProtectFlag(dr["LowProtectFlag"].ToString());
                    customer.setTranslossOrBaseprice(dr["TranslossOrBaseprice"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee2");
            }

            //获取计费信息
            string strSql3 = "Select * From CountFeeInfo Where CustomerNo = '" + txtCustomerNo.Text + "'";
            try
            {
                DataSet ds3 = SQLUtl.Query(strSql3);
                DataTable dt3 = ds3.Tables["dataSet"];
                if (dt3.Rows.Count > 0)
                {
                    AmmeterMulti = double.Parse(dt3.Rows[0]["AmmeterMulti"].ToString());
                    OldAmmeterMulti = AmmeterMulti;
                    LineLoseRate = double.Parse(dt3.Rows[0]["LineLoseRate"].ToString());
                    TransformerNo = int.Parse(dt3.Rows[0]["TransformerNo"].ToString());
                    DiscountRate = double.Parse(dt3.Rows[0]["DiscountRate"].ToString());
                }
                else
                {
                    AmmeterMulti = 1;
                    OldAmmeterMulti = AmmeterMulti;
                    LineLoseRate = 0;
                    TransformerNo = 0;
                    DiscountRate = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee3");
            }

            //计算部分
            float powerValue = thisMonthValue - lastMonthValue;                 //抄表电量
            powerValue = float.Parse(String.Format("{0:###0.00}", powerValue));
            //止码小于起码处理
            if (thisMonthValue < lastMonthValue)
            {
                if (thisMonthValue < 9999)
                {
                    powerValue = 10000 - lastMonthValue + thisMonthValue;
                }
                else if (thisMonthValue < 99999)
                {
                    powerValue = 100000 - lastMonthValue + thisMonthValue;
                }
                else if (thisMonthValue < 999999)
                {
                    powerValue = 1000000 - lastMonthValue + thisMonthValue;
                }

            }

            double lineLoss = powerValue * AmmeterMulti * LineLoseRate / 100;    //线损
            double transAmount = powerValue * AmmeterMulti;
            double transLoss = calcTransLoss(TransformerNo, transAmount);  //变损
            if (customer.getTranslossOrBaseprice().Equals("1"))
            {
                transLoss = 0;
            }
            int countPowerValue = (int)Math.Round(powerValue * AmmeterMulti + lineLoss + transLoss, MidpointRounding.AwayFromZero);       //记费电量取整
          /*
            //低保户处理
            if (customer.getLowProtectFlag().Equals("1"))
            {
                countPowerValue = countPowerValue - 10;
            }
            */
            AbnormityAmount = (float)AbnormityAmount * AmmeterMulti;      //异动电量

            double PowerFee = calcPowerPrice(txtCustomerNo.Text, countPowerValue); //综合电费

            double basePrice = 0;           //基本电费
            if (customer.getTranslossOrBaseprice().Equals("1"))
            { basePrice = TransformerVolume * EssenceFeeRate; }

            double ruralAddPrice = countPowerValue * CountryAnnex;      //农网附加
            if (customer.getElectriCharacterName().Equals("局内"))      
            {
                ruralAddPrice = 0;                                  //局内用户，不算城镇附加和农网附加
            }
            double cityAddPrice = calcCityAddValue(txtCustomerNo.Text, countPowerValue);        //城网附加
            double totalPrice = 0;
            totalPrice = PowerFee + basePrice;        //总电费
            if (DiscountRate > 0)
            {
                totalPrice = totalPrice * (100 - DiscountRate) / 100;
            }
            

            //显示
            txtPowerValue.Text = String.Format("{0:###0.00}", powerValue);
            txtMultiple.Text = String.Format("{0:###0.00}",AmmeterMulti);
            txtLineLoss.Text = String.Format("{0:###0}",lineLoss);
            txtChangeLoss.Text = String.Format("{0:###0}",transLoss);
            txtCountPowerValue.Text = String.Format("{0:###0}", countPowerValue);
            //averPrice = Price;

            switch(PriceCount)
            {
                case 1:
                    txtPowerPrice1.Text = String.Format("{0:###0.00####}",PowerPrice[0]);
                    txtPowerRate1.Text = String.Format("{0:###0.00####}", PowerPriceRate[0]);
                    break;
                case 2:
                    txtPowerPrice1.Text = String.Format("{0:###0.00####}", PowerPrice[0]);
                    txtPowerRate1.Text = String.Format("{0:###0.00####}", PowerPriceRate[0]);
                    txtPowerPrice2.Text = String.Format("{0:###0.00####}", PowerPrice[1]);
                    txtPowerRate2.Text = String.Format("{0:###0.00####}", PowerPriceRate[1]);
                    break;
                case 3:
                    txtPowerPrice1.Text = String.Format("{0:###0.00####}", PowerPrice[0]);
                    txtPowerRate1.Text = String.Format("{0:###0.00####}", PowerPriceRate[0]);
                    txtPowerPrice2.Text = String.Format("{0:###0.00####}", PowerPrice[1]);
                    txtPowerRate2.Text = String.Format("{0:###0.00####}", PowerPriceRate[1]);
                    txtPowerPrice3.Text = String.Format("{0:###0.00####}", PowerPrice[2]);
                    txtPowerRate3.Text = String.Format("{0:###0.00####}", PowerPriceRate[2]);
                    break;
                case 4:
                    txtPowerPrice1.Text = String.Format("{0:###0.00####}", PowerPrice[0]);
                    txtPowerRate1.Text = String.Format("{0:###0.00####}", PowerPriceRate[0]);
                    txtPowerPrice2.Text = String.Format("{0:###0.00####}", PowerPrice[1]);
                    txtPowerRate2.Text = String.Format("{0:###0.00####}", PowerPriceRate[1]);
                    txtPowerPrice3.Text = String.Format("{0:###0.00####}", PowerPrice[2]);
                    txtPowerRate3.Text = String.Format("{0:###0.00####}", PowerPriceRate[2]);
                    txtPowerPrice4.Text = String.Format("{0:###0.00####}", PowerPrice[3]);
                    txtPowerRate4.Text = String.Format("{0:###0.00####}", PowerPriceRate[3]);
                    break;
            }
            txtRuralPrice.Text = String.Format("{0:###0.00}",ruralAddPrice);
            txtCityPrice.Text = String.Format("{0:###0.00}",cityAddPrice);
            txtBasePrice.Text = String.Format("{0:###0.00}",basePrice);
            txtTotalPrice.Text = String.Format("{0:###0.00}",totalPrice);
            txtAbnormityAmount.Text = String.Format("{0:#########0.00####}",AbnormityAmount);

        }


        private void saveFee() 
        {
            Boolean saveFlag = true; 
            
            if (txtThisMonthValue.Text == "")
            {
                MessageBox.Show("本月止码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //saveFlag = false;
                return;
            }
            if (!Regex.IsMatch(txtThisMonthValue.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("本月止码必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //saveFlag = false;
                return;
            }
            if (dtpBirthday.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("不能录入大于当前月份的止码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //saveFlag = false;
                return;
            }
            if (dtpBirthday.Value.Year == DateTime.Now.Year && dtpBirthday.Value.Month > DateTime.Now.Month)
            {
                MessageBox.Show("不能录入大于当前月份的止码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //saveFlag = false;
                return;
            }
            if (float.Parse(txtThisMonthValue.Text) < float.Parse(txtLastMonthValue.Text))
            {
                if (MessageBox.Show("确定止码小于起码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (float.Parse(txtLastMonthValue.Text) < 999)
                    {
                        MessageBox.Show("表码有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //saveFlag = false;
                        return;
                    }
                    
                }
                else 
                {
                    saveFlag = false; 
                    return;
                }
            }

            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string strSql = "";
            DataSet ds = null;
            DataTable dt = null;
            strSql = "Select customerNo From AmmeterValue Where customerNo = '" + txtCustomerNo.Text + "' And copyValueDate = '" + time + "'";
            ds = SQLUtl.Query(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("该客户本月数据已录入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //saveFlag = false;
                return;
            }

            strSql = "Select customerNo From AmmeterValue Where customerNo = '" + txtCustomerNo.Text + "' And copyValueDate > '" + time + "'";
            ds = SQLUtl.Query(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("录入本月止码的时间必须大于录入上月起码的时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //saveFlag = false;
                return;
            }

            //计算电费
            calcPowerFee();
            //存入数据库

            //int powerValue = int.Parse(txtThisMonthValue.Text) - int.Parse(txtLastMonthValue.Text);
            float powerValue = float.Parse(txtPowerValue.Text);
            double MonthCopyAmount = powerValue * AmmeterMulti;
            //double MonthCopyAmount = int.Parse(txtPowerValue.Text) * AmmeterMulti;
            double CountFeeMoney = int.Parse(txtCountPowerValue.Text) * averPrice;
            int LineLoss = int.Parse(txtLineLoss.Text);
            if (AbnormityAmount != 0)
            { LastMonthStart = float.Parse(txtLastMonthValue.Text); }
            float lastBalance = 0;      //上期结余（预存）
            try 
            {
                strSql = "select AdvanceDeposit from CountFee where CustomerNo='" + txtCustomerNo.Text + "'"
                               + " and Invoiceflag=1 and CountFeeDate<'" + time + "'and NegativeInvoiceFlag=0 order by CountFeeDate desc";
                dt = SQLUtl.Query(strSql).Tables["dataSet"];
                if (dt.Rows.Count != 0 && dt.Rows[0]["AdvanceDeposit"].ToString() != "")
                {
                    lastBalance = float.Parse(dt.Rows[0]["AdvanceDeposit"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //saveFlag = false;
                return;
            }

            float totalFee = float.Parse(txtTotalPrice.Text);   //本期电费金额
            float preFee = totalFee - lastBalance;              //应交（预收）电费
            float actFee = (int) preFee;                         //实交电费（预收取整）
            float thisBalance = actFee - preFee;                //本期结余（下期预存）

            try
            {
                string strSql1 = "insert into AmmeterValue (CustomerNo,AmmeterValue,CopyValueDate,Inputtime,Inputman) "
                      + "Values ('" + txtCustomerNo.Text + "'," + txtThisMonthValue.Text + ",'" + time + "',GETDATE(),'" + Constant.LoginUser.UserName + "')";

                string strSql2 = "insert into CountFee(CustomerNo,StartCode,EndCode,MonthCopyAmount,CountFeeAmount,CountFeeMoney,"
                        + "OldAmmeterMulti,AbnormityAmount,LineLose ,TransformerLose,CityAnnex,CountryAnnex,BaseFeeMoney,TotalMoney,"
                        + "FactRec,AdvanceDeposit,AccountRec,CountFeeDate,InputTime,InputMan,LastMonthStart,CustomerName,CustomerAddress,LineNum,AreaNo,InvoiceType,ElectriCharacterName,"
                        + "Especialflag,TransformerNo,AmmeterMulti,LineLoseRate,DiscountRate,PowerPriceNo1,PowerPrice1,PriceRate1,"
                        + "PowerPriceNo2,PowerPrice2,PriceRate2,PowerPriceNo3,PowerPrice3,PriceRate3,PowerPriceNo4,PowerPrice4,PriceRate4,NegativeInvoiceFlag)"
                        + "values('" + txtCustomerNo.Text + "','" + txtLastMonthValue.Text + "','" + txtThisMonthValue.Text + "','" + MonthCopyAmount + "','" + txtCountPowerValue.Text + "',"
                        + " '" + CountFeeMoney + "','" + OldAmmeterMulti + "','" + AbnormityAmount + "','" + LineLoss + "','" + txtChangeLoss.Text + "',"
                        + " '" + txtCityPrice.Text + "','" + txtRuralPrice.Text + "','" + txtBasePrice.Text + "','" + txtTotalPrice.Text + "','" + actFee + "','" + thisBalance + "','" + preFee + "','" + time + "',GETDATE(),"
                        + " '" + Constant.LoginUser.UserName + "','" + LastMonthStart + "','" + txtCustomerName.Text + "','" + customer.getCustomerAddress() + "','" + customer.getLine() + "',"
                        + " '" + customer.getArea() + "','" + customer.getInvoiceType() + "','" + customer.getElectriCharacterName() + "','" + customer.getEspecialFlag() + "',"
                        + " '" + TransformerNo + "','" + AmmeterMulti + "','" + LineLoseRate + "','" + DiscountRate + "','" + PowerPriceNo[0] + "','" + PowerPrice[0] + "',"
                        + " '" + PowerPriceRate[0] + "','" + PowerPriceNo[1] + "','" + PowerPrice[1] + "','" + PowerPriceRate[1] + "','" + PowerPriceNo[2] + "',"
                        + " '" + PowerPrice[2] + "','" + PowerPriceRate[2] + "','" + PowerPriceNo[3] + "','" + PowerPrice[3] + "','" + PowerPriceRate[3] + "',0)";
                List<String> sqlList = new List<string>();
                sqlList.Add(strSql1);
                sqlList.Add(strSql2);
                SQLUtl.ExecuteSqlTran(sqlList); //事务

                for (int k = 0; k < 4; k++)
                {
                    PowerPriceNo[k] = 0;
                    PowerPrice[k] = 0;
                    PowerPriceRate[k] = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                saveFlag = false;
            }

            if (!saveFlag)
            {
                MessageBox.Show("录入失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }

            if (saveFlag)
            {
                if (lbxCustomerList.SelectedIndex < lbxCustomerList.Items.Count - 1)
                {
                    lbxCustomerList.SelectedIndex = lbxCustomerList.SelectedIndex + 1;
                }
                else
                {
                    MessageBox.Show("已经是最后一条了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFee();
        }

        private void txtThisMonthValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                calcPowerFee();
            }
            if (e.KeyCode == Keys.Enter)
            {                
                saveFee();
               
            }
        }
      
    }
}
