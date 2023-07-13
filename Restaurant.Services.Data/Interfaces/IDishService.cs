using Restaurant.Data.Models;
using Restaurant.ViewModels.Models.Dish;

namespace Restaurant.Services.Data.Interfaces
{
    public interface IDishService
    {
        Task Add(AddDishViewModel model);
        Task<IEnumerable<DishType>> AllDishTypesAsync();
        Task<IEnumerable<AllDishesViewModel>> AllDishesAsync();
        Task<Dish?> GetDishById(int id);
        Task DeleteDishByIdAsync(int id);
    }
}
