
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Get()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }

        public async Task<Category> GetById(int id)
        {
            var data = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<List<Book>> GetBooksByCategoryId(int id)
        {
            var data = await _context.Books
                .Include(x => x.Category)
                .Include(x => x.Author)
                .AsNoTracking().Where(x => x.CategoryId == id)
                .ToListAsync();
            return data;
        }

        public void Save(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(int id, Category category)
        {
            var data = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.Name = category.Name ?? data.Name;
                _context.Categories.Update(data);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        public void Delete(int id)
        {
            var data = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            _context.Categories.Remove(data);
        }
    }
}