namespace PaymentProcessingSystem.Models;

public class CreditCardPayment : PaymentMethod
{
    public CreditCardPayment(decimal paymentAmount)
        : base(paymentAmount)
    {
    }

    public override void ProcessPayment()
    {
        Console.WriteLine("Checking the credit card...");
        Console.WriteLine("Credit card payment approved.");
    }
}
