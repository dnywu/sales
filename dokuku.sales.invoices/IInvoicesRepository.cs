using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices
{
    public interface IInvoicesRepository
    {
        void Save(Invoices ci);
        Invoices Get(Guid id);
        void Delete(Guid id);
        IEnumerable<Invoices> AllInvoices();
    }
}
