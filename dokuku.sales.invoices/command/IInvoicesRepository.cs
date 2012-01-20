using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
namespace dokuku.sales.invoices.command
{
    public interface IInvoicesRepository
    {
        void Save(Invoices invoice);
        void UpdateInvoices(Invoices invoice);
        Invoices Get(string id, string ownerId);
        void Delete(string id, string ownerId);
        Invoices GetInvByNumber(string invoiceNumber, string ownerId);
    }
}