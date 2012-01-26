using System;
using System.Collections.Generic;
using dokuku.sales.paymentmode.model;
namespace dokuku.sales.paymentmode.query
{
    public interface IPaymentModeQuery
    {
        PaymentModes Get(Guid id, string ownerId);
        PaymentModes FindByName(string name,string ownerId);
        PaymentModes FindByNameAndId(string name, Guid id,string ownerId);
        IEnumerable<PaymentModes> FindAll(string ownerId);
    }
}
