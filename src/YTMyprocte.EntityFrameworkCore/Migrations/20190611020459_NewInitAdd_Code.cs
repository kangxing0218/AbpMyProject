using Microsoft.EntityFrameworkCore.Migrations;

namespace YTMyprocte.Migrations
{
    public partial class NewInitAdd_Code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PurchaseDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PurchaseDetail");
        }
    }
}
