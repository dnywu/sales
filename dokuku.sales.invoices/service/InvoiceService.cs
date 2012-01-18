using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;
namespace dokuku.sales.invoices.service
{
    public class InvoiceService : IInvoiceService
    {
        IInvoicesRepository invRepo;
        IInvoiceAutoNumberGenerator gen;

        public InvoiceService(IInvoicesRepository invoiceRepository, IInvoiceAutoNumberGenerator invNumberGenerator)
        {
            this.invRepo = invoiceRepository;
            this.gen = invNumberGenerator;
        }

        public Invoices Create(string jsonInvoice, string ownerId)
        {
            string data = jsonInvoice;
            Invoices invoice = JsonConvert.DeserializeObject<Invoices>(data);
            string invoiceNumber = gen.GenerateInvoiceNumber(invoice.InvoiceDate, ownerId);
            invoice._id = invoiceNumber;
            invoice.OwnerId = ownerId;
            invoice.InvoiceNo = invoiceNumber;
            invRepo.Save(invoice);

            return invoice;
        }
    }
}