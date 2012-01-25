using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.model;

namespace dokuku.sales.taxes.service
{
    public interface IServiceTax
    {
        Taxes Create(string taxJson,string ownerId);
        void Update(Taxes tax, string ownerId);
        void Delete(Guid guid);
    }
}
