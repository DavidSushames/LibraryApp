using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FullMetalLibrary.Models;

namespace FullMetalLibrary.Data
{
    public class FullMetalLibraryContext(DbContextOptions<FullMetalLibraryContext> options)
    : DbContext(options)
    {
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<FullMetalLibrary.Models.Admin> Admin { get; set; } = default!;
    }

}