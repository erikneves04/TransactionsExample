using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Domain.Entities;

public class BankAccount : Entity
{
    public BankAccount(string owner, decimal amount)
    {
        Owner = owner;
        Amount = amount;
    }

    public string Owner { get; set; }
    public decimal Amount { get; set; }

    public decimal Take(decimal amount)
    {
        if (amount > Amount)
            throw new InvalidOperationException("Insufficient funds");

        Amount -= amount;
        return amount; 
    }

    public void Deposit(decimal amount)
    {
        Amount += amount;
    }
}