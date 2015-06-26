namespace PowerMis.Statistical
{
    partial class Frm_CityIvoicePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CityIvoicePrint));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxBookNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.用户号 = new System.Windows.Forms.Label();
            this.lbxCustomerList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotalmoney = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cityInvoicePrintMonth = new System.Windows.Forms.ComboBox();
            this.cityInvoicePrintYear = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
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
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxBookNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.用户号);
            this.groupBox1.Controls.Add(this.lbxCustomerList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 436);
            this.groupBox1.TabIndex = 14;
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
            this.lbxCustomerList.Size = new System.Drawing.Size(215, 340);
            this.lbxCustomerList.TabIndex = 3;
            this.lbxCustomerList.SelectedIndexChanged += new System.EventHandler(this.lbxCustomerList_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtTotalmoney);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cityInvoicePrintMonth);
            this.panel1.Controls.Add(this.cityInvoicePrintYear);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPrint);
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
            this.panel1.Location = new System.Drawing.Point(268, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 436);
            this.panel1.TabIndex = 15;
            // 
            // txtTotalmoney
            // 
            this.txtTotalmoney.Enabled = false;
            this.txtTotalmoney.Location = new System.Drawing.Point(113, 210);
            this.txtTotalmoney.Name = "txtTotalmoney";
            this.txtTotalmoney.ReadOnly = true;
            this.txtTotalmoney.Size = new System.Drawing.Size(112, 21);
            this.txtTotalmoney.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(24, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 22;
            this.label12.Text = "本月电费:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(25, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 14);
            this.label11.TabIndex = 21;
            this.label11.Text = "打印月份：";
            // 
            // cityInvoicePrintMonth
            // 
            this.cityInvoicePrintMonth.FormattingEnabled = true;
            this.cityInvoicePrintMonth.Location = new System.Drawing.Point(113, 93);
            this.cityInvoicePrintMonth.Name = "cityInvoicePrintMonth";
            this.cityInvoicePrintMonth.Size = new System.Drawing.Size(112, 20);
            this.cityInvoicePrintMonth.TabIndex = 20;
            // 
            // cityInvoicePrintYear
            // 
            this.cityInvoicePrintYear.FormattingEnabled = true;
            this.cityInvoicePrintYear.Location = new System.Drawing.Point(113, 49);
            this.cityInvoicePrintYear.Name = "cityInvoicePrintYear";
            this.cityInvoicePrintYear.Size = new System.Drawing.Size(112, 20);
            this.cityInvoicePrintYear.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(25, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 14);
            this.label10.TabIndex = 18;
            this.label10.Text = "打印年份：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(27, 403);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtLastBalance
            // 
            this.txtLastBalance.Enabled = false;
            this.txtLastBalance.Location = new System.Drawing.Point(113, 169);
            this.txtLastBalance.Name = "txtLastBalance";
            this.txtLastBalance.ReadOnly = true;
            this.txtLastBalance.Size = new System.Drawing.Size(112, 21);
            this.txtLastBalance.TabIndex = 4;
            // 
            // txtAnnex
            // 
            this.txtAnnex.Enabled = false;
            this.txtAnnex.Location = new System.Drawing.Point(113, 360);
            this.txtAnnex.Name = "txtAnnex";
            this.txtAnnex.ReadOnly = true;
            this.txtAnnex.Size = new System.Drawing.Size(112, 21);
            this.txtAnnex.TabIndex = 4;
            // 
            // txtThisBalance
            // 
            this.txtThisBalance.Enabled = false;
            this.txtThisBalance.Location = new System.Drawing.Point(113, 320);
            this.txtThisBalance.Name = "txtThisBalance";
            this.txtThisBalance.ReadOnly = true;
            this.txtThisBalance.Size = new System.Drawing.Size(112, 21);
            this.txtThisBalance.TabIndex = 4;
            // 
            // txtFactFee
            // 
            this.txtFactFee.Location = new System.Drawing.Point(113, 284);
            this.txtFactFee.Name = "txtFactFee";
            this.txtFactFee.ReadOnly = true;
            this.txtFactFee.Size = new System.Drawing.Size(112, 21);
            this.txtFactFee.TabIndex = 4;
            // 
            // txtPreFee
            // 
            this.txtPreFee.Enabled = false;
            this.txtPreFee.Location = new System.Drawing.Point(113, 251);
            this.txtPreFee.Name = "txtPreFee";
            this.txtPreFee.ReadOnly = true;
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
            this.label4.Location = new System.Drawing.Point(25, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "客户编号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(25, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "上次预存:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(24, 367);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "违约金:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(24, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "本次结余:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(24, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "实收金额:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "应收金额:";
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(113, 133);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.ReadOnly = true;
            this.txtCustomerNo.Size = new System.Drawing.Size(112, 21);
            this.txtCustomerNo.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // Frm_CityIvoicePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 458);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_CityIvoicePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "城网发票打印";
            this.Load += new System.EventHandler(this.Frm_CityIvoicePrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxBookNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label 用户号;
        private System.Windows.Forms.ListBox lbxCustomerList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLastBalance;
        private System.Windows.Forms.TextBox txtAnnex;
        private System.Windows.Forms.TextBox txtThisBalance;
        private System.Windows.Forms.TextBox txtFactFee;
        private System.Windows.Forms.TextBox txtPreFee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cityInvoicePrintYear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cityInvoicePrintMonth;
        private System.Windows.Forms.TextBox txtTotalmoney;
        private System.Windows.Forms.Label label12;
    }
}