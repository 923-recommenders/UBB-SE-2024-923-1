using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UBB_SE_2024_923_1.Migrations
{
    /// <inheritdoc />
    public partial class AddSongAndImagePathToSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YoutubeLink",
                table: "Songs",
                newName: "SongPath");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "SongPath",
                table: "Songs",
                newName: "YoutubeLink");
        }
    }
}
