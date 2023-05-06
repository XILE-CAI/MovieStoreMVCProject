using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreMVC.Migrations
{
    /// <inheritdoc />
    public partial class fixbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovireGenre",
                table: "MovireGenre");

            migrationBuilder.RenameTable(
                name: "MovireGenre",
                newName: "MovieGenre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.RenameTable(
                name: "MovieGenre",
                newName: "MovireGenre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovireGenre",
                table: "MovireGenre",
                column: "Id");
        }
    }
}
