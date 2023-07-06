using Microsoft.AspNetCore.Mvc;
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

            await dishTypeService.AddDishTypeAsync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var model = await dishTypeService.AllDishTypesAsync();

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await dishTypeService.DeleteDishTypeAsync(id);
            return RedirectToAction(nameof(All));
        }
    }
}
