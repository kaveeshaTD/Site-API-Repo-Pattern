using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteAPI.Applications.DTO;
using SiteAPI.Domain.Models;

namespace SiteAPI.Applications.Interfaces
{
    public interface IPropertyServices
    {
        Task<Properties> createPropertyAsync(CreatePropertyDTO properties);
        Task<List<Properties>> getAllPropertiesAsync();
    }
}
