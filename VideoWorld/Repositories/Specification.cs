using System;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public delegate bool Specification<T>(T obj);

    public static class CustomerSpecification
    {
        public static Specification<Customer> ByUserName(string username)
        {
            return customer => customer.Username == username;
        } 
        
        public static Specification<Customer> ByUserNameAndPassword(string username, string password)
        {
            return customer => customer.IsUsernameAndPasswordValid(username, password);
        }

    }

    public static class RentalSpecification
    {
        public static Specification<Rental> ByCustomer(Customer customer)
        {
            return rental => (rental.Customer.Username == customer.Username);
        }
    }

    public static class TransactionSpecification
    {
        public static Specification<Transaction> ByCustomer(Customer customer)
        {
            return transaction => transaction.Customer.Equals(customer);
        }
    }

    public static class MovieSpecification
    {
        public static Specification<Movie> ByTitle(string title)
        {
            return movie => movie.Title == title;
        }
    }

}