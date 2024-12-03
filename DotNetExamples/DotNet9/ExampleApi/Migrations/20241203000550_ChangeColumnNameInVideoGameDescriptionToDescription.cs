using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameInVideoGameDescriptionToDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "VideoGameDetails",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "VideoGameDetails",
                newName: "Name");
        }
    }
}
