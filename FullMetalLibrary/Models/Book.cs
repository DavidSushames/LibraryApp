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
        public int AuthorId { get; set; }          

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }        

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s\-]+$", ErrorMessage = "Genre can only contain letters, space and hyphens.")]
        public string Genre { get; set; } = string.Empty;

        public bool Available { get; set; }
    }
}

//Book is one table where you can see all the books.