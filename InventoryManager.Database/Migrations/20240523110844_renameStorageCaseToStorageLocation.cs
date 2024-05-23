using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class renameStorageCaseToStorageLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    SizeX = table.Column<int>(type: "INT", nullable: false),
                    SizeY = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageLocationContainerPosition",
                columns: table => new
                {
                    StorageLocationId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ContainerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PositionX = table.Column<int>(type: "INT", nullable: false),
                    PositionY = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocationContainerPosition", x => new { x.StorageLocationId, x.ContainerId });
                    table.ForeignKey(
                        name: "FK_Container_StorageLocationContainerPosition",
                        column: x => x.ContainerId,
                        principalTable: "Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageLocation_StorageLocationContainerPosition",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocationContainerPosition_ContainerId",
                table: "StorageLocationContainerPosition",
                column: "ContainerId",
                unique: true);
            
            // TODO: Run SQL code to transfer the data from the old tables to the new ones
            migrationBuilder.Sql("INSERT INTO StorageLocation ( Id, Name, SizeX, SizeY ) SELECT Id, Name, SizeX, SizeY FROM StorageCase");
            migrationBuilder.Sql("INSERT INTO StorageLocationContainerPosition ( StorageLocationId, ContainerId, PositionX, PositionY ) SELECT CaseId, ContainerId, PositionX, PositionY FROM CaseContainerPosition");
            
            migrationBuilder.DropTable(
                name: "CaseContainerPosition");

            migrationBuilder.DropTable(
                name: "StorageCase");

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeNames",
                table: "Standard",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageLocationContainerPosition");

            migrationBuilder.DropTable(
                name: "StorageLocation");

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeNames",
                table: "Standard",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StorageCase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    SizeX = table.Column<int>(type: "INT", nullable: false),
                    SizeY = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageCase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseContainerPosition",
                columns: table => new
                {
                    CaseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ContainerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PositionX = table.Column<int>(type: "INT", nullable: false),
                    PositionY = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseContainerPosition", x => new { x.CaseId, x.ContainerId });
                    table.ForeignKey(
                        name: "FK_Container_CaseContainerPosition",
                        column: x => x.ContainerId,
                        principalTable: "Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageCase_CaseContainerPosition",
                        column: x => x.CaseId,
                        principalTable: "StorageCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseContainerPosition_ContainerId",
                table: "CaseContainerPosition",
                column: "ContainerId",
                unique: true);
        }
    }
}
