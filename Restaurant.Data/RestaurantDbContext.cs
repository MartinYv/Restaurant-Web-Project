using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using System.Runtime.CompilerServices;

namespace Restaurant2.Data
{
    public class RestaurantDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; } = null!;

        public DbSet<DishType> DishTypes { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<MenuType> MenuTypes { get; set; } = null!;

        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public DbSet<CartDetail> CartDetails { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        // public DbSet<UserOrder> UsersOrders { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<UserOrder>().HasKey(k => new { k.OrderId, k.UserId});
            //
            //builder.Entity<UserOrder>()
            //    .HasOne(u=> u.User)
            //    .WithMany()
            //    .HasForeignKey(u=>u.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Table>().HasOne(u => u.Customer).WithMany().HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}