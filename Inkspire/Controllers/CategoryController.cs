using Inkspire.DataAccess.Data;
using Inkspire.DataAccess.Repository;
using Inkspire.Filters.ActionFilters;
using Inkspire.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inkspire.Controllers
{
    [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new Object[] 
    { "X-Custom-Controller-Key", "Custom-controller-Value",3 },Order =3)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IDiagnosticContext _diagnosticContest;
        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger,IDiagnosticContext diagnosticContext)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _diagnosticContest = diagnosticContext;
        }
        [TypeFilter(typeof(CategoryListActionFilter),Order = 0)]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new Object[] 
        { "X-Custom-Key", "Custom-Value",1 },Order =1)]
        [ServiceFilter(typeof(CustomIndexActonFilter))]
        public IActionResult Index(string searchQuery)
        {
            try
            {
                _logger.LogInformation("Index action called.");
                List<Category> categories = _categoryRepository.GetAllCategories(searchQuery);
                ViewBag.SearchQuery = searchQuery; 
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Index: {ex.Message}");
                return View(new List<Category>());
            }
        }


        [ServiceFilter(typeof(CustomAsyncResultFilter))]
        [ServiceFilter(typeof(CacheResourceFilter))]
        public JsonResult GetCategoryObj(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                {
                    return Json(new { success = false, message = "Invalid category ID." });
                }

                var category = _categoryRepository.GetCategoryById(categoryId);
                if (category == null)
                {
                    return Json(new { success = false, message = "Category not found." });
                }

                return Json(new { success = true, data = category });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCategoryObj: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while fetching the category." });
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
                if (ModelState.IsValid)
                {
                    _categoryRepository.AddCategory(objCategory);
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
                if (categoryId == null || categoryId == 0)
                {
                    return NotFound();
                }
                var category = _categoryRepository.GetCategoryById(categoryId.Value);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
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
                    _categoryRepository.UpdateCategory(objCategory);
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
                _categoryRepository.DeleteCategory(categoryId.Value);
                TempData["Error"] = "Category deleted successfully";
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
