using System.Net;
using System.Web;
using System.Web.Mvc;

namespace VideoWorld.Controllers
{
    public class ErrorController : Controller
    {
        public HttpStatusCode Index()
        {
            return HttpStatusCode.Forbidden;
        }
    }
}