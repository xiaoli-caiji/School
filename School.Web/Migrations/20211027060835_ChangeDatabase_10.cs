using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class ChangeDatabase_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NewsWriteTime",
                table: "News",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsWriter",
                table: "News",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsWriteTime",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsWriter",
                table: "News");
        }
    }
}
