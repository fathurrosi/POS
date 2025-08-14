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
                    Console.WriteLine($"User: {userData.User.Username}, Role: {userData.Role.Name}");

                }
                else
                {
                    context.RejectPrincipal();
                    //context.Response.Redirect("/Information/Unauthorized");

                    //context.HttpContext.SignOutAsync(Constants.Cookies_Name);
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
    public class UserData
    {
        // Add other properties as needed based on your application's requirements
        public User User { get; set; }
        public Role Role { get; set; }
        public List<Previllage> Previllages { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
