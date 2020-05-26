using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Models;

namespace bookstore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void Save(Category book);
        Task<List<Category>> Get();
        Task<List<Book>> GetBooksByCategoryId(int id);
        Task<Category> GetById(int id);
        void Update(int id, Category category);
        void Delete(int id);
    }
}