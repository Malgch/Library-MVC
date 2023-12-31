using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Waitlist
    {
        public int Id { get; set; }
       

        public int LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
