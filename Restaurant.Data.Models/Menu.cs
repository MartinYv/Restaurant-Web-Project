using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Restaurant.Common.EntityValidationConstants.Menu;

namespace Restaurant.Data.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MenuUrlMaxLength)]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [ForeignKey("DishType")]
        public int DishTypeId { get; set; }
        public DishType DishType { get; set; } = null!;

        public List<Dish> Dishes { get; set; } = new List<Dish>();

        public bool IsDeleted { get; set; }
    }
}