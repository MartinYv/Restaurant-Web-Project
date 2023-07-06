using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant2.Data;
using System.Threading.Tasks;

public class ShoppingCartController : Controller
{
    private readonly ShoppingCartService _shoppingCartService;
    private readonly IDishService dishService; 

    public ShoppingCartController(ShoppingCartService shoppingCartService, IDishService _dishService)
    {
        _shoppingCartService = shoppingCartService;
        dishService = _dishService;
    }

    public async Task<IActionResult> Index()
    {
        var cart = await _shoppingCartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int dishId)
    {
        var dish = await dishService.GetDishById(dishId);

        if (dish != null)
        {
            await _shoppingCartService.AddToCart(dish);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        await _shoppingCartService.RemoveFromCart(cartItemId);
        return RedirectToAction("Index");
    }
}
