using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.events;
using Ncqrs.Spec;
using Ncqrs;
using Ncqrs.Commanding;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.domain;
namespace dokuku.sales.invoices.fixture
{
    public class GivenEvents_InvoiceCreated
    {
        public virtual Object[] Events
        {
            get
            {
                return new Object[1]
                {
                    new InvoiceCreated
                    {
                        InvoiceId = new Guid("AD4DB777-3329-46E9-9712-04465DED0722"),
                        CustomerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC"),
                        DecimalPlace = 2,
                        BaseCurrency = "IDR",
                        PONo = "PO-001",
                        OwnerId = "mart@y.c",
                        UserName = "marthin",
                        Status = InvoiceStatus.DRAFT,
                        TermCode = "001",
                        InvoiceDate = DateTime.Now.Date,
                        DueDate = DateTime.Now.Date.AddDays(7),
                        InvoiceNo = "DRAFT-1",
                        Rate = 1,
                        TransactionCurrency = "IDR"
                    }
                };
            }
        }
    }
}