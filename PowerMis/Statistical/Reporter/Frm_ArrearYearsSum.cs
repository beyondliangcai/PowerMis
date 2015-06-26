using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_ArrearYearsSum : Form
    {
        public Frm_ArrearYearsSum()
        {
            InitializeComponent();
        }
        private void Frm_ArrearYearsSum_Load(object sender, EventArgs e)
        {
            fillYear();
        }
        private void fillYear()
        {
            string strSql = "Select * From V_Year Order by year0 desc";
            DataTable dt = SQLUtl.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbxMinYear.Items.Add(dt.Rows[i]["year0"].ToString());
                cbxMaxYear.Items.Add(dt.Rows[i]["year0"].ToString());
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            
            string minyear = cbxMinYear.Text;
            string maxyear = cbxMaxYear.Text;
            if (int.Parse(minyear) >int.Parse(maxyear)) {
                MessageBox.Show("选择的最小年必须小于选择的最大年！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            
            }
            fillTempTable(minyear,maxyear);
           Frm_ArrearYearsSumRep frmAreearYearsSumRep = new Frm_ArrearYearsSumRep(minyear,maxyear);
            frmAreearYearsSumRep.Show();
            //Frm_ArrearSumRep frmArrearSumRep = new Frm_ArrearSumRep(year);
            //frmArrearSumRep.Show();7
        }


        public void fillTempTable(string minyear, string maxyear)
        {
            string VolumeNo = "";
            string strSql = "";
           // string insertyearsql = "";
            List<string> strSQL = new List<string>();
            DataTable dt = null;
            try
            {
                strSql = "Delete From  Temp_ArrearYearsSum";
                SQLUtl.ExecuteSql(strSql);          
                for (int k = int.Parse(minyear);k <= int.Parse(maxyear); k++)
                {
                  //  MessageBox.Show("k:"+k);
                    strSql = "Select VolumeNo From V_LotteArrear where year(CountFeeDate)='" + k.ToString() + "' order by VolumeNo";
                    dt = SQLUtl.Query(strSql).Tables["dataSet"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strSql = "insert into Temp_ArrearYearsSum(VolumeNo,Year) values('" + dt.Rows[i]["VolumeNo"].ToString() + "','" + k.ToString() + "')";
                        if (!VolumeNo.Equals(dt.Rows[i]["VolumeNo"].ToString()))
                        {
                            SQLUtl.ExecuteSql(strSql);
                            VolumeNo = dt.Rows[i]["VolumeNo"].ToString();

                        }
                       
                    }

                    
                    for (int i = 1; i <= 12; i++)
                    {
                        strSql = "Select Sum(TotalMoney) As TotalMoneySum,VolumeNo From V_LotteArrear Where year(CountFeeDate)='" + k.ToString() + "' and month(CountFeeDate)='" + i + "' Group By VolumeNo";
                        dt = SQLUtl.Query(strSql).Tables["dataSet"];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 1)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set JanArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='"+k +"'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 2)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set FebArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 3)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set MarArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 4)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set AprArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 5)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set MayArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 6)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set JunArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 7)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set JulArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 8)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set AugArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 9)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set SepArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 10)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set OctArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 11)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set NovArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            else if (i == 12)
                            {
                                strSql = "Update Temp_ArrearYearsSum Set DecArrearMoney = '" + dt.Rows[j]["TotalMoneySum"].ToString() + "' Where VolumeNo = '" + dt.Rows[j]["VolumeNo"].ToString() + "'and Year='" + k + "'";
                                strSQL.Add(strSql);
                            }
                            
                        }
                        
                        strSQL.Add(strSql);
                        
                    }
                  //  MessageBox.Show(strSQL.ElementAt(0));
                    SQLUtl.ExecuteSqlTran(strSQL);
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


      
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    
     
    }
}
