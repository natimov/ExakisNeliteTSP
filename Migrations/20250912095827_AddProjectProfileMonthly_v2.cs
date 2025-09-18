using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectProfileMonthly_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RemiseTjmPercent",
                table: "Projects",
                type: "decimal(9,4)",
                precision: 9,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectProfileMonthlies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfilRowId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    ChargeJh = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProfileMonthlies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProfileMonthlies_ProjectProfilRows_ProfilRowId",
                        column: x => x.ProfilRowId,
                        principalTable: "ProjectProfilRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProfileMonthlies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProfileMonthlies_ProfilRowId",
                table: "ProjectProfileMonthlies",
                column: "ProfilRowId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProfileMonthlies_ProjectId_ProfilRowId_Year_Month",
                table: "ProjectProfileMonthlies",
                columns: new[] { "ProjectId", "ProfilRowId", "Year", "Month" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProfileMonthlies");

            migrationBuilder.AlterColumn<decimal>(
                name: "RemiseTjmPercent",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,4)",
                oldPrecision: 9,
                oldScale: 4,
                oldNullable: true);
        }
    }
}
