using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class DbSeederBooksBorrowed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO BooksBorrowed (StartTime, EndTime, IsReturned, UserId, BookId) VALUES ('2023-11-24 09:30:00', '2023-12-24 09:30:00', 'False', '1', '5')");
            migrationBuilder.Sql("INSERT INTO BooksBorrowed ( StartTime, EndTime, IsReturned, UserId, BookId) VALUES ( '2023-11-24 09:30:00', '2023-12-24 09:30:00', 'False', '2', '6')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
