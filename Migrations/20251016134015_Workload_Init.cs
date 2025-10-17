using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class Workload_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkloadItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lot = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phase = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Etape = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tache = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Livrable = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkloadItems_WorkloadItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WorkloadItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkloadAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkloadItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    JoursHomme = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkloadAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkloadAllocations_WorkloadItems_WorkloadItemId",
                        column: x => x.WorkloadItemId,
                        principalTable: "WorkloadItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadAllocations_WorkloadItemId_ProfileId",
                table: "WorkloadAllocations",
                columns: new[] { "WorkloadItemId", "ProfileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadItems_ParentId",
                table: "WorkloadItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkloadItems_ProjectId_OrderIndex",
                table: "WorkloadItems",
                columns: new[] { "ProjectId", "OrderIndex" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkloadAllocations");

            migrationBuilder.DropTable(
                name: "WorkloadItems");
        }
    }
}
