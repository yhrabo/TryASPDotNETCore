using Microsoft.EntityFrameworkCore;
using Purchase.Core.Domain.Models;
using System;

namespace Purchase.Core.Infrastructure
{
    public class PurchaseCoreContext : DbContext
    {
        internal DbSet<Category> Categories { get; set; }
        internal DbSet<Domain.Models.Purchase> Purchases { get; set; }

        public PurchaseCoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Models.Purchase>().Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Domain.Models.Purchase>().Property(p => p.Price)
                .HasColumnType("money");
            modelBuilder.Entity<Domain.Models.Purchase>().Property(p => p.RowVersion)
                .IsRowVersion();
            modelBuilder.Entity<Domain.Models.Purchase>().Property(p => p.CategoryId)
                .IsRequired(false);
            modelBuilder.Entity<Domain.Models.Purchase>()
                .HasIndex(p => new { p.Name, p.DoneAt })
                .IsUnique();
            modelBuilder.Entity<Domain.Models.Purchase>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>().Property(c => c.Name)
                .HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name).IsUnique();
        }

        public void CreateAndSeedDb()
        {
            Database.EnsureCreated();

            var categories = new Category[]
            {
                new Category { Name = "Food", Description = "Home food."},
                new Category { Name = "Rent", Description = "Bills payments."},
                new Category { Name = "Evening time", Description = "Theaters, cinemas, etc."}
            };
            Categories.AddRange(categories);
            SaveChanges();

            var purchases = new Domain.Models.Purchase[]
            {
                new Domain.Models.Purchase { Name = "Supermarket", Price = 565.3m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 4, 19, 20, 0), CategoryId = 1 },
                new Domain.Models.Purchase { Name = "Violin gathering", Price = 200m, Quantity = 2,
                    DoneAt = new DateTime(2020, 4, 5, 15, 11, 0), CategoryId = 3 },
                new Domain.Models.Purchase { Name = "Supper", Price = 99.1m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 7, 20, 2, 3), CategoryId = 1 },
                new Domain.Models.Purchase { Name = "Water bill", Price = 80m, Quantity = 1,
                    DoneAt = new DateTime(2020, 4, 15, 17, 29, 35), CategoryId = 2 },
                new Domain.Models.Purchase { Name = "Philarmonia Orchestra", Price = 250m, Quantity = 2,
                    DoneAt = new DateTime(2020, 4, 17, 9, 31, 0), CategoryId = 3 },
            };
            Purchases.AddRange(purchases);
            SaveChanges();
        }
    }
}
