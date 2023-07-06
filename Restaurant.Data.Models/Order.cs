using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Restaurant.Common.EntityValidationConstants.Order;

namespace Restaurant.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string Phone { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
                 
        public DateTime OrderPlaced { get; set; }

        [Required]
        public DateTime TimePlaced { get; set; }
        public DateTime? TimeCompleted { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;

        public List<CartItem> DishesOrdered { get; set; } = new List<CartItem>();

        public bool IsDeleted { get; set; }
    }
}
