using Inkspire.DataAccess.Data;
using Inkspire.Models;
 

namespace Inkspire.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Category> GetAllCategories(string? searchQuery = null)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                return _db.categories.Where(c => c.Name.Contains(searchQuery)).ToList();
            }
            return _db.categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            var objCategory =  _db.categories.FirstOrDefault(c => c.Id == categoryId);
            return objCategory;
        }

        public void AddCategory(Category category)
        {
            _db.categories.Add(category);
            _db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _db.categories.Update(category);
            _db.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _db.categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                _db.categories.Remove(category);
                _db.SaveChanges();
            }
        }
    }
}
