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
                        Name = "William Powell",
                        Id = 1
                    },
                    new Author
                    {
                        Name = "Charles Bronson",
                        Id = 2  
                    },
                    new Author
                    {
                        Name = "Lee Marvin",
                        Id = 3
                    },
                    new Author
                    {
                        Name = "Clint Eastwood",
                        Id = 4
                    }
                );
                //context.SaveChanges();

                //Look for any movies
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