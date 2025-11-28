using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure.Contracts;
using System.Linq.Expressions;

namespace project_assignments.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext DbContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            DbContext = repositoryContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().FirstAsync() != null;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        Task IRepositoryBase<T>.CreateAsync(T entity)
        {
            return CreateAsync(entity);
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().AsNoTracking().Where(expression).FirstOrDefaultAsync();
        }
    }
}
