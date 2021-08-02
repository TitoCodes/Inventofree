using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.User.Infrastructure.Persistence.Migrations
{
    public partial class updatedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "User",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "User",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
