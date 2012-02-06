using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using dokuku.sales.invoices.domain;
using dokuku.sales.invoices.commands;
using Ncqrs;
namespace dokuku.sales.invoice.services
{
    public class AddInvoiceItemService : Ncqrs.Commanding.CommandExecution.CommandExecutorBase<AddInvoiceItem>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, AddInvoiceItem command)
        {
            ITaxRepository taxRepo = NcqrsEnvironment.Get<ITaxRepository>();
            Tax tax = taxRepo.FindByCode(command.TaxCode, command.OwnerId);
            
            Invoice invoice = context.GetById<Invoice>(command.InvoiceId, command.KnownVersion);
            invoice.AddInvoiceItem(command.ItemId, command.Description, command.Quantity, command.Price, command.DiscountInPercent, tax, command.OwnerId,command.UserName);
            context.Accept();
        }
    }
}