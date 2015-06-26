namespace PowerMis.BasicInfo
{
    partial class Frm_TransformerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TransformerInfo));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.T_Close = new System.Windows.Forms.Button();
            this.T_Delete = new System.Windows.Forms.Button();
            this.T_Update = new System.Windows.Forms.Button();
            this.T_Lose_Type = new System.Windows.Forms.ComboBox();
            this.T_Name = new System.Windows.Forms.TextBox();
            this.T_Code = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.T_List = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.T_Close_Add = new System.Windows.Forms.Button();
            this.T_Add_Button = new System.Windows.Forms.Button();
            this.T_Lose_Add = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.T_Name_Add = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.T_Code_Add = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(369, 318);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.T_Close);
            this.tabPage1.Controls.Add(this.T_Delete);
            this.tabPage1.Controls.Add(this.T_Update);
            this.tabPage1.Controls.Add(this.T_Lose_Type);
            this.tabPage1.Controls.Add(this.T_Name);
            this.tabPage1.Controls.Add(this.T_Code);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.T_List);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(361, 293);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "变压器信息维护";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "变损类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "变压器名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "变压器号";
            // 
            // T_Close
            // 
            this.T_Close.Location = new System.Drawing.Point(278, 233);
            this.T_Close.Name = "T_Close";
            this.T_Close.Size = new System.Drawing.Size(75, 23);
            this.T_Close.TabIndex = 7;
            this.T_Close.Text = "关闭";
            this.T_Close.UseVisualStyleBackColor = true;
            this.T_Close.Click += new System.EventHandler(this.T_Close_Click);
            // 
            // T_Delete
            // 
            this.T_Delete.Location = new System.Drawing.Point(197, 233);
            this.T_Delete.Name = "T_Delete";
            this.T_Delete.Size = new System.Drawing.Size(75, 23);
            this.T_Delete.TabIndex = 6;
            this.T_Delete.Text = "删除";
            this.T_Delete.UseVisualStyleBackColor = true;
            this.T_Delete.Click += new System.EventHandler(this.T_Delete_Click);
            // 
            // T_Update
            // 
            this.T_Update.Location = new System.Drawing.Point(116, 233);
            this.T_Update.Name = "T_Update";
            this.T_Update.Size = new System.Drawing.Size(75, 23);
            this.T_Update.TabIndex = 5;
            this.T_Update.Text = "更新";
            this.T_Update.UseVisualStyleBackColor = true;
            this.T_Update.Click += new System.EventHandler(this.T_Update_Click);
            // 
            // T_Lose_Type
            // 
            this.T_Lose_Type.FormattingEnabled = true;
            this.T_Lose_Type.Location = new System.Drawing.Point(197, 164);
            this.T_Lose_Type.Name = "T_Lose_Type";
            this.T_Lose_Type.Size = new System.Drawing.Size(121, 20);
            this.T_Lose_Type.TabIndex = 4;
            // 
            // T_Name
            // 
            this.T_Name.Location = new System.Drawing.Point(197, 104);
            this.T_Name.Name = "T_Name";
            this.T_Name.Size = new System.Drawing.Size(121, 21);
            this.T_Name.TabIndex = 3;
            // 
            // T_Code
            // 
            this.T_Code.Location = new System.Drawing.Point(197, 52);
            this.T_Code.Name = "T_Code";
            this.T_Code.Size = new System.Drawing.Size(121, 21);
            this.T_Code.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "变压器列表";
            // 
            // T_List
            // 
            this.T_List.FormattingEnabled = true;
            this.T_List.ItemHeight = 12;
            this.T_List.Location = new System.Drawing.Point(6, 32);
            this.T_List.Name = "T_List";
            this.T_List.Size = new System.Drawing.Size(104, 256);
            this.T_List.TabIndex = 0;
            this.T_List.SelectedIndexChanged += new System.EventHandler(this.T_List_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.T_Close_Add);
            this.tabPage2.Controls.Add(this.T_Add_Button);
            this.tabPage2.Controls.Add(this.T_Lose_Add);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.T_Name_Add);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.T_Code_Add);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 293);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "添加变压器信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // T_Close_Add
            // 
            this.T_Close_Add.Location = new System.Drawing.Point(201, 227);
            this.T_Close_Add.Name = "T_Close_Add";
            this.T_Close_Add.Size = new System.Drawing.Size(75, 23);
            this.T_Close_Add.TabIndex = 7;
            this.T_Close_Add.Text = "关闭";
            this.T_Close_Add.UseVisualStyleBackColor = true;
            this.T_Close_Add.Click += new System.EventHandler(this.T_Close_Add_Click);
            // 
            // T_Add_Button
            // 
            this.T_Add_Button.Location = new System.Drawing.Point(52, 227);
            this.T_Add_Button.Name = "T_Add_Button";
            this.T_Add_Button.Size = new System.Drawing.Size(75, 23);
            this.T_Add_Button.TabIndex = 6;
            this.T_Add_Button.Text = "添加";
            this.T_Add_Button.UseVisualStyleBackColor = true;
            this.T_Add_Button.Click += new System.EventHandler(this.T_Add_Button_Click);
            // 
            // T_Lose_Add
            // 
            this.T_Lose_Add.FormattingEnabled = true;
            this.T_Lose_Add.Location = new System.Drawing.Point(135, 153);
            this.T_Lose_Add.Name = "T_Lose_Add";
            this.T_Lose_Add.Size = new System.Drawing.Size(121, 20);
            this.T_Lose_Add.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "变损类型";
            // 
            // T_Name_Add
            // 
            this.T_Name_Add.Location = new System.Drawing.Point(135, 92);
            this.T_Name_Add.Name = "T_Name_Add";
            this.T_Name_Add.Size = new System.Drawing.Size(121, 21);
            this.T_Name_Add.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "变压器名称";
            // 
            // T_Code_Add
            // 
            this.T_Code_Add.Location = new System.Drawing.Point(135, 35);
            this.T_Code_Add.Name = "T_Code_Add";
            this.T_Code_Add.Size = new System.Drawing.Size(121, 21);
            this.T_Code_Add.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "变压器号";
            // 
            // Frm_TransformerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 342);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_TransformerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变压器信息";
            this.Load += new System.EventHandler(this.TransformerInfo_Load);
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button T_Close;
        private System.Windows.Forms.Button T_Delete;
        private System.Windows.Forms.Button T_Update;
        private System.Windows.Forms.ComboBox T_Lose_Type;
        private System.Windows.Forms.TextBox T_Name;
        private System.Windows.Forms.TextBox T_Code;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox T_List;
        private System.Windows.Forms.Button T_Close_Add;
        private System.Windows.Forms.Button T_Add_Button;
        private System.Windows.Forms.ComboBox T_Lose_Add;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox T_Name_Add;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox T_Code_Add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}