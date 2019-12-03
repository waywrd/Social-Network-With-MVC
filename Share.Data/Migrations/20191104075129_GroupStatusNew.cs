using Microsoft.EntityFrameworkCore.Migrations;

namespace Share.Data.Migrations
{
    public partial class GroupStatusNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupStatusEnum",
                table: "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupStatusEnum",
                table: "Groups");
        }
    }
}
