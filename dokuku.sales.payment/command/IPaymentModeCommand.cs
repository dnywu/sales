using System;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.command
{
    public interface IPaymentModeCommand
    {
        void Save(PaymentMode paymentMode);
        void Update(PaymentMode paymentMode);
        void Delete(Guid id);
    }
}