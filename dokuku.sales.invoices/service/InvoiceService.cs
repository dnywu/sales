using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.model;
using Newtonsoft.Json;
using dokuku.sales.config;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace dokuku.sales.invoices.service
{
    public class InvoiceService : IInvoiceService
    {
        IInvoicesRepository invRepo;
        IInvoiceAutoNumberGenerator gen;
        MongoConfig mongo;

        public InvoiceService(IInvoicesRepository invoiceRepository, IInvoiceAutoNumberGenerator invNumberGenerator, MongoConfig mongo)
        {
            this.invRepo = invoiceRepository;
            this.gen = invNumberGenerator;
            this.mongo = mongo;
        }

        public Invoices Create(string jsonInvoice, string ownerId)
        {
            string data = jsonInvoice;
            Invoices invoice = JsonConvert.DeserializeObject<Invoices>(data);
            string invoiceNumber = gen.GenerateInvoiceNumberDraft(ownerId);
            invoice._id = invoiceNumber;
            invoice.OwnerId = ownerId;
            invoice.InvoiceNo = invoiceNumber;
            invRepo.Save(invoice);

            return invoice;
        }
    }
}