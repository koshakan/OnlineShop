using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        IEnumerable<Category> GetAllCategory();
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(int id);
    }
}
