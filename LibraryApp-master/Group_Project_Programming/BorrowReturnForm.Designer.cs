namespace LibraryForms
{
    partial class BorrowReturnForm
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
            this.components = new System.ComponentModel.Container();
            this.lbl_BIBorrowItem = new System.Windows.Forms.Label();
            this.btn_BIBack = new System.Windows.Forms.Button();
            this.btn_BIOk = new System.Windows.Forms.Button();
            this.lbl_BITitle = new System.Windows.Forms.Label();
            this.lbl_BIUser = new System.Windows.Forms.Label();
            this.cmb_BITitle = new System.Windows.Forms.ComboBox();
            this.cmb_BIUser = new System.Windows.Forms.ComboBox();
            this.rad_BRBorrow = new System.Windows.Forms.RadioButton();
            this.rad_BRReturn = new System.Windows.Forms.RadioButton();
            this.rad_BRRenew = new System.Windows.Forms.RadioButton();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_BIBorrowItem
            // 
            this.lbl_BIBorrowItem.AutoSize = true;
            this.lbl_BIBorrowItem.Font = new System.Drawing.Font("Stencil", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BIBorrowItem.Location = new System.Drawing.Point(-1, 38);
            this.lbl_BIBorrowItem.Name = "lbl_BIBorrowItem";
            this.lbl_BIBorrowItem.Size = new System.Drawing.Size(740, 76);
            this.lbl_BIBorrowItem.TabIndex = 0;
            this.lbl_BIBorrowItem.Text = "Borrow/Return Item";
            // 
            // btn_BIBack
            // 
            this.btn_BIBack.Location = new System.Drawing.Point(12, 12);
            this.btn_BIBack.Name = "btn_BIBack";
            this.btn_BIBack.Size = new System.Drawing.Size(75, 23);
            this.btn_BIBack.TabIndex = 1;
            this.btn_BIBack.Text = "Back";
            this.btn_BIBack.UseVisualStyleBackColor = true;
            this.btn_BIBack.Click += new System.EventHandler(this.btn_BIBack_Click);
            // 
            // btn_BIOk
            // 
            this.btn_BIOk.Location = new System.Drawing.Point(327, 357);
            this.btn_BIOk.Name = "btn_BIOk";
            this.btn_BIOk.Size = new System.Drawing.Size(75, 23);
            this.btn_BIOk.TabIndex = 2;
            this.btn_BIOk.Text = "Ok";
            this.btn_BIOk.UseVisualStyleBackColor = true;
            this.btn_BIOk.Click += new System.EventHandler(this.btn_BIOk_Click);
            // 
            // lbl_BITitle
            // 
            this.lbl_BITitle.AutoSize = true;
            this.lbl_BITitle.BackColor = System.Drawing.Color.White;
            this.lbl_BITitle.Location = new System.Drawing.Point(212, 189);
            this.lbl_BITitle.Name = "lbl_BITitle";
            this.lbl_BITitle.Size = new System.Drawing.Size(50, 13);
            this.lbl_BITitle.TabIndex = 4;
            this.lbl_BITitle.Text = "Item Title";
            this.lbl_BITitle.Click += new System.EventHandler(this.lbl_BITitle_Click);
            // 
            // lbl_BIUser
            // 
            this.lbl_BIUser.AutoSize = true;
            this.lbl_BIUser.BackColor = System.Drawing.Color.White;
            this.lbl_BIUser.Location = new System.Drawing.Point(231, 215);
            this.lbl_BIUser.Name = "lbl_BIUser";
            this.lbl_BIUser.Size = new System.Drawing.Size(29, 13);
            this.lbl_BIUser.TabIndex = 9;
            this.lbl_BIUser.Text = "User";
            this.lbl_BIUser.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmb_BITitle
            // 
            this.cmb_BITitle.DataSource = this.itemBindingSource1;
            this.cmb_BITitle.FormattingEnabled = true;
            this.cmb_BITitle.Location = new System.Drawing.Point(266, 185);
            this.cmb_BITitle.Name = "cmb_BITitle";
            this.cmb_BITitle.Size = new System.Drawing.Size(252, 21);
            this.cmb_BITitle.TabIndex = 11;
            this.cmb_BITitle.Text = "<Select Title>";
            // 
            // cmb_BIUser
            // 
            this.cmb_BIUser.FormattingEnabled = true;
            this.cmb_BIUser.Location = new System.Drawing.Point(266, 212);
            this.cmb_BIUser.Name = "cmb_BIUser";
            this.cmb_BIUser.Size = new System.Drawing.Size(252, 21);
            this.cmb_BIUser.TabIndex = 12;
            this.cmb_BIUser.Text = "<Select User>";
            // 
            // rad_BRBorrow
            // 
            this.rad_BRBorrow.AutoSize = true;
            this.rad_BRBorrow.BackColor = System.Drawing.Color.White;
            this.rad_BRBorrow.Location = new System.Drawing.Point(266, 239);
            this.rad_BRBorrow.Name = "rad_BRBorrow";
            this.rad_BRBorrow.Size = new System.Drawing.Size(58, 17);
            this.rad_BRBorrow.TabIndex = 13;
            this.rad_BRBorrow.TabStop = true;
            this.rad_BRBorrow.Text = "Borrow";
            this.rad_BRBorrow.UseVisualStyleBackColor = false;
            // 
            // rad_BRReturn
            // 
            this.rad_BRReturn.AutoSize = true;
            this.rad_BRReturn.BackColor = System.Drawing.Color.White;
            this.rad_BRReturn.Location = new System.Drawing.Point(362, 239);
            this.rad_BRReturn.Name = "rad_BRReturn";
            this.rad_BRReturn.Size = new System.Drawing.Size(57, 17);
            this.rad_BRReturn.TabIndex = 13;
            this.rad_BRReturn.TabStop = true;
            this.rad_BRReturn.Text = "Return";
            this.rad_BRReturn.UseVisualStyleBackColor = false;
            // 
            // rad_BRRenew
            // 
            this.rad_BRRenew.AutoSize = true;
            this.rad_BRRenew.BackColor = System.Drawing.Color.White;
            this.rad_BRRenew.Location = new System.Drawing.Point(459, 239);
            this.rad_BRRenew.Name = "rad_BRRenew";
            this.rad_BRRenew.Size = new System.Drawing.Size(59, 17);
            this.rad_BRRenew.TabIndex = 13;
            this.rad_BRRenew.TabStop = true;
            this.rad_BRRenew.Text = "Renew";
            this.rad_BRRenew.UseVisualStyleBackColor = false;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(Item);
            // 
            // itemBindingSource1
            // 
            this.itemBindingSource1.DataSource = typeof(Item);
            // 
            // BorrowReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.rad_BRRenew);
            this.Controls.Add(this.rad_BRReturn);
            this.Controls.Add(this.rad_BRBorrow);
            this.Controls.Add(this.cmb_BIUser);
            this.Controls.Add(this.cmb_BITitle);
            this.Controls.Add(this.lbl_BIUser);
            this.Controls.Add(this.lbl_BITitle);
            this.Controls.Add(this.btn_BIOk);
            this.Controls.Add(this.btn_BIBack);
            this.Controls.Add(this.lbl_BIBorrowItem);
            this.Name = "BorrowReturnForm";
            this.Text = "BorrowReturnForm";
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_BIBorrowItem;
        private System.Windows.Forms.Button btn_BIBack;
        private System.Windows.Forms.Button btn_BIOk;
        private System.Windows.Forms.Label lbl_BITitle;
        private System.Windows.Forms.Label lbl_BIUser;
        private System.Windows.Forms.ComboBox cmb_BITitle;
        private System.Windows.Forms.ComboBox cmb_BIUser;
        private System.Windows.Forms.RadioButton rad_BRBorrow;
        private System.Windows.Forms.RadioButton rad_BRReturn;
        private System.Windows.Forms.RadioButton rad_BRRenew;
        private System.Windows.Forms.BindingSource itemBindingSource1;
        private System.Windows.Forms.BindingSource itemBindingSource;
    }
}