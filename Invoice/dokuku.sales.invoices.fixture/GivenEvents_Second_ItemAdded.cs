using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using NUnit.Framework;

namespace dokuku.sales.invoices.fixture
{
    public class GivenEvents_Second_ItemAdded : GivenEvents_First_ItemAdded
    {
        public override Object[] Events
        {
            get
            {
                Object[] result = new Object[1] {new InvoiceItemAdded
                    {
                        OwnerId = "mart@y.c",
                        UserName = "marthin",
                        InvoiceId = new Guid("AD4DB777-3329-46E9-9712-04465DED0722"),
                        ItemId = new Guid("6D810987-F3A1-4826-8AFE-294B64C097F0"),
                        Description = "untuk beli hp samsung",
                        Quantity = 10,
                        Price = 500000,
                        DiscountInPercent = 0,
                        TaxCode = "PPN",
                        Summary = new Summary()
                        {
                            SubTotal = 9500000,
                            DiscountTotal = 0,
                            NetTotal = 10000000,
                            Taxes = new TaxSummary[] {new TaxSummary{ TaxCode = "PPN", TaxAmount = 500000} }
                        },
                        Total = 5000000,
                        ItemNumber = 2
                    }};
                return base.Events.Concat(result).ToArray();
            }
        }
    }
}
