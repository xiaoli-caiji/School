using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class DatabaseChange_22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChooseRounds",
                table: "UserCourses");

            migrationBuilder.AddColumn<string>(
                name: "ChooseRounds",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChooseRounds",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "ChooseRounds",
                table: "UserCourses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
