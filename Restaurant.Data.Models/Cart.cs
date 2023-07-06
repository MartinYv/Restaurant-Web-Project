using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; } = null!;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }


}
