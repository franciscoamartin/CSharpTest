using Microsoft.EntityFrameworkCore.Migrations;

namespace BludataTest.Migrations
{
    public partial class Ediçãodocumentodaempresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
