using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Computedproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(5,1)",
                nullable: false,
                computedColumnSql: "dbo.GetValue(Id)",
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(5,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldComputedColumnSql: "dbo.GetValue(Id)");
        }
    }
}
