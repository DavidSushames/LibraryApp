using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FullMetalLibrary.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new FullMetalLibraryContext(
                serviceProvider.GetRequiredService<DbContextOptions<FullMetalLibraryContext>>());

            if (context.Author.Any()) return; // already seeded

            var authors = new[]
            {
                new Author { FirstName = "William", LastName = "Powell" },
                new Author { FirstName = "Charles", LastName = "Bronson" },
                new Author { FirstName = "Lee", LastName = "Marvin" },
                new Author { FirstName = "Clint", LastName = "Eastwood" }
            };

            context.Author.AddRange(authors);
            context.SaveChanges(); // Ids are now assigned

            // Use AuthorId explicitly (keeps Book tied to id)
            context.Book.AddRange(
                new Book
                {
                    Title = "The Anarchist's Cookbook",
                    AuthorId = authors[0].Id, // safe: Id exists after SaveChanges()
                    PublishDate = DateTime.Parse("1971-01-01"),
                    Genre = "Reference",
                    Available = true
                }
            );

            context.SaveChanges();
        }
    }
}