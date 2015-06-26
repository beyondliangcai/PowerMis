namespace PowerMis.BasicInfo
{
    partial class Frm_LineInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LineInfo));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LineInfoMaintain = new System.Windows.Forms.TabPage();
            this.CloseInfo = new System.Windows.Forms.Button();
            this.DeleteInfo = new System.Windows.Forms.Button();
            this.UpdateInfo = new System.Windows.Forms.Button();
            this.PowerPlace = new System.Windows.Forms.ComboBox();
            this.ReportOrder = new System.Windows.Forms.TextBox();
            this.LineName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LineList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddLineInfo = new System.Windows.Forms.TabPage();
            this.ReportOrder_add = new System.Windows.Forms.TextBox();
            this.PowerStation_add = new System.Windows.Forms.ComboBox();
            this.LineName_add = new System.Windows.Forms.TextBox();
            this.LineNum_add = new System.Windows.Forms.TextBox();
            this.Close_add = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.LineInfoMaintain.SuspendLayout();
            this.AddLineInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LineInfoMaintain);
            this.tabControl1.Controls.Add(this.AddLineInfo);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(385, 290);
            this.tabControl1.TabIndex = 0;
            // 
            // LineInfoMaintain
            // 
            this.LineInfoMaintain.Controls.Add(this.CloseInfo);
            this.LineInfoMaintain.Controls.Add(this.DeleteInfo);
            this.LineInfoMaintain.Controls.Add(this.UpdateInfo);
            this.LineInfoMaintain.Controls.Add(this.PowerPlace);
            this.LineInfoMaintain.Controls.Add(this.ReportOrder);
            this.LineInfoMaintain.Controls.Add(this.LineName);
            this.LineInfoMaintain.Controls.Add(this.label4);
            this.LineInfoMaintain.Controls.Add(this.label3);
            this.LineInfoMaintain.Controls.Add(this.label2);
            this.LineInfoMaintain.Controls.Add(this.LineList);
            this.LineInfoMaintain.Controls.Add(this.label1);
            this.LineInfoMaintain.Location = new System.Drawing.Point(4, 21);
            this.LineInfoMaintain.Name = "LineInfoMaintain";
            this.LineInfoMaintain.Padding = new System.Windows.Forms.Padding(3);
            this.LineInfoMaintain.Size = new System.Drawing.Size(377, 265);
            this.LineInfoMaintain.TabIndex = 0;
            this.LineInfoMaintain.Text = "线区信息维护";
            this.LineInfoMaintain.UseVisualStyleBackColor = true;
            // 
            // CloseInfo
            // 
            this.CloseInfo.Location = new System.Drawing.Point(278, 198);
            this.CloseInfo.Name = "CloseInfo";
            this.CloseInfo.Size = new System.Drawing.Size(75, 23);
            this.CloseInfo.TabIndex = 11;
            this.CloseInfo.Text = "关闭";
            this.CloseInfo.UseVisualStyleBackColor = true;
            this.CloseInfo.Click += new System.EventHandler(this.CloseInfo_Click);
            // 
            // DeleteInfo
            // 
            this.DeleteInfo.Location = new System.Drawing.Point(197, 198);
            this.DeleteInfo.Name = "DeleteInfo";
            this.DeleteInfo.Size = new System.Drawing.Size(75, 23);
            this.DeleteInfo.TabIndex = 10;
            this.DeleteInfo.Text = "删除";
            this.DeleteInfo.UseVisualStyleBackColor = true;
            this.DeleteInfo.Click += new System.EventHandler(this.DeleteInfo_Click);
            // 
            // UpdateInfo
            // 
            this.UpdateInfo.Location = new System.Drawing.Point(116, 198);
            this.UpdateInfo.Name = "UpdateInfo";
            this.UpdateInfo.Size = new System.Drawing.Size(75, 23);
            this.UpdateInfo.TabIndex = 9;
            this.UpdateInfo.Text = "更新";
            this.UpdateInfo.UseVisualStyleBackColor = true;
            this.UpdateInfo.Click += new System.EventHandler(this.UpdateInfo_Click);
            // 
            // PowerPlace
            // 
            this.PowerPlace.FormattingEnabled = true;
            this.PowerPlace.Location = new System.Drawing.Point(222, 97);
            this.PowerPlace.Name = "PowerPlace";
            this.PowerPlace.Size = new System.Drawing.Size(100, 20);
            this.PowerPlace.TabIndex = 8;
            // 
            // ReportOrder
            // 
            this.ReportOrder.Location = new System.Drawing.Point(222, 143);
            this.ReportOrder.Name = "ReportOrder";
            this.ReportOrder.Size = new System.Drawing.Size(100, 21);
            this.ReportOrder.TabIndex = 7;
            // 
            // LineName
            // 
            this.LineName.Location = new System.Drawing.Point(222, 45);
            this.LineName.Name = "LineName";
            this.LineName.Size = new System.Drawing.Size(100, 21);
            this.LineName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "报表顺序";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "所属供电所";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "线区名称";
            // 
            // LineList
            // 
            this.LineList.FormattingEnabled = true;
            this.LineList.ItemHeight = 12;
            this.LineList.Location = new System.Drawing.Point(6, 31);
            this.LineList.Name = "LineList";
            this.LineList.Size = new System.Drawing.Size(89, 220);
            this.LineList.TabIndex = 1;
            this.LineList.SelectedIndexChanged += new System.EventHandler(this.LineList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线路";
            // 
            // AddLineInfo
            // 
            this.AddLineInfo.Controls.Add(this.ReportOrder_add);
            this.AddLineInfo.Controls.Add(this.PowerStation_add);
            this.AddLineInfo.Controls.Add(this.LineName_add);
            this.AddLineInfo.Controls.Add(this.LineNum_add);
            this.AddLineInfo.Controls.Add(this.Close_add);
            this.AddLineInfo.Controls.Add(this.AddButton);
            this.AddLineInfo.Controls.Add(this.label8);
            this.AddLineInfo.Controls.Add(this.label7);
            this.AddLineInfo.Controls.Add(this.label6);
            this.AddLineInfo.Controls.Add(this.label5);
            this.AddLineInfo.Location = new System.Drawing.Point(4, 21);
            this.AddLineInfo.Name = "AddLineInfo";
            this.AddLineInfo.Padding = new System.Windows.Forms.Padding(3);
            this.AddLineInfo.Size = new System.Drawing.Size(377, 265);
            this.AddLineInfo.TabIndex = 1;
            this.AddLineInfo.Text = "添加线区";
            this.AddLineInfo.UseVisualStyleBackColor = true;
            // 
            // ReportOrder_add
            // 
            this.ReportOrder_add.Location = new System.Drawing.Point(155, 163);
            this.ReportOrder_add.Name = "ReportOrder_add";
            this.ReportOrder_add.Size = new System.Drawing.Size(100, 21);
            this.ReportOrder_add.TabIndex = 9;
            // 
            // PowerStation_add
            // 
            this.PowerStation_add.FormattingEnabled = true;
            this.PowerStation_add.Location = new System.Drawing.Point(155, 124);
            this.PowerStation_add.Name = "PowerStation_add";
            this.PowerStation_add.Size = new System.Drawing.Size(100, 20);
            this.PowerStation_add.TabIndex = 8;
            // 
            // LineName_add
            // 
            this.LineName_add.Location = new System.Drawing.Point(155, 81);
            this.LineName_add.Name = "LineName_add";
            this.LineName_add.Size = new System.Drawing.Size(100, 21);
            this.LineName_add.TabIndex = 7;
            // 
            // LineNum_add
            // 
            this.LineNum_add.Location = new System.Drawing.Point(155, 39);
            this.LineNum_add.Name = "LineNum_add";
            this.LineNum_add.Size = new System.Drawing.Size(100, 21);
            this.LineNum_add.TabIndex = 6;
            // 
            // Close_add
            // 
            this.Close_add.Location = new System.Drawing.Point(180, 210);
            this.Close_add.Name = "Close_add";
            this.Close_add.Size = new System.Drawing.Size(75, 23);
            this.Close_add.TabIndex = 5;
            this.Close_add.Text = "关闭";
            this.Close_add.UseVisualStyleBackColor = true;
            this.Close_add.Click += new System.EventHandler(this.Close_add_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(86, 210);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 4;
            this.AddButton.Text = "添加";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "报表顺序";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "所属供电所";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "线路名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "线路代码";
            // 
            // Frm_LineInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 314);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_LineInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路信息";
            this.Load += new System.EventHandler(this.LineInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.LineInfoMaintain.ResumeLayout(false);
            this.LineInfoMaintain.PerformLayout();
            this.AddLineInfo.ResumeLayout(false);
            this.AddLineInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage LineInfoMaintain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage AddLineInfo;
        private System.Windows.Forms.Button CloseInfo;
        private System.Windows.Forms.Button DeleteInfo;
        private System.Windows.Forms.Button UpdateInfo;
        private System.Windows.Forms.ComboBox PowerPlace;
        private System.Windows.Forms.TextBox ReportOrder;
        private System.Windows.Forms.TextBox LineName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LineList;
        private System.Windows.Forms.TextBox ReportOrder_add;
        private System.Windows.Forms.ComboBox PowerStation_add;
        private System.Windows.Forms.TextBox LineName_add;
        private System.Windows.Forms.TextBox LineNum_add;
        private System.Windows.Forms.Button Close_add;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}