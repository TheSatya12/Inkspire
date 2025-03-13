using Inkspire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkspire.DataAccess.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories(string searchQuery = null);
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
