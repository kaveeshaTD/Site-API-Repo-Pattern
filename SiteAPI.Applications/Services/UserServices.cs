using SiteAPI.Applications.DTO;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Applications.Services
{
    public class UserServices :IUserServices
    {
        private readonly ITransaction _transaction;

        public UserServices(ITransaction transaction)
        {
            _transaction = transaction;
        }
        public  async Task<User> CreateUSerAsync(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = new User
                {
                    First_Name = createUserDTO.First_Name,
                    Last_Name = createUserDTO.Last_Name,
                    Email = createUserDTO.Email,
                    Country = createUserDTO.Country,
                };
                await _transaction.GetRepository<User>().AddAsync(user);
                await _transaction.SavechangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurred");
            } 
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            try
            {
                var users = await _transaction.GetRepository<User>().GetAllAsync();
                if (users == null)
                {
                    throw new Exception("No user Founs");
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurred");
            }
        }

        public async Task<User> DdeletUserAsync(Guid id)
        {
            var user = await _transaction.GetRepository<User>().GetByIdAsync(id);

            if(user == null)
            {
                // Handle the case where the user was not found (throw exception or return null)
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            await _transaction.GetRepository<User>().DeleteAsync(user);
            await _transaction.SavechangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(Guid id , CreateUserDTO userdto)
        {
            var user = await _transaction.GetRepository<User>().GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.First_Name = userdto.First_Name;
            user.Last_Name = userdto.Last_Name;
            user.Email = userdto.Email;
            user.Country = userdto.Country;

           await _transaction.SavechangesAsync();
            return user;
        }
    }
}
