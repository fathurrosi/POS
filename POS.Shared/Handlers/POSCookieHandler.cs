using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Handlers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Newtonsoft.Json;
    using POS.Domain.Entities;
    using POS.Domain.Entities.Custom;
    using System;
    using System.ComponentModel.Design;
    using System.Security.Claims;

    public class POSCookieHandler : CookieAuthenticationEvents
    {
        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }

        public override Task SigningIn(CookieSigningInContext context)
        {    
            return base.SigningIn(context);
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            if (context.Principal?.Identity?.IsAuthenticated == false)
            {
                return Task.CompletedTask;
            }

            string userName = context.Principal?.Identity?.Name;
            if (context.HttpContext.Request.Cookies.TryGetValue("UserData", out string protectedUserData))
            {
                UserData userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
                if (userData != null && userData.Previllages.Count() > 0)
                {
                    // Example: Log the username
                    Console.WriteLine($"User: {userData.User.Username}, Role: {string.Join(", ", userData.Roles.Select(t => t.Name).ToArray())}");

                }
                else
                {
                    //context.RejectPrincipal();
                }
            }
            return base.ValidatePrincipal(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }
        //public override async Task OnSigningIn(CookieSigningInContext context)
        //{
        //    var userData = new UserData { CustomClaimValue = "CustomValue" };

        //    var protectedUserData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userData)));

        //    context.CookieOptions.Expires = DateTime.Now.AddDays(30);
        //    context.Response.Cookies.Append("UserData", protectedUserData);

        //    await base.OnSigningIn(context);
        //}

        //public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        //{
        //    if (context.HttpContext.Request.Cookies.TryGetValue("UserData", out string protectedUserData))
        //    {
        //        var userData = JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));

        //        // Use the user data
        //    }

        //    await base.ValidatePrincipal(context);
        //}

        //public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        //{
        //    var user = context.Principal;

        //    // Retrieve user data from the database or cache
        //    var userData = await GetUserDataAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));

        //    // Add custom claims to the principal
        //    var claims = new[]
        //    {
        //    new Claim("CustomClaim", userData.CustomClaimValue),
        //    };

        //    var appIdentity = new ClaimsIdentity(claims);
        //    context.Principal.AddIdentity(appIdentity);

        //    await base.ValidatePrincipal(context);
        //}

    }

}
