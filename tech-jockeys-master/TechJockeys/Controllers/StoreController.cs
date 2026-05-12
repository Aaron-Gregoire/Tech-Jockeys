using Microsoft.AspNetCore.Mvc;

namespace TechJockeys.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
