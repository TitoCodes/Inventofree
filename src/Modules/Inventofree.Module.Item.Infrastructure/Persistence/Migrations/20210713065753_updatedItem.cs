using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.Item.Infrastructure.Persistence.Migrations
{
    public partial class updatedItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Item",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "Item",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Item",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "Item",
                table: "Items");
        }
    }
}
