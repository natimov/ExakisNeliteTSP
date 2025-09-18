using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class InitialProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Complexity = table.Column<int>(type: "int", nullable: false),
                    ArchitectCds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchitectInternal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectLead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: true),
                    ProposalRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyDirector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLineManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesRep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackProjectManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractCaBack = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractBackAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RemiseTjmPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectConsultants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectConsultants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectConsultants_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConsultants_ProjectId_Name",
                table: "ProjectConsultants",
                columns: new[] { "ProjectId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectConsultants");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
