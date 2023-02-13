using Domain.Examples;

public class Program
{
    public static void Main()
    {
        var backExamples = new BankExamples();

        backExamples.NotFoundAccountFailure_WithoutTransaction();
        backExamples.NotFoundAccountFailure_WithTransaction();
    }
}