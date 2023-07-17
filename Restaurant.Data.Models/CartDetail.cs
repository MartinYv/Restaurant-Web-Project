using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models
{

    public class CartDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cart")] 
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;


        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; } = null!;


        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}
