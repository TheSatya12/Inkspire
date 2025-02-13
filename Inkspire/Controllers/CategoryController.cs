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
    }
}
