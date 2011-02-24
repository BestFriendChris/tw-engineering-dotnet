using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;

namespace UnitTests.Controllers
{
    class LoginControllerTests
    {
        private LoginController loginController;
        private CustomerRepository customerRepository;

        [SetUp]
        public void SetUp()
        {
            customerRepository = new CustomerRepository();
            var builder = new TestControllerBuilder();
            loginController = builder.CreateController<LoginController>(customerRepository);
        }

        [Test]
        public void ShouldShowLoginPageForUser()
        {
            var controller = loginController;
            Assert.That(controller.Index().ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ShouldredirectToHomePageWhenUserLogsIn()
        {
            RedirectResult redirect = loginController.Login("username");
            Assert.That(redirect.Url, Is.EqualTo("/"));
            Assert.That(loginController.Session["CurrentUser"], Is.EqualTo("username"));
        }

        [Test]
        public void ShouldAddCustomerToRepostoryWhenUserLogsIn()
        {
            var redirect = (RedirectResult)loginController.Login("username");
            Assert.That(customerRepository.FindByName("username"), Is.Not.Null);
        }
    }
}
