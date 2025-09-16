using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FullMetalLibrary.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
