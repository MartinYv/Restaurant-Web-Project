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



       
       
    }
}
