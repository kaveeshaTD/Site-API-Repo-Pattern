using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Domain.Models
{
    public class User
    {
        public Guid User_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }

        public ICollection<Properties> Properties { get; set; }
      
    }
}
