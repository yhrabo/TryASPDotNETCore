using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Purchase.Core.Models
{
    public class PurchaseCoreContext : DbContext
    {
        internal DbSet<Category> Categories { get; set; }
        internal DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Purchase>().Property(p => p.Name)
                .HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Purchase>().Property(p => p.Price)
                .HasColumnType("money");
            modelBuilder.Entity<Purchase>().Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Category>().Property(c => c.Name)
                .HasMaxLength(50).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("PurchaseApp");
        }

        public void CreateAndSeedDb()
        {
            Database.EnsureCreated();

            var categories = new Category[]
            {
                new Category { CategoryId = 1, Name = "Food", Description = "Home food."},
                new Category { CategoryId = 2, Name = "Rent", Description = "Bills payments."},
                new Category { CategoryId = 5, Name = "Evening time", Description = "Theaters, cinemas, etc."}
            };
            Categories.AddRange(categories);
            SaveChanges();

            var purchases = new Purchase[]
            {
                new Purchase { PurchaseId = 1, Name = "Supermarket", Price = 565.3m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 4, 19, 20, 0), CategoryId = 1 },
                new Purchase { PurchaseId = 3, Name = "Violin gathering", Price = 200m, Quantity = 2,
                    DoneAt = new DateTime(2020, 4, 5, 15, 11, 0), CategoryId = 5 },
                new Purchase { PurchaseId = 4, Name = "Supper", Price = 99.1m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 7, 20, 2, 3), CategoryId = 1 },
                new Purchase { PurchaseId = 5, Name = "Water bill", Price = 80m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 15, 17, 29, 35), CategoryId = 2},
                new Purchase { PurchaseId = 14, Name = "Philarmonia Orchestra", Price = 250m, Quantity = 2,
                    DoneAt = new DateTime(2020, 4, 17, 9, 31, 0), CategoryId = 5 },
            };
            Purchases.AddRange(purchases);
            SaveChanges();
        }
    }
}
