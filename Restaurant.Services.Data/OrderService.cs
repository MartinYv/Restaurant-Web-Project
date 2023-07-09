using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Order;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Restaurant.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext context;

        public OrderService(RestaurantDbContext _context)
        {
            context = _context;
        }
        
        public async Task<IEnumerable<OrderViewModel>> AllOrdersAcync()
        {
            var model = await context.Orders.Select(o => new OrderViewModel()
            {
                CustomerId = o.CustomerId,
               
                
                IsCompleted = o.IsCompleted ? "False" : "True",
            }).ToListAsync();

            return model;
        }
    }
}
