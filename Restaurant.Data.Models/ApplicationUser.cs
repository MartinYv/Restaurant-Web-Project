﻿using Microsoft.AspNetCore.Identity;

namespace Restaurant.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

       public HashSet<Order> OrdersPlaced { get; set; } = new HashSet<Order>();
    }
}