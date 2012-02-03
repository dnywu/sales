using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoices.fixture
{
    [Specification]
    public class When_add_item : OneEventTestFixture<AddInvoiceItem, InvoiceItemAdded>
    {
        Guid _invoiceid;
        Guid _customerId;
        public When_add_item()
        {
            this.SetupInvoiceFixture();
            _customerId = new Guid("DCCD617E-6083-4FAA-A328-15ADD3771DBC");
            _invoiceid = new Guid("AD4DB777-3329-46E9-9712-04465DED0722");
        }
        protected override IEnumerable<object> GivenEvents()
        {
            return new List<InvoiceCreated>() { 
                new InvoiceCreated
                {
                    InvoiceId = _invoiceid,
                    CustomerId = _customerId,
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
        protected override AddInvoiceItem WhenExecuting()
        {
            
        }
    }
}
