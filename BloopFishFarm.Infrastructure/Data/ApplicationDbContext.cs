using BloopFishFarm.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BloopFishFarm.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fish> Fish { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Fish entity
            modelBuilder.Entity<Fish>()
                .Property(f => f.PricePerKg)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fish>()
                .Property(f => f.Weight)
                .HasColumnType("decimal(18,2)");

            // Configure Order entity
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Weight)
                
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.PricePerKg)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Seed some initial data
            modelBuilder.Entity<Fish>().HasData(
                new Fish
                {
                    Id = 1,
                    Name = "Catfish",
                    Type = "Live",
                    PricePerKg = 2500.99M,
                    Weight = 1.0M,
                    ImageUrl = "/images/smoked.png",
                    IsAvailable = true,
                    Description = "Fresh live catfish from our farm"
                },
                new Fish
                {
                    Id = 2,
                    Name = "Catfish",
                    Type = "Smoked",
                    PricePerKg = 2000.99M,
                    Weight = 1.0M,
                    ImageUrl = "/images/live.png",
                    IsAvailable = true,
                    Description = "Delicious smoked catfish, ready to eat"
                }
            );
        }
    }
}