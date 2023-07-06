using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Menu;


namespace Restaurant.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService _menuService)
        {
            menuService = _menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddMenuViewModel model = new AddMenuViewModel()
            {
                MenuTypes = await menuService.GetAllMenuTypesAsync()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddMenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           var menuTypes= await menuService.GetAllMenuTypesAsync();

          // if (menuTypes.Any(x=>x.Id == model.MenuTypeId))
          // {
          //     ModelState.AddModelError("", "Menu with the same type is already added."); // its not writen on the screen
          //    return RedirectToAction(nameof(Add));
          // }
           await  menuService.AddMenuAcync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var model = await menuService.AllMenusAsync();
            return View(model);
        }

      //  public async Task<IActionResult> AddOrderToUser()
      //  {
      //
      //  }
    }
}
