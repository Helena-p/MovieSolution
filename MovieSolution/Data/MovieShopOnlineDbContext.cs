using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieSolution.Entities;

namespace MovieSolution.Data
{
    public class MovieShopOnlineDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public MovieShopOnlineDbContext(DbContextOptions options) : base(options)
        {
            
        }

        // Seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Add Product Categories
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 1,
                Name = "Action",
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 2,
                Name = "Comedy",
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 3,
                Name = "Drama",
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 4,
                Name = "Sci-Fi",
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 5,
                Name = "Thriller",
            });
            //Products
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Title = "When Harry Met Sally",
                Description = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.",
                Director = "Rob Reiner",
                ReleaseYear = 1989,
                ImageURL = "https://miro.medium.com/v2/resize:fit:1400/format:webp/1*8u9DpZV620Yv0-65t9cVOQ@2x.jpeg",
                Price = 6,
                CategoryId = 2
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Title = "TopGun",
                Description = "As students at the United States Navy's elite fighter weapons school compete to be best in the class, one daring young pilot learns a few things from a civilian instructor that are not taught in the classroom.",
                Director = "Tony Scott",
                ReleaseYear = 1986,
                ImageURL = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/401320/small_8870f3dfdb243500f32398c661394202-Top_20Gun.jpg",
                Price = 7,
                CategoryId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Title = "1984",
                Description = "In a totalitarian future society, a man, whose daily work is re-writing history, tries to rebel by falling in love.",
                Director = "Michael Radford",
                ReleaseYear = 1986,
                ImageURL = "https://m.media-amazon.com/images/M/MV5BMWFkNzIzNDUtNWI1Mi00ODA2LTgyMTMtYTZiYWMxMDFlNmNhL2ltYWdlXkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_UX140_CR0,0,140,209_AL_.jpg",
                Price = 5,
                CategoryId = 4
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Title = "Dunkirk",
                Description = "Never has a military defeat looked so victorious as in Christopher Nolan's trifecta of interlocking vignettes in this old-school-feeling epic of the British desperate retreat from France in 1940.",
                Director = "Christopher Nolan",
                ReleaseYear = 2017,
                ImageURL = "https://www.movienewsletters.net/photos/SWE_225351R1.jpg",
                Price = 8,
                CategoryId = 3
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Title = "Tinker Tailor Soldier Spy",
                Description = "In the bleak days of the Cold War, espionage veteran George Smiley is forced from semi-retirement to uncover a Soviet Agent within MI6.",
                Director = "Tomas Alfredsson",
                ReleaseYear = 2011,
                ImageURL = "https://m.media-amazon.com/images/M/MV5BMTU2OTkwNzMyM15BMl5BanBnXkFtZTcwOTI4ODg2Ng@@._V1_UY209_CR0,0,140,209_AL_.jpg",
                Price = 12,
                CategoryId = 5
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Title = "Alien",
                Description = "The crew of a commercial spacecraft encounters a deadly lifeform after investigating an unknown transmission.",
                Director = "Ridley Scott",
                ReleaseYear = 1979,
                ImageURL = "https://m.media-amazon.com/images/M/MV5BOGQzZTBjMjQtOTVmMS00NGE5LWEyYmMtOGQ1ZGZjNmRkYjFhXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_FMjpg_UX1000_.jpg",
                Price = 4,
                CategoryId = 4
            });
        }
    }
}
