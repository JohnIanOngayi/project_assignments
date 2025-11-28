using System.Linq.Expressions;

namespace project_assignments.Infrastructure.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    }
}
