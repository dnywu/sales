using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public interface ITaxRepository
    {
        Tax FindByCode(string taxCode, string ownerId);
    }
}