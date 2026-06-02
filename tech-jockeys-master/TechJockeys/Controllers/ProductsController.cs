using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            //join to parent category so we can include category name 
            //sort by name a-z 
            var products = _context.Product
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToList();

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

        //GET: /Products/Edit/5 => show populated product form
        public IActionResult Edit(int id)
        {
            // find prduct by id
            var product = _context.Product.Find(id);

            //if not found => error
            if(product == null)
            {
                 return NotFound();
            }

            // fetch categories for dropwdown ordered a-z by name
            ViewBag.CategoryId = new SelectList(_context.Category.OrderBy(c => c.Name).ToList(), "CategoryId", "Name");

            //pass product to view 
            return View(product);
        }

        //POST: /Products/Edit/5 => update product with form data
        [HttpPost]
        public IActionResult Edit([Bind("ProductId,Name,Price,Stock,Description,CategoryId")] Product product, IFormFile? Image)
        {
            //input validation
            if (!ModelState.IsValid)
            {
                //invalid => reload page w/ existing values
                return View(product);
            }

            //data valid => save to db
            _context.Product.Update(product);
            _context.SaveChanges();

            //redirect to list
            return RedirectToAction("Index");

        }


        //GET: /Products/Delete/5 => find and delete the selected product

        public IActionResult Delete(int id)
        {
            //find product by id
            var product = _context.Product.Find(id);

            //if not found error
            if (product == null)
            {
                return NotFound();
            }

            //remove from db
            _context.Product.Remove(product);
            _context.SaveChanges();

            //refresh list on index
            return RedirectToAction("Index");
        }

        private static string UploadImage(IFormFile Image)
        {
            // get temp location of uploaded image
            var filePath = Path.GetTempFileName();

            //create unique name to prevent overwriting using globally unique identifier (GUID)
            //e.g. product.jpg => 24g24h2gr2-product.jpg
            var FileName = Guid.NewGuid().ToString() + "-" + Image.FileName;

            //set destination path dynamically so it works locally and in production 
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\" + FileName;

            //use filestream to copy image from temp folder to img folder
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            // return new unique file name for saving to db 
            return FileName;
        }

    }
}
