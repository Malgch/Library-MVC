using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class SeedWaitlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Waitlists (LibraryUserId, BookId) VALUES ('5','6')");
            migrationBuilder.Sql("INSERT INTO Waitlists (LibraryUserId, BookId) VALUES ('6','5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
