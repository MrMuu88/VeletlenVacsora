using Microsoft.EntityFrameworkCore.Migrations;

namespace VeletlenVacsora.Data.Migrations
{
    public partial class RecepieIsOnMenu_Modified_2_RecepieOnMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnMenu",
                table: "Recepies");

            migrationBuilder.AddColumn<int>(
                name: "OnMenu",
                table: "Recepies",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnMenu",
                table: "Recepies");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnMenu",
                table: "Recepies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
