﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Menu;
using Restaurant2.Data;
using System.Security.Cryptography.X509Certificates;
using Restaurant2.Models;

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
			bool isMenuExist = context.Menus.Where(m => m.IsDeleted == false).Any(mt => mt.DishType.Id == model.Id);

			if (isMenuExist == true)
			{
				throw new ArgumentException("Menu with that type is already added.");
			}

			Menu menu = new Menu()
			{
				DishTypeId	 = model.Id

			};

			await context.Menus.AddAsync(menu);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllMenusViewModel>> AllMenusAsync()
		{
			return await context.Menus.Where(m => m.IsDeleted == false).Select(m =>
				  new AllMenusViewModel()
				  {
					  Id = m.Id,
					  MenuType = m.DishType.Name
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

		public async Task<Menu?> GetMenuByName(string menuName)
		{
			return await context.Menus.Where(m => m.DishType.Name == menuName && m.IsDeleted == false).Include(x => x.DishType).Include(x => x.Dishes).FirstOrDefaultAsync();
		}

		public async Task AddDishAsync(Dish dish)
		{
			Menu? menu = await GetMenuByName(dish.DishType.Name);

			if (menu == null)
			{
				throw new ArgumentException("There isn't menu with that type of dishes. First add the menu.");
			}

			if (menu.Dishes.Where(d => d.IsDeleted == false).Any(d => d.Name == dish.Name))
			{
				throw new ArgumentException("Dish with that name is already added");
			}

			menu.Dishes.Add(dish);
			
		}

		
	}
}