using ECommerce.CartExperience.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.CartExperience.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasKey(e => new { e.ItemId, e.ItemName });

            modelBuilder.Entity<CartItem>()
                .HasKey(e => e.CartItemId);

            modelBuilder.Entity<Cart>()
                .HasKey(e => new { e.Id, e.PhoneNumber });

            base.OnModelCreating(modelBuilder);
        }

    }
}
