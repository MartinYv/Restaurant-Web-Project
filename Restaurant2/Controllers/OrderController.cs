using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Order;
using System.Security.Claims;

namespace Restaurant.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }


        public IActionResult All()
        {
            var model = orderService.AllOrdersAcync();
            return View(model);
        }


        [HttpGet]
        public IActionResult FinishOrder()
        {
            OrderUsersInfoViewModel usersInfo = new OrderUsersInfoViewModel();
            return View(usersInfo);
        }

        [HttpPost]
        public IActionResult FinishOrder(OrderUsersInfoViewModel usersInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(usersInfo);
            }

           

            return RedirectToAction("CheckOut", "Cart", usersInfo);
        }

    }
}
