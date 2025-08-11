//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace POS.Shared.Extentions
//{
//    public static class HtmlActionExtensions
//    {
//        public static Task<IHtmlContent> Action(this IHtmlHelper htmlHelper, string actionName, string controllerName)
//        {
//            return htmlHelper.Action(actionName, controllerName, new { });
//        }

//        public static async Task<IHtmlContent> Action(this IHtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
//        {
//            var controllerType = GetControllerType(controllerName);
//            if (controllerType is null)
//                throw new InvalidOperationException($"Controller '{controllerName}' not found.");

//            var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
//            var controller = serviceProvider.GetRequiredService(controllerType) as BaseController;

//            var routeData = new RouteData(new RouteValueDictionary(ParseRoute(actionName, controllerName, routeValues)));
//            var actionDescriptor = GetDescriptor(serviceProvider, actionName, controllerName);

//            controller.ControllerContext = new ChildControllerContext
//            {
//                HttpContext = htmlHelper.ViewContext.HttpContext,
//                RouteData = routeData,
//                ActionDescriptor = actionDescriptor,
//            };

//            var method = controllerType.GetMethod(actionName);
//            if (method == null)
//                throw new InvalidOperationException($"Action '{actionName}' not found in controller '{controllerName}'.");

//            var filters = actionDescriptor.FilterDescriptors
//                .Select(x => x.Filter)
//                .Where(attr => attr is ActionFilterAttribute)
//                .Cast<ActionFilterAttribute>()
//                .ToArray();
//            var actionContext = new ActionExecutingContext(
//                new ActionContext(htmlHelper.ViewContext.HttpContext, routeData, actionDescriptor),
//                new List<IFilterMetadata>(),
//                ParseRoute(actionName, controllerName, routeValues),
//                controller);
//            foreach (var filter in filters)
//                filter.OnActionExecuting(actionContext);

//            var parameters = method.GetParameters()
//                .Select(p => routeValues?.GetType().GetProperty(p.Name)?.GetValue(routeValues))
//                .ToArray();

//            try
//            {
//                var actionResult = await GetActionResult(method.Invoke(controller, parameters));
//                var view = actionResult switch
//                {
//                    ViewResult x => await RenderViewAsync(serviceProvider, controller.ControllerContext, x.ViewData, x.ViewName),
//                    PartialViewResult x => await RenderViewAsync(serviceProvider, controller.ControllerContext, x.ViewData, x.ViewName),
//                    ContentResult x => x.Content,
//                    JsonResult x => x.Value?.ToString(),
//                    _ => throw new InvalidOperationException($"Unexpected action result. Controller {controllerName}, action {actionName}")
//                };

//                return new HtmlString(view ?? string.Empty);
//            }
//            catch (Exception ex)
//            {
//                return await htmlHelper.ErrorAction(ex);
//            }
//        }

//        public static async Task<IHtmlContent> ErrorAction(this IHtmlHelper htmlHelper, Exception exception)
//        {
//            var actionName = "Exception";
//            var controllerName = "Error";

//            var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
//            var controller = serviceProvider.GetRequiredService<ErrorController>();

//            var routeData = new RouteData(new RouteValueDictionary(ParseRoute(actionName, controllerName, new { })));
//            var actionDescriptor = GetDescriptor(serviceProvider, actionName, controllerName);

//            controller.ControllerContext = new ChildControllerContext
//            {
//                HttpContext = htmlHelper.ViewContext.HttpContext,
//                RouteData = routeData,
//                ActionDescriptor = actionDescriptor,
//            };

//            var viewModel = new ErrorViewModel()
//            {
//                Exception = exception,
//                AspNetRequestId = controller.ControllerContext.HttpContext.TraceIdentifier
//            };

//            var actionResult = controller.PartialView("Error", viewModel);
//            var view = await RenderViewAsync(serviceProvider, controller.ControllerContext, actionResult.ViewData, actionResult.ViewName);

//            return new HtmlString(view ?? string.Empty);
//        }

//        private static async Task<string> RenderViewAsync(
//            IServiceProvider serviceProvider,
//            ControllerContext controllerContext,
//            ViewDataDictionary viewData,
//            string viewName)
//        {
//            var viewEngine = serviceProvider.GetRequiredService<IRazorViewEngine>();
//            var tempDataProvider = serviceProvider.GetRequiredService<ITempDataProvider>();
//            var tempData = new TempDataDictionary(controllerContext.HttpContext, tempDataProvider);

//            var view = viewEngine.FindView(controllerContext, viewName, false)?.View
//                ?? throw new InvalidOperationException($"View '{viewName}' not found.");

//            using var stringWriter = new StringWriter();

//            var viewContext = new ChildViewContext(
//                controllerContext,
//                view,
//                viewData,
//                tempData,
//                stringWriter);

//            await view.RenderAsync(viewContext);

//            return stringWriter.ToString();
//        }

//        private static ControllerActionDescriptor GetDescriptor(IServiceProvider serviceProvider, string action, string controller)
//        {
//            var endpointDataSource = serviceProvider.GetRequiredService<EndpointDataSource>();

//            var endpoint = endpointDataSource.Endpoints
//            .OfType<RouteEndpoint>()
//            .FirstOrDefault(e => {
//                var actionDescriptor = e.Metadata.GetMetadata<ControllerActionDescriptor>();
//                return actionDescriptor != null &&
//                        actionDescriptor.ControllerName == controller &&
//                        actionDescriptor.ActionName == action;
//            });

//            return endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
//        }

//        private static Dictionary<string, object> ParseRoute(string action, string controller, object routeValues)
//        {
//            var res = new Dictionary<string, object>();

//            res.Add("controller", controller);
//            res.Add("action", action);

//            foreach (var property in routeValues.GetType().GetProperties())
//            {
//                var value = property.GetValue(routeValues)?.ToString();
//                if (value is null)
//                    throw new ArgumentNullException(property.Name);

//                res.Add(property.Name, value);
//            }

//            return res;
//        }

//        private static Type GetControllerType(string controllerName)
//        {
//            return Type.GetType($"YourProject.Controllers.{controllerName}Controller");
//        }

//        private static async Task<IActionResult> GetActionResult(object actionResult)
//        {
//            if (actionResult is not Task<IActionResult> actionResultTask)
//                return actionResult as IActionResult;

//            await actionResultTask;
//            return actionResultTask.Result;
//        }
//    }
//}
