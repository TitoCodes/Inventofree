using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.Item.Infrastructure.Persistence.Migrations
{
    public partial class updateItemsAndAddedUpdatedByProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "Item",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Item",
                table: "Items");
        }
    }
}
