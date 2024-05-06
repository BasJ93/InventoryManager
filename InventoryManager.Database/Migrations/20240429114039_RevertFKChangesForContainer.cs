using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManager.Database.Migrations
{
    public partial class RevertFKChangesForContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Content",
                table: "Container");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContentId",
                table: "Container",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Content",
                table: "Container",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Content",
                table: "Container");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContentId",
                table: "Container",
                type: "UNIQUEIDENTIFIER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Content",
                table: "Container",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id");
        }
    }
}
