using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(225, ErrorMessage = "The title must be minimum 2 characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(225, ErrorMessage = "The Author must be minimum 2 characters long.", MinimumLength = 2)]
        [RegularExpression("^[^\\W\\d_]+$", ErrorMessage = "Author can contain only letters.")]
        public string Author { get; set; }

        [StringLength(225, ErrorMessage = "The description must be minimum 10 characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        

        public ICollection<Waitlist> Waitlist { get; set; }

        public ICollection<BookBorrowed> BooksBorrowed { get; set; }


    }
}
