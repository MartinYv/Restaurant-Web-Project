using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.ViewModels.Models.Table;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;

namespace Restaurant.Web.Controllers
{
	public class TableController : Controller
	{
		private readonly ITableService tableService;

        public TableController(ITableService _tableService)
        {
			tableService = _tableService;
        }

        [HttpGet]
		public IActionResult Add()
		{
			var model = new AddTableViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddTableViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				await tableService.AddTableAsync(model);

				return RedirectToAction(nameof(All));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(All));
			}
			
		}

		public async Task<IActionResult> All()
		{
			var model = await tableService.AllTablesAsync();
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await tableService.DeleteTableById(id);
			return RedirectToAction(nameof(All));
		}
	}
}
