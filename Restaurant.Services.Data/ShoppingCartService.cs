using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.Services.Data
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly RestaurantDbContext _context;
        public ShoppingCartService(RestaurantDbContext context)
        {
            _context = context;
        }

        public void AddToCart(string cartId, int dishId, int quantity, int? orderId)
        {
            var cart = _context.Carts.Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Dish)
                                     .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = cartId
                };

                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.DishId == dishId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    DishId = dishId,
                    Quantity = quantity,
                    OrderId = orderId  // Set the OrderId for the CartItem
                };

                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }


        public void RemoveFromCart(string cartId, int cartItemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.CartItemId == cartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void UpdateCartItemQuantity(string cartId, int cartItemId, int quantity)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.CartItemId == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
        }

        public void ClearCart(string cartId)
        {
            var cart = _context.Carts.Include(c => c.CartItems)
                                     .FirstOrDefault(c => c.CartId == cartId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }

        public Cart GetCart(string cartId)
        {
            return _context.Carts.Include(c => c.CartItems)
                                 .ThenInclude(ci => ci.Dish)
                                 .FirstOrDefault(c => c.CartId == cartId);
        }
    }
}