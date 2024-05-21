using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAnalysisAPI.Migrations
{
    /// <inheritdoc />
    public partial class moreFieldsInAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Challenges",
                table: "Abouts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Improvements",
                table: "Abouts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Challenges",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Improvements",
                table: "Abouts");
        }
    }
}
