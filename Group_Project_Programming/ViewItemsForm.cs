using LibraryForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group_Project_Programming
{
    public partial class ViewItemsForm : Form
    {
        public ViewItemsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += ViewItemsForm_Load;
            LoadItemsIntoGrid();
        }

        private void LoadItemsIntoGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("IsBorrowed", typeof(bool));
            dt.Columns.Add("BorrowedBy", typeof(string));
            dt.Columns.Add("BorrowDate", typeof(DateTime));
            dt.Columns.Add("DueDate", typeof(DateTime));

            foreach (var item in Program.Items)
            {
                dt.Rows.Add(
                item.Title,
                item.Type.ToString(),
                item.IsBorrowed,
                item.BorrowedBy != null ? item.BorrowedBy.Name : "",
                item.BorrowDate,
                item.DueDate
                );
            }

            dgv_VIItemList.AutoGenerateColumns = true;
            dgv_VIItemList.DataSource = dt;
            dgv_VIItemList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_VIBack_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewItemsForm_Load(object sender, EventArgs e)
        {
            Program.Items = Program.LoadItemsFromCsv("items.csv");
            LoadItemsIntoGrid();
        }
    }
}
