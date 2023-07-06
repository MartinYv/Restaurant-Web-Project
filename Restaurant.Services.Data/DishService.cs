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

        public DishService(RestaurantDbContext _context)
        {
            context = _context;
        }


      
        public async Task Add(AddDishViewModel model)
        {
            Dish dish = new Dish()
            {
                Name = model.Name,
                Description = model.Description,
                DishTypeId = model.DishTypeId,
                ImageUrl = model.ImageUrl,
                Price = model.Price
            };

           // var menu = await context.Menus.FirstOrDefaultAsync(x => x.MenuType.Name == dish.DishType.Name);
           //
           // if (menu == null)
           // {
           //     throw new ArgumentException("Invalid menu or dishtype");
           // }
           //
			//menu.Dishes.Add(dish);
           //

			await context.Dishes.AddAsync(dish);
            await context.SaveChangesAsync();
        }

        public async Task AddDishToUserCollectionAsync(int id, Guid userId)
        {
            var dish = await context.Dishes.FindAsync(id);

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

           
        }

        public async Task<IEnumerable<AllDishesViewModel>> AllDishesAsync()
        {
            return await context.Dishes.Where(d=>d.IsDeleted == false)
                .Select(d => new AllDishesViewModel
            {
                Id= d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                Price = d.Price,
                DishType = d.DishType.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<DishType>> AllDishTypesAsync()
        {
            return await context.DishTypes.ToListAsync();
        }

        public async Task DeleteDishByIdAsync(int id)
        {
            Dish dish = await context.Dishes.Where(d => d.IsDeleted == false && d.Id == id).FirstOrDefaultAsync();

            dish.IsDeleted = true;
        }

        public async Task<Dish> GetDishById(int Id)
        {
            return await context.Dishes.FindAsync(Id);
        }
    }
}