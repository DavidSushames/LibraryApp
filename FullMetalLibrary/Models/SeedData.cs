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
                //Look for any movies
                if (context.Book.Any())
                {
                    return;
                }
                context.Book.AddRange(
                    new Book
                    {
                        Title = "The Anarchist's Cookbook",
                        Author = "William Powell",
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
