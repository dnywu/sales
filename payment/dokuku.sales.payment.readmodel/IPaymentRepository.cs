using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.payment.readmodel
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> FindAll(string ownerId);
    }
}