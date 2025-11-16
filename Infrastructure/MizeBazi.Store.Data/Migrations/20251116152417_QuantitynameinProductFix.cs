using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MizeBazi.Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuantitynameinProductFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "Products",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "StockQuantity");
        }
    }
}
