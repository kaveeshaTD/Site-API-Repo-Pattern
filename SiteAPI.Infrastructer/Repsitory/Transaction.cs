using Microsoft.EntityFrameworkCore.Storage;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Infrastructer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Infrastructer.Repsitory
{
    public class Transaction : ITransaction
    {
        private readonly AppDbContext _appDbContext;
        private IDbContextTransaction _dbContextTransaction;
        private bool disposed = false;
        private readonly Dictionary<Type, object> _repositories;

        public Transaction( AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            _dbContextTransaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commite
        /// </summary>
        /// <returns></returns>
        public async Task CommiteAsync()
        {
            try
            {
                await _dbContextTransaction.CommitAsync();
            }
            catch
            {
                await _dbContextTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Retrieves an instance of the repository for the specified entity type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IBaseRepository<T>;
            }
            var repository = new BaseRepository<T>(_appDbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        /// <summary>
        /// Roll back the current database transaction and disposes of the transaction resources asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task RollBackAsync()
        {
            await _dbContextTransaction.RollbackAsync();
            await _dbContextTransaction.DisposeAsync();
            _dbContextTransaction = null;
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        public async Task<int> SavechangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
        
        //for interface inherit no used for implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
