using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UBB_SE_2024_923_1.Migrations
{
    /// <inheritdoc />
    public partial class MostPlayedArtistInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MostPlayedArtistInformation",
                columns: table => new
                {
                    Artist_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Listen_Events = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MostPlayedArtistInformation", x => x.Artist_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MostPlayedArtistInformation");
        }
    }
}
