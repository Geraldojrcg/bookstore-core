using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var data = await _context.Books
                .Include(x => x.Category)
                .Include(x => x.Author)
                .ToListAsync();
            return data;
        }

        public async Task<Book> GetById(int id)
        {
            var data = await _context.Books
                .Include(x => x.Category)
                .Include(x => x.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public void Save(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(int id, Book book)
        {
            var data = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.Title = book.Title ?? data.Title;
                data.AuthorId = book.AuthorId;
                data.CategoryId = book.CategoryId ?? data.CategoryId;
                data.ReleaseDate = book.ReleaseDate ?? data.ReleaseDate;
                _context.Books.Update(data);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        public void Delete(int id)
        {
            var data = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            _context.Books.Remove(data);
        }
    }
}