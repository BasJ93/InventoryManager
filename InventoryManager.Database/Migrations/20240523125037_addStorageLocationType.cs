using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class addStorageLocationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "StorageLocation",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "StorageLocation");
        }
    }
}
