using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Order;

using static Restaurant.Common.NotificationMessagesConstants;

namespace Restaurant.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartService cartService;

        public CartController(IShoppingCartService cartRepo)
        {
            cartService = cartRepo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddItem(int dishId, int qty = 1, int redirect = 0)
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                TempData[WarningMessage] = "First you have to log-in.";
				return RedirectToPage("/Account/Login", new { area = "Identity" }); // js is making problems, to fix that

			}
			try
            {
                /*var cartCount = */await cartService.AddItem(dishId, qty);
                TempData[SuccessMessage] = "Successfully added";
            return RedirectToAction("GetUserCart");

            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = ex.Message;
               return RedirectToAction("GetUserCart");
            }
            //if (redirect == 0)
            //{
            //    return Ok(cartCount);             // TO DO if redirect is equal to 0 to stay at the same page
            //}

        }

        public async Task<IActionResult> RemoveItem(int dishId)
        {
            try
            {
           await cartService.RemoveItem(dishId);
                return RedirectToAction("GetUserCart");


            }
            catch (Exception)
            {

                return RedirectToAction("GetUserCart");

            }

        }
        public async Task<IActionResult> GetUserCart()
        {

            try
            {
                var cart = await cartService.GetUserCart();
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
            int cartItem = await cartService.GetCartItemCount();

            return Ok(cartItem);
        }

        [Authorize]
        public async Task<IActionResult> Checkout(OrderUsersInfoViewModel usersInfo)
        {

            try
            {
                await cartService.DoCheckout(usersInfo);

                TempData[SuccessMessage] = "Your order is successfully recieved. We will call you when our deliveryman is at your address.";
                return RedirectToAction("MyOrders", "Order");
            }
            catch (Exception)
            {

                TempData[ErrorMessage] = "Something went wrong";
                throw new Exception("Something happen in server side");
            }               
        }

    }
}