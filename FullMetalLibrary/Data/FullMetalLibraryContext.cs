using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FullMetalLibrary.Models;

namespace FullMetalLibrary.Data
{
    public class FullMetalLibraryContext : DbContext
    {
        public FullMetalLibraryContext (DbContextOptions<FullMetalLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<FullMetalLibrary.Models.Book> Book { get; set; } = default!;
    }
}
