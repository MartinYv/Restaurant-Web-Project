namespace Restaurant.Tests.ServiceTests
{
	using System;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Http;
	using Moq;
	using NUnit.Framework;

	using Restaurant.Services.Data;
	using Restaurant.Services.Data.Interfaces;
	using Restaurant2.Data;
	using Restaurant.Data.Models;
	using Restaurant.ViewModels.Models.Order;


	[TestFixture]
	public class ShoppingCartServiceTests
	{
		private IShoppingCartService shoppingCartService = null!;
		private RestaurantDbContext dbContext;
		private IHttpContextAccessor httpContextAccessor;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RestaurantDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			dbContext = new RestaurantDbContext(options);
			httpContextAccessor = new HttpContextAccessor();
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Database.EnsureDeleted();
		}

		[Test]
		public void AddItem_ShouldThrowException_WhenUserIsNotLoggedIn()
		{
			var userId = string.Empty;
			var dishId = 1;
			var qty = 2;

			MockHttpContextAccessor(userId);

			Assert.ThrowsAsync<ArgumentException>(async () => await shoppingCartService.AddItem(dishId, qty));
		}

		[Test]
		public async Task AddItem_ShouldCreateCartAndCreateDetail()
		{
			var dish = new Dish
			{
				Id = 1,
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1,
				ImageUrl = "test.jpg",
				Price = 10.33m

			};
			await dbContext.Dishes.AddAsync(dish);

			var userId = Guid.NewGuid().ToString();
			var qty = 2;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });

			await dbContext.SaveChangesAsync();

			await shoppingCartService.AddItem(dish.Id, qty);

			var cart = await dbContext.ShoppingCarts.Include(c => c.CartDetails).FirstOrDefaultAsync(c => c.UserId == Guid.Parse(userId));

			Assert.NotNull(cart);
			Assert.That(dbContext.ShoppingCarts.Count(), Is.EqualTo(1));

			Assert.That(cart.CartDetails.Count, Is.EqualTo(1));
			Assert.That(cart.CartDetails.First().DishId, Is.EqualTo(dish.Id));
		}

		[Test]
		public async Task AddItem_ShouldIncreaseQuantity_WhenCartItemExists()
		{
			var dish = new Dish
			{
				Id = 1,
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1,
				ImageUrl = "test.jpg",
				Price = 10m

			};
			await dbContext.Dishes.AddAsync(dish);

			var userId = Guid.NewGuid().ToString();
			var qty = 2;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });

			await dbContext.SaveChangesAsync();

			dbContext.ShoppingCarts.Add(new ShoppingCart { UserId = Guid.Parse(userId) });
			dbContext.CartDetails.Add(new CartDetail { ShoppingCartId = 1, DishId = dish.Id, Quantity = 3, UnitPrice = 10 });

			await dbContext.SaveChangesAsync();

			await shoppingCartService.AddItem(dish.Id, qty);

			var cartDetail = await dbContext.CartDetails.FirstOrDefaultAsync(cd => cd.DishId == dish.Id);

			Assert.That(cartDetail, Is.Not.Null);
			Assert.That(cartDetail.Quantity, Is.EqualTo(5));
		}

		[Test]
		public void RemoveItem_ShouldThrowException_WhenUserIsNotLoggedIn()
		{
			var userId = string.Empty;
			var dishId = 1;

			MockHttpContextAccessor(userId);

			Assert.ThrowsAsync<Exception>(async () => await shoppingCartService.RemoveItem(dishId));
		}

		[Test]
		public async Task RemoveItem_ShouldThrowException_WhenCartIsNull()
		{
			var userId = Guid.NewGuid().ToString();
			var dishId = 1;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			await dbContext.SaveChangesAsync();

			Assert.ThrowsAsync<Exception>(async () => await shoppingCartService.RemoveItem(dishId));
		}

		[Test]
		public async Task RemoveItem_ShouldThrowException_WhenCartItemIsNull()
		{
			var userId = Guid.NewGuid().ToString();
			var dishId = 1;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			dbContext.ShoppingCarts.Add(new ShoppingCart { UserId = Guid.Parse(userId) });

			await dbContext.SaveChangesAsync();

			Assert.ThrowsAsync<Exception>(async () => await shoppingCartService.RemoveItem(dishId));
		}

		[Test]
		public async Task RemoveItem_ShouldRemoveCartItem_WhenQuantityIsOne()
		{
			var dish = new Dish
			{
				Id = 1,
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1,
				ImageUrl = "test.jpg",
				Price = 10

			};
			dbContext.Dishes.Add(dish);

			var userId = Guid.NewGuid().ToString();
			var dishId = 1;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			dbContext.ShoppingCarts.Add(new ShoppingCart { UserId = Guid.Parse(userId) });
			dbContext.CartDetails.Add(new CartDetail { ShoppingCartId = 1, DishId = dishId, Quantity = 1, UnitPrice = 10 });

			await dbContext.SaveChangesAsync();

			await shoppingCartService.RemoveItem(dishId);

			var cart = await dbContext.ShoppingCarts.Include(c => c.CartDetails).FirstOrDefaultAsync(c => c.UserId == Guid.Parse(userId));

			Assert.NotNull(cart);
			Assert.That(cart.CartDetails.Count, Is.EqualTo(0));
		}

		[Test]
		public async Task RemoveItem_ShouldDecreaseQuantity_WhenCartItemExists()
		{
			var dish = new Dish
			{
				Id = 1,
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1,
				ImageUrl = "test.jpg",
				Price = 10

			};
			dbContext.Dishes.Add(dish);

			var userId = Guid.NewGuid().ToString();
			var dishId = 1;

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			dbContext.ShoppingCarts.Add(new ShoppingCart { UserId = Guid.Parse(userId) });
			dbContext.CartDetails.Add(new CartDetail { ShoppingCartId = 1, DishId = dishId, Quantity = 3, UnitPrice = 10 });

			await dbContext.SaveChangesAsync();

			await shoppingCartService.RemoveItem(dishId);

			var cartDetail = await dbContext.CartDetails.FirstOrDefaultAsync(cd => cd.DishId == dishId);

			Assert.NotNull(cartDetail);
			Assert.That(cartDetail.Quantity, Is.EqualTo(2));
		}

		[Test]
		public void GetUserCart_ShouldThrowException_WhenUserIsNotLoggedIn()
		{
			var userId = string.Empty;

			MockHttpContextAccessor(userId);

			Assert.ThrowsAsync<ArgumentException>(async () => await shoppingCartService.GetUserCart());
		}

		[Test]
		public async Task GetUserCart_ShouldReturnCart_WhenUserIsLoggedIn()
		{
			var userId = Guid.NewGuid().ToString();

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			dbContext.ShoppingCarts.Add(new ShoppingCart { UserId = Guid.Parse(userId) });

			await dbContext.SaveChangesAsync();

			var cart = await shoppingCartService.GetUserCart();

			Assert.That(cart, Is.Not.Null);
			Assert.That(cart.UserId, Is.EqualTo(Guid.Parse(userId)));
		}

		[Test]
		public void DoCheckout_ShouldThrowException_WhenUserIsNotLoggedIn()
		{
			var userId = string.Empty;

			MockHttpContextAccessor(userId);

			Assert.ThrowsAsync<Exception>(async () => await shoppingCartService.DoCheckout(new OrderUsersInfoViewModel()));
		}

		[Test]
		public async Task DoCheckout_ShouldThrowException_WhenInvalidUser()
		{
			MockHttpContextAccessor(Guid.NewGuid().ToString());

			dbContext.Users.Add(new ApplicationUser { Id = Guid.NewGuid() });

			await dbContext.SaveChangesAsync();

			Assert.ThrowsAsync<ArgumentException>(async () => await shoppingCartService.DoCheckout(new OrderUsersInfoViewModel()));
		}

		[Test]
		public async Task DoCheckout_ShouldThrowException_WhenCartIsEmpty()
		{
			var userId = Guid.NewGuid().ToString();

			MockHttpContextAccessor(userId);

			dbContext.Users.Add(new ApplicationUser { Id = Guid.Parse(userId) });
			await dbContext.SaveChangesAsync();

			Assert.ThrowsAsync<Exception>(async () => await shoppingCartService.DoCheckout(new OrderUsersInfoViewModel()));
		}

		/// <summary>
		/// This method mocks the HttpContextAccessor and replace it with new. 
		/// If its not invoked, the HttpContextAccessor is awlays NULL in ShoppingCartService!
		/// </summary>
		/// <param name="userId"></param>
		private void MockHttpContextAccessor(string userId)
		{
			var claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) };
			var identity = new ClaimsIdentity(claims);
			var claimsPrincipal = new ClaimsPrincipal(identity);

			var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
			mockHttpContextAccessor.Setup(a => a.HttpContext.User).Returns(claimsPrincipal);

			httpContextAccessor = mockHttpContextAccessor.Object;

			shoppingCartService = new ShoppingCartService(dbContext, httpContextAccessor);
		}
	}
}
