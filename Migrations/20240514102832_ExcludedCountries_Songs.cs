using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UBB_SE_2024_923_1.Migrations
{
    /// <inheritdoc />
    public partial class ExcludedCountries_Songs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ExcludedCountriesSongs",
               columns: table => new
               {
                   CountryId = table.Column<int>(type: "int", nullable: false),
                   SongId = table.Column<int>(type: "int", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_ExcludedCountriesSongs_SongId_CountryId", x => new { x.CountryId, x.SongId });
               });

               migrationBuilder.AddForeignKey(
                name: "FK_ExcludedCountriesSongs_SongId",
                table: "ExcludedCountriesSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId",
                onDelete: ReferentialAction.Cascade);

               migrationBuilder.AddForeignKey(
                name: "FK_ExcludedCountriesSongs_CountryId",
                table: "ExcludedCountriesSongs",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("insert into Countries VALUES (1, 'Romania') ");
            migrationBuilder.Sql("insert into Countries VALUES (2, 'UK') ");
            migrationBuilder.Sql("insert into Countries VALUES (3, 'USA') ");
            migrationBuilder.Sql("insert into Countries VALUES (4, 'Botswana') ");
            migrationBuilder.Sql("insert into Countries VALUES (5, 'Italy') ");
            migrationBuilder.Sql("insert into Countries VALUES (6, 'France') ");
            migrationBuilder.Sql("insert into Countries VALUES (7, 'Egypt') ");
            migrationBuilder.Sql("insert into Countries VALUES (8, 'Turkey') ");
            migrationBuilder.Sql("insert into Countries VALUES (9, 'Poland') ");
            migrationBuilder.Sql("insert into Countries VALUES (10,'Moldova') ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ExcludedCountriesSongs");
        }
    }
}
