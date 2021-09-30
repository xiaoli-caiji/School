using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Web.Migrations
{
    public partial class ChangeDatabase_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ReportCards_CourseId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Courses");

            migrationBuilder.AddColumn<double>(
                name: "Report",
                table: "ReportCards",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ReportCards_CourseId",
                table: "ReportCards",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportCards_Courses_CourseId",
                table: "ReportCards",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportCards_Courses_CourseId",
                table: "ReportCards");

            migrationBuilder.DropIndex(
                name: "IX_ReportCards_CourseId",
                table: "ReportCards");

            migrationBuilder.DropColumn(
                name: "Report",
                table: "ReportCards");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Courses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ReportCards_CourseId",
                table: "Courses",
                column: "CourseId",
                principalTable: "ReportCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
