using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;
using Restaurant2.Data;

namespace Restaurant.Services.Data
{
    public class DishTypeService : IDishTypeService
    {
        private readonly RestaurantDbContext context;

        public DishTypeService(RestaurantDbContext _context)
        {
            context = _context;
        }
        public async Task AddDishTypeAsync(AddDishTypeViewModel model)
        {
            DishType dishType = new DishType()
            {
                Name = model.Name
            };

            await context.DishTypes.AddAsync(dishType);
            await context.SaveChangesAsync();
        }

   

        public async Task<IEnumerable<AllDishTypesViewModel>> AllDishTypesAsync()
        {
            return await context.DishTypes
                .Select(dt=> new AllDishTypesViewModel()
                {
                    Id = dt.Id,
                    Name = dt.Name
                }).ToListAsync();
        }
    }
}
