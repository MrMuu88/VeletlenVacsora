using Microsoft.EntityFrameworkCore.Migrations;

namespace VeletlenVacsora.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IngredientTypeID = table.Column<int>(nullable: true),
                    PackageTypeID = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ingredients_Categories_IngredientTypeID",
                        column: x => x.IngredientTypeID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Categories_PackageTypeID",
                        column: x => x.PackageTypeID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recepies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recepies_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecepieIngredient",
                columns: table => new
                {
                    RecepieID = table.Column<int>(nullable: false),
                    IngredientID = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecepieIngredient", x => new { x.RecepieID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_RecepieIngredient_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecepieIngredient_Recepies_RecepieID",
                        column: x => x.RecepieID,
                        principalTable: "Recepies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientTypeID",
                table: "Ingredients",
                column: "IngredientTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PackageTypeID",
                table: "Ingredients",
                column: "PackageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecepieIngredient_IngredientID",
                table: "RecepieIngredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Recepies_CategoryID",
                table: "Recepies",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecepieIngredient");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recepies");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
