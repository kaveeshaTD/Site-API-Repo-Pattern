using System.Linq.Expressions;

namespace SiteAPI.Applications.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync<TId>(TId id);
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<List<T>> GetByIdPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetByConditionalIdAsync(Expression<Func<T, bool>> predicate, bool firstOrDefault = false);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate);
    }
}
