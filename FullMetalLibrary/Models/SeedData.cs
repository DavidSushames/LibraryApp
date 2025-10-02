using FullMetalLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FullMetalLibrary.Models
{
    public static class SeedData
    {
        // Used in Production (Program.cs)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new FullMetalLibraryContext(
                serviceProvider.GetRequiredService<DbContextOptions<FullMetalLibraryContext>>());

            Seed(context);
        }

        // Overload for Unit Testing (direct context)
        public static void Initialize(FullMetalLibraryContext context)
        {
            Seed(context);
        }

        // Shared logic
        private static void Seed(FullMetalLibraryContext context)
        {
            if (context.Author.Any() && context.Book.Any())
                return; // already seeded

            // --- Authors ---
            var authorsToAdd = new[]
            {
                new Author { FirstName = "William", LastName = "Powell" },
                new Author { FirstName = "Charles", LastName = "Bronson" },
                new Author { FirstName = "Lee", LastName = "Marvin" },
                new Author { FirstName = "Clint", LastName = "Eastwood" },
                new Author { FirstName = "Howard", LastName = "Lovecraft" }
            };

            for (int i = 0; i < authorsToAdd.Length; i++)
            {
                Author? author = authorsToAdd[i];
                if (!context.Author.Any(a => a.FirstName == author.FirstName && a.LastName == author.LastName))
                {
                    context.Author.Add(author);
                }
            }
            context.SaveChanges();

            // --- Books ---
            var authors = context.Author.ToList();

            var booksToAdd = new[]
            {
                new { Title = "The Anarchist's Cookbook", AuthorIndex = 0, PublishDate = new DateTime(1971, 1, 1, 0, 0, 0, DateTimeKind.Utc),  Genre="Reference", Available=true },
                new { Title = "The Shadow over Innsmouth", AuthorIndex = 4, PublishDate = new DateTime(1936,4,1, 0, 0, 0, DateTimeKind.Utc),  Genre="Horror", Available=true },
                new { Title = "Cowboy 101", AuthorIndex = 3, PublishDate = new DateTime(1999,1,1, 0, 0, 0, DateTimeKind.Utc),  Genre="Western", Available=false }
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
