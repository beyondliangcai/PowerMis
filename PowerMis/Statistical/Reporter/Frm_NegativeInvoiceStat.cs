using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace PowerMis.Statistical.Reporter
{
    public partial class Frm_NegativeInvoiceStat : Form
    {
        public Frm_NegativeInvoiceStat()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] date = dateTimePicker1.Value.ToShortDateString().Split('-');
            Frm_NegativeInvoiceRep frmNegativeInvoiceRep = new Frm_NegativeInvoiceRep(date);
            frmNegativeInvoiceRep.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
