using System;
using System.Collections.Generic;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.query
{
    public interface IPaymentModeQuery
    {
        PaymentMode Get(Guid id);
        PaymentMode FindByName(string name);
        PaymentMode FindByNameAndId(string name, Guid id);
        IEnumerable<PaymentMode> FindAll();
    }
}
