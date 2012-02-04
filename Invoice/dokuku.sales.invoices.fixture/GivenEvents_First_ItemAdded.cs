using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.events;
namespace dokuku.sales.invoices.fixture
{
    public class GivenEvents_First_ItemAdded : GivenEvents_InvoiceCreated
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
                        DiscountInPercent = 10,
                        TaxCode = "NONE",
                        Summary = new Summary()
                        {
                            SubTotal = 4500000,
                            DiscountTotal = 0,
                            NetTotal = 4500000,
                            Taxes = new TaxSummary[] { }
                        },
                        Total = 4500000,
                        ItemNumber = 1
                    }};
                return base.Events.Concat(result).ToArray();
            }
        }
    }
}