using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcFakes;
using NUnit.Framework;
using VideoWorld.Controllers;

namespace UnitTests.Controllers
{
    class LoginControllerTests
    {

        [Test]
        public void ShouldShowLoginPageForUser()
        {
            var controller = new LoginController();
            Assert.That(controller.Index().ViewName, Is.EqualTo("Index"));
        }

//        [Test]
//        public void ShouldredirectToHomePageWhenUserLogsIn()
//        {
//            var controller = new LoginController();
//            controller.ControllerContext = new FakeControllerContext(controller);
//            var redirect = (RedirectResult) controller.Login("username");
//            Assert.That(redirect.Url, Is.EqualTo("/"));
//            Assert.That(controller.Session["CurrentUser"], Is.EqualTo("username"));
//        }
    }
}
