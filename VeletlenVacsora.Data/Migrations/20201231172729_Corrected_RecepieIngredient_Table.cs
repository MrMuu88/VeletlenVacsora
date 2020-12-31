using Microsoft.EntityFrameworkCore.Migrations;

namespace VeletlenVacsora.Data.Migrations
{
    public partial class Corrected_RecepieIngredient_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecepieIngredientModel_Ingredients_IngredientId",
                table: "RecepieIngredientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RecepieIngredientModel_Recepies_RecepieId",
                table: "RecepieIngredientModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecepieIngredientModel",
                table: "RecepieIngredientModel");

            migrationBuilder.RenameTable(
                name: "RecepieIngredientModel",
                newName: "RecepieIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_RecepieIngredientModel_IngredientId",
                table: "RecepieIngredients",
                newName: "IX_RecepieIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecepieIngredients",
                table: "RecepieIngredients",
                columns: new[] { "RecepieId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecepieIngredients_Ingredients_IngredientId",
                table: "RecepieIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecepieIngredients_Recepies_RecepieId",
                table: "RecepieIngredients",
                column: "RecepieId",
                principalTable: "Recepies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecepieIngredients_Ingredients_IngredientId",
                table: "RecepieIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecepieIngredients_Recepies_RecepieId",
                table: "RecepieIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecepieIngredients",
                table: "RecepieIngredients");

            migrationBuilder.RenameTable(
                name: "RecepieIngredients",
                newName: "RecepieIngredientModel");

            migrationBuilder.RenameIndex(
                name: "IX_RecepieIngredients_IngredientId",
                table: "RecepieIngredientModel",
                newName: "IX_RecepieIngredientModel_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecepieIngredientModel",
                table: "RecepieIngredientModel",
                columns: new[] { "RecepieId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecepieIngredientModel_Ingredients_IngredientId",
                table: "RecepieIngredientModel",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecepieIngredientModel_Recepies_RecepieId",
                table: "RecepieIngredientModel",
                column: "RecepieId",
                principalTable: "Recepies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
