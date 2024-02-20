using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSolution.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Sci-Fi" },
                    { 5, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Director", "ImageURL", "Price", "ProductCategoryId", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.", "Rob Reiner", "https://miro.medium.com/v2/resize:fit:1400/format:webp/1*8u9DpZV620Yv0-65t9cVOQ@2x.jpeg", 6m, null, 1989, "When Harry Met Sally" },
                    { 2, 1, "As students at the United States Navy's elite fighter weapons school compete to be best in the class, one daring young pilot learns a few things from a civilian instructor that are not taught in the classroom.", "Tony Scott", "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/401320/small_8870f3dfdb243500f32398c661394202-Top_20Gun.jpg", 7m, null, 1986, "TopGun" },
                    { 3, 4, "In a totalitarian future society, a man, whose daily work is re-writing history, tries to rebel by falling in love.", "Michael Radford", "https://m.media-amazon.com/images/M/MV5BMWFkNzIzNDUtNWI1Mi00ODA2LTgyMTMtYTZiYWMxMDFlNmNhL2ltYWdlXkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_UX140_CR0,0,140,209_AL_.jpg", 5m, null, 1986, "1984" },
                    { 4, 3, "Never has a military defeat looked so victorious as in Christopher Nolan's trifecta of interlocking vignettes in this old-school-feeling epic of the British desperate retreat from France in 1940.", "Christopher Nolan", "https://www.movienewsletters.net/photos/SWE_225351R1.jpg", 8m, null, 2017, "Dunkirk" },
                    { 5, 5, "In the bleak days of the Cold War, espionage veteran George Smiley is forced from semi-retirement to uncover a Soviet Agent within MI6.", "Tomas Alfredsson", "https://m.media-amazon.com/images/M/MV5BMTU2OTkwNzMyM15BMl5BanBnXkFtZTcwOTI4ODg2Ng@@._V1_UY209_CR0,0,140,209_AL_.jpg", 12m, null, 2011, "Tinker Tailor Soldier Spy" },
                    { 6, 4, "The crew of a commercial spacecraft encounters a deadly lifeform after investigating an unknown transmission.", "Ridley Scott", "https://m.media-amazon.com/images/M/MV5BOGQzZTBjMjQtOTVmMS00NGE5LWEyYmMtOGQ1ZGZjNmRkYjFhXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg", 4m, null, 1979, "Alien" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
