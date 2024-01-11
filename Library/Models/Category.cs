using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(225, ErrorMessage = "Category must be at lease 2 letters long.", MinimumLength = 2)]
        [RegularExpression("^[^\\W\\d_]+$", ErrorMessage = "Category can contain only letters.")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
