namespace LibraryForms
{
    partial class MenuForm
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
            this.lbl_Menu = new System.Windows.Forms.Label();
            this.btn_NewUser = new System.Windows.Forms.Button();
            this.btn_NewItem = new System.Windows.Forms.Button();
            this.btn_BorrowReturnItem = new System.Windows.Forms.Button();
            this.btn_ViewUsers = new System.Windows.Forms.Button();
            this.btn_ViewItems = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Menu
            // 
            this.lbl_Menu.AutoSize = true;
            this.lbl_Menu.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Menu.Font = new System.Drawing.Font("Stencil", 72F);
            this.lbl_Menu.ForeColor = System.Drawing.Color.Black;
            this.lbl_Menu.Location = new System.Drawing.Point(207, 11);
            this.lbl_Menu.Name = "lbl_Menu";
            this.lbl_Menu.Size = new System.Drawing.Size(308, 114);
            this.lbl_Menu.TabIndex = 0;
            this.lbl_Menu.Text = "Menu";
            this.lbl_Menu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Menu.Click += new System.EventHandler(this.Menu_Click);
            // 
            // btn_NewUser
            // 
            this.btn_NewUser.Location = new System.Drawing.Point(364, 198);
            this.btn_NewUser.Name = "btn_NewUser";
            this.btn_NewUser.Size = new System.Drawing.Size(75, 23);
            this.btn_NewUser.TabIndex = 1;
            this.btn_NewUser.Text = "New User";
            this.btn_NewUser.UseVisualStyleBackColor = true;
            this.btn_NewUser.Click += new System.EventHandler(this.btn_NewUser_Click);
            // 
            // btn_NewItem
            // 
            this.btn_NewItem.Location = new System.Drawing.Point(288, 198);
            this.btn_NewItem.Name = "btn_NewItem";
            this.btn_NewItem.Size = new System.Drawing.Size(70, 23);
            this.btn_NewItem.TabIndex = 1;
            this.btn_NewItem.Text = "New Item";
            this.btn_NewItem.UseVisualStyleBackColor = true;
            this.btn_NewItem.Click += new System.EventHandler(this.btn_NewItem_Click);
            // 
            // btn_BorrowReturnItem
            // 
            this.btn_BorrowReturnItem.Location = new System.Drawing.Point(288, 140);
            this.btn_BorrowReturnItem.Name = "btn_BorrowReturnItem";
            this.btn_BorrowReturnItem.Size = new System.Drawing.Size(151, 23);
            this.btn_BorrowReturnItem.TabIndex = 1;
            this.btn_BorrowReturnItem.Text = "Borrow/Return/Renew Item";
            this.btn_BorrowReturnItem.UseVisualStyleBackColor = true;
            this.btn_BorrowReturnItem.Click += new System.EventHandler(this.btn_BorrowReturnItem_Click);
            // 
            // btn_ViewUsers
            // 
            this.btn_ViewUsers.Location = new System.Drawing.Point(364, 169);
            this.btn_ViewUsers.Name = "btn_ViewUsers";
            this.btn_ViewUsers.Size = new System.Drawing.Size(75, 23);
            this.btn_ViewUsers.TabIndex = 1;
            this.btn_ViewUsers.Text = "View Users";
            this.btn_ViewUsers.UseVisualStyleBackColor = true;
            this.btn_ViewUsers.Click += new System.EventHandler(this.btn_ViewUsers_Click);
            // 
            // btn_ViewItems
            // 
            this.btn_ViewItems.Location = new System.Drawing.Point(288, 169);
            this.btn_ViewItems.Name = "btn_ViewItems";
            this.btn_ViewItems.Size = new System.Drawing.Size(70, 23);
            this.btn_ViewItems.TabIndex = 1;
            this.btn_ViewItems.Text = "View Items";
            this.btn_ViewItems.UseVisualStyleBackColor = true;
            this.btn_ViewItems.Click += new System.EventHandler(this.btn_ViewItems_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Group_Project_Programming.Properties.Resources.Book;
            this.pictureBox1.Location = new System.Drawing.Point(182, 169);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(333, 292);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.btn_ViewItems);
            this.Controls.Add(this.btn_ViewUsers);
            this.Controls.Add(this.btn_BorrowReturnItem);
            this.Controls.Add(this.btn_NewItem);
            this.Controls.Add(this.btn_NewUser);
            this.Controls.Add(this.lbl_Menu);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_Menu;
        public System.Windows.Forms.Button btn_NewUser;
        public System.Windows.Forms.Button btn_NewItem;
        public System.Windows.Forms.Button btn_BorrowReturnItem;
        public System.Windows.Forms.Button btn_ViewUsers;
        public System.Windows.Forms.Button btn_ViewItems;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

