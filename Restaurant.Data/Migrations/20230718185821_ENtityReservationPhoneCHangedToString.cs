using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class ENtityReservationPhoneCHangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hour",
                table: "Reservations",
                type: "decimal(4,2)",
                maxLength: 22,
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 22);

            migrationBuilder.AddColumn<int>(
                name: "Persons",
                table: "Reservations",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Persons",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hour",
                table: "Reservations",
                type: "decimal(18,2)",
                maxLength: 22,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldMaxLength: 22,
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
