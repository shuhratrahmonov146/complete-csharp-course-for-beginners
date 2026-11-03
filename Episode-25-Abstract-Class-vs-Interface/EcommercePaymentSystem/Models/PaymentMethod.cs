namespace EcommercePaymentSystem.Models;

public abstract class PaymentMethod
{
    public string PaymentId { get; }
    public decimal Amount { get; }

    protected PaymentMethod(string paymentId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        PaymentId = paymentId;
        Amount = amount;
    }

    public void DisplayPaymentInformation()
    {
        Console.WriteLine($"Payment ID: {PaymentId}");
        Console.WriteLine($"Amount: {Amount:F2} USD");
    }

    public abstract void ProcessPayment();
}
