using Inkspire.Data;
using Inkspire.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inkspire.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();


            //present Not Needed
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{

            //    //if you dont mention the property in place of key of  ModelState.AddModelError(key,errorMessage) and kept empty
            //    ModelState.AddModelError("", "Display Order and Name should not be same");
            //    //error is shown on top for complete form and you have to mention {asp-validation-for=ModelOnly} in <div> tag

            //    //if you mention the property in place of key of  ModelState.AddModelError(key,errorMessage)
            //    //ModelState.AddModelError("Name", "Display Order and Name should not be same");
            //    //error is shown below the input element individually
            //}

        }
    }
}
