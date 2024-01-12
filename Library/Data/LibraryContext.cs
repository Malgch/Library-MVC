using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace Library.Data
{

    public class LibraryContext : IdentityDbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\localDb1;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }



        private class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<LibraryUser>
        {
            public void Configure(EntityTypeBuilder<LibraryUser> builder)
            {
                builder.Property(x => x.FirstName).HasMaxLength(255);
                builder.Property(x => x.Surname).HasMaxLength(255);
            }
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

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            Seed(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookBorrowed> BooksBorrowed { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Waitlist> Waitlists { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }

        private void Seed(ModelBuilder modelBuilder)
        {
  
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
             {
              Id = "1",
              Name = "Admin",
              NormalizedName = "ADMIN"
              });               

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
               Id = "2",
               Name = "User",
               NormalizedName = "USER"
             });
                            


            //////////////////////////////////////////////

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Classic"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                Name = "Thriller"
            });

            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Description = "Book explores themes of wealth, love, and the American Dream through the eyes of the mysterious Jay Gatsby.",
                IsAvailable = true,
                CategoryId = 1,
            });

            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 2,
                Title = "The Da Vinci Code",
                Author = "Dan Brown",
                Description = "A gripping mystery",
                IsAvailable = true,
                CategoryId = 2,
            });

        }
    }
}