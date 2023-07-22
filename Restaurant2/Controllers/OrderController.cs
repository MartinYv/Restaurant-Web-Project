using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.Services.Data.Models.Order;
using Restaurant.ViewModels.Models.Order;
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


		public async Task<IActionResult> All()
		{
			var model = await orderService.AllOrdersAcync();
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


		public async Task<IActionResult> MyOrders()
		{
			try
			{
				var model =await orderService.UsersOrdersAsync();
				return View("Mine", model);
			}
			catch (Exception)
			{

				return RedirectToAction("Index", "Home");
			}

		}


		[HttpGet]
		
		public async Task<IActionResult> AllFiltered([FromQuery] AllOrdersQueryViewModel queryModel)
		{
			AllOrdersFilteredServiceModel serviceModel =
				await orderService.AllFilteredAsync(queryModel);

			queryModel.Orders = serviceModel.Orders;
			queryModel.TotalOrders = serviceModel.TotalOrdersCount;
			//queryModel.Categories = await categoryService.AllCategoryNamesAsync();

			return View("AllSorted",queryModel);
		}
	}
}
