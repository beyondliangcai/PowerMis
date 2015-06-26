using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtility;
using Common;
using System.Data.SqlClient;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_RuralOweStat : Form
    {
        public Frm_RuralOweStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] str = dateTimePicker1.Value.ToShortDateString().Split('-');
            string date = str[0] + "-" + str[1] + "-01";
            string printMan = Constant.LoginUser.UserName;
            string type = "农网";
            float money = 0;
            SqlParameter[] sqlParams = { new SqlParameter("@fDate", date), new SqlParameter("@fType", type), SQLUtl.MakeOutParam("@Deposit", SqlDbType.Float, 0) };
            try
            {
                string deposit = SQLUtl.ExecuteProcedure("gaoxiong_pDeposit", sqlParams, "@Deposit");
                money = float.Parse(deposit);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "111");
                return;
            }
            Frm_RuralOweRep frmRuralOweRep = new Frm_RuralOweRep(money,date);
            frmRuralOweRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
