using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Order
{
    using static Restaurant.Common.EntityValidationConstants.Order;
    public class OrderUsersInfoViewModel
    {

        // public Guid? CustomerId { get; set; }
        // public ApplicationUser? Customer { get; set; } = null!;
        //
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneMaxLength, MinimumLength = PhoneMinLength)]
        public string Phone { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = PhoneMinLength)]
        public string Address { get; set; } = null!;

    //  [Required]
    //  [Precision(18, 2)]
    //  public decimal Price { get; set; }
    //
    //  [Required]
    //  public DateTime TimePlaced { get; set; }
    //
    //
    //  public DateTime? TimeCompleted { get; set; }
    //
    //  [Required]
    //  public bool IsCompleted { get; set; }
    //
      public List<CartDetail> DishesOrdered { get; set; } = new List<CartDetail>();
    }
}
