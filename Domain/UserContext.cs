using System;
using ApiBooks.Enums;
using Microsoft.EntityFrameworkCore;

namespace ApiBooks.Domain
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<UserBase> UserBases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  => optionsBuilder.UseNpgsql("Host=localhost,5432;Database=postgres;Username=postgres;Password=host123");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<UserBase>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Articles);

            modelBuilder.Entity<UserBase>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Articles
            modelBuilder.Entity<Articles>()
                .HasKey(a => a.ArticlesId);
            modelBuilder.Entity<Articles>()
                .Property(a => a.Name)
                .HasMaxLength(80)
                .IsRequired();
            modelBuilder.Entity<Articles>()
               .Property(a => a.Description)
               .HasMaxLength(1000);
            modelBuilder.Entity<Articles>()
               .Property(a => a.ImgUrl)
               .HasMaxLength(1000);
              
            modelBuilder.Entity<Articles>()
               .Property(a => a.Content)
               .HasMaxLength(5000)
               .IsRequired();

            modelBuilder.Entity<Articles>()
                .HasOne(a => a.User)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Articles>()
                .HasOne(a => a.Categories)
                .WithOne(a => a.Articles)
                .OnDelete(DeleteBehavior.Restrict);
               

           

            //Categories 
            /*
            modelBuilder.Entity<Categories>()
                .HasOne(c => c.Articles)
                .WithOne(c => c.Categories)
                .HasForeignKey<Articles>(p => p.CategoriesId)
                .OnDelete(DeleteBehavior.Restrict); */ 

            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoriesId);

            modelBuilder.Entity<Categories>()
                .Property(c => c.ParentId)
                .HasDefaultValue(null);



        }
    }
}
