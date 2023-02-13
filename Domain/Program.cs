using Data.ContextDb;
using Domain.Examples;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        var backExamples = new BankExamples();

        backExamples.NotFoundAccountFailure_WithoutTransaction();
        backExamples.NotFoundAccountFailure_WithTransaction();
    }
}