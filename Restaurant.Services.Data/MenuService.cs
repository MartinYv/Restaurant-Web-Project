using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Menu;
using Restaurant2.Data;
using System.Security.Cryptography.X509Certificates;

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
			bool isMenuExist = context.Menus.Where(m=>m.IsDeleted == false).Any(mt => mt.MenuType.Id == model.Id);

			if (isMenuExist == true)
			{
				throw new ArgumentException("Menu with that type is already added.");
			}

			Menu menu = new Menu()
			{
				MenuTypeId = model.Id
				
			};

			await context.Menus.AddAsync(menu);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllMenusViewModel>> AllMenusAsync()
		{
			return await context.Menus.Where(m=>m.IsDeleted == false).Select(m => 
			      new AllMenusViewModel()
				{
					  Id = m.Id,
					MenuType = m.MenuType.Name
				}).ToListAsync();
		}


        public async Task<IEnumerable<MenuType>> GetAllMenuTypesAsync()
		{
			return await context.MenuTypes.Where(m => m.IsDeleted == false).ToListAsync();
		}
        public async Task DeleteMenuAsync(int menuId)
        {
			var menu = await context.Menus.Where(m => m.IsDeleted == false).FirstOrDefaultAsync(m => m.Id == menuId);

			if (menu != null)
			{
				menu.IsDeleted = true;
				await context.SaveChangesAsync();
			}
        }
	}
}
