using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.DAL.Context
{
    public class AppDbContext : DbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserFood> UserFoods { get; set; }
        DbSet<UserDayMealFood> UserDayMealFoods { get; set; }
        DbSet<FoodPhoto> FoodPhotos { get; set; }
        DbSet<MealType> MealTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-F4M3HC0\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Initial Catalog=DietAppDb; Application Intent=ReadWrite;Multi Subnet Failover=False");
        }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Seed Data's
            //Categoriler
            mb.Entity<Category>().HasData(
                                            new { ID = 1, CategoryName = "Et Ürünleri" },
                                            new { ID = 2, CategoryName = "Deniz Ürünleri" },
                                            new { ID = 3, CategoryName = "Sebzeler" },
                                            new { ID = 4, CategoryName = "Meyveler" },
                                            new { ID = 5, CategoryName = "Süt ve Süt Ürünleri" },
                                            new { ID = 6, CategoryName = "Baklagiller" },
                                            new { ID = 7, CategoryName = "Tahıllar ve Ekmekler" },
                                            new { ID = 8, CategoryName = "Hamur İşleri" },
                                            new { ID = 9, CategoryName = "Atıştırmalıklar" },
                                            new { ID = 10, CategoryName = "Tatlılar ve Şekerli Ürünler" },
                                            new { ID = 11, CategoryName = "Salatalar" },
                                            new { ID = 12, CategoryName = "İçecekler" },
                                            new { ID = 13, CategoryName = "Çorba ve Çorba Çeşitleri" },
                                            new { ID = 14, CategoryName = "Baharatlar ve Soslar" },
                                            new { ID = 15, CategoryName = "Atıştırmalık Yiyecekler" },
                                            new { ID = 16, CategoryName = "Diyabetik Ürünler" },
                                            new { ID = 17, CategoryName = "Vegan Yiyecekler" },
                                            new { ID = 18, CategoryName = "Vejetaryen Yiyecekler" },
                                            new { ID = 19, CategoryName = "Glutensiz Ürünler" },
                                            new { ID = 20, CategoryName = "Laktosiz Ürünler" },
                                            new { ID = 21, CategoryName = "Sağlıklı Yağlar ve Yağlı Yiyecekler" },
                                            new { ID = 22, CategoryName = "Dondurulmuş Yiyecekler" },
                                            new { ID = 23, CategoryName = "Hazır Yemekler" },
                                            new { ID = 24, CategoryName = "Hızlı Yiyecekler" },
                                            new { ID = 25, CategoryName = "Sağlıklı Atıştırmalıklar" },
                                            new { ID = 26, CategoryName = "Organik Ürünler" },
                                            new { ID = 27, CategoryName = "Yoğurt ve Fermente Ürünler" },
                                            new { ID = 28, CategoryName = "Kahvaltılıklar" },
                                            new { ID = 29, CategoryName = "Kuruyemişler ve Tohumlar" },
                                            new { ID = 30, CategoryName = "Diyet İçecekler" });

            //Öğünler
            mb.Entity<MealType>().HasData(new { ID = 1, MealName = "Kahvaltı" });
            mb.Entity<MealType>().HasData(new { ID = 2, MealName = "Brunch" });
            mb.Entity<MealType>().HasData(new { ID = 3, MealName = "Öğle Yemeği" });
            mb.Entity<MealType>().HasData(new { ID = 4, MealName = "Çay Vakti" });
            mb.Entity<MealType>().HasData(new { ID = 5, MealName = "Akşam Yemeği" });
            mb.Entity<MealType>().HasData(new { ID = 6, MealName = "Hafif Akşam Yemeği" });
            mb.Entity<MealType>().HasData(new { ID = 7, MealName = "Gece Atıştırmalığı" });
            //Relations
            mb.Entity<Category>()
                .HasKey(c => c.ID);
            mb.Entity<Category>()
                .HasMany(c => c.UserFoods)
                .WithOne(f => f.Category)
                .HasForeignKey(f => f.CategoryID)
                .IsRequired();


            mb.Entity<UserFood>()
                .HasKey(f => f.ID);
            mb.Entity<UserFood>()
                .HasMany(uf => uf.UserDayMealFoods)
                .WithOne(udmf => udmf.UserFood)
                .HasForeignKey(udmf => udmf.UserFoodID);


            mb.Entity<User>()
                .HasKey(u => u.ID);

            mb.Entity<User>()
                .HasMany(u => u.UserFoods)
                .WithOne(um => um.User)
                .HasForeignKey(um => um.UserID);


            mb.Entity<User>()
                .HasOne(u => u.UserDetails)
                .WithOne(um => um.User)
                .HasForeignKey<UserDetails>(um => um.UserID);


            mb.Entity<MealType>()
                .HasKey(mt => mt.ID);
            mb.Entity<MealType>()
                .HasMany(mt => mt.UserDayMealFoods)
                .WithOne(mt => mt.MealType)
                .HasForeignKey(mt => mt.MealTypeID);

            mb.Entity<UserDayMealFood>()
                .HasKey(uf => uf.ID);
            mb.Entity<UserDayMealFood>()
                .Property(uf => uf.ID)
                .HasColumnOrder(1);
            mb.Entity<UserDayMealFood>()
                .Property(uf => uf.UserFoodID)
                .HasColumnOrder(3);
            mb.Entity<UserDayMealFood>()
                .Property(uf => uf.MealTypeID)
                .HasColumnOrder(4);
            mb.Entity<UserDayMealFood>()
                .Property(uf => uf.Portion)
                .HasPrecision(2, 1);


            mb.Entity<FoodPhoto>()
                .HasKey(fd => fd.ID);
            mb.Entity<FoodPhoto>()
                .HasMany(fd => fd.UserDayMealFoods)
                .WithOne(uf => uf.FoodPhoto)
                .HasForeignKey(uf => uf.FoodPhotoID)
                .IsRequired(false);


            mb.Entity<UserDetails>()
            .HasKey(uf => uf.ID);



        }
    }
}
