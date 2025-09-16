using System.ComponentModel.DataAnnotations;

namespace FullMetalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string? Genre { get; set; }
        public bool Available { get; set; }

        public Author? Author { get; set; }
        
    }
}