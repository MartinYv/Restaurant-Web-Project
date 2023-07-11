using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Order;

namespace Restaurant.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartRepo;

        public CartController(IShoppingCartService cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int dishId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(dishId, qty);

            if (redirect == 0)
            {
                return Ok(cartCount);
            }

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int dishId)
        {
            var cartCount = await _cartRepo.RemoveItem(dishId);

            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();

            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();

            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout(OrderUsersInfoViewModel usersInfo)
        {
            bool isCheckedOut = await _cartRepo.DoCheckout(usersInfo);

            if (!isCheckedOut)
            {
                throw new Exception("Something happen in server side");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}