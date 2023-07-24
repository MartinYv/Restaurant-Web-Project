using Restaurant.Data.Models;
using System.ComponentModel.DataAnnotations;

using static Restaurant.Common.EntityValidationConstants.Menu;

namespace Restaurant.ViewModels.Models.Menu
{
    public class AddMenuViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MenuUrlMaxLength, MinimumLength = MenuUrlMinLength)]
        public string ImageUrl { get; set; } = null!;
        public IEnumerable<DishType> MenuTypes { get; set; } = new List<DishType>();
    }
}
