namespace PowerMis.Statistical
{
    partial class Frm_CityInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CityInvoice));
            this.label3 = new System.Windows.Forms.Label();
            this.用户号 = new System.Windows.Forms.Label();
            this.lbxCustomerList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxBookNo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CustomerNopayList = new System.Windows.Forms.CheckedListBox();
            this.txtLastBalance = new System.Windows.Forms.TextBox();
            this.txtAnnex = new System.Windows.Forms.TextBox();
            this.txtThisBalance = new System.Windows.Forms.TextBox();
            this.txtFactFee = new System.Windows.Forms.TextBox();
            this.txtPreFee = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustomerNo = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrinterSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPageSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrintView = new System.Windows.Forms.ToolStripButton();
            this.btconfirm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(107, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "客户姓名";
            // 
            // 用户号
            // 
            this.用户号.AutoSize = true;
            this.用户号.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.用户号.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.用户号.Location = new System.Drawing.Point(6, 69);
            this.用户号.Name = "用户号";
            this.用户号.Size = new System.Drawing.Size(63, 14);
            this.用户号.TabIndex = 4;
            this.用户号.Text = "客户编号";
            // 
            // lbxCustomerList
            // 
            this.lbxCustomerList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbxCustomerList.FormattingEnabled = true;
            this.lbxCustomerList.ItemHeight = 12;
            this.lbxCustomerList.Location = new System.Drawing.Point(6, 88);
            this.lbxCustomerList.Name = "lbxCustomerList";
            this.lbxCustomerList.Size = new System.Drawing.Size(215, 316);
            this.lbxCustomerList.TabIndex = 3;
            this.lbxCustomerList.SelectedIndexChanged += new System.EventHandler(this.lbxCustomerList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "账本号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxBookNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.用户号);
            this.groupBox1.Controls.Add(this.lbxCustomerList);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 414);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户列表";
            // 
            // cbxBookNo
            // 
            this.cbxBookNo.FormattingEnabled = true;
            this.cbxBookNo.ItemHeight = 12;
            this.cbxBookNo.Location = new System.Drawing.Point(6, 41);
            this.cbxBookNo.Name = "cbxBookNo";
            this.cbxBookNo.Size = new System.Drawing.Size(215, 20);
            this.cbxBookNo.TabIndex = 5;
            this.cbxBookNo.SelectedIndexChanged += new System.EventHandler(this.cbxBookNo_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txtLastBalance);
            this.panel1.Controls.Add(this.txtAnnex);
            this.panel1.Controls.Add(this.txtThisBalance);
            this.panel1.Controls.Add(this.txtFactFee);
            this.panel1.Controls.Add(this.txtPreFee);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCustomerNo);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(271, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 386);
            this.panel1.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CustomerNopayList);
            this.groupBox2.Location = new System.Drawing.Point(3, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 146);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户欠费记录";
            // 
            // CustomerNopayList
            // 
            this.CustomerNopayList.FormattingEnabled = true;
            this.CustomerNopayList.Location = new System.Drawing.Point(6, 20);
            this.CustomerNopayList.Name = "CustomerNopayList";
            this.CustomerNopayList.Size = new System.Drawing.Size(317, 116);
            this.CustomerNopayList.TabIndex = 0;
            this.CustomerNopayList.SelectedIndexChanged += new System.EventHandler(this.CustomerNopayList_SelectedIndexChanged);
            // 
            // txtLastBalance
            // 
            this.txtLastBalance.Enabled = false;
            this.txtLastBalance.Location = new System.Drawing.Point(113, 111);
            this.txtLastBalance.Name = "txtLastBalance";
            this.txtLastBalance.Size = new System.Drawing.Size(112, 21);
            this.txtLastBalance.TabIndex = 4;
            // 
            // txtAnnex
            // 
            this.txtAnnex.Enabled = false;
            this.txtAnnex.Location = new System.Drawing.Point(113, 208);
            this.txtAnnex.Name = "txtAnnex";
            this.txtAnnex.Size = new System.Drawing.Size(112, 21);
            this.txtAnnex.TabIndex = 4;
            // 
            // txtThisBalance
            // 
            this.txtThisBalance.Enabled = false;
            this.txtThisBalance.Location = new System.Drawing.Point(113, 176);
            this.txtThisBalance.Name = "txtThisBalance";
            this.txtThisBalance.Size = new System.Drawing.Size(112, 21);
            this.txtThisBalance.TabIndex = 4;
            // 
            // txtFactFee
            // 
            this.txtFactFee.Location = new System.Drawing.Point(113, 144);
            this.txtFactFee.Name = "txtFactFee";
            this.txtFactFee.Size = new System.Drawing.Size(112, 21);
            this.txtFactFee.TabIndex = 4;
            this.txtFactFee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFactFee_KeyDown);
            // 
            // txtPreFee
            // 
            this.txtPreFee.Enabled = false;
            this.txtPreFee.Location = new System.Drawing.Point(113, 79);
            this.txtPreFee.Name = "txtPreFee";
            this.txtPreFee.Size = new System.Drawing.Size(112, 21);
            this.txtPreFee.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(25, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "发票日期:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(25, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "客户编号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(25, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "上次预存:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(25, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "违约金:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(25, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "本次结余:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(25, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "实收金额:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "应收金额:";
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(113, 48);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Size = new System.Drawing.Size(112, 21);
            this.txtCustomerNo.TabIndex = 3;
            this.txtCustomerNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerNo_KeyDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(406, 441);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(524, 441);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrinterSet,
            this.toolStripSeparator1,
            this.tsbPageSet,
            this.toolStripSeparator2,
            this.tsbPrintView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(628, 35);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPrinterSet
            // 
            this.tsbPrinterSet.AutoSize = false;
            this.tsbPrinterSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrinterSet.Image = global::PowerMis.Properties.Resources.printer1;
            this.tsbPrinterSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrinterSet.Name = "tsbPrinterSet";
            this.tsbPrinterSet.Size = new System.Drawing.Size(35, 32);
            this.tsbPrinterSet.Text = "toolStripButton1";
            this.tsbPrinterSet.ToolTipText = "打印机设置";
            this.tsbPrinterSet.Click += new System.EventHandler(this.tsbPrinterSet_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbPageSet
            // 
            this.tsbPageSet.AutoSize = false;
            this.tsbPageSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPageSet.Image = global::PowerMis.Properties.Resources.pageset;
            this.tsbPageSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPageSet.Name = "tsbPageSet";
            this.tsbPageSet.Size = new System.Drawing.Size(35, 32);
            this.tsbPageSet.Text = "toolStripButton2";
            this.tsbPageSet.ToolTipText = "页面设置";
            this.tsbPageSet.Click += new System.EventHandler(this.tsbPageSet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbPrintView
            // 
            this.tsbPrintView.AutoSize = false;
            this.tsbPrintView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrintView.Image = global::PowerMis.Properties.Resources.printview;
            this.tsbPrintView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrintView.Name = "tsbPrintView";
            this.tsbPrintView.Size = new System.Drawing.Size(35, 32);
            this.tsbPrintView.Text = "toolStripButton3";
            this.tsbPrintView.ToolTipText = "打印预览";
            this.tsbPrintView.Click += new System.EventHandler(this.tsbPrintView_Click);
            // 
            // btconfirm
            // 
            this.btconfirm.Location = new System.Drawing.Point(281, 441);
            this.btconfirm.Name = "btconfirm";
            this.btconfirm.Size = new System.Drawing.Size(75, 23);
            this.btconfirm.TabIndex = 19;
            this.btconfirm.Text = "确定";
            this.btconfirm.UseVisualStyleBackColor = true;
            this.btconfirm.Click += new System.EventHandler(this.btconfirm_Click);
            // 
            // Frm_CityInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 495);
            this.Controls.Add(this.btconfirm);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_CityInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "城网发票";
            this.Load += new System.EventHandler(this.Frm_CityInvoice_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label 用户号;
        private System.Windows.Forms.ListBox lbxCustomerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtPreFee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.TextBox txtLastBalance;
        private System.Windows.Forms.TextBox txtAnnex;
        private System.Windows.Forms.TextBox txtThisBalance;
        private System.Windows.Forms.TextBox txtFactFee;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox CustomerNopayList;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxBookNo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrinterSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPageSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPrintView;
        private System.Windows.Forms.Button btconfirm;

    }
}