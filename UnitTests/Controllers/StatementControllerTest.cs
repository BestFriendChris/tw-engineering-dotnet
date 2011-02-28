using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using VideoWorld.Controllers;
using VideoWorld.Models;

namespace UnitTests.Controllers
{
    class StatementControllerTest
    {
        private CustomerRepository customerRepository;
        private StatementRepository repository;
        private StatementsController controller;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customerRepository = new CustomerRepository();
            customer = new Customer("Test customer");
            customerRepository.Add(customer);
            repository = new StatementRepository();
            var builder = new TestControllerBuilder();
            controller = builder.CreateController<StatementsController>(repository, customerRepository);
            controller.Session["CurrentUser"] = "Test customer";
        }

        [Test]
        public void ShouldRedirectToStatementView()
        {
            RedirectResult redirect = controller.Create();
            Assert.That(redirect.Url, Is.StringMatching(@"/statements/\d+"));
        }

        [Test]
        public void ShouldCreateStatementWithSameCustomer()
        {
            controller.Create();
            Assert.That(repository.FindById(0).Customer, Is.SameAs(customer));
        }

        [Test]
        public void ShouldRecordStatement()
        {
            controller.Create();
            Assert.That(repository.FindById(0).Text, Contains.Substring("Amount charged is"));
        }

        [Test]
        public void ShouldClearCartOnCheckout()
        {
            customer.Cart.AddMovie(new Movie("Mad Max 2", new NewReleasePrice()), 1);
            Assert.That(customer.Cart.Count, Is.EqualTo(1));
            controller.Create();
            Assert.That(customer.Cart.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldPopulatestatementModel()
        {
            var statement = new Statement(customer);
            repository.Add(statement);
            ViewResult result = controller.Show(0);
            Assert.That(result.Model, Is.SameAs(statement));
        }

        [Test]
        public void ShouldShowStatementHistory()
        {
            var s1 = new Statement(customer);
            var s2 = new Statement(customer);

            repository.Add(s1);
            repository.Add(s2);

            ViewResult result = controller.Index();
            Assert.That(result.Model, Contains.Item(s1));
            Assert.That(result.Model, Contains.Item(s2));
        }
    }
}
