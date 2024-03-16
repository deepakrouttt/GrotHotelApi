using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrotHotelApi.Migrations
{
    /// <inheritdoc />
    public partial class addBlackOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ListDate",
                table: "BlackOutDates",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.AlterColumn<string>(
                name: "ListDate",
                table: "BlackOutDates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId",
                unique: true);
        }
    }
}
