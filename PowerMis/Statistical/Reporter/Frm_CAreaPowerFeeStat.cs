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
    public partial class Frm_CAreaPowerFeeStat : Form
    {
        public Frm_CAreaPowerFeeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string top = "企业台区";
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string time = str[0] + "-" + str[1] + "-01";
            SqlParameter[] sqlParams = { new SqlParameter("@selectMonth", time) };
            try
            {
                SQLUtl.RunProcedure("dt_ClacCAreaMonthFee1", CommandType.StoredProcedure, "dataSet", sqlParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            string selectMonth = str[1];
            Frm_AreaPowerFeeRep frmAreaPowerFeeRep = new Frm_AreaPowerFeeRep(selectMonth,top);
            frmAreaPowerFeeRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
