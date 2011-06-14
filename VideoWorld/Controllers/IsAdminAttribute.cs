using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;
using VideoWorld.Repositories;
using VideoWorld.Utils;

namespace VideoWorld.Controllers
{
    public class IsAdminAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loggedInUser = (string)filterContext.HttpContext.Session["CurrentUser"];
            var customerRepository = ((NinjectHttpApplication)filterContext.HttpContext.ApplicationInstance).Kernel.Get(typeof(ICustomerRepository)) as CustomerRepository;
            var customer = customerRepository.SelectUnique(cust => cust.Username == loggedInUser);

            if(Feature.AdminAccount.IsEnabled() && customer != null && customer.IsAdmin)
                return;
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                    {
                        {"controller", "Error"},
                        {"action", "Index"}
                    });
        }
    }
}