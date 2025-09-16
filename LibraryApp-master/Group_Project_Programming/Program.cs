using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using LibraryForms;

class Program
{
    public static List<User> Users;
    public static List<Item> Items;
    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Program.Users = LoadUsersFromCsv("users.csv");
        Program.Items = LoadItemsFromCsv("items.csv");

        Application.Run(new MenuForm());
    }

    public static List<User> LoadUsersFromCsv(string filePath)
    {
        var users = new List<User>();

        if (!File.Exists(filePath))
        {
            MessageBox.Show("users.csv not found.");
            return users;
        }

        var lines = File.ReadAllLines(filePath).Skip(1); // Skip header
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 5)
            {
                string first = parts[0];
                string last = parts[1];
                string email = parts[2];
                string phone = parts[3];
                string type = parts[4].ToLower();

                User user = type switch
                {
                    "student" => new Student(),
                    "staff" => new Staff(),
                    _ => null
                };

                if (user != null)
                {
                    user.FirstName = first;
                    user.LastName = last;
                    user.Email = email;
                    user.PhoneNumber = phone;
                    users.Add(user);
                }
            }
        }
        return users;
    }

    public static List<Item> LoadItemsFromCsv(string filePath)
    {
        var items = new List<Item>();

        if (!File.Exists(filePath))
        {
            MessageBox.Show("items.csv not found.");
            return items;
        }

        var lines = File.ReadAllLines(filePath).Skip(1); // Skip header

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length >= 2 && Enum.TryParse(parts[1], out ItemType type))
            {
                bool isBorrowed = false;
                User borrowedBy = null;
                DateTime borrowDate = DateTime.MinValue;
                DateTime dueDate = DateTime.MinValue;
                int timesRenewed = 0;

                bool.TryParse(parts[2], out isBorrowed);

                if (parts.Length >= 4 && !string.IsNullOrWhiteSpace(parts[3]))
                {
                    borrowedBy = Program.Users.FirstOrDefault(u => u.Name == parts[3]);
                }

                if (parts.Length >= 5)
                { DateTime.TryParse(parts[4], out borrowDate); }

                if (parts.Length >= 6)
                { DateTime.TryParse(parts[5], out dueDate);}
                if (parts.Length >= 7)
                { int.TryParse(parts[6], out timesRenewed); }

                var item = new Item(parts[0], type, isBorrowed, borrowedBy, borrowDate, dueDate, timesRenewed);
                items.Add(item);
            }
        }

        return items;
    }




    public static void SaveItemsToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Title,Type,IsBorrowed,BorrowedBy,BorrowDate,DueDate,TimesRenewed");
            foreach (var item in Items)
            {
                writer.WriteLine($"{item.Title},{item.Type},{item.IsBorrowed},{(item.BorrowedBy != null ? item.BorrowedBy.Name : "")},{item.BorrowDate:yyyy-MM-dd},{item.DueDate:yyyy-MM-dd},{item.TimesRenewed}");
            }
        }
    }
    public static void SaveUsersToCsv(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("FirstName,LastName,Email,PhoneNumber,UserType");
            foreach (var user in Users)
            {
                writer.WriteLine($"{user.FirstName},{user.LastName},{user.Email},{user.PhoneNumber},{user.UserType}");
            }
        }
    }
}

public enum ItemType
{
    Book,
    Article,
    DigitalMedia
}

public class Item
{
    public string Title { get; set; }
    public ItemType Type { get; set; }
    public bool IsBorrowed { get; set; } = false;
    public User BorrowedBy { get; set; } = null;
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public int TimesRenewed { get; set; } = 0;

    public Item(string title, ItemType type, bool isBorrowed, User borrowedBy, DateTime borrowDate, DateTime dueDate, int timesRenewed)
    {
        Title = title;
        Type = type;
        IsBorrowed = isBorrowed;
        BorrowedBy = borrowedBy;
        BorrowDate = borrowDate;
        DueDate = dueDate;
        TimesRenewed = timesRenewed;
    }

    public override string ToString() => $"{Type} - {Title}";
}

public enum UserType { Student, Staff }

public abstract class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserType UserType { get; set; }
    public List<Item> BorrowedItems { get; set; } = new List<Item>();
    public string Name => $"{FirstName} {LastName} ({UserType.ToString().ToLower()})"; // Combined Firstname, Lastname, and (user type)

}

public class Staff : User
{
    public Staff()
    {
        UserType = UserType.Staff;
    }
}

public class Student : User
{
    public Student()
    {
        UserType = UserType.Student;
    }
}