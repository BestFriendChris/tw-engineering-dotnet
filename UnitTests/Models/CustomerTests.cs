using System.Collections.Generic;
using NUnit.Framework;
using VideoWorld.Models;

namespace UnitTests.Models
{
    internal class CustomerTests
    {
        private Customer customer;
        private List<Rental> mixedRentals;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer("John Smith", "jsmith", "password");

            var montyPython = new Movie("Monty Python and the Holy Grail", new RegularPrice());
            var ran = new Movie("Ran", new RegularPrice());
            var laConfidential = new Movie("LA Confidential", new NewReleasePrice());
            var starTrek = new Movie("Star Trek 13.2", new NewReleasePrice());
            var wallaceAndGromit = new Movie("Wallace and Gromit", new ChildrensPrice());

            mixedRentals = new List<Rental>
                               {
                                   new Rental(montyPython, new Period(3)),
                                   new Rental(ran, new Period(1)),
                                   new Rental(laConfidential, new Period(2)),
                                   new Rental(starTrek, new Period(1)),
                                   new Rental(wallaceAndGromit, new Period(6))
                               };
        }

        [Test]
        public void Empty()
        {
            const string noRentalsStatement = "Rental Record for John Smith\n"
                                              + "Amount charged is $0.00\n"
                                              + "You have a new total of 0 frequent renter points";
            Assert.AreEqual(noRentalsStatement, customer.Statement(new List<Rental>()));
        }

        [Test]
        public void NonEmpty()
        {
            const string expected = "Rental Record for John Smith\n"
                                    + "  Monty Python and the Holy Grail  -  $3.50\n"
                                    + "  Ran  -  $2.00\n"
                                    + "  LA Confidential  -  $6.00\n"
                                    + "  Star Trek 13.2  -  $3.00\n"
                                    + "  Wallace and Gromit  -  $6.00\n"
                                    + "Amount charged is $20.50\n"
                                    + "You have a new total of 6 frequent renter points";

            Assert.AreEqual(expected, customer.Statement(mixedRentals));
        }

        [Test]
        public void TestPasswordValidated()
        {
            Assert.False(customer.IsUsernameAndPasswordValid("jsmith","incorrect"));
            Assert.False(customer.IsUsernameAndPasswordValid("incorrect","password"));
            Assert.True(customer.IsUsernameAndPasswordValid("jsmith","password"));
        }


    }
}