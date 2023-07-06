using Restaurant.ViewModels.Models.Dish;
using Restaurant.ViewModels.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Data.Interfaces
{
    public interface IMenuTypeService
    {
        Task AddMenuTypeAsync(AddMenuTypeViewModel model);
        Task<IEnumerable<AllMenuTypesViewModel>> AllMenuTypesAsync();
    }
}
