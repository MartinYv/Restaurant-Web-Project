﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> AddItem(int dishId, int quantity = 1, int redirect = 0)
        {
				if (!User?.Identity?.IsAuthenticated ?? false)
				{
					TempData[WarningMessage] = "First you have to log-in.";
				return RedirectToAction("Login", "Account", new { area = "Identity" });
			}
			try
			{

				await cartService.AddItem(dishId, quantity);

				if (redirect == 1)
				{
					return RedirectToAction("GetUserCart", "Cart");
				}

				return Json(new { success = true });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}


		}

		public async Task<IActionResult> RemoveItem(int dishId)
        {
            try
            {
                await cartService.RemoveItem(dishId);
				//TempData[SuccessMessage] = "Successfully removed";

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

        [AllowAnonymous]
		[HttpGet]
		public IActionResult IsAuthenticated()
		{
            if (!User?.Identity?.IsAuthenticated ?? false)
            {
			return Json(new { isAuthenticated = false });
            }
            else
            {
				return Json(new { isAuthenticated = true });

			}
		}
	}
}