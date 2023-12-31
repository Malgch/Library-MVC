using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class SeedUsersAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Caroline', 'Everton', 'carever@user.com', '12345678', 'False')");
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Wanda', 'Daniels', 'wadan@user.com', '12345678', 'True')");
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Jon', 'Doe', 'user@library.com', '12345678', 'False')");
            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Brandi', 'Kirby', 'bkirby@user.com', '12345678', 'False')");

            migrationBuilder.Sql("INSERT INTO LibraryUsers (FirstName, Surname, Email, Password, IsBlocked) VALUES ('Ralph', 'Watts', 'admin@library.com', '12345678', 'False')");


            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Mystery')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Thriller')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Romance')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Fantasy')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Novel')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
