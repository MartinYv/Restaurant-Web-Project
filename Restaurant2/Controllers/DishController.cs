using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Restaurant.Web.Controllers
{
    public class DishController : Controller
    {
        private readonly IDishService dishService;

        public DishController(IDishService _dishService)
        {
            dishService = _dishService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddDishViewModel model = new AddDishViewModel()
            {
                DishTypes = await dishService.AllDishTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDishViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await dishService.Add(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return RedirectToAction(nameof(All));// to do 
            }

        }

        public async Task<IActionResult> All()
        {
            var model = await dishService.AllDishesAsync();
            return View(model);
        }

      
        public async Task<IActionResult> Delete(int id)
        {          

            try
            {
                await dishService.DeleteDishByIdAsync(id);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid dish Id");
            }
            
        }
    }
}





