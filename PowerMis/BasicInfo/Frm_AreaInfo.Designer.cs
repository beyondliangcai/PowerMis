namespace PowerMis.BasicInfo
{
    partial class Frm_AreaInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AreaInfo));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.AreaType = new System.Windows.Forms.ComboBox();
            this.AreaName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AreaList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AreaType_add = new System.Windows.Forms.ComboBox();
            this.AreaName_add = new System.Windows.Forms.TextBox();
            this.AreaNum_add = new System.Windows.Forms.TextBox();
            this.add_close_button = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabpage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabpage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(340, 283);
            this.tabControl1.TabIndex = 0;
            // 
            // tabpage1
            // 
            this.tabpage1.Controls.Add(this.button3);
            this.tabpage1.Controls.Add(this.button2);
            this.tabpage1.Controls.Add(this.button1);
            this.tabpage1.Controls.Add(this.AreaType);
            this.tabpage1.Controls.Add(this.AreaName);
            this.tabpage1.Controls.Add(this.label3);
            this.tabpage1.Controls.Add(this.label2);
            this.tabpage1.Controls.Add(this.AreaList);
            this.tabpage1.Controls.Add(this.label1);
            this.tabpage1.Location = new System.Drawing.Point(4, 21);
            this.tabpage1.Name = "tabpage1";
            this.tabpage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage1.Size = new System.Drawing.Size(332, 258);
            this.tabpage1.TabIndex = 0;
            this.tabpage1.Text = "台区信息维护";
            this.tabpage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(251, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(170, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AreaType
            // 
            this.AreaType.FormattingEnabled = true;
            this.AreaType.Location = new System.Drawing.Point(193, 118);
            this.AreaType.Name = "AreaType";
            this.AreaType.Size = new System.Drawing.Size(100, 20);
            this.AreaType.TabIndex = 5;
            // 
            // AreaName
            // 
            this.AreaName.Location = new System.Drawing.Point(193, 65);
            this.AreaName.Name = "AreaName";
            this.AreaName.Size = new System.Drawing.Size(100, 21);
            this.AreaName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "台区性质";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "台区名称";
            // 
            // AreaList
            // 
            this.AreaList.FormattingEnabled = true;
            this.AreaList.ItemHeight = 12;
            this.AreaList.Location = new System.Drawing.Point(6, 40);
            this.AreaList.Name = "AreaList";
            this.AreaList.Size = new System.Drawing.Size(77, 208);
            this.AreaList.TabIndex = 1;
            this.AreaList.SelectedIndexChanged += new System.EventHandler(this.AreaList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "台区代码";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AreaType_add);
            this.tabPage2.Controls.Add(this.AreaName_add);
            this.tabPage2.Controls.Add(this.AreaNum_add);
            this.tabPage2.Controls.Add(this.add_close_button);
            this.tabPage2.Controls.Add(this.add_button);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 258);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "台区信息添加";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AreaType_add
            // 
            this.AreaType_add.FormattingEnabled = true;
            this.AreaType_add.Location = new System.Drawing.Point(145, 126);
            this.AreaType_add.Name = "AreaType_add";
            this.AreaType_add.Size = new System.Drawing.Size(103, 20);
            this.AreaType_add.TabIndex = 7;
            // 
            // AreaName_add
            // 
            this.AreaName_add.Location = new System.Drawing.Point(148, 83);
            this.AreaName_add.Name = "AreaName_add";
            this.AreaName_add.Size = new System.Drawing.Size(100, 21);
            this.AreaName_add.TabIndex = 6;
            // 
            // AreaNum_add
            // 
            this.AreaNum_add.Location = new System.Drawing.Point(148, 36);
            this.AreaNum_add.Name = "AreaNum_add";
            this.AreaNum_add.Size = new System.Drawing.Size(100, 21);
            this.AreaNum_add.TabIndex = 5;
            // 
            // add_close_button
            // 
            this.add_close_button.Location = new System.Drawing.Point(191, 194);
            this.add_close_button.Name = "add_close_button";
            this.add_close_button.Size = new System.Drawing.Size(75, 23);
            this.add_close_button.TabIndex = 4;
            this.add_close_button.Text = "关闭";
            this.add_close_button.UseVisualStyleBackColor = true;
            this.add_close_button.Click += new System.EventHandler(this.add_close_button_Click);
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(59, 194);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(75, 23);
            this.add_button.TabIndex = 3;
            this.add_button.Text = "添加";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "台区性质";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "台区名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "台区代码";
            // 
            // Frm_AreaInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 298);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_AreaInfo";
            this.Text = "台区信息";
            this.Load += new System.EventHandler(this.Frm_AreaInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabpage1.ResumeLayout(false);
            this.tabpage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox AreaType;
        private System.Windows.Forms.TextBox AreaName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox AreaList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AreaType_add;
        private System.Windows.Forms.TextBox AreaName_add;
        private System.Windows.Forms.TextBox AreaNum_add;
        private System.Windows.Forms.Button add_close_button;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}