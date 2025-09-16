namespace LibraryForms
{
    partial class NewItemForm
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
            this.lbl_NewItem = new System.Windows.Forms.Label();
            this.btn_NIBack = new System.Windows.Forms.Button();
            this.txt_NITitle = new System.Windows.Forms.TextBox();
            this.lbl_NITitle = new System.Windows.Forms.Label();
            this.btn_NIOk = new System.Windows.Forms.Button();
            this.rad_NIBook = new System.Windows.Forms.RadioButton();
            this.rad_NIArticle = new System.Windows.Forms.RadioButton();
            this.rad_NIDigitalMedia = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbl_NewItem
            // 
            this.lbl_NewItem.AutoSize = true;
            this.lbl_NewItem.Font = new System.Drawing.Font("Stencil", 72F);
            this.lbl_NewItem.Location = new System.Drawing.Point(124, 68);
            this.lbl_NewItem.Name = "lbl_NewItem";
            this.lbl_NewItem.Size = new System.Drawing.Size(497, 114);
            this.lbl_NewItem.TabIndex = 0;
            this.lbl_NewItem.Text = "New Item";
            // 
            // btn_NIBack
            // 
            this.btn_NIBack.Location = new System.Drawing.Point(12, 12);
            this.btn_NIBack.Name = "btn_NIBack";
            this.btn_NIBack.Size = new System.Drawing.Size(75, 23);
            this.btn_NIBack.TabIndex = 1;
            this.btn_NIBack.Text = "Back";
            this.btn_NIBack.UseVisualStyleBackColor = true;
            this.btn_NIBack.Click += new System.EventHandler(this.btn_NIBack_Click);
            // 
            // txt_NITitle
            // 
            this.txt_NITitle.Location = new System.Drawing.Point(256, 217);
            this.txt_NITitle.Name = "txt_NITitle";
            this.txt_NITitle.Size = new System.Drawing.Size(216, 20);
            this.txt_NITitle.TabIndex = 2;
            this.txt_NITitle.TextChanged += new System.EventHandler(this.txt_NITitle_TextChanged);
            // 
            // lbl_NITitle
            // 
            this.lbl_NITitle.AutoSize = true;
            this.lbl_NITitle.BackColor = System.Drawing.Color.White;
            this.lbl_NITitle.Location = new System.Drawing.Point(223, 220);
            this.lbl_NITitle.Name = "lbl_NITitle";
            this.lbl_NITitle.Size = new System.Drawing.Size(27, 13);
            this.lbl_NITitle.TabIndex = 3;
            this.lbl_NITitle.Text = "Title";
            this.lbl_NITitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_NIOk
            // 
            this.btn_NIOk.Location = new System.Drawing.Point(316, 292);
            this.btn_NIOk.Name = "btn_NIOk";
            this.btn_NIOk.Size = new System.Drawing.Size(75, 23);
            this.btn_NIOk.TabIndex = 6;
            this.btn_NIOk.Text = "Ok";
            this.btn_NIOk.UseVisualStyleBackColor = true;
            this.btn_NIOk.Click += new System.EventHandler(this.btn_NIOk_Click);
            // 
            // rad_NIBook
            // 
            this.rad_NIBook.AutoSize = true;
            this.rad_NIBook.BackColor = System.Drawing.Color.White;
            this.rad_NIBook.Location = new System.Drawing.Point(256, 243);
            this.rad_NIBook.Name = "rad_NIBook";
            this.rad_NIBook.Size = new System.Drawing.Size(50, 17);
            this.rad_NIBook.TabIndex = 7;
            this.rad_NIBook.TabStop = true;
            this.rad_NIBook.Text = "Book";
            this.rad_NIBook.UseVisualStyleBackColor = false;
            // 
            // rad_NIArticle
            // 
            this.rad_NIArticle.AutoSize = true;
            this.rad_NIArticle.BackColor = System.Drawing.Color.White;
            this.rad_NIArticle.Location = new System.Drawing.Point(320, 243);
            this.rad_NIArticle.Name = "rad_NIArticle";
            this.rad_NIArticle.Size = new System.Drawing.Size(54, 17);
            this.rad_NIArticle.TabIndex = 7;
            this.rad_NIArticle.TabStop = true;
            this.rad_NIArticle.Text = "Article";
            this.rad_NIArticle.UseVisualStyleBackColor = false;
            // 
            // rad_NIDigitalMedia
            // 
            this.rad_NIDigitalMedia.AutoSize = true;
            this.rad_NIDigitalMedia.BackColor = System.Drawing.Color.White;
            this.rad_NIDigitalMedia.Location = new System.Drawing.Point(386, 243);
            this.rad_NIDigitalMedia.Name = "rad_NIDigitalMedia";
            this.rad_NIDigitalMedia.Size = new System.Drawing.Size(86, 17);
            this.rad_NIDigitalMedia.TabIndex = 7;
            this.rad_NIDigitalMedia.TabStop = true;
            this.rad_NIDigitalMedia.Text = "Digital Media";
            this.rad_NIDigitalMedia.UseVisualStyleBackColor = false;
            // 
            // NewItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.rad_NIDigitalMedia);
            this.Controls.Add(this.rad_NIArticle);
            this.Controls.Add(this.rad_NIBook);
            this.Controls.Add(this.btn_NIOk);
            this.Controls.Add(this.lbl_NITitle);
            this.Controls.Add(this.txt_NITitle);
            this.Controls.Add(this.btn_NIBack);
            this.Controls.Add(this.lbl_NewItem);
            this.Name = "NewItemForm";
            this.Text = "NewItemForm";
            this.Load += new System.EventHandler(this.NewItemForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_NewItem;
        private System.Windows.Forms.Button btn_NIBack;
        private System.Windows.Forms.TextBox txt_NITitle;
        private System.Windows.Forms.Label lbl_NITitle;
        private System.Windows.Forms.Button btn_NIOk;
        private System.Windows.Forms.RadioButton rad_NIBook;
        private System.Windows.Forms.RadioButton rad_NIArticle;
        private System.Windows.Forms.RadioButton rad_NIDigitalMedia;
    }
}

