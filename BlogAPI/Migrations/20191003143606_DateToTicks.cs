using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAPI.Migrations
{
    public partial class DateToTicks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DateOfPost",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfPost",
                table: "Posts",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
