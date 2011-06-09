using System.Collections.Generic;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Controllers
{
    class AdminControllerTests
    {
        private Mock<ICustomerRepository> mockedCustomerRepository;
        private AdminController controller;

        [SetUp]
        public void SetUp()
        {
            mockedCustomerRepository =new Mock<ICustomerRepository>();
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<AdminController>(mockedCustomerRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            mockedCustomerRepository.VerifyAll();
        }

        [Test]
        public void ShouldShowAdminPage()
        {
            var customers = new List<Customer> {new Customer(), new Customer()};
            mockedCustomerRepository.Setup(repository => repository.SelectAllInAlphabeticalOrder()).Returns(customers);
            var viewResult = controller.Index();
            viewResult.AssertViewRendered().ForView("Index");
            Assert.That(viewResult.Model is List<Customer>);
            Assert.That(((List<Customer>)viewResult.Model).Count, Is.EqualTo(customers.Count));
        }
    }
}