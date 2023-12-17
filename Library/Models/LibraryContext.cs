using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext>options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7CRVRRO;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookBorrowed> BooksBorrowed { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LibraryAdmin> LibraryAdmins { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Waitlist> Waitlists { get; set; }
    }
}