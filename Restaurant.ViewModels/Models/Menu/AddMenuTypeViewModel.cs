using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Menu
{
    using static Restaurant.Common.EntityValidationConstants.MenuType;

    public class AddMenuTypeViewModel
    {
        [Required]
        [StringLength(MenuTypeMaxLenght, MinimumLength = MenuTypeMinLenght)]
        public string Name { get; set; } = null!;
    }
}
