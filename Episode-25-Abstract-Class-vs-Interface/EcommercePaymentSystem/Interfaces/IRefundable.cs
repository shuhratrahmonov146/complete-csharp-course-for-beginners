namespace EcommercePaymentSystem.Interfaces;

public interface IRefundable
{
    bool Refund(decimal amount);
}
