using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FullMetalLibrary.Filter;

namespace FullMetalLibrary.Controllers
{
    [AuthFilter]
    public class BooksController(FullMetalLibraryContext context) : Controller
    {
        private readonly FullMetalLibraryContext _context = context;

        //Login Get
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // GET: Books
        // In your BooksController - Update the Index method search logic:
        // GET: Books
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Sorting parameters
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "author_az" ? "author_za" : "author_az";
            ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var books = _context.Book.Include(b => b.Author).AsQueryable();
<<<<<<< HEAD
=======
           
>>>>>>> 0eb5fad1b76ec1af27a6a18c1a386497137e2cf9

            // Searching - FIXED WITH NULL CHECKS
            if (!string.IsNullOrEmpty(searchString))
            {
<<<<<<< HEAD
                //Search bar in Books - FIXED: Added null checks for Author
                bool bookMatch = books.Any(b =>
                    (b.Title != null && b.Title.Contains(searchString)) ||
                    (b.Genre != null && b.Genre.Contains(searchString)) ||
                    (b.Author != null && b.Author.FirstName != null && b.Author.FirstName.Contains(searchString)) ||
                    (b.Author != null && b.Author.LastName != null && b.Author.LastName.Contains(searchString)));

                //Search bar in Admins 
                bool adminMatch = _context.Admin.Any(a =>
                    (a.UserName != null && a.UserName.Contains(searchString)) ||
                    (a.EmailAddress != null && a.EmailAddress.Contains(searchString)));
=======
                //Search bar in Books 
                bool bookMatch = await books.AnyAsync(b =>
                    b.Title.Contains(searchString) ||
                    b.Genre.Contains(sortOrder) ||
                    b.Author!.FirstName.Contains(searchString) ||
                    b.Author.LastName.Contains(searchString));

                //Search bar in Admins 
                bool adminMatch = await _context.Admin.AnyAsync(a =>
                    a.UserName.Contains(searchString) ||
                    a.EmailAddress.Contains(searchString));
>>>>>>> 0eb5fad1b76ec1af27a6a18c1a386497137e2cf9


                if (bookMatch)
                {
                    books = books.Where(b =>
<<<<<<< HEAD
                        (b.Title != null && b.Title.Contains(searchString)) ||
                        (b.Genre != null && b.Genre.Contains(searchString)) ||
                        (b.Author != null && b.Author.FirstName != null && b.Author.FirstName.Contains(searchString)) ||
                        (b.Author != null && b.Author.LastName != null && b.Author.LastName.Contains(searchString)));
=======
                    b.Title.Contains(searchString) ||
                    b.Genre.Contains(searchString) ||
                    b.Author!.FirstName.Contains(searchString) ||
                    b.Author.LastName.Contains(searchString));
>>>>>>> 0eb5fad1b76ec1af27a6a18c1a386497137e2cf9
                }
                else if (adminMatch)
                {
                    //this method checks if the admin matched, and keep all books but flag it
                    ViewBag.AdminMatched = true;
                }
                else
                {
                    //if nothing matched, return empty list
                    ViewBag.NotFoundMessage = $"No results found for '{searchString} in the Library system";
                    return View(new List<Book>());
                }
            }

            // Sorting
            books = sortOrder switch
            {
                "az" => books
                        .OrderBy(b => b.Title)
<<<<<<< HEAD
                        .ThenBy(b => b.Author != null ? b.Author.LastName : "")
                        .ThenBy(b => b.Author != null ? b.Author.FirstName : ""),
                "za" => books
                        .OrderByDescending(b => b.Title)
                        .ThenByDescending(b => b.Author != null ? b.Author.LastName : "")
                        .ThenByDescending(b => b.Author != null ? b.Author.FirstName : ""),
=======
                        .ThenBy(b => b.Author!.LastName)
                        .ThenBy(b => b.Author!.FirstName),
                "za" => books
                        .OrderByDescending(b => b.Title)
                        .ThenByDescending(b => b.Author!.LastName)
                        .ThenByDescending(b => b.Author!.FirstName),
>>>>>>> 0eb5fad1b76ec1af27a6a18c1a386497137e2cf9
                _ => books.OrderBy(b => b.Title)
            };

            return View(await books.ToListAsync());
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book
                .Include(b => b.Author)
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
            if (book.PublishDate >= DateTime.Today)
            {
                ModelState.AddModelError("PublishDate", "Publish date must be in the past.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
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
            if (book.PublishDate >= DateTime.Today)
            {
                ModelState.AddModelError("PublishDate", "Publish date must be in the past.");
            }
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
        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Find the book by primary key
            var bookToDelete = await _context.Book.FindAsync(id);

            if (bookToDelete == null) return NotFound();

            // Optionally, include the author name for the confirmation view
            var model = new
            {
                bookToDelete.Id,
                bookToDelete.Title,
                AuthorName = bookToDelete.Author?.FirstName + " " + bookToDelete.Author?.LastName
            };

            return View(model);
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
            var authors = _context.Author
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Select(a => new { a.Id, Name = a.FirstName + " " + a.LastName })
                .ToList();

            ViewBag.AuthorList = new SelectList(authors, "Id", "Name", selectedAuthor);
        }
    }
}
