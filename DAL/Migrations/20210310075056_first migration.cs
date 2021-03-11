using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CategoryIndex = table.Column<int>(type: "int", nullable: false),
                    AgeRating = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "varchar(200)", nullable: false),
                    BackGround = table.Column<string>(type: "varchar(200)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "date", nullable: false),
                    TotalRating = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BackGround", "CategoryIndex", "Count", "DateOfCreation", "IsDeleted", "Logo", "ProductName", "Price", "AgeRating", "TotalRating" },
                values: new object[,]
                {
                    { 1, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadBack.jpg", 0, 100, new DateTime(1996, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadLogo.jpg", "Left 4 Dead", 29.99m, 18, 0m },
                    { 19, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Heroesback.jpg", 3, 100, new DateTime(2007, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/HeroesLogo.jpg", "Heroes of Might and Magic 5", 29.99m, 12, 0m },
                    { 18, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongBack.jpg", 3, 100, new DateTime(2002, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongLogo.jpg", "Stronghold Crusader", 29.99m, 6, 0m },
                    { 17, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Starback.jpg", 3, 100, new DateTime(2003, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StarLogo.jpg", "StarCraft 2", 29.99m, 12, 0m },
                    { 16, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarBack.jpg", 3, 30, new DateTime(2005, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarLogo.jpg", "WarCraft 3", 19.99m, 12, 0m },
                    { 15, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordBack.jpg", 2, 100, new DateTime(2007, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordLogo.jpg", "Overlord", 9.99m, 6, 0m },
                    { 14, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkBack.jpg", 2, 200, new DateTime(2015, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkLogo.jpg", "Dark Souls 3", 19.99m, 18, 0m },
                    { 13, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Mountback.jpg", 2, 300, new DateTime(2012, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/MountLogo.jpg", "Mount and Blade", 39.99m, 18, 0m },
                    { 12, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberBack.jpg", 2, 100, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberLogo.jpg", "Cyberpunk 2077", 19.99m, 18, 0m },
                    { 20, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaBack.jpg", 4, 100, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaLogo.jpg", "Syberia", 4.99m, 6, 0m },
                    { 11, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherback.jpg", 2, 100, new DateTime(2015, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherLogo.jpg", "The Witcher 3", 49.99m, 18, 0m },
                    { 9, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimBack.jpg", 2, 1000, new DateTime(2005, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimLogo.jpg", "Skyrim", 19.99m, 18, 0m },
                    { 8, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftBack.jpg", 1, 100, new DateTime(2017, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftLogo.jpg", "Drift 5", 23.99m, 12, 0m },
                    { 7, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Nbaback.jpg", 1, 100, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/NbaLogo.jpg", "NBA 2k21", 24.99m, 12, 0m },
                    { 6, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagback.jpg", 1, 100, new DateTime(2015, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagLogo.jpg", "Rocket League", 19.99m, 12, 0m },
                    { 5, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesBack.jpeg", 1, 50, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesLogo.jpg", "Pes 21", 29.99m, 6, 0m },
                    { 4, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaBack.jpg", 1, 100, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaLogo.jpg", "Fifa 21", 9.99m, 18, 0m },
                    { 3, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CodBack.jpg", 0, 150, new DateTime(2015, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CoDLogo.jpg", "Call of Duty", 39.99m, 18, 0m },
                    { 2, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfBack.png", 0, 200, new DateTime(2018, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfLogo.jpg", "Battlefield", 19.99m, 18, 0m },
                    { 10, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Fallback.jpg", 2, 100, new DateTime(2017, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FallLogo.jpg", "Fallout 3", 29.99m, 18, 0m },
                    { 21, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Sherlockback.jpg", 4, 100, new DateTime(2012, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SherlockLogo.jpg", "Sherlock Holmes", 29.99m, 6, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRatings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_UserId",
                table: "ProductRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName_CategoryIndex_DateOfCreation_AgeRating_Price",
                table: "Products",
                columns: new[] { "ProductName", "CategoryIndex", "DateOfCreation", "AgeRating", "Price" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
