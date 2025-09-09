using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace LibraryForms
{
    public partial class NewUserForm : Form
    {
        public User CreatedUser { get; private set; }
        public NewUserForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        

        private void btn_NUBack_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm(); 
            menuForm.Show();               
            this.Hide();                
        }

        private void txt_NUFirstName_TextChanged(object sender, EventArgs e)
        {
            string first = Console.ReadLine();
        }

        private void txt_NULastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_NUEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_NUPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void rad_NUStudent_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rad_NUStaff_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void btn_NUOk_Click(object sender, EventArgs e)
        {
            string first = txt_NUFirstName.Text.Trim();
            string last = txt_NULastName.Text.Trim();
            string email = txt_NUEmail.Text.Trim();
            string phone = txt_NUPhone.Text.Trim();
            if (string.IsNullOrEmpty(first))
            { MessageBox.Show("Please enter First name."); return; }
            if (string.IsNullOrEmpty(last))
            { MessageBox.Show("Please enter Last name."); return; }
            if (string.IsNullOrEmpty(email))
            { MessageBox.Show("Please enter your Email."); return; }
            if (string.IsNullOrEmpty(phone))
            { MessageBox.Show("Please enter your Phone Number."); return;}
            if (!rad_NUStaff.Checked && !rad_NUStudent.Checked)
            { MessageBox.Show("Please select either Staff or Student."); return; }
            if (rad_NUStaff.Checked)
            {
                CreatedUser = new Staff();
                txt_NUFirstName.Clear();
                txt_NULastName.Clear();
                txt_NUEmail.Clear();
                txt_NUPhone.Clear();
                rad_NUStaff.Checked = false;
            }
            else if (rad_NUStudent.Checked)
            {
                CreatedUser = new Student();
                txt_NUFirstName.Clear();
                txt_NULastName.Clear();
                txt_NUEmail.Clear();
                txt_NUPhone.Clear();
                rad_NUStudent.Checked = false;
            }
            CreatedUser.FirstName = first;
            CreatedUser.LastName = last;
            CreatedUser.Email = email;
            CreatedUser.PhoneNumber = phone;
            //Add to csv
            Program.Users.Add(CreatedUser);
            Program.SaveUsersToCsv("users.csv");
            MessageBox.Show($"New {CreatedUser.UserType} \"{CreatedUser.Name}\" created.");
        }

        private void lbl_NewUser_Click(object sender, EventArgs e)
        {

        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
//static void RegisterUser(List<User> users)
//{
//    Console.Write("First name: ");
//    string first = Console.ReadLine();

//    Console.Write("Last name: ");
//    string last = Console.ReadLine();

//    Console.Write("Type (Student or Staff): ");
//    string type = Console.ReadLine().ToLower();

//    User user;
//    if (type == "student")
//        user = new Student();
//    else if (type == "staff")
//        user = new Staff();
//    else
//    {
//        Console.WriteLine("Invalid user type.");
//        return;
//    }

//    user.FirstName = first;
//    user.LastName = last;
//    users.Add(user);
//    Console.WriteLine($"User {user.Name} registered as {type}.");
//}