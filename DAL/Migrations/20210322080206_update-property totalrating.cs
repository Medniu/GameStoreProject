using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updatepropertytotalrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(5,1)",
                nullable: true,
                computedColumnSql: "dbo.GetValue(Id)",
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true,
                oldComputedColumnSql: "dbo.GetValue(Id)");
        }
    }
}
