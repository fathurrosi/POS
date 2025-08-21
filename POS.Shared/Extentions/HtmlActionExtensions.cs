
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using System.Data;
using System.Text;

namespace POS.Shared.Extentions
{
    public static class HtmlHelperExtensions
    {
        private static UserData? GetUserData(ViewContext context)
        {
            string username = string.Format("[0}", context.HttpContext.User.Identity?.Name);
            string protectedUserData = context.HttpContext.Session.GetString($"UserData_{username}");
            if (!string.IsNullOrEmpty(protectedUserData))
            {
                return JsonConvert.DeserializeObject<UserData>(Encoding.UTF8.GetString(Convert.FromBase64String(protectedUserData)));
            }
            return null;
        }
        public static HtmlString AllowCreate(this IHtmlHelper htmlHelper, string screen)
        {
            bool isAllowed = false;
            UserData? userData = GetUserData(htmlHelper.ViewContext);
            if (userData != null)
            {
                List<string> roles = userData.Roles;
                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == screen && roles.Contains(t.Role)).ToList();
                isAllowed = prevItems.Where(t => t.AllowCreate== true).Any();
            }
            return new HtmlString(isAllowed ? "Y" : "N");
        }

        public static HtmlString AllowDelete(this IHtmlHelper htmlHelper, string screen)
        {
            bool isAllowed = false;
            UserData? userData = GetUserData(htmlHelper.ViewContext);
            if (userData != null)
            {
                List<string> roles = userData.Roles;
                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == screen && roles.Contains(t.Role)).ToList();
                isAllowed = prevItems.Where(t => t.AllowDelete== true).Any();
            }
            return new HtmlString(isAllowed ? "Y" : "N");
        }

        public static HtmlString AllowPrint(this IHtmlHelper htmlHelper, string screen)
        {
            bool isAllowed = false;
            UserData? userData = GetUserData(htmlHelper.ViewContext);
            if (userData != null)
            {
                List<string> roles = userData.Roles;
                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == screen && roles.Contains(t.Role)).ToList();
                isAllowed = prevItems.Where(t => t.AllowPrint== true).Any();
            }
            return new HtmlString(isAllowed ? "Y" : "N");
        }

        public static HtmlString AllowEdit(this IHtmlHelper htmlHelper, string screen)
        {
            bool isAllowed = false;
            UserData? userData = GetUserData(htmlHelper.ViewContext);
            if (userData != null)
            {
                List<string> roles = userData.Roles;
                List<VUserPrevillage> prevItems = userData.Previllages.Where(t => t.Menu == screen && roles.Contains(t.Role)).ToList();
                isAllowed = prevItems.Where(t => t.AllowUpdate== true).Any();
            }
            return new HtmlString(isAllowed ? "Y" : "N");
        }
    }
}
