using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class DbSeederMoreUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Jon', 'Doe', 'jondoe@user.com', '12345678', 'False')");
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Brandi', 'Kirby', 'bkirby@user.com', '12345678', 'False')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
