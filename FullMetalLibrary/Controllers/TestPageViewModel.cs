using FullMetalLibrary.Models;

namespace FullMetalLibrary.Controllers
{
    internal class TestPageViewModel
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Admin> Admins { get; set; }
    }
}

//This is a simple view model class for the test page to hold lists of books, authors, and admins.