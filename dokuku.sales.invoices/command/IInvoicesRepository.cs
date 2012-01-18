using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
namespace dokuku.sales.invoices.command
{
    public interface IInvoicesRepository
    {
        void Save(Invoices ci);
        Invoices Get(Guid id);
        void Delete(Guid id);
    }
}