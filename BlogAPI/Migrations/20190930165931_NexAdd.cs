using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAPI.Migrations
{
    public partial class NexAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogUserIdentity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogUserIdentity");
        }
    }
}
