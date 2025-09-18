using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class Recreate_ProjectTjmItems_Guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Drop table si elle existe (pour éviter l'ALTER int→guid qui plante)
            migrationBuilder.Sql(@"
        IF OBJECT_ID(N'[dbo].[ProjectTjmItems]', 'U') IS NOT NULL
            DROP TABLE [dbo].[ProjectTjmItems];
    ");

            // 2) Recréer la table avec ProjectId en GUID
            migrationBuilder.CreateTable(
                name: "ProjectTjmItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                                  .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Tjm = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTjmItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTjmItems_ProjectId_Category_Profile_Location",
                table: "ProjectTjmItems",
                columns: new[] { "ProjectId", "Category", "Profile", "Location" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revenir à l'ancienne version (ProjectId en int)
            migrationBuilder.Sql(@"
        IF OBJECT_ID(N'[dbo].[ProjectTjmItems]', 'U') IS NOT NULL
            DROP TABLE [dbo].[ProjectTjmItems];
    ");

            migrationBuilder.CreateTable(
                name: "ProjectTjmItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                                  .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Tjm = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTjmItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTjmItems_ProjectId_Category_Profile_Location",
                table: "ProjectTjmItems",
                columns: new[] { "ProjectId", "Category", "Profile", "Location" },
                unique: true);
        }


    }
}
