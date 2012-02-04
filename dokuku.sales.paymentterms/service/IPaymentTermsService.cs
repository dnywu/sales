using System;
using System.Collections.Generic;
using dokuku.sales.paymentterms.model;
namespace dokuku.sales.paymentterms.service
{
    public interface IPaymentTermsService
    {
        PaymentTerms Insert(string json,string ownerId);
        PaymentTerms Update(string json, string ownerId);
        void Delete(Guid id);
    }
}
