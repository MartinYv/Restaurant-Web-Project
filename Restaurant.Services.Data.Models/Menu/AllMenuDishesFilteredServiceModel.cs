using Restaurant.ViewModels.Models.Dish;

namespace Restaurant.Services.Data.Models.Menu
{
    public class AllMenuDishesFilteredServiceModel
    {
        public int TotalDishesCount { get; set; }
        public IEnumerable<DishViewModel> Dishes { get; set; } = new List<DishViewModel>();
    }
}
