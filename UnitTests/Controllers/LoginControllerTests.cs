using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace UnitTests.Controllers
{
    class LoginControllerTests
    {
        private LoginController loginController;
        private ICustomerRepository customerRepository;

        [SetUp]
        public void SetUp()
        {
            customerRepository = new ListBasedCustomerRepository();
            customerRepository.Add(new Customer("Test Customer","username","password"));
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
            var redirect = (RedirectResult) loginController.Login("username", "password");
            Assert.That(redirect.Url, Is.EqualTo("/"));
            Assert.That(loginController.Session["CurrentUser"], Is.EqualTo("username"));
        }

        [Test]
        public void ShouldReturnLoginWhenProvidedNoCustomerName()
        {
            var actionResult = loginController.Login("","") as ViewResult;
            actionResult.AssertViewRendered().ForView("Index");
            var viewModel = actionResult.Model as LoginViewModel;
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Username cannot be empty"));
        }
    }
}
