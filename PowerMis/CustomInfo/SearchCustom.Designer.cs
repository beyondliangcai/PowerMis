namespace PowerMis.CustomInfo
{
    partial class SearchCustom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCustom));
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomInfoList = new System.Windows.Forms.ListBox();
            this.label47 = new System.Windows.Forms.Label();
            this.BookType = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomerName = new System.Windows.Forms.TextBox();
            this.search_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CommonInfo = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Countfeeinfo = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PriceInfo = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.otherInfo = new System.Windows.Forms.DataGridView();
            this.search_button2 = new System.Windows.Forms.Button();
            this.txtCustomerNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox16.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CommonInfo)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Countfeeinfo)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriceInfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.otherInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label2);
            this.groupBox16.Controls.Add(this.CustomInfoList);
            this.groupBox16.Controls.Add(this.label47);
            this.groupBox16.Controls.Add(this.BookType);
            this.groupBox16.Controls.Add(this.label48);
            this.groupBox16.Location = new System.Drawing.Point(12, 12);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(150, 545);
            this.groupBox16.TabIndex = 8;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "客户列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "序号";
            // 
            // CustomInfoList
            // 
            this.CustomInfoList.FormattingEnabled = true;
            this.CustomInfoList.ItemHeight = 12;
            this.CustomInfoList.Location = new System.Drawing.Point(6, 82);
            this.CustomInfoList.Name = "CustomInfoList";
            this.CustomInfoList.Size = new System.Drawing.Size(138, 448);
            this.CustomInfoList.TabIndex = 1;
            this.CustomInfoList.SelectedIndexChanged += new System.EventHandler(this.CustomInfoList_SelectedIndexChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(51, 65);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(53, 12);
            this.label47.TabIndex = 1;
            this.label47.Text = "客户编号";
            // 
            // BookType
            // 
            this.BookType.FormattingEnabled = true;
            this.BookType.Location = new System.Drawing.Point(6, 33);
            this.BookType.Name = "BookType";
            this.BookType.Size = new System.Drawing.Size(122, 20);
            this.BookType.TabIndex = 1;
            this.BookType.SelectedIndexChanged += new System.EventHandler(this.BookType_SelectedIndexChanged_1);
            this.BookType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BookType_KeyDown);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(26, 18);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(53, 12);
            this.label48.TabIndex = 1;
            this.label48.Text = "选择账本";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "按用户姓名";
            // 
            // CustomerName
            // 
            this.CustomerName.Location = new System.Drawing.Point(532, 12);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(100, 21);
            this.CustomerName.TabIndex = 10;
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(673, 10);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(75, 23);
            this.search_button.TabIndex = 11;
            this.search_button.Text = "查询";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CommonInfo);
            this.groupBox1.Location = new System.Drawing.Point(177, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 127);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "普通信息";
            // 
            // CommonInfo
            // 
            this.CommonInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CommonInfo.Location = new System.Drawing.Point(7, 13);
            this.CommonInfo.Name = "CommonInfo";
            this.CommonInfo.RowTemplate.Height = 23;
            this.CommonInfo.Size = new System.Drawing.Size(570, 108);
            this.CommonInfo.TabIndex = 0;
          //  this.CommonInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CommonInfo_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Countfeeinfo);
            this.groupBox2.Location = new System.Drawing.Point(177, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 116);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用电信息";
            // 
            // Countfeeinfo
            // 
            this.Countfeeinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Countfeeinfo.Location = new System.Drawing.Point(6, 12);
            this.Countfeeinfo.Name = "Countfeeinfo";
            this.Countfeeinfo.RowTemplate.Height = 23;
            this.Countfeeinfo.Size = new System.Drawing.Size(571, 98);
            this.Countfeeinfo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PriceInfo);
            this.groupBox3.Location = new System.Drawing.Point(177, 317);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(585, 118);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "电价信息";
            // 
            // PriceInfo
            // 
            this.PriceInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PriceInfo.Location = new System.Drawing.Point(6, 20);
            this.PriceInfo.Name = "PriceInfo";
            this.PriceInfo.RowTemplate.Height = 23;
            this.PriceInfo.Size = new System.Drawing.Size(571, 88);
            this.PriceInfo.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(680, 534);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.otherInfo);
            this.groupBox4.Location = new System.Drawing.Point(177, 441);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(584, 87);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "其他信息";
            // 
            // otherInfo
            // 
            this.otherInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.otherInfo.Location = new System.Drawing.Point(6, 19);
            this.otherInfo.Name = "otherInfo";
            this.otherInfo.RowTemplate.Height = 23;
            this.otherInfo.Size = new System.Drawing.Size(571, 62);
            this.otherInfo.TabIndex = 0;
            // 
            // search_button2
            // 
            this.search_button2.Location = new System.Drawing.Point(673, 42);
            this.search_button2.Name = "search_button2";
            this.search_button2.Size = new System.Drawing.Size(75, 23);
            this.search_button2.TabIndex = 20;
            this.search_button2.Text = "查询";
            this.search_button2.UseVisualStyleBackColor = true;
            this.search_button2.Click += new System.EventHandler(this.search_button2_Click);
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(532, 44);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Size = new System.Drawing.Size(100, 21);
            this.txtCustomerNo.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(461, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "按用户编号";
            // 
            // SearchCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 569);
            this.Controls.Add(this.search_button2);
            this.Controls.Add(this.txtCustomerNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.CustomerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox16);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchCustom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找客户信息";
            this.Load += new System.EventHandler(this.SearchCustom_Load);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CommonInfo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Countfeeinfo)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PriceInfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.otherInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.ListBox CustomInfoList;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.ComboBox BookType;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CustomerName;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView CommonInfo;
        private System.Windows.Forms.DataGridView PriceInfo;
        private System.Windows.Forms.DataGridView Countfeeinfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView otherInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button search_button2;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.Label label3;
    }
}