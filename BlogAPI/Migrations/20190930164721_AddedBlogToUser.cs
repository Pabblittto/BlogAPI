﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAPI.Migrations
{
    public partial class AddedBlogToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogUserIdentity_User",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_User",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogUserIdentity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyBlog",
                table: "BlogUserIdentity",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogUserIdentity_BlogId",
                table: "BlogUserIdentity",
                column: "BlogId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUserIdentity_Blogs_BlogId",
                table: "BlogUserIdentity",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUserIdentity_Blogs_BlogId",
                table: "BlogUserIdentity");

            migrationBuilder.DropIndex(
                name: "IX_BlogUserIdentity_BlogId",
                table: "BlogUserIdentity");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogUserIdentity");

            migrationBuilder.DropColumn(
                name: "MyBlog",
                table: "BlogUserIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_User",
                table: "Blogs",
                column: "User",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogUserIdentity_User",
                table: "Blogs",
                column: "User",
                principalTable: "BlogUserIdentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
