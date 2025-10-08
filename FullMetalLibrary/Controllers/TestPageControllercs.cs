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

            return View(books);
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
