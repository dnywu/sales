using System;
using System.Collections.Generic;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.service
{
    public interface IPaymentModeService
    {
        PaymentMode Insert(string json);
        PaymentMode Update(string json);
        IEnumerable<PaymentMode> FindAll();
        PaymentMode Get(Guid id);
        void Delete(Guid id);
        void FailedIfNameAlreadyExistsOnInsert(PaymentMode paymentMode);
        void FailedIfNameAlreadyExistsOnUpdate(PaymentMode paymentMode);        
    }
}
