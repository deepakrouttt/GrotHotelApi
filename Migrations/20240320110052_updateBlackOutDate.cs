using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrotHotelApi.Migrations
{
    /// <inheritdoc />
    public partial class updateBlackOutDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateEntries_BlackOutDates_BlackOutDateId",
                table: "DateEntries");

            migrationBuilder.AlterColumn<int>(
                name: "BlackOutDateId",
                table: "DateEntries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DateEntries_BlackOutDates_BlackOutDateId",
                table: "DateEntries",
                column: "BlackOutDateId",
                principalTable: "BlackOutDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateEntries_BlackOutDates_BlackOutDateId",
                table: "DateEntries");

            migrationBuilder.AlterColumn<int>(
                name: "BlackOutDateId",
                table: "DateEntries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DateEntries_BlackOutDates_BlackOutDateId",
                table: "DateEntries",
                column: "BlackOutDateId",
                principalTable: "BlackOutDates",
                principalColumn: "Id");
        }
    }
}
