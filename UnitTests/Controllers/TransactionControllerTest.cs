using System;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Controllers
{
    class TransactionControllerTest
    {
        private ICustomerRepository customerRepository;
        private TransactionRepository repository;
        private TransactionController controller;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customerRepository = new CustomerRepository();
            customer = new Customer("Test customer", "test", "password");
            customerRepository.Add(customer);
            repository = new TransactionRepository();
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<TransactionController>(repository, customerRepository);
            controller.Session["CurrentUser"] = "test";
        }

        [Test]
        public void ShouldShowTransactionHistory()
        {
            var s1 = new Transaction(customer, DateTime.Now, customer.Cart.Rentals);
            var s2 = new Transaction(customer, DateTime.Now.AddHours(1), customer.Cart.Rentals);

            repository.Add(s1);
            repository.Add(s2);

            ViewResult result = controller.Index();
            Assert.That(result.Model, Contains.Item(s1));
            Assert.That(result.Model, Contains.Item(s2));
        }
    }
}
