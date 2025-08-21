using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using POS.Shared.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace POS.Shared.Filters
{
    public class POSAuthorizeFilter : IAsyncAuthorizationFilter
    {
        //private string _roles;
        //private string _permissions;
        private string _screen;

        //public POSAuthorizeFilter(Dictionary<string, object> args)
        //{
        //    _roles = string.Format("{0}", args["roles"]);
        //    _permissions = string.Format("{0}", args["permissions"]);
        //}

        public POSAuthorizeFilter(string screen)
        {
            _screen = screen;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            if (!HasAccess(context.HttpContext))
            {
                context.Result = new ForbidResult();
                return;
            }

            return;
        }

        private bool HasAccess(HttpContext context)
        {
            ClaimsPrincipal user = context.User;
            string username = string.Format("{0}", user.Identity?.Name);
            List<string> roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            string protectedUserData = context.Session.GetString($"UserData_{username}");
            //HttpContext.Session.SetString($"UserData_{model.Username}", base64data);

            //if (context.Request.Cookies.TryGetValue("UserData", out string protectedUserData))
            if (!string.IsNullOrEmpty(protectedUserData))
            {
                UserData? userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
                if (userData == null) return false;

                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == _screen && roles.Contains(t.Role)).ToList();
                bool allowRead = prevItems.Where(t => t.AllowRead == true).Any();
                if (userData != null && userData.Previllages.Count() > 0 && allowRead)
                {
                    // Example: Log the username
                    Console.WriteLine($"User: {userData.Username}, Role: {string.Join(", ", roles.ToArray())}");
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
