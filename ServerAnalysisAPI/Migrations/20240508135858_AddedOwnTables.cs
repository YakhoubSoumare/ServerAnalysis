using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAnalysisAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedOwnTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Advantages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comparison = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryInsights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beneficiaries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Benefits_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenefitSources",
                columns: table => new
                {
                    BenefitId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitSources", x => new { x.BenefitId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_BenefitSources_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenefitSources_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerBasedApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseCases = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Limitations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerBasedApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerBasedApplications_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerlessFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseCases = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Limitations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerlessFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerlessFunctions_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerBasedApplicationSources",
                columns: table => new
                {
                    ServerBasedApplicationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerBasedApplicationSources", x => new { x.ServerBasedApplicationId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_ServerBasedApplicationSources_ServerBasedApplications_ServerBasedApplicationId",
                        column: x => x.ServerBasedApplicationId,
                        principalTable: "ServerBasedApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerBasedApplicationSources_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerlessFunctionSources",
                columns: table => new
                {
                    ServerlessFunctionId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerlessFunctionSources", x => new { x.ServerlessFunctionId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_ServerlessFunctionSources_ServerlessFunctions_ServerlessFunctionId",
                        column: x => x.ServerlessFunctionId,
                        principalTable: "ServerlessFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerlessFunctionSources_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_TopicId",
                table: "Benefits",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitSources_SourceId",
                table: "BenefitSources",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerBasedApplications_BenefitId",
                table: "ServerBasedApplications",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerBasedApplicationSources_SourceId",
                table: "ServerBasedApplicationSources",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerlessFunctions_BenefitId",
                table: "ServerlessFunctions",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerlessFunctionSources_SourceId",
                table: "ServerlessFunctionSources",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitSources");

            migrationBuilder.DropTable(
                name: "ServerBasedApplicationSources");

            migrationBuilder.DropTable(
                name: "ServerlessFunctionSources");

            migrationBuilder.DropTable(
                name: "ServerBasedApplications");

            migrationBuilder.DropTable(
                name: "ServerlessFunctions");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
