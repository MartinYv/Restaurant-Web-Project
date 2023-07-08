using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant2.Data;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

    public IActionResult AddToCart(int dishId, int quantity)
    {
        string cartId = GetOrCreateCartId();

        // Retrieve or create the order
        int orderId = GetOrCreateOrder();

        _cartService.AddToCart(cartId, dishId, quantity, orderId);
        return RedirectToAction("Cart", new { cartId });
    }

    public IActionResult RemoveFromCart(int cartItemId)
        {
            string cartId = GetOrCreateCartId();
            _cartService.RemoveFromCart(cartId, cartItemId);
            return RedirectToAction("Cart", new { cartId });
        }

        public IActionResult UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            string cartId = GetOrCreateCartId();
            _cartService.UpdateCartItemQuantity(cartId, cartItemId, quantity);
            return RedirectToAction("Cart", new { cartId });
        }

        public IActionResult ClearCart()
        {
            string cartId = GetOrCreateCartId();
            _cartService.ClearCart(cartId);
            return RedirectToAction("Cart", new { cartId });
        }

        public IActionResult Cart()
        {
            string cartId = GetOrCreateCartId();
            var cart = _cartService.GetCart(cartId);

            if (cart == null)
            {
                // Handle cart not found
            }

            return View(cart);
        }

        private string GetOrCreateCartId()
        {
            // Retrieve the cartId from the user session
            var cartId = HttpContext.Session.GetString("CartId");

            // Generate a new cartId if it doesn't exist
            if (string.IsNullOrEmpty(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartId", cartId);
            }

            return cartId;
        }

    private int GetOrCreateOrder()
    {
        // Retrieve the orderId from the user session or create a new order
        int orderId = 0;
        // Retrieve orderId from session or create a new order using the OrderService
        // For example:
        // orderId = _orderService.GetOrCreateOrder();

        // You can modify this logic to match your specific requirements

        return orderId;
    }
}
