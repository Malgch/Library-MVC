using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace Library.Data
{
    public class YourDbContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-7CRVRRO;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new LibraryContext(optionsBuilder.Options);
        }
    }
    public class LibraryContext : IdentityDbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7CRVRRO;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookBorrowed>()
                .HasKey(bb => new { bb.LibraryUserId, bb.BookId });


            modelBuilder.Entity<BookBorrowed>()
                  .HasOne(bb => bb.LibraryUser)
                  .WithMany(u => u.BooksBorrowed)
                  .HasForeignKey(bb => bb.LibraryUserId)
                   .IsRequired();

            modelBuilder.Entity<BookBorrowed>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BooksBorrowed)
                .HasForeignKey(bb => bb.BookId)
                .IsRequired();

            modelBuilder.Entity<Waitlist>()
                .HasOne(w => w.LibraryUser)
                .WithMany(u => u.Waitlists)
                .HasForeignKey(w => w.LibraryUserId)
                .IsRequired();

            modelBuilder.Entity<Waitlist>()
                .HasOne(w => w.Book)
                .WithMany(b => b.Waitlist)
                .HasForeignKey(w => w.BookId)
                .IsRequired();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookBorrowed> BooksBorrowed { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Waitlist> Waitlists { get; set; }


    }
}