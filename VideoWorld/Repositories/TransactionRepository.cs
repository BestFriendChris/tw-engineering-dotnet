using System.Collections.Generic;
using System.Linq;
using VideoWorld.Models;

namespace VideoWorld.Repositories
{
    public class TransactionRepository
    {
        readonly List<Transaction> transactions = new List<Transaction>();

        public Transaction FindById(int id)
        {
            return transactions[id];
        }

        public int Add(Transaction transaction)
        {
            transactions.Add(transaction);
            return transactions.Count - 1;
        }

        public IEnumerable<Transaction> TransactionsBy(Customer customer)
        {
            return transactions.Where(s => s.Customer == customer);
        }
    }
}