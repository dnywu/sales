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
using NServiceBus;
using dokuku.sales.invoice.messages;
using MongoDB.Bson;
namespace dokuku.sales.invoices.service
{
    public class InvoiceService : IInvoiceService
    {
        IInvoicesRepository invRepo;
        IInvoiceAutoNumberGenerator gen;
        MongoConfig mongo;
        IBus bus;

        public InvoiceService(IInvoicesRepository invoiceRepository, IInvoiceAutoNumberGenerator invNumberGenerator, MongoConfig mongo, IBus bus)
        {
            this.invRepo = invoiceRepository;
            this.gen = invNumberGenerator;
            this.mongo = mongo;
            this.bus = bus;
        }

        public Invoices Create(string jsonInvoice, string ownerId)
        {
            string data = jsonInvoice;
            Invoices invoice = JsonConvert.DeserializeObject<Invoices>(data);
            string invoiceNumber = gen.GenerateInvoiceNumberDraft(ownerId);
            FailIfInvoiceNumberAlreadyUsed(invoiceNumber,ownerId);

            invoice._id = Guid.NewGuid();
            invoice.OwnerId = ownerId;
            invoice.InvoiceNo = invoiceNumber;
            invRepo.Save(invoice);

            if (bus != null)
                bus.Publish<InvoiceCreated>(new InvoiceCreated { InvoiceJson = invoice.ToJson<Invoices>() });

            return invoice;
        }

        public void Delete(string id, string ownerId)
        {
            IsInvoiceStatusDraft(id, ownerId);
            invRepo.Delete(id, ownerId);
        }

        private void IsInvoiceStatusDraft(string id, string ownerId)
        {
            Invoices invoice = invRepo.Get(id, ownerId);
            if (invoice.Status.ToLower() != "draft")
                throw new Exception("Hapus invoice gagal, status invoice bukan draft");
		}
		
        private void FailIfInvoiceNumberAlreadyUsed(string invoiceNumber,string ownerId)
        {
            Invoices inv = invRepo.GetInvByNumber(invoiceNumber, ownerId);
            if (inv != null)
                throw new ApplicationException("Invoice " + invoiceNumber + " sudah digunakan!");
        }
    }
}