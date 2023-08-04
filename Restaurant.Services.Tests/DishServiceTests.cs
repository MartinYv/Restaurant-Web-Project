namespace Restaurant.Tests.ServiceTests
{
	using Microsoft.EntityFrameworkCore;
	using Moq;

	using Restaurant2.Data;
	using Restaurant.Services.Data;
	using Restaurant.Services.Data.Interfaces;
	using Restaurant.ViewModels.Models.Dish;
	using Restaurant.Data.Models;

	[TestFixture]
	public class DishServiceTests
	{
		private RestaurantDbContext dbContext;
		private Mock<IMenuService> menuServiceMock;
		private Mock<IDishTypeService> dishTypeServiceMock;
		private IDishService dishService;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<RestaurantDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			dbContext = new RestaurantDbContext(options);

			menuServiceMock = new Mock<IMenuService>();
			dishTypeServiceMock = new Mock<IDishTypeService>();

			dishService = new DishService(dbContext, menuServiceMock.Object);
			 MenuService menuService = new MenuService(dbContext);
		}

		[Test]
		public async Task Add_ShouldAddDish()
		{
			DishType dishType = new DishType() { Id = 1, Name = "Test type" };
			dishTypeServiceMock

			var dishTypeName = "Test Dish Type";
			var model = new AddDishViewModel
			{
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1,
				ImageUrl = "test.jpg",
				Price = 10.33m
			};

			menuServiceMock.Setup(ms => ms.AddDishAsync(It.IsAny<Dish>()));

			// Act
			await dishService.Add(model);

			// Assert
			var addedDish = await dbContext.Dishes.FirstOrDefaultAsync(d => d.Name == "Test Dish");
			Assert.NotNull(addedDish);
			Assert.AreEqual(dishTypeName, addedDish.DishType.Name);
		}

		[Test]
		public async Task Add_ShouldThrowArgumentException_WhenDishTypeNotFound()
		{
			// Arrange
			var model = new AddDishViewModel
			{
				Name = "Test Dish",
				Description = "Test Description",
				DishTypeId = 1, // DishType with this ID doesn't exist
				ImageUrl = "test.jpg",
				Price = 10.99
			};

			// Act & Assert
			Assert.ThrowsAsync<ArgumentException>(async () => await dishService.Add(model));
		}

		[Test]
		public async Task AllDishesAsync_ShouldReturnAllDishes()
		{
			// ... (Same as before)

			// Additional Arrange
			var dishType = new DishType { Name = "Another Type" };
			var dish3 = new Dish { Name = "Dish 3", DishType = dishType };
			await dbContext.Dishes.AddAsync(dish3);
			await dbContext.SaveChangesAsync();

			// Act
			var result = await dishService.AllDishesAsync();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(3));
			CollectionAssert.AreEquivalent(new[] { dish1.Name, dish2.Name, dish3.Name }, result.Select(d => d.Name));
		}

		[Test]
		public async Task DeleteDishByIdAsync_ShouldDeleteDish()
		{
			// ... (Same as before)

			// Additional Arrange
			var deletedDish = new Dish { Name = "Deleted Dish" };
			await dbContext.Dishes.AddAsync(deletedDish);
			await dbContext.SaveChangesAsync();

			// Act
			await dishService.DeleteDishByIdAsync(deletedDish.Id);

			// Assert
			var deletedDishEntity = await dbContext.Dishes.FindAsync(deletedDish.Id);
			Assert.True(deletedDishEntity.IsDeleted);
		}

		[Test]
		public async Task DeleteDishByIdAsync_ShouldThrowArgumentException_WhenInvalidDishId()
		{
			// Arrange
			var invalidDishId = -1;

			// Act & Assert
			Assert.ThrowsAsync<ArgumentException>(async () => await dishService.DeleteDishByIdAsync(invalidDishId));
		}

		// ... (Other test methods, similar to before)

		[TearDown]
		public void CleanUp()
		{
			dbContext.Database.EnsureDeleted();
			dbContext.Dispose();
		}
	}
}