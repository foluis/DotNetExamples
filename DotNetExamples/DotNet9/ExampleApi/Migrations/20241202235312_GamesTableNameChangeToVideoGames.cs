using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleApi.Migrations
{
    /// <inheritdoc />
    public partial class GamesTableNameChangeToVideoGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameDetails_Games_VideoGameId",
                table: "VideoGameDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "VideoGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoGames",
                table: "VideoGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameDetails_VideoGames_VideoGameId",
                table: "VideoGameDetails",
                column: "VideoGameId",
                principalTable: "VideoGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGameDetails_VideoGames_VideoGameId",
                table: "VideoGameDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoGames",
                table: "VideoGames");

            migrationBuilder.RenameTable(
                name: "VideoGames",
                newName: "Games");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGameDetails_Games_VideoGameId",
                table: "VideoGameDetails",
                column: "VideoGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
