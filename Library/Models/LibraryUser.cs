using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LibraryUser : IdentityUser
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(".+\\@.+\\.[a-z]{2,3}", ErrorMessage = "Please add a valid email address!")]
        public string Email { get; set; }

        public bool IsBlocked { get; set; }


        public ICollection<BookBorrowed> BooksBorrowed { get; set; }
        public ICollection<Waitlist> Waitlists { get; set; }
    }
}
