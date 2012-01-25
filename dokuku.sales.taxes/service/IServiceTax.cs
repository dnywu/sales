using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.taxes.model;

namespace dokuku.sales.taxes.service
{
    public interface IServiceTax
    {
        void Create(Taxes tax);
        void Update(Taxes tax);
        void Delete(Guid guid);
    }
}
