using Restaurant.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Order
{
    using static Restaurant.Common.EntityValidationConstants.Order;
    public class OrderUsersInfoViewModel
    {

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(PhoneMaxLength, MinimumLength = PhoneMinLength)]
        public string Phone { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = PhoneMinLength)]
        public string Address { get; set; } = null!;
        public List<CartDetail> DishesOrdered { get; set; } = new List<CartDetail>();
    }
}
