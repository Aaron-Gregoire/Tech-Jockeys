using Microsoft.AspNetCore.Mvc;
using TechJockeys.Data;
using TechJockeys.Models;

namespace TechJockeys.Controllers
{
    public class CategoriesController : Controller
    {
        // shared db conn available to all controller method 
        private readonly ApplicationDbContext _context;

        // constructor with db dependancy 
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // fetch category list from db using dbset object 
            var categories = _context.Category.ToList();

            // pass empty list to view
            return View(categories);
        }

        // GET: /Categories/Create => show empty Category form
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Categories/Create => process form submission and create new category
        [HttpPost]
        public IActionResult Create([Bind("Name")] Category category)
        {
            // validate input. show form again if invaid
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create new category and save to db
            _context.Category.Add(category);
            _context.SaveChanges();

            // redirect to index to see updated list of catgories
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
