using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Data
{
    public class ShoppingCartService
    {
        private const string CartSessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RestaurantDbContext _context;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, RestaurantDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public async Task <Cart> GetCart()
        {
            Cart cart = _session.GetObjectFromJson<Cart>(CartSessionKey);
            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    CartItems = new List<CartItem>()
                };
            await _context.Carts.AddAsync(cart);
                _session.SetObjectAsJson(CartSessionKey, cart);
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task AddToCart(Dish dish)
        {
            Cart cart = await GetCart();
            CartItem existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.DishId == dish.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                CartItem newCartItem = new CartItem
                {
                    Cart = cart,
                    CartId= cart.CartId,

                    DishId = dish.Id,
                    Dish = dish,
                    Quantity = 1
                };
                cart.CartItems.Add(newCartItem);
            }

           await _context.SaveChangesAsync();
            _session.SetObjectAsJson(CartSessionKey, cart);
        }


        public async Task RemoveFromCart(int cartItemId)
        {
            Cart cart = await GetCart();
            CartItem cartItemToRemove = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (cartItemToRemove != null)
            {
                cart.CartItems.Remove(cartItemToRemove);
                await _context.SaveChangesAsync();
                _session.SetObjectAsJson(CartSessionKey, cart);
            }
        }

    }
}
