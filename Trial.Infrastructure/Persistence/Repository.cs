using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trial.Application.Repository;
using Trial.Domain.Entities;

namespace Trial.Infrastructure.Persistence
{
    public class Repository<T> : IAsyncRepository<T> where T : class
    {
        private readonly SysContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SysContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> query)
        {
            return (await _dbSet.FirstOrDefaultAsync(query))!;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            var result = 0;
            if (entity != null)
            {
                _dbSet.Remove(entity);
                result = await _context.SaveChangesAsync();
            }
            return (result > 0);
        }
    }
}
