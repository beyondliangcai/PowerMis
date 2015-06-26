using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBUtility;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_LinePowerFeeStat : Form
    {
        public Frm_LinePowerFeeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            SqlParameter[] sqlParams1 = { new SqlParameter("@selectMonth", time) };
            SqlParameter[] sqlParams2 = { new SqlParameter("@selectMonth", time) };
            try
            {
                SQLUtl.RunProcedure("dt_ClacLineMonthFee", CommandType.StoredProcedure, "dataSet", sqlParams1);
                SQLUtl.RunProcedure("dt_ClacLineYearFee", CommandType.StoredProcedure, "dataSet", sqlParams2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            string selectMonth = str[1];
            Frm_LinePowerFeeRep frmLinePowerFeeRep = new Frm_LinePowerFeeRep(selectMonth,time);
            frmLinePowerFeeRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
