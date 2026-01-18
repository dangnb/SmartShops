using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAdjustmentLine_InventoryAdjustments_InventoryAdjust~",
                table: "InventoryAdjustmentLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLine_PurchaseReturns_PurchaseReturnId",
                table: "PurchaseReturnLine");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferLine_StockTransfers_StockTransferId",
                table: "StockTransferLine");

            migrationBuilder.DropTable(
                name: "PurchaseReturns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransfers",
                table: "StockTransfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryAdjustments",
                table: "InventoryAdjustments");

            migrationBuilder.RenameTable(
                name: "StockTransfers",
                newName: "stock_transfers");

            migrationBuilder.RenameTable(
                name: "InventoryAdjustments",
                newName: "inventory_adjustments");

            migrationBuilder.AlterColumn<string>(
                name: "TransferNo",
                table: "stock_transfers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustmentNo",
                table: "inventory_adjustments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stock_transfers",
                table: "stock_transfers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory_adjustments",
                table: "inventory_adjustments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "purchase_returns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReturnNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupplierId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ComId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_returns", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_returns_ReturnNo",
                table: "purchase_returns",
                column: "ReturnNo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentLine_inventory_adjustments_InventoryAdjus~",
                table: "InventoryAdjustmentLine",
                column: "InventoryAdjustmentId",
                principalTable: "inventory_adjustments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLine_purchase_returns_PurchaseReturnId",
                table: "PurchaseReturnLine",
                column: "PurchaseReturnId",
                principalTable: "purchase_returns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferLine_stock_transfers_StockTransferId",
                table: "StockTransferLine",
                column: "StockTransferId",
                principalTable: "stock_transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAdjustmentLine_inventory_adjustments_InventoryAdjus~",
                table: "InventoryAdjustmentLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLine_purchase_returns_PurchaseReturnId",
                table: "PurchaseReturnLine");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferLine_stock_transfers_StockTransferId",
                table: "StockTransferLine");

            migrationBuilder.DropTable(
                name: "purchase_returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stock_transfers",
                table: "stock_transfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory_adjustments",
                table: "inventory_adjustments");

            migrationBuilder.RenameTable(
                name: "stock_transfers",
                newName: "StockTransfers");

            migrationBuilder.RenameTable(
                name: "inventory_adjustments",
                newName: "InventoryAdjustments");

            migrationBuilder.AlterColumn<string>(
                name: "TransferNo",
                table: "StockTransfers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustmentNo",
                table: "InventoryAdjustments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransfers",
                table: "StockTransfers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryAdjustments",
                table: "InventoryAdjustments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PurchaseReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ComId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturns", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentLine_InventoryAdjustments_InventoryAdjust~",
                table: "InventoryAdjustmentLine",
                column: "InventoryAdjustmentId",
                principalTable: "InventoryAdjustments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLine_PurchaseReturns_PurchaseReturnId",
                table: "PurchaseReturnLine",
                column: "PurchaseReturnId",
                principalTable: "PurchaseReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferLine_StockTransfers_StockTransferId",
                table: "StockTransferLine",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
