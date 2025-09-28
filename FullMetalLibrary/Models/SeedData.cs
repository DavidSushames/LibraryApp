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

            // --- Seed Authors (add missing only) ---
            var authorsToAdd = new[]
            {
                new Author { FirstName = "William", LastName = "Powell" },
                new Author { FirstName = "Charles", LastName = "Bronson" },
                new Author { FirstName = "Lee", LastName = "Marvin" },
                new Author { FirstName = "Clint", LastName = "Eastwood" },
                new Author { FirstName = "Howard", LastName = "Lovecraft" }
            };

            foreach (var author in authorsToAdd)
            {
                if (!context.Author.Any(a => a.FirstName == author.FirstName && a.LastName == author.LastName))
                {
                    context.Author.Add(author);
                }
            }

            context.SaveChanges(); // IDs assigned here

            // Get all authors from DB
            var authors = context.Author.ToList();

            // --- Seed Books using only IDs ---
            var booksToAdd = new[]
            {
                new { Title = "The Anarchist's Cookbook", AuthorIndex = 0, PublishDate = new DateTime(1971,1,1), Genre="Reference", Available=true },
                new { Title = "The Shadow over Innsmouth", AuthorIndex = 4, PublishDate = new DateTime(1936,4,1), Genre="Horror", Available=true },
                new { Title = "Cowboy 101", AuthorIndex = 3, PublishDate = new DateTime(1999,1,1), Genre="Western", Available=false }
            };

            foreach (var b in booksToAdd)
            {
                var authorId = authors[b.AuthorIndex].Id;

                if (!context.Book.Any(book => book.Title == b.Title && book.AuthorId == authorId))
                {
                    context.Book.Add(new Book
                    {
                        Title = b.Title,
                        AuthorId = authorId,
                        PublishDate = b.PublishDate,
                        Genre = b.Genre,
                        Available = b.Available
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
