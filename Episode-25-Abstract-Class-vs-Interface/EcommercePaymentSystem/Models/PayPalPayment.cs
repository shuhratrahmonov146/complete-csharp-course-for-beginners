using EcommercePaymentSystem.Interfaces;

namespace EcommercePaymentSystem.Models;

public class PayPalPayment : PaymentMethod, IReceiptSender
{
    public string Email { get; }

    public PayPalPayment(string paymentId, decimal amount,
        string email)
        : base(paymentId, amount)
    {
        Email = email;
    }

    public override void ProcessPayment()
    {
        Console.WriteLine($"Paying via PayPal: {Email}");
    }

    public void SendReceipt()
    {
        Console.WriteLine($"Receipt sent to {Email}.");
    }
}
