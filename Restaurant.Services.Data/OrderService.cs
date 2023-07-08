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
         public async Task AddOrderAsync(AddOrderViewModel model, List<int> cartItems, Guid userId)
         {
             var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
             if (user != null)
             {
       
                        Order order = new Order
                 {
                     CustomerId = userId,
                     Customer = user,
                     OrderPlaced = DateTime.Now,
                     Phone = model.Phone,
                     Address = model.Address,
                     DishesOrdered = new List<CartItem>() 
                 };
       
                 context.Orders.Add(order);
                 await context.SaveChangesAsync();
       
                 var cartItemsFromDb = await context.CartItems
                     .Where(ci => cartItems.Contains(ci.CartItemId))
                     .ToListAsync();

                

                 order.DishesOrdered.AddRange(cartItemsFromDb);
       
                 await context.SaveChangesAsync();
             }
         }
 
        public async Task<IEnumerable<OrderViewModel>> AllOrdersAcync()
        {
            var model = await context.Orders.Select(o => new OrderViewModel()
            {
                CustomerId = o.CustomerId,
                Address = o.Address,
                Phone = o.Phone,
                TimePlaced = o.TimePlaced,
                IsCompleted = o.IsCompleted ? "False" : "True",
                Price = o.DishesOrdered.Sum(d=>d.Quantity * d.Dish.Price)
            }).ToListAsync();

            return model;
        }
    }
}
