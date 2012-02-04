using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.repository;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.fixture
{
    public class FakeOrganizationRepository : IOrganizationRepository
    {
        public Currency GetOrganizationBaseCurrency(string ownerId)
        {
            return new Currency("IDR", 2);
        }
    }
}
