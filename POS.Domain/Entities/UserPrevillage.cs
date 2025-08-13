using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class UserPrevillage
    {
        //[Column("Username",50)] 
        public string Username { get; set; }

        //[Column("RoleName",100)] 
        public string RoleName { get; set; }

        //[Column("MenuID")]
        public Int32? MenuID { get; set; }

        //[Column("RoleID")]
        public Int32? RoleID { get; set; }

        //[Column("AllowCreate")]
        public bool AllowCreate { get; set; }

        //[Column("AllowRead")]
        public bool AllowRead { get; set; }

        //[Column("AllowUpdate")]
        public bool AllowUpdate { get; set; }

        //[Column("AllowDelete")]
        public bool AllowDelete { get; set; }

        //[Column("AllowPrint")]
        public bool AllowPrint { get; set; }
    }
}
