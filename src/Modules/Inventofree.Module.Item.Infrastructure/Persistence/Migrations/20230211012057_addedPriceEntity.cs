using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.Item.Infrastructure.Persistence.Migrations
{
    public partial class addedPriceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PriceId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_PriceId",
                table: "Items",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Price_PriceId",
                table: "Items",
                column: "PriceId",
                principalTable: "Price",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Price_PriceId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropIndex(
                name: "IX_Items_PriceId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Items");
        }
    }
}
