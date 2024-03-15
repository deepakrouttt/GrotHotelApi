using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrotHotelApi.Migrations
{
    /// <inheritdoc />
    public partial class addException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomRates_BlackOutDates_BlackOutDateId",
                table: "RoomRates");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomRates_HotelRooms_HotelRoomId",
                table: "RoomRates");

            migrationBuilder.DropIndex(
                name: "IX_RoomRates_BlackOutDateId",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "AdultChildSetting",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "BlackOutDateId",
                table: "RoomRates");

            migrationBuilder.AlterColumn<int>(
                name: "HotelRoomId",
                table: "RoomRates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChildAllow",
                table: "RoomRates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExtraAdult",
                table: "RoomRates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingleEqualDouble",
                table: "RoomRates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "HotelRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomRateId",
                table: "BlackOutDates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlackOutDates_RoomRates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId",
                principalTable: "RoomRates",
                principalColumn: "RoomRateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRates_HotelRooms_HotelRoomId",
                table: "RoomRates",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "HotelRoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackOutDates_RoomRates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomRates_HotelRooms_HotelRoomId",
                table: "RoomRates");

            migrationBuilder.DropIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.DropColumn(
                name: "IsChildAllow",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "IsExtraAdult",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "IsSingleEqualDouble",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.AlterColumn<int>(
                name: "HotelRoomId",
                table: "RoomRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdultChildSetting",
                table: "RoomRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlackOutDateId",
                table: "RoomRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "HotelRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRates_BlackOutDateId",
                table: "RoomRates",
                column: "BlackOutDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRates_BlackOutDates_BlackOutDateId",
                table: "RoomRates",
                column: "BlackOutDateId",
                principalTable: "BlackOutDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRates_HotelRooms_HotelRoomId",
                table: "RoomRates",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "HotelRoomId");
        }
    }
}
