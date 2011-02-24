using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace VideoWorld.Controllers
{
    public class LoginController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Index")]
        public RedirectResult Login(string username)
        {
            Session["CurrentUser"] = username;
            return Redirect("/");
        }
    }
}