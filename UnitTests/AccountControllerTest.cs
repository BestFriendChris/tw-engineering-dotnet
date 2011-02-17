using System.Web.Mvc;
using Moq;
using MvcApplication.Controllers;
using MvcApplication.Models;
using NUnit.Framework;

namespace UnitTests
{
    class AccountControllerTest
    {
        [TestCase]
        public void ShouldReturnViewIfNotLoggedIn()
        {
            var controller = new AccountController();
            var actionResult = (ViewResult)controller.LogOn();
            Assert.AreEqual("LogOn", actionResult.ViewName);
        }

        [TestCase]
        public void ShouldShowErrorIfLogOnFails()
        {
            var membershipService = new Mock<IMembershipService>();

            var controller = new AccountController
                                 {
                                     MembershipService = membershipService.Object
                                 };

            var logOnModel = new LogOnModel {UserName = "Unknown", Password = "WrongPassword", RememberMe = true};

            var actionResult = (ViewResult)controller.LogOn(logOnModel, null);
            
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);
            Assert.AreEqual(1, controller.ModelState.Count);
            Assert.AreEqual("FOO", controller.ModelState..Value);
        }
    }
}
