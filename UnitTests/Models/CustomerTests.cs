using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using VideoWorld.Models;
using VideoWorld.Utils;

namespace UnitTests.Models
{
    internal class CustomerTests
    {
        private Customer customer;
        private List<Rental> mixedRentals;
        private DateTime startDate;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer("John Smith", "jsmith", "password");

            var montyPython = new Movie("Monty Python and the Holy Grail", new RegularPrice());
            var ran = new Movie("Ran", new RegularPrice());
            var laConfidential = new Movie("LA Confidential", new NewReleasePrice());
            var starTrek = new Movie("Star Trek 13.2", new NewReleasePrice());
            var wallaceAndGromit = new Movie("Wallace and Gromit", new ChildrensPrice());

            startDate = DateTime.Now;
            mixedRentals = new List<Rental>
                               {
                                   new Rental(montyPython, Period.Of(startDate, Duration.OfDays(3)) , customer),
                                   new Rental(ran, Period.Of(startDate, Duration.OfDays(1)),customer),
                                   new Rental(laConfidential, Period.Of(startDate, Duration.OfDays(2)),customer),
                                   new Rental(starTrek, Period.Of(startDate, Duration.OfDays(1)),customer),
                                   new Rental(wallaceAndGromit, Period.Of(startDate, Duration.OfDays(6)),customer)
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

        [Test]
        public void CreateAdminUser()
        {
            SetCurrentIteration("2");
            var adminUser = Customer.CreateAdminUser("Test Admin", "admin", "password");
            Assert.True(adminUser.IsAdmin);
        }

        [Test, ExpectedException(typeof(NotSupportedException), ExpectedMessage = "Admin account feature is not enabled")]
        public void CreateAdminUserThrowsExceptionForIteration1()
        {
            SetCurrentIteration("1");
            Customer.CreateAdminUser("Fake Admin", "i1Admin", "pword");
        }

        private static void SetCurrentIteration(string iteration)
        {
            ConfigurationManager.AppSettings["CurrentIteration"] = iteration;
        }
    }
}