using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class menuEntityChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenuTypes_MenuTypeId",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "MenuTypeId",
                table: "Menus",
                newName: "DishTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_MenuTypeId",
                table: "Menus",
                newName: "IX_Menus_DishTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_DishTypes_DishTypeId",
                table: "Menus",
                column: "DishTypeId",
                principalTable: "DishTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_DishTypes_DishTypeId",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "DishTypeId",
                table: "Menus",
                newName: "MenuTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_DishTypeId",
                table: "Menus",
                newName: "IX_Menus_MenuTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuTypes_MenuTypeId",
                table: "Menus",
                column: "MenuTypeId",
                principalTable: "MenuTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
