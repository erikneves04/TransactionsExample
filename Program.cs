using System;
using TransactionsExample.Data.Repository;
using TransactionsExample.Domain.Examples;

namespace TransactionsExample;

public class Program
{
    public static void Main()
    {
        var bankExamples = new BankExamples(new BankAccountRepository());

        bankExamples.NotFoundAccountFailure_WithoutTransaction();
        bankExamples.NotFoundAccountFailure_WithTransaction();

        var messageExamples = new MessageExamples(new MessageRepository());

        messageExamples.MessageSendFailure_WithTransaction();
        messageExamples.MessageSendFailure_WithoutTransaction();
    }
}