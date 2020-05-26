using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Models;

namespace bookstore.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        void Save(Author author);
        Task<List<Author>> Get();
        Task<Author> GetById(int id);
        Task<List<Book>> GetBooksByAuthorId(int id);
        void Update(int id, Author author);
        void Delete(int id);
    }
}