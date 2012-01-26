using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.currency.model;

namespace dokuku.sales.currency.service
{
    public interface ICurrencyService
    {
        Currencies Create(string data, string ownerId);
        void Delete(Guid id);
    }
}
