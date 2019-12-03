using Microsoft.EntityFrameworkCore.Migrations;

namespace Share.Data.Migrations
{
    public partial class removepublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Public",
                table: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Groups",
                nullable: false,
                defaultValue: false);
        }
    }
}
