using ElectronicsShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.DAL.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            string AdminId = "66374cf0–7hur–bnu5-rf57-3r5706d72bgt";
            string CustomerId = "4ty74cf0–7h67–bnu5-fh67-45t706d72huy";
            string RoleId = "3417fyr-adr4–4f5e-afbf-58mrryi72gyk";

            modelBuilder.Entity<ProductCategory>().HasData(
               new ProductCategory { ProductCategoryId = 1, TypeNameAr = "جهاز تلفزيون", TypeNameEn = "Televisions" },
               new ProductCategory { ProductCategoryId = 2, TypeNameAr = "جهاز لابتوب", TypeNameEn = "Laptops" },
               new ProductCategory { ProductCategoryId = 3, TypeNameAr = "نظام صوت", TypeNameEn = "Sound Systems" });

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = RoleId,
                ConcurrencyStamp = RoleId
            });

            //create user
            var AdminUser = new ApplicationUser
            {
                Id = AdminId,
                UserName = "admin@admin.com",
                NormalizedUserName= "ADMIN@ADMIN.COM",
                NormalizedEmail= "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FullName = "Application Admin",
                FullAddress="Mokkattam",
                LockoutEnabled = false,
                PhoneNumber = "01013852175"
            };

            var CustomerUser = new ApplicationUser
            {
                Id = CustomerId,
                UserName = "eng.mhajjaj@gmail.com",
                Email = "eng.mhajjaj@gmail.com",
                NormalizedUserName = "ENG.MHAJJAJ@GMAIL.COM",
                NormalizedEmail = "ENG.MHAJJAJ@GMAIL.COM",
                EmailConfirmed = true,
                FullName = "Mohamed Hajjaj",
                FullAddress = "Mokkattam",
                LockoutEnabled = false,
                PhoneNumber = "01013852175"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            AdminUser.PasswordHash = ph.HashPassword(AdminUser, "12345");
            CustomerUser.PasswordHash = ph.HashPassword(AdminUser, "12345");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(AdminUser);
            modelBuilder.Entity<ApplicationUser>().HasData(CustomerUser);


            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = RoleId,
                    UserId = AdminId
                });
        }
    }

}