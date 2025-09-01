using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMyRatingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyRating",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyRating",
                table: "Movies",
                type: "integer",
                nullable: true);
        }
    }
}
