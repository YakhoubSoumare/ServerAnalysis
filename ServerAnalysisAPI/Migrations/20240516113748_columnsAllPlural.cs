using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAnalysisAPI.Migrations
{
    /// <inheritdoc />
    public partial class columnsAllPlural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "Topics",
                newName: "Introductions");

            migrationBuilder.RenameColumn(
                name: "Comparison",
                table: "Topics",
                newName: "Comparisons");

            migrationBuilder.RenameColumn(
                name: "Approach",
                table: "Topics",
                newName: "Approaches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introductions",
                table: "Topics",
                newName: "Introduction");

            migrationBuilder.RenameColumn(
                name: "Comparisons",
                table: "Topics",
                newName: "Comparison");

            migrationBuilder.RenameColumn(
                name: "Approaches",
                table: "Topics",
                newName: "Approach");
        }
    }
}
