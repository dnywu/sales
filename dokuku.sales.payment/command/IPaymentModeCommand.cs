using System;
namespace dokuku.sales.payment
{
    public interface IPaymentModeCommand
    {
        void Save(PaymentMode paymentMode);
        void Update(PaymentMode paymentMode);
        void Delete(Guid id);
    }
}