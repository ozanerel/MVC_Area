using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Area.Models.Context.Seeds;
using MVC_Area.Models.Entities;

namespace MVC_Area.Models.Context
{
    public class ProjectContext : IdentityDbContext<AppUser>//, AppUserRole, int>
        //AppUSer dan sonra role ve primary key tipi belirtilmeli
        //<Tuser> yani <AppUser> IdentityUser dan miras almalı 
    {
        public ProjectContext(DbContextOptions<ProjectContext> options):base(options)
        {
            
        }
        public ProjectContext()
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-ATORPEDA;Database=AreaProjectDB;Trusted_Connection=true;TrustServerCertificate=True ");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API (model oluşturulurken tanımlanan ilişkilendirme yöntemi)
            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);



            modelBuilder.Entity<Category>().HasData(CategorySeedData.categories);
            modelBuilder.Entity<Product>().HasData(ProductSeedData.products);

            base.OnModelCreating(modelBuilder);
        }
    }
}
