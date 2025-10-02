using FullMetalLibrary.Controllers;
using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims; 
using System.Text; 

namespace FMLTestProject.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock logger
            var loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(loggerMock.Object);
        }

        [TestMethod]
        public void Home_Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Home_Privacy_ReturnsViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Home_Error_ReturnsViewResult_WithErrorViewModel()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Act
            var result = _controller.Error() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(ErrorViewModel));

            var model = result.Model as ErrorViewModel;
            Assert.IsNotNull(model.RequestId);
        }

        [TestMethod]
        public void Home_Error_RequestId_IsSetCorrectly()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Act
            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(model.RequestId));
        }

        [TestMethod]
        public void Home_Error_ShowRequestId_WhenRequestIdNotEmpty_ReturnsTrue()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Act
            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            // Assert
            Assert.IsTrue(model.ShowRequestId);
        }

        [TestMethod]
        public void Home_Controller_Constructor_WithLogger_WorksCorrectly()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();

            // Act
            var controller = new HomeController(loggerMock.Object);

            // Assert
            Assert.IsNotNull(controller);
        }
    }
    
    [TestClass]
    public class BooksControllerTests
    {
        private BooksController _controller;
        private FullMetalLibraryContext _context;
        private DbContextOptions<FullMetalLibraryContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FullMetalLibraryContext>()
                .UseInMemoryDatabase(databaseName: "BooksTest_" + Guid.NewGuid())
                .Options;

            _context = new FullMetalLibraryContext(_options);
            _controller = new BooksController(_context);
            SetupMockHttpContext();
        }
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SetupMockHttpContext()
        {
            var httpContext = new DefaultHttpContext();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "testadmin") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            httpContext.User = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        private void SeedTestData()
        {
            // Add authors
            var authors = new List<Author>
            {
                new() { Id = 1, FirstName = "Stephen", LastName = "King" },
                new() { Id = 2, FirstName = "J.K.", LastName = "Rowling" },
                new() { Id = 3, FirstName = "George", LastName = "Orwell" }
            };

            // Add admins for search functionality
            var admins = new List<Admin>
            {
                new() { Id = 1, UserName = "libraryadmin", EmailAddress = "admin@library.com", IsActive = true },
                new() { Id = 2, UserName = "testuser", EmailAddress = "test@library.com", IsActive = true }
            };

            // Add books - make sure AuthorId matches existing authors
            var books = new List<Book>
            {
                new() { Id = 1, Title = "The Shining", AuthorId = 1, PublishDate = new DateTime(1977, 1, 1), Genre = "Horror", Available = true },
                new() { Id = 2, Title = "Harry Potter", AuthorId = 2, PublishDate = new DateTime(1997, 6, 1), Genre = "Fantasy", Available = true },
                new() { Id = 3, Title = "1984", AuthorId = 3, PublishDate = new DateTime(1949, 6, 1), Genre = "Dystopian", Available = false },
                new() { Id = 4, Title = "The Stand", AuthorId = 1, PublishDate = new DateTime(1978, 1, 1), Genre = "Horror", Available = true },
                new() { Id = 5, Title = "Animal Farm", AuthorId = 3, PublishDate = new DateTime(1945, 8, 1), Genre = "Political", Available = true }
            };

                _context.Author.AddRange(authors);
                _context.Admin.AddRange(admins);
                _context.Book.AddRange(books);
                _context.SaveChanges();
        }

        
        [TestMethod]
        public async Task Books_Index_ReturnsViewResult_WithBookList()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<Book>;
            Assert.AreEqual(5, model.Count);
        }

        [TestMethod]
        public async Task Books_Index_SearchByTitle_ReturnsFilteredBooks()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, "Harry") as ViewResult;
            var model = result.Model as List<Book>;

            // Assert
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("Harry Potter", model[0].Title);
        }

        [TestMethod]
        public async Task Books_Index_SearchByAuthorFirstName_ReturnsFilteredBooks()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, "Stephen") as ViewResult;
            var model = result.Model as List<Book>;

            // Assert
            Assert.AreEqual(2, model.Count); 

            foreach (var book in model)
            {
                var author = await _context.Author.FindAsync(book.AuthorId);
                Assert.AreEqual("Stephen", author.FirstName);
            }
        }

        [TestMethod]
        public async Task Books_Index_SearchByAuthorLastName_ReturnsFilteredBooks()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, "King") as ViewResult;
            var model = result.Model as List<Book>;

            // Assert
            Assert.AreEqual(2, model.Count);
            Assert.IsTrue(model.All(b => b.Author.LastName == "King"));
        }

        [TestMethod]
        public async Task Books_Index_SearchByGenre_ReturnsFilteredBooks()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, "Horror") as ViewResult;
            var model = result.Model as List<Book>;

            // Assert
            Assert.AreEqual(2, model.Count);
            Assert.IsTrue(model.All(b => b.Genre == "Horror"));
        }

        [TestMethod]
        public async Task Books_Index_SearchNoMatches_ReturnsEmptyList()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index(null, "NonexistentBook123") as ViewResult;
            var model = result.Model as List<Book>;

            // Assert
            Assert.AreEqual(0, model.Count);
            
        }

        [TestMethod]
        public async Task Books_Index_SortAZ_ReturnsBooksInAscendingOrder()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index("az", null) as ViewResult;
            var model = result.Model as List<Book>;

            // Assert 
            Assert.AreEqual("1984", model[0].Title);
            Assert.AreEqual("Animal Farm", model[1].Title);
            Assert.AreEqual("Harry Potter", model[2].Title);
            Assert.AreEqual("The Shining", model[3].Title);
            Assert.AreEqual("The Stand", model[4].Title);
        }

        [TestMethod]
        public async Task Books_Index_SortZA_ReturnsBooksInDescendingOrder()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _controller.Index("za", null) as ViewResult;
            var model = result.Model as List<Book>;

            // Assert 
            Assert.AreEqual("The Stand", model[0].Title);
            Assert.AreEqual("The Shining", model[1].Title);
            Assert.AreEqual("Harry Potter", model[2].Title);
            Assert.AreEqual("Animal Farm", model[3].Title);
            Assert.AreEqual("1984", model[4].Title);
        }

        
        [TestMethod]
        public async Task Books_Details_ValidId_ReturnsViewWithBook()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            var book = new Book { Id = 1, Title = "Test Book", AuthorId = 1, Author = author, Genre = "Test", Available = true };
            _context.Author.Add(author);
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Book));
            var model = result.Model as Book;
            Assert.AreEqual("Test Book", model.Title);
            Assert.IsNotNull(model.Author);
        }

        [TestMethod]
        public async Task Books_Details_NullId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Books_Details_NonExistentId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        
        [TestMethod]
        public void Books_Create_Get_ReturnsView()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            _context.SaveChanges();

            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            
        }

        [TestMethod]
        public async Task Books_Create_Post_ValidBook_CreatesAndRedirects()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            var book = new Book
            {
                Title = "New Book",
                AuthorId = 1,
                PublishDate = DateTime.Now.AddDays(-100), 
                Genre = "Test-Genre",
                Available = true
            };

            // Act
            var result = await _controller.Create(book);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var bookInDb = await _context.Book.FirstOrDefaultAsync(b => b.Title == "New Book");
            Assert.IsNotNull(bookInDb);
            Assert.AreEqual("Test-Genre", bookInDb.Genre);
        }

        [TestMethod]
        public async Task Books_Create_Post_FuturePublishDate_ReturnsViewWithError()
        {
            // Arrange
            var book = new Book
            {
                Title = "Future Book",
                AuthorId = 1,
                PublishDate = DateTime.Today.AddDays(1), 
                Genre = "Test",
                Available = true
            };

            // Act
            var result = await _controller.Create(book) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
            Assert.IsTrue(_controller.ModelState.ContainsKey("PublishDate"));
        }

        [TestMethod]
        public async Task Books_Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var book = new Book
            {
                Title = "", 
                AuthorId = 1,
                PublishDate = DateTime.Now.AddDays(-1),
                Genre = "Test",
                Available = true
            };

            // Act
            _controller.ModelState.AddModelError("Title", "Title is required");
            var result = await _controller.Create(book) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

       
        [TestMethod]
        public async Task Books_Edit_Get_ValidId_ReturnsViewWithBook()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            var book = new Book { Id = 1, Title = "Test Book", AuthorId = 1, Genre = "Test", Available = true };
            _context.Author.Add(author);
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Book));
        }

        [TestMethod]
        public async Task Books_Edit_Post_ValidData_UpdatesBook()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            var book = new Book { Id = 1, Title = "Old Title", AuthorId = 1, Genre = "Old Genre", Available = true };
            _context.Author.Add(author);
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            book.Title = "Updated Title";
            book.Genre = "Updated Genre";

            // Act
            var result = await _controller.Edit(1, book);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var updatedBook = await _context.Book.FindAsync(1);
            Assert.AreEqual("Updated Title", updatedBook.Title);
            Assert.AreEqual("Updated Genre", updatedBook.Genre);
        }

        [TestMethod]
        public async Task Books_Edit_Post_IdMismatch_ReturnsNotFound()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test", AuthorId = 1, Genre = "Test", Available = true };

            // Act
            var result = await _controller.Edit(2, book); 

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        
        [TestMethod]
        public async Task Books_Delete_Get_ValidId_ReturnsViewWithBook()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            var book = new Book { Id = 1, Title = "Test Book", AuthorId = 1, Author = author, Genre = "Test", Available = true };
            _context.Author.Add(author);
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Book));
            var model = result.Model as Book;
            Assert.AreEqual("Test Book", model.Title);
        }

        [TestMethod]
        public async Task Books_Delete_Post_ValidId_RemovesBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", AuthorId = 1, Genre = "Test", Available = true };
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            var initialCount = await _context.Book.CountAsync();

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(initialCount - 1, await _context.Book.CountAsync());

            var deletedBook = await _context.Book.FindAsync(1);
            Assert.IsNull(deletedBook);
        }

        [TestMethod]
        public async Task Books_Delete_Post_NonExistentId_RedirectsWithoutError()
        {
            // Act
            var result = await _controller.DeleteConfirmed(999);

            // Assert 
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

       
        [TestMethod]
        public void Books_BookExists_ExistingBook_ReturnsTrue()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", AuthorId = 1, Genre = "Test", Available = true };
            _context.Book.Add(book);
            _context.SaveChanges();

            // Use reflection to test private method
            var method = typeof(BooksController).GetMethod("BookExists",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)method.Invoke(_controller, new object[] { 1 });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Books_BookExists_NonExistingBook_ReturnsFalse()
        {
            // Use reflection to test private method
            var method = typeof(BooksController).GetMethod("BookExists",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)method.Invoke(_controller, new object[] { 999 });

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Books_IsLoggedIn_WithSession_ReturnsTrue()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var session = new TestSession();
            session.SetString("AdminUser", "testuser");
            httpContext.Session = session;
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Use reflection to test private method
            var method = typeof(BooksController).GetMethod("IsLoggedIn",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)method.Invoke(_controller, null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Books_IsLoggedIn_WithoutSession_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FullMetalLibraryContext>()
                .UseInMemoryDatabase(databaseName: "BooksTest_" + Guid.NewGuid())
                .Options;
            var context = new FullMetalLibraryContext(options);
            var controller = new BooksController(context);

            
            var httpContext = new DefaultHttpContext();

           
            var session = new TestSession();
            httpContext.Session = session;

            
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            
            var method = typeof(BooksController).GetMethod("IsLoggedIn",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Act & Assert 
            var result = (bool)method.Invoke(controller, null);
            Assert.IsFalse(result);
        }
    }
    
    [TestClass]
    public class AuthorsControllerTests
    {
        private AuthorsController _controller;
        private FullMetalLibraryContext _context;
        private DbContextOptions<FullMetalLibraryContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FullMetalLibraryContext>()
                .UseInMemoryDatabase(databaseName: "AuthorsTest_" + Guid.NewGuid())
                .Options;

            _context = new FullMetalLibraryContext(_options);
            _controller = new AuthorsController(_context);
            SetupMockHttpContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SetupMockHttpContext()
        {
            var httpContext = new DefaultHttpContext();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "testadmin") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            httpContext.User = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        private void SeedTestAuthors()
        {
            var authors = new List<Author>
        {
            new Author { Id = 1, FirstName = "Stephen", LastName = "King" },
            new Author { Id = 2, FirstName = "George", LastName = "Orwell" },
            new Author { Id = 3, FirstName = "J.K.", LastName = "Rowling" },
            new Author { Id = 4, FirstName = "J.R.R.", LastName = "Tolkien" },
            new Author { Id = 5, FirstName = "Agatha", LastName = "Christie" }
        };

            _context.Author.AddRange(authors);
            _context.SaveChanges();
        }

        
        [TestMethod]
        public async Task Authors_Index_ReturnsViewResult_WithAuthorList()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<Author>;
            Assert.AreEqual(5, model.Count);
        }

        [TestMethod]
        public async Task Authors_Index_SearchByFirstName_ReturnsFilteredAuthors()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index(null, "Stephen") as ViewResult;
            var model = result.Model as List<Author>;

            // Assert
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("Stephen", model[0].FirstName);
            Assert.AreEqual("King", model[0].LastName);
        }

        [TestMethod]
        public async Task Authors_Index_SearchByLastName_ReturnsFilteredAuthors()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index(null, "Orwell") as ViewResult;
            var model = result.Model as List<Author>;

            // Assert
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("George", model[0].FirstName);
            Assert.AreEqual("Orwell", model[0].LastName);
        }

        [TestMethod]
        public async Task Authors_Index_SearchNoResults_ReturnsEmptyList()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index(null, "NonexistentAuthor") as ViewResult;
            var model = result.Model as List<Author>;

            // Assert
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public async Task Authors_Index_SortAZ_ReturnsAuthorsInAscendingOrder()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index("az", null) as ViewResult;
            var model = result.Model as List<Author>;

            // Assert 
            Assert.AreEqual("Christie", model[0].LastName);
            Assert.AreEqual("King", model[1].LastName);
            Assert.AreEqual("Orwell", model[2].LastName);
            Assert.AreEqual("Rowling", model[3].LastName);
            Assert.AreEqual("Tolkien", model[4].LastName);
        }

        [TestMethod]
        public async Task Authors_Index_SortZA_ReturnsAuthorsInDescendingOrder()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index("za", null) as ViewResult;
            var model = result.Model as List<Author>;

            // Assert 
            Assert.AreEqual("Tolkien", model[0].LastName);
            Assert.AreEqual("Rowling", model[1].LastName);
            Assert.AreEqual("Orwell", model[2].LastName);
            Assert.AreEqual("King", model[3].LastName);
            Assert.AreEqual("Christie", model[4].LastName);
        }

        [TestMethod]
        public async Task Authors_Index_DefaultSort_ReturnsAuthorsInAscendingOrder()
        {
            // Arrange
            SeedTestAuthors();

            // Act
            var result = await _controller.Index(null, null) as ViewResult;
            var model = result.Model as List<Author>;

            // Assert 
            Assert.AreEqual("Christie", model[0].LastName);
            Assert.AreEqual("King", model[1].LastName);
        }

        [TestMethod]
        public async Task Authors_Index_EmptyDatabase_ReturnsEmptyList()
        {
            // Act
            var result = await _controller.Index(null, null) as ViewResult;
            var model = result.Model as List<Author>;

            // Assert
            Assert.AreEqual(0, model.Count);
        }

        
        [TestMethod]
        public async Task Authors_Details_ValidId_ReturnsViewWithAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Author));
            var model = result.Model as Author;
            Assert.AreEqual("Test", model.FirstName);
            Assert.AreEqual("Author", model.LastName);
        }

        [TestMethod]
        public async Task Authors_Details_NullId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Authors_Details_NonExistentId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Authors_Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Authors_Create_Post_ValidAuthor_CreatesAndRedirects()
        {
            // Arrange
            var author = new Author { FirstName = "New", LastName = "Author" };

            // Act
            var result = await _controller.Create(author);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify author was created in database
            var authorInDb = await _context.Author.FirstOrDefaultAsync(a => a.FirstName == "New");
            Assert.IsNotNull(authorInDb);
            Assert.AreEqual("Author", authorInDb.LastName);
        }

        [TestMethod]
        public async Task Authors_Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var author = new Author { FirstName = "", LastName = "" }; // Invalid
            _controller.ModelState.AddModelError("FirstName", "First name is required");

            // Act
            var result = await _controller.Create(author) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        [TestMethod]
        public async Task Authors_Create_Post_AuthorWithHyphenatedName_WorksCorrectly()
        {
            // Arrange
            var author = new Author { FirstName = "Jean-Paul", LastName = "Sartre" };

            // Act
            var result = await _controller.Create(author);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var authorInDb = await _context.Author.FirstOrDefaultAsync(a => a.LastName == "Sartre");
            Assert.IsNotNull(authorInDb);
            Assert.AreEqual("Jean-Paul", authorInDb.FirstName);
        }

        
        [TestMethod]
        public async Task Authors_Edit_Get_ValidId_ReturnsViewWithAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Old", LastName = "Name" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Author));
            var model = result.Model as Author;
            Assert.AreEqual("Old", model.FirstName);
        }

        [TestMethod]
        public async Task Authors_Edit_Get_NonExistentId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Authors_Edit_Get_NullId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Authors_Edit_Post_ValidData_UpdatesAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Old", LastName = "Name" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            author.FirstName = "Updated";
            author.LastName = "Name";

            // Act
            var result = await _controller.Edit(1, author);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var updatedAuthor = await _context.Author.FindAsync(1);
            Assert.AreEqual("Updated", updatedAuthor.FirstName);
            Assert.AreEqual("Name", updatedAuthor.LastName);
        }

        [TestMethod]
        public async Task Authors_Edit_Post_IdMismatch_ReturnsNotFound()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };

            // Act
            var result = await _controller.Edit(2, author); // ID mismatch

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Authors_Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "", LastName = "" }; // Invalid
            _controller.ModelState.AddModelError("FirstName", "First name is required");

            // Act
            var result = await _controller.Edit(1, author) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        
        [TestMethod]
        public async Task Authors_Delete_Get_ValidId_ReturnsViewWithAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Author));
            var model = result.Model as Author;
            Assert.AreEqual("Test", model.FirstName);
        }

        [TestMethod]
        public async Task Authors_Delete_Get_NonExistentId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Authors_Delete_Post_ValidId_RemovesAuthor()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            var initialCount = await _context.Author.CountAsync();

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(initialCount - 1, await _context.Author.CountAsync());

            var deletedAuthor = await _context.Author.FindAsync(1);
            Assert.IsNull(deletedAuthor);
        }

        [TestMethod]
        public async Task Authors_Delete_Post_NonExistentId_RedirectsWithoutError()
        {
            // Act
            var result = await _controller.DeleteConfirmed(999);

            // Assert 
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        
        [TestMethod]
        public void Authors_AuthorExists_ExistingAuthor_ReturnsTrue()
        {
            // Arrange
            var author = new Author { Id = 1, FirstName = "Test", LastName = "Author" };
            _context.Author.Add(author);
            _context.SaveChanges();

            // Use reflection to test private method
            var method = typeof(AuthorsController).GetMethod("AuthorExists",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)method.Invoke(_controller, new object[] { 1 });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Authors_AuthorExists_NonExistingAuthor_ReturnsFalse()
        {
            // Use reflection to test private method
            var method = typeof(AuthorsController).GetMethod("AuthorExists",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)method.Invoke(_controller, new object[] { 999 });

            // Assert
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class AdminsControllerTests
    {
        private AdminsController _controller;
        private FullMetalLibraryContext _context;
        private DbContextOptions<FullMetalLibraryContext> _options;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FullMetalLibraryContext>()
                .UseInMemoryDatabase(databaseName: "AdminsTest_" + Guid.NewGuid())
                .Options;

            _context = new FullMetalLibraryContext(_options);
            _controller = new AdminsController(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        // SIMPLE SESSION MOCK
        private void SetupSession(string userName = null)
        {
            var httpContext = new DefaultHttpContext();

            // Simple session simulation using Dictionary
            var session = new TestSession();
            if (userName != null)
            {
                session.SetString("AdminUser", userName);
            }

            httpContext.Session = session;

            // ADD AUTHENTICATION FOR AuthFilter
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName ?? "testuser") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            httpContext.User = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        // ===== LOGIN TESTS (Simplified) ====
        [TestMethod]
        public async Task Login_ValidCredentials_RedirectsToHome()
        {
            // Arrange
            var admin = new Admin
            {
                UserName = "testadmin",
                EmailAddress = "test@test.com",
                PasswordHash = PasswordHelper.HashPassword("ValidPass1!"),
                IsActive = true
            };
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            SetupSession(); 

            var model = new LoginViewModel { Email = "test@test.com", Password = "ValidPass1!" };

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }

        [TestMethod]
        public void Login_Get_WhenUserLoggedIn_RedirectsToHome()
        {
            // Arrange - User is already logged in
            SetupSession("existinguser");

            // Act
            var result = _controller.Login();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }

        [TestMethod]
        public void Login_Get_WhenUserNotLoggedIn_ReturnsView()
        {
            // Arrange - No user logged in
            SetupSession();

            // Act
            var result = _controller.Login();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(LoginViewModel));
        }

        [TestMethod]
        public async Task Login_InvalidPassword_ReturnsViewWithError()
        {
            // Arrange
            var admin = new Admin
            {
                UserName = "testadmin",
                EmailAddress = "test@test.com",
                PasswordHash = PasswordHelper.HashPassword("CorrectPass1!"),
                IsActive = true
            };
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            SetupSession();
            var model = new LoginViewModel { Email = "test@test.com", Password = "WrongPass1!" };

            // Act
            var result = await _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        
        [TestMethod]
        public void Logout_RedirectsToLogin()
        {
            // Arrange
            SetupSession("someuser");

            // Act
            var result = _controller.Logout();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Login", redirectResult.ActionName);
        }

        
        [TestMethod]
        public async Task Register_ValidData_CreatesAdmin()
        {
            // Arrange
            SetupSession();
            var model = new RegisterViewModel
            {
                UserName = "newuser",
                Email = "newuser@test.com",
                Password = "StrongPass1!",
                ConfirmPassword = "StrongPass1!"
            };

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            // Verify admin was created in database
            var adminInDb = await _context.Admin.FirstOrDefaultAsync(a => a.EmailAddress == "newuser@test.com");
            Assert.IsNotNull(adminInDb);
            Assert.AreEqual("newuser", adminInDb.UserName);
            Assert.IsTrue(adminInDb.IsActive);
        }

        [TestMethod]
        public async Task Register_DuplicateEmail_ReturnsError()
        {
            // Arrange
            var existingAdmin = new Admin
            {
                UserName = "existing",
                EmailAddress = "existing@test.com",
                PasswordHash = "hash"
            };
            _context.Admin.Add(existingAdmin);
            await _context.SaveChangesAsync();

            SetupSession();
            var model = new RegisterViewModel
            {
                UserName = "newuser",
                Email = "existing@test.com", 
                Password = "StrongPass1!",
                ConfirmPassword = "StrongPass1!"
            };

            // Act
            var result = await _controller.Register(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
            Assert.IsTrue(_controller.ModelState.ContainsKey("Email"));
        }

        
        [TestMethod]
        public async Task Index_ReturnsViewResult_WithAdminList()
        {
            // Arrange
            var admin = new Admin { UserName = "admin1", EmailAddress = "admin1@test.com", PasswordHash = "hash" };
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            SetupSession("testuser"); 

            // Act
            var result = await _controller.Index(null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<Admin>;
            Assert.AreEqual(1, model.Count);
        }

        [TestMethod]
        public async Task Details_ValidId_ReturnsViewWithAdmin()
        {
            // Arrange
            var admin = new Admin { Id = 1, UserName = "test", EmailAddress = "test@test.com" };
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            SetupSession("testuser");

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Admin));
            var model = result.Model as Admin;
            Assert.AreEqual("test", model.UserName);
        }

        [TestMethod]
        public async Task Details_NonExistentId_ReturnsNotFound()
        {
            // Arrange
            SetupSession("testuser");

            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_Post_ValidAdmin_CreatesAndRedirects()
        {
            // Arrange
            var admin = new Admin
            {
                UserName = "newadmin",
                EmailAddress = "newadmin@test.com",
                PasswordHash = "StrongPass1!",
                IsActive = true
            };

            SetupSession("testuser");

            // Act
            var result = await _controller.Create(admin);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            // Verify admin was created
            var adminInDb = await _context.Admin.FirstOrDefaultAsync(a => a.EmailAddress == "newadmin@test.com");
            Assert.IsNotNull(adminInDb);
        }

        
        [TestMethod]
        public void IsStrongPassword_ValidPasswords_ReturnsTrue()
        {
            // Arrange
            var strongPasswords = new[] { "StrongPass1!", "Another1@", "Test123#" };

            // Act & Assert
            foreach (var password in strongPasswords)
            {
                Assert.IsTrue(_controller.IsStrongPassword(password), $"Password '{password}' should be strong");
            }
        }

        [TestMethod]
        public void IsStrongPassword_WeakPasswords_ReturnsFalse()
        {
            // Arrange
            var weakPasswords = new[] { "weak", "weak123", "WEAK123", "WeakPass" };

            // Act & Assert
            foreach (var password in weakPasswords)
            {
                Assert.IsFalse(_controller.IsStrongPassword(password), $"Password '{password}' should be weak");
            }
        }

        [TestMethod]
        public void IsStrongPassword_NullOrEmpty_ReturnsFalse()
        {
            // Act & Assert
            Assert.IsFalse(_controller.IsStrongPassword(""));
            Assert.IsFalse(_controller.IsStrongPassword(null));
        }
    }

    // ===== SIMPLE SESSION IMPLEMENTATION =====
    public class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _storage = new Dictionary<string, byte[]>();

        public string Id => "TestSessionId";
        public bool IsAvailable => true;
        public IEnumerable<string> Keys => _storage.Keys;

        public void Clear() => _storage.Clear();

        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Remove(string key) => _storage.Remove(key);

        public void Set(string key, byte[] value) => _storage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _storage.TryGetValue(key, out value);

        
        public void SetString(string key, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            Set(key, bytes);
        }

        public string GetString(string key)
        {
            if (TryGetValue(key, out byte[] value))
            {
                return Encoding.UTF8.GetString(value);
            }
            return null;
        }
    }
}