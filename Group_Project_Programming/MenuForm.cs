using Group_Project_Programming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryForms
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Menu_Click(object sender, EventArgs e)
        {

        }

        private void btn_NewUser_Click(object sender, EventArgs e)
        {
            NewUserForm newUserForm = new NewUserForm();  // Create an instance of NewUserForm
            newUserForm.Show();               // Show NewUserForm
            this.Hide();                // Hide MenuForm
        }

        private void btn_NewItem_Click(object sender, EventArgs e)
        {
            NewItemForm newItemForm = new NewItemForm();  // Create an instance of NewItemForm
            newItemForm.Show();               // Show NewItemForm
            this.Hide();                // Hide MenuForm
        }

        private void btn_BorrowReturnItem_Click(object sender, EventArgs e)
        {
            BorrowReturnForm borrowItemForm = new BorrowReturnForm();
            borrowItemForm.Show();
            this.Hide();
        }

        private void btn_ViewUsers_Click(object sender, EventArgs e)
        {
            ViewUsersForm viewUsersForm = new ViewUsersForm();  // Create an instance of ViewUsersForm
            viewUsersForm.Show();               // Show ViewUsersForm
            this.Hide();                // Hide MenuForm
        }

        private void btn_ViewItems_Click(object sender, EventArgs e)
        {
            ViewItemsForm viewItemsForm = new ViewItemsForm();  // Create an instance of ViewItemsForm
            viewItemsForm.Show();               // Show ViewItemsForm
            this.Hide();                // Hide MenuForm
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
