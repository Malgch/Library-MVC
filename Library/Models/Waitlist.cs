using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Waitlist
    {
        [Key]
        public int IdUser { get; set; }
        [Key]
        public int IdBook { get; set; }

        public LibraryUser User { get; set; }
        public Book Book { get; set; }

    }
}
