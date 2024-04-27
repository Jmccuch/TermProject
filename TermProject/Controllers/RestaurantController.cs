using Microsoft.AspNetCore.Mvc;
using TermProject.Models;

namespace TermProject.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index(List<RestaurantViewModel> restaurants)
        {
            return View(restaurants);
        }
    }
}
