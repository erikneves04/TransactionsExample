using TransactionsExample.Domain.Entities;
using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Data.Repository;

public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
{
    public BankAccountRepository() 
        : base(new())
    {
    }
}