using Inkspire.DataAccess.Data;
using Inkspire.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inkspire.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CategoryController> _logger;
        private readonly IDiagnosticContext _diagnosticContest;
        public CategoryController(ApplicationDbContext db, ILogger<CategoryController> logger,IDiagnosticContext diagnosticContext)
        {
            _db = db;
            _logger = logger;
            _diagnosticContest = diagnosticContext;
        }
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Index Action method of Category Controller");
                List<Category> categories = _db.categories.ToList();
                _diagnosticContest.Set("Categories",categories);
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Index: {ex.Message}");
                return View(new List<Category>());
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category objCategory)
        {
            try
            {
                _logger.LogInformation("Create Action Method of Category Controller");
                _logger.LogDebug($"Category obj: {objCategory}");

                if (ModelState.IsValid)
                {
                    _db.categories.Add(objCategory);
                    _db.SaveChanges();
                    TempData["Success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
                return View(objCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create: {ex.Message}");
                TempData["Error"] = "An error occurred while creating the category.";
                return View(objCategory);
            }
        }
        public IActionResult Edit(int? categoryId)
        {
            try
            {
                _logger.LogInformation("Edit Action Method of Category Controller");
                _logger.LogDebug($"Category Id: {categoryId}");
                if (categoryId == null || categoryId == 0)
                {
                    return NotFound();
                }
                Category categoryFromDb = _db.categories.FirstOrDefault(i => i.Id == categoryId);
                if (categoryFromDb == null)
                {
                    return NotFound();
                }
                return View(categoryFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Edit: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Category objCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.categories.Update(objCategory);
                    _db.SaveChanges();
                    TempData["Success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                return View(objCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Edit (POST): {ex.Message}");
                TempData["Error"] = "An error occurred while updating the category.";
                return View(objCategory);
            }
        }

        public IActionResult Delete(int? categoryId)
        {
            try
            {
                if (categoryId == null || categoryId == 0)
                {
                    return NotFound();
                }
                Category categoryFromDb = _db.categories.FirstOrDefault(i => i.Id == categoryId);
                if (categoryFromDb == null)
                {
                    return NotFound();
                }
                _db.categories.Remove(categoryFromDb);
                _db.SaveChanges();
                TempData["Error"] = $"Category: {categoryFromDb.Name} deleted from DB";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Delete: {ex.Message}");
                TempData["Error"] = "An error occurred while deleting the category.";
                return RedirectToAction("Index");
            }
        }
    }
}
