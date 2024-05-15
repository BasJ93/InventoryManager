using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class changeStandardToComplexObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StandardId",
                table: "Content",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Standard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Path = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, defaultValue: ""),
                    AlternativeNames = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standard", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_StandardId",
                table: "Content",
                column: "StandardId");

            migrationBuilder.Sql("INSERT INTO Standard ( Name ) SELECT DISTINCT Standard FROM Content");
            
            migrationBuilder.Sql("UPDATE c SET c.StandardId = s.Id FROM Content c INNER JOIN Standard s ON c.Standard = s.Name ");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Content_Standard",
                table: "Content",
                column: "StandardId",
                principalTable: "Standard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "Standard",
                table: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Standard",
                table: "Content");

            migrationBuilder.DropTable(
                name: "Standard");

            migrationBuilder.DropIndex(
                name: "IX_Content_StandardId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "StandardId",
                table: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Standard",
                table: "Content",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "");
        }
    }
}
