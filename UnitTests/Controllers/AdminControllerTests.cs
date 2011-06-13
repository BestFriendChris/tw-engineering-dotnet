using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;
using VideoWorld.ViewModels;

namespace UnitTests.Controllers
{
    class AdminControllerTests
    {
        private CustomerRepository customerRepository;
        private AdminController controller;
        private readonly Customer james = new Customer("James Madison", "jmadison", "jm");
        private readonly Customer ben = new Customer("Ben Franklin", "bfranklin", "bf");

        [SetUp]
        public void SetUp()
        {
            customerRepository =new CustomerRepository();
            var customers = new List<Customer>{ james, ben };
            customerRepository.Add(customers);
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<AdminController>(customerRepository);
        }

        [Test]
        public void ShouldShowAdminPage()
        {
            var viewResult = controller.Index();
            viewResult.AssertViewRendered().ForView("Index");
            Assert.That(((List<Customer>)viewResult.Model).Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldAddNewCustomer()
        {
            var model = new NewCustomerViewModel { DisplayName = "Zack Taylor", Username = "ztaylor", Password1 = "zt", Password2 = "zt" };
            var result = controller.AddNewCustomer(model) as ViewResult;
            result.AssertViewRendered().ForView("Index");
        }

        [Test]
        public void ShouldThrowErrorIfUsernameAlreadyExists()
        {
            var model = new NewCustomerViewModel { DisplayName = "James Madison", Username = "jmadison", Password1 = "jm", Password2 = "jm" };
            var result = controller.AddNewCustomer(model) as ViewResult;
            result.AssertViewRendered().ForView("AddCustomer");
            Assert.That(((NewCustomerViewModel)result.Model).ErrorMessage, Is.EqualTo(string.Format("Customer with username {0} already exists",model.Username)));
        }
        
    }
}