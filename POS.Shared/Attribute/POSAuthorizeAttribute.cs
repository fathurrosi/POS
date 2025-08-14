
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace POS.Shared.Attribute
{
    public class POSAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Roles { get; set; }

        public POSAuthorizeAttribute(string roles)
        {
            Roles = roles;
        }
    }
}