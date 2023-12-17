using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Waitlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int BookId { get; set; }

        public LibraryUser User { get; set; }
        public Book Book { get; set; }

    }
}
