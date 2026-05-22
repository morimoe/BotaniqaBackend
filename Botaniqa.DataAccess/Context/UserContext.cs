using Botaniqa.Domain.Entities.Cart;
using Botaniqa.Domain.Entities.Favorites;
using Botaniqa.Domain.Entities.Product;
using Botaniqa.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Botaniqa.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserData> Users { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }      // ← внутри класса
        public DbSet<FavoriteItem> FavoriteItems { get; set; } // ← внутри класса

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbSession.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteItem>()
                .HasOne<ProductData>()
                .WithMany()
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne<ProductData>()
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}