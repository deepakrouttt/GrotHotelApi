using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrotHotelApi.Migrations
{
    /// <inheritdoc />
    public partial class updateBlackout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.DropColumn(
                name: "ListDate",
                table: "BlackOutDates");

            migrationBuilder.CreateTable(
                name: "DateEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlackOutDateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateEntries_BlackOutDates_BlackOutDateId",
                        column: x => x.BlackOutDateId,
                        principalTable: "BlackOutDates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DateEntries_BlackOutDateId",
                table: "DateEntries",
                column: "BlackOutDateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateEntries");

            migrationBuilder.DropIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates");

            migrationBuilder.AddColumn<DateTime>(
                name: "ListDate",
                table: "BlackOutDates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BlackOutDates_RoomRateId",
                table: "BlackOutDates",
                column: "RoomRateId");
        }
    }
}
