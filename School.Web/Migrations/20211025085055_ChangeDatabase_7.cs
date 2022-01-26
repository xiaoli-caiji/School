using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class ChangeDatabase_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadImgAddress",
                table: "Users",
                newName: "HeadPictureAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadPictureAddress",
                table: "Users",
                newName: "HeadImgAddress");
            migrationBuilder.DropColumn(
                name: "HeadImg",
                table: "Users");
        }
    }
}
