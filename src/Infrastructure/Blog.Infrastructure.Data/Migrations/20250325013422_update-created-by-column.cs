using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatecreatedbycolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedBy",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Users",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Tags",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Tags",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "PostTags",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PostTags",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Posts",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Posts",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatedBy",
                table: "Posts",
                newName: "IX_Posts_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedById",
                table: "Posts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedById",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Tags",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tags",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "PostTags",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "PostTags",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Posts",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Posts",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                newName: "IX_Posts_CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedBy",
                table: "Posts",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
