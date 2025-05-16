using System.Linq.Expressions;

namespace Trial.Application.Repository
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByAsync(Expression<Func<T,bool>> query);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id); 
    }
}
