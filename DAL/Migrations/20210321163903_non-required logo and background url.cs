using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class nonrequiredlogoandbackgroundurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Products",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "BackGround",
                table: "Products",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Products",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BackGround",
                table: "Products",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);
        }
    }
}
