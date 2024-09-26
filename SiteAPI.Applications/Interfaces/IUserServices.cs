using SiteAPI.Applications.DTO;
using SiteAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Applications.Interfaces
{
    public interface IUserServices
    {
        Task<User> CreateUSerAsync(CreateUserDTO createUserDTO);
        Task<List<User>> GetAllUserAsync();
        Task<User> DdeletUserAsync(Guid id);
        Task<User> UpdateUserAsync(Guid id, CreateUserDTO userdto);
    }
}
