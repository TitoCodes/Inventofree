using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventofree.Module.AuditTrail.Infrastructure.Persistence.Migrations
{
    public partial class added_new_property_in_audittrail_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "AuditTrails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AuditTrails");
        }
    }
}
