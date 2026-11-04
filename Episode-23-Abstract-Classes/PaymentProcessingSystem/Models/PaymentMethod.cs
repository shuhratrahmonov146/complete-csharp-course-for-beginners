namespace PaymentProcessingSystem.Models;

public abstract class PaymentMethod
{
    public decimal PaymentAmount { get; }

    protected PaymentMethod(decimal paymentAmount)
    {
        PaymentAmount = paymentAmount;
    }

    public void DisplayPaymentInformation()
    {
        Console.WriteLine($"Amount: {PaymentAmount:F2} USD");
    }

    public abstract void ProcessPayment();
}
