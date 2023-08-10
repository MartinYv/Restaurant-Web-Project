﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class dbSetAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PromoCode_PromoCodeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCode",
                table: "PromoCode");

            migrationBuilder.RenameTable(
                name: "PromoCode",
                newName: "PromoCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PromoCodes_PromoCodeId",
                table: "Orders",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PromoCodes_PromoCodeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes");

            migrationBuilder.RenameTable(
                name: "PromoCodes",
                newName: "PromoCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCode",
                table: "PromoCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PromoCode_PromoCodeId",
                table: "Orders",
                column: "PromoCodeId",
                principalTable: "PromoCode",
                principalColumn: "Id");
        }
    }
}
