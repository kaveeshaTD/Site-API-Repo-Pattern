using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteAPI.Applications.DTO
{
    public class CreatePropertyDTO
    {
        public Guid Propertie_Id { get; set; }
        public string Location { get; set; }
        public Double Net_Worth { get; set; }
        public Guid User_Id { get; set; }
    }
}
