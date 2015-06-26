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
    public partial class Frm_RuralPowerFeeStat : Form
    {
        public Frm_RuralPowerFeeStat()
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
                SQLUtl.RunProcedure("dt_ClacRAreaMonthFee", CommandType.StoredProcedure, "dataSet", sqlParams1);
                SQLUtl.RunProcedure("dt_ClacRAreaYearFee1", CommandType.StoredProcedure, "dataSet", sqlParams2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            string selectMonth = str[1];
            Frm_RuralPowerFeeRep frmRuralPowerFeeRep = new Frm_RuralPowerFeeRep(selectMonth);
            frmRuralPowerFeeRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
