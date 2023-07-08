﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Restaurant.Common.EntityValidationConstants.Dish;

namespace Restaurant.Data.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DishNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DishDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(DishUrlMaxLength)]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(18,2)]
        [Range(DishPriceMinLength, DishPriceMaxLength)]
        public decimal Price { get; set; }


        [ForeignKey("DishType")]
        public int DishTypeId { get; set; }
        public DishType DishType { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}