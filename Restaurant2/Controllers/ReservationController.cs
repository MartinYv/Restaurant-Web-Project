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
		public IActionResult Add()
		{
			var model = new AddReservationViewModel();
			return View(model);
		}

		public async Task<IActionResult> Add(AddReservationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await reservationService.AddReservationAsync(model);
			return RedirectToAction(nameof(All));
		}

		public IActionResult All()
		{
			return RedirectToAction(nameof(Add)); // to change it! 
		}

	}
}
