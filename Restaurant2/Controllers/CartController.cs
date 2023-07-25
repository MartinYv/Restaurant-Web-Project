using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Order;

using static Restaurant.Common.NotificationMessagesConstants;

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

            try
            {
                var cart = await _cartRepo.GetUserCart();
                return View(cart);

            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = ex.Message;
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
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
                TempData[ErrorMessage] = "Something went wrong";
                throw new Exception("Something happen in server side");
            }

            TempData[SuccessMessage] = "Your order is successfully recieved. We will call you when our deliveryman is at your address.";
            return RedirectToAction("Index", "Home");
        }

    }
}