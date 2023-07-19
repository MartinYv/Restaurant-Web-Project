using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;

namespace Restaurant.Web.Controllers
{
	public class DishTypeController : Controller
	{
		private readonly IDishTypeService dishTypeService;

		public DishTypeController(IDishTypeService _dishTypeService)
		{
			dishTypeService = _dishTypeService;
		}

		[HttpGet]
		public IActionResult Add()
		{
			var model = new AddDishTypeViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddDishTypeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				await dishTypeService.AddDishTypeAsync(model);
				return RedirectToAction(nameof(All));
			}
			catch (Exception ex)
			{

				throw new ArgumentException(ex.Message);
			}
			
		}

		public async Task<IActionResult> All()
		{
			var model = await dishTypeService.AllDishTypesAsync();

			return View(model);
		}

		public async Task<IActionResult> Delete(int typeId)
		{
			await dishTypeService.DeleteDishTypeAsync(typeId);
			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			AddDishTypeViewModel? model = await dishTypeService.GetDishTypeForEditById(id);

			if (model == null)
			{
				return RedirectToAction(nameof(All));
			}

			return View(model);
		}

	
		public async Task<IActionResult> Edit(AddDishTypeViewModel model, int id)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await dishTypeService.EditDishTypeById(model, id);

				return RedirectToAction(nameof(All));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}

		}

	}
}
