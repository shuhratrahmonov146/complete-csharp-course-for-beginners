using EcommercePaymentSystem.Interfaces;

namespace EcommercePaymentSystem.Models;

public class CreditCardPayment : PaymentMethod,
    IRefundable, IReceiptSender
{
    public string LastDigits { get; }

    public CreditCardPayment(string paymentId, decimal amount,
        string lastDigits)
        : base(paymentId, amount)
    {
        LastDigits = lastDigits;
    }

    public override void ProcessPayment()
    {
        Console.WriteLine($"Charging card ending in {LastDigits}.");
    }

    public bool Refund(decimal amount)
    {
        if (amount <= 0 || amount > Amount)
        {
            Console.WriteLine($"Refund of {amount:F2} USD rejected.");
            return false;
        }

        Console.WriteLine($"Refunded {amount:F2} USD to the card.");
        return true;
    }

    public void SendReceipt()
    {
        Console.WriteLine($"Receipt emailed for {PaymentId}.");
    }
}
