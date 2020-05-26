using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Models;

namespace bookstore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        void Save(Book book);
        Task<List<Book>> Get();
        Task<Book> GetById(int id);
        void Update(int id, Book book);
        void Delete(int id);
    }
}