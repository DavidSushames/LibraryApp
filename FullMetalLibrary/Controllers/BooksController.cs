using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FullMetalLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly FullMetalLibraryContext _context;
        public BooksController(FullMetalLibraryContext context) => _context = context;

        // Index: include Author so we can show Author.Name
        public async Task<IActionResult> Index()
        {
            var books = await _context.Book
                                      .Include(b => b.Author)   // load author
                                      .OrderBy(b => b.Title)
                                      .ToListAsync();
            return View(books);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book
                                     .Include(b => b.Author) // load author
                                     .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Create
        public IActionResult Create()
        {
            PopulateAuthorDropDownList();
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AuthorId,PublishDate,Genre,Available")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book); // AuthorId will be stored
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorDropDownList(book.AuthorId);
            return View(book);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book.FindAsync(id);
            if (book == null) return NotFound();

            PopulateAuthorDropDownList(book.AuthorId);
            return View(book);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,AuthorId,PublishDate,Genre,Available")] Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorDropDownList(book.AuthorId);
            return View(book);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book
                                     .Include(b => b.Author) // load author
                                     .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAuthorDropDownList(object? selectedAuthor = null)
        {
            // Order by LastName,FirstName, project to Id+Name for the SelectList
            var authors = _context.Author
                                  .OrderBy(a => a.LastName)
                                  .ThenBy(a => a.FirstName)
                                  .Select(a => new { a.Id, Name = a.FirstName + " " + a.LastName })
                                  .ToList();

            ViewBag.AuthorList = new SelectList(authors, "Id", "Name", selectedAuthor);
        }
    }
}
