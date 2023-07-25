using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Restaurant.Data.Models;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Dish;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

using static Restaurant.Common.NotificationMessagesConstants;

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
                TempData[SuccessMessage] = "Dish successfully added.";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {

                TempData[ErrorMessage] = ex.Message;
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
                TempData[SuccessMessage] = "Dish successfully deleted.";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = ex.Message;
                return RedirectToAction(nameof(All));
            }    
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await dishService.GetDishForEditByIdAsync(id);
            return View(model);
        }



		[HttpPost]
		public async Task<IActionResult> Edit(AddDishViewModel model, int id)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
			}

			try
			{
				await dishService.EditDishById(model, id);
                TempData[SuccessMessage] = "Dish successfully deleted.";
				return RedirectToAction(nameof(All));
			}
			catch (Exception ex)
			{
                TempData[ErrorMessage] = ex.Message;
                return RedirectToAction(nameof(All));
            }
		}
	}
}





