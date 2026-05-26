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

        //GET: /Categories/Edit/5 => fetch and display selected category 
        public IActionResult Edit(int id)
        {
            //fetch category by id 
            var category = _context.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            // pass seleceted category to view for display in the form 
            return View(category);
        }

        //POST: /Categories/Edit/5 => update selected category from form submission
        [HttpPost]
        public IActionResult Edit([Bind("CategoryId,Name")] Category category)
        {
            // validate form input
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            
            //update db 
            _context.Category.Update(category);
            _context.SaveChanges();

            // redirect to list
            return RedirectToAction("Index");
        }

        //GET: /Categroies/Delete/5 => delete selected category
        public IActionResult Delete(int id)
        {
            // find category by id
            var category = _context.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            // delete from db
            _context.Category.Remove(category);
            _context.SaveChanges();

            //refresh list
            return RedirectToAction("Index");
        }
    }
}
