using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data.Models
{
    using static Restaurant.Common.EntityValidationConstants.MenuType;
    public class MenuType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MenuTypeMaxLenght)]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }

    }
}
