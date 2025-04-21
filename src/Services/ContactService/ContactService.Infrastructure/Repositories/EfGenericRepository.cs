using ContactService.Application.Interfaces.Repositories;
using ContactService.Domain.Common;
using ContactService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactService.Infrastructure.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _table;
        public EfGenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _table = _appDbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _table.AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            var result = await _table.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsyncByFilter(Expression<Func<T, bool>> predicate)
        {
            var result = await _table.AsNoTracking().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public IQueryable<T> GetQueryable()
        {
            var result = _table.AsQueryable();
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
