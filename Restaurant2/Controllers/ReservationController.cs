using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Reservation;

namespace Restaurant.Web.Controllers
{
	public class ReservationController : Controller
	{
		private readonly IReservationService reservationService;

		public ReservationController(IReservationService _reservationService)
		{
			reservationService = _reservationService;
		}

		[HttpGet]
		public IActionResult Add()
		{
			var model = new AddReservationInfoViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddReservationInfoViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await reservationService.AddReservationAsync(model);

			

			return RedirectToAction(nameof(All));
		}

		public async Task<IActionResult> All()
		{
			var model = await reservationService.AllReservationsAsync();
			return View(model);
		}

	}
}
