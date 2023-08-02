using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;


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
		public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
		public DbSet<CartDetail> CartDetails { get; set; } = null!;
		public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
		public DbSet<Reservation> Reservations { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}