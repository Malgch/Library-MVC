﻿using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(225)]
        public string Title { get; set; }

        [Required]
        [StringLength(225)]
        public string Author { get; set; }

        [StringLength(225)]
        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        

        public ICollection<Waitlist> Waitlist { get; set; }

        public ICollection<BookBorrowed> BooksBorrowed { get; set; }


    }
}
