using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LibraryUser
    {
        [Key]
        public int Id { get; set; }     
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(".+\\@.+\\.[a-z]{2,3}", ErrorMessage = "Please add a valid email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please set your password, it needs to be 8-20 digits/letters long")]
        [MinLength(8), MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        public ICollection<BookBorrowed> BooksBorrowed { get; set; }
    }
}
