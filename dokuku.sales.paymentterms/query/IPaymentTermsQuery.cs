using System;
using System.Collections.Generic;
using dokuku.sales.paymentterms.model;
namespace dokuku.sales.paymentterms.query
{
    public interface IPaymentTermsQuery
    {
        PaymentTerms Get(Guid id, string ownerId);
        PaymentTerms FindByName(string name, string ownerId);
        PaymentTerms FindByNameAndId(string name, Guid id, string ownerId);
        PaymentTerms[] FindAll(string ownerId);
    }
}
