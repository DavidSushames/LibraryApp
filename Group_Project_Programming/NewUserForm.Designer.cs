namespace LibraryForms
{
    partial class NewUserForm
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
            this.lbl_NewUser = new System.Windows.Forms.Label();
            this.btn_NUBack = new System.Windows.Forms.Button();
            this.txt_NUFirstName = new System.Windows.Forms.TextBox();
            this.lbl_NUFirstName = new System.Windows.Forms.Label();
            this.lbl_NULastName = new System.Windows.Forms.Label();
            this.txt_NULastName = new System.Windows.Forms.TextBox();
            this.lbl_NUEmail = new System.Windows.Forms.Label();
            this.lbl_NUPhone = new System.Windows.Forms.Label();
            this.txt_NUEmail = new System.Windows.Forms.TextBox();
            this.txt_NUPhone = new System.Windows.Forms.TextBox();
            this.btn_NUOk = new System.Windows.Forms.Button();
            this.rad_NUStaff = new System.Windows.Forms.RadioButton();
            this.rad_NUStudent = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbl_NewUser
            // 
            this.lbl_NewUser.AutoSize = true;
            this.lbl_NewUser.Font = new System.Drawing.Font("Stencil", 72F);
            this.lbl_NewUser.Location = new System.Drawing.Point(109, 74);
            this.lbl_NewUser.Name = "lbl_NewUser";
            this.lbl_NewUser.Size = new System.Drawing.Size(513, 114);
            this.lbl_NewUser.TabIndex = 0;
            this.lbl_NewUser.Text = "New User";
            this.lbl_NewUser.Click += new System.EventHandler(this.lbl_NewUser_Click);
            // 
            // btn_NUBack
            // 
            this.btn_NUBack.Location = new System.Drawing.Point(12, 12);
            this.btn_NUBack.Name = "btn_NUBack";
            this.btn_NUBack.Size = new System.Drawing.Size(75, 23);
            this.btn_NUBack.TabIndex = 1;
            this.btn_NUBack.Text = "Back";
            this.btn_NUBack.UseVisualStyleBackColor = true;
            this.btn_NUBack.Click += new System.EventHandler(this.btn_NUBack_Click);
            // 
            // txt_NUFirstName
            // 
            this.txt_NUFirstName.Location = new System.Drawing.Point(306, 208);
            this.txt_NUFirstName.Name = "txt_NUFirstName";
            this.txt_NUFirstName.Size = new System.Drawing.Size(162, 20);
            this.txt_NUFirstName.TabIndex = 2;
            this.txt_NUFirstName.TextChanged += new System.EventHandler(this.txt_NUFirstName_TextChanged);
            // 
            // lbl_NUFirstName
            // 
            this.lbl_NUFirstName.AutoSize = true;
            this.lbl_NUFirstName.BackColor = System.Drawing.Color.White;
            this.lbl_NUFirstName.Location = new System.Drawing.Point(243, 211);
            this.lbl_NUFirstName.Name = "lbl_NUFirstName";
            this.lbl_NUFirstName.Size = new System.Drawing.Size(57, 13);
            this.lbl_NUFirstName.TabIndex = 3;
            this.lbl_NUFirstName.Text = "First Name";
            // 
            // lbl_NULastName
            // 
            this.lbl_NULastName.AutoSize = true;
            this.lbl_NULastName.BackColor = System.Drawing.Color.White;
            this.lbl_NULastName.Location = new System.Drawing.Point(243, 234);
            this.lbl_NULastName.Name = "lbl_NULastName";
            this.lbl_NULastName.Size = new System.Drawing.Size(58, 13);
            this.lbl_NULastName.TabIndex = 4;
            this.lbl_NULastName.Text = "Last Name";
            // 
            // txt_NULastName
            // 
            this.txt_NULastName.Location = new System.Drawing.Point(306, 231);
            this.txt_NULastName.Name = "txt_NULastName";
            this.txt_NULastName.Size = new System.Drawing.Size(162, 20);
            this.txt_NULastName.TabIndex = 5;
            this.txt_NULastName.TextChanged += new System.EventHandler(this.txt_NULastName_TextChanged);
            // 
            // lbl_NUEmail
            // 
            this.lbl_NUEmail.AutoSize = true;
            this.lbl_NUEmail.BackColor = System.Drawing.Color.White;
            this.lbl_NUEmail.Location = new System.Drawing.Point(269, 257);
            this.lbl_NUEmail.Name = "lbl_NUEmail";
            this.lbl_NUEmail.Size = new System.Drawing.Size(32, 13);
            this.lbl_NUEmail.TabIndex = 6;
            this.lbl_NUEmail.Text = "Email";
            // 
            // lbl_NUPhone
            // 
            this.lbl_NUPhone.AutoSize = true;
            this.lbl_NUPhone.BackColor = System.Drawing.Color.White;
            this.lbl_NUPhone.Location = new System.Drawing.Point(238, 280);
            this.lbl_NUPhone.Name = "lbl_NUPhone";
            this.lbl_NUPhone.Size = new System.Drawing.Size(63, 13);
            this.lbl_NUPhone.TabIndex = 7;
            this.lbl_NUPhone.Text = "Phone Num";
            // 
            // txt_NUEmail
            // 
            this.txt_NUEmail.Location = new System.Drawing.Point(306, 254);
            this.txt_NUEmail.Name = "txt_NUEmail";
            this.txt_NUEmail.Size = new System.Drawing.Size(162, 20);
            this.txt_NUEmail.TabIndex = 8;
            this.txt_NUEmail.TextChanged += new System.EventHandler(this.txt_NUEmail_TextChanged);
            // 
            // txt_NUPhone
            // 
            this.txt_NUPhone.Location = new System.Drawing.Point(306, 277);
            this.txt_NUPhone.Name = "txt_NUPhone";
            this.txt_NUPhone.Size = new System.Drawing.Size(162, 20);
            this.txt_NUPhone.TabIndex = 9;
            this.txt_NUPhone.TextChanged += new System.EventHandler(this.txt_NUPhone_TextChanged);
            // 
            // btn_NUOk
            // 
            this.btn_NUOk.Location = new System.Drawing.Point(341, 327);
            this.btn_NUOk.Name = "btn_NUOk";
            this.btn_NUOk.Size = new System.Drawing.Size(75, 23);
            this.btn_NUOk.TabIndex = 10;
            this.btn_NUOk.Text = "Ok";
            this.btn_NUOk.UseVisualStyleBackColor = true;
            this.btn_NUOk.Click += new System.EventHandler(this.btn_NUOk_Click);
            // 
            // rad_NUStaff
            // 
            this.rad_NUStaff.AutoSize = true;
            this.rad_NUStaff.BackColor = System.Drawing.Color.White;
            this.rad_NUStaff.Location = new System.Drawing.Point(324, 304);
            this.rad_NUStaff.Name = "rad_NUStaff";
            this.rad_NUStaff.Size = new System.Drawing.Size(47, 17);
            this.rad_NUStaff.TabIndex = 11;
            this.rad_NUStaff.TabStop = true;
            this.rad_NUStaff.Text = "Staff";
            this.rad_NUStaff.UseVisualStyleBackColor = false;
            this.rad_NUStaff.CheckedChanged += new System.EventHandler(this.rad_NUStaff_CheckedChanged);
            // 
            // rad_NUStudent
            // 
            this.rad_NUStudent.AutoSize = true;
            this.rad_NUStudent.BackColor = System.Drawing.Color.White;
            this.rad_NUStudent.Location = new System.Drawing.Point(377, 304);
            this.rad_NUStudent.Name = "rad_NUStudent";
            this.rad_NUStudent.Size = new System.Drawing.Size(62, 17);
            this.rad_NUStudent.TabIndex = 12;
            this.rad_NUStudent.TabStop = true;
            this.rad_NUStudent.Text = "Student";
            this.rad_NUStudent.UseVisualStyleBackColor = false;
            this.rad_NUStudent.CheckedChanged += new System.EventHandler(this.rad_NUStudent_CheckedChanged);
            // 
            // NewUserForm
            // 
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.rad_NUStudent);
            this.Controls.Add(this.rad_NUStaff);
            this.Controls.Add(this.btn_NUOk);
            this.Controls.Add(this.txt_NUPhone);
            this.Controls.Add(this.txt_NUEmail);
            this.Controls.Add(this.lbl_NUPhone);
            this.Controls.Add(this.lbl_NUEmail);
            this.Controls.Add(this.txt_NULastName);
            this.Controls.Add(this.lbl_NULastName);
            this.Controls.Add(this.lbl_NUFirstName);
            this.Controls.Add(this.txt_NUFirstName);
            this.Controls.Add(this.btn_NUBack);
            this.Controls.Add(this.lbl_NewUser);
            this.Name = "NewUserForm";
            this.Text = "NewUserForm";
            this.Load += new System.EventHandler(this.NewUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Menu;
        private System.Windows.Forms.Button btn_NewUser;
        private System.Windows.Forms.Button btn_NewItem;
        private System.Windows.Forms.Button btn_BorrowItem;
        private System.Windows.Forms.Button btn_ViewUsers;
        private System.Windows.Forms.Button btn_ViewItems;
        private System.Windows.Forms.Label lbl_NewUser;
        private System.Windows.Forms.Button btn_NUBack;
        private System.Windows.Forms.TextBox txt_NUFirstName;
        private System.Windows.Forms.Label lbl_NUFirstName;
        private System.Windows.Forms.Label lbl_NULastName;
        private System.Windows.Forms.TextBox txt_NULastName;
        private System.Windows.Forms.Label lbl_NUEmail;
        private System.Windows.Forms.Label lbl_NUPhone;
        private System.Windows.Forms.TextBox txt_NUEmail;
        private System.Windows.Forms.TextBox txt_NUPhone;
        private System.Windows.Forms.Button btn_NUOk;
        private System.Windows.Forms.RadioButton rad_NUStaff;
        private System.Windows.Forms.RadioButton rad_NUStudent;
    }
}

