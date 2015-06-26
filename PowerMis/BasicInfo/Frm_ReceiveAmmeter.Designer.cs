namespace PowerMis
{
    partial class Frm_ReceiveAmmeter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ReceiveAmmeter));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.CloseForm = new System.Windows.Forms.Button();
            this.DeleteAmmeter = new System.Windows.Forms.Button();
            this.UpdateAmmeter = new System.Windows.Forms.Button();
            this.AmmeterMulti = new System.Windows.Forms.TextBox();
            this.AmmeterName = new System.Windows.Forms.TextBox();
            this.AmmeterNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AmmeterList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CloseForm_Add = new System.Windows.Forms.Button();
            this.AddAmmeter = new System.Windows.Forms.Button();
            this.AmmeterMulti_Add = new System.Windows.Forms.TextBox();
            this.AmmeterName_Add = new System.Windows.Forms.TextBox();
            this.AmmeterNo_Add = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 327);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.CloseForm);
            this.tabPage1.Controls.Add(this.DeleteAmmeter);
            this.tabPage1.Controls.Add(this.UpdateAmmeter);
            this.tabPage1.Controls.Add(this.AmmeterMulti);
            this.tabPage1.Controls.Add(this.AmmeterName);
            this.tabPage1.Controls.Add(this.AmmeterNo);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.AmmeterList);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(356, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "接收电表信息维护";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(54, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "名称";
            // 
            // CloseForm
            // 
            this.CloseForm.Location = new System.Drawing.Point(273, 230);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(75, 23);
            this.CloseForm.TabIndex = 10;
            this.CloseForm.Text = "关闭";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // DeleteAmmeter
            // 
            this.DeleteAmmeter.Location = new System.Drawing.Point(192, 230);
            this.DeleteAmmeter.Name = "DeleteAmmeter";
            this.DeleteAmmeter.Size = new System.Drawing.Size(75, 23);
            this.DeleteAmmeter.TabIndex = 9;
            this.DeleteAmmeter.Text = "删除";
            this.DeleteAmmeter.UseVisualStyleBackColor = true;
            this.DeleteAmmeter.Click += new System.EventHandler(this.DeleteAmmeter_Click);
            // 
            // UpdateAmmeter
            // 
            this.UpdateAmmeter.Location = new System.Drawing.Point(111, 230);
            this.UpdateAmmeter.Name = "UpdateAmmeter";
            this.UpdateAmmeter.Size = new System.Drawing.Size(75, 23);
            this.UpdateAmmeter.TabIndex = 8;
            this.UpdateAmmeter.Text = "更新";
            this.UpdateAmmeter.UseVisualStyleBackColor = true;
            this.UpdateAmmeter.Click += new System.EventHandler(this.UpdateAmmeter_Click);
            // 
            // AmmeterMulti
            // 
            this.AmmeterMulti.Location = new System.Drawing.Point(207, 158);
            this.AmmeterMulti.Name = "AmmeterMulti";
            this.AmmeterMulti.Size = new System.Drawing.Size(100, 21);
            this.AmmeterMulti.TabIndex = 7;
            // 
            // AmmeterName
            // 
            this.AmmeterName.Location = new System.Drawing.Point(207, 115);
            this.AmmeterName.Name = "AmmeterName";
            this.AmmeterName.Size = new System.Drawing.Size(100, 21);
            this.AmmeterName.TabIndex = 6;
            // 
            // AmmeterNo
            // 
            this.AmmeterNo.Location = new System.Drawing.Point(207, 71);
            this.AmmeterNo.Name = "AmmeterNo";
            this.AmmeterNo.Size = new System.Drawing.Size(100, 21);
            this.AmmeterNo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "电表倍率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "电表名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "电表号";
            // 
            // AmmeterList
            // 
            this.AmmeterList.FormattingEnabled = true;
            this.AmmeterList.ItemHeight = 12;
            this.AmmeterList.Location = new System.Drawing.Point(7, 34);
            this.AmmeterList.Name = "AmmeterList";
            this.AmmeterList.Size = new System.Drawing.Size(98, 256);
            this.AmmeterList.TabIndex = 1;
            this.AmmeterList.SelectedIndexChanged += new System.EventHandler(this.AmmeterList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代码";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CloseForm_Add);
            this.tabPage2.Controls.Add(this.AddAmmeter);
            this.tabPage2.Controls.Add(this.AmmeterMulti_Add);
            this.tabPage2.Controls.Add(this.AmmeterName_Add);
            this.tabPage2.Controls.Add(this.AmmeterNo_Add);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(356, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "添加接收电表信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CloseForm_Add
            // 
            this.CloseForm_Add.Location = new System.Drawing.Point(195, 225);
            this.CloseForm_Add.Name = "CloseForm_Add";
            this.CloseForm_Add.Size = new System.Drawing.Size(75, 23);
            this.CloseForm_Add.TabIndex = 7;
            this.CloseForm_Add.Text = "关闭";
            this.CloseForm_Add.UseVisualStyleBackColor = true;
            this.CloseForm_Add.Click += new System.EventHandler(this.CloseForm_Add_Click);
            // 
            // AddAmmeter
            // 
            this.AddAmmeter.Location = new System.Drawing.Point(87, 225);
            this.AddAmmeter.Name = "AddAmmeter";
            this.AddAmmeter.Size = new System.Drawing.Size(75, 23);
            this.AddAmmeter.TabIndex = 6;
            this.AddAmmeter.Text = "添加";
            this.AddAmmeter.UseVisualStyleBackColor = true;
            this.AddAmmeter.Click += new System.EventHandler(this.AddAmmeter_Click);
            // 
            // AmmeterMulti_Add
            // 
            this.AmmeterMulti_Add.Location = new System.Drawing.Point(170, 144);
            this.AmmeterMulti_Add.Name = "AmmeterMulti_Add";
            this.AmmeterMulti_Add.Size = new System.Drawing.Size(100, 21);
            this.AmmeterMulti_Add.TabIndex = 5;
            // 
            // AmmeterName_Add
            // 
            this.AmmeterName_Add.Location = new System.Drawing.Point(170, 94);
            this.AmmeterName_Add.Name = "AmmeterName_Add";
            this.AmmeterName_Add.Size = new System.Drawing.Size(100, 21);
            this.AmmeterName_Add.TabIndex = 4;
            // 
            // AmmeterNo_Add
            // 
            this.AmmeterNo_Add.Location = new System.Drawing.Point(170, 46);
            this.AmmeterNo_Add.Name = "AmmeterNo_Add";
            this.AmmeterNo_Add.Size = new System.Drawing.Size(100, 21);
            this.AmmeterNo_Add.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "电表倍率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "电表名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "电表号";
            // 
            // Frm_ReceiveAmmeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 352);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_ReceiveAmmeter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "接收电表信息";
            this.Load += new System.EventHandler(this.Frm_ReceiveAmmeter_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox AmmeterList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox AmmeterMulti;
        private System.Windows.Forms.TextBox AmmeterName;
        private System.Windows.Forms.TextBox AmmeterNo;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.Button DeleteAmmeter;
        private System.Windows.Forms.Button UpdateAmmeter;
        private System.Windows.Forms.Button CloseForm_Add;
        private System.Windows.Forms.Button AddAmmeter;
        private System.Windows.Forms.TextBox AmmeterMulti_Add;
        private System.Windows.Forms.TextBox AmmeterName_Add;
        private System.Windows.Forms.TextBox AmmeterNo_Add;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
    }
}