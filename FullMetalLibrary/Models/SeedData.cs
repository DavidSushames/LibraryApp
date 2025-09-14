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
            using (var context = new FullMetalLibraryContext(
                serviceProvider.GetRequiredService<DbContextOptions<FullMetalLibraryContext>>()))
            {
                context.Author.AddRange(
                    new Author
                    {
                        FirstName = "William",
                        LastName = "Powell",
                        Id = 1
                    },
                    new Author
                    {
                        FirstName = "Charles",
                        LastName = "Bronson",
                        Id = 2  
                    },
                    new Author
                    {
                        FirstName = "Lee",
                        LastName = "Marvin",
                        Id = 3
                    },
                    new Author
                    {
                        FirstName = "Clint",
                        LastName = "Eastwood",
                        Id = 4
                    }
                );
                //context.SaveChanges();

                //Look for any books
                if (context.Book.Any())
                {
                    return;
                }
                context.Book.AddRange(
                    new Book
                    {
                        Title = "The Anarchist's Cookbook",
                        AuthorId = 1,
                        PublishDate = DateTime.Parse("1/1/1971"),
                        Genre = "Reference",
                        Available = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}