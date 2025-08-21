using Microsoft.AspNetCore.Mvc;
using POS.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class POSAuthorizeAttribute : TypeFilterAttribute
    {
        //public POSAuthorizeAttribute(string roles = null, string permissions = null)
        //: base(typeof(POSAuthorizeFilter))
        //{
        //    Arguments = new object[] { new Dictionary<string, object>        {
        //    {"roles", roles},
        //    {"permissions", permissions}
        //}};
        //}

        public POSAuthorizeAttribute(string screen = null)
       : base(typeof(POSAuthorizeFilter))
        {
            //rguments = new object[] { new Dictionary<string, object> { { "screen", screen } } };
            Screen = screen;
            Arguments = new object[] { screen };
        }

        //public string Roles { get; set; }
        //public string Permissions { get; set; }
        public string Screen { get; set; }
    }

}
