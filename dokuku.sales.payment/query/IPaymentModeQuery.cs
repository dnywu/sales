using System;
namespace dokuku.sales.payment
{
    public interface IPaymentModeQuery
    {
        PaymentMode Get(Guid id);
    }
}
