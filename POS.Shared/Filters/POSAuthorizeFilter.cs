using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                context.Result = new UnauthorizedResult();
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

            if (context.Request.Cookies.TryGetValue("UserData", out string protectedUserData))
            {
                UserData? userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
                if (userData == null) return false;

                Menu? menuItem = userData.Menus.Where(t => t.Code == _screen).FirstOrDefault();
                if (menuItem == null) return false;

                List<int> roleIdList = userData.Roles.Where(t => roles.Contains(t.Name)).Select(t => t.Id).ToList();

                List<Previllage> prevItems = userData.Previllages.Where(t => t.MenuId == menuItem.Id && roleIdList.Contains(t.RoleId)).ToList();
                bool allowRead = prevItems.Where(t => t.AllowRead == true).Any();
                if (userData != null && userData.Previllages.Count() > 0 && allowRead)
                {
                    // Example: Log the username
                    Console.WriteLine($"User: {userData.User.Username}, Role: {string.Join(", ", roles.ToArray())}");
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
