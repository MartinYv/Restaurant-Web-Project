using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Menu;
using Restaurant2.Data;

namespace Restaurant.Services.Data
{
	public class MenuService : IMenuService
	{
		private readonly RestaurantDbContext context;

		public MenuService(RestaurantDbContext _context)
		{
			context = _context;
		}
		public async Task AddMenuAcync(AddMenuViewModel model)
		{
			Menu menu = new Menu()
			{
				MenuTypeId = model.MenuTypeId
			};

			await context.Menus.AddAsync(menu);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllMenusViewModel>> AllMenusAsync()
		{
			return await context.Menus.Select(m => 
			      new AllMenusViewModel()
				{
					MenuType = m.MenuType.Name
				}).ToListAsync();
		}

		public async Task<IEnumerable<MenuType>> GetAllMenuTypesAsync()
		{
			return await context.MenuTypes.ToListAsync();
		}
	}
}
