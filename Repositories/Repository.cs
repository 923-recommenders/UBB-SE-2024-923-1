using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByTwoIdentifiers(int id1, int id2)
        {
            return await _context.Set<T>().FindAsync(id1, id2);
        }

        public async Task<T> GetByThreeIdentifiers(int id1, int id2, int id3)
        {
            return await _context.Set<T>().FindAsync(id1, id2, id3);
        }

        public async Task<T> GetByThreeIdentifiers(int id1, int id2, DateTime id3)
        {
            return await _context.Set<T>().FindAsync(id1, id2, id3);
        }

        public async Task<T> GetByFourIdentifiers(int id1, string id2, string id3, string id4)
        {
            return await _context.Set<T>().FindAsync(id1, id2, id3, id4);
        }
    }
}
