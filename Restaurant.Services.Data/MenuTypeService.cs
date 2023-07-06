using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;
using Restaurant.ViewModels.Models.Menu;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Data
{
    public class MenuTypeService : IMenuTypeService
    {
        private readonly RestaurantDbContext context;

        public MenuTypeService(RestaurantDbContext _context)
        {
            context = _context;
        }
        public async Task AddMenuTypeAsync(AddMenuTypeViewModel model)
        {
            MenuType menuType = new MenuType()
            {
                Name = model.Name
            };

            await context.MenuTypes.AddAsync(menuType);
            await context.SaveChangesAsync();
        }



        public async Task<IEnumerable<AllMenuTypesViewModel>> AllMenuTypesAsync()
        {
            return await context.MenuTypes
                .Select(dt => new AllMenuTypesViewModel()
                {
                    Id = dt.Id,
                    Name = dt.Name
                }).ToListAsync();
        }
    }
}
