using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class asdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Orders_OrderId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ShoppingCarts_CartId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_OrderId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartDetails",
                newName: "ShoppingCartId");

            migrationBuilder.RenameColumn(
                name: "CartItemId",
                table: "CartDetails",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                newName: "IX_CartDetails_ShoppingCartId");

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "CartDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartId",
                table: "CartDetails",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "CartDetails",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetails",
                newName: "CartItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ShoppingCartId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CartDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_OrderId",
                table: "CartDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Orders_OrderId",
                table: "CartDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ShoppingCarts_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
