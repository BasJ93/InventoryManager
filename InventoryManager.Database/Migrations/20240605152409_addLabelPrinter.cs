using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class addLabelPrinter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabelDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    CommandText = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelPrinterConfiguration",
                columns: table => new
                {
                    LabelPrinterEnabled = table.Column<bool>(type: "BIT", nullable: false),
                    NetworkLabelPrinter = table.Column<bool>(type: "BIT", nullable: false),
                    LabelPrinterAddress = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    HasCutter = table.Column<bool>(type: "BIT", nullable: false),
                    UsesDelayedCut = table.Column<bool>(type: "BIT", nullable: false),
                    DelayedCutterCommand = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelDefinition");

            migrationBuilder.DropTable(
                name: "LabelPrinterConfiguration");
        }
    }
}
