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
    public class OrderViewModel
    {
        public Guid CustomerId { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime TimePlaced { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public string IsCompleted { get; set; } = null!;
        public List<Dish> DishesOrdered { get; set; } = new List<Dish>();
    }
}
