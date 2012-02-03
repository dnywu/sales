using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.repository;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.fixture
{
    public class FakeExchangeRateRepository : IExchangeRateRepository
    {
        public ExchangeRate GetRate(DateTime dateTime,string transCurrencyCode, string ownerId)
        {
            return new ExchangeRate(new Currency("IDR", 2), new Currency("IDR", 2), 2);
        }
    }
}
