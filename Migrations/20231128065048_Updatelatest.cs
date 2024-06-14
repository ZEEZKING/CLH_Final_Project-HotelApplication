using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLH_Final_Project.Migrations
{
    public partial class Updatelatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Sale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_BookingId",
                table: "Sale",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_CustomerId",
                table: "History",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Customers_CustomerId",
                table: "History",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Bookings_BookingId",
                table: "Sale",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Customers_CustomerId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Bookings_BookingId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_BookingId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_History_CustomerId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "History");
        }
    }
}
