using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.repository
{
    public interface IOrganizationRepository
    {
        Currency GetOrganizationBaseCurrency(string ownerId);
    }
}
