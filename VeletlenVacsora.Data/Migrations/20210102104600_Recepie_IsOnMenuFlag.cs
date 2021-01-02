using Microsoft.EntityFrameworkCore.Migrations;

namespace VeletlenVacsora.Data.Migrations
{
    public partial class Recepie_IsOnMenuFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnMenu",
                table: "Recepies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnMenu",
                table: "Recepies");
        }
    }
}
