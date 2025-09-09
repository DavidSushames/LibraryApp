namespace Group_Project_Programming
{
    partial class ViewItemsForm
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
            this.lbl_VIViewItems = new System.Windows.Forms.Label();
            this.btn_VIBack = new System.Windows.Forms.Button();
            this.dgv_VIItemList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.itemBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsBorrowed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BorrowedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BorrowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimesRenewed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VIItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_VIViewItems
            // 
            this.lbl_VIViewItems.AutoSize = true;
            this.lbl_VIViewItems.Font = new System.Drawing.Font("Stencil", 72F);
            this.lbl_VIViewItems.Location = new System.Drawing.Point(69, 38);
            this.lbl_VIViewItems.Name = "lbl_VIViewItems";
            this.lbl_VIViewItems.Size = new System.Drawing.Size(583, 114);
            this.lbl_VIViewItems.TabIndex = 0;
            this.lbl_VIViewItems.Text = "View Items";
            // 
            // btn_VIBack
            // 
            this.btn_VIBack.Location = new System.Drawing.Point(12, 12);
            this.btn_VIBack.Name = "btn_VIBack";
            this.btn_VIBack.Size = new System.Drawing.Size(75, 23);
            this.btn_VIBack.TabIndex = 2;
            this.btn_VIBack.Text = "Back";
            this.btn_VIBack.UseVisualStyleBackColor = true;
            this.btn_VIBack.Click += new System.EventHandler(this.btn_VIBack_Click);
            // 
            // dgv_VIItemList
            // 
            this.dgv_VIItemList.AllowUserToAddRows = false;
            this.dgv_VIItemList.AllowUserToDeleteRows = false;
            this.dgv_VIItemList.AllowUserToOrderColumns = true;
            this.dgv_VIItemList.AutoGenerateColumns = false;
            this.dgv_VIItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_VIItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.IsBorrowed,
            this.BorrowedBy,
            this.BorrowDate,
            this.DueDate,
            this.TimesRenewed});
            this.dgv_VIItemList.DataSource = this.itemBindingSource2;
            this.dgv_VIItemList.Location = new System.Drawing.Point(12, 146);
            this.dgv_VIItemList.Name = "dgv_VIItemList";
            this.dgv_VIItemList.ReadOnly = true;
            this.dgv_VIItemList.Size = new System.Drawing.Size(710, 289);
            this.dgv_VIItemList.TabIndex = 1;
            this.dgv_VIItemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "BorrowedBy";
            this.dataGridViewTextBoxColumn1.HeaderText = "BorrowedBy";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "BorrowedBy";
            this.dataGridViewTextBoxColumn2.HeaderText = "BorrowedBy";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "BorrowedBy";
            this.dataGridViewTextBoxColumn3.HeaderText = "BorrowedBy";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "BorrowedBy";
            this.dataGridViewTextBoxColumn4.FillWeight = 150F;
            this.dataGridViewTextBoxColumn4.HeaderText = "BorrowedBy";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "BorrowedBy";
            this.dataGridViewTextBoxColumn5.FillWeight = 150F;
            this.dataGridViewTextBoxColumn5.HeaderText = "BorrowedBy";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // itemBindingSource2
            // 
            this.itemBindingSource2.DataSource = typeof(Item);
            // 
            // itemBindingSource1
            // 
            this.itemBindingSource1.DataSource = typeof(Item);
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(Item);
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.FillWeight = 70F;
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.Width = 70;
            // 
            // IsBorrowed
            // 
            this.IsBorrowed.DataPropertyName = "IsBorrowed";
            this.IsBorrowed.FillWeight = 65F;
            this.IsBorrowed.HeaderText = "IsBorrowed";
            this.IsBorrowed.Name = "IsBorrowed";
            this.IsBorrowed.ReadOnly = true;
            this.IsBorrowed.Width = 65;
            // 
            // BorrowedBy
            // 
            this.BorrowedBy.DataPropertyName = "BorrowedBy";
            this.BorrowedBy.FillWeight = 150F;
            this.BorrowedBy.HeaderText = "BorrowedBy";
            this.BorrowedBy.Name = "BorrowedBy";
            this.BorrowedBy.ReadOnly = true;
            // 
            // BorrowDate
            // 
            this.BorrowDate.DataPropertyName = "BorrowDate";
            this.BorrowDate.HeaderText = "BorrowDate";
            this.BorrowDate.Name = "BorrowDate";
            this.BorrowDate.ReadOnly = true;
            // 
            // DueDate
            // 
            this.DueDate.DataPropertyName = "DueDate";
            this.DueDate.HeaderText = "DueDate";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            // 
            // TimesRenewed
            // 
            this.TimesRenewed.DataPropertyName = "TimesRenewed";
            this.TimesRenewed.HeaderText = "TimesRenewed";
            this.TimesRenewed.Name = "TimesRenewed";
            this.TimesRenewed.ReadOnly = true;
            // 
            // ViewItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.btn_VIBack);
            this.Controls.Add(this.dgv_VIItemList);
            this.Controls.Add(this.lbl_VIViewItems);
            this.Name = "ViewItemsForm";
            this.Text = "ViewItemsForm";
            this.Load += new System.EventHandler(this.ViewItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_VIItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_VIViewItems;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.Button btn_VIBack;
        private System.Windows.Forms.DataGridView dgv_VIItemList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRenewedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource itemBindingSource1;
        private System.Windows.Forms.BindingSource itemBindingSource2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsBorrowed;
        private System.Windows.Forms.DataGridViewTextBoxColumn BorrowedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn BorrowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimesRenewed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}