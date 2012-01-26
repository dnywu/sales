using System;
using System.Collections.Generic;
using dokuku.sales.paymentmode.model;
namespace dokuku.sales.paymentmode.service
{
    public interface IPaymentModeService
    {
        PaymentModes Insert(string json,string ownerId);
        PaymentModes Update(string json, string ownerId);
        void Delete(Guid id);
    }
}
