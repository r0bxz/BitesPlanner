using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitesPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanItems",
                table: "PlanItems");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlanItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanItems",
                table: "PlanItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_PlanId",
                table: "PlanItems",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanItems",
                table: "PlanItems");

            migrationBuilder.DropIndex(
                name: "IX_PlanItems_PlanId",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlanItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanItems",
                table: "PlanItems",
                columns: new[] { "PlanId", "LineNumber" });
        }
    }
}
