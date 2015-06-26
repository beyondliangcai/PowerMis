using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using Common;
using DBUtility;
using BusinessModel;
using System.Text.RegularExpressions;

namespace PowerMis.Statistical
{
    public partial class Frm_negativeInvoice : Form
    {
        private Customer customer = null;
        private string CustomerNo = "";
        private string CustomerName = "";
        private float PowerValue = 0;
        private int TransformerVolume = 0;      //变压器额定容量
        private double AmmeterMulti = 0;                //电表倍率
        private double OldAmmeterMulti = 1;             ////电表倍率
        private double LineLoseRate = 0;                //线损率
        private int TransformerNo = 0;                  //变压器号
        private double DiscountRate = 0;                //折扣
        private int PriceCount = 0;             //记录客户使用电价种数
        private int[] PowerPriceNo = null;
        private double[] PowerPrice = null;
        private float[] PowerPriceRate = null;
        private PrintDocument printDocument = null;
        private Common.Print Print = null;

        private float CountryAnnex = 0;      //农网附加比率
        private float StartCode = 0;
        private float EndCode = 0;
        private float CopyPowerValue = 0;   //抄表电量
        private int CountPowerValue = 0;  //计费电量
        private int TransLose = 0;
        private int LineLose = 0; 
        private float TotalMoney = 0;
        private float BaseFee = 0;          //基本电费
        private float CityAddPrice = 0;        //城镇附加费
        private float RuralAddPrice = 0;
        private string PrintDate = "";      //发票打印日期
        private string million, hundredThousand, tenThousand, thousand, hundren, ten, one, fens, tenFen;

        public Frm_negativeInvoice()
        {
            InitializeComponent();
            customer = new Customer();
            PowerPriceNo = new int[4];
            PowerPrice = new double[4];
            PowerPriceRate = new float[4];
            printDocument = new PrintDocument();
            Print = new Print();
        }

        private void Frm_negativeInvoice_Load(object sender, EventArgs e)
        {
            Margins margin = new Margins(50, 50, 50, 50);
            this.printDocument.DefaultPageSettings.Margins = margin;
            PaperSize paperSize = new PaperSize("Custum", 800, 367);
            this.printDocument.DefaultPageSettings.PaperSize = paperSize;    
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CustomerNo = txtCustomerNo.Text.ToString().Trim();
            if (!Regex.IsMatch(txtPowerValue.Text.Trim(), "^[0-9]+[.]?[0-9]*$"))
            {
                MessageBox.Show("冲抵电量必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string[] str1 = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str1[0] + "-" + str1[1] + "-01";
            string strSql3 = "Select * From CountFee Where CustomerNo = '" + CustomerNo + "' and countfeedate= '" + time + "'";
            try
            {
                DataSet ds3 = SQLUtl.Query(strSql3);
                DataTable dt3 = ds3.Tables["dataSet"];
                if (dt3.Rows.Count == 0)
                {
                    MessageBox.Show("该月没有用电！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (int.Parse(dt3.Rows[0]["CountFeeAmount"].ToString()) != int.Parse(txtPowerValue.Text.Trim()))
                {
                    MessageBox.Show("冲抵电量必须等于上月电量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"dayin01");
            }

            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            string time1 = str[0] + "-" + str[1] + "-01";
            string strSql = "Select CustomerNo From CountFee Where CustomerNo = '" + CustomerNo + "' and countfeedate= '" + time1 + "' and NegativeInvoiceFlag=1 and  Inputtime='"+ time + "'";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                DataTable dt = ds.Tables["dataSet"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("该用户本月已经打过负数发票！请先删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"dayin02");
            }

            PowerValue = float.Parse(txtPowerValue.Text.ToString());
            saveFee(PowerValue);

            if (MessageBox.Show("客户名称："+CustomerName+"  冲抵金额："+ TotalMoney+"  确定打印？", "打印提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                printData();
            }
            
        }

        private double calcPowerPrice1(string customerNo, int countPowerValue, string time) //计算电费(实行阶梯电价以前的方式)
        {
            double PowerFee = 0;
            double[] powerFee = new double[4];

            string strSql2 = "SELECT PowerPriceNo1,PriceRate1,PowerPrice1,PowerPriceNo2,PriceRate2,PowerPrice2,"
                          + "PowerPriceNo3,PriceRate3,PowerPrice3,PowerPriceNo4,PriceRate4,PowerPrice4 FROM CountFee"
                          + " WHERE CustomerNo = '" + customerNo + "' and countfeedate = '"+ time +"'";
            try
            {
                DataSet dataSet2 = SQLUtl.Query(strSql2);
                DataTable dataTable2 = dataSet2.Tables["dataSet"];
                if (dataTable2.Rows.Count == 0)
                {
                    PowerFee = 0.52 * countPowerValue;
                }

                else
                {
                    PowerPriceNo[0] = int.Parse(dataTable2.Rows[0]["PowerPriceNo1"].ToString());
                    PowerPriceRate[0] = float.Parse(dataTable2.Rows[0]["PriceRate1"].ToString());
                    PowerPrice[0] = double.Parse(dataTable2.Rows[0]["PowerPrice1"].ToString());
                    PowerPriceNo[1] = int.Parse(dataTable2.Rows[0]["PowerPriceNo2"].ToString());
                    PowerPriceRate[1] = float.Parse(dataTable2.Rows[0]["PriceRate2"].ToString());
                    PowerPrice[1] = double.Parse(dataTable2.Rows[0]["PowerPrice2"].ToString());
                    PowerPriceNo[2] = int.Parse(dataTable2.Rows[0]["PowerPriceNo3"].ToString());
                    PowerPriceRate[2] = float.Parse(dataTable2.Rows[0]["PriceRate3"].ToString());
                    PowerPrice[2] = double.Parse(dataTable2.Rows[0]["PowerPrice3"].ToString());
                    PowerPriceNo[3] = int.Parse(dataTable2.Rows[0]["PowerPriceNo4"].ToString());
                    PowerPriceRate[3] = float.Parse(dataTable2.Rows[0]["PriceRate4"].ToString());
                    PowerPrice[3] = double.Parse(dataTable2.Rows[0]["PowerPrice4"].ToString());

                    for (int i = 0; i < PowerPriceNo.Length; i++)
                    {                    
                        powerFee[i] = countPowerValue * PowerPrice[i] * PowerPriceRate[i] / 100;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        PowerFee += powerFee[i];
                        PowerPriceNo[i] = 0;
                        PowerPriceRate[i] = 0;
                        PowerPrice[i] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");

            }

            return PowerFee;
        }

        private double calcPowerPrice2(string customerNo, int countPowerValue, string time) //计算电费
        {
            double PowerFee = 0;
            int powerValue1 = 180;
            double powerPrice1 = 0.57;
            int powerValue2 = 400;
            double powerPrice2 = 0.05;
            double powerPrice3 = 0.3;
            double[] powerFee = new double[4];

        

            string strSql2 = "SELECT PowerPriceNo1,PriceRate1,PowerPrice1,PowerPriceNo2,PriceRate2,PowerPrice2,"
                          + "PowerPriceNo3,PriceRate3,PowerPrice3,PowerPriceNo4,PriceRate4,PowerPrice4 FROM CountFee"
                          + " WHERE CustomerNo = '" + customerNo + "' and countfeedate = '" + time + "'";
            try
            {
                DataSet dataSet2 = SQLUtl.Query(strSql2);
                DataTable dataTable2 = dataSet2.Tables["dataSet"];
                if (dataTable2.Rows.Count == 0)
                {
                    PowerFee = 0.52 * countPowerValue;
                }

                else
                {
                    PowerPriceNo[0] = int.Parse(dataTable2.Rows[0]["PowerPriceNo1"].ToString());
                    PowerPriceRate[0] = float.Parse(dataTable2.Rows[0]["PriceRate1"].ToString());
                    PowerPrice[0] = double.Parse(dataTable2.Rows[0]["PowerPrice1"].ToString());
                    PowerPriceNo[1] = int.Parse(dataTable2.Rows[0]["PowerPriceNo2"].ToString());
                    PowerPriceRate[1] = float.Parse(dataTable2.Rows[0]["PriceRate2"].ToString());
                    PowerPrice[1] = double.Parse(dataTable2.Rows[0]["PowerPrice2"].ToString());
                    PowerPriceNo[2] = int.Parse(dataTable2.Rows[0]["PowerPriceNo3"].ToString());
                    PowerPriceRate[2] = float.Parse(dataTable2.Rows[0]["PriceRate3"].ToString());
                    PowerPrice[2] = double.Parse(dataTable2.Rows[0]["PowerPrice3"].ToString());
                    PowerPriceNo[3] = int.Parse(dataTable2.Rows[0]["PowerPriceNo4"].ToString());
                    PowerPriceRate[3] = float.Parse(dataTable2.Rows[0]["PriceRate4"].ToString());
                    PowerPrice[3] = double.Parse(dataTable2.Rows[0]["PowerPrice4"].ToString());
                    for (int i = 0; i < PowerPriceNo.Length; i++)
                    {
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
                        PowerPriceNo[i] = 0;
                        PowerPriceRate[i] = 0;
                        PowerPrice[i] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerPrice");

            }

            return PowerFee;
        }

        //由月阶梯计费改为年阶梯计费   calcPowerPrice3
        private double calcPowerPrice3(string customerNo, int countPowerValue, string time) //计算电费
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
            double facCountPowerValue = 0;

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


            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');

            string times = str[0] + "-" + str[1] + "-01";
            string fristmonth = "";         
            
            //2015年从4月开始算阶梯电价
            if (int.Parse(str[0]) == 2015 && int.Parse(str[1]) >= 4)
            {
                //   MessageBox.Show(str[0]);
                fristmonth = str[0] + "-04-01";
            }

            //从2016年开始从1月记阶梯电价
            else if (int.Parse(str[0]) > 2015)
            {
                fristmonth = str[0] + "-01-01";
            }
            string strSql3 = "select * from CountFee where CustomerNo='" + customerNo + "'and CountFeeDate <'" + time + "'and CountFeeDate>='" + fristmonth + "'";
            double lasttotalprice = 0, totalprice = 0;
            int lasttotalcount = 0, totalcount = 0;
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
                            lasttotalcount = lasttotalcount + (int.Parse(dataTable3.Rows[j]["CountFeeAmount"].ToString()) * int.Parse(dataTable3.Rows[j]["PriceRate1"].ToString())) / 100;
                            //   facCountPowerValue = countPowerValue * float.Parse(dataTable3.Rows[j]["PriceRate1"].ToString()) / 100;
                       
                        }
                        if (int.Parse(dataTable3.Rows[j]["PowerPriceNo2"].ToString()) == 1 || int.Parse(dataTable3.Rows[j]["PowerPriceNo2"].ToString()) == 9)
                        {

                            lasttotalcount = lasttotalcount + int.Parse(dataTable3.Rows[j]["CountFeeAmount"].ToString()) * int.Parse(dataTable3.Rows[j]["PriceRate2"].ToString()) / 100;

                            //           facCountPowerValue = countPowerValue * float.Parse(dataTable3.Rows[j]["PriceRate2"].ToString()) / 100;


                        }
                    }

                }
               // MessageBox.Show("lasttotalcount" + lasttotalcount);                
               
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
                        if (PowerPriceNo[i] == 1)
                        {
                            totalcount = lasttotalcount + (countPowerValue * (int)PowerPriceRate[i]) / 100;
                         //   MessageBox.Show("totalcount" + totalcount);
                            lasttotalprice = 0;
                            totalprice = 0;
                            //上个月电费总和
                            if (lasttotalcount <= powerValue1)
                            {
                                lasttotalprice = lasttotalcount * powerPrice1;

                            }
                            else if (powerValue1 < lasttotalcount && lasttotalcount <= powerValue2)
                            {
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue1) * powerPrice2;
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1;
                            }
                            else if (lasttotalcount > powerValue2)
                            {
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1;
                                lasttotalprice = lasttotalprice + (powerValue2 - powerValue1) * powerPrice2;
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue2) * powerPrice3;
                            }
                          //  MessageBox.Show("lasttotalprice:" + lasttotalprice);

                            if (totalcount <= powerValue1)
                            {
                                totalprice = totalcount * powerPrice1;

                            }
                            else if (totalcount > powerValue1 && totalcount <= powerValue2)
                            {
                                totalprice = (totalcount - powerValue1) * powerPrice2;
                                totalprice = totalprice + powerValue1 * powerPrice1;
                            }
                            else if (totalcount > powerValue2)
                            {
                                totalprice = totalprice + powerValue1 * powerPrice1;
                                totalprice = totalprice + (powerValue2 - powerValue1) * powerPrice2;
                                totalprice = totalprice + (totalcount - powerValue2) * powerPrice3;
                            }
                         //   MessageBox.Show("totalprice:" + totalprice);
                            powerFee[i] = totalprice - lasttotalprice;

                        }

                        //如果是农网用户，价格和城网不一样
                        else if (PowerPriceNo[i] == 9)
                        {
                            totalcount = lasttotalcount + (countPowerValue * (int)PowerPriceRate[i]) / 100;
                          //  MessageBox.Show("totalcount" + totalcount);
                            // MessageBox.Show("rural");                           
                              
                            powerPrice1 = PowerPrice[i];
                          
                            lasttotalprice = 0;
                            totalprice = 0;

                            powerPrice2 = powerPrice1 + compairPowerValue1;
                            powerPrice3 = powerPrice1 + compairPowerValue2;
                            
                            //上个月电费总和
                            if (lasttotalcount <= powerValue1)
                            {
                                lasttotalprice = lasttotalcount * powerPrice1;

                            }
                            else if (powerValue1 < lasttotalcount && lasttotalcount <= powerValue2)
                            {
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue1) * powerPrice2;
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1;
                            }
                            else if (lasttotalcount > powerValue2)
                            {
                                lasttotalprice = lasttotalprice + powerValue1 * powerPrice1;
                                lasttotalprice = lasttotalprice + (powerValue2 - powerValue1) * powerPrice2;
                                lasttotalprice = lasttotalprice + (lasttotalcount - powerValue2) * powerPrice3;
                            }
                          //  MessageBox.Show("lasttotalprice:" + lasttotalprice);

                            if (totalcount <= powerValue1)
                            {
                                totalprice = totalcount * powerPrice1;

                            }
                            else if (totalcount > powerValue1 && totalcount <= powerValue2)
                            {
                                totalprice = (totalcount - powerValue1) * powerPrice2;
                                totalprice = totalprice + powerValue1 * powerPrice1;
                            }
                            else if (totalcount > powerValue2)
                            {
                                totalprice = totalprice + powerValue1 * powerPrice1;
                                totalprice = totalprice + (powerValue2 - powerValue1) * powerPrice2;
                                totalprice = totalprice + (totalcount - powerValue2) * powerPrice3;
                            }
                      //      MessageBox.Show("totalprice:" + totalprice);
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
            string strSql1 = "SELECT ElectriCharacterName FROM CustomerInfo WHERE CustomerNo='" + customerNo + "'";
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
                            cityAddValue = cityAddValue + countPowerValue * double.Parse(dt.Rows[i]["PriceRate"].ToString()) * double.Parse(dt.Rows[i]["CountryAnnex"].ToString()) / 100;
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

        private void saveFee(float powerValue)
        {         
            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            PrintDate = str[0] + "年" + str[1] + "月" + str[2] + "日";
            string[] str1 = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str1[0] + "-" + str1[1] + "-01";

            //计算电费
            double CountryAnnex = 0;       //农网附加比例
            int EssenceFeeVolume = 0;       //变压器容量参照
            double EssenceFeeRate = 0;      //计算倍率

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
            string strSql2 = "select * from CustomerInfo where CustomerNo='" + CustomerNo + "'";
            try
            {
                DataSet ds2 = SQLUtl.Query(strSql2);
                DataTable dt2 = ds2.Tables["dataSet"];
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr = dt2.Rows[0];
                    customer.setCustomerName(dr["CustomerName"].ToString());
                    txtCustomerName.Text = dr["CustomerName"].ToString();
                    CustomerName = dr["CustomerName"].ToString();
                    customer.setCustomerAddress(dr["CustomerAddress"].ToString());
                    customer.setInvoiceType(dr["InvoiceType"].ToString());
                    customer.setElectriCharacterName(dr["ElectriCharacterName"].ToString());
                    customer.setLine(dr["LineNum"].ToString());
                    customer.setArea(dr["AreaNo"].ToString());
                    customer.setEspecialFlag(dr["Especialflag"].ToString());
                    customer.setLowProtectFlag(dr["LowProtectFlag"].ToString());
                    customer.setTranslossOrBaseprice(dr["TranslossOrBaseprice"].ToString());
                }
                else 
                {
                    MessageBox.Show("该客户号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee2");
            }

            //获取计费信息
            //string strSql3 = "Select * From CountFeeInfo Where CustomerNo = '" + CustomerNo + "'";
            string strSql3 = "Select AmmeterMulti,LineLoseRate,TransformerNo,DiscountRate From CountFee Where CustomerNo = '" + CustomerNo + "' and countfeedate= '" + time + "'";
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

            double lineLoss = powerValue * AmmeterMulti * LineLoseRate / 100;    //线损
            LineLose = int.Parse(String.Format("{0:###0}", lineLoss));
            double transAmount = powerValue * AmmeterMulti;
            double transLoss = calcTransLoss(TransformerNo, transAmount);  //变损
            
            if (customer.getTranslossOrBaseprice().Equals("1"))
            {
                transLoss = 0;
            }
            TransLose = int.Parse(String.Format("{0:#####0}", transLoss));
            int countPowerValue = (int)Math.Round(powerValue * AmmeterMulti + lineLoss + transLoss, MidpointRounding.AwayFromZero);       //记费电量取整
            CountPowerValue = 0 - countPowerValue;

            double PowerFee = 0; //综合电费
            double PowerFee1 = 0;
            DateTime datetime1 = DateTime.Parse(time);
            DateTime datetime2 = DateTime.Parse("2012-07-01");
            DateTime datetime3 = DateTime.Parse("2015-04-01");

            //负数发票时间小于2012-07-01
            if (DateTime.Compare(datetime1, datetime2) < 0)
            {
                PowerFee = calcPowerPrice1(CustomerNo, countPowerValue,time); //综合电费
            }
            //负数发票时间大于2012-07-01小与2015-04-01，采用按月阶梯收费
            else if (DateTime.Compare(datetime1, datetime2) >= 0 && DateTime.Compare(datetime1, datetime3) < 0)
            {
                PowerFee = calcPowerPrice2(CustomerNo, countPowerValue,time); //综合电费
            }

            else if (DateTime.Compare(datetime1, datetime3) >= 0 )
            {
          
                PowerFee = calcPowerPrice3(CustomerNo, countPowerValue, time); //综合电费
            }
            double basePrice = 0;           //基本电费
            if (customer.getTranslossOrBaseprice().Equals("1"))
            { basePrice = TransformerVolume * EssenceFeeRate; }
            BaseFee = float.Parse(String.Format("{0:###0.00}", basePrice));

            double ruralAddPrice = countPowerValue * CountryAnnex;      //农网附加
            if (customer.getElectriCharacterName().Equals("局内"))
            {
                ruralAddPrice = 0;                                  //局内用户，不算城镇附加和农网附加
            }
            RuralAddPrice = float.Parse(String.Format("{0:###0.00}", ruralAddPrice));
            double cityAddPrice = calcCityAddValue(CustomerNo, countPowerValue);        //城网附加
            CityAddPrice = float.Parse(String.Format("{0:###0.00}", cityAddPrice));
            double totalPrice = 0;
            totalPrice = PowerFee + basePrice;        //总电费
            if (DiscountRate > 0)
            {
                totalPrice = totalPrice * (100 - DiscountRate) / 100;
            }
            TotalMoney = 0 - float.Parse(String.Format("{0:###0.00}", totalPrice));
            txtTotalPrice.Text = TotalMoney.ToString();
            
            CopyPowerValue = 0 - float.Parse(String.Format("{0:###0.00}", powerValue * AmmeterMulti));
            StartCode = 0;
            EndCode = 0 - powerValue;
           
        }

        public void printData()
        {
            int temp;
            int tempmode;
            string[] str = dtpBirthday.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string[] str1 = dateTimePicker1.Value.ToShortDateString().Split('-');
            string negativeDate = str1[0] + "-" + str1[1] + "-01";
                        temp = (int)((0-TotalMoney) / 1000000);
                        tempmode = (int)(0-TotalMoney) % 1000000;
                        million = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 100000);
                        tempmode = tempmode % 100000;
                        hundredThousand = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 10000);
                        tempmode = tempmode % 10000;
                        tenThousand = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 1000);
                        tempmode = tempmode % 1000;
                        thousand = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 100);
                        tempmode = tempmode % 100;
                        hundren = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 10);
                        tempmode = tempmode % 10;
                        ten = Print.MoneyNumToStr(temp);
                        temp = (int)(tempmode / 1);
                        tempmode = tempmode % 1;
                        one = Print.MoneyNumToStr(temp);

                        fens = Print.ConverMoneTofees((0-TotalMoney).ToString(), true);
                        tenFen = Print.ConverMoneTofees((0-TotalMoney).ToString(), false);

            try
            {
                this.printDocument.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
                printDocument.Print();
                //Print.ShowPrintPreviewDialog(printDocument);
            }
            catch (Exception excep)
            {
                MessageBox.Show("不能正确打印,请检查打印机是否故障！", "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printDocument.PrintController.OnEndPrint(printDocument, new PrintEventArgs());
                return;
            }

            //存入数据库

            try
            {

               // MessageBox.Show(CountPowerValue.ToString());
                //MessageBox.Show(time);

               // MessageBox.Show(negativeDate);
                string strSql1 = "insert into CountFee(CustomerNo,StartCode,EndCode,MonthCopyAmount,CountFeeAmount,"
                       + "OldAmmeterMulti,LineLose ,TransformerLose,CityAnnex,CountryAnnex,BaseFeeMoney,TotalMoney,"
                       + "FactRec,AdvanceDeposit,AccountRec,CountFeeDate,InputTime,InputMan,LastMonthStart,CustomerName,CustomerAddress,LineNum,AreaNo,InvoiceType,ElectriCharacterName,"
                       + "Especialflag,TransformerNo,AmmeterMulti,LineLoseRate,DiscountRate,PowerPriceNo1,PowerPrice1,PriceRate1,"
                       + "PowerPriceNo2,PowerPrice2,PriceRate2,PowerPriceNo3,PowerPrice3,PriceRate3,PowerPriceNo4,PowerPrice4,PriceRate4,InvoiceFlag,InvoicePrintDate,InvoicePrintMan,NegativeInvoiceFlag)"
                       + "values('" + CustomerNo + "','" + StartCode + "','" + EndCode + "','" + CopyPowerValue + "','" + CountPowerValue + "',"
                       + " '" + OldAmmeterMulti + "','" + LineLose + "','" + TransLose + "',"
                       + " '" + CityAddPrice + "','" + RuralAddPrice + "','" + BaseFee + "','" + TotalMoney + "','" + TotalMoney + "',0,0,'" + time + "','" + negativeDate + "',"
                       + " '" + Constant.LoginUser.UserName + "',0,'" + customer.getCustomerName() + "','" + customer.getCustomerAddress() + "','" + customer.getLine() + "',"
                       + " '" + customer.getArea() + "','" + customer.getInvoiceType() + "','" + customer.getElectriCharacterName() + "','" + customer.getEspecialFlag() + "',"
                       + " '" + TransformerNo + "','" + AmmeterMulti + "','" + LineLoseRate + "','" + DiscountRate + "','" + PowerPriceNo[0] + "','" + PowerPrice[0] + "',"
                       + " '" + PowerPriceRate[0] + "','" + PowerPriceNo[1] + "','" + PowerPrice[1] + "','" + PowerPriceRate[1] + "','" + PowerPriceNo[2] + "',"
                       + " '" + PowerPrice[2] + "','" + PowerPriceRate[2] + "','" + PowerPriceNo[3] + "','" + PowerPrice[3] + "','" + PowerPriceRate[3] + "',1,left(GETDATE(),10),'" + Constant.LoginUser.UserName + "',1)";



              //  string strSql1 = "delete from CountFee where CustomerNo='" + CustomerNo + "' and CountFeeDate='" + time + "'";
              //  string strSql3 = "delete from AmmeterValue where CustomerNo='" + CustomerNo + "' and CopyValueDate='" + time + "'"; 
   
                string strSql2 = "insert into NegativeInvoice values ('" + CustomerNo + "','" + customer.getCustomerName() + "','" + CountPowerValue + "','" + TotalMoney + "','" + negativeDate + "',left(GETDATE(),10),'" + Constant.LoginUser.UserName + "' )";
                List<String> sql = new List<string>();
                sql.Add(strSql1);
                sql.Add(strSql2);
              //  sql.Add(strSql3);
                SQLUtl.ExecuteSqlTran(sql);

                for (int k = 0; k < 4; k++)
                {
                    PowerPriceNo[k] = 0;
                    PowerPrice[k] = 0;
                    PowerPriceRate[k] = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("负数发票信息录入数据库失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

             
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = new Font(new FontFamily("宋体"), 12); //打印字体  
            SolidBrush myBrush = new SolidBrush(Color.Black);//刷子
            Graphics g = e.Graphics; //获得绘图对象

            //g.DrawString("客户编号：", printFont, myBrush, 150, 50);
            g.DrawString(CustomerNo, printFont, myBrush, 120, 50);
            //g.DrawString("客户名称：", printFont, myBrush, 420, 50);
            g.DrawString(customer.getCustomerName(), printFont, myBrush, 120, 70);
            //g.DrawString("客户地址：", printFont, myBrush, 150, 80);
            g.DrawString(customer.getCustomerAddress(), printFont, myBrush, 120, 90);

            //g.DrawString("起码：", printFont, myBrush, 150, 110);
            g.DrawString(String.Format("{0:#####0.00}", StartCode), printFont, myBrush, 120, 155);
            //g.DrawString("止码：", printFont, myBrush, 285, 110);
            g.DrawString(String.Format("{0:#####0.00}", EndCode), printFont, myBrush, 120, 135);
            //g.DrawString("电表倍率：", printFont, myBrush, 420, 110);
            g.DrawString(String.Format("{0:#####0.00}", AmmeterMulti), printFont, myBrush, 120, 170);
            //g.DrawString("抄表电量：", printFont, myBrush, 150, 140);
            g.DrawString(String.Format("{0:#####0.00}", CopyPowerValue), printFont, myBrush, 120, 190);
            g.DrawString(String.Format("{0:#####0.00}", TransLose), printFont, myBrush, 150, 210);
            //g.DrawString("计费电量：", printFont, myBrush, 285, 140);
            g.DrawString(String.Format("{0:#####0.00}", CountPowerValue), printFont, myBrush, 150, 230);

            //g.DrawString("基本电费：", printFont, myBrush, 150, 170);
            g.DrawString(String.Format("{0:#####0.00}", BaseFee), printFont, myBrush, 550, 95);

            //g.DrawString("应收：", printFont, myBrush, 150, 200);
            g.DrawString(String.Format("{0:#####0.00}", TotalMoney), printFont, myBrush, 550, 285);
            //g.DrawString("金额大写：", printFont, myBrush, 285, 200);
            g.DrawString(million, printFont, myBrush, 165, 285);
            g.DrawString(hundredThousand, printFont, myBrush, 195, 285);
            g.DrawString(tenThousand, printFont, myBrush, 225, 285);
            g.DrawString(thousand, printFont, myBrush, 255, 285);
            g.DrawString(hundren, printFont, myBrush, 285, 285);
            g.DrawString(ten, printFont, myBrush, 315, 285);
            g.DrawString(one, printFont, myBrush, 345, 285);
            g.DrawString(fens, printFont, myBrush, 375, 285);
            g.DrawString(tenFen, printFont, myBrush, 405, 285);


            g.DrawString("实收金额：", printFont, myBrush, 470, 265);
            g.DrawString(String.Format("{0:#####0.00}", TotalMoney), printFont, myBrush, 550, 265);
            //g.DrawString("打印日期：", printFont, myBrush, 150, 260);
            g.DrawString(PrintDate, printFont, myBrush, 320, 50);
            //g.DrawString("打印人员：", printFont, myBrush, 420, 260);
            g.DrawString(Constant.LoginUser.UserName, printFont, myBrush, 410, 310);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
