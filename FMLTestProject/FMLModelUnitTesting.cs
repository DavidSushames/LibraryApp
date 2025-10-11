using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace FMLTestProject
{
    [TestClass]
    public class SeedDataTests
    {
        private static FullMetalLibraryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<FullMetalLibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FullMetalLibraryContext(options);
        }

        [TestMethod]
        public void SeedData_AddAuthorAndBooks()
        {
            using var context = GetInMemoryContext();

            SeedData.Initialize(context);

            Assert.AreEqual(5, context.Author.Count(), "Expected 5 authors seeded.");
            Assert.IsTrue(context.Author.Any(a => a.LastName == "Lovecraft"), "Expected Lovecraft author to exist.");

            // Assert: Books
            Assert.AreEqual(3, context.Book.Count(), "Expected 3 books seeded.");
            Assert.IsTrue(context.Book.Any(b => b.Title == "The Shadow over Innsmouth"), "Expected specific book to exist.");

        }

        [TestMethod]
        public void SeedData_ShouldNotDuplicate()
        {
            using var context = GetInMemoryContext();

            SeedData.Initialize(context);
            SeedData.Initialize(context);

            Assert.AreEqual(5, context.Author.Count(), "Authors should not duplicate.");
            Assert.AreEqual(3, context.Book.Count(), "Books should not duplicate.");
        }
    }
    [TestClass]
    public class RegisterViewModelTests
    {
        private static List<ValidationResult> ValidationModel(RegisterViewModel model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, result, true);
            return result;
        }

        [TestMethod]
        public void Register_ValidData_ShouldPass()
        {
            var model = new RegisterViewModel
            {
                UserName = "TestUser",
                Email = "test@example.com",
                Password = "Un!tPassword123",
                ConfirmPassword = "Un!tPassword123"
            };

            var results = ValidationModel(model);
            Assert.AreEqual(0, results.Count, "Expect nno validation errors.");
        }

        [TestMethod]
        public void Register_MissignUserName()
        {
            var model = new RegisterViewModel
            {
                //UserName = "",
                Email = "test@example.com",
                Password = "Un!tPassword123",
                ConfirmPassword = "Un!tPassword123"
            };

            var results = ValidationModel(model);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results.Any(r => r.MemberNames.Contains("UserName")));
        }

        [TestMethod]
        public void Register_InvalidEmail()
        {
            var model = new RegisterViewModel
            {
                UserName = "TestUser",
                Email = "invalid-email",
                Password = "Un!tPassword123",
                ConfirmPassword = "Un!tPassword123"
            };

            var results = ValidationModel(model);
            Assert.IsTrue(results.Any(r => r.MemberNames.Contains("Email")));
        }

        [TestMethod]
        public void Register_WeekPassword()
        {
            var model = new RegisterViewModel
            {
                UserName = "TestUser",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };

            var results = ValidationModel(model);
            Assert.IsTrue(results.Any(r => r.MemberNames.Contains("Password")));
        }

        [TestMethod]
        public void Register_NotmatchinPasswords()
        {
            var model = new RegisterViewModel
            {
                UserName = "Testuser",
                Email = "test@example.com",
                Password = "Un!tPassword123",
                ConfirmPassword = "un!tPassword123"
            };

            var results = ValidationModel(model);
            Assert.IsTrue(results.Any(r => r.MemberNames.Contains("ConfirmPassword")));
        }

    }

    [TestClass]
    public class PasswordHelperTests
    {
        [TestMethod]
        public void HashPassword_ReturnHashedValue()
        {
            var password = "Un!tPassword123";

            var has1 = PasswordHelper.HashPassword(password);
            var has2 = PasswordHelper.HashPassword(password);

            Assert.AreNotEqual(has1, has2, "Hashing the same password twice should produce different results because of unidue salt."); ;
        }

        [TestMethod]
        public void VerifyPassword_ForCorrectPasswordAndHash()
        {
            var password = "StrongP@ssword123";
            var hash = PasswordHelper.HashPassword(password);

            // Act
            var result = PasswordHelper.VerifyPassword(password, hash);

            // Assert
            Assert.IsTrue(result, "VerifyPassword should return true for a valid password and its hash.");
        }

        [TestMethod]
        public void VerifyPassword_IncorrectPassword()
        {
            string password = "Un!tPassword123";
            string wrongPass = "WrongPass123";
            string hash = PasswordHelper.HashPassword(password);

            bool result = PasswordHelper.VerifyPassword(wrongPass, hash);

            Assert.IsFalse(result, "Password verification should fail with the wrong password.");
        }

        [TestMethod]
        public void HashPassword_DifferentHashforSamePassword()
        {
            string password = "Un!tPassword123";
            string hash1 = PasswordHelper.HashPassword(password);
            string hash2 = PasswordHelper.HashPassword(password);

            Assert.AreNotEqual(hash1, hash2, "Hashes should differ due to random salt.");
        }
    }
    [TestClass]
    public class LoginViewModelTests
    {
        private static List<ValidationResult> ValidationModel(LoginViewModel model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            return results;
        }

        [TestMethod]
        public void LoginViewModel_IsValidEmail()
        {
            var model = new LoginViewModel
            {
                Email = "admin@example.com",
                Password = "Un!tPassword123"
            };

            var result = ValidationModel(model);

            Assert.AreEqual(0, result.Count, "Expected no validation errors for valid login data.");
        }

        [TestMethod]
        public void LoginModel_InvalidEmail()
        {
            var model = new LoginViewModel
            {
                Email = "not-an-email",
                Password = "Un!tPassword123"
            };

            var result = ValidationModel(model);

            Assert.IsTrue(result.Exists(r => r.ErrorMessage.Contains("Invalid Email or Password")));
        }

        [TestMethod]
        public void LoginViewModel_EmptyPassword()
        {
            var model = new LoginViewModel
            {
                Email = "admin@example.com",
                Password = ""
            };

            var result = ValidationModel(model);

            Assert.IsFalse(result.Exists(r => r.ErrorMessage.Contains("Password is requierd")));
        }

        [TestMethod]
        public void LoginViewModel_EmptyPassword_ShouldFail()
        {
            var model = new LoginViewModel
            {
                Email = "admin@example.com",
                Password = ""
            };

            var result = ValidationModel(model);

            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("Password is required")),
                "Expected error message 'Password is required'.");
        }

        [TestMethod]
        public void LoginViewModel_InvalidPassword_ShouldFailRegex()
        {
            var model = new LoginViewModel
            {
                Email = "admin@example.com",
                Password = "password" // lowercase only, invalid per regex
            };

            var result = ValidationModel(model);

            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("Password must contain")),
        "Expected regex validation to catch weak password.");
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
        private static List<ValidationResult> ValidationResults(Book model)
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

                Assert.IsTrue(result.Count == 0,
                $"Expected no validation errors from genre '{genre}', but got: {string.Join(", ", result.Select(r => r.ErrorMessage))}");

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
                Assert.IsTrue(result.Count > 0,  $"Expected validation errors from genre '{genre}', but got none.");
            }
        }


    }
    [TestClass]
    public class AuthorModelTests
    {
        private static List<ValidationResult> ValidateModel(Author model)
        {
            var result = new List<ValidationResult>();
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
        private static List<ValidationResult> ValidateModel(Admin model)
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
}
   

