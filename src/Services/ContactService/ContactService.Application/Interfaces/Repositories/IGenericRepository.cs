using ContactService.Domain.Common;
using System.Linq.Expressions;

namespace ContactService.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetQueryable();
        Task<IList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsyncByFilter(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(T entity);

    }
}
