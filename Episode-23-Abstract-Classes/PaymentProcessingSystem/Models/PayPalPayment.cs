namespace PaymentProcessingSystem.Models;

public class PayPalPayment : PaymentMethod
{
    public PayPalPayment(decimal paymentAmount)
        : base(paymentAmount)
    {
    }

    public override void ProcessPayment()
    {
        Console.WriteLine("Connecting to PayPal...");
        Console.WriteLine("PayPal payment completed.");
    }
}
