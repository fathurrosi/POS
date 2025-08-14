using Microsoft.AspNetCore.Authorization;
using POS.Shared.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Handlers
{
    public class POSAuthorizeHandler : AuthorizationHandler<POSAuthorizeAttribute>
    {
        //protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizeAttribute requirement)
        //{

        //}

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, POSAuthorizeAttribute requirement)
        {
            var user = context.User;
            if (user == null)
            {
                context.Fail();
                return;
            }

            var roles = requirement.Roles.Split(',');
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            if (userRoles.Intersect(roles).Any())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            await Task.CompletedTask;
        }
    }
}