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
    public partial class Frm_PowerFeeListStat : Form
    {
        public Frm_PowerFeeListStat()
        {
            InitializeComponent();
        }

        private void Frm_PowerFeeListStat_Load(object sender, EventArgs e)
        {
            string strSql = "Select Distinct Left(CustomerNo, 5) as volumNo From CustomerInfo Order By volumNo";
            DataTable dt = SQLUtl.Query(strSql).Tables["DataSet"];
            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                cbxVolumNo.Items.Add(dt.Rows[i]["volumNo"].ToString());
            }
            cbxVolumNo.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            string volumNo = cbxVolumNo.Text.Trim();
            fillTempTable(volumNo,time);
            string selectMonth = str[1];
            Frm_PowerFeeListRep frmPowerFeeListRep = new Frm_PowerFeeListRep(selectMonth, volumNo);
            frmPowerFeeListRep.Show();
        }
        /*
        private void fillTempTable(string volumNo, string time)
        {
            string[] CustomerNo = new string[3];
            string[] CustomerName  =new string[3];
            string[] StartCode = new string[3];
            string[] EndCode = new string[3];
            string[] CountFeeAmount = new string[3];
            string[] TotalFee = new string[3];
            string strSql;
            DataTable dt;
            string strSql1 = "Delete From  Temp_PowerFeeList";
            SQLUtl.ExecuteSql(strSql1);

            strSql = "Select CustomerNo, CustomerName, StartCode,EndCode,CountFeeAmount,TotalMoney"
                    + "  From CountFee Where CountFeeDate = '" + time + "' And Left(CustomerNo, 5) ='" + volumNo + "' Order By CustomerNo";
            dt = SQLUtl.Query(strSql).Tables["dataSet"];
            int i = 0;
            while (i < dt.Rows.Count)
            {
                CustomerNo[0] = dt.Rows[i]["CustomerNo"].ToString();
                CustomerName[0] = dt.Rows[i]["CustomerName"].ToString();
                StartCode[0] = String.Format("{0:###0}", dt.Rows[i]["StartCode"]);
                EndCode[0] = String.Format("{0:###0}", dt.Rows[i]["EndCode"]);
                CountFeeAmount[0] = String.Format("{0:###0}", dt.Rows[i]["CountFeeAmount"]);
                TotalFee[0] = String.Format("{0:###0.00}", dt.Rows[i]["TotalMoney"]);
                i++;
                if (i < dt.Rows.Count)
                {
                    CustomerNo[1] = dt.Rows[i]["CustomerNo"].ToString();
                    CustomerName[1] = dt.Rows[i]["CustomerName"].ToString();
                    StartCode[1] = String.Format("{0:###0}", dt.Rows[i]["StartCode"]);
                    EndCode[1] = String.Format("{0:###0}", dt.Rows[i]["EndCode"]);
                    CountFeeAmount[1] = String.Format("{0:###0}", dt.Rows[i]["CountFeeAmount"]);
                    TotalFee[1] = String.Format("{0:###0.00}", dt.Rows[i]["TotalMoney"]);
                    i++;
                    if (i < dt.Rows.Count)
                    {
                        CustomerNo[2] = dt.Rows[i]["CustomerNo"].ToString();
                        CustomerName[2] = dt.Rows[i]["CustomerName"].ToString();
                        StartCode[2] = String.Format("{0:###0}", dt.Rows[i]["StartCode"]);
                        EndCode[2] = String.Format("{0:###0}", dt.Rows[i]["EndCode"]);
                        CountFeeAmount[2] = String.Format("{0:###0}", dt.Rows[i]["CountFeeAmount"]);
                        TotalFee[2] = String.Format("{0:###0.00}", dt.Rows[i]["TotalMoney"]);
                        i++;
                    }
                }
                try
                {
                    strSql = "Insert Into Temp_PowerFeeList "
                            + "values('" + CustomerNo[0] + "', '" + CustomerName[0] + "', '" + CountFeeAmount[0] + "','" + TotalFee[0] + "','" + StartCode[0] + "','" + EndCode[0] + "', "
                            + " '" + CustomerNo[1] + "', '" + CustomerName[1] + "', '" + CountFeeAmount[1] + "','" + TotalFee[1] + "','" + StartCode[1] + "','" + EndCode[1] + "',"
                            + " '" + CustomerNo[2] + "', '" + CustomerName[2] + "', '" + CountFeeAmount[2] + "','" + TotalFee[2] + "','" + StartCode[2] + "','" + EndCode[2] + "')";
                    SQLUtl.ExecuteSql(strSql);
                    
                    for (int k = 0; k < 3; k++)
                    {
                        CustomerNo[k] = "";
                        CustomerName[k] = "";
                        StartCode[k] = "";
                        EndCode[k] = "";
                        CountFeeAmount[k] = "";
                        TotalFee[k] = "";
                    }
                }
                catch(Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString());
                    throw ex;
                }
            }
        }
        */
        private void fillTempTable(string volumNo, string time)
        {
            string strSql = "";
            string insertSql = "";
            string updateSql = "";
            
            int firstPageCount = 0;
            int nextPageCount = 0;
            int firstPageLoop = 0;
            int nextPageLoop = 0;
            int havePrintCount = 0;
            string[] customerNo = new string[20];
            string[] countfeeAmount = new string[20];
            string CustomerNo = "";
            string CustomerName = "";
            string StartCode = "";
            string EndCode = "";
            string CountFeeAmount = "";
            string TotalFee = "";
            int i = 0;
            int j = 0;

            strSql = "Delete From  Temp_PowerFeeList";
            SQLUtl.ExecuteSql(strSql);

            strSql = "Select CustomerNo, CustomerName, StartCode,EndCode,CountFeeAmount,TotalMoney"
                    + "  From CountFee Where CountFeeDate = '" + time + "' And Left(CustomerNo, 5) ='" + volumNo + "' Order By CustomerNo";
            DataTable dt = SQLUtl.Query(strSql).Tables["dataSet"];
            //报表第一页显示15行，以后每页显示19行
            firstPageLoop = 15;
            if (dt.Rows.Count < 45)
            {
                firstPageLoop = dt.Rows.Count / 3;
                if (nextPageLoop == 0)
                {
                    nextPageLoop = 1;
                }
            }
            //获取第一页第一排
            while (firstPageCount < firstPageLoop)
            {
                if (havePrintCount >= dt.Rows.Count)
                {
                    return;
                }
                CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                insertSql = "Insert Into Temp_PowerFeeList (CustomerNo1,CustomerName1,CountFeeAmount1,TotalMoney1,StartCode1,EndCode1)"
                            + "values('" + CustomerNo + "', '" + CustomerName + "', '" + CountFeeAmount + "','" + TotalFee + "','" + StartCode + "','" + EndCode + "' )";
                SQLUtl.ExecuteSql(insertSql);
                customerNo[i++] = CustomerNo;
                countfeeAmount[j++] = CountFeeAmount;
                havePrintCount++;
                firstPageCount++;
            }
            firstPageCount = 0;
            i = 0;
            j = 0;
            insertSql = "";
            //获取第一页第二排
            while (firstPageCount < firstPageLoop)
            {
                if (havePrintCount >= dt.Rows.Count)
                {
                    return;
                }
                CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                updateSql = "Update Temp_PowerFeeList set CustomerNo2='" + CustomerNo + "', CustomerName2='" + CustomerName + "', CountfeeAmount2='" + CountFeeAmount + "',TotalMoney2='" + TotalFee + "',StartCode2='" + StartCode + "',EndCode2='" + EndCode + "' where CustomerNo1='" + customerNo[i++] + "' and CountfeeAmount1='" + countfeeAmount[j++] + "'";
                SQLUtl.ExecuteSql(updateSql);
                havePrintCount++;
                firstPageCount++;
            }
            firstPageCount = 0;
            i = 0;
            j = 0;
            updateSql = "";
        
                        //获取第一页第三排
                        while (firstPageCount < firstPageLoop)
                        {
                            if (havePrintCount >= dt.Rows.Count)
                            {
                                return;
                            }
                            CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                            CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                            StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                            EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                            CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                            TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                            updateSql = "Update Temp_PowerFeeList set CustomerNo3='" + CustomerNo + "', CustomerName3='" + CustomerName + "', CountfeeAmount3='" + CountFeeAmount + "',TotalMoney3='" + TotalFee + "',StartCode3='" + StartCode + "',EndCode3='" + EndCode + "' where CustomerNo1='" + customerNo[i++] + "'and CountfeeAmount1='" + countfeeAmount[j++] + "'";
                            SQLUtl.ExecuteSql(updateSql);
                            havePrintCount++;
                            firstPageCount++;
                        }
                        firstPageCount = 0;
                        i = 0;
                        j = 0;
                        updateSql = "";

                        //获取以后的页
                        while (havePrintCount < dt.Rows.Count)
                        {
                            for (int k = 0; k < 20; k++)
                            {
                                customerNo[k] = "";
                            }
                            nextPageLoop = 20;
                            if ((dt.Rows.Count - havePrintCount) < 57)
                            {
                                nextPageLoop = (dt.Rows.Count - havePrintCount) / 3;
                                if (nextPageLoop == 0)
                                {
                                    nextPageLoop = 1;
                                }
                            }

                            //获取第一排
                            while (nextPageCount < nextPageLoop)
                            {
                                if (havePrintCount >= dt.Rows.Count)
                                {
                                    return;
                                }
                                CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                                CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                                StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                                EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                                CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                                TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                                insertSql = "Insert Into Temp_PowerFeeList (CustomerNo1,CustomerName1,CountFeeAmount1,TotalMoney1,StartCode1,EndCode1)"
                                            + "values('" + CustomerNo + "', '" + CustomerName + "', '" + CountFeeAmount + "','" + TotalFee + "','" + StartCode + "','" + EndCode + "' )";
                                SQLUtl.ExecuteSql(insertSql);
                                customerNo[i++] = CustomerNo;
                                countfeeAmount[j++] = CountFeeAmount;
                                havePrintCount++;
                                nextPageCount++;
                            }
                            nextPageCount = 0;
                            i = 0;
                            j = 0;
                            insertSql = "";
                            //获取第二排
                            while (nextPageCount < nextPageLoop)
                            {
                                if (havePrintCount >= dt.Rows.Count)
                                {
                                    return;
                                }
                                CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                                CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                                StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                                EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                                CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                                TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                                updateSql = "Update Temp_PowerFeeList set CustomerNo2='" + CustomerNo + "', CustomerName2='" + CustomerName + "', CountfeeAmount2='" + CountFeeAmount + "',TotalMoney2='" + TotalFee + "',StartCode2='" + StartCode + "',EndCode2='" + EndCode + "' where CustomerNo1='" + customerNo[i++] + "'and CountfeeAmount1='" + countfeeAmount[j++] + "'";
                                SQLUtl.ExecuteSql(updateSql);
                                havePrintCount++;
                                nextPageCount++;
                            }
                            nextPageCount = 0;
                            i = 0;
                            j = 0;
                            updateSql = "";

                            //获取第三排
                            while (nextPageCount < nextPageLoop)
                            {
                                if (havePrintCount >= dt.Rows.Count)
                                {
                                    return;
                                }
                                CustomerNo = dt.Rows[havePrintCount]["CustomerNo"].ToString();
                                CustomerName = dt.Rows[havePrintCount]["CustomerName"].ToString();
                                StartCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["StartCode"]);
                                EndCode = String.Format("{0:###0}", dt.Rows[havePrintCount]["EndCode"]);
                                CountFeeAmount = String.Format("{0:###0}", dt.Rows[havePrintCount]["CountFeeAmount"]);
                                TotalFee = String.Format("{0:###0.00}", dt.Rows[havePrintCount]["TotalMoney"]);
                                updateSql = "Update Temp_PowerFeeList set CustomerNo3='" + CustomerNo + "', CustomerName3='" + CustomerName + "', CountfeeAmount3='" + CountFeeAmount + "',TotalMoney3='" + TotalFee + "',StartCode3='" + StartCode + "',EndCode3='" + EndCode + "' where CustomerNo1='" + customerNo[i++] + "'and CountfeeAmount1='" + countfeeAmount[j++] + "'";
                                SQLUtl.ExecuteSql(updateSql);
                                havePrintCount++;
                                nextPageCount++;
                            }
                            nextPageCount = 0;
                            i = 0;
                            j = 0;
                            updateSql = "";

                        }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
