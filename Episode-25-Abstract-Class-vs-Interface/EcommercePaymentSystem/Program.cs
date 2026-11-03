using EcommercePaymentSystem.Interfaces;
using EcommercePaymentSystem.Models;

List<PaymentMethod> payments = new();
payments.Add(new CreditCardPayment("P-1001", 120.50m, "4242"));
payments.Add(new PayPalPayment("P-1002", 45.99m, "ali@example.com"));

foreach (PaymentMethod payment in payments)
{
    payment.DisplayPaymentInformation();
    payment.ProcessPayment();

    if (payment is IReceiptSender sender)
    {
        sender.SendReceipt();
    }

    if (payment is IRefundable refundable)
    {
        refundable.Refund(20.00m);
    }
    else
    {
        Console.WriteLine("Refunds are not supported.");
    }

    Console.WriteLine();
}
