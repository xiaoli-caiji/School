using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class ChangeDatabase_15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsHtmlImgsAddress",
                table: "News",
                newName: "NewsImgsAddress");

            migrationBuilder.RenameColumn(
                name: "NewsFileAndImgAddress",
                table: "News",
                newName: "NewsFileAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsImgsAddress",
                table: "News",
                newName: "NewsHtmlImgsAddress");

            migrationBuilder.RenameColumn(
                name: "NewsFileAddress",
                table: "News",
                newName: "NewsFileAndImgAddress");
        }
    }
}
