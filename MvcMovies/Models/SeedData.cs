using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                //Look for any movies
                if (context.Movie.Any())
                {
                    return;
                }
                context.Movie.AddRange(
                    new Movie
                    {
                       Title = "A",
                       ReleaseDate = DateTime.Parse("1/1/1"),
                       Genre = "A",
                       Price = 1.00M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
