using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitesPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlanMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Plans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Plans");
        }
    }
}
