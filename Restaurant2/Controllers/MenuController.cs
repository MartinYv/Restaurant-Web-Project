﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            try
            {
				await menuService.AddMenuAcync(model);
				return RedirectToAction(nameof(All));
			}
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);

			}

		}

        public async Task<IActionResult> All()
        {
            var model = await menuService.AllMenusAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int menuId)
        {
            await menuService.DeleteMenuAsync(menuId);
            return RedirectToAction(nameof(All));
        }


       
    }
}
