using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tokobaju.Migrations
{
    /// <inheritdoc />
    public partial class FixStoreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_store_user_id",
                table: "store");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "store",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "store",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_store_user_id",
                table: "store",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_store_user_id",
                table: "store");

            migrationBuilder.AlterColumn<string>(
                name: "updated_at",
                table: "store",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "created_at",
                table: "store",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_store_user_id",
                table: "store",
                column: "user_id");
        }
    }
}
