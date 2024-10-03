using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerRaph.Migrations
{
    /// <inheritdoc />
    public partial class Favoris5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "FavorisId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FavorisId",
                table: "Tasks",
                column: "FavorisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Favorites_FavorisId",
                table: "Tasks",
                column: "FavorisId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Favorites_FavorisId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FavorisId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FavorisId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Favorites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
