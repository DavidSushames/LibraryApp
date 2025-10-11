using FullMetalLibrary.Data;
using FullMetalLibrary.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullMetalLibrary.Controllers
{
    [AuthFilter]
    public class TestPageController(FullMetalLibraryContext context) : Controller
    {
        private readonly FullMetalLibraryContext _context = context;

        public async Task<IActionResult> Index()
        {
            var books = await _context.Book
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToListAsync();

            var auhors = await _context.Author
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToListAsync();

            var admins = await _context.Admin
                .OrderBy(a => a.UserName)
                .ToListAsync();

            //this helps to view all the data (model data i guess)
            var viewModel = new TestPageViewModel
            {
                Books = books,
                Authors = auhors,
                Admins = admins
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}

public class TestPageViewModel
{
    public List<FullMetalLibrary.Models.Book> Books { get; set; }
    public List<FullMetalLibrary.Models.Author> Authors { get; set; }
    public List<FullMetalLibrary.Models.Admin> Admins { get; set; }

}
