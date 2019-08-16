using Microsoft.EntityFrameworkCore.Migrations;

namespace YTMyprocte.Migrations
{
    public partial class AddSellIsbound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutbound",
                table: "Sell",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutbound",
                table: "Sell");
        }
    }
}
