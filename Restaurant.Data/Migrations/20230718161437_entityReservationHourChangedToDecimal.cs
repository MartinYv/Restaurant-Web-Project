using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class entityReservationHourChangedToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Hour",
                table: "Reservations",
                type: "decimal(18,2)",
                maxLength: 22,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 23);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Hour",
                table: "Reservations",
                type: "int",
                maxLength: 23,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 22);
        }
    }
}
