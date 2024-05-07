using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tokobaju.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderEntitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orders_OrderId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_OrderId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderId",
                table: "orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orders_OrderId",
                table: "orders",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "id");
        }
    }
}
