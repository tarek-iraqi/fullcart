using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullCart.Server.Persistence.Migrations;

/// <inheritdoc />
public partial class AddProductNameToOrderItem : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_OrderItem_Products_ProductId",
            table: "OrderItem");

        migrationBuilder.AddColumn<string>(
            name: "ProductName",
            table: "OrderItem",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ProductName",
            table: "OrderItem");

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItem_Products_ProductId",
            table: "OrderItem",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
