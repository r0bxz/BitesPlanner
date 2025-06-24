using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitesPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class intitialk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_UserId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Plans",
                newName: "assignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_UserId",
                table: "Plans",
                newName: "IX_Plans_assignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_assignedUserId",
                table: "Plans",
                column: "assignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_assignedUserId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "assignedUserId",
                table: "Plans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_assignedUserId",
                table: "Plans",
                newName: "IX_Plans_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_UserId",
                table: "Plans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
