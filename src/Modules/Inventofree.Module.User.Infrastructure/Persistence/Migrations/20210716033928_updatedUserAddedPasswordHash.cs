using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.User.Infrastructure.Persistence.Migrations
{
    public partial class updatedUserAddedPasswordHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "User",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                schema: "User",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                schema: "User",
                table: "Users");
        }
    }
}
