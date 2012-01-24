using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.payment.domain;
namespace dokuku.sales.payment.repository
{
    public interface IPaymentRepository
    {
        void Save(InvoicePayment invoicePayment);
        void Update(InvoicePayment invoicePayment);
    }
}
