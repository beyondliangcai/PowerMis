using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using BusinessModel;
using System.Data.SqlClient;
using Common;

namespace PowerMis.CountFeeManagement
{
    public partial class Frm_ModifyTransLoss : Form
    {
        private Customer customer = null;
        private double LineLoseRate = 0;                //线损率
        private double AmmeterMulti = 0;
        private int TransformerNo = 0;                  //变压器号
        private int TransformerVolume = 0;      //变压器额定容量
        private double DiscountRate = 0;                //折扣
        private double averPrice = 0;
        private int[] PowerPriceNo = null;
        private double[] PowerPrice = null;
        private float[] PowerPriceRate = null;
        private Boolean flag = true;
        private int PriceCount = 0;             //记录客户使用电价种数

        public Frm_ModifyTransLoss()
        {
            InitializeComponent();
            customer = new Customer();
            PowerPriceNo = new int[4];
            PowerPrice = new double[4];
            PowerPriceRate = new float[4];
        }

        private void Frm_ModifyTransLoss_Load(object sender, EventArgs e)
        {
            initTransformer();
        }

        private void initTransformer()
        {
            string strSql = "select * from TransformerInfo ";
            try
            {
                DataSet ds = SQLUtl.Query(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++ )
                    {
                        cmdTransType.Items.Add(dt.Rows[i]["TransformerNo"] + " " + dt.Rows[i]["TransformerName"]);
                    }
                    cmdTransType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private double calcPowerPrice(string customerNo, int countPowerValue) //计算电费
        {
            double PowerFee = 0;
            int powerValue1 = 0;
            double powerPrice1 = 0;
            int powerValue2 = 0;
            double powerPrice2 = 0;
            double powerPrice3 = 0;
            double[] powerFee = new double[4];

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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            string strSql2 = "SELECT PriceRate.PowerPriceNo as PowerPriceNo,PowerPriceInfo.PowerPriceName as PowerPriceName,"
                          + "PriceRate.PriceRate as PriceRate,PowerPriceInfo.PowerPrice as PowerPrice FROM PriceRate,PowerPriceInfo"
                          + " WHERE CustomerNo = '" + customerNo + "' AND PriceRate.PowerPriceNo = PowerPriceInfo.PowerPriceNo";
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
                    PriceCount = dataTable2.Rows.Count;
                    for (int i = 0; i < dataTable2.Rows.Count; i++)
                    {
                        PowerPriceNo[i] = int.Parse(dataTable2.Rows[i]["PowerPriceNo"].ToString());
                        PowerPriceRate[i] = float.Parse(dataTable2.Rows[i]["PriceRate"].ToString());
                        PowerPrice[i] = double.Parse(dataTable2.Rows[i]["PowerPrice"].ToString());
                        powerFee[i] = countPowerValue * PowerPrice[i] * PowerPriceRate[i] / 100;

                        if (PowerPriceNo[i] == 1 || PowerPriceNo[i] == 9)
                        {
                            int compairPowerValue = (int)(countPowerValue * PowerPriceRate[i] / 100);
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

        private double calcTransLoss(int transType, double transAmount, double sumCount)         //计算变损电量
        {
            double transLossValue = 0;
            string strSql1 = " SELECT TOP 1 TransformerLose ,standarVolume From TransformerLoseInfo, TransformerInfo "
                            + " Where TransformerLoseInfo.TransformerLoseNo= TransformerInfo.TransformerLoseNo And "
                            + " TransformerInfo.TransformerNo = '" + transType + "' And MonthUsed >= " + sumCount + " And LessOrMoreFlag = 0 "
                            + " ORDER BY TransformerLose ASC";
            try
            {
                DataSet ds1 = SQLUtl.Query(strSql1);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    TransformerVolume = int.Parse(ds1.Tables[0].Rows[0]["standarVolume"].ToString());
                    transLossValue = double.Parse(ds1.Tables[0].Rows[0]["TransformerLose"].ToString()) * transAmount / sumCount;
                }
                else
                {
                    string strSql2 = " SELECT TOP 1 TransformerLose ,standarVolume From TransformerLoseInfo, TransformerInfo "
                                    + " Where TransformerLoseInfo.TransformerLoseNo= TransformerInfo.TransformerLoseNo And "
                                    + " TransformerInfo.TransformerNo = '" + transType + "' And MonthUsed < " + sumCount + " And LessOrMoreFlag = 1"
                                    + " ORDER BY TransformerLose DESC";
                    DataSet ds2 = SQLUtl.Query(strSql2);
                    if (ds2.Tables[0].Rows.Count != 0)
                    {
                        TransformerVolume = int.Parse(ds2.Tables[0].Rows[0]["standarVolume"].ToString());
                        transLossValue = double.Parse(ds2.Tables[0].Rows[0]["TransformerLose"].ToString()) * transAmount / sumCount;
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

        public void calcPowerFee(string customerNo, double sumCount)
        {

            double CountryAnnex = 0;       //农网附加比例
            double EssenceFeeRate = 0;      //计算倍率
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            //获取附加信息：农网附加率，基本电费计算参数
            string strSql1 = "select * from countrycityannexinfo order by annexdate asc";
            try
            {
                DataSet ds1 = SQLUtl.Query(strSql1);
                DataTable dt1 = ds1.Tables["dataSet"];
                if (dt1.Rows.Count > 0)
                {
                    CountryAnnex = double.Parse(dt1.Rows[0]["CountryAnnex"].ToString());
                    EssenceFeeRate = double.Parse(dt1.Rows[0]["EssenceFeeRate"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee1");
            }

            //获取顾客信息
            string strSql2 = "select * from CustomerInfo where CustomerNo='" + customerNo + "'";
            try
            {
                DataSet ds2 = SQLUtl.Query(strSql2);
                DataTable dt2 = ds2.Tables["dataSet"];
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr = dt2.Rows[0];
                    customer.setElectriCharacterName(dr["ElectriCharacterName"].ToString());
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
            string strSql3 = "Select * From CountFeeInfo Where CustomerNo = '" + customerNo + "'";
            try
            {
                DataSet ds3 = SQLUtl.Query(strSql3);
                DataTable dt3 = ds3.Tables["dataSet"];
                if (dt3.Rows.Count > 0)
                {
                    AmmeterMulti = double.Parse(dt3.Rows[0]["AmmeterMulti"].ToString());
                    LineLoseRate = double.Parse(dt3.Rows[0]["LineLoseRate"].ToString());
                    TransformerNo = int.Parse(dt3.Rows[0]["TransformerNo"].ToString());
                    DiscountRate = double.Parse(dt3.Rows[0]["DiscountRate"].ToString());
                }
                else
                {
                    AmmeterMulti = 1;
                    
                    LineLoseRate = 0;
                    TransformerNo = 0;
                    DiscountRate = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee3");
            }

            //获取抄表电量
            float powerValue = 0;
            string strSql4 = "select MonthCopyAmount from CountFee Where CustomerNo = '" + customerNo + "' And CountFeeDate = '" + time + "' and NegativeInvoiceFlag=0";
            try
            {
                DataSet ds4 = SQLUtl.Query(strSql4);
                DataTable dt4 = ds4.Tables["dataSet"];
                if (dt4.Rows.Count > 0)
                {
                    powerValue = float.Parse(dt4.Rows[0]["MonthCopyAmount"].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "calcPowerFee3");
            }

            //计算部分
            
            double lineLoss = powerValue  * LineLoseRate / 100;    //线损
            //double transAmount = powerValue * AmmeterMulti;
            double transLoss = calcTransLoss(TransformerNo, powerValue, sumCount);  //变损
            if (customer.getTranslossOrBaseprice().Equals("1"))
            {
                transLoss = 0;
            }
            int countPowerValue = (int)Math.Round(powerValue  + lineLoss + transLoss, MidpointRounding.AwayFromZero);       //记费电量取整
            //低保户处理
            /*
            if (customer.getLowProtectFlag().Equals("1"))
            {
                countPowerValue = countPowerValue - 10;
            }
            */
            double Price = calcPowerPrice(customerNo, countPowerValue); //综合电价
            averPrice = Price; 
            double basePrice = 0;           //基本电费
            if (customer.getTranslossOrBaseprice().Equals("1"))
            { basePrice = TransformerVolume * EssenceFeeRate; }

            double ruralAddPrice = countPowerValue * CountryAnnex;      //农网附加
            if (customer.getElectriCharacterName().Equals("局内"))
            {
                ruralAddPrice = 0;                                  //局内用户，不算城镇附加和农网附加
            }
            double cityAddPrice = calcCityAddValue(customerNo, countPowerValue);        //城网附加

            double totalPrice = Price + basePrice;        //总电费
            if (DiscountRate > 0)
            {
                totalPrice = totalPrice * (100 - DiscountRate) / 100;
            }                   

            //修改数据库
            double CountFeeMoney = countPowerValue * averPrice;
            try
            {
                string strSql = "UPDATE CountFee SET CountFeeAmount=@countFeeAmount,CountFeeMoney=@countFeeMoney, "
                        + "LineLose=@lineLose,TransformerLose=@transformerLose,"
                        + "CityAnnex=@cityAnnex, CountryAnnex=@countryAnnex, BaseFeeMoney=@baseFeeMoney, TotalMoney=@totalMoney,"
                        + "InputTime=GETDATE(), InputMan=@inputMan,"
                        + "PowerPriceNo1=@powerPriceNo1,PowerPrice1=@powerPrice1,PriceRate1=@priceRate1,"
                        + "PowerPriceNo2=@powerPriceNo2,PowerPrice2=@powerPrice2,PriceRate2=@priceRate2,"
                        + "PowerPriceNo3=@powerPriceNo3,PowerPrice3=@powerPrice3,PriceRate3=@priceRate3,"
                        + "PowerPriceNo4=@powerPriceNo4,PowerPrice4=@powerPrice4,PriceRate4=@priceRate4"
                        + " WHERE CustomerNo = '" + customerNo + "' And CountFeeDate = '" + time + "' and NegativeInvoiceFlag=0";
                SqlParameter[] sqlParams = { 
                    new SqlParameter("@countFeeAmount", countPowerValue),
                    new SqlParameter("@countFeeMoney", CountFeeMoney),
                    new SqlParameter("@lineLose", lineLoss),
                    new SqlParameter("@transformerLose", transLoss),
                    new SqlParameter("@cityAnnex", cityAddPrice),
                    new SqlParameter("@countryAnnex", ruralAddPrice),
                    new SqlParameter("@baseFeeMoney", basePrice),
                    new SqlParameter("@totalMoney", totalPrice),
                    new SqlParameter("@inputMan", Constant.LoginUser.UserName),
                    new SqlParameter("@powerPriceNo1", PowerPriceNo[0]),
                    new SqlParameter("@powerPrice1", PowerPrice[0]),
                    new SqlParameter("@priceRate1", PowerPriceRate[0]),
                    new SqlParameter("@powerPriceNo2", PowerPriceNo[1]),
                    new SqlParameter("@powerPrice2", PowerPrice[1]),
                    new SqlParameter("@priceRate2", PowerPriceRate[1]),
                    new SqlParameter("@powerPriceNo3", PowerPriceNo[2]),
                    new SqlParameter("@powerPrice3", PowerPrice[2]),
                    new SqlParameter("@priceRate3", PowerPriceRate[2]),
                    new SqlParameter("@powerPriceNo4", PowerPriceNo[3]),
                    new SqlParameter("@powerPrice4", PowerPrice[3]),
                    new SqlParameter("@priceRate4", PowerPriceRate[3]),
                };
                SQLUtl.ExecuteSql(strSql, sqlParams);

            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message.ToString());               
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            double sumCount = 0;
            string customerNo = "";
            if (cmdTransType.Text == " ")
            {
                MessageBox.Show("变压器号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string transNo = cmdTransType.Text.Split(' ')[0];
            string SQL = "Select Sum(MonthCopyAmount) As sumCopyAmount From "
                         + " CountFee Where TransformerNo = '" + transNo + "' And CountFeeDate = '" + time + "'and NegativeInvoiceFlag=0";
            DataSet ds = SQLUtl.Query(SQL);
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows[0]["sumCopyAmount"].ToString() == "")
            {
                MessageBox.Show("该变压器号未被使用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string value = ds.Tables[0].Rows[0]["sumCopyAmount"].ToString();
                sumCount = double.Parse(value);
            }

            SQL = "Select CustomerNo From CountFee  Where TransformerNo = '" + transNo + "' And CountFeeDate = '" + time + "'and NegativeInvoiceFlag=0";
            ds = SQLUtl.Query(SQL);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    customerNo = ds.Tables[0].Rows[i]["CustomerNo"].ToString();
                    calcPowerFee(customerNo, sumCount);
                }
            }
            if (flag)
            { MessageBox.Show("分摊变损成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else { MessageBox.Show("分摊变损失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
