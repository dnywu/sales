using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;

namespace dokuku.sales.invoices.service
{
    public interface IInvoiceService
    {
        Invoices Create(string jsonInvoice, string ownerId);
        void Update(string jsonInvoice, string ownerId);
<<<<<<< HEAD
        void InvoiceFullyPaid(Guid invoiceId, string ownerId);
        void InvoicePartialyPaid(Guid invoiceId, string ownerId);
=======
        void Delete(Guid id, string ownerId);
>>>>>>> 29abdf5baa5c495ab06646fa6efc218ca7af0f2e
    }
}
