using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Domain.Models
{
    public class Properties
    {
        public Guid Propertie_Id { get; set; }
        public string Location { get; set; }
        public Double Net_Worth{ get; set; }

        public User User { get; set; }
        public Guid User_Id { get; set; }
    }
}
