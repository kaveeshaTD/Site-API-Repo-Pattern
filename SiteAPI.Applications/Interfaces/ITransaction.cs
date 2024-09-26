using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Applications.Interfaces
{
    public interface ITransaction : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommiteAsync();
        IBaseRepository<T> GetRepository<T>() where T : class;
        Task RollBackAsync();
        Task<int> SavechangesAsync();
    }
}
