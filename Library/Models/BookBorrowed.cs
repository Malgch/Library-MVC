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

        public LibraryUser User { get; set; }
        public int IdUser { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
