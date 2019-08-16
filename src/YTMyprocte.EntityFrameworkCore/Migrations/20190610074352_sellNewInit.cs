using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YTMyprocte.Migrations
{
    public partial class sellNewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "materielCode",
                table: "PurchaseDetail",
                newName: "MaterielCode");

            migrationBuilder.CreateTable(
                name: "Sell",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sell", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellOrderDe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrderCode = table.Column<string>(maxLength: 32, nullable: true),
                    MaterielId = table.Column<int>(nullable: false),
                    MaterielCode = table.Column<string>(maxLength: 32, nullable: true),
                    MaterielName = table.Column<string>(maxLength: 32, nullable: true),
                    GoodsFomat = table.Column<string>(maxLength: 32, nullable: true),
                    GoodsUnit = table.Column<string>(nullable: true),
                    SellMoney = table.Column<double>(nullable: false),
                    BuyMoney = table.Column<double>(nullable: false),
                    OutPrice = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrderDe", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sell");

            migrationBuilder.DropTable(
                name: "SellOrderDe");

            migrationBuilder.RenameColumn(
                name: "MaterielCode",
                table: "PurchaseDetail",
                newName: "materielCode");
        }
    }
}
