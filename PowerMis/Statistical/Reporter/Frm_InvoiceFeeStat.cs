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
    public partial class Frm_InvoiceFeeStat : Form
    {
        public Frm_InvoiceFeeStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] date = dateTimePicker1.Value.ToShortDateString().Split('-');
            Frm_InvoiceFeeRep frmInvoiceFeeRep = new Frm_InvoiceFeeRep(date);
            frmInvoiceFeeRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
