using Microsoft.EntityFrameworkCore.Migrations;

namespace YTMyprocte.Migrations
{
    public partial class NewInitAddIsbound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInbound",
                table: "Purchase",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInbound",
                table: "Purchase");
        }
    }
}
