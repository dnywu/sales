using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.commands;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;
using dokuku.sales.invoices.domain;

namespace dokuku.sales.invoice.services
{
    public class AddInvoiceDiscuntService : CommandExecutorBase<AddInvoiceDiscount>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, AddInvoiceDiscount command)
        {
            Invoice invoice = context.GetById<Invoice>(command.InvoiceId, command.KnownVersion);
            invoice.GiveDiscount(command.DiscountInPercent);
            context.Accept();
        }
    }
}
