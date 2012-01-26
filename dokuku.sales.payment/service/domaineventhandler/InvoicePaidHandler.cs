using dokuku.sales.domainevents;
using dokuku.sales.payment.domainevents;
using NServiceBus;
using dokuku.sales.payment.messages;
using MongoDB.Bson;
using dokuku.sales.payment.events;
namespace dokuku.sales.payment.service
{
    public class InvoicePaidHandler : Handles<dokuku.sales.payment.domainevents.InvoicePaid>
    {
        public IBus Bus { get; set; }
        public void Handle(dokuku.sales.payment.domainevents.InvoicePaid args)
        {
            Bus.Publish(new dokuku.sales.payment.events.InvoicePaid 
            {
                 PaymentJson = args.ToJson()
            });
        }
    }
}