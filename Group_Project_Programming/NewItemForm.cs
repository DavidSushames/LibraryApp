using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibraryForms
{
    public partial class NewItemForm : Form
    {
        public Item CreatedItem { get; private set; }
        public NewItemForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_NIBack_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();  
            menuForm.Show();           
            this.Hide();            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btn_NIOk_Click(object sender, EventArgs e)
        {
            string title = txt_NITitle.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title.");
                return;
            }

            ItemType type;

            if (rad_NIBook.Checked)
                type = ItemType.Book;
            else if (rad_NIArticle.Checked)
                type = ItemType.Article;
            else if (rad_NIDigitalMedia.Checked)
                type = ItemType.DigitalMedia;
            else
            {
                MessageBox.Show("Please select an item type.");
                return;
            }

            //creates new item
            CreatedItem = new Item(title, type, false, null, DateTime.MinValue, DateTime.MinValue, 0);
            Program.Items.Add(CreatedItem);
            //Adds to csv
            Program.SaveItemsToCsv("items.csv");
            MessageBox.Show($"New {type} \"{title}\" added to the library.");
        }

        private void cmb_NIItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_NITitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewItemForm_Load(object sender, EventArgs e)
        {

        }
    }
}


