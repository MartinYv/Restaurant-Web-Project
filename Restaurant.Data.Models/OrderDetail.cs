﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Data.Models
{

    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish Dish { get; set; } = null!;


        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}

