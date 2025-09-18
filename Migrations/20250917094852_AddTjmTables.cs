using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class AddTjmTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTjmItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Tjm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTjmItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferentielTjmItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Tjm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferentielTjmItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTjmItems_ProjectId_Category_Profile_Location",
                table: "ProjectTjmItems",
                columns: new[] { "ProjectId", "Category", "Profile", "Location" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferentielTjmItems_Category_Profile_Location",
                table: "ReferentielTjmItems",
                columns: new[] { "Category", "Profile", "Location" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTjmItems");

            migrationBuilder.DropTable(
                name: "ReferentielTjmItems");
        }
    }
}
