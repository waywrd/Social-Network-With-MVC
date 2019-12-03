using Microsoft.EntityFrameworkCore.Migrations;

namespace Share.Data.Migrations
{
    public partial class CoverProfilePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhotoImagePath",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureImagePath",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhotoImagePath",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ProfilePictureImagePath",
                table: "AspNetUsers");
        }
    }
}
