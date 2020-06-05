using Microsoft.EntityFrameworkCore.Migrations;

namespace BludataTest.Migrations
{
    public partial class Alteraçãodepropriedadeeentidadetelefone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Telephones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Suppliers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Companies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Telephones");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Companies");
        }
    }
}
