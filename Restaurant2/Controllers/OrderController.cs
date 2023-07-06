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
        public IActionResult Add()
        {
            var model = new AddOrderViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderViewModel model, List<int> cartItems)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await orderService.AddOrderAsync(model, cartItems, userId);

            return RedirectToAction(nameof(All));
        }



    }
}
