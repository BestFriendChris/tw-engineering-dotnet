using System;
using System.Linq;
using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Repositories;

namespace UnitTests.Repositories
{
    class TransactionRepositoryTests
    {
        [Test]
        public void ShouldProvideTransactionsForCustomer()
        {
            var repository = new TransactionRepository();

            var c1 = new Customer("One", null, null);
            var c2 = new Customer("Two", null, null);

            var transaction = new Transaction(c1, DateTime.Now, c1.Cart.Rentals);
            repository.Add(transaction);

            repository.Add(new Transaction(c2, DateTime.Now.AddHours(1), c2.Cart.Rentals));

            var c1transactions = repository.TransactionsBy(c1);

            Assert.That(c1transactions.Contains(transaction));
            Assert.That(c1transactions.Count(), Is.EqualTo(1));
        }
    }
}
