using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class SeedBooksBorrowed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO BooksBorrowed (StartTime, EndTime, IsReturned, LibraryUserId, BookId) VALUES ('2023-11-24 09:30:00', '2023-12-24 09:30:00', 'False', '5', '5')");
            migrationBuilder.Sql("INSERT INTO BooksBorrowed ( StartTime, EndTime, IsReturned, LibraryUserId, BookId) VALUES ( '2023-11-24 09:30:00', '2023-12-24 09:30:00', 'False', '6', '6')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
