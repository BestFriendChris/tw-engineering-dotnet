using System;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public delegate bool Specification<T>(T obj);

    public static class CustomerSpecification
    {
        public static Specification<Customer> ByUserName(string username)
        {
            return customer => customer.Username.Equals(username);
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
            return rental => (rental.Customer.Username.Equals(customer.Username));
        }
    }

}