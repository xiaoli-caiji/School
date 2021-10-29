using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class ChangeDatabase_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "HeadImg",
                table: "Users",
                type: "longblob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadImg",
                table: "Users");
        }
    }
}
