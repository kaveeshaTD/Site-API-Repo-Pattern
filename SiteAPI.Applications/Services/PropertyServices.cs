using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteAPI.Applications.DTO;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Domain.Models;

namespace SiteAPI.Applications.Services
{
    public class PropertyServices : IPropertyServices
    {
        private readonly ITransaction _transaction;

        public PropertyServices(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<Properties> createPropertyAsync(CreatePropertyDTO properties)
        {
            try
            {
                var properite = new Properties
                {
                    Location = properties.Location,
                    Net_Worth = properties.Net_Worth,
                    User_Id = properties.User_Id,

                };

                await _transaction.GetRepository<Properties>().AddAsync(properite);
                await _transaction.SavechangesAsync();
                return properite;
            }
            catch (Exception ex)
            {

                throw new Exception($"Exception Occurred during creating property: {ex.Message}", ex);
            }
        }

        public async Task<List<Properties>> getAllPropertiesAsync()
        {
            try
            {
                var propertyes = await _transaction.GetRepository<Properties>().GetAllAsync();
                if (propertyes is null) {
                    throw new Exception("property is null");
                }
                return propertyes;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurred fetching property");
            }
        }

        public async Task<List<Properties>> getUserPropertiesAsync(Guid userId)
        {
            try
            {


                var item = await _transaction.GetRepository<Properties>().GetByIdPredicateAsync(p => p.User_Id == userId);
                if (item == null)
                {
                    throw new Exception("item not found ..... ");
                }
                return item;
            }
            catch (Exception ex) {
                throw new Exception($"Exception occurred while fetching properties for user {userId}: {ex.Message}", ex);
            }
        }
    }
}
