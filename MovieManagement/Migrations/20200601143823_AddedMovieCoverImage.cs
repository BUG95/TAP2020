using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieManagement.Migrations
{
    public partial class AddedMovieCoverImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieCoverImage",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieCoverImage",
                table: "Movies");
        }
    }
}
