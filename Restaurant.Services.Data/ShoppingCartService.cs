﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Order;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Restaurant.Services.Data.ShoppingCartService;

namespace Restaurant.Services.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly RestaurantDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingCartService(RestaurantDbContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            context = _context;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<int> AddItem(int dishId, int qty)
        {
            string? userId = GetUserId();

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = Guid.Parse(userId)
                    };

                    context.ShoppingCarts.Add(cart);
                }
                await context.SaveChangesAsync();


                var cartItem = await context.CartDetails
                                  .FirstOrDefaultAsync(a => a.ShoppingCartId == cart.Id && a.DishId == dishId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var dish = await context.Dishes.FindAsync(dishId);
                    cartItem = new CartDetail
                    {
                        Dish = dish,
                        DishId = dishId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = (int)dish.Price   // TO DO UPDATE FROM INT TO DECIMAL-> UNITPRICE
                    };
                   await context.CartDetails.AddAsync(cartItem);
                }

              await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }


        public async Task<int> RemoveItem(int dishId)
        {
            //using var transaction = _db.Database.BeginTransaction();   ???
            string? userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    throw new Exception("Invalid cart");
                }

               
                var cartItem = context.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.DishId == dishId);



                if (cartItem == null)
                {
                    throw new Exception("Not items in cart");

                }
                else if (cartItem.Quantity == 1)
                {
                    context.CartDetails.Remove(cartItem);

                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            Guid? userId = Guid.Parse(GetUserId());

            if (userId == null)
            {
                throw new Exception("Invalid userid");
            }

            var shoppingCart = await context.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Dish)
                                  .ThenInclude(a => a.DishType)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));
            return cart;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in context.ShoppingCarts
                              join cartDetail in context.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }

        public async Task<bool> DoCheckout(OrderUsersInfoViewModel usersInfo)
        {



            using var transaction =await context.Database.BeginTransactionAsync();
            try
            {
                
                var userId = GetUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged-in");
                }


                var cart = await GetCart(userId);

                if (cart is null)
                {
                    throw new Exception("Invalid cart");
                }

                var cartDetail = await context.CartDetails
                                    .Where(a => a.ShoppingCartId == cart.Id).Include(a=>a.Dish).ToListAsync();

                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");

                var order = new Order
                {
                    FirstName = usersInfo.FirstName,
                    LastName = usersInfo.LastName,
                    Address = usersInfo.Address,
                    Phone = usersInfo.Phone,
                    CustomerId = Guid.Parse(userId),
                    CreateDate = DateTime.UtcNow,
                    IsCompleted = false,
                    IsDeleted = false
                    //OrderStatusId = 1//pending
                };

               await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                foreach (var item in cartDetail)
                {

					var orderDetail = new OrderDetail
                    {
                        Dish = item.Dish,
                        DishId = item.DishId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    await context.OrderDetails.AddAsync(orderDetail);
                }

                order.Price = (decimal)order.OrderDetail.Sum(x => x.Quantity * x.UnitPrice);
               await context.SaveChangesAsync();

             
                context.CartDetails.RemoveRange(cartDetail);

                var user = await context.Users.FindAsync(Guid.Parse(userId));
                user.OrdersPlaced.Add(order);

               await context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private string? GetUserId()
        {
            string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }
    }
}