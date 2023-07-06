using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Dish
{
    using static Restaurant.Common.EntityValidationConstants.DishType;
    public class AddDishTypeViewModel
    {
        [Required]
        [StringLength(DishTypeMaxLenght, MinimumLength = DishTypeMinLength)]
        public string Name { get; set; } = null!;
    }
}
