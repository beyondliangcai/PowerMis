using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessModel;
using PowerMis.SystemManagement;
using PowerMis.BasicInfo;
using PowerMis.CountFeeManagement;
using PowerMis.CustomInfo;
using PowerMis.Statistical;
using PowerMis.Statistical.Reporter;
using PowerMis.Bank;

using Common;
using DBUtility;
using BusinessModel;


namespace PowerMis
{
    public partial class Frm_Main : Form
    {
        DateTime _loginTime;
        public Frm_Main(DateTime time)
        {
            InitializeComponent();
            _loginTime = time;
            if (Constant.LoginUser.Permission.ToString().Trim().Equals("一般操作员"))
            {
                this.基本信息管理ToolStripMenuItem.Visible = false;
                this.客户信息录入ToolStripMenuItem.Visible = false;
                this.客户信息维护ToolStripMenuItem.Visible = false;
                this.客户转册ToolStripMenuItem.Visible = false;
                this.供电管理ToolStripMenuItem.Visible = false;
                this.抄表录入ToolStripMenuItem.Visible = false;
                //this.抄表维护ToolStripMenuItem.Visible = false;
                this.分摊变损ToolStripMenuItem.Visible = false;
                this.更改发票ToolStripMenuItem.Visible = false;
                this.农网发票ToolStripMenuItem.Visible = false;
                this.用户管理ToolStripMenuItem.Visible = false;
                this.违约金设置ToolStripMenuItem.Visible = false;
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {

            userName.Text = Constant.LoginUser.UserName;
            loginTime.Text = _loginTime.ToString();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_User frmUser = new Frm_User();
            frmUser.Show();
        }


        private void 客户信息维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaintainCustom maintainCustom = new MaintainCustom();
            maintainCustom.Show();
        }

        private void 客户信息录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCustom addCustom = new AddCustom();
            addCustom.Show();
        }

        private void 客户信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchCustom searchCustom = new SearchCustom();
            searchCustom.Show();
        }


        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ModifyPassword frmPswModify = new Frm_ModifyPassword();
            frmPswModify.Show();
        }

        private void 电价信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EpriceInfo ePrice = new EpriceInfo();
            ePrice.Show();
        }

        private void 抄表录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_FillTable frmFillFillTable = new Frm_FillTable();
            frmFillFillTable.Show();
        }

        private void 抄表维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_FillTableQuery frmFillTableQuery = new Frm_FillTableQuery();
            frmFillTableQuery.Show();
        }

        private void 全年查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_QueryAllyear frmQueryAllyear = new Frm_QueryAllyear();
            frmQueryAllyear.Show();
        }

        private void 分摊变损ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ModifyTransLoss frmModifyTransLoss = new Frm_ModifyTransLoss();
            frmModifyTransLoss.Show();
        }

        private void 台区供电ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AreaPowerSupply frmAreaPowerSupply = new Frm_AreaPowerSupply();
            frmAreaPowerSupply.Show();
        }

        private void 线路供电ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LinePowerSupply frmLinePowerSuooly = new Frm_LinePowerSupply();
            frmLinePowerSuooly.Show();
        }

        private void 接收电量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ReceivePower frmReceivePower = new Frm_ReceivePower();
            frmReceivePower.Show();
        }

        private void 农网发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_RuraInvoicel frmRuralInvoice = new Frm_RuraInvoicel();
            frmRuralInvoice.Show();
        }


        private void 客户转册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSection changeSection = new ChangeSection();
            changeSection.Show();
        }


        private void 城网发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CityInvoice frmTownInvoice = new Frm_CityInvoice();
            frmTownInvoice.Show();
        }


        private void 增值税发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ValueAddedTax frmValueAddTax = new Frm_ValueAddedTax();
            frmValueAddTax.Show();
        }

        private void 线路信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LineInfo frm_LineInfo = new Frm_LineInfo();
            frm_LineInfo.Show();
        }

        private void 更改发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ChangeInvoiceFlag frmChangeInvoiceFlag = new Frm_ChangeInvoiceFlag();
            frmChangeInvoiceFlag.Show();
        }

        private void 台区信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AreaInfo frm_AreaInfo = new Frm_AreaInfo();
            frm_AreaInfo.Show();
        }

        private void 电费统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PowerFeeStat frmPowerFeeRep = new Frm_PowerFeeStat();
            frmPowerFeeRep.Show();
        }

        private void 变损信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_TranLossInfo frm_tranLossInfo = new Frm_TranLossInfo();
            frm_tranLossInfo.Show();
        }

        private void 台区电量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AreaPowerFeeStat frmAreaPowerFeeRep = new Frm_AreaPowerFeeStat();
            frmAreaPowerFeeRep.Show();
        }

        private void 线路电量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LinePowerFeeStat frmLinePowerStat = new Frm_LinePowerFeeStat();
            frmLinePowerStat.Show();
        }

        private void 农村电量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_RuralPowerFeeStat frmRuralPowerFeeStat = new Frm_RuralPowerFeeStat();
            frmRuralPowerFeeStat.Show();
        }

        private void 机关枢纽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DepartmentPowerFeeStat frmDepPowerFeeStat = new Frm_DepartmentPowerFeeStat();
            frmDepPowerFeeStat.Show();
        }

        private void 电费清单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PowerFeeListStat1 frmPowerFeeListStat = new Frm_PowerFeeListStat1();
            frmPowerFeeListStat.Show();
        }

        private void 变压器信息toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_TransformerInfo frm_TransformerInfo = new Frm_TransformerInfo();
            frm_TransformerInfo.Show();
        }

        private void 线路电表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LineAmmeter frm_LineAmmeter = new Frm_LineAmmeter();
            frm_LineAmmeter.Show();
        }

        private void 台区电表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AreaAmmeter frm_AreaAmmeter = new Frm_AreaAmmeter();
            frm_AreaAmmeter.Show();
        }

        private void 接收电表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ReceiveAmmeter frm_ReceiveAmmeter = new Frm_ReceiveAmmeter();
            frm_ReceiveAmmeter.Show();
        }

        private void 附加信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AdditionInfo frm_AdditionInfo = new Frm_AdditionInfo();
            frm_AdditionInfo.Show();
        }

        private void 按册统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_VolumPowerFeeStat frmVolumPowerFeeStat = new Frm_VolumPowerFeeStat();
            frmVolumPowerFeeStat.Show();
        }

        private void 发票金额ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_InvoiceFeeStat frmInvoiceStat = new Frm_InvoiceFeeStat();
            frmInvoiceStat.Show();
        }

        private void 收费日报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DayChargeStat frmDayChargeStat = new Frm_DayChargeStat();
            frmDayChargeStat.Show();
        }

        private void 收费月报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_MonthChargeStat frmMonthChargeStat = new Frm_MonthChargeStat();
            frmMonthChargeStat.Show();
        }

        private void 企业台区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CAreaPowerFeeStat frmCareaPowerFeeStat = new Frm_CAreaPowerFeeStat();
            frmCareaPowerFeeStat.Show();
        }

        private void 欠费清单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ArrearListStat frmArrearListStat = new Frm_ArrearListStat();
            frmArrearListStat.Show();
        }

        private void 变损信息ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_TranLossInfo frmTranceLossInfo = new Frm_TranLossInfo();
            frmTranceLossInfo.Show();
        }

        private void 变压器信息toolStripMenuItem_Click_1(object sender, EventArgs e)
        { 
            Frm_TransformerInfo frmTransformerInfo = new Frm_TransformerInfo();
            frmTransformerInfo.Show();
        }

        private void 台区信息ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_AreaInfo frmAreaInfo = new Frm_AreaInfo();
            frmAreaInfo.Show();
        }

        private void 线路信息ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_LineInfo frmlineInfo = new Frm_LineInfo();
            frmlineInfo.Show();
        }

        private void 附加信息ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_AdditionInfo frmAdditionInfo = new Frm_AdditionInfo();
            frmAdditionInfo.Show();
        }

        private void 线路电表ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_LineAmmeter frmLineAmmeter = new Frm_LineAmmeter();
            frmLineAmmeter.Show();
        }

        private void 台区电表ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_AreaAmmeter frmAreaAmmeter = new Frm_AreaAmmeter();
            frmAreaAmmeter.Show();
        }

        private void 接收电表ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_ReceiveAmmeter frmReceiveAmmeter = new Frm_ReceiveAmmeter();
            frmReceiveAmmeter.Show();
        }

        private void 违约金设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PenaltySet frmPenaltySet = new Frm_PenaltySet();
            frmPenaltySet.Show();
        }

        private void 农网未交ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_RuralOweStat frmRuralOweStat = new Frm_RuralOweStat();
            frmRuralOweStat.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_ArrearSumStat frmArrearSumStat = new Frm_ArrearSumStat();
            frmArrearSumStat.Show();
        }

        private void 负数发票打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_negativeInvoice frmNegative = new Frm_negativeInvoice();
            frmNegative.Show();
        }

        private void 负数发票维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_NegativeInvoiceDelete frmNegativeInvoiceDelete = new Frm_NegativeInvoiceDelete();
            frmNegativeInvoiceDelete.Show();
        }

        private void 负数发票统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_NegativeInvoiceStat frmNegativeInvoiceStat = new Frm_NegativeInvoiceStat();
            frmNegativeInvoiceStat.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Frm_NegativeSearch frmNegativeSearch = new Frm_NegativeSearch();
            frmNegativeSearch.Show();
        }

        private void 统计欠费年ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ArrearYearsSum frmArrearYearSum = new Frm_ArrearYearsSum();
            frmArrearYearSum.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         //   MessageBox.Show("农网");
            string ruralsql = "select * from countfee where CountFeeDate ='2015-4-1' and ElectriCharacterName = '农网'";
            string lastsql;
            string updatesql;
            List<String> sqlList = new List<string>();
            int k=0;
            DataSet lastSet;
            DataTable lastTable;
            double sumf=0;
            float f;
            try
            {
                DataSet ruralSet = SQLUtl.Query(ruralsql);
               // MessageBox.Show("1");
                DataTable ruralTable = ruralSet.Tables["dataSet"];
                MessageBox.Show("农网用户"+ruralTable.Rows.Count);
                for (int i = 0; i < ruralTable.Rows.Count; i++) {
                //    MessageBox.Show("customerno:" + ruralTable.Rows[i]["customerno"].ToString());
                    lastsql = "SELECT top 1 * FROM countfee WHERE CustomerNo ='" + ruralTable.Rows[i]["customerno"].ToString() + "'and CountFeeDate < '2015-4-1' order by CountFeeDate desc";
                lastSet = SQLUtl.Query(lastsql);
                lastTable = lastSet.Tables["dataSet"];
              //  MessageBox.Show("k");
                if (lastTable.Rows.Count==0)
                {
                    MessageBox.Show(ruralTable.Rows[i]["customerno"].ToString());
                }
                else {
                    if (float.Parse(lastTable.Rows[0]["AdvanceDeposit"].ToString()) == null)
                    {
                        f = 0;
                    }
                    else
                    {
                        f=float.Parse(lastTable.Rows[0]["AdvanceDeposit"].ToString()) ;
                        MessageBox.Show("f:"+f.ToString());
                    }
                    sumf = sumf + f;
                    k++;
                   // MessageBox.Show("yes1");
                    updatesql = "update countfee set AccountRec =TotalMoney -'" + f + "' where CountFeeDate = '2015-4-1' and  ElectriCharacterName = '农网' and CustomerNo ='" + ruralTable.Rows[i]["customerno"].ToString() + "'";
                    //MessageBox.Show(updatesql);
                    SQLUtl.ExecuteSql(updatesql);
                    //sqlList.Add(updatesql);
                  
                   // MessageBox.Show("AdvanceDeposit:" + float.Parse(lastTable.Rows[0]["AdvanceDeposit"].ToString()));
                 
                  
                   // MessageBox.Show("yes2");
                }
                
                }
                MessageBox.Show("update:" + k);
                MessageBox.Show("sumf:" + sumf);
             //   SQLUtl.ExecuteSqlTran(sqlList);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void 银行导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_BankExport frm_BankExport = new Frm_BankExport();
            frm_BankExport.Show();
        }

        private void 打印发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CityIvoicePrint frm_CityInvoicePrint = new Frm_CityIvoicePrint();
            frm_CityInvoicePrint.Show();
        }

    }
}
