using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAPI.Migrations
{
    public partial class unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "BlogUserIdentity",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogUserIdentity_UserName",
                table: "BlogUserIdentity",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogName",
                table: "Blogs",
                column: "BlogName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_BlogUserIdentity_UserName",
                table: "BlogUserIdentity");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogName",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "BlogUserIdentity",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
