using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class SeedTjmDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReferentielTjmItems",
                columns: new[] { "Id", "Category", "Location", "Profile", "Tjm" },
                values: new object[,]
                {
                    { 1, "Agences", "Paris", "Consultant Junior", 500m },
                    { 2, "Agences", "Province", "Consultant Junior", 300m },
                    { 3, "Agences", "Région", "Consultant Junior", 261m },
                    { 4, "Agences", "Paris", "Consultant", 583m },
                    { 5, "Agences", "Province", "Consultant", 300m },
                    { 6, "Agences", "Région", "Consultant", 283m },
                    { 100, "CES Log", "CES", "Architecte", 652m },
                    { 101, "CES Log", "CES", "Chef de Projet", 603m },
                    { 102, "CES Log", "CES", "Dev confirmé", 570m },
                    { 103, "CES Log", "CES", "Dev junior", 417m },
                    { 104, "CES Log", "CES", "Manager", 915m },
                    { 200, "CES Infra", "CES", "Technicien Support N1", 180m },
                    { 201, "CES Infra", "CES", "Technicien Support N2", 235m },
                    { 202, "CES Infra", "CES", "Ingénieur Support N3", 325m },
                    { 203, "CES Infra", "CES", "TAM", 360m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 203);
        }
    }
}
