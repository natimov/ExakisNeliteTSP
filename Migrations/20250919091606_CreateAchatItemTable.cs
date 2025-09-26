using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class CreateAchatItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AchatItems",
                table: "AchatItems");

            migrationBuilder.RenameTable(
                name: "AchatItems",
                newName: "AchatItem");

            migrationBuilder.RenameIndex(
                name: "IX_AchatItems_ProjectId_Designation",
                table: "AchatItem",
                newName: "IX_AchatItem_ProjectId_Designation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchatItem",
                table: "AchatItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AchatItem",
                table: "AchatItem");

            migrationBuilder.RenameTable(
                name: "AchatItem",
                newName: "AchatItems");

            migrationBuilder.RenameIndex(
                name: "IX_AchatItem_ProjectId_Designation",
                table: "AchatItems",
                newName: "IX_AchatItems_ProjectId_Designation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchatItems",
                table: "AchatItems",
                column: "Id");
        }
    }
}
