using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.Services.Data.Models.Order;
using Restaurant.ViewModels.Models.Order;

namespace Restaurant.Web.Controllers
{
    [Authorize]
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

		[HttpGet]
		public async Task<IActionResult> MyOrders([FromQuery] AllOrdersQueryViewModel queryModel)
		{
			try
			{
                AllOrdersFilteredServiceModel serviceModel =
                await orderService.UserOrdersAsync(queryModel);

                queryModel.Orders = serviceModel.Orders;
                queryModel.TotalOrders = serviceModel.TotalOrdersCount;


                return View("Mine", queryModel);
            }
			catch (Exception)
			{

				return RedirectToAction("Index", "Home");
			}

		}

        [Authorize(Roles = "Administrator")]
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
