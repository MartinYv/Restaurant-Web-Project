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
            var model = await context.Orders.Where(o=>o.IsDeleted == false).Include(o => o.OrderDetail).ThenInclude(o=>o.Dish).Select(o => new OrderViewModel()
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                Address = o.Address,                                          // TO FIX THE INCULDES, DISH IS ALWAYS NULL!!!!!!
                Phone = o.Phone,
                Price = o.Price.ToString(),
                CustomerId = o.CustomerId,
                CreateDate = o.CreateDate.ToString("MM/dd/yy H:mm:ss"),
                IsCompleted = o.IsCompleted ? "Delivered" : "Pending",
                OrderDetail = o.OrderDetail
            }).ToListAsync();

            return model;
        }
    }
}
