using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared
{
    public class Constants
    {
        public static readonly string Allow_Create = "Create";
        public static readonly string Allow_Read = "Read";
        public static readonly string Allow_Update = "Update";
        public static readonly string Allow_Delete = "Delete";
        public static readonly string Allow_Approve = "Approve";
        public static readonly string Allow_Reject = "Reject";

        public static readonly string Role_Administrators= "Administrators";
        public static readonly string Role_Managers= "Managers";
        public static readonly string Role_Users = "Users";

        public static readonly string Cookies_Name= "POSAuthCookies";
    }
}
