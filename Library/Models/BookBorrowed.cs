using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookBorrowed
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsReturned { get; set; }

        public int LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        
    }
}
