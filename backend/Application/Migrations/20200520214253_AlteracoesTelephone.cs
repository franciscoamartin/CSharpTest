using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BludataTest.Migrations
{
    public partial class AlteracoesTelephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telephones",
                table: "Suppliers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Suppliers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telephones_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_SupplierId",
                table: "Telephones",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "Suppliers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephones",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
