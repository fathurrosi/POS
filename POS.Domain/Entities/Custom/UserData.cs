using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities.Custom
{
    public class UserData
    {
        // Add other properties as needed based on your application's requirements
        public string Username{ get; set; }
        public List<string> Roles { get; set; }
        public List<VUserPrevillage> Previllages { get; set; }
        //public List<Menu> Menus { get; set; }
    }
}
