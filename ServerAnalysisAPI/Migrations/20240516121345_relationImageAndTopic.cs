using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAnalysisAPI.Migrations
{
    /// <inheritdoc />
    public partial class relationImageAndTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Images",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TopicId",
                table: "Images",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Topics_TopicId",
                table: "Images",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Topics_TopicId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TopicId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Images");
        }
    }
}
