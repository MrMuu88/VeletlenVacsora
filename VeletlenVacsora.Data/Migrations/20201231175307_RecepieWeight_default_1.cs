using Microsoft.EntityFrameworkCore.Migrations;

namespace VeletlenVacsora.Data.Migrations
{
    public partial class RecepieWeight_default_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Recepies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Recepies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 1);
        }
    }
}
