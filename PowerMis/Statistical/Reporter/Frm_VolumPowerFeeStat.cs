using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_VolumPowerFeeStat : Form
    {
        public Frm_VolumPowerFeeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] date = dateTimePicker1.Value.ToShortDateString().Split('-');
            int chracter = cbxChracter.SelectedIndex;
            Frm_VolumPowerFeeRep frmVolumPowerFeeRep = new Frm_VolumPowerFeeRep(date, chracter);
            frmVolumPowerFeeRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_VolumPowerFeeStat_Load(object sender, EventArgs e)
        {
            cbxChracter.Items.Add("城网");
            cbxChracter.Items.Add("农网");
            cbxChracter.SelectedIndex = 0;
        }
    }
}
