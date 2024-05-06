using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    Standard = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Size = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Length = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

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
                name: "Container",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Size = table.Column<int>(type: "INT", nullable: false),
                    ContentId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Container_Content",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Container_ContentId",
                table: "Container",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseContainerPosition");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "StorageCase");

            migrationBuilder.DropTable(
                name: "Content");
        }
    }
}
