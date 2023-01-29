using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.Item.Infrastructure.Persistence.Migrations
{
    public partial class addedCategoryInItemsAddedItemsInCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Items",
                schema: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "Item",
                newName: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Items");

            migrationBuilder.EnsureSchema(
                name: "Item");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Items",
                newSchema: "Item");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "Item");
        }
    }
}
