using Restaurant.ViewModels.Models.Dish;
using Restaurant.ViewModels.Order.Enum;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Menu
{
    public class AllMenuDishesQueryViewModel
    {
        public AllMenuDishesQueryViewModel()
        {
            CurrentPage = 1;
            DishesPerPage = 3;
        }

        [Display(Name = "Sort Orders By")]
        public OrderSorting OrderSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Orders On Page")]
        public int DishesPerPage { get; set; }

        public int TotalDishes { get; set; }

        public int MenuId { get; set; }
        public IEnumerable<DishViewModel> Dishes { get; set; } = new HashSet<DishViewModel>();
    }
}
