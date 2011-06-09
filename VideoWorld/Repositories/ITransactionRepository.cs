using System.Collections.Generic;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> TransactionsBy(Customer customer);
    }
}