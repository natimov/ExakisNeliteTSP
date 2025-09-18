using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class Reseed_Tjm_Full : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [ReferentielTjmItems];");

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

            migrationBuilder.InsertData(
                table: "ReferentielTjmItems",
                columns: new[] { "Id", "Category", "Location", "Profile", "Tjm" },
                values: new object[,]
                {
                    { 1000, "Agences", "TJM Paris", "Consultant Junior", 500m },
                    { 1001, "Agences", "CJM Paris", "Consultant Junior", 300m },
                    { 1002, "Agences", "TJM Région", "Consultant Junior", 435m },
                    { 1003, "Agences", "CJM Région", "Consultant Junior", 261m },
                    { 1010, "Agences", "TJM Paris", "Consultant", 583m },
                    { 1011, "Agences", "CJM Paris", "Consultant", 350m },
                    { 1012, "Agences", "TJM Région", "Consultant", 482m },
                    { 1013, "Agences", "CJM Région", "Consultant", 289m },
                    { 1020, "Agences", "TJM Paris", "Consultant confirmé", 717m },
                    { 1021, "Agences", "CJM Paris", "Consultant confirmé", 430m },
                    { 1022, "Agences", "TJM Région", "Consultant confirmé", 567m },
                    { 1023, "Agences", "CJM Région", "Consultant confirmé", 340m },
                    { 1030, "Agences", "TJM Paris", "Expert", 792m },
                    { 1031, "Agences", "CJM Paris", "Expert", 475m },
                    { 1032, "Agences", "TJM Région", "Expert", 667m },
                    { 1033, "Agences", "CJM Région", "Expert", 400m },
                    { 1040, "Agences", "TJM Paris", "Expert senior", 842m },
                    { 1041, "Agences", "CJM Paris", "Expert senior", 505m },
                    { 1042, "Agences", "TJM Région", "Expert senior", 767m },
                    { 1043, "Agences", "CJM Région", "Expert senior", 460m },
                    { 1050, "Agences", "TJM Paris", "Architecte", 842m },
                    { 1051, "Agences", "CJM Paris", "Architecte", 505m },
                    { 1052, "Agences", "TJM Région", "Architecte", 697m },
                    { 1053, "Agences", "CJM Région", "Architecte", 418m },
                    { 1060, "Agences", "TJM Paris", "Architecte Senior", 917m },
                    { 1061, "Agences", "CJM Paris", "Architecte Senior", 550m },
                    { 1062, "Agences", "TJM Région", "Architecte Senior", 833m },
                    { 1063, "Agences", "CJM Région", "Architecte Senior", 500m },
                    { 1070, "Agences", "TJM Paris", "Chef de Projet", 755m },
                    { 1071, "Agences", "CJM Paris", "Chef de Projet", 453m },
                    { 1072, "Agences", "TJM Région", "Chef de Projet", 625m },
                    { 1073, "Agences", "CJM Région", "Chef de Projet", 375m },
                    { 1080, "Agences", "TJM Paris", "Chef de Projet Senior", 883m },
                    { 1081, "Agences", "CJM Paris", "Chef de Projet Senior", 530m },
                    { 1082, "Agences", "TJM Région", "Chef de Projet Senior", 718m },
                    { 1083, "Agences", "CJM Région", "Chef de Projet Senior", 431m },
                    { 1090, "Agences", "TJM Paris", "Directeur de projet", 983m },
                    { 1091, "Agences", "CJM Paris", "Directeur de projet", 590m },
                    { 1092, "Agences", "TJM Région", "Directeur de projet", 867m },
                    { 1093, "Agences", "CJM Région", "Directeur de projet", 520m },
                    { 1100, "Agences", "TJM Paris", "Consultant Fonctionnel", 648m },
                    { 1101, "Agences", "CJM Paris", "Consultant Fonctionnel", 389m },
                    { 1102, "Agences", "TJM Région", "Consultant Fonctionnel", 505m },
                    { 1103, "Agences", "CJM Région", "Consultant Fonctionnel", 303m },
                    { 1110, "Agences", "TJM Paris", "Consultant Fonctionnel Senior", 700m },
                    { 1111, "Agences", "CJM Paris", "Consultant Fonctionnel Senior", 420m },
                    { 1112, "Agences", "TJM Région", "Consultant Fonctionnel Senior", 600m },
                    { 1113, "Agences", "CJM Région", "Consultant Fonctionnel Senior", 360m },
                    { 1120, "Agences", "TJM Paris", "Expert Fonctionnel", 775m },
                    { 1121, "Agences", "CJM Paris", "Expert Fonctionnel", 465m },
                    { 1122, "Agences", "TJM Région", "Expert Fonctionnel", 683m },
                    { 1123, "Agences", "CJM Région", "Expert Fonctionnel", 410m },
                    { 1130, "Agences", "TJM Paris", "Référent", 1000m },
                    { 1131, "Agences", "CJM Paris", "Référent", 600m },
                    { 1132, "Agences", "TJM Région", "Référent", 767m },
                    { 1133, "Agences", "CJM Région", "Référent", 460m },
                    { 1140, "Agences", "TJM Paris", "Manager", 1050m },
                    { 1141, "Agences", "CJM Paris", "Manager", 630m },
                    { 1142, "Agences", "TJM Région", "Manager", 833m },
                    { 1143, "Agences", "CJM Région", "Manager", 500m },
                    { 2000, "CES Log", "TJM CES", "Architecte", 652m },
                    { 2001, "CES Log", "CJM CES", "Architecte", 391m },
                    { 2002, "CES Log", "PRCS CES", "Architecte", 555m },
                    { 2010, "CES Log", "TJM CES", "Chef de Projet", 603m },
                    { 2011, "CES Log", "CJM CES", "Chef de Projet", 362m },
                    { 2012, "CES Log", "PRCS CES", "Chef de Projet", 514m },
                    { 2020, "CES Log", "TJM CES", "Dev confirmé", 570m },
                    { 2021, "CES Log", "CJM CES", "Dev confirmé", 342m },
                    { 2022, "CES Log", "PRCS CES", "Dev confirmé", 486m },
                    { 2030, "CES Log", "TJM CES", "Dev junior", 417m },
                    { 2031, "CES Log", "CJM CES", "Dev junior", 250m },
                    { 2032, "CES Log", "PRCS CES", "Dev junior", 355m },
                    { 2040, "CES Log", "TJM CES", "Manager", 915m },
                    { 2041, "CES Log", "CJM CES", "Manager", 549m },
                    { 2042, "CES Log", "PRCS CES", "Manager", 780m },
                    { 3000, "CES Infra", "TJM CES", "Technicien Support N1", 180m },
                    { 3001, "CES Infra", "CJM CES", "Technicien Support N1", 180m },
                    { 3010, "CES Infra", "TJM CES", "Technicien Support N2", 235m },
                    { 3011, "CES Infra", "CJM CES", "Technicien Support N2", 235m },
                    { 3020, "CES Infra", "TJM CES", "Ingénieur Support N3", 325m },
                    { 3021, "CES Infra", "CJM CES", "Ingénieur Support N3", 325m },
                    { 3030, "CES Infra", "TJM CES", "TAM", 360m },
                    { 3031, "CES Infra", "CJM CES", "TAM", 360m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1120);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1121);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1122);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1130);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1131);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1132);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1133);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1140);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1141);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1142);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 1143);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2040);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2041);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3000);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3010);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3011);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3020);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3021);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3030);

            migrationBuilder.DeleteData(
                table: "ReferentielTjmItems",
                keyColumn: "Id",
                keyValue: 3031);

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
    }
}
