using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExakisNeliteTSP.Migrations
{
    /// <inheritdoc />
    public partial class DropComputedCols_AchatItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPurchaseHt",
                table: "AchatItem");

            migrationBuilder.DropColumn(
                name: "AmountResaleHt",
                table: "AchatItem");

            migrationBuilder.DropColumn(
                name: "MarkupPercent",
                table: "AchatItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountPurchaseHt",
                table: "AchatItem",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountResaleHt",
                table: "AchatItem",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MarkupPercent",
                table: "AchatItem",
                type: "decimal(9,4)",
                precision: 9,
                scale: 4,
                nullable: true);
        }
    }
}
