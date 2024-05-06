using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UBB_SE_2024_923_1.Migrations
{
    /// <inheritdoc />
    public partial class UserPlayback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPlaybackBehaviour",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlaybackBehaviour", x => new { x.UserId, x.SongId, x.Timestamp });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPlaybackBehaviour");
        }
    }
}
