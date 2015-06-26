namespace PowerMis.CustomInfo
{
    partial class ChangeSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSection));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CustomerList_old = new System.Windows.Forms.ListBox();
            this.BookType_old = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CustomerList_new = new System.Windows.Forms.ListBox();
            this.BookType_new = new System.Windows.Forms.ComboBox();
            this.NewBook = new System.Windows.Forms.CheckBox();
            this.UpdateSection = new System.Windows.Forms.Button();
            this.CloseForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewCuatomerNo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CustomerList_old);
            this.groupBox1.Controls.Add(this.BookType_old);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 443);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账簿信息";
            // 
            // CustomerList_old
            // 
            this.CustomerList_old.FormattingEnabled = true;
            this.CustomerList_old.ItemHeight = 12;
            this.CustomerList_old.Location = new System.Drawing.Point(6, 85);
            this.CustomerList_old.Name = "CustomerList_old";
            this.CustomerList_old.Size = new System.Drawing.Size(120, 352);
            this.CustomerList_old.TabIndex = 2;
            // 
            // BookType_old
            // 
            this.BookType_old.FormattingEnabled = true;
            this.BookType_old.Location = new System.Drawing.Point(6, 36);
            this.BookType_old.Name = "BookType_old";
            this.BookType_old.Size = new System.Drawing.Size(121, 20);
            this.BookType_old.TabIndex = 2;
            this.BookType_old.SelectedIndexChanged += new System.EventHandler(this.BookType_old_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CustomerList_new);
            this.groupBox2.Controls.Add(this.BookType_new);
            this.groupBox2.Location = new System.Drawing.Point(352, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 443);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "账簿信息";
            // 
            // CustomerList_new
            // 
            this.CustomerList_new.FormattingEnabled = true;
            this.CustomerList_new.ItemHeight = 12;
            this.CustomerList_new.Location = new System.Drawing.Point(16, 73);
            this.CustomerList_new.Name = "CustomerList_new";
            this.CustomerList_new.Size = new System.Drawing.Size(120, 364);
            this.CustomerList_new.TabIndex = 1;
            // 
            // BookType_new
            // 
            this.BookType_new.FormattingEnabled = true;
            this.BookType_new.Location = new System.Drawing.Point(16, 36);
            this.BookType_new.Name = "BookType_new";
            this.BookType_new.Size = new System.Drawing.Size(121, 20);
            this.BookType_new.TabIndex = 0;
            this.BookType_new.SelectedIndexChanged += new System.EventHandler(this.BookType_new_SelectedIndexChanged);
            // 
            // NewBook
            // 
            this.NewBook.AutoSize = true;
            this.NewBook.Location = new System.Drawing.Point(170, 85);
            this.NewBook.Name = "NewBook";
            this.NewBook.Size = new System.Drawing.Size(84, 16);
            this.NewBook.TabIndex = 2;
            this.NewBook.Text = "转到新账簿";
            this.NewBook.UseVisualStyleBackColor = true;
            this.NewBook.CheckedChanged += new System.EventHandler(this.NewBook_CheckedChanged);
            // 
            // UpdateSection
            // 
            this.UpdateSection.Location = new System.Drawing.Point(170, 199);
            this.UpdateSection.Name = "UpdateSection";
            this.UpdateSection.Size = new System.Drawing.Size(75, 23);
            this.UpdateSection.TabIndex = 2;
            this.UpdateSection.Text = "确认转册";
            this.UpdateSection.UseVisualStyleBackColor = true;
            this.UpdateSection.Click += new System.EventHandler(this.UpdateSection_Click);
            // 
            // CloseForm
            // 
            this.CloseForm.Location = new System.Drawing.Point(271, 199);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(75, 23);
            this.CloseForm.TabIndex = 3;
            this.CloseForm.Text = "关闭";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "新账户号";
            // 
            // txtNewCuatomerNo
            // 
            this.txtNewCuatomerNo.Location = new System.Drawing.Point(188, 142);
            this.txtNewCuatomerNo.Name = "txtNewCuatomerNo";
            this.txtNewCuatomerNo.Size = new System.Drawing.Size(125, 21);
            this.txtNewCuatomerNo.TabIndex = 5;
            // 
            // ChangeSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 467);
            this.Controls.Add(this.NewBook);
            this.Controls.Add(this.txtNewCuatomerNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.UpdateSection);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeSection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更换账簿";
            this.Load += new System.EventHandler(this.ChangeSection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox CustomerList_old;
        private System.Windows.Forms.ComboBox BookType_old;
        private System.Windows.Forms.ListBox CustomerList_new;
        private System.Windows.Forms.ComboBox BookType_new;
        private System.Windows.Forms.Button UpdateSection;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.CheckBox NewBook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewCuatomerNo;
    }
}