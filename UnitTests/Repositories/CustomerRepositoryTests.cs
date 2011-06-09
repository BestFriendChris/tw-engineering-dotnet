using System.Collections.Generic;
using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Repositories
{
    class CustomerRepositoryTests
    {
        private CustomerRepository customerRepository;
        private readonly Customer mFaraday = new Customer("M Faraday", "mf", "mf-pw");
        private readonly Customer aGrahambell = new Customer("A Grahambell", "ag", "ag-pw");
        private readonly Customer jWatt = new Customer("J Watt", "jw", "jw-pw");

        [SetUp]
        public void SetUp()
        {
            customerRepository = new CustomerRepository();
            customerRepository.Add(new List<Customer>{mFaraday, aGrahambell, jWatt});
        }

        [Test]
        public void ShouldOrderCustomersInAlphabeticalOrder()
        {
            var customers = customerRepository.SelectAllInAlphabeticalOrder();
            Assert.That(customers.Count, Is.EqualTo(3));
            Assert.That(customers[0], Is.EqualTo(aGrahambell));
            Assert.That(customers[1], Is.EqualTo(jWatt));
            Assert.That(customers[2], Is.EqualTo(mFaraday));
        }
    }
}