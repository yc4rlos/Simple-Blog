using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "About", "CreatedAt", "CreatedById", "DeletedAt", "Email", "ImageFileName", "Login", "Name", "Password", "ResetPassword", "Role", "Slug", "UpdatedAt", "UpdatedById" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, "author@example.com", null, "author", "Author", "$2a$11$/Sy9bhuVPG3RoJrIAIjX5uZmZcj6jys4E/p2dgKSb5OQoJoh7VQoa", false, 0, "author", null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
