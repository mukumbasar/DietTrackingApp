using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DietApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FoodPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPhotos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserFoods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFoods_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFoods_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDayMealFoods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFoodID = table.Column<int>(type: "int", nullable: false),
                    MealTypeID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Portion = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FoodPhotoID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDayMealFoods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserDayMealFoods_FoodPhotos_FoodPhotoID",
                        column: x => x.FoodPhotoID,
                        principalTable: "FoodPhotos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserDayMealFoods_MealTypes_MealTypeID",
                        column: x => x.MealTypeID,
                        principalTable: "MealTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDayMealFoods_UserFoods_UserFoodID",
                        column: x => x.UserFoodID,
                        principalTable: "UserFoods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDayMealFoods_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Et Ürünleri" },
                    { 2, "Deniz Ürünleri" },
                    { 3, "Sebzeler" },
                    { 4, "Meyveler" },
                    { 5, "Süt ve Süt Ürünleri" },
                    { 6, "Baklagiller" },
                    { 7, "Tahıllar ve Ekmekler" },
                    { 8, "Hamur İşleri" },
                    { 9, "Atıştırmalıklar" },
                    { 10, "Tatlılar ve Şekerli Ürünler" },
                    { 11, "Salatalar" },
                    { 12, "İçecekler" },
                    { 13, "Çorba ve Çorba Çeşitleri" },
                    { 14, "Baharatlar ve Soslar" },
                    { 15, "Atıştırmalık Yiyecekler" },
                    { 16, "Diyabetik Ürünler" },
                    { 17, "Vegan Yiyecekler" },
                    { 18, "Vejetaryen Yiyecekler" },
                    { 19, "Glutensiz Ürünler" },
                    { 20, "Laktosiz Ürünler" },
                    { 21, "Sağlıklı Yağlar ve Yağlı Yiyecekler" },
                    { 22, "Dondurulmuş Yiyecekler" },
                    { 23, "Hazır Yemekler" },
                    { 24, "Hızlı Yiyecekler" },
                    { 25, "Sağlıklı Atıştırmalıklar" },
                    { 26, "Organik Ürünler" },
                    { 27, "Yoğurt ve Fermente Ürünler" },
                    { 28, "Kahvaltılıklar" },
                    { 29, "Kuruyemişler ve Tohumlar" },
                    { 30, "Diyet İçecekler" }
                });

            migrationBuilder.InsertData(
                table: "MealTypes",
                columns: new[] { "ID", "MealName" },
                values: new object[,]
                {
                    { 1, "Kahvaltı" },
                    { 2, "Brunch" },
                    { 3, "Öğle Yemeği" },
                    { 4, "Çay Vakti" },
                    { 5, "Akşam Yemeği" },
                    { 6, "Hafif Akşam Yemeği" },
                    { 7, "Gece Atıştırmalığı" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDayMealFoods_FoodPhotoID",
                table: "UserDayMealFoods",
                column: "FoodPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDayMealFoods_MealTypeID",
                table: "UserDayMealFoods",
                column: "MealTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDayMealFoods_UserFoodID",
                table: "UserDayMealFoods",
                column: "UserFoodID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDayMealFoods_UserID",
                table: "UserDayMealFoods",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoods_CategoryID",
                table: "UserFoods",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoods_UserID",
                table: "UserFoods",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDayMealFoods");

            migrationBuilder.DropTable(
                name: "FoodPhotos");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DropTable(
                name: "UserFoods");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
