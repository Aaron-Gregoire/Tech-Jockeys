using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJockeys.Data;
using TechJockeys.Models;

namespace TechJockeys.Controllers
{
    public class ProductsController : Controller
    {
        // shared db object 
        private readonly ApplicationDbContext _context;
        
        //constructor with db dependacy 
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // product list to pass for display in view
            var products = _context.Product.ToList();

            return View(products);
        }

        //GET: /Products/Create => show empty product form including category lsit dropdown
        public IActionResult Create()
        {
            // fetch categories for dropwdown ordered a-z by name
            ViewBag.CategoryId = new SelectList(_context.Category.OrderBy(c => c.Name).ToList(), "CategoryId", "Name");
            return View();
        }

        //POST: /Products/Create => save new product from form data
        [HttpPost]
        public IActionResult Create([Bind("Name,Price,Stock,Description,Image,CategoryId")] Product product)
        {
            // validate 
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            //create and save 
            _context.Product.Add(product);
            _context.SaveChanges(); 

            //redirect to list
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
