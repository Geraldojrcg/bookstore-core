using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> Get()
        {
            var data = await _context.Authors.ToListAsync();
            return data;
        }

        public async Task<Author> GetById(int id)
        {
            var data = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<List<Book>> GetBooksByAuthorId(int id)
        {
            var data = await _context.Books
                .Include(x => x.Category)
                .Include(x => x.Author)
                .AsNoTracking()
                .Where(x => x.AuthorId == id)
                .ToListAsync();
            return data;
        }

        public void Save(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Update(int id, Author author)
        {
            var data = _context.Authors.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.Name = author.Name ?? data.Name;
                data.LastName = author.LastName ?? data.LastName;
                data.Email = author.Email ?? data.Email;
                _context.Authors.Update(data);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        public void Delete(int id)
        {
            var data = _context.Authors.Where(x => x.Id == id).FirstOrDefault();
            _context.Authors.Remove(data);
        }
    }
}