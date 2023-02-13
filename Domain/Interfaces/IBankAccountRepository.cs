using Domain.Entities;
using Domain.Interfaces;

namespace TransactionsExample.Domain.Interfaces;

public interface IBankAccountRepository : IGenericRepository<BankAccount>
{
}