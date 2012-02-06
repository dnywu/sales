using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.domain
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public Currency Currency { get; private set; }
        public Term Term { get; private set; }
        public Customer(Guid id, Currency ccy, Term term)
        {
            this.Id = id;
            this.Currency = ccy;
            this.Term = term;
        }
    }
}
