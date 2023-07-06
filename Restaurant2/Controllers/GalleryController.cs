using Microsoft.AspNetCore.Mvc;

using Restaurant2.Data;

namespace Restaurant.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly RestaurantDbContext context;

        public GalleryController(RestaurantDbContext _context)
        {
            context = _context;
        }
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
