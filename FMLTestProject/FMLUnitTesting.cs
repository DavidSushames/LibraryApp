using FullMetalLibrary.Controllers;
using FullMetalLibrary.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography.Xml;

namespace FMLTestProject
{
    [TestClass]
    public class LoginViewModelTests
    {
        private List<ValidationResult> ValidationModel(LoginViewModel model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void ShowRequestId_ShouldTrue_IsNotEmpty()
        {
            //Arrange 
            var model = new ErrorViewModel { RequestId = "12345" };

            //Act 
            var result = model.ShowRequestId;

            //Assert
            Assert.IsTrue(result, "ShowRequestId should be true when RequestId in not empty.");
        }

        [TestMethod]
        public void ShowRequestId_ShouldIsEmpty()
        {
            //Arrange 
            var model = new ErrorViewModel { RequestId = "" };

            //Act 
            var result = model.ShowRequestId;

            //Assert 
            Assert.IsFalse(result, "ShowRequestId should be false when RequestId is empty.");
        }
    }
    [TestClass]
    public class BookModelTests
    {
        private IList<ValidationResult> ValidationResults(Book model)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, result, true);
            return result;
        }

        [TestMethod]
        public void BookModel_ValidData()
        {
            var book = new Book
            {
                Title = "Full Metal Stuff",
                AuthorId = 1,
                PublishDate = DateTime.Now,
                Genre = "Action",
                Available = true
            };

            var result = ValidationResults(book);

            Assert.AreEqual(0, result.Count, "Expected no validation errors for valid book model.");
        }

        [TestMethod]
        public void BookModel_MissingTitle()
        {
            var book = new Book
            {
                Title = "",
                AuthorId = 1,
                PublishDate = DateTime.Now,
                Genre = "Action",
                Available = true
            };

            var result = ValidationResults(book);

            Assert.IsTrue(result.Count > 0, "Expected validation error for missing Title.");
        }

        [TestMethod]
        public void BookModel_ValidGenre()
        {
            var validGenre = new[] { "Action", "Sci-Fi", "Sci Fi", "Comedy", "Historical" };

            foreach (var genre in validGenre)
            {
                var book = new Book
                {
                    Title = "Full Metal Stuff",
                    AuthorId = 1,
                    PublishDate = DateTime.Now,
                    Genre = genre,
                    Available = true
                };

                var result = ValidationResults(book);

                Assert.IsFalse(result.Any(), $"Expected no validation errors from genre '{genre}, but got: {string.Join(", ", result.Select(r => r.ErrorMessage))}");
            }
        }

        [TestMethod]
        public void BookModel_InvalidGenre()
        {
            var invalidGenres = new[] { "Action123", "Drama!", "Comedy&Action" };

            foreach (var genre in invalidGenres)
            {
                var book = new Book
                {
                    Title = "Full Metal Stuff",
                    AuthorId = 1,
                    PublishDate = DateTime.Now,
                    Genre = genre,
                    Available = true
                };

                var result = ValidationResults(book);
                Assert.IsTrue(result.Any(), $"Expected validation errors from genre '{genre}', but got none.");
            }
        }


    }
    [TestClass]
    public class AuthorModelTests
    {
        private IList<ValidationResult> ValidateModel(Author model)
        {
            var result =  new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, result, true);
            return result;
        }

        [TestMethod]
        public void Author_ValidData()
        {
            var author = new Author
            {
                FirstName = "Peter",
                LastName = "Parker"
            };

            var result = ValidateModel(author);

            Assert.AreEqual(0, result.Count, "Expected no validation errors for valid Author model.");
            Assert.AreEqual("Peter Parker", author.Name, "Name property should return 'FirstName and LastName'.");
        }

        [TestMethod]
        public void AuthorModel_MissingFirstName()
        {
            var author = new Author
            {
                FirstName = "",
                LastName = "Parker"
            };

            var result = ValidateModel(author);

            Assert.IsTrue(result.Count > 0, "Expected validation error for missing FirstName.");
        }

        [TestMethod]
        public void AuthorModel_InvalidFirstName()
        {
            var author = new Author
            {
                FirstName = "Peter1",
                LastName = "Parker"
            };

            var result = ValidateModel(author);

            Assert.IsTrue(result.Count > 0, "Expected validation error for invalid FirstName.");
        }

        [TestMethod]
        public void AuthorModel_InvalidLastName()
        {
            var author = new Author
            {
                FirstName = "Peter",
                LastName = "Parker!"
            };

            var result = ValidateModel(author);

            Assert.IsTrue(result.Count > 0, "Expected validation error for invalid LastName.");
        }


    }
    [TestClass]
    public class AdminModelTests
    {
        private IList<ValidationResult>ValidateModel(Admin model)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, result, true);
            return result;
        }

        [TestMethod]
        public void AdminValidate_PassValidation()
        {
            //Arrange
            var admin = new Admin
            {
                UserName = "TestUser",
                EmailAddress = "unit.test@example.com",
                PasswordHash = "Un!tPassword123"
            };

            //Act 
            var result = ValidateModel(admin);

            //Assert 
            Assert.AreEqual(0, result.Count, "Expected no vslidation errors for valid Admin model.");
        }

        [TestMethod]
        public void AdminModel_MissingUsername()
        {
            var admin = new Admin
            {
                UserName = "",
                EmailAddress = "unit.test@example.com",
                PasswordHash = "Un!tPassword123"
            };

            var result = ValidateModel(admin);

            Assert.IsTrue(result.Count > 0, "Expected validation errors for missing UserName.");
        }

        [TestMethod]
        public void AdminModel_InvalidEmail()
        {
            var admin = new Admin
            {
                UserName = "TestUser",
                EmailAddress = "unit.test-example.com",
                PasswordHash = "Un!tPassword123"
            };

            var result = ValidateModel(admin);

            Assert.IsTrue(result.Count > 0, "Expected validation errors for invalid email.");
        }

        [TestMethod]
        public void AdminModel_MissingPassword()
        {
            var admin = new Admin
            {
                UserName = "TestUser",
                EmailAddress = "unit.test@example.com",
                PasswordHash = ""
            };

            var result = ValidateModel(admin);

            Assert.IsTrue(result.Count > 0, "Expected validation errors for missing PasswordHash.");
        }
    }
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void IsStrongPass_ValidPass()
        {
            //Arrange
            var controller = new AdminsController(null);
            string password = "TestPass123@";

            //Act 
            var result = controller.IsStrongPassword(password);

            //Assert 
            Assert.IsTrue(result, "Expected a strong password to return true.");

        }

        [TestMethod]
        public void IsStrongPass_InvalidPass()
        {
            var controller = new AdminsController(null);
            string password = "testpass@";

            var result = controller.IsStrongPassword(password);

            Assert.IsFalse(result, "Expected a short password to return false.");
        }

        [TestMethod]
        public void IsStrongPass_MissingSymbols()
        {
            var controller = new AdminsController(null);
            string password = "TestPass123";

            var result = controller.IsStrongPassword(password);

            Assert.IsFalse(result, "Expected password without symbol to return fales.");
        }
    }
}
