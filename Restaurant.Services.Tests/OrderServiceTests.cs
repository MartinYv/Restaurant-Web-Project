namespace Restaurant.Tests.ServiceTests
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using System.Security.Claims;

	using Restaurant2.Data;
	using Restaurant.Data.Models;
	using Restaurant.Services.Data;
	using Restaurant.Services.Data.Interfaces;
	using Restaurant.ViewModels.Models.Order;
	using Restaurant.ViewModels.Order.Enum;
	
	[TestFixture]
	public class OrderServiceTests
	{
		private RestaurantDbContext dbContext;
		private IHttpContextAccessor httpContextAccessor;
		private IOrderService orderService;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RestaurantDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			dbContext = new RestaurantDbContext(options);

			httpContextAccessor = new HttpContextAccessor();
			var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) };
			var identity = new ClaimsIdentity(claims, "TestAuthType");
			var claimsPrincipal = new ClaimsPrincipal(identity);
			var httpContext = new DefaultHttpContext { User = claimsPrincipal };
			httpContextAccessor.HttpContext = httpContext;

			orderService = new OrderService(dbContext, httpContextAccessor);
		}

		[Test]
		public async Task AllOrdersAsync_ShouldReturnAllOrders()
		{
			ApplicationUser user1 = new ApplicationUser() { Id = new Guid(), Email = "Test@abv.bg" };
			ApplicationUser user2 = new ApplicationUser() { Id = new Guid(), Email = "Test2@abv.bg" };

			await dbContext.Users.AddRangeAsync(user1, user2);

			var order1 = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer = user1 };
			var order2 = new Order { FirstName = "Connor", LastName = "MCcregor", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer =user2  };
			
			await dbContext.Orders.AddRangeAsync(order1, order2);
			await dbContext.SaveChangesAsync();

			var result = await orderService.AllOrdersAcync();

			Assert.That(result.Count(), Is.EqualTo(2));
			CollectionAssert.AreEquivalent(new[] { order1.FirstName, order2.FirstName }, result.Select(o => o.FirstName));
		}

		[Test]
		public async Task UserOrdersAsync_ShouldReturnUserOrders()
		{	
			var user = new ApplicationUser {  Email = "test@abv.bg" };
			await dbContext.Users.AddAsync(user);

			var order1 = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer = user };
			var order2 = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 20, Customer = user };

			user.OrdersPlaced.Add(order1);
			user.OrdersPlaced.Add(order2);
			await dbContext.Orders.AddRangeAsync(order1, order2);
			await dbContext.SaveChangesAsync();

			var queryModel = new AllOrdersQueryViewModel { OrderSorting = OrderSorting.Newest, CurrentPage = 1, OrdersPerPage = 3 };
			var result = await orderService.UserOrdersAsync(queryModel);

			Assert.That(result.TotalOrdersCount, Is.EqualTo(2));
			CollectionAssert.AreEquivalent(new[] { order1.FirstName, order2.FirstName }, result.Orders.Select(o => o.FirstName));
		}

		[Test]
		public void UserOrdersAsync_ShouldThrowArgumentException_WhenInvalidUserId() // not working right, shows me different user's id's
		{
			// Arrange
			var queryModel = new AllOrdersQueryViewModel { OrderSorting = OrderSorting.Newest, CurrentPage = 1, OrdersPerPage = 10 };

			// Act & Assert
			Assert.ThrowsAsync<ArgumentException>(async () => await orderService.UserOrdersAsync(queryModel));
		}

		[Test]
		public async Task AllFilteredAsync_ShouldReturnFilteredOrders()
		{
			var order1 = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer = new ApplicationUser() };
			var order2 = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 20, Customer =  new ApplicationUser() };
			
			await dbContext.Orders.AddRangeAsync(order1, order2);
			await dbContext.SaveChangesAsync();

			var queryModel = new AllOrdersQueryViewModel { OrderSorting = OrderSorting.Newest, CurrentPage = 1, OrdersPerPage = 10 };
			var result = await orderService.AllFilteredAsync(queryModel);

			Assert.That(result.TotalOrdersCount, Is.EqualTo(2));
			CollectionAssert.AreEquivalent(new[] { order1.FirstName, order2.FirstName }, result.Orders.Select(o => o.FirstName));
		}

		[Test]
		public async Task FindOrderByIdAsync_ShouldReturnOrder()
		{
			var order = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer = new ApplicationUser() };
			
			await dbContext.Orders.AddAsync(order);
			await dbContext.SaveChangesAsync();

			var result = await orderService.FindOrderByIdAsync(order.Id);

			Assert.NotNull(result);
			Assert.That(result.FirstName, Is.EqualTo(order.FirstName));
			Assert.That(result.Id, Is.EqualTo(order.Id));
		}

		[Test]
		public async Task ChangeStatusByIdAsync_ShouldChangeOrderStatus()
		{
			var order = new Order { FirstName = "Jo", LastName = "Masvidal", Address = "Test Address", IsCompleted = false, Phone = "0888130130", CreateDate = DateTime.Now, Price = 10, Customer = new ApplicationUser() };
			
			await dbContext.Orders.AddAsync(order);
			await dbContext.SaveChangesAsync();

			await orderService.ChangeStatusByIdAsync(order.Id);

			var updatedOrder = await dbContext.Orders.FindAsync(order.Id);
			Assert.That(order.IsCompleted, Is.Not.EqualTo(updatedOrder!.IsCompleted));
		}


		[TearDown]
		public void CleanUp()
		{
			dbContext.Database.EnsureDeleted();
			dbContext.Dispose();
		}
	}
}