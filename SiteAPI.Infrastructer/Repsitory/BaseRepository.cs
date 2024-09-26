using Microsoft.EntityFrameworkCore;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Infrastructer.Database;
using System.Linq.Expressions;

namespace SiteAPI.Infrastructer.Repsitory
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext appDbContext )
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        /// <summary>
        /// Get AllList
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync<TId>(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
           // await SaveChanges();
            return entity;
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
           // await SaveChanges();
            return entity;

        }

        /// <summary>
        /// Rmove Range
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            //await SaveChanges();
       
        }

        /// <summary>
        /// get By Id Querable async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIdPredicateAsync(Expression<Func<T,bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get By Id For FirstOrDefaultAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _dbSet.FirstOrDefaultAsync(predicate);
            return result;
        }

        /// <summary>
        /// Alternative Function Can Use to Whwre and FirstOrDefault Boath this do a boath Job
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="firstOrDefault"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetByConditionalIdAsync(Expression<Func<T, bool>> predicate,bool firstOrDefault  = false)
        {
            if (firstOrDefault)
            {
                var result = await _dbSet.FirstOrDefaultAsync(predicate);
                return result == null ? Enumerable.Empty<T>() : new List<T> { result };
            }
            else
            {
                var result = await _dbSet.Where(predicate).ToListAsync();
                return result;
            }
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        //public async Task SaveChanges()
        //{
        //    await _appDbContext.SaveChangesAsync();
        //}

    }
}
