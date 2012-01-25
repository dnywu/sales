using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.model;
namespace dokuku.sales.taxes.query
{
    public interface ITaxQueryRepository
    {
        Taxes GetTaxById(Guid guid, string ownerId);
        IEnumerable<Taxes> GetAllTaxes(string OwnerId);
    }
}
