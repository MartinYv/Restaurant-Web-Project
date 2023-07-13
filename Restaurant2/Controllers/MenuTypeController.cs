using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Menu;

namespace Restaurant.Web.Controllers
{
    public class MenuTypeController : Controller
    {
        private readonly IMenuTypeService menuTypeService;

        public MenuTypeController(IMenuTypeService _menuTypeService)
        {
            menuTypeService = _menuTypeService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddMenuTypeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMenuTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await menuTypeService.AddMenuTypeAsync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var model = await menuTypeService.AllMenuTypesAsync();

            return View(model);
        }

        public async Task<IActionResult> Delete(int typeId)
        {
            try
            {
                await menuTypeService.DeleteMenuTypeAsync(typeId);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid menu type id!");
                return RedirectToAction(nameof(All));
            }

        }
    }
}
