﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Data.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MenuType")]
        public int MenuTypeId { get; set; }
        public MenuType MenuType { get; set; } = null!;

        public List<Dish> Dishes { get; set; } = new List<Dish>();
       // [ForeignKey("Dish")]
       // public int DishId { get; set; }
       // public Dish Dish { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}