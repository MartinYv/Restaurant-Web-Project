using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;
using Restaurant2.Data;
using System.Security.Cryptography.X509Certificates;

namespace Restaurant.Services.Data
{

	public class DishService : IDishService
	{
		private readonly RestaurantDbContext context;
		private readonly IMenuService menuService;


		public DishService(RestaurantDbContext _context, IMenuService menuService)
		{
			context = _context;
			this.menuService = menuService;
		}



		public async Task Add(AddDishViewModel model)
		{
			var dishTypeName = await context.DishTypes.Where(dt => dt.IsDeleted == false && dt.Id == model.DishTypeId).FirstOrDefaultAsync();

			Dish dish = new Dish()
			{
				Name = model.Name,
				Description = model.Description,
				DishTypeId = model.DishTypeId,
				DishType = dishTypeName,
				ImageUrl = model.ImageUrl,
				Price = model.Price,
				IsDeleted = false
			};
	

			await menuService.AddDishAsync(dish);		
			await context.Dishes.AddAsync(dish);		
			
			await context.SaveChangesAsync();
		}


		public async Task<IEnumerable<AllDishesViewModel>> AllDishesAsync()
		{
			return await context.Dishes.Where(d => d.IsDeleted == false)
				.Select(d => new AllDishesViewModel
				{
					Id = d.Id,
					Name = d.Name,
					Description = d.Description,
					ImageUrl = d.ImageUrl,
					Price = d.Price,
					DishType = d.DishType.Name
				}).ToListAsync();
		}

		public async Task<IEnumerable<DishType>> AllDishTypesAsync()
		{
			return await context.DishTypes.Where(dt=>dt.IsDeleted == false).ToListAsync();
		}

		public async Task DeleteDishByIdAsync(int id)
		{
			Dish dish = await context.Dishes.Where(d => d.IsDeleted == false && d.Id == id).FirstOrDefaultAsync();

			if (dish != null)
			{
				dish.IsDeleted = true;
				await context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentException("Invalid dish Id.");
			}

		}

		public async Task<Dish?> GetDishById(int Id)
		{
			return await context.Dishes.FindAsync(Id);
		}
	}
}