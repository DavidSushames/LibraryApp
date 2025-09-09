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
    public partial class BorrowReturnForm : Form
    {
        public BorrowReturnForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            cmb_BITitle.DataSource = Program.Items;
            cmb_BITitle.DisplayMember = "Title";
            cmb_BIUser.DataSource = Program.Users;
            cmb_BIUser.DisplayMember = "Name";
        }

        private void btn_BIBack_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_BITitle_Click(object sender, EventArgs e)
        {

        }

        private void btn_BIOk_Click(object sender, EventArgs e)
        {
            var selectedItem = cmb_BITitle.SelectedItem as Item;
            var selectedUser = cmb_BIUser.SelectedItem as User;

            if (selectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            if (!rad_BRBorrow.Checked && !rad_BRReturn.Checked && !rad_BRRenew.Checked)
            {
                MessageBox.Show("Please select Borrow, Return, or Renew.");
                return;
            }

            if (rad_BRBorrow.Checked)
            {
                if (selectedItem.IsBorrowed)
                {
                    MessageBox.Show("This item is already borrowed.");
                    return;
                }

                if (selectedUser is Student student && student.BorrowedItems.Count >= 5)
                {
                    MessageBox.Show("Students can only borrow up to 5 items at a time.");
                    return;
                }

                selectedItem.IsBorrowed = true;
                selectedItem.BorrowedBy = selectedUser;
                selectedItem.BorrowDate = DateTime.Now;

                selectedItem.DueDate = DateTime.Now.AddDays(selectedUser.UserType == UserType.Staff ? 365 : 90);

                selectedUser.BorrowedItems.Add(selectedItem);

                MessageBox.Show($"{selectedUser.Name} has borrowed \"{selectedItem.Title}\". It is due in {((selectedItem.DueDate.Date - selectedItem.BorrowDate.Date).Days)} days, on {selectedItem.DueDate:dd/MM/yyyy}.");

                Program.SaveItemsToCsv("items.csv");
            }

            if (rad_BRReturn.Checked)
            {
                if (!selectedItem.IsBorrowed)
                {
                    MessageBox.Show("This item is not currently borrowed.");
                    return;
                }

                if (selectedItem.BorrowedBy != selectedUser)
                {
                    MessageBox.Show("User has not borrowed this item.");
                    return;
                }

                // LATE FEES
                DateTime dueDate = selectedItem.DueDate;
                DateTime returnDate = DateTime.Now;

                int daysLate = (returnDate - dueDate).Days;
                if (daysLate > 0)
                {
                    int fee = daysLate * 5;
                    MessageBox.Show($"Late return! {selectedUser.Name} returned \"{selectedItem.Title}\" {daysLate} day(s) late. Total late fee: ${fee}.");
                }
                else
                {
                    MessageBox.Show($"{selectedUser.Name} has returned \"{selectedItem.Title}\" on time.");
                }

                selectedItem.IsBorrowed = false;
                selectedItem.BorrowedBy = null;
                selectedItem.BorrowDate = DateTime.MinValue;
                selectedItem.DueDate = DateTime.MinValue;
                selectedItem.TimesRenewed = 0;
                selectedUser.BorrowedItems.Remove(selectedItem);

                Program.SaveItemsToCsv("items.csv");
            }
            if (rad_BRRenew.Checked)
            {
                if (selectedItem.BorrowedBy != selectedUser)
                {
                    MessageBox.Show("User has not borrowed this item.");
                    return;
                }
                if (selectedUser is Student student && selectedItem.TimesRenewed >= 1)
                {
                    MessageBox.Show("Students can only renew once per item.");
                    return;
                }
                else 
                {
                    selectedItem.DueDate = selectedItem.DueDate.AddDays(7);
                    selectedItem.TimesRenewed++;

                    MessageBox.Show($"Renewed. New due date is in {((selectedItem.DueDate.Date - selectedItem.BorrowDate.Date).Days)} days, on { selectedItem.DueDate:dd / MM / yyyy}.");
                }
            }
        }
    }
}
