using PaymentProcessingSystem.Models;

List<PaymentMethod> payments = new();
payments.Add(new CreditCardPayment(120.50m));
payments.Add(new PayPalPayment(45.99m));

foreach (PaymentMethod payment in payments)
{
    payment.DisplayPaymentInformation();
    payment.ProcessPayment();
    Console.WriteLine();
}
