using Restaurant.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Menu
{

    public class AddMenuViewModel
    {
		public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;
        public IEnumerable<DishType> MenuTypes { get; set; } = new List<DishType>();

	}
}
