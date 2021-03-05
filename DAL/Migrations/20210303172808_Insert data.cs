using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Insertdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryIndex", "DateOfCreation", "ProductName", "TotalRating" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1996, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quаke", 9.0m },
                    { 19, 3, new DateTime(2007, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heroes of Might and Magic 5", 9.9m },
                    { 18, 3, new DateTime(2002, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stronghold Crusader", 9.7m },
                    { 17, 3, new DateTime(2003, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "StarCraft 2", 9.5m },
                    { 16, 3, new DateTime(2005, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "WarCraft 3", 9.7m },
                    { 15, 2, new DateTime(2007, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Overlord", 9.1m },
                    { 14, 2, new DateTime(2015, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Souls 3", 9.5m },
                    { 13, 2, new DateTime(2012, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mount and Blade", 9.0m },
                    { 12, 2, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077", 7.5m },
                    { 20, 4, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Syberia", 6.3m },
                    { 11, 2, new DateTime(2015, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3", 9.9m },
                    { 9, 2, new DateTime(2005, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Skyrim", 9.5m },
                    { 8, 1, new DateTime(2017, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drift 5", 8.2m },
                    { 7, 1, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "NBA 2k21", 7.2m },
                    { 6, 1, new DateTime(2015, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rocket League", 8.2m },
                    { 5, 1, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pes 21", 9.2m },
                    { 4, 1, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fifa 21", 9.5m },
                    { 3, 0, new DateTime(2015, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Call of Duty", 8.1m },
                    { 2, 0, new DateTime(2018, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Battlefield", 7.3m },
                    { 10, 2, new DateTime(2017, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fallout 3", 9.5m },
                    { 21, 4, new DateTime(2012, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sherlock Holmes", 9.9m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);
        }
    }
}
