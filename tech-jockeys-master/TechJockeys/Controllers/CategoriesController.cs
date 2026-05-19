using Microsoft.AspNetCore.Mvc;
using TechJockeys.Models;

namespace TechJockeys.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            // create empty category list so view doesnt crash
            var categories = new List<Category>();

            // pass empty list to view
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
