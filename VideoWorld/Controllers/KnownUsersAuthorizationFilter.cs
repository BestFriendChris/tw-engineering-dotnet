using System.Web.Mvc;

namespace VideoWorld.Controllers
{
    public class KnownUsersAuthorizationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Login")
                return;

            if (filterContext.HttpContext.Session == null)
            {
                filterContext.Result = new RedirectResult("/login");
            }
            else if (filterContext.HttpContext.Session["CurrentUser"] == null)
            {
                filterContext.Result = new RedirectResult("/login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}