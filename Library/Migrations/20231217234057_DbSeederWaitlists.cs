using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class DbSeederWaitlists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Waitlists (UserId, BookId) VALUES ('1','6')");
            migrationBuilder.Sql("INSERT INTO Waitlists (UserId, BookId) VALUES ('2','5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
