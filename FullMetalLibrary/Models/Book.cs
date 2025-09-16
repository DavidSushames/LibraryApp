using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullMetalLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Author")]
        public int AuthorId { get; set; }          // Keep FK here

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }        // Navigation property

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string? Genre { get; set; }

        public bool Available { get; set; }
    }
}

//Book is one table where you can see all the books.