using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.repository;
using Ncqrs;
using dokuku.sales.invoices.domain;
using dokuku.sales.config;

namespace dokuku.sales.invoice.services
{
    public class CreateInvoiceService : Ncqrs.Commanding.CommandExecution.CommandExecutorBase<CreateInvoice>
    {
        protected override void ExecuteInContext(Ncqrs.Domain.IUnitOfWorkContext context, CreateInvoice command)
        {
            string invoiceNumber = GenerateInvoiceNumber(command.OwnerId);
            Customer customer = GetCustomer(command.CustomerId);
            Currency baseCurrency = GetOrganizationBaseCurrency(command.OwnerId);
            new Invoice(command.InvoiceId,invoiceNumber, customer,baseCurrency, command.PONumber, command.OwnerId, command.UserName);
            context.Accept();
        }

        private string GenerateInvoiceNumber(string ownerId)
        {
            IInvoiceAutoNumberGenerator autonumber = NcqrsEnvironment.Get<IInvoiceAutoNumberGenerator>();
            return autonumber.GenerateInvoiceNumberDraft(ownerId);
        }

        private Customer GetCustomer(Guid customerId)
        {
            ICustomerRepository repo = NcqrsEnvironment.Get<ICustomerRepository>();
            return repo.GetCustomerById(customerId);
        }

        private Currency GetOrganizationBaseCurrency(string ownerId)
        {
            IOrganizationRepository organizationRepo = NcqrsEnvironment.Get<IOrganizationRepository>();
            return organizationRepo.GetOrganizationBaseCurrency(ownerId);
        }
    }
}
