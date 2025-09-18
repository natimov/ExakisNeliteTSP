using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProjectProfilRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProfilRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Profil = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Entite = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tjm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cjm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Prcs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FraisDeplacementSiCession = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TciOverPrcs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChargeJh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProfilRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProfilRows_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProfilRows_ProjectId",
                table: "ProjectProfilRows",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProfilRows");
        }
    }
}
