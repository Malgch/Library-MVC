using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class SeedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Books (Title, Author, Description, IsAvailable, CategoryId) VALUES ('The Catcher in the Rye', 'J.D. Salinger', 'A classic coming-of-age novel', 'True', '5')");
            migrationBuilder.Sql("INSERT INTO Books (Title, Author, Description, IsAvailable, CategoryId) VALUES ('1984', 'George Orwell', 'A dystopian masterpiece', 'True', '2')");
            migrationBuilder.Sql("INSERT INTO Books (Title, Author, Description, IsAvailable, CategoryId) VALUES ('The Great Gatsby', 'F. Scott Fitzgerald', 'A tale of decadence, idealism, and obsession', 'True', '3')");
            migrationBuilder.Sql("INSERT INTO Books ( Title, Author, Description, IsAvailable, CategoryId) VALUES ( 'Harry Potter and the Sorcerers Stone', 'J.K. Rowling', 'The first installment in the magical series', 'True', '4')");
            migrationBuilder.Sql("INSERT INTO Books (Title, Author, Description, IsAvailable, CategoryId) VALUES ('One Hundred Years of Solitude', 'Gabriel Garcia Marquez', 'A magical realist epic spanning generations in the Buendía family', 'False', '5')");
            migrationBuilder.Sql("INSERT INTO Books (Title, Author, Description, IsAvailable, CategoryId) VALUES ('The Da Vinci Code', 'Dan Brown', 'A gripping mystery thriller', 'False', '1')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
