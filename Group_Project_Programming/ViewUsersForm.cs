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
    public partial class ViewUsersForm : Form
    {
        public ViewUsersForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                MenuForm menuForm = new MenuForm();
                menuForm.Show();
                this.Hide();
        }

        private void ViewUsersForm_Load(object sender, EventArgs e)
        {
            dataGridViewUsers.AutoGenerateColumns = true;
            dataGridViewUsers.Columns.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add("First Name", typeof(string));
            dt.Columns.Add("Last Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            foreach (var user in Program.Users)
            {
                dt.Rows.Add(user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.UserType);
            }

            dataGridViewUsers.DataSource = dt;
            dataGridViewUsers.AutoGenerateColumns = true;
        }

        private void dgv_UserType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
