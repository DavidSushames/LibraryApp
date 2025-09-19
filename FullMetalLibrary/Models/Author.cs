using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullMetalLibrary.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\-]+$", ErrorMessage = "First name can only contain letters and hyphens.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-z\-]+$", ErrorMessage = "Last name can only contain letters and hyphens.")]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string Name => $"{FirstName} {LastName}".Trim();

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

//Author is one table where you can see all the authors.
