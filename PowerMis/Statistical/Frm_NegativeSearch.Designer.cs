namespace PowerMis.Statistical
{
    partial class Frm_NegativeSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NegativeSearch));
            this.search_button = new System.Windows.Forms.Button();
            this.txtCustomerNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.CNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NegativeValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NegativeMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NegativeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPrintDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(472, 22);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(75, 23);
            this.search_button.TabIndex = 14;
            this.search_button.Text = "查询";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(110, 22);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Size = new System.Drawing.Size(110, 21);
            this.txtCustomerNo.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "用户编号";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrintDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustomerNo);
            this.groupBox1.Controls.Add(this.search_button);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(630, 60);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvInfo);
            this.groupBox2.Location = new System.Drawing.Point(22, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 234);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询结果";
            // 
            // dgvInfo
            // 
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNo,
            this.CustomerName,
            this.NegativeValue,
            this.NegativeMoney,
            this.NegativeDate,
            this.PrintDate});
            this.dgvInfo.Location = new System.Drawing.Point(6, 20);
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.RowTemplate.Height = 23;
            this.dgvInfo.Size = new System.Drawing.Size(644, 208);
            this.dgvInfo.TabIndex = 0;
            // 
            // CNo
            // 
            this.CNo.DataPropertyName = "CustomerNo";
            this.CNo.HeaderText = "用户编号";
            this.CNo.Name = "CNo";
            this.CNo.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "用户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // NegativeValue
            // 
            this.NegativeValue.DataPropertyName = "NegativeValue";
            this.NegativeValue.HeaderText = "冲抵电量";
            this.NegativeValue.Name = "NegativeValue";
            this.NegativeValue.ReadOnly = true;
            // 
            // NegativeMoney
            // 
            this.NegativeMoney.DataPropertyName = "NegativeMoney";
            this.NegativeMoney.HeaderText = "冲抵金额";
            this.NegativeMoney.Name = "NegativeMoney";
            this.NegativeMoney.ReadOnly = true;
            // 
            // NegativeDate
            // 
            this.NegativeDate.DataPropertyName = "NegativeDate";
            this.NegativeDate.HeaderText = "冲抵月份";
            this.NegativeDate.Name = "NegativeDate";
            this.NegativeDate.ReadOnly = true;
            // 
            // PrintDate
            // 
            this.PrintDate.DataPropertyName = "PrintDate";
            this.PrintDate.HeaderText = "打印日期";
            this.PrintDate.Name = "PrintDate";
            this.PrintDate.ReadOnly = true;
            // 
            // txtPrintDate
            // 
            this.txtPrintDate.Location = new System.Drawing.Point(315, 22);
            this.txtPrintDate.Name = "txtPrintDate";
            this.txtPrintDate.Size = new System.Drawing.Size(110, 21);
            this.txtPrintDate.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(246, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "打印日期";
            // 
            // Frm_NegativeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 346);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_NegativeSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "负数发票查询";
            this.Load += new System.EventHandler(this.Frm_NegativeSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NegativeValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn NegativeMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn NegativeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintDate;
        private System.Windows.Forms.TextBox txtPrintDate;
        private System.Windows.Forms.Label label2;
    }
}