using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitesPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "PlanItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Carbs",
                table: "PlanItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fats",
                table: "PlanItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Protein",
                table: "PlanItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "Fats",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "PlanItems");
        }
    }
}
