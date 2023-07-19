using Restaurant.Data.Models;
using Restaurant.ViewModels.Models.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Data.Interfaces
{
    public interface IDishTypeService
    {
        Task AddDishTypeAsync(AddDishTypeViewModel model);
        Task <IEnumerable<AllDishTypesViewModel>> AllDishTypesAsync();
        Task DeleteDishTypeAsync(int id);

        Task EditDishTypeById(AddDishTypeViewModel model, int id);
        Task <AddDishTypeViewModel?> GetDishTypeForEditById(int id);
	}
}
